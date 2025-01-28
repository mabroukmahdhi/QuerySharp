// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace QuerySharp.Services.Processings.Expressions
{
    internal interface IExpressionProcessingService
    {
        void AddFilter<T>(Expression<Func<T, bool>> predicate);
        void AddOrderBy<T>(Expression<Func<T, object>> keySelector);
        void AddOrderByDescending<T>(Expression<Func<T, object>> keySelector);
        void AddTop(int count);
        void AddSkip(int count);
        void Expand<T, TProperty>(Expression<Func<T, TProperty>> navigationProperty);
        string BuildQuery();
    }
}
