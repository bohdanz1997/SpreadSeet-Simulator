using Spreadsheet.Core.Cells.Expressions;
using System;

namespace Spreadsheet.Core.Cells
{
    public class Cell
    {
        public CellAddress Address { get; }
        
        internal IExpression Expression { get; }

        internal Cell(CellAddress address, IExpression expression)
        {
            Address = address;
            Expression = expression;
        }

        public object Evaluate(SheetProcessor processor)
        {
            try
            {
                return Expression.Evaluate(processor);
            }
            catch (Exception exception)
            {
                throw SheetException.AddCellAddressToErrorStack(new ExpressionEvaluationException(exception.Message, exception), Address);
            }
        }

        public override string ToString() => $"{Address}|{Expression}";
    }
}
