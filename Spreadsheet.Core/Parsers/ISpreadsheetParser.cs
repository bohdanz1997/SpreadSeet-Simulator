using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadsheet.Core.Cells.Expressions;

namespace Spreadsheet.Core.Parsers
{
    internal interface ISpreadsheetParser
    {
        IExpression ReadExpression();
    }
}
