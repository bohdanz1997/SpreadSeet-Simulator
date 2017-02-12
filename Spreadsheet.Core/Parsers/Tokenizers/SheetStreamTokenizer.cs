using System;
using Spreadsheet.Core.Cells;
using Spreadsheet.Core.Parsers.Operators;
using Spreadsheet.Core.Parsers.Tokenizers.Readers;
using static Spreadsheet.Core.Utils.TextVariables;
using System.IO;

namespace Spreadsheet.Core.Parsers.Tokenizers
{
    class SheetStreamTokenizer : AbstractReaderWithPeekSupport<Token>, ISheetTokenizer
    {
        public OperatorsFactory OperatorManager { get; }
        
        private readonly StreamReaderWithPeekSupport charReader;

        public SheetStreamTokenizer(Stream stream,
            OperatorsFactory operatorManager = null) : this(new StreamReader(stream), operatorManager)
        {
        }

        public SheetStreamTokenizer(TextReader stream, OperatorsFactory operatorManager = null)
        {
            charReader = new StreamReaderWithPeekSupport(stream);
            OperatorManager = operatorManager ?? OperatorsFactory.Default;
        }

        protected override Token GetNextValue()
        {
            var peek = charReader.Peek();
            
            while (char.IsWhiteSpace(peek) && !IsSeparationCharacter(peek))
            {
                charReader.Read();
                peek = charReader.Peek();
            }
            
            if (peek == StreamEnd)
            {
                return new Token(TokenType.EndOfStream);
            }
            
            if (IsSeparationCharacter(peek) && peek != StreamEnd)
            {
                charReader.Read();
                if (peek == CarriageReturn && charReader.Peek() == RowSeparator)
                {
                    charReader.Read();
                }
                return new Token(TokenType.EndOfCell);
            }
            
            if (peek == StringStart)
            {
                charReader.Read();
                return new Token(charReader.ReadRemainExpression());
            }
            
            if (char.IsDigit(peek))
            {
                return new Token(charReader.ReadInteger());
            }
            
            if (IsColumnLetter(peek))
            {
                var column = charReader.ReadColumnNumber();
                var row = charReader.ReadInteger();
                return new Token(new CellAddress(row - 1, column - 1));
            }
            
            if (peek == ExpressionStart)
            {
                charReader.Read();
                return new Token(TokenType.ExpressionStart);
            }
            
            if (OperatorManager.Operators.ContainsKey(peek))
            {
                charReader.Read();
                return new Token(OperatorManager.Operators[peek]);
            }

            throw new ExpressionParsingException(charReader.ReadRemainExpression());
        }
    }
}
