using Spreadsheet.Core.Utils;
using System;
using System.IO;

namespace Spreadsheet.Core
{
    public class SheetWriter : IDisposable
    {
        private readonly TextWriter streamWriter;

        public SheetWriter(TextWriter streamWriter)
        {
            this.streamWriter = streamWriter;
        }

        public SheetWriter(Stream stream) : this(new StreamWriter(new BufferedStream(stream)))
        {
        }

        public void WriteSpreedsheat(SheetProcessingResult result)
        {
            var index = 1;
            foreach(var value in result.Values)
            {
                var exception = value as Exception;
                if (exception != null)
                {
                    streamWriter.Write(TextVariables.ErrorStartMarker);
                    var spreadsheetException = exception as SheetException;
                    streamWriter.Write(
                        spreadsheetException == null
                        ? exception.Message
                        : spreadsheetException.MessageWithCellCallStack);
                }
                else
                {
                    streamWriter.Write(value);
                }
                streamWriter.Write(TextVariables.CellSeparator);
                if (index++ % result.ColumnCount == 0)
                {
                    streamWriter.WriteLine();
                }
            }
            streamWriter.Flush();
        }

        public void Dispose()
        {
            streamWriter?.Dispose();
        }
    }
}
