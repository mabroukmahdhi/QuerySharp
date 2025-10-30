// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using System.Linq.Expressions;
using QuerySharp.Models.Expressions.Exceptions;

namespace QuerySharp.Services.Expressions
{
    internal partial class ExpressionService
    {
        private static void ValidateExpression(Expression expression)
        {
            if (expression == null)
                throw new NullExpressionException("Expression is null.");
        }
    }
}