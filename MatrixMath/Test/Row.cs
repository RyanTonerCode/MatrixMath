using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixMath
{
    public class Row
    {
        public int Columns { get; private set; }

        private readonly double[] Vector;

        public Row(int Columns)
        {
            this.Columns = Columns;
        }

        public Row(int Columns, double[] Values)
        {
            this.Columns = Columns;
            Vector = new double[Columns];
            Values.CopyTo(Vector, 0);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < Vector.Length; i++)
            {
                if (i != Vector.Length - 1)
                    sb.Append(Vector[i]).Append(", ");
                else
                    sb.Append(Vector[i]).Append("\n");
            }
            return sb.ToString();
        }

        //return value in row at column index j
        public double GetValue(int J)
        {
            if(J <= Columns)
                return Vector[J - 1];
            throw new Exception();
        }

        //row operations:

        //Multiply the matrix by a value
        public void Multiply(double Value)
        {
            for(int i = 0; i < Vector.Length; i++)
                if(Vector[i] != 0)
                    Vector[i] *= Value;
        }

        //Adds a linear multiple of another row
        public void Add(Row R, double Multiple = 0)
        {
            if (Multiple == 0)
                Multiple = 1;

            for (int i = 0; i < Vector.Length; i++)
                Vector[i] += R.Vector[i] * Multiple;
        }

        public AugmentedRow GetAugmentedRow(int AugColumns)
        {
            return new AugmentedRow(Columns, AugColumns, Vector);
        }


    }
}
