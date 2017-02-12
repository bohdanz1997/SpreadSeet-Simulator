using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Spreadsheet.Core.Parsers.Operators
{
    internal class OperatorsFactory
    {
        public IReadOnlyDictionary<char, IOperator> Operators { get; }

        public IReadOnlyList<int> Priorities { get; }

        private readonly static Lazy<OperatorsFactory> @default;

        public static OperatorsFactory Default => @default.Value;

        public OperatorsFactory(IList<IOperator> operators)
        {
            Operators = new ReadOnlyDictionary<char, IOperator>(
                operators
                .ToDictionary(o => o.OperatorCharacter, o => o));

            Priorities = operators
                .Select(o => o.Priority)
                .Distinct()
                .OrderBy(g => g)
                .ToList()
                .AsReadOnly();
        }

        static OperatorsFactory()
        {
            @default = new Lazy<OperatorsFactory>(
                () => new OperatorsFactory(new List<IOperator>
                {
                    new Operator<int>('+', 0, (l, r) => l + r, v => v),
                    new Operator<int>('-', 0, (l, r) => l - r, v => -v),
                    new Operator<int>('*', 1, (l, r) => l * r),
                    new Operator<int>('/', 1, (l, r) => l / r)
                }), LazyThreadSafetyMode.ExecutionAndPublication);
        }
    }
}
