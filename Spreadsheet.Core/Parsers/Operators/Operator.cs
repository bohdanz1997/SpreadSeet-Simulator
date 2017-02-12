using System;

namespace Spreadsheet.Core.Parsers.Operators
{
    class Operator<T> : IOperator
    {
        private readonly Func<T, T> unaryOperation;
        private readonly Func<T, T, T> binaryOperation;

        public bool IsBinaryOperationSupported => binaryOperation != null;

        public bool IsUnaryOperationSupported => unaryOperation != null;

        public char OperatorCharacter { get; }

        public int Priority { get; }

        public Operator(char operatorCharacter, int priority, Func<T, T, T> binaryOperation = null, Func<T, T> unaryOperation = null)
        {
            OperatorCharacter = operatorCharacter;
            Priority = priority;
            this.binaryOperation = binaryOperation;
            this.unaryOperation = unaryOperation;
        }

        public object BinaryOperation(object left, object right)
        {
            if (IsBinaryOperationSupported)
            {
                return binaryOperation(Cast(left), Cast(right));
            }
            throw new ExpressionEvaluationException(string.Format(Resources.BinaryOperationNotSupported, OperatorCharacter));
        }

        public object UnaryOperation(object value)
        {
            if (IsUnaryOperationSupported)
            {
                return unaryOperation(Cast(value));
            }
            throw new ExpressionEvaluationException(string.Format(Resources.UnaryOperationNotSupported, OperatorCharacter));
        }

        private T Cast(object value)
        {
            if (typeof(T) != value?.GetType())
            {
                throw new ExpressionEvaluationException(string.Format(Resources.WrongTypeError, typeof(T), value?.GetType()));
            }
            return (T)value;
        }

        public override string ToString() => OperatorCharacter.ToString();
    }
}
