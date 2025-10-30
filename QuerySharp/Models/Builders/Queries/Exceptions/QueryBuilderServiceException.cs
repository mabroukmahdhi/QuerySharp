// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace QuerySharp.Models.Builders.Queries.Exceptions
{
    internal class QueryBuilderServiceException : Xeption
    {
        public QueryBuilderServiceException(string message, Xeption innerException)
            : base(message, innerException)
        {
        }
    }
}
