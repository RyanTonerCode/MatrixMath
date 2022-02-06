using System;
using System.Text;

namespace MatrixMath
{
    class Matrix
    {
        public static Matrix GetIdentityMatrix(int dim)
        {
            Matrix I = new Matrix(dim, dim);
            for (int i = 0; i < dim; i++)
                I[i, i] = 1;
            return I;
        }

        /*
         * Linear Combinations/ Linear Dependence/ Independence
         * 
         * Subroutine that actually spits out x1, x2, x3 solutions to a system
         * ==> needed for eigenvector computation
         * 
         * Programming in augmented space somehow?
         * 
         * 
         * eigenvector/ eigenvalue
         * 
         * dimensionality
         * 
         * rank
         * 
         * kernel
         * 
         * 
         * 
         */

        public bool IsEigenvector(Matrix x)
        {
            if (IsSquare)
            {
                Matrix mult = this * x;
                double scalefactor = mult[0, 0] / x[0, 0];
                Matrix check_scale = scalefactor * x;
                return check_scale.Equals(mult);
            }
            throw new Exception();
        }

        public Matrix CalculateEigenvector(double eigenvalue)
        {
            /*
             * From Ax = lx,
             * We know (A - lI)x = 0
             * 
             * 
             * 
             */
            Matrix I = GetIdentityMatrix(Rows);
            Matrix calc = this - (eigenvalue * I);

            calc.ReducedRowEchelonForm();

            calc.Print();

            calc.PrintSolutions();

            return calc;
        }

        public void PrintSolutions(bool steps = true)
        {
            //a column with a leading-one is not a free variable (basic variable)
            bool[] basicVariables = new bool[Cols];
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Cols; j++)
                {
                    double val = this[i, j];
                    if (val == 1 && basicVariables[i] == false)
                        basicVariables[i] = true;
                }
            }

            char[] freeVariables = new char[Cols];

            int freeCount = 0;
            for(int i = Cols - 1; i >= 0; i--)
            {
                if(basicVariables[i] == false) //a free variable
                    freeVariables[i] = (char)('s' + Convert.ToChar(freeCount++)); //assign it a name
            }

            StringBuilder sb = new StringBuilder();

            //go through each row:
            for (int i = 0; i < Rows; i++)
            {
                if (freeVariables[i] != '\0') //a free variable
                {
                    sb.AppendFormat("x{0} = {1}", i + 1, freeVariables[i]);
                    sb.AppendLine();
                    continue;
                }
                sb.AppendFormat("x{0} = ", i + 1);
                bool settableToZero = true;
                for (int j = i + 1; j < Cols; j++) //iterate diagonally
                {
                    if(this[i,j] != 0)
                    {
                        sb.AppendFormat("{0}{1}", -this[i,j], freeVariables[j]);
                        settableToZero = false;
                    }
                }
                if (settableToZero)
                    sb.Append("0");
                sb.AppendLine();
            }

            if (steps)
                Console.WriteLine(sb);

        }


        /// <summary>
        /// This is a 0-indexed indexer for the underlying matrix.
        /// </summary>
        /// <param name="i">row index</param>
        /// <param name="j">col index</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get => MatrixArray[i, j];
            internal set => MatrixArray[i, j] = value;
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
            int m = left.Cols;
            int p = right.Cols;

            if (m != right.Rows)
                throw new Exception(
                    string.Format("Cannot multiply a {0}x{1} matrix by a {2}x{3} matrix", n, m, right.Rows, p)
                );

            Matrix mult = new Matrix(n,p);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < p; j++)
                    for (int k = 0; k < m; k++)
                        mult[i, j] += left[i, k] * right[k,j];

            return mult;
        }

        public static Matrix operator *(double left, Matrix right)
        {
            int n = right.Rows;
            int m = right.Cols;

            Matrix mult = new Matrix(n, m);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    mult[i, j] = left * right[i, j];

            return mult;
        }

        public override bool Equals(object obj)
        {
            if(obj is Matrix mat)
            {
                if (mat.Rows != Rows || mat.Cols != Cols)
                    return false;

                for (int i = 0; i < Rows; i++)
                    for (int j = 0; j < Cols; j++)
                        if (this[i, j] != mat[i, j])
                            return false;


                return true;
            }
            return base.Equals(obj);
        }

        public double[,] MatrixArray { get; private set; }

        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public bool IsSquare => Rows == Cols;

        public static Matrix GetMinor(Matrix mat, int row, int col)
        {
            double[,] minor = new double[mat.Rows - 1, mat.Cols - 1];
            int auxI = 0;
            for (int i = 0; i < mat.Rows; i++) {
                if (i == row)
                    continue;
                int auxj = 0;
                for (int j = 0; j < mat.Cols; j++) {
                    if (j == col)
                        continue;
                    minor[auxI, auxj] = mat[i, j];
                    auxj++;
                }
                auxI++;
            }
            return new Matrix(minor);
        }

        public static double Determinant(Matrix mat, int expansion_row = 0)
        {
            if (mat.Rows == 2 && mat.Cols == 2)
                return mat[0, 0] * mat[1, 1] - (mat[0, 1] * mat[1, 0]);
            else if(mat.IsSquare && mat.Rows > 2)
            {
                double result = 0;
                for(int i = 0; i < mat.Cols; i++)
                {
                    double cofactor = Math.Pow(-1, expansion_row + i) * mat[expansion_row,i];
                    double expansion = cofactor * Determinant(GetMinor(mat, expansion_row, i));
                    result += expansion;
                }

                return result;

            }
            throw new Exception("Invalid matrix dimensions!");
        }

        //how many columns are augmented
        public int Augmented { get; internal set; }

        public Matrix GetIdentityAugmented()
        {
            //double the number of columns to append the augmented matrix
            Matrix aug_matrix = Resize(Rows, Cols * 2);

            int identityStart = Cols;
            for (int i = 0; i < Cols; i++)
                aug_matrix[i, i + identityStart] = 1;

            aug_matrix.Augmented = Cols;

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

                    if (j + 1 == Augmented)
                        sb.Append("|");
                    else if (j + 1 != Cols)
                        sb.Append(",");
                }
                sb.Append("]\n");
            }

            Console.WriteLine(sb);
        }

        public void ReducedRowEchelonForm(bool print = false) => RowEchelonForm(true, print);

        public void RowEchelonForm(bool rref = false, bool print = false)
        {
            //Process: go column by column and make leading 1-s.

            StringBuilder output = new StringBuilder();

            for (int j = 0; j < Cols; j++)
            {
                output.Append(("COLUMN {0}", j + 1)).AppendLine();
                columnMath(j, rref, print);

                output.Append("=>").AppendLine();

                if(print)
                    Print();

                output.Append("COLUMN FINISHED\n").AppendLine();

            }
            if (rref)
                output.Append("Finished rref").AppendLine();
            else
                output.Append("Finished ref").AppendLine();

            if (print)
                Console.Write(output.ToString());
        }

        //start indicates which row to start on
        private void columnMath(int solving_col, bool rref = false, bool print = false)
        {
            StringBuilder output = new StringBuilder();

            //pick either a 1 or the largest possible number in that column.
            int best_col_index = -1;
            double best_col_value = 0;

            //this method will retrieve the value of the column at the 0-based row index.
            double get_col_val(int row) => this[row, solving_col];

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

                output.Append(("R{0} = R{0}({1})", best_col_index + 1, inverse)).AppendLine();
            }

            //put this row at the top of the start row
            if (best_col_index != 0 && best_col_index != solving_col)
            {
                //exchange these two rows
                RowSwap(best_col_index + 1, solving_col + 1);

                //need to update this value
                best_col_index = solving_col;

                output.Append(("Swap R{0} with R{1}", solving_col + 1, best_col_index + 1)).AppendLine();
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
                //then add, 0 = b + a*(-b/a)

                double inv = val / get_col_val(best_col_index);

                RowLinearMultiple(i + 1, solving_col + 1, inv);

                output.Append(("R{0} = R{0} + R{1}({2})", i + 1, best_col_index + 1, inv)).AppendLine();
            }

            if (print)
                Console.WriteLine(output.ToString());

        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}

