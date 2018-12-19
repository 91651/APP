using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace APP.Framework.DynamicLinq
{
    public static class QueryableExtensions
    {
        public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, int skip, int take, IEnumerable<Sort> sorts, IEnumerable<Filter> filters)
        {
            var result = new DataSourceResult();
            if (filters != null && filters.Any())
            {
                var filterStr = string.Join(",", filters?.Select((filter, index) => $"{filter.Field}==@{index}"));
                var args = filters?.Select(filter => filter.Value);
                queryable = queryable.Where(filterStr, args);
            }
            if (sorts != null && sorts.Any())
            {
                var sortStr = string.Join(",", sorts?.Select(sort => $"{sort.Field} " + (sort.Desc ? "descending " : " ")));
                queryable = queryable.OrderBy(sortStr);
            }
            result.Total = queryable.Count();
            if (skip > 0)
            {
                queryable = queryable.Skip(skip);
            }
            if (take > 0)
            {
                queryable = queryable.Take(take);
            }
            result.Data = queryable;
            return result;
        }
        public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, Query query)
        {
            return queryable.ToDataSourceResult(query.Skip, query.Take, query.Sort, query.Filter);
        }
    }
}
