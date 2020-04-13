namespace MatrixMath
{
    class ColumnVector : Matrix
    {
        public ColumnVector(double[,] Input) : base(Input.GetLength(0), 1)
        {
            for (int i = 0; i < Rows; i++)
                this[i, 0] = Input[i, 0];
        }

        public ColumnVector(double[] vector) : base(vector.Length, 1)
        {
            for (int i = 0; i < Rows; i++)
                this[i, 0] = vector[i];
        }

        public ColumnVector(int dimensions) : base(dimensions, 1)
        {
        }
    }
}
