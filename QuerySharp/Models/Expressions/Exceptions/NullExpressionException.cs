// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace QuerySharp.Models.Expressions.Exceptions
{
    public class NullExpressionException : Xeption
    {
        public NullExpressionException(string message)
            : base(message)
        {
        }

        public NullExpressionException(string message, Xeption innerException)
            : base(message, innerException)
        {
        }
    }
}