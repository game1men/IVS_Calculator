/*****************************************
 * This application uses Fody Property Changed NuGet package, the licence is stated bellow:
 * The MIT License

Copyright (c) 2012 Simon Cropp and contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

* https://github.com/Fody/PropertyChanged
 *****************************************/

using MathLib;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace GUI_Application {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        public MainWindow() {
            InitializeComponent();
            DataContext = this; // This enables binding to properties
            DotText = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }
#pragma warning disable CS0067
        public event PropertyChangedEventHandler? PropertyChanged; // Binding event
#pragma warning restore CS0067
        //UI binding 
        public string EquationTextBox { get; set; } = @"";///<summary> text EquationTextBox </summary>/
        public string MainTextBox { get; set; } = @""; /// <summary> text MainTextBox </summary>
        public int MainTextFontSize { get; set; } = 35;/// <summary> font size of MainText</summary>
        public string DotText { get; set; } = ".";/// <summary> text of number separator button </summary>
        public string DeleteText { get; set; } = "C";/// <summary> text of delete button </summary>

        //Logic
        private double LastInput { get; set; } = 0;/// <summary> stores last input</summary>
        private int OperandNumber { get; set; } = 0;/// <summary> indicates which operand of function is now inputed </summary>
        private bool IsNewInput { get; set; } = false;/// <summary> indicates if last input was finished </summary>
        private string LastOperation { get; set; } = "";/// <summary> indicates what was last operation </summary>
        private bool WasError { get; set; } = false;/// <summary> indicates if there was some kind of error</summary>

        //Constansts
        private const string CANT_DIVIDE_BY_ZERO = "Nelze dělit 0";
        private const string N_CANT_BE_ZERO = "n nesmí být 0";
        private const string N_HAS_TO_BE_INTEGER = "n musí být celé číslo";
        private const string X_HAS_TO_BE_INTEGER = "n musí být celé číslo";
        private const string X_HAS_TO_BE_POSITIVE = "x musí být kladné číslo";
        private const string MAIN_TEXT_BOX_FORMATING = "G14";
        private const int MAINT_TEXT_BOX_LENGTH = 18;



        /// <summary>
        /// Resets calculator to default state
        /// </summary>
        private void ResetCalc() {
            MainTextBox = "";
            EquationTextBox = "";
            WasError = false;
            OperandNumber = 0;
            LastInput = 0;
            IsNewInput = false;
            LastOperation = "";
            DeleteText = "C";
        }

        /// <summary>
        /// Handles operations that take only one argument
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        /// <returns>True if operation was found</returns>
        private bool OneArgumentOperations(string formula) {

            //determines which operation to use
            switch (formula) {
                case @"\sqrt[2]{x}":
                EquationTextBox = @"\sqrt[2]{" + MainTextBox + "} =";//formats EquationTextBox
                if (double.Parse(MainTextBox) < 0) {
                    ShowError(X_HAS_TO_BE_POSITIVE);
                    return true;
                }
                MainTextBox = "" + CalcMathLib.Root(double.Parse(MainTextBox), 2).ToString(MAIN_TEXT_BOX_FORMATING);//does operation and shows output on MainTextBox
                break;

                case @"x!":
                EquationTextBox = @"" + MainTextBox + "! =";
                if (double.Parse(MainTextBox) < 0) {
                    ShowError(CANT_DIVIDE_BY_ZERO);
                    return true;
                }
                if (double.Parse(MainTextBox) % 1 != 0 || MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                    ShowError(X_HAS_TO_BE_INTEGER);
                    return true;
                }
                MainTextBox = "" + CalcMathLib.Factorial(int.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);
                break;

                case @"x^2":
                EquationTextBox = MainTextBox + "^2 =";
                MainTextBox = "" + CalcMathLib.Power(double.Parse(MainTextBox), 2).ToString(MAIN_TEXT_BOX_FORMATING);

                break;

                case @"=":
                if (!EquationTextBox.Contains('=') && EquationTextBox != "") {
                    EquationTextBox += "=";
                }

                break;
                default:
                return false;
            }
            //set settings for next input
            OperandNumber = 0;
            IsNewInput = true;
            return true;
        }

        /// <summary>
        /// Handles operations that take two arguments
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        private void TwoArgumentOperations(string formula) {

            //determines which two operator operation to use 
            switch (LastOperation) {
                case "+":
                EquationTextBox = "" + LastInput + " + " + MainTextBox;//formats EquationTextBox
                MainTextBox = "" + CalcMathLib.Add(LastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);//does operation and shows output on MainTextBox

                break;
                case "-":
                EquationTextBox = "" + LastInput + " - " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Sub(LastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"\times":
                EquationTextBox = "" + LastInput + @" \times " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Mul(LastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"\div":
                EquationTextBox = "" + LastInput + @" \div " + MainTextBox;

                if (double.Parse(MainTextBox) == 0) {
                    ShowError(CANT_DIVIDE_BY_ZERO);
                    return;
                }

                MainTextBox = "" + CalcMathLib.Div(LastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"\sqrt[n]{x}":
                EquationTextBox = @"\sqrt[" + MainTextBox + "]{" + LastInput + "}";
                if (LastInput < 0) {
                    ShowError(X_HAS_TO_BE_POSITIVE);
                    return;
                }
                if (double.Parse(MainTextBox) == 0) {
                    ShowError(N_CANT_BE_ZERO);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Root(LastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"mod":
                EquationTextBox = "" + LastInput + " mod " + MainTextBox;
                if (double.Parse(MainTextBox) == 0) {
                    ShowError(CANT_DIVIDE_BY_ZERO);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Mod(LastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"x^n":
                EquationTextBox = "" + LastInput + "^{" + MainTextBox + "}";
                if (double.Parse(MainTextBox) % 1 != 0 || MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                    ShowError(N_HAS_TO_BE_INTEGER);
                    return;
                }
                if (double.Parse(MainTextBox) == 0 && LastInput == 0) {
                    ShowError(N_CANT_BE_ZERO);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Power(LastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
            }//switch for determining which two operator operation to use 

            //set settings for next input
            LastInput = double.Parse(MainTextBox);
            LastOperation = formula;
            IsNewInput = true;//sets flag to clear MainTextBox when new number is inputed

            FormatEquationTextBox(formula);
            return;
        }

        /// <summary>
        /// Formats EquationTextBox according to operation 
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        void FormatEquationTextBox(string formula) {

            switch (LastOperation) {
                case @"\sqrt[n]{x}":
                EquationTextBox = @"\sqrt[n]{" + MainTextBox + "}";

                break;
                case @"x^n":
                EquationTextBox = "" + MainTextBox + "^n";

                break;
                case @"=":

                break;
                default:
                EquationTextBox = "" + MainTextBox + " " + formula;

                break;
            }
        }
        /// <summary>
        /// Shows error and raises err flag
        /// </summary>
        /// <param name="err">String which shows to user</param>
        private void ShowError(string err) {
            MainTextBox = err;
            WasError = true;
            return;
        }

        /// <summary>
        /// Handles math operations
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        private void MathOperations(string? formula) {

            if (formula == null) {
                return;
            }
            if (formula == "-") {
                //sets number as negative if '-' and newInput is set 
                if (IsNewInput && OperandNumber != 0) {//(OperandNumber cant be 0 because there would be no way of knowing if it is sing or operation)
                    MainTextBox = "-";
                    IsNewInput = false;//set IsNewInput to false so it is not erased when next number is imputed 
                    return;
                }
                //sets number as negative if '-' is pressed and MainTextBox is empty
                if (MainTextBox == "") {
                    MainTextBox = "-";
                    return;
                }
            }
            //inverts number sign
            if (MainTextBox == "-" && (formula == "-" || formula == "+")) {
                MainTextBox = "";
                return;
            }
            //removes decimal separator if it is last character
            if (MainTextBox.EndsWith(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                MainTextBox = MainTextBox.Substring(0, MainTextBox.Length - 1);
            }
            //if input is invalid it will change it to 0
            if (MainTextBox == "" || MainTextBox == "-" || WasError) {
                MainTextBox = "0";
            }
            //Checks if this is first operand of operation
            if (OperandNumber == 0) {

                //if formula is one argument operation return;
                if (OneArgumentOperations(formula))
                    return;

                //set settings for next input
                LastInput = double.Parse(MainTextBox);
                LastOperation = formula;
                OperandNumber = 1;
                IsNewInput = true;

                FormatEquationTextBox(formula);
                return;
            }
            TwoArgumentOperations(formula);
            OneArgumentOperations(formula);
        }
        /// <summary>
        /// Handles math operations
        /// </summary>
        private void Function_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (WasError == true) {
                ResetCalc();
            }
            MathOperations(e.Parameter.ToString());
            if (MainTextBox != "") {
                DeleteText = "CE";
            } else {
                DeleteText = "C";
            }
        }

        /// <summary>
        /// adds number to MainTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_Executed(object sender, ExecutedRoutedEventArgs e) {

            //checks if zero is not only character
            if (MainTextBox == "-0" || MainTextBox == "0") {

                if (e.Parameter.ToString() == "0") {//user cant input just zeros
                    IsNewInput = false;
                    return;
                } else {//remove zero so it can be replaced by number
                    MainTextBox = "";
                }

            }
            DeleteText = "CE";
            formatMainTextBox();
            if (MainTextBox.Length > MAINT_TEXT_BOX_LENGTH) {//size restriction of MainTextBox
                return;
            }
            MainTextBox += e.Parameter.ToString();//adds inputed number to MainTextBox
            IsNewInput = false;
        }
        /// <summary>
        /// Handles deleting of text
        /// </summary>
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (WasError == true) {
                ResetCalc();
            }
            switch (e.Parameter.ToString()) {//chooses from different options of deleting
                case "Back":
                if (IsNewInput) { //if last input was finished, clear whole MainTextBox
                    MainTextBox = "";
                }
                if (MainTextBox.Length >= 1) {
                    MainTextBox = MainTextBox.Substring(0, MainTextBox.Length - 1);//removes one character from MainTextBox
                    //checks if decimal separator or - is not last character
                    if (MainTextBox.EndsWith(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) || MainTextBox.EndsWith("-")) {
                        MainTextBox = MainTextBox.Substring(0, MainTextBox.Length - 1);
                    }
                }
                break;
                case "Escape":
                ResetCalc();
                break;
                case "Delete":
                MainTextBox = "";
                break;
                case "CE":
                if (MainTextBox == "" || EquationTextBox == "") {
                    ResetCalc();
                } else {
                    MainTextBox = "";
                    DeleteText = "C";
                }
                break;
                case "C":
                ResetCalc();
                break;
            }
        }
        /// <summary>
        /// Formats MainTextBox by flags which are set
        /// </summary>
        private void formatMainTextBox() {
            if (IsNewInput && OperandNumber == 0) { //resets calculator when last equation was finished and number is inputed
                ResetCalc();
            }
            if (IsNewInput) { //clear text box, if last input was finished
                MainTextBox = "";
            }
            if (WasError == true) {//clear text box, if there was an error
                MainTextBox = "";
                WasError = false;
            }
        }
        /// <summary>
        /// Adds dot to the MainTextBox.
        /// </summary>
        private void Dot_Executed(object sender, ExecutedRoutedEventArgs e) {

            formatMainTextBox();
            if (MainTextBox.Length > 18) {//size restriction of MainTextBox
                return;
            }
            //checks if it does not contain number decimal separator already
            if (!MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) && MainTextBox != "") {
                MainTextBox += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
            if (MainTextBox == "") {
                DeleteText = "C";
            }
        }
    }
}
