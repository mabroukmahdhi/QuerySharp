// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using QuerySharp.Services.Foundations.Expressions;

namespace QuerySharp.Services.Expressions
{
    internal partial class ExpressionService : ExpressionVisitor, IExpressionService
    {
        private readonly StringBuilder queryBuilder;

        public ExpressionService() =>
            queryBuilder = new StringBuilder();

        public string TranslateExpression(Expression expression) =>
        TryCatch(() =>
        {
            ValidateExpression(expression);

            Visit(expression);

            return queryBuilder.ToString().Trim();
        });

        protected override Expression VisitBinary(BinaryExpression node)
        {
            //queryBuilder.Append("(");
            Visit(node.Left);
            queryBuilder.Append($" {GetODataOperator(node.NodeType)} ");
            Visit(node.Right);
            //queryBuilder.Append(")");

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is MemberExpression parent)
            {
                Visit(parent);
                queryBuilder.Append('/');
            }

            queryBuilder.Append(node.Member.Name);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type == typeof(string))
            {
                queryBuilder.Append($"'{node.Value}'");
            }
            else
            {
                queryBuilder.Append(node.Value);
            }

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(string))
            {
                HandleStringMethod(node);
            }
            else if (node.Method.DeclaringType == typeof(Enumerable) && node.Method.Name == "Where")
            {
                HandleCollectionFilter(node);
            }
            else
            {
                throw new NotSupportedException($"Method {node.Method.Name} is not supported.");
            }

            return node;
        }

        private void HandleStringMethod(MethodCallExpression node)
        {
            string odataFunction;

            switch (node.Method.Name)
            {
                case "Contains":
                    odataFunction = "contains";
                    break;
                case "StartsWith":
                    odataFunction = "startswith";
                    break;
                case "EndsWith":
                    odataFunction = "endswith";
                    break;
                default:
                    throw new NotSupportedException(
                        message: $"String method {node.Method.Name} is not supported.");
            }

            queryBuilder.Append($"{odataFunction}(");
            Visit(node.Object);
            queryBuilder.Append(",");
            Visit(node.Arguments[0]);
            queryBuilder.Append(")");
        }

        private void HandleCollectionFilter(MethodCallExpression node)
        {
            Visit(node.Arguments[0]);
            queryBuilder.Append("($filter=");
            Visit(node.Arguments[1]);
            queryBuilder.Append(')');
        }

        private static string GetODataOperator(ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.Equal:
                    return "eq";
                case ExpressionType.NotEqual:
                    return "ne";
                case ExpressionType.GreaterThan:
                    return "gt";
                case ExpressionType.GreaterThanOrEqual:
                    return "ge";
                case ExpressionType.LessThan:
                    return "lt";
                case ExpressionType.LessThanOrEqual:
                    return "le";
                case ExpressionType.AndAlso:
                    return "and";
                case ExpressionType.OrElse:
                    return "or";
                default:
                    throw new NotSupportedException($"Operator {expressionType} is not supported.");
            }
        }
    }
}
