using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Reflection;

namespace APP.Framework.EFCore
{
    public static class QueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");
        private static readonly FieldInfo QueryModelGeneratorField = typeof(QueryCompiler).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryModelGenerator");
        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");
        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        /// <summary>
        /// 获取本次查询SQL语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string ToSql<TEntity>(this IQueryable<TEntity> query)
        {
            throw new NotImplementedException();
            //var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            //var queryModelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            //var queryModel = queryModelGenerator.ParseQuery(query.Expression);
            //var database = DataBaseField.GetValue(queryCompiler);
            //var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            //var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            //var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            //modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            //var sql = modelVisitor.Queries.First().ToString();

            //return sql;
        }
        
        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">按哪个字段更新</typeparam>
        /// <param name="dbSet"></param>
        /// <param name="keySelector">按哪个字段更新</param>
        /// <param name="entity"></param>
        public static void AddOrUpdate<T, TKey>(this DbSet<T> dbSet, Expression<Func<T, TKey>> keySelector, T entity) where T : class
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var keyObject = keySelector.Compile()(entity);
            var parameter = Expression.Parameter(typeof(T), "p");
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Equal(ReplaceParameter(keySelector.Body, parameter), Expression.Constant(keyObject)), parameter);
            var item = dbSet.FirstOrDefault(lambda);
            if (item == null)
            {
                dbSet.Add(entity);
            }
            else
            {
                // 获取主键字段
                var dataType = typeof(T);
                var keyFields = dataType.GetProperties().Where(p => p.GetCustomAttribute<KeyAttribute>() != null).ToList();
                if (!keyFields.Any())

                {
                    string idName = dataType.Name + "Id";
                    keyFields = dataType.GetProperties().Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) || p.Name.Equals(idName, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                // 更新所有非主键属性
                foreach (var p in typeof(T).GetProperties().Where(p => p.GetSetMethod
                () != null && p.GetGetMethod() != null))
                {
                    // 忽略主键
                    if (keyFields.Any(x => x.Name == p.Name))
                    {
                        continue;
                    }
                    var existingValue = p.GetValue(entity);
                    if (p.GetValue(item) != existingValue)
                    {
                        p.SetValue(item, existingValue);
                    }
                }
                foreach (var idField in keyFields.Where(p => p.GetSetMethod() != null
                && p.GetGetMethod() != null))
                {
                    var existingValue = idField.GetValue(item);
                    if (idField.GetValue(entity) != existingValue)
                    {
                        idField.SetValue(entity, existingValue);
                    }
                }
            }
        }
        private static Expression ReplaceParameter(Expression oldExpression, ParameterExpression newParameter)
        {
            return oldExpression.NodeType switch
            {
                ExpressionType.MemberAccess => Expression.MakeMemberAccess(newParameter, ((MemberExpression)oldExpression).Member),
                ExpressionType.New => Expression.New(((NewExpression)oldExpression).Constructor, ((NewExpression)oldExpression).Arguments.Select(a => ReplaceParameter(a, newParameter)).ToArray()),
                _ => throw new NotSupportedException("不支持的表达式类型：" + oldExpression.NodeType)
            };
        }
    }
}
