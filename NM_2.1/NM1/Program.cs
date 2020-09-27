using System;

namespace NM1
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix(3, 3)
            {
                Elem = new double[][]{
                    new double[]{ -2.0, -2.0, -1.0 },
                    new double[]{ 1.0, 0.0, -2.0 },
                    new double[]{ 0.0, 1.0, 2.0 }
                }
            };
            Vector F = new Vector(3)
            {
                Elem = new double[] { -5.0, -1.0, 3.0 }
            };
            Vector RES = LU.LUMethod(A, F);
            RES.ConsoleWriteVector();
        }
    }
}
