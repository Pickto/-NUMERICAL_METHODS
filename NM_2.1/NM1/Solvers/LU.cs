using System;
using System.Collections.Generic;
using System.Text;

namespace NM1
{
    class LU
    {
        public static Vector LUMethod(Matrix A, Vector F)
        {
            Vector RES;
            Matrix LU = new Matrix(A.M, A.N);
            LU.Copy(A);
            MatrixСonverting.BringingMatrixToTopTriangle(LU);

            for (int i = 1; i < A.N; i++)
                for (int j = 0; j < i; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < j; k++)
                        sum += LU.Elem[i][k] * LU.Elem[k][j];
                    LU.Elem[i][j] = (A.Elem[i][j] - sum) / LU.Elem[j][j];
                }

            Vector d = new Vector(A.N);

            for (int i = 0; i < A.N; i++)
            {
                d.Elem[i] = LU.Elem[i][i];
                LU.Elem[i][i] = 1;
            }

            Vector Y = Substitutions.DirectRowSubstitution(LU, F);

            for (int i = 0; i < A.N; i++)
                LU.Elem[i][i] = d.Elem[i];

            RES = Substitutions.BackRowSubstitution(LU, Y);
            return RES;
        }
    }
}
