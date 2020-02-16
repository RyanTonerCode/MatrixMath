using System;

namespace MatrixMath
{
    class Program
    {
        static void Main(string[] args)
        {

            double[,] m1 = new double[,]{
                { 1, 2, 3, 10},
                { 2, 3, 4, 25 },
                { 3, 6, 5, 13 }
            };

            double[,] m2 = new double[,]{
                { 1, 2, 3, 10},
                { 2, 3, 4, 25 },
                { 3, 5, 7, 13 }
            };

            double[,] lol = new double[,]
            {
                { 1, 2, 3 },
                { 2, 3, 4 },
                { 3, 6, 5 }
            };

            var m = new Matrix(lol);

            m.Print();

            var aug = m.GetIdentityAugmented();

            aug.Print();

            aug.ReducedRowEchelonForm();

            m.GetInverseMatrix().Print();

            var npc = m * m;

            npc.Print();


            //Matrix2 m = new Matrix2(lol);

            //m.Print();

            //m.ReducedRowEchelonForm();


            Console.ReadLine();
        }


    }
}
