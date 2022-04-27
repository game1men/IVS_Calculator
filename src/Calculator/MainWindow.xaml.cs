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
        ///<summary>Text EquationTextBox</summary>
        public string EquationTextBox { get; set; } = string.Empty;
        /// <summary>Text MainTextBox</summary>
        public string MainTextBox { get; set; } = string.Empty;
        /// <summary>Font size of MainText</summary>
        public int MainTextFontSize { get; set; } = 35;
        /// <summary>Text of number separator button</summary>
        public string DotText { get; set; } = ".";
        /// <summary>Text of delete button</summary>
        public string DeleteText { get; set; } = "C";

        // Logic
        private double _lastInput = 0;
        private int _operandNumber = 0;
        private bool _isNewInput = false;
        private string _lastOperation = string.Empty;
        private bool _wasError = false;
        bool _wasEqualsPressed = false;
        double _iterationNumber = 0;
        string _iterationOperation = "";
        int _iterationOperandNumber = 0;

        //Constants
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
            _wasError = false;
            _operandNumber = 0;
            _lastInput = 0;
            _isNewInput = false;
            _lastOperation = "";
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
            _lastOperation = formula;
            _operandNumber = 0;
            _isNewInput = true;
            return true;
        }

        /// <summary>
        /// Handles operations that take two arguments
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        private void TwoArgumentOperations(string formula) {

            //determines which two operator operation to use 
            switch (_lastOperation) {
                case "+":
                EquationTextBox = "" + _lastInput + " + " + MainTextBox;//formats EquationTextBox
                MainTextBox = "" + CalcMathLib.Add(_lastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);//does operation and shows output on MainTextBox

                break;
                case "-":
                EquationTextBox = "" + _lastInput + " - " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Sub(_lastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"\times":
                EquationTextBox = "" + _lastInput + @" \times " + MainTextBox;
                MainTextBox = "" + CalcMathLib.Mul(_lastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"\div":
                EquationTextBox = "" + _lastInput + @" \div " + MainTextBox;

                if (double.Parse(MainTextBox) == 0) {
                    ShowError(CANT_DIVIDE_BY_ZERO);
                    return;
                }

                MainTextBox = "" + CalcMathLib.Div(_lastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"\sqrt[n]{x}":
                EquationTextBox = @"\sqrt[" + MainTextBox + "]{" + _lastInput + "}";
                if (_lastInput < 0) {
                    ShowError(X_HAS_TO_BE_POSITIVE);
                    return;
                }
                if (double.Parse(MainTextBox) == 0) {
                    ShowError(N_CANT_BE_ZERO);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Root(_lastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"mod":
                EquationTextBox = "" + _lastInput + " mod " + MainTextBox;
                if (double.Parse(MainTextBox) == 0) {
                    ShowError(CANT_DIVIDE_BY_ZERO);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Mod(_lastInput, double.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
                case @"x^n":
                EquationTextBox = "" + _lastInput + "^{" + MainTextBox + "}";
                if (double.Parse(MainTextBox) % 1 != 0 || MainTextBox.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) {
                    ShowError(N_HAS_TO_BE_INTEGER);
                    return;
                }
                if (double.Parse(MainTextBox) == 0 && _lastInput == 0) {
                    ShowError(N_CANT_BE_ZERO);
                    return;
                }
                MainTextBox = "" + CalcMathLib.Power(_lastInput, long.Parse(MainTextBox)).ToString(MAIN_TEXT_BOX_FORMATING);

                break;
            }//switch for determining which two operator operation to use 

            //set settings for next input
            _lastInput = double.Parse(MainTextBox);
            _lastOperation = formula;
            _isNewInput = true;//sets flag to clear MainTextBox when new number is inputed

            FormatEquationTextBox(formula);
            return;
        }

        /// <summary>
        /// Formats EquationTextBox according to operation 
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        void FormatEquationTextBox(string formula) {

            switch (_lastOperation) {
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
            _wasError = true;
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
 
            //iterating operation when equals pressed multiple times
            if (_wasEqualsPressed && formula == "=") {//when equals was pressed second time load iteration values
                _operandNumber = _iterationOperandNumber;               
                if (_operandNumber != 0) {//in two operand operations it needs to set _lastInput and MainTextBox
                    _lastInput = double.Parse(MainTextBox);
                    MainTextBox = _iterationNumber.ToString(MAIN_TEXT_BOX_FORMATING);
                    _lastOperation = _iterationOperation;
                } else {
                    formula = _iterationOperation;
                }   
            } else if (formula == "=") {//when equals was pressed for first time in row set iteration values
                _iterationOperation = _lastOperation; //last operation will be used for iterating
                _iterationNumber = double.Parse(MainTextBox); //first inputed number will be used for iterating
                _wasEqualsPressed = true;
                _iterationOperandNumber = _operandNumber;//sets right operand
            } else {//if other operation than equals was pressed
                _wasEqualsPressed = false;
            }

            if (formula == "-") {
                //sets number as negative if '-' and newInput is set 
                if (_isNewInput && _operandNumber != 0 && !_wasEqualsPressed) {//(OperandNumber cant be 0 because there would be no way of knowing if it is sing or operation)
                    MainTextBox = "-";
                    _isNewInput = false;//set IsNewInput to false so it is not erased when next number is imputed 
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
            if (MainTextBox == "" || MainTextBox == "-" || _wasError) {
                MainTextBox = "0";
            }
            //Checks if this is first operand of operation
            if (_operandNumber == 0) {

                //if formula is one argument operation return;
                if (OneArgumentOperations(formula))
                    return;

                //set settings for next input
                _lastInput = double.Parse(MainTextBox);
                _lastOperation = formula;
                _operandNumber = 1;
                _isNewInput = true;

                FormatEquationTextBox(formula);
                return;
            }
            TwoArgumentOperations(formula);
            OneArgumentOperations(formula);

            //formats EquationTextBox when was iteration operation set
            if (_wasEqualsPressed && _operandNumber != 0) {
                EquationTextBox = "" + _lastInput + " " + _iterationOperation + " " + _iterationNumber + " =";
            }
        }

      

        /// <summary>
        /// Handles math operations
        /// </summary>
        private void Function_Executed(object sender, ExecutedRoutedEventArgs e) {        
            if (_wasError == true) {
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
                    _isNewInput = false;
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
            _isNewInput = false;
        }
        /// <summary>
        /// Handles deleting of text
        /// </summary>
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (_wasError == true) {
                ResetCalc();
            }
            switch (e.Parameter.ToString()) {//chooses from different options of deleting
                case "Back":
                if (_isNewInput) { //if last input was finished, clear whole MainTextBox
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
            if (_isNewInput && _operandNumber == 0) { //resets calculator when last equation was finished and number is inputed
                ResetCalc();
            }
            if (_isNewInput) { //clear text box, if last input was finished
                MainTextBox = "";
            }
            if (_wasError == true) {//clear text box, if there was an error
                MainTextBox = "";
                _wasError = false;
            }
        }
        /// <summary>
        /// Adds dot to the MainTextBox.
        /// </summary>
        private void Dot_Executed(object sender, ExecutedRoutedEventArgs e) {

            formatMainTextBox();
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
