using System;

namespace Converter {
	public class Editor {
		public enum Commands {
			ADD0, ADD1, ADD2, ADD3, ADD4, ADD5, ADD6, ADD7, ADD8, ADD9, ADDA, ADDB, ADDC, ADDD, ADDE, ADDF, ADDDOT, BS, CLEAR, EXECUTE
		};
		string number = zero;
		const string delim = ".";
		const string zero = "0";

		public string Number { get => number; set => number = value; }

		public string AddDigit(int n) {
			if (n < 0 || n > 16)
				throw new IndexOutOfRangeException();
			if (number == zero)
				number = Conver_10_p.int_to_Char(n).ToString();
			else
				number += Conver_10_p.int_to_Char(n);
			return Number;
		}

		public int Acc() {
			return number.Contains(delim) ? number.Length - number.IndexOf(delim) - 1 : 0;
		}

		public string AddZero() {
			return AddDigit(0);
		}

		public string AddDelim() {
			if (number.Length > 0 && !number.Contains(delim))
				number += delim;
			return Number;
		}

		public string Bs() {
			if (number.Length > 1)
				number = number.Remove(Number.Length - 1);
			else
				number = zero;
			return Number;
		}

		public string Clear() {
			number = zero;
			return Number;
		}

		public string DoEdit(Commands j) {
			switch (j) {
				case Commands.ADD0:
					AddZero();
					break;
				case Commands.ADD1:
					AddDigit(1);
					break;
				case Commands.ADD2:
					AddDigit(2);
					break;
				case Commands.ADD3:
					AddDigit(3);
					break;
				case Commands.ADD4:
					AddDigit(4);
					break;
				case Commands.ADD5:
					AddDigit(5);
					break;
				case Commands.ADD6:
					AddDigit(6);
					break;
				case Commands.ADD7:
					AddDigit(7);
					break;
				case Commands.ADD8:
					AddDigit(8);
					break;
				case Commands.ADD9:
					AddDigit(9);
					break;
				case Commands.ADDA:
					AddDigit(10);
					break;
				case Commands.ADDB:
					AddDigit(11);
					break;
				case Commands.ADDC:
					AddDigit(12);
					break;
				case Commands.ADDD:
					AddDigit(13);
					break;
				case Commands.ADDE:
					AddDigit(14);
					break;
				case Commands.ADDF:
					AddDigit(15);
					break;
				case Commands.ADDDOT:
					AddDelim();
					break;
				case Commands.BS:
					Bs();
					break;
				case Commands.CLEAR:
					Clear();
					break;
				default:
					return number;
			}
			return number;
		}
	}
}
