using System.Linq;
using System.Text;
using Spreadsheet.Core.Cells;
using static Spreadsheet.Core.Utils.TextVariables;

namespace Spreadsheet.Core.Utils
{
    public static class CellAddressConverter
    {
        public static CellAddress FromString(string address)
        {
            var index = 0;
            var column = 0;
            while (index < address.Length && char.IsLetter(address[index]))
            {
                if ((uint)column > (int.MaxValue / LettersUsedForColumnNumber))
                {
                    throw new InvalidCellAdressException(Resources.IntegerToBig);
                }
                column = column * LettersUsedForColumnNumber + (char.ToUpper(address[index]) - ColumnStartLetter + 1);
                index++;
            }
            column = column - 1;

            var row = 0;
            while(index < address.Length && char.IsDigit(address[index]))
            {
                if ((uint)row > (int.MaxValue / 10))
                {
                    throw new InvalidCellAdressException(Resources.IntegerToBig);
                }
                row = row * 10 + (address[index] - '0');
                index++;
            }
            row = row - 1;

            if (index < address.Length)
            {
                throw new InvalidCellAdressException(string.Format(Resources.WrongCellAddress, address));
            }

            return new CellAddress(row, column);
        }

        public static string GetString(CellAddress address)
        {
            var index = address.Column + 1;
            var result = new StringBuilder();
            while(index / LettersUsedForColumnNumber > 1)
            {
                result.Append((char)(ColumnStartLetter + index % LettersUsedForColumnNumber - 1));
                index = index / LettersUsedForColumnNumber;
            }
            result.Append((char)(ColumnStartLetter + index - 1));
            var column = new string(result.ToString().Reverse().ToArray());

            return $"{column}{address.Row + 1}";
        }
    }
}
