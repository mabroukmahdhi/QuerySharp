// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace QuerySharp.Models.Expressions.Exceptions
{
    public class ExpressionValidationException : Xeption
    {
        public ExpressionValidationException(string message, Xeption innerException)
            : base(message, innerException)
        {
        }
    }
}