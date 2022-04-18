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
        [InlineData(double.NaN, double.NaN, double.NaN)]
        [InlineData(double.NaN, 4, double.NaN)]
        [InlineData(45423, double.NaN, double.NaN)]
        [InlineData(double.NaN, 15, double.NaN)]
        public void AddNaNValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Add(x, y);
            Assert.Equal(expected, actual);

        }
        
        [Theory]
        [InlineData(double.PositiveInfinity, 3, double.PositiveInfinity)]
        [InlineData(145, double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(-23, double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.PositiveInfinity, double.NegativeInfinity, double.NaN)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity)]
        public void AddInfinityValsShouldCalculate(double x, double y, double expected) {

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
        [InlineData(double.NaN, double.NaN, double.NaN)]
        [InlineData(double.NaN, 4, double.NaN)]
        [InlineData(45423, double.NaN, double.NaN)]
        [InlineData(double.NaN, 15, double.NaN)]
        public void SubNaNValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Sub(x, y);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(double.PositiveInfinity, 3, double.PositiveInfinity)]
        [InlineData(145, double.PositiveInfinity, double.NegativeInfinity)]
        [InlineData(-23, double.PositiveInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity, double.NaN)]
        public void SubInfinityValsShouldCalculate(double x, double y, double expected) {

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
        [InlineData(double.NaN, double.NaN, double.NaN)]
        [InlineData(double.NaN, 4, double.NaN)]
        [InlineData(45423, double.NaN, double.NaN)]
        [InlineData(double.NaN, 15, double.NaN)]
        public void MulNaNValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mul(x, y);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(double.PositiveInfinity, 3, double.PositiveInfinity)]
        [InlineData(145, double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(-23, double.PositiveInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.NegativeInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity)]
        public void MulInfinityValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mul(x, y);
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
        [InlineData(double.MinValue, -2, double.MaxValue * 2)]
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
        [InlineData(double.PositiveInfinity, 3, double.PositiveInfinity)]
        [InlineData(145, double.PositiveInfinity, 0)]
        [InlineData(-23, double.PositiveInfinity, -0)]
        [InlineData(double.PositiveInfinity, double.NegativeInfinity, double.NaN)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity, double.NaN)]
        public void DivInfinityValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Div(x, y);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(double.NaN, double.NaN, double.NaN)]
        [InlineData(double.NaN, 4, double.NaN)]
        [InlineData(45423, double.NaN, double.NaN)]
        [InlineData(double.NaN, 15, double.NaN)]
        public void DivNaNValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Div(x, y);
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
        [InlineData(0.0,0.0)]
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
        [InlineData(double.MaxValue, -2, double.MinValue / 2)]
        public void DivNegativeAndPositiveValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Div(x, y);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(double.NaN, double.NaN, double.NaN)]
        [InlineData(double.NaN, 4, double.NaN)]
        [InlineData(45423, double.NaN, double.NaN)]
        [InlineData(double.NaN, 15, double.NaN)]
        public void ModNaNValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mod(x, y);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(double.PositiveInfinity, 3, double.NaN)]
        [InlineData(145, double.PositiveInfinity, 145)]
        [InlineData(-23, double.PositiveInfinity, -23)]
        [InlineData(double.PositiveInfinity, double.NegativeInfinity, double.NaN)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity, double.NaN)]
        public void ModInfinityValsShouldCalculate(double x, double y, double expected) {

            double actual = CalcMathLib.Mod(x, y);
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
        [InlineData(0.0,0.0)]
        [InlineData(12.0, 0.0)]
        [InlineData(-11.1, 0.0)]
        public void ModByZeroShouldNotCalculate(double x, double y) {
            Assert.Throws<Exception>(() => CalcMathLib.Mod(x, y));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(5, 120)]
        [InlineData(7, 5040)]
        [InlineData(8, 40320)]
        [InlineData(9, 362880)]
        [InlineData(10, 3628800)]
        [InlineData(11, 39916800)]
        [InlineData(12, 479001600)]
        public void FactorialPositiveValsShouldCalculate(int x, int expected) {

            double actual = CalcMathLib.Factorial(x);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(-7)]
        [InlineData(-8)]
        [InlineData(-9)]
        [InlineData(-10)]
        [InlineData(-11)]
        [InlineData(-12)]
        [InlineData(-14)]
        [InlineData(-18)]
        [InlineData(-20)]
        public void FactorialNegativeValsShouldNotCalculate(int x) {
            Assert.Throws<Exception>(() => CalcMathLib.Factorial(x));

        }

        [Theory]
        [InlineData(double.NaN, 45, double.NaN)]
        [InlineData(double.NaN, 4, double.NaN)]
        [InlineData(double.NaN, 15, double.NaN)]
        public void PowerNaNValsShouldCalculate(double x, int exponent, double expected) {

            double actual = CalcMathLib.Power(x, exponent);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(double.PositiveInfinity, 3, double.PositiveInfinity)]
        [InlineData(145, int.MaxValue, double.PositiveInfinity)]
        [InlineData(-23, int.MaxValue, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, int.MaxValue, double.PositiveInfinity)]
        public void PowerInfinityValsShouldCalculate(double x, int exponent, double expected) {

            double actual = CalcMathLib.Power(x, exponent);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(3.0, 2, 9.0)]
        [InlineData(4.0, 8, 65536.0)]
        [InlineData(2.0, 32, 4294967296.0)]
        [InlineData(1.0, 100, 1.0)]
        [InlineData(1.0, 0, 1.0)]
        [InlineData(15.0, 0, 1.0)]
        [InlineData(17.56, 0, 1.0)]
        [InlineData(50.0, 10, 97656250000000000.0)]
        public void PowerPositiveValsWithNaturalExponentsShouldCalculate(double x, int exponent, double expected) {

            double actual = CalcMathLib.Power(x, exponent);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(-3.0, 2, 9.0)]
        [InlineData(-4.0, 8, 65536.0)]
        [InlineData(-2.0, 32, 4294967296.0)]
        [InlineData(-1.0, 100, 1.0)]
        [InlineData(-50.0, 10, 97656250000000000.0)]
        [InlineData(-11.0, 3, -1331.0)]
        [InlineData(-2, 3, -8.0)]
        [InlineData(-2, 0, 1.0)]
        [InlineData(-27, 0, 1.0)]
        [InlineData(-13.5888, 0, 1.0)]
        [InlineData(-777.84, 2.0, 605035.0656000001)]
        public void PowerNegativeValsWithNaturalExponentsShouldCalculate(double x, int exponent, double expected) {

            double actual = CalcMathLib.Power(x, exponent);
            Assert.Equal(expected, actual);


        }

        [Fact]
        public void PowerWithZeroExponentShouldNotCalculate() {
            Assert.Throws<DivideByZeroException>(() => CalcMathLib.Power(0.0, 0));
        }
        [Theory]
        [InlineData(double.NaN, 45, double.NaN)]
        [InlineData(double.NaN, 4, double.NaN)]
        [InlineData(double.NaN, 15, double.NaN)]
        public void RootNaNValsShouldCalculate(double x, int exponent, double expected) {

            double actual = CalcMathLib.Add(x, exponent);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(double.PositiveInfinity, 3, double.PositiveInfinity)]
        [InlineData(145, int.MaxValue, 1.0000000023174722)]
        [InlineData(double.PositiveInfinity, int.MaxValue, double.PositiveInfinity)]
        public void RootInfinityValsShouldCalculate(double x, int exponent, double expected) {

            double actual = CalcMathLib.Root(x, exponent);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(9.0, 2.0, 3.0)]
        [InlineData(25.0, 2.0, 5.0)]
        [InlineData(4.0, 2.0, 2.0)]
        [InlineData(144.0, 2.0, 12.0)]
        [InlineData(255.0, 8.0, 1.9990217644839356)]
        public void RootPositiveValsShouldCalculate(double x, int exponent, double expected) {

            double actual = CalcMathLib.Root(x, exponent);
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(0.0,0.0)]
        [InlineData(10.0, 0.0)]
        [InlineData(-779.0, 2.0)]
        [InlineData(-474725.0, 2.0)]
        [InlineData(-5854.0, 2.0)]
        [InlineData(-668144.0, 2.0)]
        [InlineData(-5858255.0, 8.0)]
        public void RootNegativeOrZeroValsShouldNotCalculate(double x, int exponent) {
            Assert.Throws<Exception>(() => CalcMathLib.Root(x, exponent));
        }

        [Theory]
        [InlineData(4.0, 0)]
        [InlineData(11.0, 0)]
        [InlineData(25.0, 0)]
        [InlineData(78.0, 0)]
        [InlineData(963.0, 0)]
        public void RootPositiveValsWithZeroExponentShouldCalculate(double x, int exponent) {

            Assert.Throws<Exception>(() => CalcMathLib.Root(x, exponent));

        }

        [Theory]
        [InlineData(-4.0, 0)]
        [InlineData(-567.0, 0)]
        [InlineData(-111.0, 0)]
        [InlineData(-125.0, 0)]
        [InlineData(-78523.0, 0)]
        public void RootNegativeValsWithZeroExponentShouldCalculate(double x, int exponent) {

            Assert.Throws<Exception>(() => CalcMathLib.Root(x, exponent));

        }

        [Fact]
        public void NaturalLnNaNValShouldCalculate() {
            double actual = CalcMathLib.Log(double.NaN);
            Assert.Equal(double.NaN, actual);
        }
        [Theory]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        public void NaturalLNInfinityValShouldCalculate(double x, double expected) {
            double actual = CalcMathLib.Log(x);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(Math.E,1.0)]
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
