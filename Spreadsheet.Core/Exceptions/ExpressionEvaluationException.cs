using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core
{
    public class ExpressionEvaluationException : SheetException
    {
        public ExpressionEvaluationException(string message) : base(message)
        {
        }

        public ExpressionEvaluationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
