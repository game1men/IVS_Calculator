namespace MathLib {
    public static class CalcMathLib {
        public static double Add(double a, double b) {

            //The add function
            return a + b;

        }
        public static double Sub(double a, double b) {

            //The substraction function
            return a - b;

        }
        public static double Mul(double a, double b) {

            //The multiplication function
            return a * b;

        }
        public static double Div(double a, double b) {

            if (a == 0 || b == 0) {

                throw new DivideByZeroException("Division by zero is not possible.");
            }

            //The dividing function
            return a / b;

        }
        public static double Mod(double a, double b) {
            //The modulo function
            if (b == 0) {
                throw new Exception("Modulo by 0 is not possible.");
            }
            return a % b;
        }
        public static double Factorial(double a) {

            //The factorial function
            if (a < 0) {
                //return double.NaN;
                throw new Exception("Factorial of negative number is not possible.");
            }
            if (a % 1 != 0) {
                //return double.NaN;
                throw new Exception("Factorial of decimal number is not possible.");
            }
            if (a == 1 || a == 0) {
                return 1;

            } else {
                return a * Factorial(a - 1);

            }


        }
        public static double Power(double a, double exponent) {

            //The power function
            if (exponent < 0) {
                throw new Exception("Number to the power of a negative number is not possible.");
            }
            if (exponent % 1 != 0) {
                throw new Exception("Number to the power of a decimal number is not possible.");

            }
            return Math.Pow(a, exponent);


        }
        public static double Root(double a, double exponent) {

            //The square root function
            if (a < 0) {
                throw new Exception("Root of a negative number is not possible.");
            }
            return Math.Pow(a, 1.0 / exponent);

        }
        public static double Log(double a) {

            //The logarithm function
            if (a < 0) {
                throw new Exception("Logarithm of a negative number is not possible.");
            }
            if (a == 0) {
                throw new Exception("Logarithm of zero is not possible.");
            }
            return Math.Log(a);

        }
    }

}