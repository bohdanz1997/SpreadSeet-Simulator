using Spreadsheet.Core.Cells;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spreadsheet.Core
{
    public class Sheet : IEnumerable<Cell>
    {
        private readonly Cell[,] content;

        public Sheet(int rowCount, int columnCount, IEnumerable<Cell> content)
        {
            this.content = new Cell[rowCount, columnCount];
            foreach (var cell in content)
            {
                this.content[cell.Address.Row, cell.Address.Column] = cell;
            }
        }

        public int RowCount => content.GetLength(0);
        public int ColumnCount => content.GetLength(1);

        public Cell this[CellAddress address]
        {
            get
            {
                address.Validate(RowCount, ColumnCount);
                return content[address.Row, address.Column];
            }
        }

        public IEnumerator<Cell> GetEnumerator() => content.Cast<Cell>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
