// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using FluentAssertions;

namespace QuerySharp.Tests.Unit.Services.Processings.Expressions
{
    public partial class ExpressionProcessingServiceTests
    {
        [Fact]
        public void ShouldAddTranslatedExpressionToFilters()
        {
            // given
            Expression<Func<int, bool>> predicate = x => x > 10;
            string translatedTranslation = "x gt 10";
            string expectedTranslation = $"$filter={translatedTranslation}";

            expressionServiceMock.Setup(service =>
                service.TranslateExpression(predicate.Body))
                    .Returns(translatedTranslation);

            // when
            this.expressionProcessingService.AddFilter(predicate);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);
        }
    }
}
