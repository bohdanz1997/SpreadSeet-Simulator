using System.Collections.Generic;

namespace Spreadsheet.Core
{
    public class SheetProcessingResult
    {
        public int ColumnCount { get; }
        public IEnumerable<object> Values { get; }

        public SheetProcessingResult(int columnsCount, IEnumerable<object> values)
        {
            ColumnCount = columnsCount;
            Values = values;
        }
    }
}
