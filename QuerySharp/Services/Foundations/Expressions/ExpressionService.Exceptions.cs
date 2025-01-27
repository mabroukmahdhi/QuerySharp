// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using QuerySharp.Models.Expressions.Exceptions;
using Xeptions;

namespace QuerySharp.Services.Expressions
{
    internal partial class ExpressionService
    {
        private delegate string ReturningString();

        private string TryCatch(ReturningString returningString)
        {
            try
            {
                return returningString();
            }
            catch (NullExpressionException nullExpressionException)
            {
                throw CreateExpressionValidationException(
                    innerException: nullExpressionException);
            }
        }

        private static ExpressionValidationException CreateExpressionValidationException(Xeption innerException)
        {
            return new ExpressionValidationException(
                message: "Expression validation error occurred, fix the errors and try again.",
                innerException);
        }
    }
}