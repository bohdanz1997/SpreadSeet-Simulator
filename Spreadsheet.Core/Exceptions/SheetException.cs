using Spreadsheet.Core.Cells;
using Spreadsheet.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Core
{
    public class SheetException : Exception
    {
        protected StringBuilder cellCallStack = new StringBuilder();

        protected SheetException(string message) : base(message) { }

        protected SheetException(string message, Exception innerException) : base(message, innerException) { }

        protected SheetException() { }

        public string MessageWithCellCallStack => $"{Message} {(InnerException as SheetException)?.cellCallStack}";
        
        public static T AddCellAddressToErrorStack<T>(T exception, CellAddress address) where T : SheetException
        {
            if (exception.cellCallStack.Length != 0)
            {
                var result = (T)Activator.CreateInstance(exception.GetType(), new object[] { exception.Message, exception });
                result.cellCallStack.Append(CellAddressConverter.GetString(address));
                result.cellCallStack.Append("<");
                result.cellCallStack.Append(exception.cellCallStack);
                return result;
            }
            else
            {
                exception.cellCallStack.Append(CellAddressConverter.GetString(address));
                return exception;
            }
        }
    }
}
