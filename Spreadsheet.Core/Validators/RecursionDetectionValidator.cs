using System;
using System.Collections.Generic;
using Spreadsheet.Core.Cells;
using Microsoft.CodeAnalysis.Collections;
using Spreadsheet.Core.Cells.Expressions;

namespace Spreadsheet.Core.Validators
{
    class RecursionDetectionValidator : ISheetValidator
    {
        public void Validate(Sheet spreadsheet, Cell cell)
        {
            var hashset = PooledHashSet<CellAddress>.GetInstance();
            try
            {
                CheckRecursion(spreadsheet, cell, hashset);
            }
            finally
            {
                hashset.Free();
            }
        }

        private void CheckRecursion(Sheet spreadsheet, Cell current, ISet<CellAddress> stack)
        {
            try
            {
                var dependencies = PooledHashSet<CellAddress>.GetInstance();
                try
                {
                    GetDependencies(current.Expression, dependencies);
                    if (dependencies.Overlaps(stack))
                    {
                        throw new CircularCellRefereceException(Resources.CircularReference);
                    }

                    stack.Add(current.Address);
                    foreach(var address in dependencies)
                    {
                        CheckRecursion(spreadsheet, spreadsheet[address], stack);
                    }
                    stack.Remove(current.Address);
                }
                finally
                {
                    dependencies.Free();
                }
            }
            catch(CircularCellRefereceException ex)
            {
                throw SheetException.AddCellAddressToErrorStack(ex, current.Address);
            }
        }

        private void GetDependencies(IExpression expression, ISet<CellAddress> addresses)
        {
            var binaryExpression = expression as BinaryExpression;
            if (binaryExpression != null)
            {
                GetDependencies(binaryExpression.Left, addresses);
                GetDependencies(binaryExpression.Right, addresses);
            }

            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                GetDependencies(unaryExpression.Value, addresses);
            }

            var referenceExpression = expression as CellRefereceExpression;
            if (referenceExpression != null)
            {
                addresses.Add(referenceExpression.Address);
            }
        }
    }
}
