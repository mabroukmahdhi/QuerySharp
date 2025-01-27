// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace QuerySharp.Services.Processings.Expressions
{
    internal interface IExpressionProcessingService
    {
        IExpressionProcessingService AddFilter<T>(Expression<Func<T, bool>> predicate);
        IExpressionProcessingService AddOrderBy<T>(Expression<Func<T, object>> keySelector);
        IExpressionProcessingService AddOrderByDescending<T>(Expression<Func<T, object>> keySelector);
        IExpressionProcessingService AddTop(int count);
        IExpressionProcessingService AddSkip(int count);
        IExpressionProcessingService Expand<T, TProperty>(Expression<Func<T, TProperty>> navigationProperty);
        string BuildQuery();
    }
}
