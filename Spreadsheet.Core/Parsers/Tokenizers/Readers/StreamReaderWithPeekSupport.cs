using System.IO;
using System.Text;
using Spreadsheet.Core.Utils;

namespace Spreadsheet.Core.Parsers.Tokenizers.Readers
{
    class StreamReaderWithPeekSupport : AbstractReaderWithPeekSupport<char>
    {
        private readonly StringBuilder stringBuilder;
        private readonly TextReader stream;

        public StreamReaderWithPeekSupport(TextReader stream)
        {
            this.stream = stream;
            stringBuilder = new StringBuilder();
        }

        protected override char GetNextValue()
        {
            var result = stream.Read();
            return result == -1
                ? TextVariables.StreamEnd
                : (char)result;
        }

        public string ReadRemainExpression()
        {
            stringBuilder.Clear();
            while (!TextVariables.IsSeparationCharacter(Peek()))
            {
                stringBuilder.Append(Read());
            }
            return stringBuilder.ToString();
        }

        public int ReadInteger()
        {
            var value = 0;
            while (char.IsDigit(Peek()))
            {
                if ((uint)value > (int.MaxValue / 10))
                {
                    throw new ExpressionParsingException(Resources.IntegerToBig);
                }
                value = value * 10 + (Read() - '0');
            }
            return value;
        }

        public int ReadColumnNumber()
        {
            var value = 0;
            while (TextVariables.IsColumnLetter(Peek()))
            {
                if ((uint)value > (int.MaxValue / TextVariables.LettersUsedForColumnNumber))
                {
                    throw new ExpressionParsingException(Resources.IntegerToBig);
                }
                value = value
                    * TextVariables.LettersUsedForColumnNumber
                    + (char.ToUpper(Read()) - TextVariables.ColumnStartLetter + 1);
            }
            return value;
        }
    }
}
