// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace QuerySharp.Models.Expressions.Exceptions
{
    internal class NotSupportedExpressionException : Exception
    {
        public NotSupportedExpressionException(string message) : base(message)
        {
        }
    }
}