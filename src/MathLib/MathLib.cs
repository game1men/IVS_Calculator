/**
 * @brief Math Library for IVS project (Calculator)
 * 
 * This library contains basic mathematic functions 
 * such as sum, difference, multiplication, division and 
 * more complex mathemati functions such as factorial,
 * power, root and basic logarithm.
 * 
 * @author Matej Smida
 * 
 */

namespace MathLib {
    public static class CalcMathLib {

        /**
         * @brief Sum of two numbers
         * 
         * @param a first number to be added
         * @param b second number to be added
         * 
         * @return Sum of numbers a and b
         */
        public static double Add(double a, double b) {

            return a + b;

        }

        /**
         * @brief Subtraction of two numbers
         *
         * @param a first number
         * @param b second number
         *
         * @return Difference of numbers a and b
         */
        public static double Sub(double a, double b) {

            return a - b;

        }

        /**
         * @brief Multiplication of two numbers 
         * 
         * @param a first number to be multiplied
         * @param b second number to be multiplied
         * 
         * @return Product of two numbers
         */
        public static double Mul(double a, double b) {

            return a * b;

        }

        /**
         * @brief Division of two numbers
         * 
         * @param a first number (dividend)
         * @param b second number (divisor)
         * 
         * @return Quotient of two numbers
         */
        public static double Div(double a, double b) {

            if (b == 0)
                throw new DivideByZeroException();

            else
                return a / b;

        }

        /**
         * @brief Modulo of two numbers
         * 
         * @param a first number (dividend)
         * @param b second number (divisor)
         * 
         * @return The rest after division
         */
        public static double Mod(double a, double b) {

            if (b == 0) { 
                throw new Exception();
            }

            else { 
                return a % b;
            }

        }


        /**
         * @brief Factorial functions for a number
         * 
         * @param a number
         * 
         * @return Factorial of a given number
         */
        public static double Factorial(double a) {

            if (a < 0 || a % 1 != 0) {
                throw new Exception();
            }

            else { 
                for(double i = a - 1; i > 0; i--) { 
                    a *= i;
                }

                return a;
            }

        }

        /**
         * @brief Power function for a number
         * 
         * @param a number to be multiplied
         * @param exponent shows how many times should be the first number multiplied
         * 
         * @return number to the power of exponent
         */
        public static double Power(double a, double exponent) {

            if (exponent == 0) {
                return 1.0;
            }

            else if (exponent % 1 != 0) {
                throw new Exception();
            }

            else {
                double power = a;
                for(int i = 1; i <exponent; i++) {
                    a *= power;
                }

                return a;
            }

        }

        /**
         * @brief
         * @param
         * @param
         * @return
         */
        public static double Root(double a, double exponent) {

            if(a < 0) {
                throw new Exception();
            }

            double result = Math.Pow(a, 1 / exponent);
            return result;

        }

        public static double Log(double a) {

            //The logarithm function
            return 1.0;
        }
    }

}