// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System.Linq.Expressions;

namespace QuerySharp.Services.Expressions
{
    internal interface IExpressionService
    {
        string TranslateExpression(Expression expression);
    }
}
