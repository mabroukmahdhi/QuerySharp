// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using FluentAssertions;
using QuerySharp.Tests.Unit.Models;

namespace QuerySharp.Tests.Unit.Services.Processings.Expressions
{
    public partial class ExpressionProcessingServiceTests
    {
        [Fact]
        public void ShouldAddTranslatedExpressionToFilters()
        {
            // given
            Expression<Func<SomeModel, bool>> predicate = x => x.Id > 10;
            string translatedTranslation = "Id gt 10";
            string expectedTranslation = $"$filter={translatedTranslation}";

            // when
            this.expressionProcessingService.AddFilter(predicate);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);
        }

        [Fact]
        public void ShouldAddTranslatedExpressionToOrderBy()
        {
            // given
            Expression<Func<SomeModel, object>> keySelector = x => x.Name;
            string expectedTranslation = "$orderby=Name asc";

            // when
            this.expressionProcessingService.AddOrderBy(keySelector);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);
        }

        [Fact]
        public void ShouldAddTranslatedExpressionToOrderByDescending()
        {
            // given
            Expression<Func<SomeModel, object>> keySelector = x => x.Name;
            string expectedTranslation = "$orderby=Name desc";

            // when
            this.expressionProcessingService.AddOrderByDescending(keySelector);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);
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
        }

        [Fact]
        public void ShouldAddTranslatedExpressionToExpands()
        {
            // given
            Expression<Func<SomeModel, bool>> navigationProperty = x => x.Parent.Id == 5;
            string expectedTranslation = "$expand=Parent/Id eq 5";

            // when
            this.expressionProcessingService.Expand(navigationProperty);

            // then
            string query = this.expressionProcessingService.BuildQuery();
            query.Should().Be(expectedTranslation);
        }

        [Fact]
        public void ShouldBuildCompleteQueryString()
        {
            // given
            Expression<Func<SomeModel, bool>> filter = x => x.Id > 10;
            Expression<Func<SomeModel, object>> orderBy = x => x.Name;
            Expression<Func<SomeModel, SomeModel>> expand = x => x.Parent;

            string expectedQuery = "$filter=Id gt 10&$orderby=Name asc";

            this.expressionProcessingService.AddFilter(filter);
            this.expressionProcessingService.AddOrderBy(orderBy);

            // when
            string actualQuery =
                this.expressionProcessingService.BuildQuery();

            // then
            actualQuery.Should().Be(expectedQuery);
        }

        [Fact]
        public void ShouldBuildCompleteQueryWithContainsOperation()
        {
            // given 
            Expression<Func<SomeModel, bool>> contains = x => x.Name.Contains("Mabrouk");
            string expectedQuery = "$filter=contains(Name,'Mabrouk')";
            this.expressionProcessingService.AddFilter(contains);

            // when
            string actualQuery =
                this.expressionProcessingService.BuildQuery();

            // then
            actualQuery.Should().Be(expectedQuery);
        }

        [Fact]
        public void ShouldBuildCompleteQueryWithContainsOperationWithVariable()
        {
            // given 
            string namePart = "Mabrouk";
            Expression<Func<SomeModel, bool>> contains = x => x.Name.Contains(namePart);
            string expectedQuery = "$filter=contains(Name,'Mabrouk')";
            this.expressionProcessingService.AddFilter(contains);

            // when
            string actualQuery =
                this.expressionProcessingService.BuildQuery();

            // then
            actualQuery.Should().Be(expectedQuery);
        }
    }
}
