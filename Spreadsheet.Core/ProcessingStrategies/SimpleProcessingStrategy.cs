using System;
using System.Collections.Generic;
using System.Linq;
using Spreadsheet.Core.Cells;

namespace Spreadsheet.Core.ProcessingStrategies
{
    public class SimpleProcessingStrategy : IProcessingStrategy
    {
        public IEnumerable<object> Evaluate(Sheet spreadsheet, Func<Cell, object> evaluation)
        {
            return spreadsheet.Select(evaluation);
        }
    }
}
