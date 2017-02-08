using Spreadsheet.Core.Parsers.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core.Parsers.Tokenizers
{
    internal interface ISpreadsheetTokenizer
    {
        Token Peek();
        Token Read();
        OperatorManager OperatorManager { get; }
    }
}
