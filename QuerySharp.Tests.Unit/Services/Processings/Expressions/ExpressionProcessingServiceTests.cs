// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using Moq;
using QuerySharp.Services.Foundations.Expressions;
using QuerySharp.Services.Processings.Expressions;

namespace QuerySharp.Tests.Unit.Services.Processings.Expressions
{
    public partial class ExpressionProcessingServiceTests
    {
        private readonly Mock<IExpressionService> expressionServiceMock;
        private readonly IExpressionProcessingService expressionProcessingService;

        public ExpressionProcessingServiceTests()
        {
            this.expressionServiceMock =
                new Mock<IExpressionService>();

            this.expressionProcessingService =
                new ExpressionProcessingService(
                    expressionService: this.expressionServiceMock.Object);
        }
    }
}
