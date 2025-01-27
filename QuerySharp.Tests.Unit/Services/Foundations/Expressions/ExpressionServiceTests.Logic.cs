// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using FluentAssertions;
using QuerySharp.Tests.Unit.Models;

namespace QuerySharp.Tests.Unit.Services.Expressions
{
    public partial class ExpressionServiceTests
    {
        [Fact]
        public void ShouldTranslateExpressionToQueryString()
        {
            // given 
            Expression<Func<SomeModel, bool>> expression =
                someModel => someModel.Id == 5;

            string expectedExpression = "(Id eq 5)";

            // when
            string actualQuery =
                expressionService.TranslateExpression(expression.Body);

            // then
            actualQuery.Should().NotBeNull();
            actualQuery.Should().Be(expectedExpression);
        }
    }
}
