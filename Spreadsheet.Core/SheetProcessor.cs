using System;
using Spreadsheet.Core.Cells;
using Spreadsheet.Core.ProcessingStrategies;
using Spreadsheet.Core.Validators;

namespace Spreadsheet.Core
{
    public class SheetProcessor
    {
        private readonly Sheet spreadsheet;
        private readonly ISheetValidator validator;

        public SheetProcessor(Sheet spreadsheet, ISheetValidator validator = null)
        {
            this.spreadsheet = spreadsheet;
            this.validator = validator ?? new RecursionDetectionValidator();
        }

        public SheetProcessingResult Evaluate()
        {
            return Evaluate(new SimpleProcessingStrategy());
        }

        public SheetProcessingResult Evaluate(IProcessingStrategy strategy)
        {
            return new SheetProcessingResult(spreadsheet.ColumnCount, strategy.Evaluate(spreadsheet, GetCellValue));
        }

        public object GetCellValue(CellAddress address)
        {
            return GetCellValue(spreadsheet[address]);
        }

        private object GetCellValue(Cell cell)
        {
            try
            {
                validator?.Validate(spreadsheet, cell);
                return cell.Evaluate(this);
            }
            catch (SheetException exception)
            {
                return exception;
            }
            catch (Exception exception)
            {
                return new ExpressionEvaluationException(exception.Message, exception);
            }
        }
    }
}
