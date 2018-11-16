using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace IView.AspNetCore.DynamicLinq
{
    public static class QueryableExtensions
    {
        public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, int take, int skip, IEnumerable<Sort> sorts, Filter filter)
        {
            var result = new DataSourceResult();

            var a = queryable.Where($"{filter.Field} = {filter.Value}").OrderBy().Skip(skip).Take(take);
            return null;
        }
        public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, Query query)
        {
            return queryable.ToDataSourceResult(query.Take, query.Take, query.Sort, query.Filter);
        }
    }
}
