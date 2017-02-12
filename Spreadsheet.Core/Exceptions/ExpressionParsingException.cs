using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core
{
    public class ExpressionParsingException : SheetException
    {
        public ExpressionParsingException(string message) : base(message)
        {
        }

        public ExpressionParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
