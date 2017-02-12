using System;
using System.Collections.Generic;
using System.Linq;
using Spreadsheet.Core.Cells;
using Spreadsheet.Core.Cells.Expressions;
using Spreadsheet.Core.Parsers;
using Spreadsheet.Core.Parsers.Tokenizers;
using static Spreadsheet.Core.Utils.TextVariables;
using System.IO;

namespace Spreadsheet.Core
{
    public class SheetReader : IDisposable
    {
        private static readonly char[] SpreadsheetSizeSeparators =
        {
            WhiteSpace,
            CellSeparator,
            RowSeparator,
            CarriageReturn
        };

        private readonly TextReader textReader;
        private readonly Func<TextReader, ISheetParser> parserFactory;

        public SheetReader(Stream stream) : this(new StreamReader(stream))
        {
        }

        public SheetReader(TextReader textReader) : this(textReader, CreateParser)
        {
        }

        internal SheetReader(TextReader textReader, Func<TextReader, ISheetParser> parserFactory)
        {
            this.textReader = textReader;
            this.parserFactory = parserFactory;
        }

        public Sheet ReadSpreadsheet()
        {
            var line = textReader?.ReadLine();
            var size = line?.Split(SpreadsheetSizeSeparators, StringSplitOptions.RemoveEmptyEntries).ToArray();
            int maxRow;
            int maxColumn;
            if (size == null
                || size.Length != 2
                || !int.TryParse(size[0], out maxRow)
                || maxRow < 0
                || !int.TryParse(size[1], out maxColumn)
                || maxColumn < 0)
            {
                throw new SpreadsheatReadingException(string.Format(Resources.FailedToReadSpreadsheetSize, line));
            }

            return new Sheet(maxRow, maxColumn, GetCells(maxColumn, maxColumn * maxRow));
        }

        private IEnumerable<Cell> GetCells(int maxColumn, int cellCount)
        {
            var parser = parserFactory(textReader);
            for (var i = 0; i < cellCount; i++)
            {
                IExpression expression;
                try
                {
                    expression = parser.ReadExpression();
                }
                catch (SheetException exception)
                {
                    expression = new ConstantExpression(exception);
                }
                yield return new Cell(new CellAddress(i / maxColumn, i % maxColumn),
                    expression ?? new ConstantExpression(null));
            }
        }

        private static ISheetParser CreateParser(TextReader treader)
        {
            return new SheetStreamParser(new SheetStreamTokenizer(treader));
        }

        public void Dispose()
        {
            textReader?.Dispose();
        }
    }
}
