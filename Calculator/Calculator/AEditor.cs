using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
	public abstract class AEditor {
		public abstract string Number {
			get;
			set;
		}
		public enum Command {
			cZero, cOne, cTwo, cThree, cFour, cFive, cSix, cSeven, cEight, cNine, cA, cB, cC, cD, cE, cF, cSign, cSeparator, cBS, CE, cNumbSeparator, cNone
		}

		public abstract bool IsZero();
		public abstract string TogleSign();
		public abstract string AddNumber(int num);
		public abstract string AddZero();
		public abstract string RemoveSymbol();
		public abstract string Clear();
		public abstract string Edit(Enum com);
		public abstract string AddSeparator();
		public abstract bool HaveSeparator();
	}
}
