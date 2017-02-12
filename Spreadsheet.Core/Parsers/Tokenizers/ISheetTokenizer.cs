using Spreadsheet.Core.Parsers.Operators;

namespace Spreadsheet.Core.Parsers.Tokenizers
{
    internal interface ISheetTokenizer
    {
        Token Peek();
        Token Read();
        OperatorsFactory OperatorManager { get; }
    }
}
