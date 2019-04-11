using System;
using System.Linq;
using System.Windows.Forms;

namespace Calculator {
	public partial class Form1 : Form {
		TCtrl<TFrac, FracEditor> fracController;
		TCtrl<TPNumber, PNumberEditor> pNumberController;
		TCtrl<TComplex, ComplexEditor> complexController;
		const string TAG_FRAC = "FRAC_";
		const string TAG_COMPLEX = "COMPLEX_";
		const string TAG_PNUMBER = "PNUMBER_";
		const string OPERATIONS = "+-/*";
		bool PNumberMode = true;
		bool FracMode = true;
		bool ComplexMode = true;
		enum ComplexFunctions {
			Pwr, Root, Abs, Dgr, Rad
		}

		private string NumberBeatifier(string Tag, string str) {
			if (str == "ERROR")
				return str;
			string ToReturn = str;
			switch (Tag) {
				case TAG_PNUMBER:
					break;
				case TAG_FRAC:
					if (FracMode == true)
						ToReturn = str;
					else if (new TFrac(str).Denominator == 1)
						ToReturn = new TFrac(str).Numerator.ToString();
					break;
				case TAG_COMPLEX:
					if (ComplexMode == true)
						ToReturn = str;
					else if (new TComplex(str).Imaginary == 0)
						ToReturn = new TComplex(str).Real.ToString();
					break;
			}
			return ToReturn;
		}
		private static AEditor.Command CharToEditorCommand(char ch) {
			AEditor.Command command = AEditor.Command.cNone;
			switch (ch) {
				case '0':
					command = AEditor.Command.cZero;
					break;
				case '1':
					command = AEditor.Command.cOne;
					break;
				case '2':
					command = AEditor.Command.cTwo;
					break;
				case '3':
					command = AEditor.Command.cThree;
					break;
				case '4':
					command = AEditor.Command.cFour;
					break;
				case '5':
					command = AEditor.Command.cFive;
					break;
				case '6':
					command = AEditor.Command.cSix;
					break;
				case '7':
					command = AEditor.Command.cSeven;
					break;
				case '8':
					command = AEditor.Command.cEight;
					break;
				case '9':
					command = AEditor.Command.cNine;
					break;
				case 'A':
					command = AEditor.Command.cA;
					break;
				case 'B':
					command = AEditor.Command.cB;
					break;
				case 'C':
					command = AEditor.Command.cC;
					break;
				case 'D':
					command = AEditor.Command.cD;
					break;
				case 'E':
					command = AEditor.Command.cE;
					break;
				case 'F':
					command = AEditor.Command.cF;
					break;
				case '.':
					command = AEditor.Command.cSeparator;
					break;
				case '-':
					command = AEditor.Command.cSign;
					break;
			}
			return command;
		}
		private static TProc<T>.Oper CharToOperationsCommand<T>(char ch) where T : ANumber, new() {
			TProc<T>.Oper command = TProc<T>.Oper.None;
			switch (ch) {
				case '+':
					command = TProc<T>.Oper.Add;
					break;
				case '-':
					command = TProc<T>.Oper.Sub;
					break;
				case '*':
					command = TProc<T>.Oper.Mul;
					break;
				case '/':
					command = TProc<T>.Oper.Div;
					break;
			}
			return command;
		}
		private static AEditor.Command KeyCodeToEditorCommand(Keys ch) {
			AEditor.Command command = AEditor.Command.cNone;
			switch (ch) {
				case Keys.Back:
					command = AEditor.Command.cBS;
					break;
				case Keys.Delete:
				case Keys.Escape:
					command = AEditor.Command.CE;
					break;
			}
			return command;
		}

		public Form1() {
			fracController = new TCtrl<TFrac, FracEditor>();
			pNumberController = new TCtrl<TPNumber, PNumberEditor>();
			complexController = new TCtrl<TComplex, ComplexEditor>();
			InitializeComponent();
			Size = new System.Drawing.Size(355, 433);
		}

		private void Button_Number_Edit(object sender, EventArgs e) {
			Button button = (Button)sender;
			string FullTag = button.Tag.ToString();
			if (FullTag.StartsWith(TAG_FRAC)) {
				Enum.TryParse(FullTag.Replace(TAG_FRAC, string.Empty), out AEditor.Command ParsedEnum);
				tB_Frac.Text = fracController.ExecCommandEditor(ParsedEnum);
			} else if (FullTag.StartsWith(TAG_COMPLEX)) {
				Enum.TryParse(FullTag.Replace(TAG_COMPLEX, string.Empty), out AEditor.Command ParsedEnum);
				tB_Complex.Text = complexController.ExecCommandEditor(ParsedEnum);
			} else if (FullTag.StartsWith(TAG_PNUMBER)) {
				pNumberController.Edit.Notation = new TNumber(trackBar_PNumber.Value);
				Enum.TryParse(FullTag.Replace(TAG_PNUMBER, string.Empty), out AEditor.Command ParsedEnum);
				tB_PNumber.Text = pNumberController.ExecCommandEditor(ParsedEnum);
			}
		}

		private void Button_Number_Operation(object sender, EventArgs e) {
			Button button = (Button)sender;
			string FullTag = button.Tag.ToString();
			if (FullTag.StartsWith(TAG_FRAC)) {
				string Command = FullTag.Replace(TAG_FRAC, string.Empty);
				Enum.TryParse(Command, out TProc<TFrac>.Oper ParsedEnum);
				tB_Frac.Text = NumberBeatifier(TAG_FRAC, fracController.ExecOperation(ParsedEnum));
			} else if (FullTag.StartsWith(TAG_COMPLEX)) {
				string Command = FullTag.Replace(TAG_COMPLEX, string.Empty);
				Enum.TryParse(Command, out TProc<TComplex>.Oper ParsedEnum);
				tB_Complex.Text = NumberBeatifier(TAG_COMPLEX, complexController.ExecOperation(ParsedEnum));
			} else if (FullTag.StartsWith(TAG_PNUMBER)) {
				string Command = FullTag.Replace(TAG_PNUMBER, string.Empty);
				Enum.TryParse(Command, out TProc<TPNumber>.Oper ParsedEnum);
				tB_PNumber.Text = pNumberController.ExecOperation(ParsedEnum);
			}
		}

		private void Button_Number_Function(object sender, EventArgs e) {
			Button button = (Button)sender;
			string FullTag = button.Tag.ToString();
			if (FullTag.StartsWith(TAG_FRAC)) {
				string Command = FullTag.Replace(TAG_FRAC, string.Empty);
				Enum.TryParse(Command, out TProc<TFrac>.Func ParsedEnum);
				tB_Frac.Text = NumberBeatifier(TAG_FRAC, fracController.ExecFunction(ParsedEnum));
			} else if (FullTag.StartsWith(TAG_COMPLEX)) {
				string Command = FullTag.Replace(TAG_COMPLEX, string.Empty);
				Enum.TryParse(Command, out TProc<TComplex>.Func ParsedEnum);
				tB_Complex.Text = NumberBeatifier(TAG_COMPLEX, complexController.ExecFunction(ParsedEnum));
			} else if (FullTag.StartsWith(TAG_PNUMBER)) {
				string Command = FullTag.Replace(TAG_PNUMBER, string.Empty);
				Enum.TryParse(Command, out TProc<TPNumber>.Func ParsedEnum);
				tB_PNumber.Text = pNumberController.ExecFunction(ParsedEnum);
			}
		}

		private void Button_Reset(object sender, EventArgs e) {
			Button button = (Button)sender;
			string FullTag = button.Tag.ToString();
			if (FullTag.StartsWith(TAG_FRAC)) {
				tB_Frac.Text = fracController.Reset();
				label_Frac_Memory.Text = string.Empty;
			} else if (FullTag.StartsWith(TAG_COMPLEX)) {
				tB_Complex.Text = complexController.Reset();
				tB_Complex_SpecialOut.Text = string.Empty;
				label_Complex_Memory.Text = string.Empty;
			} else if (FullTag.StartsWith(TAG_PNUMBER)) {
				tB_PNumber.Text = pNumberController.Reset();
				label_PNumber_Memory.Text = string.Empty;
			}
		}

		private void Button_FinishEval(object sender, EventArgs e) {
			Button button = (Button)sender;
			string FullTag = button.Tag.ToString();
			if (FullTag.StartsWith(TAG_FRAC)) {
				tB_Frac.Text = NumberBeatifier(TAG_FRAC, fracController.Calculate());
			} else if (FullTag.StartsWith(TAG_COMPLEX)) {
				tB_Complex.Text = NumberBeatifier(TAG_COMPLEX, complexController.Calculate());
			} else if (FullTag.StartsWith(TAG_PNUMBER)) {
				tB_PNumber.Text = pNumberController.Calculate();
			}
		}

		private void Button_Memory(object sender, EventArgs e) {
			Button button = (Button)sender;
			string FullTag = button.Tag.ToString();
			if (FullTag.StartsWith(TAG_FRAC)) {
				string Command = FullTag.Replace(TAG_FRAC, string.Empty);
				Enum.TryParse(Command, out TMemory<TFrac>.Commands ParsedEnum);
				dynamic exec = fracController.ExecCommandMemory(ParsedEnum, tB_Frac.Text);
				if (ParsedEnum == TMemory<TFrac>.Commands.Copy)
					tB_Frac.Text = exec.Item1.ToString();
				label_Frac_Memory.Text = exec.Item2 == TMemory<TFrac>.NumStates.ON ? "M" : string.Empty;
			} else if (FullTag.StartsWith(TAG_COMPLEX)) {
				string Command = FullTag.Replace(TAG_COMPLEX, string.Empty);
				Enum.TryParse(Command, out TMemory<TComplex>.Commands ParsedEnum);
				dynamic exec = complexController.ExecCommandMemory(ParsedEnum, tB_Complex.Text);
				if (ParsedEnum == TMemory<TComplex>.Commands.Copy)
					tB_Complex.Text = exec.Item1.ToString();
				label_Complex_Memory.Text = exec.Item2 == TMemory<TComplex>.NumStates.ON ? "M" : string.Empty;
			} else if (FullTag.StartsWith(TAG_PNUMBER)) {
				string Command = FullTag.Replace(TAG_PNUMBER, string.Empty);
				Enum.TryParse(Command, out TMemory<TPNumber>.Commands ParsedEnum);
				dynamic exec = pNumberController.ExecCommandMemory(ParsedEnum, tB_PNumber.Text);
				if (ParsedEnum == TMemory<TPNumber>.Commands.Copy)
					tB_PNumber.Text = exec.Item1.ToString();
				label_PNumber_Memory.Text = exec.Item2 == TMemory<TPNumber>.NumStates.ON ? "M" : string.Empty;
			}
		}

		private void СправкаToolStripMenuItem_Click(object sender, EventArgs e) {
			MessageBox.Show("Универсальнй калькулятор. Авторы: Студентки ИП-512(самые красивые на потоке) Монгуш Айгуль, Енина Екатерина, Морозова Кристина, Павлова Ульяна", "Калькулятор", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void TrackBar_PNumber_ValueChanged(object sender, EventArgs e) {
			label_PNumber_P.Text = trackBar_PNumber.Value.ToString();
			pNumberController.Edit.Notation = new TNumber(trackBar_PNumber.Value);
			tB_PNumber.Text = pNumberController.Reset();
			label_PNumber_Memory.Text = string.Empty;
			string AllowedEndings = "0123456789ABCDEF";
			foreach (Control i in tabPage_PNumber.Controls.OfType<Button>()) {
				if (AllowedEndings.Contains(i.Name.ToString().Last()) && i.Name.ToString().Substring(i.Name.ToString().Length - 2, 1) == "_") {
					int j = AllowedEndings.IndexOf(i.Name.ToString().Last());
					if (j < trackBar_PNumber.Value) {
						i.Enabled = true;
					}
					if ((j >= trackBar_PNumber.Value) && (j <= 15)) {
						i.Enabled = false;
					}
				}
			}
		}

		private void Button_Complex_Special(object sender, EventArgs e) {
			try {
				Button button = (Button)sender;
				string Tag = button.Tag.ToString().Replace(TAG_COMPLEX, string.Empty);
				Enum.TryParse(Tag, out ComplexFunctions ParsedEnum);
				TComplex number = new TComplex(tB_Complex.Text);
				switch (ParsedEnum) {
					case ComplexFunctions.Pwr:
						int PwrN = Convert.ToInt32(nUD_Complex_Pwr.Value);
						tB_Complex_SpecialOut.Text = number.Pwr(PwrN).ToString();
						break;
					case ComplexFunctions.Root:
						int RootN = Convert.ToInt32(nUD_Complex_Root_N.Value);
						int Rooti = Convert.ToInt32(nUD_Complex_Root_i.Value);
						tB_Complex_SpecialOut.Text = number.Root(RootN, Rooti).ToString();
						break;
					case ComplexFunctions.Abs:
						tB_Complex_SpecialOut.Text = number.Abs().ToString();
						break;
					case ComplexFunctions.Dgr:
						tB_Complex_SpecialOut.Text = number.GetDegree().ToString();
						break;
					case ComplexFunctions.Rad:
						tB_Complex_SpecialOut.Text = number.GetRad().ToString();
						break;
				}
			} catch {
				tB_Complex_SpecialOut.Text = "ERROR";
			}
		}

		private void Button_Complex_ShowAllRoots(object sender, EventArgs e) {
			ShowRoots RootsForm = new ShowRoots();
			int RootN = Convert.ToInt32(nUD_Complex_Root_N.Value);
			TComplex number = new TComplex(tB_Complex.Text);
			//try {
			for (int i = 0; i < RootN; ++i)
				RootsForm.richTB_Roots.Text += "Root " + i.ToString() + ": " + number.Root(RootN, i).ToString() + Environment.NewLine;
			//} catch {
			//RootsForm.richTB_Roots.Text = "Error while computing roots";
			//}
			RootsForm.ShowDialog();
		}

		private void nUD_Complex_Root_N_ValueChanged(object sender, EventArgs e) {
			nUD_Complex_Root_i.Maximum = nUD_Complex_Root_N.Value;
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
			switch (tabControl.SelectedIndex) {
				case 0:
					Size = new System.Drawing.Size(355, 433);
					break;
				case 1:
					Size = new System.Drawing.Size(310, 382);
					break;
				case 2:
					Size = new System.Drawing.Size(445, 418);
					break;
				default:
					break;
			}
		}

		private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
			switch (tabControl.SelectedIndex) {
				case 0: {
					if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'A' && e.KeyChar <= 'F') || (e.KeyChar == '.' && PNumberMode))
						tB_PNumber.Text = pNumberController.ExecCommandEditor(CharToEditorCommand(e.KeyChar));
					else if (OPERATIONS.Contains(e.KeyChar))
						tB_PNumber.Text = NumberBeatifier(TAG_PNUMBER, pNumberController.ExecOperation(CharToOperationsCommand<TPNumber>(e.KeyChar)));
					break;
				}
				case 1: {
					if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.')
						tB_Frac.Text = fracController.ExecCommandEditor(CharToEditorCommand(e.KeyChar));
					else if (OPERATIONS.Contains(e.KeyChar))
						tB_Frac.Text = NumberBeatifier(TAG_FRAC, fracController.ExecOperation(CharToOperationsCommand<TFrac>(e.KeyChar)));
					break;
				}
				case 2: {
					if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.')
						tB_Complex.Text = complexController.ExecCommandEditor(CharToEditorCommand(e.KeyChar));
					else if (OPERATIONS.Contains(e.KeyChar))
						tB_Complex.Text = NumberBeatifier(TAG_COMPLEX, complexController.ExecOperation(CharToOperationsCommand<TComplex>(e.KeyChar)));
					break;
				}
				default:
					break;
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch (tabControl.SelectedIndex) {
				case 0: {
					if (e.KeyCode == Keys.Enter)
						b_PNumber_Eval.PerformClick();
					else
						tB_PNumber.Text = pNumberController.ExecCommandEditor(KeyCodeToEditorCommand(e.KeyCode));
					break;
				}
				case 1: {
					if (e.KeyCode == Keys.Enter)
						b_Frac_Eval.PerformClick();
					else
						tB_Frac.Text = fracController.ExecCommandEditor(KeyCodeToEditorCommand(e.KeyCode));
					break;
				}
				case 2: {
					if (e.KeyCode == Keys.Enter)
						b_Complex_Eval.PerformClick();
					else
						tB_Complex.Text = complexController.ExecCommandEditor(KeyCodeToEditorCommand(e.KeyCode));
					break;
				}
				default:
					break;
			}
		}

		private void действительныеPNumberTSMI_Click(object sender, EventArgs e) {
			целыеPNumberTSMI.Checked = false;
			действительныеPNumberTSMI.Checked = true;
			PNumberMode = true;
			b_PNumber_Separator.Enabled = true;
			b_PNumber_Clear.PerformClick();
		}
		private void целыеPNumberTSMI_Click(object sender, EventArgs e) {
			целыеPNumberTSMI.Checked = true;
			действительныеPNumberTSMI.Checked = false;
			PNumberMode = false;
			b_PNumber_Separator.Enabled = false;
			b_PNumber_Clear.PerformClick();
		}
		private void дробьFracTSMI_Click(object sender, EventArgs e) {
			дробьFracTSMI.Checked = true;
			числоFracTSMI.Checked = false;
			FracMode = true;
		}
		private void числоFracTSMI_Click(object sender, EventArgs e) {
			дробьFracTSMI.Checked = false;
			числоFracTSMI.Checked = true;
			FracMode = false;
		}
		private void комплексноеComplexTSMI_Click(object sender, EventArgs e) {
			комплексноеComplexTSMI.Checked = true;
			действительноеComplexTSMI.Checked = false;
			ComplexMode = true;
		}
		private void действительноеComplexTSMI_Click(object sender, EventArgs e) {
			комплексноеComplexTSMI.Checked = false;
			действительноеComplexTSMI.Checked = true;
			ComplexMode = false;
		}
	}
}
