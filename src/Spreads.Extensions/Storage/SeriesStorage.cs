﻿/*
    Copyright(c) 2014-2016 Victor Baybekov.

    This file is a part of Spreads library.

    Spreads library is free software; you can redistribute it and/or modify it under
    the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 3 of the License, or
    (at your option) any later version.

    Spreads library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite.Utilities;
using Spreads.Collections;

namespace Spreads.Storage {

    public class SeriesStorage : ISeriesStorage {

        static SeriesStorage() {
            // init
            if (Bootstrap.Bootstrapper.Instance.BaseFolder == null) throw new ApplicationException();
        }

        private readonly SqliteConnection _connection;
        private readonly ConcurrentDictionary<long, object> _writableSeriesStore = new ConcurrentDictionary<long, object>();
        private readonly ConcurrentDictionary<long, object> _readOnlySeriesStore = new ConcurrentDictionary<long, object>();

        public string IdTableName { get; }
        public string ChunkTableName { get; }

        private static SqliteConnection CreateConnection(string connectionString, bool async = true) {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            //NativeMethods.sqlite3_exec(connection.DbHandle, "PRAGMA locking_mode=EXCLUSIVE; PRAGMA synchronous = 0; PRAGMA journal_mode = WAL;");
            connection.ExecuteNonQuery("PRAGMA main.page_size = 4096; ");
            connection.ExecuteNonQuery("PRAGMA main.cache_size = 25000;");
            connection.ExecuteNonQuery(!async ? "PRAGMA synchronous = NORMAL;" : "PRAGMA synchronous = OFF;");
            connection.ExecuteNonQuery("PRAGMA journal_mode = WAL;");
            return connection;
        }

        private static readonly string _defaultPath = Bootstrap.Bootstrapper.Instance.DataFolder;
        public static SeriesStorage GetDefault(string filename = "default.db") {
            return new SeriesStorage(GetDefaultConnectionString(filename));
        }

        public static string GetDefaultConnectionString(string filename = "default.db") {
            return $"Filename={filename}";
        }

        public SqliteConnection Connection
        {
            get { return _connection; }
        }

        public SeriesStorage(string SQLiteConnectionString) : this(CreateConnection(SQLiteConnectionString, true)) {

        }

        private SeriesStorage(SqliteConnection connection, string idTableName = "spreads_series_id", string chunkTableName = "spreads_series_chunk") {
            _connection = connection;
            IdTableName = idTableName;
            ChunkTableName = chunkTableName;

            var createSeriesIdTable =
                $"CREATE TABLE IF NOT EXISTS `{IdTableName}` (\n" +
                "  `Id` INTEGER  PRIMARY KEY,\n" +
                "  `TextId` TEXT NOT NULL,\n" +
                "  `UUID` BLOB NOT NULL,\n" +
                "  `KeyType` TEXT NOT NULL,\n" +
                "  `ValueType` TEXT NOT NULL,\n" +
                "  `Version` INTEGER NOT NULL,\n" +
                "  CONSTRAINT `UX_TextId` UNIQUE (`TextId`)\n" +
                "  CONSTRAINT `UX_UUID` UNIQUE (`UUID`)\n" +
                ")";
            var createSeriesChunksTable =
                $"CREATE TABLE IF NOT EXISTS `{ChunkTableName}` (\n" +
                "  `Id` INTEGER NOT NULL,\n" +
                "  `ChunkKey` INTEGER NOT NULL,\n" +
                "  `Count` INTEGER NOT NULL,\n" +
                "  `Version` INTEGER NOT NULL,\n" +
                "  `ChunkValue` BLOB NOT NULL,\n" +
                "  PRIMARY KEY (`Id`,`ChunkKey`)\n)";
            //NativeMethods.sqlite3_exec(connection.DbHandle, createSeriesIdTable);
            //NativeMethods.sqlite3_exec(connection.DbHandle, createSeriesChunksTable);
            _connection.Execute(createSeriesIdTable);
            _connection.Execute(createSeriesChunksTable);

        }

        internal virtual Task<SeriesChunk> LoadChunk(long mapid, long chunkId) {
            var sod = _connection.Query<SeriesChunk>("SELECT Id, ChunkKey, ChunkValue, Count, Version from " + ChunkTableName + "" + " WHERE Id = @Id AND ChunkKey = @Dt", new { Dt = chunkId, Id = mapid }).SingleOrDefault();
            if (sod != null) {
                return Task.FromResult(sod);
            }
            throw new KeyNotFoundException();
        }

        internal virtual Task<long> SaveChunk(SeriesChunk chunk) {
            var setSQL = @"INSERT OR REPLACE INTO " + ChunkTableName + " (Id,ChunkKey,Count,Version,ChunkValue)" + " VALUES ( @id, @chKey, @count, @version, @chVal); UPDATE " + IdTableName + " SET Version = @version WHERE Id = @id;";
            var processedTmp = _connection.Execute(setSQL, new {
                id = chunk.Id,
                chKey = chunk.ChunkKey,
                count = chunk.Count,
                version = chunk.Version,
                chVal = chunk.ChunkValue
            });
            if (processedTmp < 1) throw new ApplicationException("Cannot set value");
            return Task.FromResult(0L);
        }

        // mapId, chunkKey, direction => whole map version
        internal virtual Task<long> RemoveChunk(long mapId, long key, long version, Lookup direction) {
            bool r2 = false;
            string setSQL;
            int processedTmp;
            switch (direction) {
                case Lookup.EQ:
                    setSQL = @"DELETE FROM " + ChunkTableName + "" + " WHERE Id = @id AND ChunkKey = @dt; UPDATE " + IdTableName + " SET Version = @Version WHERE Id = @id;";
                    processedTmp = _connection.Execute(setSQL, new { id = mapId, dt = key, Version = version });
                    if (processedTmp == 0) {
                        // ReSharper disable once RedundantAssignment
                        r2 = false;
                    } else if (processedTmp > 1) {
                        throw new ApplicationException("Deleted more than one row");
                    } else {
                        r2 = true;
                    }
                    break;

                case Lookup.LT:
                    setSQL = @"DELETE FROM " + ChunkTableName + "" + " WHERE Id = @id AND ChunkKey < @dt; UPDATE " + IdTableName + " SET Version = @Version WHERE Id = @id;";
                    processedTmp = _connection.Execute(setSQL, new { id = mapId, dt = key, Version = version });
                    r2 = processedTmp > 0;
                    break;

                case Lookup.LE:
                    setSQL = @"DELETE FROM " + ChunkTableName + "" + " WHERE Id = @id AND ChunkKey <= @dt; UPDATE " + IdTableName + " SET Version = @Version WHERE Id = @id;";
                    processedTmp = _connection.Execute(setSQL, new { id = mapId, dt = key, Version = version });
                    r2 = processedTmp > 0;
                    break;

                case Lookup.GE:
                    setSQL = @"DELETE FROM " + ChunkTableName + "" + " WHERE Id = @id AND ChunkKey >= @dt; UPDATE " + IdTableName + " SET Version = @Version WHERE Id = @id;";
                    processedTmp = _connection.Execute(setSQL, new { id = mapId, dt = key, Version = version });
                    r2 = processedTmp > 0;
                    break;

                case Lookup.GT:
                    setSQL = @"DELETE FROM " + ChunkTableName + "" + " WHERE Id = @id AND ChunkKey > @dt; UPDATE " + IdTableName + " SET Version = @Version WHERE Id = @id;";
                    processedTmp = _connection.Execute(setSQL, new { id = mapId, dt = key, Version = version });
                    r2 = processedTmp > 0;
                    break;
            }
            // RemoteSeries treats positive as true
            return r2 ? Task.FromResult(version) : Task.FromResult(0L);
        }

        internal virtual Task<SortedMap<long, SeriesChunk>> LoadKeys(long mapid, long version) {
            var sm = new SortedMap<long, SeriesChunk>();
            var chunks = _connection.Query<SeriesChunk>("SELECT Id, ChunkKey, Count, Version from " + ChunkTableName + "" + " WHERE Id = @Id ORDER BY ChunkKey", new { Id = mapid });
            foreach (var ch in chunks) {
                sm.AddLast(ch.ChunkKey, ch);
            }
            return Task.FromResult(sm);
        }

        public async Task FlushAll(int delay) {
            foreach (var scm in _writableSeriesStore.Values.Select(obj => obj as IPersistentObject)) {
                if(delay > 0) await Task.Delay(delay);
                scm?.Flush();
            }
        }


        public IPersistentOrderedMap<K, V> GetPersistentOrderedMap<K, V>(string seriesId, bool readOnly = false) {
            seriesId = seriesId.ToLowerInvariant().Trim();
            var idRow = GetExtendedSeriesId<K, V>(seriesId);
            if (readOnly) {
                return _readOnlySeriesStore.GetOrAdd(idRow.Id, id2 => {
                    return GetSeries<K, V>(id2, idRow.Version, true);
                }) as IPersistentOrderedMap<K, V>;
            }
            return _writableSeriesStore.GetOrAdd(idRow.Id, id2 => {
                return GetSeries<K, V>(id2, idRow.Version, false);
            }) as IPersistentOrderedMap<K, V>;
        }


        internal long GetLongId<K, V>(string seriesId) {
            seriesId = seriesId.ToLowerInvariant().Trim();
            var keyType = typeof(K).FullName;
            var valueType = typeof(V).FullName;
            
            var seriesIdRow = _connection.Query<SeriesId>("SELECT Id, TextId, UUID, KeyType, ValueType, Version from " + IdTableName + "" + " WHERE TextId = @TextId", new { TextId = seriesId }, buffered: false).SingleOrDefault();
            if (seriesIdRow != null) {
                if (seriesIdRow.KeyType != keyType || seriesIdRow.ValueType != valueType) {
                    throw new ArgumentException(
                        $"Wrong types for {seriesId}: expexting <{seriesIdRow.KeyType},{seriesIdRow.ValueType}> but requested <{keyType},{valueType}>");
                }
                return seriesIdRow.Id;
            }

            var setSql = @"INSERT INTO " + IdTableName + " (TextId, UUID, KeyType, ValueType, Version) VALUES ( @TextId, @UUID, @KeyType, @ValueType, @Version ); " + "SELECT Id from " + IdTableName + " WHERE TextId = @TextId;";
            var id = _connection.ExecuteScalar<long>(setSql, new {
                TextId = seriesId,
                UUID = seriesId.MD5Bytes(),
                KeyType = keyType,
                ValueType = valueType,
                Version = 0L
            });
            return id;
        }

        internal long GetLongId(string extendedSeriesId) {
            var sid = new SeriesId(extendedSeriesId);
            var seriesId = sid.TextId;
            var keyType = sid.KeyType;
            var valueType = sid.ValueType;

            var seriesIdRow = _connection.Query<SeriesId>("SELECT Id, TextId, UUID, KeyType, ValueType, Version from " + IdTableName + "" + " WHERE TextId = @TextId", new { TextId = seriesId }, buffered: false).SingleOrDefault();
            if (seriesIdRow != null) {
                if (seriesIdRow.KeyType != keyType || seriesIdRow.ValueType != valueType) {
                    throw new ArgumentException(
                        $"Wrong types for {seriesId}: expexting <{seriesIdRow.KeyType},{seriesIdRow.ValueType}> but requested <{keyType},{valueType}>");
                }
                return seriesIdRow.Id;
            }

            var setSql = @"INSERT INTO " + IdTableName + " (TextId, UUID, KeyType, ValueType, Version) VALUES ( @TextId, @UUID, @KeyType, @ValueType, @Version ); " + "SELECT Id from " + IdTableName + " WHERE TextId = @TextId;";
            var id = _connection.ExecuteScalar<long>(setSql, new {
                TextId = seriesId,
                UUID = seriesId.MD5Bytes(),
                KeyType = keyType,
                ValueType = valueType,
                Version = 0L
            });
            return id;
        }

        internal long GetLastVersion(string extendedSeriesId) {
            var sid = new SeriesId(extendedSeriesId);
            var seriesId = sid.TextId;
            var keyType = sid.KeyType;
            var valueType = sid.ValueType;

            var seriesIdRow = _connection.Query<SeriesId>("SELECT Id, TextId, UUID, KeyType, ValueType, Version from " + IdTableName + "" + " WHERE TextId = @TextId", new { TextId = seriesId }, buffered: false).SingleOrDefault();
            if (seriesIdRow != null) {
                if (seriesIdRow.KeyType != keyType || seriesIdRow.ValueType != valueType)
                {
                    var message =
                        $"Wrong types for {seriesId}: expexting <{seriesIdRow.KeyType},{seriesIdRow.ValueType}> but requested <{keyType},{valueType}>";
                    Trace.TraceError(message);
                    throw new ArgumentException(message);
                }
                return seriesIdRow.Version;
            }
            return -1L;
        }

        internal SeriesId GetExtendedSeriesId(long id) {
            var seriesIdRow = _connection.Query<SeriesId>("SELECT Id, TextId, UUID, KeyType, ValueType, Version from " + IdTableName + "" + " WHERE Id = @Id", new { Id = id }, buffered: false).Single();
            return seriesIdRow;
        }

        //internal SeriesId GetExtendedSeriesId(string textId) {
        //    var seriesIdRow = _connection.Query<SeriesId>("SELECT Id, TextId, UUID, KeyType, ValueType, Version from " + IdTableName + "" + " WHERE TextId = @TextId", new { TextId = textId }, buffered: false).Single();
        //    return seriesIdRow;
        //}

        internal SeriesId GetExtendedSeriesId<K, V>(string seriesId) {
            seriesId = seriesId.ToLowerInvariant().Trim();
            var keyType = typeof(K).FullName;
            var valueType = typeof(V).FullName;

            var seriesIdRow = _connection.Query<SeriesId>("SELECT Id, TextId, UUID, KeyType, ValueType, Version from " + IdTableName + "" + " WHERE TextId = @TextId", new { TextId = seriesId }, buffered: false).SingleOrDefault();
            if (seriesIdRow != null) {
                if (seriesIdRow.KeyType != keyType || seriesIdRow.ValueType != valueType) {
                    throw new ArgumentException(
                        $"Wrong types for {seriesId}: expexting <{seriesIdRow.KeyType},{seriesIdRow.ValueType}> but requested <{keyType},{valueType}>");
                }
                return seriesIdRow;
            }

            var setSql = @"INSERT INTO " + IdTableName + " (TextId, UUID, KeyType, ValueType, Version) VALUES ( @TextId, @UUID, @KeyType, @ValueType, @Version ); " + "SELECT Id from " + IdTableName + " WHERE TextId = @TextId;";
            var id = _connection.ExecuteScalar<long>(setSql, new {
                TextId = seriesId,
                UUID = seriesId.MD5Bytes(),
                KeyType = keyType,
                ValueType = valueType,
                Version = 0L
            });
            return id > 0 ? GetExtendedSeriesId<K, V>(seriesId) : null;
        }

        protected SortedChunkedMap<K, V> GetSeries<K, V>(long seriesId, long version, bool readOnly, int chunkSize = 4096) {
            SortedChunkedMap<K, V> series;
            var comparer = KeyComparer.GetDefault<K>() as IKeyComparer<K>;
            if (comparer == null) throw new NotSupportedException("Only type that have IKeyComparer<K> are supported");
            Func<IComparer<K>, IOrderedMap<K, SortedMap<K, V>>> outerFactory =
                cmp => new RemoteChunksSeries<K, V>(
                    seriesId,
                    cmp as IKeyComparer<K>,
                    LoadKeys,
                    LoadChunk,
                    SaveChunk,
                    RemoveChunk,
                    version,
                    readOnly);

            series = new SortedChunkedMap<K, V>(outerFactory, comparer, chunkSize) {
                // better be safe
                IsSynchronized = true
            };
            // NB important to set the version here because SCM does not initialize its version from outer
            series.Version = version;
            return series;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing) {
            _connection.Dispose();
            if (disposing) GC.SuppressFinalize(this);
        }

        ~SeriesStorage() {
            Dispose(false);
        }
    }
}
