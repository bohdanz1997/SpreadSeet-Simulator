﻿using Spreadsheet.Core.Cells.Expressions;

namespace Spreadsheet.Core.Parsers
{
    internal interface ISheetParser
    {
        IExpression ReadExpression();
    }
}
