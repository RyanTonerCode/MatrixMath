namespace MatrixMath
{
    class RowVector : Matrix
    {
        public RowVector(double[,] Input) : base(1, Input.GetLength(1))
        {
            for (int i = 0; i < Rows; i++)
                this[0,i] = Input[0,i];
        }

        public RowVector(double[] vector) : base(1, vector.Length)
        {
            for (int i = 0; i < Cols; i++)
                this[0, i] = vector[i];
        }

        public RowVector(int dimensions) : base(dimensions, 1)
        {
        }
    }
}
