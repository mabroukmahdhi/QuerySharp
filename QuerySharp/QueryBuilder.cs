// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using QuerySharp.Services.Processings.Expressions;

namespace QuerySharp
{
    /// <summary>
    /// Provides a fluent API for building OData queries.
    /// </summary>
    /// <typeparam name="T">The main query entity</typeparam>
    public sealed partial class QueryBuilder<T>
    {
        private IExpressionProcessingService expressionProcessingService;

        internal QueryBuilder()
        {
            IServiceProvider serviceProvider =
                RegisterServices();

            Initialize(serviceProvider);
        }

        /// <summary>
        /// Start building a query.
        /// </summary>
        /// <returns>A new instance of <see cref="QueryBuilder{T}"/></returns>
        public static QueryBuilder<T> Start() =>
            new QueryBuilder<T>();

        /// <summary>
        /// Adds a filter to the query based on the given predicate.
        /// </summary>
        /// <param name="predicate">The filter predicate expression.</param>
        /// <returns>The current instance of <see cref="QueryBuilder{T}"/></returns>
        public QueryBuilder<T> Filter(Expression<Func<T, bool>> predicate)
        {
            this.expressionProcessingService.AddFilter(predicate);

            return this;
        }

        /// <summary>
        /// Adds an ascending order by clause to the query based on the given key selector.
        /// </summary>
        /// <param name="keySelector">The key selector expression for ordering.</param>
        /// <returns>The current instance of <see cref="QueryBuilder{T}"/></returns>
        public QueryBuilder<T> OrderBy(Expression<Func<T, object>> keySelector)
        {
            this.expressionProcessingService.AddOrderBy(keySelector);

            return this;
        }

        /// <summary>
        /// Adds a descending order by clause to the query based on the given key selector.
        /// </summary>
        /// <param name="keySelector">The key selector expression for ordering.</param>
        /// <returns>The current instance of <see cref="QueryBuilder{T}"/></returns>
        public QueryBuilder<T> OrderByDescending(Expression<Func<T, object>> keySelector)
        {
            this.expressionProcessingService.AddOrderByDescending(keySelector);

            return this;
        }

        /// <summary>
        /// Limits the number of results in the query to the specified count.
        /// </summary>
        /// <param name="count">The maximum number of results to return.</param>
        /// <returns>The current instance of <see cref="QueryBuilder{T}"/></returns>
        public QueryBuilder<T> Top(int count)
        {
            this.expressionProcessingService
                .AddTop(count);

            return this;
        }

        /// <summary>
        /// Skips the specified number of results in the query.
        /// </summary>
        /// <param name="count">The number of results to skip.</param>
        /// <returns>The current instance of <see cref="QueryBuilder{T}"/></returns>
        public QueryBuilder<T> Skip(int count)
        {
            this.expressionProcessingService
                .AddTop(count);

            return this;
        }

        /// <summary>
        /// Expands the query to include the specified navigation property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the navigation property.</typeparam>
        /// <param name="navigationProperty">The navigation property expression to expand.</param>
        /// <returns>The current instance of <see cref="QueryBuilder{T}"/></returns>
        public QueryBuilder<T> Expand<TProperty>(Expression<Func<T, TProperty>> navigationProperty)
        {
            this.expressionProcessingService.Expand(navigationProperty);

            return this;
        }

        /// <summary>
        /// Builds and returns the query string.
        /// </summary>
        /// <returns>The constructed query string.</returns>
        public string Build() =>
            this.expressionProcessingService.BuildQuery();
    }
}
