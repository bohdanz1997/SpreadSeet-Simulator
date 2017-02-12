namespace Spreadsheet.Core.Cells.Expressions
{
    internal class ConstantExpression : IExpression
    {
        internal object Value { get; }

        public ConstantExpression(object value)
        {
            Value = value;
        }

        public object Evaluate(SheetProcessor processor) => Value;
        
        public override string ToString() => Value?.ToString() ?? Resources.Nothing;
    }
}
