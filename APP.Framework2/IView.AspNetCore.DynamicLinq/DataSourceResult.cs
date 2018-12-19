using System.Collections;

namespace APP.AspNetCore.DynamicLinq
{
    public class DataSourceResult
    {
        public IEnumerable Data { get; set; }
        public int Total { get; set; }
    }
}
