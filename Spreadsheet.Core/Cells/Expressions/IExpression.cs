using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core.Cells.Expressions
{
    interface IExpression
    {
        object Evaluate(SpreadsheetProcessor processor);
    }
}
