using System;

namespace NM1
{
    class Program
    {
        static void Main()
        {
            Matrix A = new Matrix(3, 3)
            {
                Elem = new double[][]{
                    new double[]{ 2.0, -9.0, 5.0 },
                    new double[]{ 1.2, -5.3999, 6.0 },
                    new double[]{ 1.0, -1.0, -7.5 }
                }
            };
            Vector F = new Vector(3)
            {
                Elem = new double[] { -4.0, 0.6001, -8.5 }
            };
            Vector X = LU.LUMethod(A, F);
        }
    }
}