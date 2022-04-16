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

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            // Will be implemented in WPF TODO: Remove this function
        }

        private void Number_Click(object sender, RoutedEventArgs e) {

            if (sender is ContentControl c) {
                if (c.Content is TextBlock t) {
                    //if error has occured reset everything
                    if (err == true) {
                        reset();
                    }
                    if (newInput) {
                        MainTextBox = "";
                    }
                    MainTextBox += t.Text;
                    newInput = false;
                }
            }
        }

        //TODO: Remove this function
        private void MathFunction_Click(object sender, RoutedEventArgs e) {
            MathOperation_Click(sender, e); // TEMPORARY
        }
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
               
                MainTextBox = "" + CalcMathLib.Root(double.Parse(MainTextBox), 2);
                operandNumber = 0;
                return true;

                break;
                case @"x!":
                EquationTextBox = @"" + MainTextBox + "!";
                if (double.Parse(MainTextBox) < 0) {
                    MainTextBox = "x musí být kladné číslo";
                    err = true;
                    return true;
                }
                if (double.Parse(MainTextBox) % 1 != 0) {
                    MainTextBox = "x musí být celé číslo";
                    err = true;
                    return true;
                }
               
                MainTextBox = "" + CalcMathLib.Factorial(int.Parse(MainTextBox));
                return true;

                break;
                case @"x^2":
                EquationTextBox = MainTextBox + "^2";
                MainTextBox = "" + CalcMathLib.Power(double.Parse(MainTextBox), 2);
                operandNumber = 0;
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
            if (MainTextBox == "-" &&(formula == "-" || formula == "+")) {
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
                MainTextBox = "" + CalcMathLib.Add(lastInput, double.Parse(MainTextBox));

                break;
                case "-":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Sub(lastInput, double.Parse(MainTextBox));

                break;
                case @"\times":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Mul(lastInput, double.Parse(MainTextBox));

                break;
                case @"\div":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                if (double.Parse(MainTextBox) == 0) {
                    MainTextBox = "Nelze dělit 0";
                    err = true;
                    return;
                }              
                MainTextBox = "" + CalcMathLib.Div(lastInput, double.Parse(MainTextBox));

                break;
                case @"\sqrt[n]{x}":
                EquationTextBox = @"\sqrt[" + MainTextBox + "]{" + lastInput + "}";
                if (double.Parse(MainTextBox) < 0) {
                    MainTextBox = "x musí být kladné číslo";
                    err = true;
                    return;
                }
                MainTextBox = "" + CalcMathLib.Root(lastInput, double.Parse(MainTextBox));

                break;
                case @"mod":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                if (double.Parse(MainTextBox) == 0) {
                    MainTextBox = "Nelze dělit 0";
                    err = true;
                    return;
                }
                MainTextBox = "" + CalcMathLib.Mod(lastInput, double.Parse(MainTextBox));

                break;
                case @"x^n":
                EquationTextBox = "" + lastInput + "^" + MainTextBox;
                if (double.Parse(MainTextBox) % 1 != 0) {
                    MainTextBox = "n musí být celé číslo";
                    err = true;
                    return;
                }
                MainTextBox = "" + CalcMathLib.Power(lastInput, double.Parse(MainTextBox));

                break;
            }

            if (OneArgumentOperations(formula))
                return;

            lastInput = double.Parse(MainTextBox);
            lastOperation = formula;
            newInput = true;//sets flag to clear MainTextBox when new number is inputed

            FormatEquationTextBox(formula);

        }

        private void MathOperation_Click(object sender, RoutedEventArgs e) {
            if (sender is ContentControl c) {
                if (err == true) {
                    reset();
                }
                if (c.Content is FormulaControl fc) {
                    MathOperations(fc.Formula);
                } else if (c.Content is TextBlock t) {
                    MathOperations(t.Text);
                }
            }
        }

        private void MathFractionDot_Click(object sender, RoutedEventArgs e) {
            if (sender is ContentControl c) {
                if (c.Content is FormulaControl) {
                    if (err == true) {
                        MainTextBox = "";
                        err = false;
                    }
                    //checks if it does not contain number decimal separator alredy
                    if (!MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) && MainTextBox != "") {
                        MainTextBox += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    }
                }
            }
        }

        private void TextClenaup_Click(object sender, RoutedEventArgs e) {
            if (sender is ContentControl c) {
                if (c.Content is TextBlock t) {
                    if (err == true) {
                        reset();
                    }
                    switch (t.Text) {
                        case "◄":
                        if (MainTextBox.Length >= 1) {
                            MainTextBox = MainTextBox.Substring(0, MainTextBox.Length - 1);
                            //checks if decimal separator is not last character
                            if (MainTextBox.EndsWith(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                                MainTextBox = MainTextBox.Substring(0, MainTextBox.Length - 1);
                            }
                        }
                        break;
                        case "C":
                        reset();
                        break;
                    }
                }
            }

        }
    }
}
