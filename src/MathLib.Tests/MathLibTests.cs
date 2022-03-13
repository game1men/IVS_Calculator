/**
 * 
 * 
 * Description: Tests for MathLib, part of IVS project 2022
 * Author: Josef Uncovsky
 * 
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib;
using Xunit;

namespace MathLib.Tests
{
    public class MathLibTests
    {
        [Fact]
        public void AddPositiveValsShouldCalculate()
        {

            double expected_1 = 4.0, expected_2 = 6.0;
            double expected_3 = 8.0, expected_4 = 97485.916145;
            double actual_1 = CalcMathLib.Add(2.0, 2.0), actual_2 = CalcMathLib.Add(3.0, 3.0);
            double actual_3 = CalcMathLib.Add(4.0, 4.0), actual_4 = CalcMathLib.Add(89941.415, 7544.501145);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);
        }
        [Fact]
        public void AddNegativeValsShouldCalculate()
        {

            double expected_1 = -34.0, expected_2 = -87.0;
            double expected_3 = -665.0, expected_4 = -8500.0;

            double actual_1 = CalcMathLib.Add(-17.0, -17.0), actual_2 = CalcMathLib.Add(-43.0, -44.0);
            double actual_3 = CalcMathLib.Add(-332, -333), actual_4 = CalcMathLib.Add(-4250, -4250);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void AddPositiveAndNegativeValsShouldCalculate()
        {

            double expected_1 = -150.0, expected_2 = 17.0;
            double expected_3 = 45.150, expected_4 = -70.450;

            double actual_1 = CalcMathLib.Add(450.0, -600.0), actual_2 = CalcMathLib.Add(-50.0, 67.0);
            double actual_3 = CalcMathLib.Add(90.750, -45.6), actual_4 = CalcMathLib.Add(0.0, -70.450);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);
        }
        [Fact]
        public void SubPositiveValsShouldCalculate()
        {

            double expected_1 = -150.0, expected_2 = -17.0;
            double expected_3 = 45.150, expected_4 = -65.450;

            double actual_1 = CalcMathLib.Sub(450.0, 600.0), actual_2 = CalcMathLib.Sub(50.0, 67.0);
            double actual_3 = CalcMathLib.Sub(90.750, 45.6), actual_4 = CalcMathLib.Sub(5.0, 70.450);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void SubNegativeValsShouldCalculate()
        {

            double expected_1 = 150.0, expected_2 = 17.0;
            double expected_3 = -45.150, expected_4 = 65.450;

            double actual_1 = CalcMathLib.Sub(-450.0, -600.0), actual_2 = CalcMathLib.Sub(-50.0, -67.0);
            double actual_3 = CalcMathLib.Sub(-90.750, -45.6), actual_4 = CalcMathLib.Sub(-5.0, -70.450);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void SubPositiveAndNegativeShouldCalculate()
        {

            double expected_1 = -1050.0, expected_2 = -117.0;
            double expected_3 = -136.350, expected_4 = -75.450;

            double actual_1 = CalcMathLib.Sub(-450.0, 600.0), actual_2 = CalcMathLib.Sub(-50.0, 67.0);
            double actual_3 = CalcMathLib.Sub(-90.750, 45.6), actual_4 = CalcMathLib.Sub(-5.0, 70.450);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);
        }
        [Fact]
        public void MulPositiveValsShouldCalculate()
        {

            double expected_1 = 90, expected_2 = 110.0;
            double expected_3 = 130.50, expected_4 = 75;

            double actual_1 = CalcMathLib.Mul(9, 10), actual_2 = CalcMathLib.Mul(11.0, 10.0);
            double actual_3 = CalcMathLib.Mul(13.05, 10.00), actual_4 = CalcMathLib.Mul(15, 5);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);
        }
        [Fact]
        public void MulNegativeValsShouldCalculate()
        {

            double expected_1 = 99, expected_2 = 440.0;
            double expected_3 = 196.05, expected_4 = 75;

            double actual_1 = CalcMathLib.Mul(-9, -11), actual_2 = CalcMathLib.Mul(-11.0, -40.0);
            double actual_3 = CalcMathLib.Mul(-13.07, -15.00), actual_4 = CalcMathLib.Mul(-15, -5);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void MulNegativeAndPositiveValsShouldCalculate()
        {

            double expected_1 = -90, expected_2 = -110.0;
            double expected_3 = -130.50, expected_4 = -150;

            double actual_1 = CalcMathLib.Mul(-9, 10), actual_2 = CalcMathLib.Mul(-11.0, 10.0);
            double actual_3 = CalcMathLib.Mul(-13.05, 10.00), actual_4 = CalcMathLib.Mul(-15, 10);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void DivPositiveValsShouldCalculate()
        {

            double expected_1 = 0.9, expected_2 = 1.1;
            double expected_3 = 10.0, expected_4 = 1.5;


            double actual_1 = CalcMathLib.Div(9, 10), actual_2 = CalcMathLib.Div(11.0, 10.0);
            double actual_3 = CalcMathLib.Div(50, 5), actual_4 = CalcMathLib.Div(15, 10);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void DivByzeroShouldNotCalculate()
        {

            double divideByZeroCase = CalcMathLib.Div(1, 0);
            Assert.Equal(divideByZeroCase, double.NaN);

        }
        [Fact]
        public void DivNegativeValsShouldCalculate()
        {

            double expected_1 = 0.9090909090909091, expected_2 = 6;
            double expected_3 = 78.26086956521739, expected_4 = 250;


            double actual_1 = CalcMathLib.Div(-50, -55), actual_2 = CalcMathLib.Div(-666, -111);
            double actual_3 = CalcMathLib.Div(-900, -11.5), actual_4 = CalcMathLib.Div(-750, -3);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void DivNegativeAndPositiveValsShouldCalculate()
        {

            double expected_1 = -10, expected_2 = -15;
            double expected_3 = -78.26086956521739, expected_4 = 250;


            double actual_1 = CalcMathLib.Div(-50, 5), actual_2 = CalcMathLib.Div(45, -3);
            double actual_3 = CalcMathLib.Div(900, -11.5), actual_4 = CalcMathLib.Div(-750, -3);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void FactorialPositiveValsShouldCalculate()
        {

            double expected_1 = 120, expected_2 = 5040;
            double expected_3 = 40320, expected_4 = 362880;



            double actual_1 = CalcMathLib.Factorial(5);
            double actual_2 = CalcMathLib.Factorial(7);
            double actual_3 = CalcMathLib.Factorial(8);
            double actual_4 = CalcMathLib.Factorial(9);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);
        }
        [Fact]
        public void FactorialDecimalValsShouldNotCalculate()
        {

            double actual_1 = CalcMathLib.Factorial(1.2);
            double actual_2 = CalcMathLib.Factorial(2.44);
            double actual_3 = CalcMathLib.Factorial(0.0002);
            double actual_4 = CalcMathLib.Factorial(9.0000000002);

            Assert.Equal(actual_1, double.NaN);
            Assert.Equal(actual_2, double.NaN);
            Assert.Equal(actual_3, double.NaN);
            Assert.Equal(actual_4, double.NaN);
        }
        [Fact]
        public void FactorialNegativeValsShouldNotCalculate()
        {

            double actual_1 = CalcMathLib.Factorial(-15.7);
            double actual_2 = CalcMathLib.Factorial(-4.44);
            double actual_3 = CalcMathLib.Factorial(-470.02);
            double actual_4 = CalcMathLib.Factorial(-89.2);

            Assert.Equal(actual_1, double.NaN);
            Assert.Equal(actual_2, double.NaN);
            Assert.Equal(actual_3, double.NaN);
            Assert.Equal(actual_4, double.NaN);
        }
        [Fact]
        public void PowerPositiveValsWithNaturalExponentsShouldCalculate()
        {

            double expected_1 = 9, expected_2 = 4;
            double expected_3 = 3375, expected_4 = 14641;


            double actual_1 = CalcMathLib.Power(3, 2), actual_2 = CalcMathLib.Power(2, 2);
            double actual_3 = CalcMathLib.Power(15, 3), actual_4 = CalcMathLib.Power(11, 4);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);

        }
        [Fact]
        public void PowerNegativeValsWithNaturalExponentsShouldCalculate()
        {

            double expected_1 = 9, expected_2 = 4;
            double expected_3 = -3375, expected_4 = 14641;


            double actual_1 = CalcMathLib.Power(-3, 2), actual_2 = CalcMathLib.Power(-2, 2);
            double actual_3 = CalcMathLib.Power(-15, 3), actual_4 = CalcMathLib.Power(-11, 4);

            Assert.Equal(expected_1, actual_1);
            Assert.Equal(expected_2, actual_2);
            Assert.Equal(expected_3, actual_3);
            Assert.Equal(expected_4, actual_4);


        }
        [Fact]
        public void PowerValsWithNonNaturalExponentsShouldNotCalculate() {

            double actual_1 = CalcMathLib.Power(3, 2.5);
            double actual_2 = CalcMathLib.Power(3, -1);
            double actual_3 = CalcMathLib.Power(-470.02, -10);
            double actual_4 = CalcMathLib.Power(-89.2, -4.1);

            Assert.Equal(actual_1, double.NaN);
            Assert.Equal(actual_2, double.NaN);
            Assert.Equal(actual_3, double.NaN);
            Assert.Equal(actual_4, double.NaN);

        }
        [Fact]
        public void RootPositiveValsShouldCalculate() {

            double expected_1 = 3, expected_2 = 5;
            double expected_3 = 2, expected_4 = 6;

            double actual_1 = CalcMathLib.Root(9, 2), actual_2 = CalcMathLib.Root(25, 2);
            double actual_3 = CalcMathLib.Root(4, 2), actual_4 = CalcMathLib.Root(36, 2);

            Assert.Equal(actual_1, expected_1);
            Assert.Equal(actual_2, expected_2);
            Assert.Equal(actual_3, expected_3);
            Assert.Equal(actual_4, expected_4);

        }
        [Fact]
        public void RootNegativeValsShouldNotCalculate() {

            double actual_1 = CalcMathLib.Root(-15.7, 5);
            double actual_2 = CalcMathLib.Root(-4.44, 4);
            double actual_3 = CalcMathLib.Root(-470.02, 3);
            double actual_4 = CalcMathLib.Root(-89.2, 2);

            Assert.Equal(actual_1, double.NaN);
            Assert.Equal(actual_2, double.NaN);
            Assert.Equal(actual_3, double.NaN);
            Assert.Equal(actual_4, double.NaN);
        }
        [Fact]
        public void NaturalLnPositiveValsShouldCalculate() {

            double expected_1 = 0, expected_2 = 0.6931471805599453;
            double expected_3 = 1.0986122886681098, expected_4 = 1.3862943611198906;

            double actual_1 = CalcMathLib.Log(1), actual_2 = CalcMathLib.Log(2);
            double actual_3 = CalcMathLib.Log(3), actual_4 = CalcMathLib.Log(4);

            Assert.Equal(actual_1, expected_1);
            Assert.Equal(actual_2, expected_2);
            Assert.Equal(actual_3, expected_3);
            Assert.Equal(actual_4, expected_4);

        }
        [Fact]
        public void NaturalLnNegativeOrZeroValsShouldNotCalculate() {

            double actual_1 = CalcMathLib.Log(-1);
            double actual_2 = CalcMathLib.Log(0);
            double actual_3 = CalcMathLib.Log(-0.5);
            double actual_4 = CalcMathLib.Log(-2);

            Assert.Equal(actual_1, double.NaN);
            Assert.Equal(actual_2, double.NaN);
            Assert.Equal(actual_3, double.NaN);
            Assert.Equal(actual_4, double.NaN);

        }
    }
}
