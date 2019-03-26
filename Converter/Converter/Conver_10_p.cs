using System;
using System.Linq;

namespace Converter {
	public class Conver_10_p {
		public static string Do(double n, int p, int c) {
			if (p < 2 || p > 16)
				throw new IndexOutOfRangeException();
			if (c < 0 || c > 10)
				throw new IndexOutOfRangeException();

			long LeftSide = (long)n;

			double RightSide = 0f;
			RightSide = n - LeftSide;
			if (RightSide < 0)
				RightSide *= -1;

			string LeftSideString = int_to_P(LeftSide, p);
			string RightSideString = flt_to_P(RightSide, p, c);

			return LeftSideString + (RightSideString == String.Empty ? "" : ".") + RightSideString;
		}

		public static char int_to_Char(int d) { //преобразовать целое в символ
			if (d < 0 || d > 15)
				throw new IndexOutOfRangeException();

			string SymbolArray = "0123456789ABCDEF";
			return SymbolArray.ElementAt(d);
		}

		public static string int_to_P(long n, long p) { 
			if (p < 2 || p > 16)
				throw new IndexOutOfRangeException();
			if (n == 0)
				return "0";
			bool Check_of_minus = false;
			if (n < 0) {
				Check_of_minus = true;
				n *= -1;
			}
			string PNum = string.Empty;

			while (n > 0) {
				PNum += int_to_Char((int)(n % p));
				n /= p;
			}

			if (Check_of_minus)
				PNum += "-";

			char[] T_Arr = PNum.ToCharArray();
			Array.Reverse(T_Arr);
			return new string(T_Arr);
		}

		public static string flt_to_P(double n, int p, int c) { 
			if (p < 2 || p > 16)
				throw new IndexOutOfRangeException();
			if (c < 0 || c > 10)
				throw new IndexOutOfRangeException();

			string PNum = string.Empty;
			for (int i = 0; i < c; ++i) {
				PNum += int_to_Char((int)(n * p));
				n = n * p - (int)(n * p);
			}
			return PNum;
		}
	}
}
