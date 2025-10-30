// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using QuerySharp.Services.Processings.Expressions;

namespace QuerySharp.Tests.Unit.Services.Processings.Expressions
{
    public partial class ExpressionProcessingServiceTests
    {
        private readonly IExpressionProcessingService expressionProcessingService;

        public ExpressionProcessingServiceTests()
        {
            this.expressionProcessingService =
                new ExpressionProcessingService();
        }
    }
}
