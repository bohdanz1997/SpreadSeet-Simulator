using System;
using Spreadsheet.Core.Cells.Expressions;
using Spreadsheet.Core.Parsers.Tokenizers;

namespace Spreadsheet.Core.Parsers
{
    class SheetStreamParser : ISheetParser
    {
        private readonly ISheetTokenizer tokenizer;

        public SheetStreamParser(ISheetTokenizer tokenizer)
        {
            this.tokenizer = tokenizer;
        }

        public IExpression ReadExpression()
        {
            if (Peek(TokenType.EndOfStream))
                return null;

            var result = ReadCellContent();
            if (Peek(TokenType.EndOfCell) || Peek(TokenType.EndOfStream))
            {
                Read();
                return result;
            }
            throw InvalidContent(Resources.WrongTokenType, Resources.EndOfExpression);
        }

        private Token Peek() => tokenizer.Peek();

        private bool Peek(TokenType type) => Peek().Type == type;

        private Token Read() => tokenizer.Read();

        private IExpression ReadCellContent()
        {
            switch (tokenizer.Peek().Type)
            {
                case TokenType.EndOfCell:
                    return ReadNothing();
                case TokenType.Integer:
                    return ReadInteger();
                case TokenType.String:
                    return ReadString();
                case TokenType.ExpressionStart:
                    return ReadComplexExpression();
                default:
                    throw InvalidContent(Resources.UnknownCellStart);
            }
        }

        private IExpression ReadNothing() => new ConstantExpression(null);

        private IExpression ReadInteger() => new ConstantExpression(tokenizer.Read().Integer);

        private IExpression ReadString() => new ConstantExpression(tokenizer.Read().String);

        private IExpression ReadCellReference() => new CellRefereceExpression(tokenizer.Read().Address);

        private IExpression ReadComplexExpression()
        {
            Read();
            return ReadOperation();
        }

        private IExpression ReadOperation(int priority = 0)
        {
            if (priority >= tokenizer.OperatorManager.Priorities.Count)
            {
                return ReadIdentifier();
            }
            var expression = ReadOperation(priority + 1);
            while (Peek(TokenType.Operator)
                && (Peek().Operator.Priority == tokenizer.OperatorManager.Priorities[priority]))
            {
                expression = new BinaryExpression(expression, Read().Operator, ReadOperation(priority + 1));
            }
            return expression;
        }

        private IExpression ReadIdentifier()
        {
            switch (Peek().Type)
            {
                case TokenType.Operator:
                    return new UnaryExpression(Read().Operator, ReadIdentifier());
                case TokenType.Integer:
                    return ReadInteger();
                case TokenType.CellReference:
                    return ReadCellReference();
                default:
                    throw InvalidContent(Resources.UnknownExpressionElement);
            }
        }

        private ExpressionParsingException InvalidContent(string message, object expected = null)
        {
            return new ExpressionParsingException(string.Format(message, tokenizer.Read(), expected));
        }
    }
}
