using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core
{
    public class SpreadsheatReadingException : SheetException
    {
        public SpreadsheatReadingException(string message) : base(message)
        {
        }

        public SpreadsheatReadingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
