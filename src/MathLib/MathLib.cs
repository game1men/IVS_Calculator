///<summary>
/// Math Library for IVS project (Calculator)
/// 
/// This library contains basic mathematic functions 
/// such as sum, difference, multiplication, division and 
/// more complex mathemati functions such as factorial,
/// power, root and basic logarithm.
/// 
/// author Matej Smida
///</summary>

namespace MathLib {
    public static class CalcMathLib {

        /// <summary>
        /// Precision which is used in internal calculations
        /// </summary>
        public const int ROUNDING_PRECISION = 14;
     
        /// <summary>
        /// Sum of two numbers
        /// </summary>
        /// <param name="a">First number to be added</param>
        /// <param name="b">Second number to be added</param>
        /// <returns>Sum of numbers a and b</returns>
        public static double Add(double a, double b) {

            return a + b;

        }

        /// <summary>
        /// Substraction of two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Difference of numbers a and b</returns>
        public static double Sub(double a, double b) {

            return a - b;

        }

        /// <summary>
        /// Multiplication of two nubers
        /// </summary>
        /// <param name="a">First number to be multiplied</param>
        /// <param name="b">Second number to be multiplied</param>
        /// <returns>Product of two numbers</returns>
        public static double Mul(double a, double b) {

            return a * b;

        }

        /// <summary>
        /// Division of two numbers
        /// </summary>
        /// <param name="a">First number (dividend)</param>
        /// <param name="b">Second number (divisor)</param>
        /// <returns>Quotient od two numbers</returns>
        /// <exception cref="DivideByZeroException">
        /// Throws an exception when you are trying to divide by zero
        /// </exception>
        public static double Div(double a, double b) {

            if (b == 0)
                throw new DivideByZeroException();

            else
                return a / b;

        }

        /// <summary>
        /// Modulo of two numbers
        /// </summary>
        /// <param name="a">First number (dividend)</param>
        /// <param name="b">Second number (divisor)</param>
        /// <returns>The rest after division</returns>
        /// <exception cref="Exception">Throws an exception when you are trying to divide by zero</exception>
        public static double Mod(double a, double b) {

            if (b == 0) { 
                throw new Exception();
            }

            else { 
                return a % b;
            }

        }

        /// <summary>
        /// Factorial function for a number
        /// </summary>
        /// <param name="a">A whole number</param>
        /// <returns>Factorial of a given number</returns>
        /// <exception cref="Exception">Throws an exception when number is not whole</exception>
        public static double Factorial(int a) {

            if (a < 0 || a % 1 != 0) {
                throw new Exception();
            }

            if (a == 0) {
                return 1;
            }

            else { 
                double result = a;

                for(int i = a - 1; i > 0; i--) { 
                    result *= i;
                }

                return result;
            }

        }

        /// <summary>
        /// Power function for a number
        /// </summary>
        /// <param name="a">A number to be multiplied</param>
        /// <param name="exponent">Exponent that shows how many times should be the first number multiplied</param>
        /// <returns>Number to the power of exponent</returns>
        /// <exception cref="DivideByZeroException">Throws an exception when number or exponent is zero</exception>
        /// <exception cref="Exception">Throws exception when exponent is not whole number</exception>
        public static double Power(double a, long exponent) {

            if (a == 0 && exponent == 0) {
                throw new DivideByZeroException();
            }

            if (exponent == 0) { 
                return 1.0;
            }

            
            
            double power = Math.Abs(a); 
            for(int i = 1; i < Math.Abs(exponent); i++) {
                a *= power;
            }

            //decimal x = Convert.ToDecimal(a);
            if (exponent <= -1) {
                //x = 1 / Convert.ToDecimal(a);
                a = 1 / a;
            }
            //x = Math.Round(x, ROUNDING_PRECISION);
            //return Convert.ToDouble(x);
            return Math.Round(a, ROUNDING_PRECISION);

        }

        /// <summary>
        /// Root function of any positive real number
        /// </summary>
        /// <param name="a">Radicant</param>
        /// <param name="exponent">Root</param>
        /// <returns>Root of any number for given root</returns>
        /// <exception cref="Exception">Throws an exception when number is below zero or exponent is zero</exception>
        public static double Root(double a, double exponent) {

            if (a < 0 || exponent == 0) {
                throw new Exception();
            }

            double result = Math.Pow(a, 1 / exponent);
            return result;

        }

        /// <summary>
        /// Natural logarithm function
        /// </summary>
        /// <param name="a">A non zero number</param>
        /// <returns>Exponent that is needed to make a certain number</returns>
        /// <exception cref="Exception">Throws an exception when number is below or equal to zero</exception>
        public static double Log(double a) {

            if (double.IsNegativeInfinity(a) == true) {
                return double.NegativeInfinity;
            }

            if (double.IsPositiveInfinity(a) == true) {
                return double.PositiveInfinity;
            }

            if (a <= 0) { 
                throw new Exception();
            }

            double result = Math.Log(a);
            return result;
        }
    }

}