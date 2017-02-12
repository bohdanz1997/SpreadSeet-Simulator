using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core
{
    public class InvalidCellAdressException : SheetException
    {
        public InvalidCellAdressException(string message) : base(message)
        {
        }

        public InvalidCellAdressException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
