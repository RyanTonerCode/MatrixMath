using System;
using System.Text;

namespace MatrixMath
{
    class Matrix
    {
        /// <summary>
        /// This is a 0-indexed indexer for the underlying matrix.
        /// </summary>
        /// <param name="a">row index</param>
        /// <param name="b">col index</param>
        /// <returns></returns>
        public double this[int a, int b]
        {
            get => getValue(a, b);
            internal set => MatrixArray[a, b] = value;
        }

        /// <summary>
        /// Adds two matrices and will produce a matrix of the largest dimension possible.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix left, Matrix right)
        {
            int minRows = Math.Min(left.Rows, right.Rows);
            int minCols = Math.Min(left.Cols, right.Cols);

            Matrix m = new Matrix(minRows, minCols);

            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    m[i, j] = left[i, j] + right[i, j];

            return m;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            int minRows = Math.Min(left.Rows, right.Rows);
            int minCols = Math.Min(left.Cols, right.Cols);

            Matrix m = new Matrix(minRows, minCols);

            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    m[i, j] = left[i, j] - right[i, j];

            return m;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            int n = left.Rows;
            int m = right.Cols;
            int p = left.Cols;

            if (p != right.Rows)
                throw new Exception(
                    string.Format("Cannot multiply a {0}x{1} matrix by a {2}x{3} matrix", n, p, right.Rows, m)
                );

            Matrix mult = new Matrix(n, m);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < p; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m; k++)
                        sum += left[i, k] * right[k, j];
                    mult[i, j] = sum;
                }

            return mult;
        }

        public double[,] MatrixArray { get; private set; }

        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public bool IsSquare => Rows == Cols;

        //how many columns are augmented
        public int Augmented { get; private set; }

        public Matrix GetIdentityAugmented()
        {
            //double the number of columns to append the augmented matrix
            Matrix aug_matrix = Resize(Rows, Cols * 2);

            int identityStart = Cols;
            for (int i = 0; i < Cols; i++)
                aug_matrix[i, i + identityStart] = 1;

            return aug_matrix;
        }

        public Matrix GetInverseMatrix()
        {
            //To get the inverse matrix: rref [A|I] => [I|A^(-1)]

            Matrix aug = GetIdentityAugmented();

            //perform rref on the augmented identity matrix
            aug.ReducedRowEchelonForm();

            double[,] inverse = new double[Rows, Cols];

            //retrieve the inverse matrix from the augmented side of the matrix
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    inverse[i, j] = aug[i, j + Cols];

            return new Matrix(inverse);
        }

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Augmented = 0;
            MatrixArray = new double[rows, cols];
        }

        public Matrix(double[,] Input)
        {
            Rows = Input.GetLength(0);
            Cols = Input.GetLength(1);
            MatrixArray = Input;
            Augmented = 0;
        }

        /// <summary>
        /// Get the transpose A^T of the current matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix GetTranspose()
        {
            Matrix transpose = new Matrix(Cols, Rows);

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    transpose[j, i] = this[i, j];
            return transpose;
        }

        /// <summary>
        /// Basic function to resize the array and copy all possible elements.
        /// </summary>
        /// <param name="new_rows"></param>
        /// <param name="new_cols"></param>
        /// <returns></returns>
        public Matrix Resize(int new_rows, int new_cols)
        {
            double[,] resizeMatrix = new double[new_rows, new_cols];

            for (int i = 0; i < new_rows && i < Rows; i++)
                for (int j = 0; j < new_cols && j < Cols; j++)
                    resizeMatrix[i, j] = MatrixArray[i, j];

            return new Matrix(resizeMatrix);
        }

        //multiply a row by a multiple
        public void RowMultiplication(int row, double multiple)
        {
            for (int j = 0; j < Cols; j++)
                if (MatrixArray[row - 1, j] != 0)
                    MatrixArray[row - 1, j] *= multiple;
        }

        //runs a basic row swap on two rows
        public void RowSwap(int row1, int row2)
        {
            for (int j = 0; j < Cols; j++)
            {
                double _tmp = MatrixArray[row1 - 1, j];
                MatrixArray[row1 - 1, j] = MatrixArray[row2 - 1, j];
                MatrixArray[row2 - 1, j] = _tmp;
            }
        }

        public void RowLinearMultiple(int row1, int row2, double multiple = 1)
        {
            for (int i = 0; i < Cols; i++)
                MatrixArray[row1 - 1, i] += MatrixArray[row2 - 1, i] * multiple;
        }

        public void Print()
        {
            //go column by column to determine number of spaces.

            string[,] str = new string[Rows, Cols];

            int[] col_len = new int[Cols];

            for (int i = 0; i < Cols; i++)
                for (int j = 0; j < Rows; j++)
                {
                    str[j, i] = MatrixArray[j, i].ToString();

                    if (str[j, i].Length > col_len[i])
                        col_len[i] = str[j, i].Length;
                }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Rows; i++)
            {
                sb.Append("[");
                for (int j = 0; j < Cols; j++)
                {
                    int spaces = col_len[j] - str[i, j].Length;

                    for (int k = 0; k < spaces; k++)
                        sb.Append(" ");

                    sb.Append(str[i, j]);

                    if (j + 1 != Cols)
                        sb.Append(",");
                }
                sb.Append("]\n");
            }

            Console.WriteLine(sb);
        }

        public void RowEchelonForm()
        {
            //go column by column
            //process... pick pivot per column:::

            //meaningless for rows <= 1
            if (Rows <= 1)
                return;

            for (int j = 0; j < Cols; j++)
            {
                Console.WriteLine("COLUMN {0}", j + 1);
                ColumnMath(j);

                Console.WriteLine("==========");

                Print();

                Console.WriteLine("COLUMN FINISHED\n");

            }
            Console.WriteLine("Finished REF");
        }

        public void ReducedRowEchelonForm()
        {
            if (Rows <= 1)
                return;

            for (int j = 0; j < Cols; j++)
            {
                Console.WriteLine("COLUMN {0}", j + 1);
                ColumnMath(j, true);

                Console.WriteLine("==========");

                Print();

                Console.WriteLine("COLUMN FINISHED\n");

            }

            Console.WriteLine("Finished RREF");
        }

        /// <summary>
        /// 1-indexed external getter
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public double GetValue(int i, int j)
        {
            return MatrixArray[i - 1, j - 1];
        }

        /// <summary>
        /// 0-indexed internal getter
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        internal double getValue(int i, int j)
        {
            return MatrixArray[i, j];
        }

        //start indicates which row to start on
        private void ColumnMath(int solving_col, bool rref = false)
        {
            //pick either a 1 or the largest possible number in that column.
            int best_col_index = -1;
            double best_col_value = 0;

            //this method will retrieve the value of the column at the 0-based row index.
            double get_col_val(int row) => GetValue(row + 1, solving_col + 1);

            for (int i = solving_col; i < Rows; i++)
            {
                if (get_col_val(i) == 1)
                {
                    best_col_index = i;
                    best_col_value = 1;
                    break;
                }
                else if (get_col_val(i) != 0 && Math.Abs(get_col_val(i)) > Math.Abs(best_col_value))
                {
                    best_col_index = i;
                    best_col_value = get_col_val(i);
                }
            }

            //cannot do anything with column filled with 0s
            if (best_col_value == 0 || best_col_index == -1)
                return;

            if (best_col_value != 1)
            {
                double inverse = 1 / best_col_value;

                //need to make this row a 1.
                RowMultiplication(best_col_index + 1, inverse);

                Console.WriteLine("R{0} = R{0}({1})", best_col_index + 1, inverse);
            }

            //put this row at the top of the start row
            if (best_col_index != 0 && best_col_index != solving_col)
            {
                //exchange these two rows
                RowSwap(best_col_index + 1, solving_col + 1);

                Console.WriteLine("Swap R{0} with R{1}", solving_col + 1, best_col_index + 1);
            }


            int start_zeroing = solving_col + 1;
            if (rref)
                start_zeroing = 0;

            //make all other rows below zeroed out at this column
            for (int i = start_zeroing; i < Rows; i++)
            {
                if (solving_col == i)
                    continue;

                //nothing to do here if this value is already zero
                if (get_col_val(i) == 0)
                    continue;

                double val = -1 * get_col_val(i);

                //suppose value a is in the top (cur_value)
                //suppose value b is what needs to be zeroed.
                //then add b/a

                double inv = val / get_col_val(best_col_index);

                RowLinearMultiple(i + 1, solving_col + 1, inv);

                Console.WriteLine("R{0} = R{0} + R{1}({2})", i + 1, best_col_index + 1, inv);
            }

        }

    }
}

