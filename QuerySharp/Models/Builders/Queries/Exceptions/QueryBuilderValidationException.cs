// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace QuerySharp.Models.Builders.Queries.Exceptions
{
    internal class QueryBuilderValidationException : Xeption
    {
        public QueryBuilderValidationException(string message, Xeption innerException)
            : base(message, innerException)
        {
        }
    }
}
