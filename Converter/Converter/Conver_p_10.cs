using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Converter {
	public class Conver_p_10 {
		private static int char_To_num(char ch) {
			string AllVariants = "0123456789ABCDEF";
			if (!AllVariants.Contains(ch))
				throw new IndexOutOfRangeException();
			return AllVariants.IndexOf(ch);
		}

		private static double convert(string PNum, int P, double weight) {
			if (weight % P != 0)
				throw new Exception();

			long Degree = (long)Math.Log(weight, P) - 1;
			double Result = 0.0f;

			for (int i = 0; i < PNum.Length; ++i, --Degree)
				Result += char_To_num(PNum.ElementAt(i)) * Math.Pow(P, Degree);

			return Result;
		}

		public static double dval(string PNum, int P) {
			if (P < 2 || P > 16)
				throw new IndexOutOfRangeException();
			foreach (char ch in PNum) {
				if (ch == '.')
					continue;
				if (char_To_num(ch) > P)
					throw new Exception();
			}

			double Number = 0.0f;
			Regex LeftRight = new Regex("^[0-9A-F]+\\.[0-9A-F]+$");
			Regex Right = new Regex("^0\\.[0-9A-F]+$");
			Regex Left = new Regex("^[0-9A-F]+$");
			if (LeftRight.IsMatch(PNum)) {
				Number = convert(PNum.Remove(PNum.IndexOf('.'), 1), P, Math.Pow(P, PNum.IndexOf('.')));
			} else if (Left.IsMatch(PNum)) {
				Number = convert(PNum, P, Math.Pow(P, PNum.Length));
			} else if (Right.IsMatch(PNum)) {
				Number = convert(PNum.Remove(PNum.IndexOf('.'), 1), P, 0);
			} else throw new Exception();

			return Number;
		}
	}
}