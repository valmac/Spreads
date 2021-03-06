using System.Runtime.InteropServices;

namespace Yeppp
{

	/// <summary>Basic arithmetic operations</summary>
	public partial class Core
	{


		/// <summary>Computes the sum of squares of single precision (32-bit) floating-point array elements.</summary>
		/// <exception cref="System.NullReferenceException">If vArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If vArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If vOffset is negative, vOffset + length exceeds the length of vArray, or length is negative.</exception>
		public static unsafe float SumSquares_V32f_S32f(float[] vArray, int vOffset, int length)
		{
			if (vOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (vOffset + length > vArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (float* v = &vArray[vOffset])
			{
				return SumSquares_V32f_S32f(v, length);
			}
		}


		/// <summary>Computes the sum of squares of double precision (64-bit) floating-point array elements.</summary>
		/// <exception cref="System.NullReferenceException">If vArray is null.</exception>
		/// <exception cref="System.DataMisalignedException">If vArray is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		/// <exception cref="System.IndexOutOfRangeException">If vOffset is negative, vOffset + length exceeds the length of vArray, or length is negative.</exception>
		public static unsafe double SumSquares_V64f_S64f(double[] vArray, int vOffset, int length)
		{
			if (vOffset < 0)
				throw new System.IndexOutOfRangeException();

			if (vOffset + length > vArray.Length)
				throw new System.IndexOutOfRangeException();

			if (length < 0)
				throw new System.ArgumentException();

			fixed (double* v = &vArray[vOffset])
			{
				return SumSquares_V64f_S64f(v, length);
			}
		}


		/// <summary>Computes the sum of squares of single precision (32-bit) floating-point array elements.</summary>
		/// <param name="v">Pointer to the array of elements which will be squared (without write-back) and summed up.</param>
		/// <param name="length">Length of the array specified by v. If length is zero, the computed sum of squares will be 0.</param>
		/// <exception cref="System.NullReferenceException">If v is null.</exception>
		/// <exception cref="System.DataMisalignedException">If v is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe float SumSquares_V32f_S32f(float* v, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			float sumSquares;
			Status status = yepCore_SumSquares_V32f_S32f(v, out sumSquares, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
			return sumSquares;
		}


		/// <summary>Computes the sum of squares of double precision (64-bit) floating-point array elements.</summary>
		/// <param name="v">Pointer to the array of elements which will be squared (without write-back) and summed up.</param>
		/// <param name="length">Length of the array specified by v. If length is zero, the computed sum of squares will be 0.</param>
		/// <exception cref="System.NullReferenceException">If v is null.</exception>
		/// <exception cref="System.DataMisalignedException">If v is not naturally aligned.</exception>
		/// <exception cref="System.ArgumentException">If length is negative.</exception>
		public static unsafe double SumSquares_V64f_S64f(double* v, int length)
		{
			if (length < 0)
				throw new System.ArgumentException();

			double sumSquares;
			Status status = yepCore_SumSquares_V64f_S64f(v, out sumSquares, new System.UIntPtr(unchecked((uint) length)));
			if (status != Status.Ok)
				throw Library.GetException(status);
			return sumSquares;
		}


		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_SumSquares_V32f_S32f")]
		private static unsafe extern Status yepCore_SumSquares_V32f_S32f(float* v, out float sumSquares, System.UIntPtr length);

		[DllImport("yeppp", ExactSpelling=true, CallingConvention=CallingConvention.Cdecl, EntryPoint="yepCore_SumSquares_V64f_S64f")]
		private static unsafe extern Status yepCore_SumSquares_V64f_S64f(double* v, out double sumSquares, System.UIntPtr length);

	}

}
