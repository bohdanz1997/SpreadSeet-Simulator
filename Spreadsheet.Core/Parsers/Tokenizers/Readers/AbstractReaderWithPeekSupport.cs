namespace Spreadsheet.Core.Parsers.Tokenizers.Readers
{
    internal abstract class AbstractReaderWithPeekSupport<T> where T : struct
    {
        private T? current;

        protected abstract T GetNextValue();

        public T Peek()
        {
            if (current.HasValue)
            {
                return current.Value;
            }
            var value = GetNextValue();
            current = value;
            return value;
        }

        public T Read()
        {
            if (!current.HasValue)
            {
                return GetNextValue();
            }
            var value = current.Value;
            current = null;
            return value;
        }
    }
}
