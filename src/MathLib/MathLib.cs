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
         * @param b first number (divisor)
         * 
         * @return quotient of two numbers
         */
        public static double Div(double a, double b) {

            double result = a / b;
            //TODO division by zero Error
            return result;

        }

        /**
         * @brief
         * @param
         * @return 
         */
        public static double Factorial(double a) {

            //The factorial function
            return 1.0;


        }

        /**
         * @brief
         * @param
         * @param
         * @return
         */
        public static double Power(double a, double exponent) {

            double power = a;
            for(int i = 1; i <exponent; i++) {
                a *= power;
            }
            //TODO Power with Nonnatural exponent
            return a;


        }
        public static double Root(double a, double exponent) {

            //The square root function
            return 1.0;
            

        }
        public static double Log(double a) {

            //The logarithm function
            return 1.0;
        }
    }

}