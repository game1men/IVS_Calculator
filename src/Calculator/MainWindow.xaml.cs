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
        public string EquationTextBox { get; set; } = @"";
        public string MainTextBox { get; set; } = @"";
        public int operandNumber { get; set; } = 0;
        public double lastInput { get; set; } = 0;
        public bool newInput { get; set; } = false;
        public string lastOperation { get; set; } = "";
        public bool err { get; set; } = false;
        public int MainTextFontSize { get; set; } = 35;
        public string DotText { get; set; } = ".";
        public string DeleteText { get; set; } = "C";

        /// <summary>
        /// Resets calculator to default state
        /// </summary>
        private void reset() {
            MainTextBox = "";
            EquationTextBox = "";
            err = false;
            operandNumber = 0;
            lastInput = 0;
            newInput = false;
            lastOperation = "";
        }

        /// <summary>
        /// Handles operations that take only one argument
        /// </summary>
        /// <param name="formula">string containing operation type</param>
        /// <returns>true if operation was found</returns>
        /// <exception cref="NotImplementedException">TODO: remove</exception>
        private bool OneArgumentOperations(string formula) {

            switch (formula) {
                case @"\sqrt[2]{x}":
                EquationTextBox = @"\sqrt[2]{" + MainTextBox + "}";
                if (double.Parse(MainTextBox) < 0) {
                    MainTextBox = "x musí být kladné číslo";
                    err = true;
                    return true;
                }

                MainTextBox = "" + CalcMathLib.Root(double.Parse(MainTextBox), 2).ToString("G14");
                operandNumber = 0;
                newInput = false;
                return true;

                break;
                case @"x!":
                EquationTextBox = @"" + MainTextBox + "!";
                if (double.Parse(MainTextBox) < 0) {
                    MainTextBox = "x musí být kladné číslo";
                    err = true;
                    return true;
                }
                if (double.Parse(MainTextBox) % 1 != 0 || MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                    MainTextBox = "x musí být celé číslo";
                    err = true;
                    return true;
                }

                MainTextBox = "" + CalcMathLib.Factorial(int.Parse(MainTextBox)).ToString("G14");
                newInput = false;
                return true;

                break;
                case @"x^2":
                EquationTextBox = MainTextBox + "^2";
                MainTextBox = "" + CalcMathLib.Power(double.Parse(MainTextBox), 2).ToString("G14");
                operandNumber = 0;
                newInput = false;
                return true;

                break;
                case @"=":
                if (!EquationTextBox.Contains("=")) {
                    EquationTextBox = EquationTextBox + "=";
                    operandNumber = 0;
                    newInput = false;
                }
                return true;

                break;
            }
            return false;
        }

        /// <summary>
        /// Formats EquationTextBox acording to operation 
        /// </summary>
        /// <param name="formula">string containing operation type</param>
        void FormatEquationTextBox(string formula) {

            switch (lastOperation) {
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
        /// Handles math operations
        /// </summary>
        /// <param name="formula">string containing operation type</param>
        private void MathOperations(string formula) {

            //sets number as negative
            if (formula == "-" && newInput) {
                MainTextBox = "-";
                newInput = false;
                return;
            }
            //sets number as negative
            if (formula == "-" && MainTextBox == "") {
                MainTextBox = "-";
                return;
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

            if (MainTextBox == "" || MainTextBox == "-") {
                MainTextBox = "0";
            }

            //Checks if this is first operand of operation
            if (operandNumber == 0) {

                if (OneArgumentOperations(formula))
                    return;

                lastInput = double.Parse(MainTextBox);
                lastOperation = formula;
                operandNumber = 1;
                newInput = true;
                FormatEquationTextBox(formula);
                return;
            }

            //determines whicht two operator operation to use 
            switch (lastOperation) {
                case "+":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Add(lastInput, double.Parse(MainTextBox)).ToString("G14");

                break;
                case "-":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Sub(lastInput, double.Parse(MainTextBox)).ToString("G14");

                break;
                case @"\times":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Mul(lastInput, double.Parse(MainTextBox)).ToString("G14");

                break;
                case @"\div":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                if (double.Parse(MainTextBox) == 0) {
                    MainTextBox = "Nelze dělit 0";
                    err = true;
                    return;
                }
                MainTextBox = "" + CalcMathLib.Div(lastInput, double.Parse(MainTextBox)).ToString("G14");

                break;
                case @"\sqrt[n]{x}":
                EquationTextBox = @"\sqrt[" + MainTextBox + "]{" + lastInput + "}";
                if (double.Parse(MainTextBox) < 0) {
                    MainTextBox = "x musí být kladné číslo";
                    err = true;
                    return;
                }
                MainTextBox = "" + CalcMathLib.Root(lastInput, double.Parse(MainTextBox)).ToString("G14");

                break;
                case @"mod":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                if (double.Parse(MainTextBox) == 0) {
                    MainTextBox = "Nelze dělit 0";
                    err = true;
                    return;
                }
                MainTextBox = "" + CalcMathLib.Mod(lastInput, double.Parse(MainTextBox)).ToString("G14");

                break;
                case @"x^n":
                EquationTextBox = "" + lastInput + "^" + MainTextBox;
                if (double.Parse(MainTextBox) % 1 != 0 || MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                    MainTextBox = "n musí být celé číslo";
                    err = true;
                    return;
                }
                if (MainTextBox == "0") {
                    MainTextBox = "n nesmí být 0";
                    err = true;
                    return;
                }
                MainTextBox = "" + CalcMathLib.Power(lastInput, double.Parse(MainTextBox)).ToString("G14");

                break;
            }

            if (OneArgumentOperations(formula))
                return;

            lastInput = double.Parse(MainTextBox);
            lastOperation = formula;
            newInput = true;//sets flag to clear MainTextBox when new number is inputed

            FormatEquationTextBox(formula);

        }
        private void Function_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (err == true) {
                reset();
            }
            MathOperations(e.Parameter.ToString());
            if (MainTextBox != "") {
                DeleteText = "C";
            } else {
                DeleteText = "CE";
            }

        }

        private void Number_Executed(object sender, ExecutedRoutedEventArgs e) {

            DeleteText = "C";
            if (err == true) {
                reset();
            }
            if (newInput) {
                MainTextBox = "";
            }
            MainTextBox += e.Parameter.ToString();
            newInput = false;

        }
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (err == true) {
                reset();
            }
            switch (e.Parameter.ToString()) {
                case "Back":
                if (newInput) {
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
                reset();
                break;
                case "Delete":
                MainTextBox = "";
                break;
                case "C":
                if (MainTextBox == "" || EquationTextBox == "") {
                    reset();
                } else {
                    MainTextBox = "";
                    DeleteText = "CE";
                }
                break;
                case "CE":
                reset();
                DeleteText = "C";
                break;
            }
        }

        private void Dot_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (err == true) {
                MainTextBox = "";
                err = false;
            }
            //checks if it does not contain number decimal separator alredy
            if (!MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) && MainTextBox != "") {
                MainTextBox += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
            if (MainTextBox == "") {
                DeleteText = "CE";
            }
        }
    }
}
