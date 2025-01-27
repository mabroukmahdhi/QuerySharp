// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace QuerySharp.Models.Expressions.Exceptions
{
    public class NotSupportedExpressionException : Exception
    {
        public NotSupportedExpressionException(string message) : base(message)
        {
        }
    }
}