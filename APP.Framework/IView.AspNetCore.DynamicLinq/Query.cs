using System.Collections.Generic;

namespace IView.AspNetCore.DynamicLinq
{
    public class Query
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public IEnumerable<Sort> Sort { get; set; }
        public IEnumerable<Filter> Filter { get; set; }
    }
}
