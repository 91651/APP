﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace APP.Framework.EFCore
{
    public static class QueryableExtensions
    {
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
