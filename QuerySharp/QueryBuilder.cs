// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using QuerySharp.Services.Processings.Expressions;

namespace QuerySharp
{
    public sealed class QueryBuilder<T>
    {
        private readonly IExpressionProcessingService expressionProcessingService;

        internal QueryBuilder(IExpressionProcessingService expressionProcessingService) =>
            this.expressionProcessingService = expressionProcessingService;

        public QueryBuilder<T> Filter(Expression<Func<T, bool>> predicate)
        {
            this.expressionProcessingService.AddFilter(predicate);

            return this;
        }

        public QueryBuilder<T> OrderBy(Expression<Func<T, object>> keySelector)
        {
            this.expressionProcessingService.AddOrderBy(keySelector);

            return this;
        }

        public QueryBuilder<T> OrderByDescending(Expression<Func<T, object>> keySelector)
        {
            this.expressionProcessingService.AddOrderByDescending(keySelector);

            return this;
        }

        public QueryBuilder<T> Top(int count)
        {
            this.expressionProcessingService
                .AddTop(count);

            return this;
        }

        public QueryBuilder<T> Skip(int count)
        {
            this.expressionProcessingService
                .AddTop(count);

            return this;
        }

        public QueryBuilder<T> Expand<TProperty>(Expression<Func<T, TProperty>> navigationProperty)
        {
            this.expressionProcessingService.Expand(navigationProperty);

            return this;
        }

        public string Build() =>
            this.expressionProcessingService.BuildQuery();
    }
}
