/*****************************************
 * This application uses Fody Property Changed NuGet package, the license is stated bellow:
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
        ///<summary>Text EquationTextBox</summary>
        public string EquationTextBox { get; set; } = string.Empty;
        /// <summary>Text MainTextBox</summary>
        public string MainTextBox { get; set; } = "0";
        /// <summary>Font size of MainText</summary>
        public int MainTextFontSize { get; set; } = 35;
        /// <summary>Text of number separator button</summary>
        public string DotText { get; set; } = ".";
        /// <summary>Text of delete button</summary>
        public string DeleteText { get; set; } = "C";

        //Constants
        private const string MAIN_TEXT_BOX_FORMATING = "G14";
        private const int MAINT_TEXT_BOX_LENGTH = 18;

        private Calculator calc = new();

        /// <summary>
        /// Resets calculator to default state
        /// </summary>
        private void ResetCalc() {
            MainTextBox = "0";
            EquationTextBox = "";
            DeleteText = "C";
            calc.Reset();
        }

        /// <summary>
        /// Handles math operations
        /// </summary>
        private void Function_Executed(object sender, ExecutedRoutedEventArgs e) {
            string? formula = e.Parameter.ToString();
            if (calc.WasError) {
                ResetCalc();
            }
            if (formula == null) {
                return;
            }

            if (formula == "-" && EquationTextBox == "") {
                //sets number as negative if '-' and newInput is set 
                if (calc.IsNewInput && calc.OperandNumber != 0 && calc.EqualSignPressedInARow == 0) {//(OperandNumber cant be 0 because there would be no way of knowing if it is sing or operation)
                    MainTextBox = "-";
                    calc.IsNewInput = false;//set IsNewInput to false so it is not erased when next number is imputed 
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
            if (MainTextBox == "" || MainTextBox == "-" || calc.WasError) {
                MainTextBox = "0";
            }
            //calculates operation and sets textboxes
            if (calc.Calculate(formula, double.Parse(MainTextBox), EquationTextBox, out string outEquation, out double outputNumber, out string errMessage) == -1) {
                MainTextBox = errMessage;
            } else {
                EquationTextBox = outEquation;
                MainTextBox = outputNumber.ToString(MAIN_TEXT_BOX_FORMATING);
            }

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

            if (calc.WasError == true) {
                ResetCalc();
            }
            DeleteText = "CE";
            FormatMainTextBox();
            //checks if zero is not only character
            if (MainTextBox == "-0" || MainTextBox == "0") {

                if (e.Parameter.ToString() == "0") {//user cant input just zeros
                    calc.IsNewInput = false;
                    return;
                } else {//remove zero so it can be replaced by number
                    MainTextBox = "";
                }

            }
           
            if (MainTextBox.Length > MAINT_TEXT_BOX_LENGTH) {//size restriction of MainTextBox
                return;
            }
            MainTextBox += e.Parameter.ToString();//adds inputed number to MainTextBox
            calc.IsNewInput = false;
        }

        /// <summary>
        /// Handles deleting of text
        /// </summary>
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (calc.WasError == true) {
                ResetCalc();
            }
            switch (e.Parameter.ToString()) {//chooses from different options of deleting
                case "Back":
                if (calc.IsNewInput) { //if last input was finished, clear whole MainTextBox
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
        private void FormatMainTextBox() {
            if (calc.IsNewInput && calc.OperandNumber == 0) { //resets calculator when last equation was finished and number is inputed
                ResetCalc();
            }
            if (calc.IsNewInput) { //clear text box, if last input was finished
                MainTextBox = "";
            }
            if (calc.WasError == true) {//clear text box, if there was an error
                MainTextBox = "";
                calc.WasError = false;
            }
        }

        /// <summary>
        /// Adds dot to the MainTextBox.
        /// </summary>
        private void Dot_Executed(object sender, ExecutedRoutedEventArgs e) {

            FormatMainTextBox();
            if (MainTextBox.Length > MAINT_TEXT_BOX_LENGTH) {//size restriction of MainTextBox
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
