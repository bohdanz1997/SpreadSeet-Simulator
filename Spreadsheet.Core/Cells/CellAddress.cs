using Spreadsheet.Core.Utils;

namespace Spreadsheet.Core.Cells
{
    public class CellAddress
    {
        public int Row { get; }
        public int Column { get; }

        public CellAddress(int row, int column)
        {
            Row = row;
            if (Row < 0)
                throw new InvalidCellAdressException(Resources.NegativeCellRow);
            Column = column;
            if (Column < 0)
                throw new InvalidCellAdressException(Resources.NegativeCellColumn);
        }

        public void Validate(int maxRow, int maxColumn)
        {
            if (Row >= maxRow)
                throw new InvalidCellAdressException(Resources.WrongCellRow);
            if (Column >= maxColumn)
                throw new InvalidCellAdressException(Resources.WrongCellColumn);
        }

        public bool Equals(CellAddress other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is CellAddress && Equals((CellAddress)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 128) ^ Column;
            }
        }

        public override string ToString() => CellAddressConverter.GetString(this);
    }
}
