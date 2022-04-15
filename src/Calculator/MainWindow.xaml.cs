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

        public int inputNumber { get; set; } = 0;
        public double lastInput { get; set; } = 0;
        public bool newInput { get; set; } = false;
        public string lastOperation { get; set; } = "";

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            // Keyboard press event
            throw new NotImplementedException();
        }

        private void Number_Click(object sender, RoutedEventArgs e) { 
            if(sender is ContentControl c) {
                if(c.Content is TextBlock t) {

                    if (newInput) {
                        MainTextBox = "";
                    }
                    MainTextBox += t.Text;
                    newInput = false;
                }
            }
        }

        private void MathFunction_Click(object sender, RoutedEventArgs e) {
            MathOperation_Click( sender,  e); // TEMPORARY
        }

        private bool OneArgumentOperations(string formula) {
            switch (formula) {

                case "=":
                inputNumber = 0;
                return true;
                break;
                case @"\sqrt[2]{x}":
                EquationTextBox = @"\sqrt[2]{" + MainTextBox + "}";
                MainTextBox = "" + Math.Sqrt(double.Parse(MainTextBox));
             
                inputNumber = 0;
                return true;
                break;
                case @"x!":
                throw new NotImplementedException();
                break;
                case @"x^2":
                EquationTextBox = MainTextBox + "^2";
                MainTextBox = "" + Math.Pow(double.Parse(MainTextBox), 2);

        
                inputNumber = 0;
                return true;
                break;

            }
            return false;
        }


        private void MathOperations(string formula) {




            if (inputNumber == 0) {
                if (OneArgumentOperations(formula))
                    return;
                lastInput = double.Parse(MainTextBox);
                lastOperation = formula;
                inputNumber = 1;
                newInput = true;
                return;

            }



            switch (lastOperation) {


                case "+":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + (lastInput + double.Parse(MainTextBox));

                break;
                case "-":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + (lastInput - double.Parse(MainTextBox));

                break;
                case @"\times":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + (lastInput * double.Parse(MainTextBox));

                break;
                case @"\div":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + (lastInput / double.Parse(MainTextBox));

                break;
                case @"\sqrt[n]{x}":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + Math.Pow(lastInput, 1 / double.Parse(MainTextBox));

                break;
                case @"mod":
                EquationTextBox = "" + lastInput + " " + lastOperation + " " + MainTextBox;
                MainTextBox = "" + lastInput % double.Parse(MainTextBox);

                break;
                case @"x^n":
                EquationTextBox = "" + lastInput + "^" + MainTextBox;
                MainTextBox = "" + Math.Pow(lastInput, double.Parse(MainTextBox));

                break;

            }





            if (OneArgumentOperations(formula))
                return;


            lastInput = double.Parse(MainTextBox);
            lastOperation = formula;
            newInput = true;


        }

        private void MathOperation_Click(object sender, RoutedEventArgs e) {
            if (sender is ContentControl c) {
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
                    if (!MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                        MainTextBox += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    }
                   
                }
            }
        }

        private void TextClenaup_Click(object sender, RoutedEventArgs e) {
            MainTextBox = MainTextBox.Substring(0,MainTextBox.Length-1);
        }
    }
}
