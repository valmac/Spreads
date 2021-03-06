using System.Runtime.InteropServices;

namespace Yeppp
{

	/// <summary>Basic arithmetic operations</summary>
	public partial class Core
	{


		/// <summary>Negates elements in signed 8-bit integer array.</summary>
		/// <exception cref="System.NullReferenceException">If xArray or yArray is null.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If xOffset is negative, xOffset + length exceeds the length of xArray, yOffset is negative, yOffset + length exceeds the length of yArray, or length is negative.</exception>
		public static unsafe void Negate_V8s_V8s(sbyte[] xArray, int xOffset, sbyte[] yArray, int yOffset, int length)
		{
			if (xOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (xOffset + length > xArray.Length)
				throw new System.IndexOutOfRangeException();

			if (yOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (yOffset + length > yArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (sbyte* x = &xArray[xOffset])
			{
				fixed (sbyte* y = &yArray[yOffset])
				{
					Negate_V8s_V8s(x, y, length);
				}
			}
		}


		/// <summary>Negates elements in signed 16-bit integer array.</summary>
		/// <exception cref="System.NullReferenceException">If xArray or yArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If xArray or yArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If xOffset is negative, xOffset + length exceeds the length of xArray, yOffset is negative, yOffset + length exceeds the length of yArray, or length is negative.</exception>
		public static unsafe void Negate_V16s_V16s(short[] xArray, int xOffset, short[] yArray, int yOffset, int length)
		{
			if (xOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (xOffset + length > xArray.Length)
				throw new System.IndexOutOfRangeException();

			if (yOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (yOffset + length > yArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (short* x = &xArray[xOffset])
			{
				fixed (short* y = &yArray[yOffset])
				{
					Negate_V16s_V16s(x, y, length);
				}
			}
		}


		/// <summary>Negates elements in signed 32-bit integer array.</summary>
		/// <exception cref="System.NullReferenceException">If xArray or yArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If xArray or yArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If xOffset is negative, xOffset + length exceeds the length of xArray, yOffset is negative, yOffset + length exceeds the length of yArray, or length is negative.</exception>
		public static unsafe void Negate_V32s_V32s(int[] xArray, int xOffset, int[] yArray, int yOffset, int length)
		{
			if (xOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (xOffset + length > xArray.Length)
				throw new System.IndexOutOfRangeException();

			if (yOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (yOffset + length > yArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (int* x = &xArray[xOffset])
			{
				fixed (int* y = &yArray[yOffset])
				{
					Negate_V32s_V32s(x, y, length);
				}
			}
		}


		/// <summary>Negates elements in signed 64-bit integer array.</summary>
		/// <exception cref="System.NullReferenceException">If xArray or yArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If xArray or yArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If xOffset is negative, xOffset + length exceeds the length of xArray, yOffset is negative, yOffset + length exceeds the length of yArray, or length is negative.</exception>
		public static unsafe void Negate_V64s_V64s(long[] xArray, int xOffset, long[] yArray, int yOffset, int length)
		{
			if (xOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (xOffset + length > xArray.Length)
				throw new System.IndexOutOfRangeException();

			if (yOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (yOffset + length > yArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (long* x = &xArray[xOffset])
			{
				fixed (long* y = &yArray[yOffset])
				{
					Negate_V64s_V64s(x, y, length);
				}
			}
		}


		/// <summary>Negates elements in single precision (32-bit) floating-point array.</summary>
		/// <exception cref="System.NullReferenceException">If xArray or yArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If xArray or yArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If xOffset is negative, xOffset + length exceeds the length of xArray, yOffset is negative, yOffset + length exceeds the length of yArray, or length is negative.</exception>
		public static unsafe void Negate_V32f_V32f(float[] xArray, int xOffset, float[] yArray, int yOffset, int length)
		{
			if (xOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (xOffset + length > xArray.Length)
				throw new System.IndexOutOfRangeException();

			if (yOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (yOffset + length > yArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (float* x = &xArray[xOffset])
			{
				fixed (float* y = &yArray[yOffset])
				{
					Negate_V32f_V32f(x, y, length);
				}
			}
		}


		/// <summary>Negates elements in double precision (64-bit) floating-point array.</summary>
		/// <exception cref="System.NullReferenceException">If xArray or yArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If xArray or yArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If xOffset is negative, xOffset + length exceeds the length of xArray, yOffset is negative, yOffset + length exceeds the length of yArray, or length is negative.</exception>
		public static unsafe void Negate_V64f_V64f(double[] xArray, int xOffset, double[] yArray, int yOffset, int length)
		{
			if (xOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (xOffset + length > xArray.Length)
				throw new System.IndexOutOfRangeException();

			if (yOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (yOffset + length > yArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (double* x = &xArray[xOffset])
			{
				fixed (double* y = &yArray[yOffset])
				{
					Negate_V64f_V64f(x, y, length);
				}
			}
		}


		/// <summary>Negates elements in signed 8-bit integer array and writes the results to the same array.</summary>
		/// <exception cref="System.NullReferenceException">If vArray is null.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If vOffset is negative, vOffset + length exceeds the length of vArray, or length is negative.</exception>
		public static unsafe void Negate_IV8s_IV8s(sbyte[] vArray, int vOffset, int length)
		{
			if (vOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (vOffset + length > vArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (sbyte* v = &vArray[vOffset])
			{
				Negate_IV8s_IV8s(v, length);
			}
		}


		/// <summary>Negates elements in signed 16-bit integer array and writes the results to the same array.</summary>
		/// <exception cref="System.NullReferenceException">If vArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If vArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If vOffset is negative, vOffset + length exceeds the length of vArray, or length is negative.</exception>
		public static unsafe void Negate_IV16s_IV16s(short[] vArray, int vOffset, int length)
		{
			if (vOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (vOffset + length > vArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (short* v = &vArray[vOffset])
			{
				Negate_IV16s_IV16s(v, length);
			}
		}


		/// <summary>Negates elements in signed 32-bit integer array and writes the results to the same array.</summary>
		/// <exception cref="System.NullReferenceException">If vArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If vArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If vOffset is negative, vOffset + length exceeds the length of vArray, or length is negative.</exception>
		public static unsafe void Negate_IV32s_IV32s(int[] vArray, int vOffset, int length)
		{
			if (vOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (vOffset + length > vArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (int* v = &vArray[vOffset])
			{
				Negate_IV32s_IV32s(v, length);
			}
		}


		/// <summary>Negates elements in signed 64-bit integer array and writes the results to the same array.</summary>
		/// <exception cref="System.NullReferenceException">If vArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If vArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If vOffset is negative, vOffset + length exceeds the length of vArray, or length is negative.</exception>
		public static unsafe void Negate_IV64s_IV64s(long[] vArray, int vOffset, int length)
		{
			if (vOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (vOffset + length > vArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (long* v = &vArray[vOffset])
			{
				Negate_IV64s_IV64s(v, length);
			}
		}


		/// <summary>Negates elements in single precision (32-bit) floating-point array and writes the results to the same array.</summary>
		/// <exception cref="System.NullReferenceException">If vArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If vArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If vOffset is negative, vOffset + length exceeds the length of vArray, or length is negative.</exception>
		public static unsafe void Negate_IV32f_IV32f(float[] vArray, int vOffset, int length)
		{
			if (vOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (vOffset + length > vArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (float* v = &vArray[vOffset])
			{
				Negate_IV32f_IV32f(v, length);
			}
		}


		/// <summary>Negates elements in double precision (64-bit) floating-point array and writes the results to the same array.</summary>
		/// <exception cref="System.NullReferenceException">If vArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If vArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If vOffset is negative, vOffset + length exceeds the length of vArray, or length is negative.</exception>
		public static unsafe void Negate_IV64f_IV64f(double[] vArray, int vOffset, int length)
		{
			if (vOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (vOffset + length > vArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (double* v = &vArray[vOffset])
			{
				Negate_IV64f_IV64f(v, length);
			}
		}


		/// <summary>Negates elements in signed 8-bit integer array.</summary>
		/// <param name="x">Pointer to the array of signed 8-bit integer elements to be negated.</param>
		/// <param name="y">Pointer to the signed 8-bit integer array to store negated elements.</param>
		/// <param name="length">Length of the arrays specified by x and y.</param>
		/// <exception cref="System.NullReferenceException">If x or y is null.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_V8s_V8s(sbyte* x, sbyte* y, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_V8s_V8s(x, y, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in signed 16-bit integer array.</summary>
		/// <param name="x">Pointer to the array of signed 16-bit integer elements to be negated.</param>
		/// <param name="y">Pointer to the signed 16-bit integer array to store negated elements.</param>
		/// <param name="length">Length of the arrays specified by x and y.</param>
		/// <exception cref="System.NullReferenceException">If x or y is null.</exception>
		/// <exception cref="System.DataMisalignedException">If x or y is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_V16s_V16s(short* x, short* y, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_V16s_V16s(x, y, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in signed 32-bit integer array.</summary>
		/// <param name="x">Pointer to the array of signed 32-bit integer elements to be negated.</param>
		/// <param name="y">Pointer to the signed 32-bit integer array to store negated elements.</param>
		/// <param name="length">Length of the arrays specified by x and y.</param>
		/// <exception cref="System.NullReferenceException">If x or y is null.</exception>
		/// <exception cref="System.DataMisalignedException">If x or y is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_V32s_V32s(int* x, int* y, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_V32s_V32s(x, y, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in signed 64-bit integer array.</summary>
		/// <param name="x">Pointer to the array of signed 64-bit integer elements to be negated.</param>
		/// <param name="y">Pointer to the signed 64-bit integer array to store negated elements.</param>
		/// <param name="length">Length of the arrays specified by x and y.</param>
		/// <exception cref="System.NullReferenceException">If x or y is null.</exception>
		/// <exception cref="System.DataMisalignedException">If x or y is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_V64s_V64s(long* x, long* y, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_V64s_V64s(x, y, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in single precision (32-bit) floating-point array.</summary>
		/// <param name="x">Pointer to the array of single precision (32-bit) floating-point elements to be negated.</param>
		/// <param name="y">Pointer to the single precision (32-bit) floating-point array to store negated elements.</param>
		/// <param name="length">Length of the arrays specified by x and y.</param>
		/// <exception cref="System.NullReferenceException">If x or y is null.</exception>
		/// <exception cref="System.DataMisalignedException">If x or y is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_V32f_V32f(float* x, float* y, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_V32f_V32f(x, y, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in double precision (64-bit) floating-point array.</summary>
		/// <param name="x">Pointer to the array of double precision (64-bit) floating-point elements to be negated.</param>
		/// <param name="y">Pointer to the double precision (64-bit) floating-point array to store negated elements.</param>
		/// <param name="length">Length of the arrays specified by x and y.</param>
		/// <exception cref="System.NullReferenceException">If x or y is null.</exception>
		/// <exception cref="System.DataMisalignedException">If x or y is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_V64f_V64f(double* x, double* y, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_V64f_V64f(x, y, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in signed 8-bit integer array and writes the results to the same array.</summary>
		/// <param name="v">Pointer to the array of signed 8-bit integer elements to be negated.</param>
		/// <param name="length">Length of the array specified by v.</param>
		/// <exception cref="System.NullReferenceException">If v is null.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_IV8s_IV8s(sbyte* v, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_IV8s_IV8s(v, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in signed 16-bit integer array and writes the results to the same array.</summary>
		/// <param name="v">Pointer to the array of signed 16-bit integer elements to be negated.</param>
		/// <param name="length">Length of the array specified by v.</param>
		/// <exception cref="System.NullReferenceException">If v is null.</exception>
		/// <exception cref="System.DataMisalignedException">If v is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_IV16s_IV16s(short* v, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_IV16s_IV16s(v, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in signed 32-bit integer array and writes the results to the same array.</summary>
		/// <param name="v">Pointer to the array of signed 32-bit integer elements to be negated.</param>
		/// <param name="length">Length of the array specified by v.</param>
		/// <exception cref="System.NullReferenceException">If v is null.</exception>
		/// <exception cref="System.DataMisalignedException">If v is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_IV32s_IV32s(int* v, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_IV32s_IV32s(v, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in signed 64-bit integer array and writes the results to the same array.</summary>
		/// <param name="v">Pointer to the array of signed 64-bit integer elements to be negated.</param>
		/// <param name="length">Length of the array specified by v.</param>
		/// <exception cref="System.NullReferenceException">If v is null.</exception>
		/// <exception cref="System.DataMisalignedException">If v is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_IV64s_IV64s(long* v, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_IV64s_IV64s(v, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in single precision (32-bit) floating-point array and writes the results to the same array.</summary>
		/// <param name="v">Pointer to the array of single precision (32-bit) floating-point elements to be negated.</param>
		/// <param name="length">Length of the array specified by v.</param>
		/// <exception cref="System.NullReferenceException">If v is null.</exception>
		/// <exception cref="System.DataMisalignedException">If v is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_IV32f_IV32f(float* v, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_IV32f_IV32f(v, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		/// <summary>Negates elements in double precision (64-bit) floating-point array and writes the results to the same array.</summary>
		/// <param name="v">Pointer to the array of double precision (64-bit) floating-point elements to be negated.</param>
		/// <param name="length">Length of the array specified by v.</param>
		/// <exception cref="System.NullReferenceException">If v is null.</exception>
		/// <exception cref="System.DataMisalignedException">If v is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe void Negate_IV64f_IV64f(double* v, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			Status status = yepCore_Negate_IV64f_IV64f(v, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
		}


		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_V8s_V8s")]
		private static unsafe extern Status yepCore_Negate_V8s_V8s(sbyte* x, sbyte* y, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_V16s_V16s")]
		private static unsafe extern Status yepCore_Negate_V16s_V16s(short* x, short* y, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_V32s_V32s")]
		private static unsafe extern Status yepCore_Negate_V32s_V32s(int* x, int* y, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_V64s_V64s")]
		private static unsafe extern Status yepCore_Negate_V64s_V64s(long* x, long* y, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_V32f_V32f")]
		private static unsafe extern Status yepCore_Negate_V32f_V32f(float* x, float* y, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_V64f_V64f")]
		private static unsafe extern Status yepCore_Negate_V64f_V64f(double* x, double* y, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_IV8s_IV8s")]
		private static unsafe extern Status yepCore_Negate_IV8s_IV8s(sbyte* v, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_IV16s_IV16s")]
		private static unsafe extern Status yepCore_Negate_IV16s_IV16s(short* v, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_IV32s_IV32s")]
		private static unsafe extern Status yepCore_Negate_IV32s_IV32s(int* v, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_IV64s_IV64s")]
		private static unsafe extern Status yepCore_Negate_IV64s_IV64s(long* v, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_IV32f_IV32f")]
		private static unsafe extern Status yepCore_Negate_IV32f_IV32f(float* v, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_Negate_IV64f_IV64f")]
		private static unsafe extern Status yepCore_Negate_IV64f_IV64f(double* v, System.UIntPtr length);

	}

}
