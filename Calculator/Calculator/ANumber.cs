using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
	public abstract class ANumber {
		public abstract ANumber Add(ANumber a);
		public abstract ANumber Mul(ANumber a);
		public abstract ANumber Div(ANumber a);
		public abstract ANumber Sub(ANumber a);
		public abstract bool IsZero();
		public abstract ANumber Square();
		public abstract object Reverse();
		public abstract void SetString(string str);
	}
}
