using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator {
	public class TComplex : ANumber {
		public TNumber Real;
		public TNumber Imaginary;
		const string Separator = " + i * ";
		// Separator = NUMBER + i * NUMBER
		// Number = -?\d+.?\d*
		// Basically you cant type right part without having left part
		#region Current Class Things
		public double Abs() {
			return Math.Sqrt(Real.Number * Real.Number + Imaginary.Number * Imaginary.Number);
		}
		public double GetRad() {
			if (Real > 0)
				return Math.Atan((Imaginary / Real).Number);
			else if (Real == 0 && Imaginary > 0)
				return Math.PI / 2;
			else if (Real < 0 && Imaginary.Number >= 0)
				return Math.Atan((Imaginary / Real).Number + Math.PI);
			else if (Real < 0 && Imaginary.Number < 0)
				return Math.Atan((Imaginary / Real).Number - Math.PI);
			else if (Real == 0 && Imaginary < 0)
				return -Math.PI / 2;
			return 0;
		}
		public double GetDegree() {
			return GetRad() * 180 / Math.PI;
		}
		public TComplex Pwr(int n) {
			return new TComplex(Math.Pow(Abs(), n) * Math.Cos(n * GetRad()), Math.Pow(Abs(), n) * Math.Sin(n * GetRad()));
		}
		public TComplex Root(int n, int i) {
			if (i >= n || i < 0 || n < 0)
				return new TComplex();
			return new TComplex(Math.Pow(Abs(), 1.0 / n) * Math.Cos((GetDegree() + 2 * Math.PI * i) / n), Math.Pow(Abs(), 1.0 / n) * Math.Sin((GetDegree() + 2 * Math.PI * i) / n));
		}
		#endregion

		#region Constructor
		public TComplex() {
			Real = new TNumber(0);
			Imaginary = new TNumber(0);
		}
		public TComplex(double anReal, double anImaginary) {
			//if (anReal.ToString().Length + anImaginary.ToString().Length + Separator.Length >= 30)
			//	throw new IndexOutOfRangeException();
			Real = new TNumber(anReal);
			Imaginary = new TNumber(anImaginary);
		}
		public TComplex(int anReal, int anImaginary) {
			if (anReal.ToString().Length + anImaginary.ToString().Length + Separator.Length >= 30)
				throw new IndexOutOfRangeException();
			Real = new TNumber(anReal);
			Imaginary = new TNumber(anImaginary);
		}
		public TComplex(TNumber anReal, TNumber anImaginary) {
			if (anReal.ToString().Length + anImaginary.ToString().Length + Separator.Length >= 30)
				throw new IndexOutOfRangeException();
			Real = anReal;
			Imaginary = anImaginary;
		}
		public TComplex(TComplex anotherComplex) {
			Real = anotherComplex.Real;
			Imaginary = anotherComplex.Imaginary;
		}
		public TComplex(string str) {
			if (str.Length >= 20)
				throw new IndexOutOfRangeException();
			Regex FullNumber = new Regex(@"^-?(\d+.?\d*)\s+\+\s+i\s+\*\s+-?(\d+.?\d*)$");
			Regex LeftPart = new Regex(@"^-?(\d+.?\d*)(\s+\+\s+i\s+\*\s+)?$");
			if (FullNumber.IsMatch(str)) {
				List<string> Parts = str.Split(new string[] { Separator }, StringSplitOptions.None).ToList();
				Real = new TNumber(Parts[0]);
				Imaginary = new TNumber(Parts[1]);
			} else if (LeftPart.IsMatch(str)) {
				if (str.Contains(Separator))
					str = str.Replace(Separator, string.Empty);
				Real = new TNumber(str);
				Imaginary = new TNumber();
			} else {
				Real = new TNumber(0);
				Imaginary = new TNumber(0);
			}
		}
		#endregion

		#region Override operators
		public static TComplex operator +(TComplex a, TComplex b) {
			return new TComplex(a.Real + b.Real, a.Imaginary + b.Imaginary);
		}
		public static TComplex operator *(TComplex a, TComplex b) {
			return new TComplex(a.Real * b.Real - a.Imaginary - b.Imaginary, a.Real * b.Imaginary + b.Imaginary * a.Real);
		}
		public static TComplex operator -(TComplex a, TComplex b) {
			return new TComplex(a.Real - b.Real, a.Imaginary - b.Imaginary);
		}
		public static TComplex operator /(TComplex a, TComplex b) {
			return new TComplex((a.Real * b.Real + a.Imaginary * b.Imaginary) / (b.Real * b.Real + b.Imaginary + b.Imaginary), (b.Real * a.Imaginary - a.Real * b.Imaginary) / (b.Real * b.Real + b.Imaginary * b.Imaginary));
		}
		public static TComplex operator -(TComplex a) {
			return new TComplex(-a.Real, a.Imaginary);
		}
		public static bool operator ==(TComplex a, TComplex b) {
			return (a.Real == b.Real && a.Imaginary == b.Imaginary);
		}
		public static bool operator !=(TComplex a, TComplex b) {
			return (a.Real != b.Real || a.Imaginary != b.Imaginary);
		}
		/* Prob never finish this
		 * public static bool operator >(TComplex a, TComplex b) {
			return (a.Real > b.Real ? true : a.Imaginary > b.Imaginary);
		}
		public static bool operator <(TComplex a, TComplex b) {
		}*/
		#endregion

		#region Abstract Override
		public override ANumber Add(ANumber a) {
			return new TComplex(Real + (a as TComplex).Real, Imaginary + (a as TComplex).Imaginary);
		}
		public override ANumber Mul(ANumber a) {
			return new TComplex(Real * (a as TComplex).Real - Imaginary - (a as TComplex).Imaginary, Real * (a as TComplex).Imaginary + (a as TComplex).Imaginary * Real);
		}
		public override ANumber Div(ANumber a) {
			return new TComplex((Real * (a as TComplex).Real + Imaginary * (a as TComplex).Imaginary) / ((a as TComplex).Real * (a as TComplex).Real + (a as TComplex).Imaginary + (a as TComplex).Imaginary), ((a as TComplex).Real * Imaginary - Real * (a as TComplex).Imaginary) / ((a as TComplex).Real * (a as TComplex).Real + (a as TComplex).Imaginary * (a as TComplex).Imaginary));
		}
		public override ANumber Sub(ANumber a) {
			return new TComplex(Real - (a as TComplex).Real, Imaginary - (a as TComplex).Imaginary);
		}
		public override ANumber Square() {
			return new TComplex(Real * Real - Imaginary * Imaginary, Real * Imaginary + Real * Imaginary);
		}
		public override object Reverse() {
			return new TComplex(Real / (Real * Real + Imaginary * Imaginary), -(Imaginary / (Real * Real + Imaginary * Imaginary)));
		}
		public override bool IsZero() {
			return Real.IsZero() && Imaginary.IsZero();
		}
		public override void SetString(string str) {
			TComplex temp = new TComplex(str);
			Real = temp.Real;
			Imaginary = temp.Imaginary;
		}

		public override string ToString() {
			return Real.ToString() + Separator + Imaginary.ToString();
		}
		public override bool Equals(object obj) {
			var complex = obj as TComplex;
			return complex != null &&
				   EqualityComparer<TNumber>.Default.Equals(Real, complex.Real) &&
				   EqualityComparer<TNumber>.Default.Equals(Imaginary, complex.Imaginary);
		}
		public override int GetHashCode() {
			var hashCode = -837395861;
			hashCode = hashCode * -1521134295 + EqualityComparer<TNumber>.Default.GetHashCode(Real);
			hashCode = hashCode * -1521134295 + EqualityComparer<TNumber>.Default.GetHashCode(Imaginary);
			return hashCode;
		}
		#endregion
	}
}
