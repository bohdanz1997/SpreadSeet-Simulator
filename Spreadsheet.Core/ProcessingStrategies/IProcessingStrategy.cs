using Spreadsheet.Core.Cells;
using System;
using System.Collections.Generic;

namespace Spreadsheet.Core.ProcessingStrategies
{
    public interface IProcessingStrategy
    {
        IEnumerable<object> Evaluate(Sheet spreadsheet, Func<Cell, object> evaluation);
    }
}
