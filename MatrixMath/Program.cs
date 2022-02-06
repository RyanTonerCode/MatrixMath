using System;

namespace MatrixMath
{
    class Program
    {

        static void Main(string[] args)
        {

            /**
            Matrix A = new Matrix(new double[,]{
                { 8, -2},
                { -4, 1}
            });

            Matrix P = new Matrix(new double[,]{
                { -2, 1},
                { 1, 4}
            });

            Matrix pap = P.GetInverseMatrix() * A * P;

            pap.Print();
            */

            ColumnVector A = new ColumnVector(new double[] {
                Math.Sqrt(2)/ 2.0, 0, 0, Math.Sqrt(2)/ 2.0
            });

            ColumnVector B = new ColumnVector(new double[]{
                Math.Sqrt(6)/ -6.0, 0, Math.Sqrt(6)/3.0, Math.Sqrt(6)/6.0
            });

            ColumnVector C = new ColumnVector(new double[]{
                Math.Sqrt(3)/ -6.0, Math.Sqrt(3)/2.0, Math.Sqrt(3)/ -6.0, Math.Sqrt(3)/ 6.0
            });

            ColumnVector V = new ColumnVector(new double[]{
                1.0, 2.0, 2.0, 1.0
            });

            double d1 = V ^ A;
            double d2 = V ^ B;
            double d3 = V ^ C;


            Matrix Result = (d1 * A) + (d2 * B) + (d3 * C);


            Console.WriteLine(d1);
            Console.WriteLine(d2);
            Console.WriteLine(d3);

            Result.Print();



            Console.ReadLine();
        }

        private static void hwk14()
        {
            double[,] v1 = new double[,]{
                { 2, -2, 5},
                { 0, 3, -2},
                {0, -1, 2 }
            };

            Matrix a = new Matrix(v1);

            a.CalculateEigenvector(1).Print();
            a.CalculateEigenvector(2).Print();
            a.CalculateEigenvector(4).Print();

            Console.WriteLine("Problem 6");

            double[,] v2 = new double[,]{
                { 2, 1},
                { -2, 5}
            };

            Matrix b = new Matrix(v2);

            b.CalculateEigenvector(3).Print();
            b.CalculateEigenvector(4).Print();

            Console.WriteLine("Problem 7");

            double[,] v3 = new double[,]{
                { -4, 1 ,0},
                { 0, -4, 0},
                {0,0, -4 }
            };

            Matrix c = new Matrix(v3);

            c.CalculateEigenvector(-4).Print();

            Console.WriteLine("Problem 8");

            double[,] v4 = new double[,]{
                { 8, 0},
                { 0, -5}
            };

            Matrix d = new Matrix(v4);

            d.CalculateEigenvector(8).Print();
        }

        static void p0()
        {
            double[,] v1 = new double[,]{
                { -4,1},
                { 1, -1  },
            };

            double[,] v2 = new double[,]{
                { 1,3  },
                { 0,1 },
            };



            Matrix p = new Matrix(v1);
            Matrix a = new Matrix(v2);
            Matrix p1 = p.GetInverseMatrix();

            Matrix t = a * p;
            Matrix a1 = p1 * t;

            Console.WriteLine("result");

            a1.Print();
        }

        static void p1()
        {
            double[,] v1 = new double[,]{
                { -2, -1 },
                { 1, 1  },
            };

            double[,] v2 = new double[,]{
                { 2, -4  },
                { 1, 0 },
            };



            Matrix p = new Matrix(v1);
            Matrix a = new Matrix(v2);
            Matrix p1 = p.GetInverseMatrix();

            Matrix t = a * p;
            Matrix a1 = p1 * t;

            Console.WriteLine("result");

            a1.Print();
        }

        static void p2()
        {
            double[,] v1 = new double[,]{
                { 1, 1 },
                { -2, -1 },
            };

            double[,] v2 = new double[,]{
                { 1, 0 },
                { 0, 16},
            };


            double[,] v3 = new double[,]{
                { -1, -1},
                { 2, 1},
            };

            double[,] v4 = new double[,]{
                { 3, 1},
                {-2, 0},
            };

            Matrix a = new Matrix(v1);
            Matrix b = new Matrix(v2);
            Matrix c = new Matrix(v3);
            Matrix d = new Matrix(v4);

            Matrix f = a * (b * c);

            Matrix e = d * d * d * d;
            e.Print();

            f.Print();
        }

        static void p3()
        {
            double[,] v1 = new double[,]{
                { 1/2, -1, -1/2 },
                { -3/2, 2, 1/2},
                { 1/2, 1, 5/2}
            };

            double[,] v2 = new double[,]{
                {0, 0, 0 },
                {-1, 0, 0},
                {1, 0, 0}
            };
            Matrix a = new Matrix(v1);
            Matrix b = new Matrix(v2);

            Matrix c = a * b;

            c.Print();
        }

        static void p4()
        {
            double[,] v1 = new double[,]{
                { -1, -1},
                { 1,2}
            };

            double[,] v2 = new double[,]{
                {15 ,8 },
                {-20, -14}
            };

            Matrix p = new Matrix(v1);
            Matrix p1 = p.GetInverseMatrix();

            Matrix a = new Matrix(v2);

            Matrix f = p1 * (a * p);

            f.Print();
        }

        static void p7()
        {
            double[,] v1 = new double[,]{
                { -1, 2, 0},
                { 1, 1, 0},
                {0,0,1 }
            };

            double[,] v2 = new double[,]{
                { 1, 2, 0},
                { 1, 0, 0},
                {0,0,-1 }
            };

            Matrix p = new Matrix(v1);
            Matrix p1 = p.GetInverseMatrix();

            Matrix a = new Matrix(v2);

            Matrix f = p1 * (a * p);

            f.Print();
        }

        static void p5main()
        {
            double[,] v1 = new double[,]{
                {1/2, -1, -1/2},
                { -3/2, 2, 1/2},
                {1/2, 1, 5/2 }
            };

            double[,] v2 = new double[,]{
                {1, 0,1},
                { 0, 1, 1},
                {1,1,0 }
            };

            double[,] vb = new double[,]{
                {0,0,0},
                {-1,0,0},
                {1,0,0}
            };

            Matrix a = new Matrix(v1);

            Matrix p = new Matrix(v2);
            Matrix p1 = p.GetInverseMatrix();
            p1.Print();

            Matrix vbb = new Matrix(vb);

            Matrix vb1 = p1 * vbb;

            Console.WriteLine("b1");
            vb1.Print();

            Console.WriteLine("b2");
            Matrix avb = a * vb1;
            avb.Print();


            Matrix wtf = p * (a * p.GetInverseMatrix());
            Console.WriteLine("c1");
            p.Print();
            Console.WriteLine("c2");
            wtf.Print();

            Matrix d1 = p * avb;
            d1.Print();

            Matrix d2 = wtf * vbb;
            d2.Print();

            d2.GetInverseMatrix();
        }

        static void oldproblem()
        {
            double[,] v1 = new double[,]{
                { frac(1,2), -1, frac(-1,2)},
                { frac(-3,2),  2, frac(1,2)},
                { frac(1,2),  1,  frac(5,2)}
            };

            double[,] v2 = new double[,]{
                {1, 0, 1},
                {0, 1, 1},
                {1, 1, 0}
            };

            Matrix A = new Matrix(v1);

            Matrix B = new Matrix(v2);
            Matrix P = B.GetInverseMatrix();

            Console.WriteLine("HERE");

            Console.WriteLine("a");
            P.Print();

            double[,] vbprime = new double[,]{
                {0,0,0},
                {-1,0,0},
                {1,0,0}
            };

            Matrix vbp = new Matrix(vbprime);

            Matrix vb = P * vbp;

            Console.WriteLine("b1");
            vb.Print();

            Console.WriteLine("b2");
            Matrix tvb = A * vb;
            tvb.Print();

            //Matrix APrime = P.GetInverseMatrix() * (A * P);

            //p brings us from b to b'

            //A represents t: r3 -> r3 RELATIVE TO B

            Matrix Pinverse = B * (A * B.GetInverseMatrix());


            Console.WriteLine("c1");
            B.Print(); //this is p^-1


            Console.WriteLine("c2");
            Matrix aprime = (B * A) * P;
            aprime.Print();

            Matrix d1 = B * tvb;
            d1.Print();

            Matrix d2 = Pinverse * vbp;
            d2.Print();
        }

        static double frac(int a, int b) => a / (double)b;


        private static void crap()
        {
            //Matrix f = p1 * (a * p);
            //f.Print();


            //m1.GetInverseMatrix().Print();



            //m1.Print();


            /* todo
            double[,] v3 = new double[,]{
                { 1, -2, 1, 0  },
                { 0, 2, -8, 8 },
                {5, 0, -5, 10 }
            };
            */

            /*

            double[,] v4 = new double[,]{
                { 0, 3, -6, 6, 4, -5  },
                { 3, -7, 8, -5, 8, 9 },
                { 3, -9, 12, -9, 6, 15 }
            };

            double[,] v5 = new double[,]
            {
                { 0, 3, 5, 12 },
                { 4, 10, -5, 13 },
                { 6, 7, -2, 5 }
            };


            double[,] v6 = new double[,]
            {
                { 1,3 , -5, 0},
                { 1,4, -8, 0 },
                { -3, -7, 9, 0 }
            };


            Matrix m3 = new Matrix(v6);
            

            m3.Print();

            m3.ReducedRowEchelonForm();


            //m1.Print();
            //m2.Print();
            //m3.Print();

            /*
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


            double[,] a = new double[,]
            {
                {1,2},
                {3,4}
            };

            double[,] b = new double[,]
            {
                {2,0 },
                {0,2 }
            };

            Matrix res = new Matrix(a) * new Matrix(b);
            res.Print();
            */
            //aug.ReducedRowEchelonForm();

            //m.GetInverseMatrix().Print();

            //var npc = m * m;

            //npc.Print();

            //Matrix2 m = new Matrix2(lol);

            //m.Print();

            //m.ReducedRowEchelonForm();

        }


    }
}
