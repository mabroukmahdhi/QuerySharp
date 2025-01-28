// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace QuerySharp.Models.Expressions.Exceptions
{
    internal class ExpressionValidationException : Xeption
    {
        public ExpressionValidationException(string message, Xeption innerException)
            : base(message, innerException)
        {
        }
    }
}