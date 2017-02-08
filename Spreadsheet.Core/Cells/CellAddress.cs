using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core.Cells
{
    class CellAddress
    {
        public int Row { get; }
        public int Column { get; }

        public CellAddress(int row, int column)
        {
            Row = row;
            Column = column;
            //throws
        }

        public void Validate(int maxRow, int maxColumn)
        {
            if (Row >= maxRow)
            {

            }
            if (Column >= maxColumn)
            {

            }
            //throws
        }
    }
}
