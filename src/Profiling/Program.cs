using MathLib;

namespace Profiling {
    internal class Program {


        static void Main(string[] args) {

            StandardDeviation();
        }

        /// <summary>
        /// Calculates standard deviation
        /// </summary>
        public static void StandardDeviation() {
            char[] delimiterChars = { ' ', ',', '\n', '\t' };
            double average = 0;
            int numberCount = 0;
            double sum = 0;
            string? temp;//stores line from console
            while ((temp = Console.ReadLine()) != null)//reads input line by line
            {
                string[] line = temp.Split(delimiterChars);//split by delimiters
                if (line.Contains("\u0004"))//for support ctrl+d EOF in cmd
                    break;

                foreach (string x in line)//loads characters splitted in array
                {
                    if (String.IsNullOrWhiteSpace(x))//ignore these characters
                        continue;

                    double number;//temporary var for TryParse output
                    if (!double.TryParse(x, out number))//write error when invalid input
                    {
                        Console.WriteLine("Invalid input: " + x);
                        return;
                    }
                    average = CalcMathLib.Add(average, number);
                    sum = CalcMathLib.Add(sum, CalcMathLib.Power(number, 2));
                    numberCount++;
                }
            }
            average = CalcMathLib.Div(average, numberCount);//calculates average
            Console.WriteLine(CalcMathLib.Root(CalcMathLib.Mul((1.0 / (numberCount - 1)), (CalcMathLib.Sub(sum, CalcMathLib.Mul(numberCount, CalcMathLib.Power(average, 2))))), 2));//calculates standard deviation
        }
    }
}