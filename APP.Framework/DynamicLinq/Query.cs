using System.Collections.Generic;

namespace APP.Framework.DynamicLinq
{
    public class Query
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public IEnumerable<Sort> Sort { get; set; }
        public IEnumerable<Filter> Filter { get; set; }
    }
}
