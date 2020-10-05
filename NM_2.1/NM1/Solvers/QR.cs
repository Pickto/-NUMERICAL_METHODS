using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace NM1
{
    class QR
    {
        public enum Methods { Givens, Householder };

        public static Vector QRMethod(Methods method, Matrix A, Vector F, out Matrix Q)
        {
            switch (method)
            {
                case Methods.Givens:
                    Givens(A, out Q);
                    break;
                case Methods.Householder:
                    Householder(A, out Q);
                    break;
                default:
                    throw new Exception("Need choice one method: Givens, Householder");
            }
            F = Q.MultTransMatrixVector(F);
            
            Vector RES = Substitutions.BackRowSubstitution(A, F);
            return RES;
        }

        private static void Givens(Matrix A, out Matrix Q)
        {
            Q = new Matrix(A.M, A.N);

            double cos;
            double sin;
            
            double[] temp_x_i = new double[A.M];
            double[] temp_x_j = new double[A.M];

            double[] temp_q_i = new double[Q.M];
            double[] temp_q_j = new double[Q.M];

            for (int i = 0; i < Q.N; i++) 
                Q.Elem[i][i] = 1;

            for (int j = 0; j < A.M; j++)
            {
                for (int i = j + 1; i < A.N; i++)
                {
                    if (Math.Abs(A.Elem[i][j]) > CONSTS.EPS)
                    {
                        cos = A.Elem[j][j] / Math.Sqrt(Math.Pow(A.Elem[j][j], 2) + Math.Pow(A.Elem[i][j], 2));
                        sin = A.Elem[i][j] / Math.Sqrt(Math.Pow(A.Elem[j][j], 2) + Math.Pow(A.Elem[i][j], 2));

                        A.Elem[i].CopyTo(temp_x_i, 0);
                        A.Elem[j].CopyTo(temp_x_j, 0);
                        Q.Elem[i].CopyTo(temp_q_i, 0);
                        Q.Elem[j].CopyTo(temp_q_j, 0);

                        for (int k = 0; k < A.N; k++)
                        {
                            A.Elem[i][k] = cos * temp_x_i[k] - sin * temp_x_j[k];
                            A.Elem[j][k] = sin * temp_x_i[k] + cos * temp_x_j[k];
                            
                            Q.Elem[i][k] = cos * temp_q_i[k] - sin * temp_q_j[k];
                            Q.Elem[j][k] = sin * temp_q_i[k] + cos * temp_q_j[k];
                        }

                        A.Elem[i][j] = 0.0;
                    }
                }
            }

           Q = Q.TransposeMatrix();
        }


        private static void Householder(Matrix A, out Matrix Q)
        {
            Q = new Matrix(A.M, A.N);
            for (int i = 0; i < Q.N; i++)
                Q.Elem[i][i] = 1;

            Vector p = new Vector(A.M);

            double sum, beta, mu;

            for (int i = 0; i < A.M; i++)
            {
                sum = 0.0;
                for (int I = i; I < A.M; I++)
                    sum += Math.Pow(A.Elem[I][i], 2);

                if (Math.Abs(sum - A.Elem[i][i] * A.Elem[i][i]) > CONSTS.EPS)
                {
                    if (A.Elem[i][i] < 0)
                        beta = Math.Sqrt(sum);
                    else
                        beta = -Math.Sqrt(sum);

                    mu = 1.0 / beta / (beta - A.Elem[i][i]);
                    
                    for (int I = 0; I < A.M; I++)
                    {
                        p.Elem[I] = 0;
                        if (I >= i)
                            p.Elem[I] = A.Elem[I][i];
                    }
                    p.Elem[i] -= beta;

                    for (int m = i; m < A.N; m++)
                    {
                        sum = 0.0;
                        for (int n = i; n < A.M; n++)
                            sum += A.Elem[n][m] * p.Elem[n];
                        sum *= mu;
                        for (int n = i; n < A.M; n++)
                            A.Elem[n][m] -= sum * p.Elem[n];
                    }
                    for (int m = 0; m < A.M; m++)
                    {
                        sum = 0;
                        for (int n = i; n < A.M; n++)
                            sum += Q.Elem[m][n] * p.Elem[n];
                        sum *= mu;
                        for (int n = i; n < A.M; n++)
                            Q.Elem[m][n] -= sum * p.Elem[n];
                    }
                }
            }

        }
        
    }
}
