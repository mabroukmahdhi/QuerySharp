// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace QuerySharp.Models.Expressions.Exceptions
{
    internal class ExpressionValidationException : Xeption
    {
        public ExpressionValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}