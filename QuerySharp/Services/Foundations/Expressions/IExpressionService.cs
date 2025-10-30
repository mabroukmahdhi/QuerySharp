// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using System.Linq.Expressions;

namespace QuerySharp.Services.Foundations.Expressions
{
    internal interface IExpressionService
    {
        string TranslateExpression(Expression expression);
    }
}
