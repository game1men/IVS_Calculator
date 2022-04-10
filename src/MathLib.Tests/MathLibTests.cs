/* *
 * 
 * 
 * Description: Tests for MathLib, part of IVS project 2022
 * Author: Josef Uncovsky
 * 
 * 
 * */
using System;
using Xunit;

namespace MathLib.Tests {
    public class MathLibTests {

        [Theory]
        [InlineData(2.0, 3.0, 5.0)]
        [InlineData(8.0, 11.0, 19.0)]
        [InlineData(10.0, 14.0, 24.0)]
        [InlineData(8000000, 8000000, 16000000)]
        [InlineData(double.MaxValue, double.MaxValue, double.MaxValue * 2)]
        public void AddPositiveValsShouldCalculate(double x, double y, double expected) {
            double actual = CalcMathLib.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-17.0, -43.0, -60.0)]
        [InlineData(-17.0, -44.0, -61.0)]
        [InlineData(-34.0, -87.0, -121.0)]
        [InlineData(-67.0, -546.0, -613.0)]
        [InlineData(-double.MaxValue, double.MaxValue, 0)]
        public void AddNegativeValsShouldCalculate(double x, double y, double expected) {
            double actual = CalcMathLib.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-17.0, 12.0, -5.0)]
        [InlineData(450.0, -650.0, -200.0)]
        [InlineData(50.0, -67.0, -17.0)]
        [InlineData(67.0, -546.0, -479.0)]
        [InlineData(double.MaxValue, -double.MaxValue, 0)]
        public void AddPositiveAndNegativeValsShouldCalculate(double x, double y, double expected) {
            double actual = CalcMathLib.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(17.0, 12.0, 5.0)]
        [InlineData(450.0, 650.0, -200.0)]
        [InlineData(50.0, 67.0, -17.0)]
        [InlineData(67.0, 546.0, -479.0)]
        [InlineData(double.MaxValue, 4, double.MaxValue - 4)]
        public void SubPositiveValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Sub(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(-17.0, -12.0, -5.0)]
        [InlineData(-450.0, -650.0, 200.0)]
        [InlineData(-50.0, -67.0, 17.0)]
        [InlineData(-67.0, -546.0, 479.0)]
        [InlineData(-double.MaxValue, -double.MaxValue, 0)]
        public void SubNegativeValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Sub(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(17.0, -12.0, 29.0)]
        [InlineData(1000.0, -650.0, 1650.0)]
        [InlineData(-50.0, 67.0, -117.0)]
        [InlineData(-67.0, 546.0, -613.0)]
        [InlineData(-double.MaxValue, -double.MaxValue, 0)]
        public void SubPositiveAndNegativeShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Sub(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(44.0, 10.0, 440.0)]
        [InlineData(1000.0, 7.0, 7000.0)]
        [InlineData(50.0, 56.0, 2800.0)]
        [InlineData(67.0, 4.0, 268.0)]
        [InlineData(double.MaxValue, 2, double.MaxValue * 2)]
        public void MulPositiveValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mul(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(-75.0, -1.0, 75.0)]
        [InlineData(-1000.0, -7.0, 7000.0)]
        [InlineData(-50.0, -56.0, 2800.0)]
        [InlineData(-67.0, -4.0, 268.0)]
        [InlineData(-double.MaxValue, -2, double.MaxValue * 2)]
        public void MulNegativeValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mul(x, y);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(-75.0, 10.0, -750.0)]
        [InlineData(1000.0, -7.0, -7000.0)]
        [InlineData(-50.0, 56.0, -2800.0)]
        [InlineData(67.0, -4.0, -268.0)]
        [InlineData(-double.MaxValue, 2, (-1 * double.MaxValue) * 2)]
        public void MulNegativeAndPositiveValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mul(x, y);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(75.0, 1.0, 75.0)]
        [InlineData(1000.0, 7.0, 142.85714285714286)]
        [InlineData(50.0, 5.0, 10.0)]
        [InlineData(850000.0, 4.0, 212500.0)]
        [InlineData(double.MaxValue, 2, double.MaxValue / 2)]
        public void DivPositiveValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Div(x, y);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(1.0, 0.0)]
        public void DivByzeroShouldNotCalculate(double x, double y) {

            Assert.Throws<DivideByZeroException>(() => CalcMathLib.Div(x, y));

        }

        [Theory]
        [InlineData(-50, -55.0, 0.9090909090909091)]
        [InlineData(-666, -111, 6.0)]
        [InlineData(-900, -11.5, 78.26086956521739)]
        [InlineData(-750, -3.0, 250.0)]
        [InlineData(-double.MaxValue, 2, -double.MaxValue / 2)]
        public void DivNegativeValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Div(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(50, -55.0, -0.9090909090909091)]
        [InlineData(666.0, -111.0, -6.0)]
        [InlineData(900.0, -11.5, -78.26086956521739)]
        [InlineData(750.0, -3.0, -250.0)]
        [InlineData(double.MaxValue, -2, -double.MaxValue / 2)]
        public void DivNegativeAndPositiveValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Div(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(75.0, 10.0, 5.0)]
        [InlineData(0.0, 12.0, 0.0)]
        [InlineData(895.0, 7.0, 6.0)]
        [InlineData(11145.47, 56.0, 1.4699999999993452)]
        [InlineData(250000.0, 4.0, 0.0)]
        public void ModPositiveValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mod(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(-75.0, -10.0, -5.0)]
        [InlineData(-1000.0, -7.0, -6.0)]
        [InlineData(-50.0, -87856.0, -50.0)]
        [InlineData(-67.0, -17474747.0, -67.0)]
        public void ModNegativeValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mod(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(-75.0, 10.0, -5.0)]
        [InlineData(1000.0, -7177.0, 1000.0)]
        [InlineData(-50.0, 787878.0, -50.0)]
        [InlineData(67.0, -447477.0, 67.0)]
        public void ModNegativeAndPositiveValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mod(x, y);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(12.0, 0.0)]
        [InlineData(-11.1, 0.0)]
        public void ModByZeroShouldNotCalculate(double x, double y) {
            Assert.Throws<Exception>(() => CalcMathLib.Mod(x, y));
        }

        [Theory]
        [InlineData(5.0, 120.0)]
        [InlineData(7.0, 5040.0)]
        [InlineData(8.0, 40320.0)]
        [InlineData(9.0, 362880.0)]
        [InlineData(10.0, 3628800.0)]
        [InlineData(11.0, 39916800.0)]
        [InlineData(12.0, 479001600.0)]
        [InlineData(14.0, 87178291200.0)]

        public void FactorialPositiveValsShouldCalculate(double x, double expected) {

            double actual = CalcMathLib.Factorial(x);
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(5.2)]
        [InlineData(7.789)]
        [InlineData(8.0000000004)]
        [InlineData(9.89972)]
        [InlineData(10.47521417)]
        [InlineData(11.174775857)]
        [InlineData(12.24522452452)]
        [InlineData(14.171172827)]
        public void FactorialDecimalValsShouldNotCalculate(double x) {

            Assert.Throws<Exception>(() => CalcMathLib.Factorial(x));
        }
        [Theory]
        [InlineData(-1.0)]
        [InlineData(-5.2)]
        [InlineData(-7.789)]
        [InlineData(-8.0000000004)]
        [InlineData(-9.89972)]
        [InlineData(-10.47521417)]
        [InlineData(-11.174775857)]
        [InlineData(-12.24522452452)]
        [InlineData(-14.171172827)]
        public void FactorialNegativeValsShouldNotCalculate(double x) {
            Assert.Throws<Exception>(() => CalcMathLib.Factorial(x));

        }
        [Theory]
        [InlineData(3.0, 2.0, 9.0)]
        [InlineData(4.0, 8.0, 65536.0)]
        [InlineData(2.0, 32.0, 4294967296.0)]
        [InlineData(1.0, 100.0, 1.0)]
        [InlineData(50.0, 10.0, 97656250000000000.0)]
        public void PowerPositiveValsWithNaturalExponentsShouldCalculate(double x, double exponent, double expected) {

            double actual = CalcMathLib.Power(x, exponent);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(-3.0, 2.0, 9.0)]
        [InlineData(-4.0, 8.0, 65536.0)]
        [InlineData(-2.0, 32.0, 4294967296.0)]
        [InlineData(-1.0, 100.0, 1.0)]
        [InlineData(-50.0, 10.0, 97656250000000000.0)]
        [InlineData(-11.0, 3.0, -1331.0)]
        [InlineData(-2, 3.0, -8.0)]
        [InlineData(-777.84, 2.0, 605035.0656000001)]
        public void PowerNegativeValsWithNaturalExponentsShouldCalculate(double x, double exponent, double expected) {

            double actual = CalcMathLib.Power(x, exponent);
            Assert.Equal(expected, actual);


        }
        [Theory]
        [InlineData(-3.0, 2.5)]
        [InlineData(-4.0, 8.78772)]
        [InlineData(-2.0, 32.74177)]
        [InlineData(-1.0, 100.74747)]
        [InlineData(-50.0, 10.457)]
        [InlineData(-11.0, 3.47)]
        [InlineData(-2, 3.1111111)]
        [InlineData(-777.84, 2.55858675)]
        public void PowerValsWithNonNaturalExponentsShouldNotCalculate(double x, double exponent) {

            Assert.Throws<Exception>(() => CalcMathLib.Power(x, exponent));

        }
        [Theory]
        [InlineData(9.0, 2.0, 3.0)]
        [InlineData(25.0, 2.0, 5.0)]
        [InlineData(4.0, 2.0, 2.0)]
        [InlineData(144.0, 2.0, 12.0)]
        [InlineData(255.0, 8.0, 1.9990217644839356)]
        public void RootPositiveValsShouldCalculate(double x, double exponent, double expected) {

            double actual = CalcMathLib.Root(x, exponent);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(-779.0, 2.0)]
        [InlineData(-474725.0, 2.0)]
        [InlineData(-5854.0, 2.0)]
        [InlineData(-668144.0, 2.0)]
        [InlineData(-5858255.0, 8.0)]
        public void RootNegativeValsShouldNotCalculate(double x, double exponent) {
            Assert.Throws<Exception>(() => CalcMathLib.Root(x, exponent));
        }
        [Theory]
        [InlineData(1.0, 0.0)]
        [InlineData(2.0, 0.6931471805599453)]
        [InlineData(3.0, 1.0986122886681098)]
        [InlineData(4.0, 1.3862943611198906)]
        [InlineData(5.0, 1.6094379124341003)]
        [InlineData(6.0, 1.7917594692280550)]
        [InlineData(7.0, 1.9459101490553132)]
        [InlineData(8.0, 2.0794415416798357)]
        [InlineData(9.0, 2.1972245773362196)]
        [InlineData(10.0, 2.302585092994046)]
        public void NaturalLnPositiveValsShouldCalculate(double x, double expected) {

            double actual = CalcMathLib.Log(x);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(1.1, 0.09531017980432493)]
        [InlineData(1.2, 0.1823215567939546)]
        [InlineData(1.3, 0.26236426446749106)]
        [InlineData(2.4, 0.8754687373538999)]
        [InlineData(double.MaxValue, 709.782712893384)]
        public void NaturalLnPositiveDecimalValsShouldCalculate(double x, double expected) {

            double actual = CalcMathLib.Log(x);
            Assert.Equal(expected, actual);

        }
        [Theory]
        [InlineData(-2.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(-0.0000040)]
        [InlineData(-17176.0)]
        [InlineData(-857.0)]
        [InlineData(-879.0)]
        [InlineData(-575719.0)]
        [InlineData(-7510.10)]
        public void NaturalLnNegativeOrZeroValsShouldNotCalculate(double x) {

            Assert.Throws<Exception>(() => CalcMathLib.Log(x));
        }
    }
}
