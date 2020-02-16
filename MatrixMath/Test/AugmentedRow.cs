using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixMath
{
    public class AugmentedRow : Row
    {
        public int AugmentedColumns { get; private set; }

        public AugmentedRow(int Columns, int AugColumns) : base(Columns + AugColumns)
        {
            AugmentedColumns = AugColumns;
        }

        //Total Columns = Regular Columns + AugColumns
        public AugmentedRow(int Columns, int AugColumns, double[] Values) : base(Columns + AugColumns, Values)
        {
            AugmentedColumns = AugColumns;
        }

    }
}
