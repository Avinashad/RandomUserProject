using System.Collections.Generic;

namespace RandomUserCore.Models.coreModels
{
    public class ListResult<T>
    {
        public int Total { get; set; }

        public int Skip { get; set; }

        public int Limit { get; set; }

        public IEnumerable<T> Items { get; set; }

    }
   
}