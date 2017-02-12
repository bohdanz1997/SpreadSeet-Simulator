using Spreadsheet.Core.Cells;

namespace Spreadsheet.Core.Validators
{
    public interface ISheetValidator
    {
        void Validate(Sheet spreadsheet, Cell cell);
    }
}
