using Spreadsheet.Core.Cells.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core.Cells
{
    class Cell
    {
        public CellAddress Address { get; }
        
        internal IExpression Expression { get; }

        internal Cell(CellAddress address, IExpression expression)
        {
            Address = address;
            Expression = expression;
        }

        public object Evaluate(SpreadsheetProcessor processor)
        {
            try
            {
                return Expression.Evaluate(processor);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public override string ToString() => $"{Address}|{Expression}";
    }
}
