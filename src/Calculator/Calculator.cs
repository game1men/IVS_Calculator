using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib;

namespace GUI_Application {
    public class Calculator {



        private const string CANT_DIVIDE_BY_ZERO = "Nelze dělit 0";
        private const string N_CANT_BE_ZERO = "n nesmí být 0";
        private const string N_HAS_TO_BE_INTEGER = "n musí být celé číslo";
        private const string X_HAS_TO_BE_INTEGER = "n musí být celé číslo";
        private const string X_HAS_TO_BE_POSITIVE = "x musí být kladné číslo";
        private const string MAIN_TEXT_BOX_FORMATING = "G14";
        // Logic
        public double _lastInput { get; set; } = 0;
        public int _operandNumber { get; set; } = 0;
        public bool _isNewInput { get; set; } = false;
        public string _lastOperation { get; set; } = string.Empty;
        public bool _wasError { get; set; } = false;
        public int _equalSignPressedInARow { get; set; } = 0;
        public double _iterationNumber { get; set; } = 0;
        public string _iterationOperation { get; set; } = "";

        /// <summary>
        /// Resets calculator to default state
        /// </summary>
        private void ResetCalc() {
            _wasError = false;
            _operandNumber = 0;
            _lastInput = 0;
            _isNewInput = false;
            _lastOperation = "";
            _equalSignPressedInARow = 0;
            _iterationNumber = 0;
            _iterationOperation = "";
        }


        /// <summary>
        /// Handles operations that take one argument
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        /// <param name="equation">output of formated equation</param>
        /// <param name="outputNumber">calculated number</param>
        /// <param name="errMessage"></param>
        /// <returns>1 if operation was found 0 if operation was not found -1 if there was error</returns>
        private int OneArgumentOperations(string formula, double operand, out string equation, out double outputNumber, out string errMessage) {
            outputNumber = operand;
            equation = "";
            errMessage = "";
            //determines which operation to use
            switch (formula) {
                case @"\sqrt[2]{x}":
                equation = @"\sqrt[2]{" + operand + "} =";//formats EquationTextBox
                if (operand < 0) {
                    errMessage = X_HAS_TO_BE_POSITIVE;
                    return -1;
                }
                outputNumber = CalcMathLib.Root(operand, 2);//does operation and shows output on MainTextBox
                break;

                case @"x!":
                equation = @"" + operand + "! =";
                if (operand < 0) {
                    errMessage = CANT_DIVIDE_BY_ZERO;
                    return -1;
                }
                if (operand % 1 != 0) {
                    errMessage = X_HAS_TO_BE_INTEGER;
                    return -1;
                }
                outputNumber = CalcMathLib.Factorial((int)operand);
                break;

                case @"x^2":
                equation = operand + "^2 =";
                outputNumber = CalcMathLib.Power(operand, 2);

                break;

                default:
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// Handles operations that take two arguments
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        /// <param name="firstOperand"></param>
        /// <param name="secondOperand"></param>
        /// <param name="equation">output of formated equation</param>
        /// <param name="outputNumber">calculated number</param>
        /// <param name="errMessage"></param>
        /// <returns>1 if operation was found 0 if operation was not found -1 if there was error</returns>
        private int TwoArgumentOperations(string formula, double firstOperand, double secondOperand, out string equation, out double outputNumber, out string errMessage) {
            outputNumber = 0;
            equation = "";
            errMessage = "";
            //determines which two operator operation to use 
            switch (formula) {
                case "+":
                equation = "" + firstOperand + " + " + secondOperand;//formats EquationTextBox
                outputNumber = CalcMathLib.Add(firstOperand, secondOperand);//does operation and shows output on MainTextBox

                return 1;
                case "-":
                equation = "" + firstOperand + " - " + secondOperand;
                outputNumber = CalcMathLib.Sub(firstOperand, secondOperand);

                return 1;
                case @"\times":
                equation = "" + firstOperand + @" \times " + secondOperand;
                outputNumber = CalcMathLib.Mul(firstOperand, secondOperand);

                return 1;
                case @"\div":
                equation = "" + firstOperand + @" \div " + secondOperand;

                if (secondOperand == 0) {
                    errMessage = CANT_DIVIDE_BY_ZERO;
                    return -1;
                }

                outputNumber = CalcMathLib.Div(firstOperand, secondOperand);

                return 1;
                case @"\sqrt[n]{x}":
                equation = @"\sqrt[" + secondOperand + "]{" + firstOperand + "}";
                if (firstOperand < 0) {
                    errMessage = X_HAS_TO_BE_POSITIVE;
                    return -1;
                }
                if (secondOperand == 0) {
                    errMessage = N_CANT_BE_ZERO;
                    return -1;
                }
                outputNumber = CalcMathLib.Root(firstOperand, secondOperand);

                return 1;
                case @"mod":
                equation = "" + firstOperand + " mod " + secondOperand;
                if (secondOperand == 0) {
                    errMessage = CANT_DIVIDE_BY_ZERO;
                    return -1;
                }
                outputNumber = CalcMathLib.Mod(firstOperand, secondOperand);

                return 1;
                case @"x^n":
                equation = "" + firstOperand + "^{" + secondOperand + "}";
                if (secondOperand % 1 != 0) {
                    errMessage = N_HAS_TO_BE_INTEGER;
                    return -1;
                }
                if (secondOperand == 0 && firstOperand == 0) {
                    errMessage = N_CANT_BE_ZERO;
                    return -1;
                }
                outputNumber = CalcMathLib.Power(firstOperand, (long)secondOperand);

                return 1;
            }//switch for determining which two operator operation to use 
            return 0;
        }

        /// <summary>
        /// Formats EquationTextBox according to arguments
        /// </summary>
        /// <param name="formula">String containing operation type by which it choses formating</param>
        /// <param name="firstOperand"></param>
        /// <param name="secondOperand"></param>
        /// <param name="operation">String containing operation which will be used in formating</param>
        string FormatEquationTextBox(string formula, double firstOperand, double secondOperand, string operation, string equationIn) {
            string formatedEquation = equationIn;
            switch (formula) {
                case @"\sqrt[n]{x}":
                formatedEquation = @"\sqrt[n]{" + firstOperand + "}";

                break;
                case @"x^n":
                formatedEquation = "" + firstOperand + "^n";
                break;
                case @"x^2":
                case @"\sqrt[2]{x}":
                case @"x!":
                break;
                case @"=":
                if (!equationIn.Contains('=') && equationIn != "") {
                    formatedEquation += "=";
                }
                break;
                default:
                formatedEquation = "" + firstOperand + " " + operation;

                break;
            }
            //formats EquationTextBox when was iteration operation set
            if (_equalSignPressedInARow > 1 && _operandNumber != 0) {
                formatedEquation = "" + firstOperand + " " + secondOperand + " " + operation + " =";
            }
            return formatedEquation;
        }

        /// <summary>
        /// Shows error and raises err flag
        /// </summary>
        /// <param name="err">String which shows to user</param>
        private void ShowError(string err) {
            _wasError = true;
            return;
        }

        /// <summary>
        /// Handles math operations
        /// </summary>
        /// <param name="formula">String containing operation type</param>
        public int Calculate(string formula, double operand, string equationNow, out string outEquation, out double outNumber, out string errMessage) {

            outEquation = equationNow;
            outNumber = operand;
            string tempOutEquation = equationNow;
            double tempOutNumber = operand;
            errMessage = "";
            int outValue;
            //iterating operation when equals pressed multiple times
            if (_equalSignPressedInARow >= 1 && formula == "=") {//when equals was pressed second do iterating            
                TwoArgumentOperations(_iterationOperation, operand, _iterationNumber, out outEquation, out outNumber, out errMessage);
                outEquation = FormatEquationTextBox(formula, _lastInput, _iterationNumber, _iterationOperation, outEquation);
                return 0;
            } else if (equationNow != "" && formula == "=" && (_lastOperation == "+" || _lastOperation == "-" || _lastOperation == "*" || _lastOperation == "/")) {//when equals was pressed for first time in row set iteration values
                _iterationOperation = _lastOperation; //last operation will be used for iterating
                _iterationNumber = operand; //first inputed number will be used for iterating
                _equalSignPressedInARow++;
            } else {//if other operation than equals was pressed
                _equalSignPressedInARow = 0;
            }

            //Checks if this is first operand of operation
            if (_operandNumber == 0) {
                //if formula is one argument operation do it and return
                outValue = OneArgumentOperations(formula, operand, out tempOutEquation, out tempOutNumber, out errMessage);
                if (outValue == -1) {

                    ShowError(errMessage);
                    return -1;
                } else if (outValue == 1) {
                    outEquation = tempOutEquation;
                    outNumber = tempOutNumber;
                    return 0;
                }

                //set settings for next input
                _lastInput = operand;
                _lastOperation = formula;
                _operandNumber = 1;
                _isNewInput = true;

                outEquation = FormatEquationTextBox(formula, operand, 0, formula, outEquation);
                return 0;
            }

            outValue = TwoArgumentOperations(_lastOperation, _lastInput, operand, out tempOutEquation, out tempOutNumber, out errMessage);
            if (outValue == -1) {
                ShowError(errMessage);
                return -1;
            } else if (outValue == 1) {
                outEquation = tempOutEquation;
                outNumber = tempOutNumber;
            }

            outEquation = FormatEquationTextBox(formula, operand, 0, formula, outEquation);

            outValue = OneArgumentOperations(formula, operand, out tempOutEquation, out tempOutNumber, out errMessage);
            if (outValue == -1) {
                ShowError(errMessage);
                return -1;
            } else if (outValue == 1) {
                outEquation = tempOutEquation;
                outNumber = tempOutNumber;
            }
            //set settings for next input
            _lastInput = operand;
            _lastOperation = formula;
            _isNewInput = true;//sets flag to clear MainTextBox when new number is inputed
            _operandNumber = 0;
            return 0;
        }

    }
}
