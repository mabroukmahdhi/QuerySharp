// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using QuerySharp.Services.Expressions;

namespace QuerySharp.Tests.Unit.Services.Expressions
{
    public partial class ExpressionServiceTests
    {
        private readonly IExpressionService expressionService;

        public ExpressionServiceTests() =>
            this.expressionService = new ExpressionService();
    }
}
