// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using FluentAssertions;
using QuerySharp.Models.Expressions.Exceptions;

namespace QuerySharp.Tests.Unit.Services.Expressions
{
    public partial class ExpressionServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnVisitWhenExpressionIsNull()
        {
            // given
            Expression nullExpression = null;

            var nullExpressionException =
                new NullExpressionException(
                    message: "Expression is null.");

            var exceptedExpressionValidationException =
                new ExpressionValidationException(
                    message: "Expression validation error occurred, fix the errors and try again.",
                    innerException: nullExpressionException);

            // when
            Action visitAction = () =>
                this.expressionService.TranslateExpression(nullExpression);

            ExpressionValidationException actualExpressionValidationException =
                Assert.Throws<ExpressionValidationException>(
                    visitAction);

            // then
            actualExpressionValidationException.Should()
                .BeEquivalentTo(exceptedExpressionValidationException);
        }
    }
}
