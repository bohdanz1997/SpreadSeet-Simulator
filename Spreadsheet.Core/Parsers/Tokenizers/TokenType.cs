using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core.Parsers.Tokenizers
{
    internal enum TokenType
    {
        ExpressionStart,
        CellReference,
        Integer,
        String,
        Operator,
        LeftParenthesis,
        RightParenthesis,
        EndOfCell,
        EndOfStream
    }
}
