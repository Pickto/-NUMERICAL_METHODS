using System;
using System.IO;

namespace NM1
{
    //интерфейс вектора
    public interface IVector
    {
        //размер
        int N { set; get; }
    }

    //класс вектор
    public class Vector : IVector
    {
        //размер вектора
        public int N { set; get; }
	//элементы вектора
        public double[] Elem { set; get; }

        //конструктор по умолчанию
        public Vector()
        {
        }

        //конструктор нуль-вектора по размеру n
        public Vector(int n)
        {
            N = n;
            Elem = new double[n];
        }


        //умножение на скаляр с выделением памяти под новый вектор
        public static Vector operator *(Vector T, double Scal)
        {
            Vector RES = new Vector(T.N);

            for (int i = 0; i < T.N; i++) RES.Elem[i] = T.Elem[i] * Scal;
            return RES;
        }

        //умножение на скаляр, результат записывается в тот же вектор
        public void DotScal (double Scal)
        {
            for (int i = 0; i < N; i++) Elem[i] = Elem[i] * Scal;
        }

        //скалярное произведение векторов
        public static double operator *(Vector V1, Vector V2)
        {
            if (V1.N != V2.N) throw new Exception("V1 * V2: dim(vector1) != dim(vector2)...");

            double RES = 0.0;

            for (int i = 0; i < V1.N; i++) RES += V1.Elem[i] * V2.Elem[i];
            return RES;
        }

        //сумма векторов с выделением памяти под новый вектор
        public static Vector operator +(Vector V1, Vector V2)
        {
            if (V1.N != V2.N) throw new Exception("V1 + V2: dim(vector1) != dim(vector2)...");
            Vector RES = new Vector(V1.N);

            for (int i = 0; i < V1.N; i++) RES.Elem[i] = V1.Elem[i] + V2.Elem[i];
            return RES;
        }

        //сумма векторов без выделения памяти под новый вектор
        public void Add (Vector V2)
        {
            if (N != V2.N) throw new Exception("V1 + V2: dim(vector1) != dim(vector2)...");

            for (int i = 0; i < N; i++) Elem[i] += V2.Elem[i];
        }

        //копирование вектора V2
        public void Copy (Vector V2)
        {
            if (N != V2.N) throw new Exception("Copy: dim(vector1) != dim(vector2)...");
            for (int i = 0; i < N; i++) Elem[i] = V2.Elem[i];
        }

        //норма вектора
        public double Norma()
        {
            double RES = 0.0;

            for (int i = 0; i < N; i++) RES += Elem[i] * Elem[i];
            RES = Math.Sqrt(RES);
            return RES;
        }

        //вывод вектора на консоль
        public void ConsoleWriteVector ()
        {
            for (int i = 0; i < N; i++) Console.WriteLine(Elem[i]);
        }
    }
}