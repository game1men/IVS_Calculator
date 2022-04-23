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

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfMath.Controls;
using System.Globalization;
using MathLib;

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
        public string EquationTextBox { get; set; } = @"";
        public string MainTextBox { get; set; } = @"";
        public int MainTextFontSize { get; set; } = 35;
        public string DotText { get; set; } = ".";
        public string DeleteText { get; set; } = "CE";

        //Logic
        public int OperandNumber { get; set; } = 0;/**< indicates which operand of function is now inputed */
        public double LastInput { get; set; } = 0;/**< stores last input*/
        public bool IsNewInput { get; set; } = false;/**< indicates if last input was finished */
        public string LastOperation { get; set; } = "";/**< indicates what was last operation */
        public bool WasError { get; set; } = false;/**< indicates if there was some kind of error*/

        //Constansts
        public const string CantDivideByZero  = "Nelze dělit 0";
        public const string NCantBeZero = "n nesmí být 0";
        public const string NHasToBeInteger = "n musí být celé číslo";
        public const string XHasToBeInteger = "n musí být celé číslo";
        public const string XHasToBePositive = "x musí být kladné číslo";
        public const string MainTextBoxFormating = "G14";



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
        }

        /// <summary>
        /// Handles operations that take only one argument
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        /// <returns>True if operation was found</returns>
        private bool OneArgumentOperations(string formula) {


            switch (formula) {
                case @"\sqrt[2]{x}":
                EquationTextBox = @"\sqrt[2]{" + MainTextBox + "} =";
                if (double.Parse(MainTextBox) < 0) {
                    ShowError(XHasToBePositive);
                    return true;
                }
                MainTextBox = "" + CalcMathLib.Root(double.Parse(MainTextBox), 2).ToString(MainTextBoxFormating);
                break;

                case @"x!":
                EquationTextBox = @"" + MainTextBox + "! =";
                if (double.Parse(MainTextBox) < 0) {
                     ShowError(CantDivideByZero);
                    return true;
                }
                if (double.Parse(MainTextBox) % 1 != 0 || MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                    ShowError(XHasToBeInteger);
                    WasError = true;
                    return true;
                }
                MainTextBox = "" + CalcMathLib.Factorial(int.Parse(MainTextBox)).ToString(MainTextBoxFormating);
                break;

                case @"x^2":
                EquationTextBox = MainTextBox + "^2 =";
                MainTextBox = "" + CalcMathLib.Power(double.Parse(MainTextBox), 2).ToString(MainTextBoxFormating);

                break;

                case @"=":
                if (!EquationTextBox.Contains('=') && EquationTextBox != "") {
                    EquationTextBox += "=";
                    OperandNumber = 0;
                    IsNewInput = true;
                }

                break;
                default:
                return false;

            }
            OperandNumber = 0;
            IsNewInput = true;
            return true;
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
                if (IsNewInput && OperandNumber != 0) {
                    MainTextBox = "-";
                    IsNewInput = false;
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

            if (MainTextBox == "" || MainTextBox == "-" || WasError) {
                MainTextBox = "0";
            }

            //Checks if this is first operand of operation
            if (OperandNumber == 0) {

                if (OneArgumentOperations(formula))
                    return;

                LastInput = double.Parse(MainTextBox);
                LastOperation = formula;
                OperandNumber = 1;
                IsNewInput = true;
                FormatEquationTextBox(formula);
                return;
            }

            //determines which  two operator operation to use 
            switch (LastOperation) {
                case "+":
                EquationTextBox = "" + LastInput + " " + LastOperation + " " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Add(LastInput, double.Parse(MainTextBox)).ToString(MainTextBoxFormating);

                break;
                case "-":
                EquationTextBox = "" + LastInput + " " + LastOperation + " " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Sub(LastInput, double.Parse(MainTextBox)).ToString(MainTextBoxFormating);

                break;
                case @"\times":
                EquationTextBox = "" + LastInput + " " + LastOperation + " " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Mul(LastInput, double.Parse(MainTextBox)).ToString(MainTextBoxFormating);

                break;
                case @"\div":
                EquationTextBox = "" + LastInput + " " + LastOperation + " " + MainTextBox;
                if (double.Parse(MainTextBox) == 0) {
                    ShowError(CantDivideByZero);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Div(LastInput, double.Parse(MainTextBox)).ToString(MainTextBoxFormating);

                break;
                case @"\sqrt[n]{x}":
                EquationTextBox = @"\sqrt[" + MainTextBox + "]{" + LastInput + "}";
                if (LastInput < 0) {
                    ShowError(XHasToBePositive);
                    return;
                }
                if (double.Parse(MainTextBox) == 0) {
                    ShowError(NCantBeZero);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Root(LastInput, double.Parse(MainTextBox)).ToString(MainTextBoxFormating);

                break;
                case @"mod":
                EquationTextBox = "" + LastInput + " " + LastOperation + " " + MainTextBox;
                if (double.Parse(MainTextBox) == 0) {
                    ShowError(CantDivideByZero);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Mod(LastInput, double.Parse(MainTextBox)).ToString(MainTextBoxFormating);

                break;
                case @"x^n":
                EquationTextBox = "" + LastInput + "^{" + MainTextBox+"}";
                if (double.Parse(MainTextBox) % 1 != 0 || MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                    ShowError(NHasToBeInteger);
                    return;
                }
                if (double.Parse(MainTextBox) == 0) {
                    ShowError(NCantBeZero);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Power(LastInput, double.Parse(MainTextBox)).ToString(MainTextBoxFormating);

                break;
            }

            if (OneArgumentOperations(formula))
                return;

            LastInput = double.Parse(MainTextBox);
            LastOperation = formula;
            IsNewInput = true;//sets flag to clear MainTextBox when new number is inputed

            FormatEquationTextBox(formula);

        }
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

        private void Number_Executed(object sender, ExecutedRoutedEventArgs e) {
         

            if (e.Parameter.ToString() == "0" && (MainTextBox == "-0" || MainTextBox == "0")) {
                IsNewInput = false;
                return;
            }
            DeleteText = "CE";
            if (WasError == true) {
                ResetCalc();
            }
            if (IsNewInput) {
                MainTextBox = "";
            }
            if (IsNewInput && OperandNumber == 0) { //resets calculator when last equation was finished and number is inputed
                ResetCalc();
            }
            if (MainTextBox.Length > 18) {//size restriction of MainTextBox
                return;
            }
            MainTextBox += e.Parameter.ToString();
            IsNewInput = false;

        }
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (WasError == true) {
                ResetCalc();
            }
            switch (e.Parameter.ToString()) {
                case "Back":
                if (IsNewInput) {
                    MainTextBox = "";
                }
                if (MainTextBox.Length >= 1) {
                    MainTextBox = MainTextBox.Substring(0, MainTextBox.Length - 1);
                    //checks if decimal separator is not last character
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
                DeleteText = "CE";
                break;
            }
        }

        private void Dot_Executed(object sender, ExecutedRoutedEventArgs e) {
           
            if (IsNewInput && OperandNumber == 0) { //resets calculator when last equation was finished and number is inputed
                ResetCalc();
            }
            if (IsNewInput) {
                MainTextBox = "";
            }
            if (WasError == true) {
                MainTextBox = "";
                WasError = false;
            }
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
