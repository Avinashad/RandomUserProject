using System.Collections.Generic;

namespace RandomUserCore.Models.coreModels
{
    public class ReadOnlyListResult<T>
    {
        public int Total { get; protected set; }

        public int Skip { get; protected set; }

        public int Limit { get; protected set; }

        public IEnumerable<T> Items { get; protected set; }

        public ReadOnlyListResult(IEnumerable<T> items, int total, int skip, int limit)
        {
            Items = items;
            Total = total;
            Skip = skip;
            Limit = limit;
        }

    }

}