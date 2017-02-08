using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spreadsheet.Core.Parsers.Operators;

namespace Spreadsheet.Core.Parsers.Operators
{
    internal class OperatorManager
    {
        public IReadOnlyDictionary<char, IOperator> Operators { get; }

        public IReadOnlyList<int> Priorities { get; }

        private readonly static Lazy<OperatorManager> _default;

        public static OperatorManager Default => _default.Value;

        public OperatorManager(IList<IOperator> operators)
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

        static OperatorManager()
        {
            _default = new Lazy<OperatorManager>(
                () => new OperatorManager(new List<IOperator>
                {
                    new Operator<int>('+', 0, (l, r) => l + r, v => v),
                    new Operator<int>('-', 0, (l, r) => l - r, v => -v),
                    new Operator<int>('*', 1, (l, r) => l * r),
                    new Operator<int>('/', 1, (l, r) => l / r),
                    new Operator<int>('^', 2, (l, r) => (int) Math.Pow(l, r))
                }), LazyThreadSafetyMode.ExecutionAndPublication);
        }
    }
}
