namespace Spreadsheet.Core.Cells.Expressions
{
    interface IExpression
    {
        object Evaluate(SheetProcessor processor);
    }
}
