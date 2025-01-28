// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using QuerySharp.Services.Expressions;
using QuerySharp.Services.Foundations.Expressions;
using QuerySharp.Services.Processings.Expressions;

namespace QuerySharp
{
    public sealed partial class QueryBuilder<T>
    {
        private void Initialize(IServiceProvider serviceProvider)
        {
            this.expressionProcessingService =
                serviceProvider.GetRequiredService<IExpressionProcessingService>();
        }

        private static IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<IExpressionService, ExpressionService>()
                .AddTransient<IExpressionProcessingService, ExpressionProcessingService>();

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
