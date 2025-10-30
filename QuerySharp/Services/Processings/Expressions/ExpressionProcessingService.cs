// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using QuerySharp.Services.Expressions;

namespace QuerySharp.Services.Processings.Expressions
{
    internal class ExpressionProcessingService : IExpressionProcessingService
    {
        private readonly List<string> filters;
        private readonly List<string> expands;
        private readonly List<string> orderBys;
        private int? top;
        private int? skip;

        public ExpressionProcessingService()
        {
            this.filters = new List<string>();
            this.expands = new List<string>();
            this.orderBys = new List<string>();
            this.top = null;
            this.skip = null;
        }

        public void AddFilter<T>(Expression<Func<T, bool>> predicate)
        {
            var expressionService = new ExpressionService();

            string translatedExpression =
                expressionService.TranslateExpression(predicate.Body);

            this.filters.Add(translatedExpression);
        }

        public void AddOrderBy<T>(Expression<Func<T, object>> keySelector)
        {
            var expressionService = new ExpressionService();

            string translatedExpression =
                expressionService.TranslateExpression(keySelector.Body);

            string orderedExpression = $"{translatedExpression} asc";

            this.orderBys.Add(orderedExpression);
        }

        public void AddOrderByDescending<T>(Expression<Func<T, object>> keySelector)
        {
            var expressionService = new ExpressionService();

            string translatedExpression =
                expressionService.TranslateExpression(keySelector.Body);

            string orderedExpression = $"{translatedExpression} desc";

            this.orderBys.Add(orderedExpression);
        }

        public void AddTop(int count)
        {
            this.top = count;
        }

        public void AddSkip(int count)
        {
            this.skip = count;
        }

        public void Expand<T, TProperty>(Expression<Func<T, TProperty>> navigationProperty)
        {
            var expressionService = new ExpressionService();

            string translatedExpression =
                expressionService.TranslateExpression(navigationProperty);

            this.expands.Add(translatedExpression);
        }

        public string BuildQuery()
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
