using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NM1
{
    class MatrixСonverting
    {
        public static void LeadingElementSelection(Matrix A)
        {
            for (int k = 0; k < A.N - 1; k++)
                for (int i = k + 1; i < A.N; i++)
                    if (Math.Abs(A.Elem[k][k]) < Math.Abs(A.Elem[i][k]))
                    {
                        double[] Buf = A.Elem[k];
                        A.Elem[k] = A.Elem[i];
                        A.Elem[i] = Buf;
                    }
        }

        public static void BringingMatrixToTopTriangle(Matrix A)
        {
            for (int k = 0; k < A.N - 1; k++)
                for (int i = k + 1; i < A.N; i++)
                {
                    if (Math.Abs(A.Elem[k][k]) < CONSTS.EPS) throw new Exception("The matrix is ​​degenerate");
                    double Scal = A.Elem[i][k];
                    A.Elem[i][k] = 0;
                    for (int j = k + 1; j < A.M; j++)
                        A.Elem[i][j] -= Scal * A.Elem[k][j] / A.Elem[k][k];
                }
        }
    }
}
