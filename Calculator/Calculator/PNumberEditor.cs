﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator {
	public sealed class PNumberEditor : AEditor {
		private string number;
		public override string Number {
			get {
				return number;
			}
			set {
				number = new TPNumber(value, Notation, Precision).ToString();
			}
		}

		public TNumber Notation;
		public TNumber Precision;
		public static readonly int[] MaximumNumberLenghtForP = { 17, 17, 17, 17, 17, 15, 14, 13, 13, 12, 12, 11, 11, 11, 10 };

		Regex ZeroPNumber = new Regex("^-?(0+|.?0+|0+.(0+)?)$");
		const string Separator = ".";

		#region Constructor
		public PNumberEditor() {
			number = "0";
			Notation = new TNumber(10);
			Precision = new TNumber(5);
		}
		public PNumberEditor(string str, TNumber not, TNumber pre) {
			if (not < 2 || not > 16 || pre < 0 || pre > 10) {
				number = "0";
				Notation = new TNumber(10);
				Precision = new TNumber(5);
			} else {
				Notation = not;
				Precision = pre;
				number = new TPNumber(str, Notation, Precision).ToString();
			}
		}
		public PNumberEditor(double num, TNumber not, TNumber pre) {
			if (not < 2 || not > 16 || pre < 0 || pre > 10) {
				number = "0";
				Notation = new TNumber(10);
				Precision = new TNumber(5);
			} else {
				Notation = not;
				Precision = pre;
				number = new TPNumber(num, Convert.ToInt32(Notation.Number), Convert.ToInt32(Precision.Number)).ToString(); ;
			}
		}
		public PNumberEditor(double num, int not, int pre) {
			if (not < 2 || not > 16 || pre < 0 || pre > 10) {
				number = "0";
				Notation = new TNumber(10);
				Precision = new TNumber(5);
			} else {
				Notation = new TNumber(not);
				Precision = new TNumber(pre);
				number = TPNumber.Conver_10_p.Do(num, not, pre);
			}
		}
		public PNumberEditor(string str) {
			Notation = new TNumber(10);
			Precision = new TNumber(5);
			try {
				number = new TPNumber(str, Notation, Precision).ToString();
			} catch (IndexOutOfRangeException) {
				str = "ERROR";
			}
		}
		#endregion

		#region Standart Override
		public override bool IsZero() {
			return ZeroPNumber.IsMatch(number);
		}
		public override string TogleSign() {
			if (number.ElementAt(0) == '-')
				number = number.Remove(0, 1);
			else
				number = "-" + number;
			return number;
		}
		public override string AddNumber(int num) {
			if (number.Length >= 13)
				return number;
			if (num < 0 || num >= Notation.Number)
				return number;
			if (num == 0)
				AddZero();
			else if (number == "0" || number == "-0")
				number = number.First() == '-' ? "-" + TPNumber.Conver_10_p.int_to_Char(num).ToString() : TPNumber.Conver_10_p.int_to_Char(num).ToString();
			else
				number += TPNumber.Conver_10_p.int_to_Char(num).ToString();
			return number;
		}
		public override bool Equals(object obj) {
			var editor = obj as PNumberEditor;
			return editor != null &&
				   number == editor.number &&
				   EqualityComparer<TNumber>.Default.Equals(Notation, editor.Notation) &&
				   EqualityComparer<TNumber>.Default.Equals(Precision, editor.Precision) &&
				   Number == editor.Number &&
				   EqualityComparer<Regex>.Default.Equals(ZeroPNumber, editor.ZeroPNumber);
		}
		public override int GetHashCode() {
			var hashCode = -948279618;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(number);
			hashCode = hashCode * -1521134295 + EqualityComparer<TNumber>.Default.GetHashCode(Notation);
			hashCode = hashCode * -1521134295 + EqualityComparer<TNumber>.Default.GetHashCode(Precision);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
			hashCode = hashCode * -1521134295 + EqualityComparer<Regex>.Default.GetHashCode(ZeroPNumber);
			return hashCode;
		}
		public override string RemoveSymbol() {
			if (number.Length == 1)
				number = "0";
			else if (number.Length == 2 && number.First() == '-')
				number = "-0";
			else
				number = number.Remove(number.Length - 1);
			return number;
		}
		public override string Clear() {
			number = "0";
			return number;
		}
		public override string Edit(Enum com) {
			switch (com) {
				case Command.cZero:
					AddZero();
					break;
				case Command.cOne:
					AddNumber(1);
					break;
				case Command.cTwo:
					AddNumber(2);
					break;
				case Command.cThree:
					AddNumber(3);
					break;
				case Command.cFour:
					AddNumber(4);
					break;
				case Command.cFive:
					AddNumber(5);
					break;
				case Command.cSix:
					AddNumber(6);
					break;
				case Command.cSeven:
					AddNumber(7);
					break;
				case Command.cEight:
					AddNumber(8);
					break;
				case Command.cNine:
					AddNumber(9);
					break;
				case Command.cA:
					AddNumber(10);
					break;
				case Command.cB:
					AddNumber(11);
					break;
				case Command.cC:
					AddNumber(12);
					break;
				case Command.cD:
					AddNumber(13);
					break;
				case Command.cE:
					AddNumber(14);
					break;
				case Command.cF:
					AddNumber(15);
					break;
				case Command.cSign:
					TogleSign();
					break;
				case Command.cSeparator:
					AddSeparator();
					break;
				case Command.cBS:
					RemoveSymbol();
					break;
				case Command.CE:
					Clear();
					break;
				default:
					break;
			}
			return Number;
		}
		public override string AddSeparator() {
			if (number.Length >= 14)
				return number;
			if (!number.Contains(Separator))
				number += Separator;
			return number;
		}
		public override bool HaveSeparator() {
			return number.Contains(Separator);
		}
		public override string AddZero() {
			if (HaveSeparator() && number.Last().ToString() == Separator)
				return number;
			if (number == "0" || number == "0.")
				return number;
			number += "0";
			return number;
		}
		public override string ToString() {
			return number;
		}
		#endregion
	}
}
