// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using FluentAssertions;
using Moq;

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

            this.expressionServiceMock.Verify(service =>
                service.TranslateExpression(predicate.Body),
                    Times.Once);

            this.expressionServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldAddTranslatedExpressionToOrderBy()
        {
            // given
            Expression<Func<int, object>> keySelector = x => x;
            string translatedTranslation = "x";
            string expectedTranslation = "$orderby=x asc";

            expressionServiceMock.Setup(service =>
                service.TranslateExpression(keySelector.Body))
                    .Returns(translatedTranslation);

            // when
            this.expressionProcessingService.AddOrderBy(keySelector);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);

            this.expressionServiceMock.Verify(service =>
                service.TranslateExpression(keySelector.Body),
                    Times.Once);

            this.expressionServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldAddTranslatedExpressionToOrderByDescending()
        {
            // given
            Expression<Func<int, object>> keySelector = x => x;
            string translatedTranslation = "x";
            string expectedTranslation = "$orderby=x desc";

            expressionServiceMock.Setup(service =>
                service.TranslateExpression(keySelector.Body))
                    .Returns(translatedTranslation);

            // when
            this.expressionProcessingService.AddOrderByDescending(keySelector);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);

            this.expressionServiceMock.Verify(service =>
                service.TranslateExpression(keySelector.Body),
                    Times.Once);

            this.expressionServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldAddTopClauseToQuery()
        {
            // given
            int topValue = 5;
            string expectedTranslation = "$top=5";

            // when
            this.expressionProcessingService.AddTop(topValue);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);

            this.expressionServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldAddSkipClauseToQuery()
        {
            // given
            int skipValue = 10;
            string expectedTranslation = "$skip=10";

            // when
            this.expressionProcessingService.AddSkip(skipValue);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);

            this.expressionServiceMock.VerifyNoOtherCalls();
        }
    }
}
