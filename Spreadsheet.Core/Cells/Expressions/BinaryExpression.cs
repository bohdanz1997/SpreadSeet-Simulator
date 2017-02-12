﻿using Spreadsheet.Core.Parsers.Operators;

namespace Spreadsheet.Core.Cells.Expressions
{
    internal class BinaryExpression : IExpression
    {
        public IExpression Left { get; }
        public IExpression Right { get; }
        public IOperator Operation { get; }

        public BinaryExpression(IExpression left, IOperator operation, IExpression right)
        {
            Left = left;
            Operation = operation;
            Right = right;
        }

        public object Evaluate(SheetProcessor processor)
        {
            return Operation.BinaryOperation(Left.Evaluate(processor), Right.Evaluate(processor));
        }
        public override string ToString() => $"({Left}{Operation}{Right})";
    }
}
