// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using QuerySharp.Services.Expressions;

namespace QuerySharp
{
    public sealed class QueryBuilder<T>
    {
        private readonly IExpressionService expressionService;
        private readonly List<string> filters;
        private readonly List<string> expands;
        private readonly List<string> orderBys;
        private int? top;
        private int? skip;

        internal QueryBuilder(IExpressionService expressionService)
        {
            this.expressionService = expressionService;
            this.filters = new List<string>();
            this.expands = new List<string>();
            this.orderBys = new List<string>();
            this.top = null;
            this.skip = null;
        }

        public QueryBuilder<T> Filter(Expression<Func<T, bool>> predicate)
        {
            string translatedExpression =
                this.expressionService.TranslateExpression(predicate.Body);

            this.filters.Add(translatedExpression);

            return this;
        }

        public QueryBuilder<T> OrderBy(Expression<Func<T, object>> keySelector)
        {
            string translatedExpression =
                this.expressionService.TranslateExpression(keySelector.Body);

            string orderedExpression = $"{translatedExpression} asc";

            this.orderBys.Add(orderedExpression);

            return this;
        }

        public QueryBuilder<T> OrderByDescending(Expression<Func<T, object>> keySelector)
        {
            string translatedExpression =
                this.expressionService.TranslateExpression(keySelector.Body);

            string orderedExpression = $"{translatedExpression} desc";

            this.orderBys.Add(orderedExpression);

            return this;
        }

        public QueryBuilder<T> Top(int count)
        {
            this.top = count;

            return this;
        }

        public QueryBuilder<T> Skip(int count)
        {
            this.skip = count;

            return this;
        }

        public QueryBuilder<T> Expand<TProperty>(Expression<Func<T, TProperty>> navigationProperty)
        {
            string translatedExpression =
                this.expressionService.TranslateExpression(navigationProperty);

            this.expands.Add(translatedExpression);

            return this;
        }

        public string Build()
        {
            var query = new StringBuilder();

            if (this.filters.Count > 0)
            {
                query.Append($"$filter={string.Join(" and ", this.filters)}&");
            }

            if (this.expands.Count > 0)
            {
                query.Append($"$expand={string.Join(",", this.expands)}&");
            }

            if (this.orderBys.Count > 0)
            {
                query.Append($"$orderby={string.Join(",", this.orderBys)}&");
            }

            if (this.top.HasValue)
            {
                query.Append($"$top={this.top.Value}&");
            }

            if (this.skip.HasValue)
            {
                query.Append($"$skip={this.skip.Value}&");
            }

            return query.ToString().TrimEnd('&');
        }
    }
}
