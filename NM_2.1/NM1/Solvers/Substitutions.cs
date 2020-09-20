using NM1;
using System;
using System.Collections.Generic;
using System.Text;

namespace NM1
{
    class Substitutions
    {
        //прямая подстановка по строкам (А - нижняя треугольная матрица)
        public static Vector DirectRowSubstitution(Matrix A, Vector F)
        {
            Vector RES = new Vector();
            //скопируем по значениям вектор F в RES
            RES.Copy(F);
            //проход по строкам
            for (int i = 0; i < F.N; i++)
            {
                if (Math.Abs(A.Elem[i][i]) < CONSTS.EPS) throw new Exception("Direct Row Substitution: division by 0...");
                for (int j = 0; j < i; j++) RES.Elem[i] -= A.Elem[i][j] * RES.Elem[j];
                RES.Elem[i] /= A.Elem[i][i];
            }
            return RES;
        }
        //прямая подстановка по столбцам (А - нижняя треугольная матрица)
        public static Vector DirectColumnSubstitution(Matrix A, Vector F)
        {
            Vector RES = new Vector();
            //скопируем вектор F в RES
            RES.Copy(F);
            //проход по столбцам
            for (int j = 0; j < F.N; j++)
            {
                if (Math.Abs(A.Elem[j][j])  < CONSTS.EPS) throw new Exception("Direct Column Substitution: division by 0...");
                RES.Elem[j] /= A.Elem[j][j];
                for (int i = j + 1; i < F.N; i++) RES.Elem[i] -= A.Elem[i][j] * RES.Elem[j];
            }
            return RES;
        }
        //обратная подстановка по строкам (А - верхняя треугольная матрица)
        public static Vector BackRowSubstitution(Matrix A, Vector F)
        {
            Vector RES = new Vector();
            //скопируем вектор F в RES
            RES.Copy(F);
            //начинаем с последней строки, двигаясь вверх
            for (int i = F.N - 1; i >= 0; i--)
            {
                if (Math.Abs(A.Elem[i][i]) < CONSTS.EPS) throw new Exception("Back Row Substitution: division by 0... ");
                //двигаемся по столбцам
                for (int j = i + 1; j < F.N; j++) RES.Elem[i] -= A.Elem[i][j] * RES.Elem[j];
                RES.Elem[i] /= A.Elem[i][i];
            }
            return RES;
        }
        //обратная подстановка по столбцам (А - верхняя треугольная матрица)
        public static Vector BackColumnSubstitution(Matrix A, Vector F)
        {
            Vector RES = new Vector();
            //скопируем вектор F в RES
            RES.Copy(F);
            //начинаем с последнего столбца, сдвигаясь влево
            for (int j = F.N - 1; j >= 0; j--)
            {
                if (Math.Abs(A.Elem[j][j]) < CONSTS.EPS) throw new Exception("Back Column Substitution: division by 0...");
                RES.Elem[j] /= A.Elem[j][j];
                //двигаемся по строкам
                for (int i = j - 1; i >= 0; i--) RES.Elem[i] -= A.Elem[i][j] * RES.Elem[j];
            }
            return RES;
        }

    }
}
