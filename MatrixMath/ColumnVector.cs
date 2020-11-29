namespace MatrixMath
{
    class ColumnVector : Matrix
    {
        /// <summary>
        /// This is a 0-indexed indexer for the underlying matrix.
        /// </summary>
        /// <param name="i">row index</param>
        /// <param name="j">col index</param>
        /// <returns></returns>
        public double this[int i]
        {
            get => MatrixArray[i, 0];
            internal set => MatrixArray[i, 0] = value;
        }

        public ColumnVector(params double[] vector) : base(vector.Length, 1)
        {
            for (int i = 0; i < Rows; i++)
                this[i] = vector[i];
        }

        /// <summary>
        /// Dot Product Operation
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static double operator ^(ColumnVector left, ColumnVector right)
        {
            int leftRows = left.Rows;

            if (leftRows != right.Rows)
                throw new System.Exception();

            double sum = 0.0;
            for (int i = 0; i < leftRows; i++)
                sum += (left[i] * right[i]);

            return sum;
        }

        public ColumnVector(int dimensions) : base(dimensions, 1)
        {
        }
    }
}
