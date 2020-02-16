using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixMath
{
    public class Old_Matrix
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        //this matrix will be in row-form
        public List<Row> Row_matrix { get; private set; }


        public Old_Matrix(int Rows, int Columns)
        {
            this.Rows = Rows;
            this.Columns = Columns;
            Row_matrix = new List<Row>(Rows);
        }

        public Old_Matrix(double[,] Values)
        {
            Rows = Values.GetLength(0);
            Columns = Values.GetLength(1);

            Row_matrix = new List<Row>(Rows);

            for (int i = 0; i < Rows; i++)
            {
                double[] row_vals = new double[Columns];
                for(int j = 0; j < Columns; j++)
                {
                    row_vals[j] = Values[i, j];
                }
                Row r = new Row(Columns, row_vals);

                Row_matrix.Add(r);
            }
        }

        public void Print()
        {
            Row_matrix.ForEach(x => Console.Write(x.ToString()));
        }


        //retrieve value from the matrix using i,j notation (i is row, j is col)
        public double GetValue(int I, int J)
        {
            if (I <= Rows && J <= Columns)
                return Row_matrix[I].GetValue(J);
            throw new Exception(); //think about exceptions here
        }

        public Row GetRow(int I)
        {
            return Row_matrix[I];
        }

        public double[] GetCol(int J)
        {
            double[] Col = new double[Rows];
            for(int i = 0; i < Rows; i++)
            {
                Col[i] = Row_matrix[i].GetValue(J);
            }
            return Col;
        }

        public void RowEchelonForm()
        {
            //go column by column
            //process... pick pivot per column:::

            //meaningless for rows <= 1
            if (Rows <= 1)
                return;

            for(int j = 0; j < Columns; j++)
            {
                ColumnMath(GetCol(j + 1), j);

                Print();
                Console.WriteLine();
            }

        }

        //start indicates which row to start on
        private void ColumnMath(double[] Column, int start)
        {
            //pick either a 1 or largest absolute value number

            int largest_index = -1;
            double col_value = 0;



            for(int i = start; i < Column.Length; i++)
            {
                if(Column[i] == 1)
                {
                    largest_index = i;
                    col_value = 1;
                    break;
                }
                
                if(Column[i] != 0 && Math.Abs(Column[i]) > Math.Abs(col_value))
                {
                    largest_index = i;
                    col_value = Column[i];
                }
            }

            //cannot do anything with column filled with 0s
            if (col_value == 0 || largest_index == -1)
                return;

            if(col_value != 1)
            {
                double inverse = 1 / col_value;

                //need to make this row a 1.
                Row_matrix[largest_index].Multiply(inverse);

                Console.WriteLine("\nR{0} = R{0}({1})\n", largest_index + 1, inverse);
            }

            Row top = Row_matrix[largest_index];


            //put this row at the top of the start row
            if (largest_index != 0)
            {
                //exchange these two rows
                Row_matrix[largest_index] = Row_matrix[start];
                Row_matrix[start] = top;

                //do the same action to the column

                Column[largest_index] = Column[start];
                Column[start] = col_value; //value

                Console.WriteLine("\nSwap R{0} with R{1}\n", start + 1, largest_index + 1);
            }


            Console.WriteLine("here");
            Print();
            Console.WriteLine();


            //make all other rows after start zeroed out
            for(int i = start + 1; i < Rows; i++)
            {
                double val = -1 * Column[i];

                //suppose value a is in the top (cur_value)
                //suppose value b is what needs to be zeroed.
                //then add b/a

                double inv = val / col_value;

                Row_matrix[i].Add(top, inv);

                Console.WriteLine("R{0} = R{0} + R{1}({2})", i + 1, largest_index + 1, inv);
            }

            Console.WriteLine();
            
            //at this point all other values will be zeroed out in that column.

        }


    }
}
