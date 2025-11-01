// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
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
            Visit(node.Left);
            queryBuilder.Append($" {GetODataOperator(node.NodeType)} ");
            Visit(node.Right);

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (IsCapturedValue(node))
            {
                object value = Evaluate(node);
                AppendConstant(value);
                return node;
            }

            if (node.Expression is MemberExpression parent)
            {
                Visit(parent);
                queryBuilder.Append('/');
            }

            // When the expression is the parameter itself, just append the member name.
            queryBuilder.Append(node.Member.Name);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            AppendConstant(node.Value);
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

        private static bool IsCapturedValue(MemberExpression node)
        {
            // Walk up to the root expression; if it's a ConstantExpression, the member is coming from a closure/captured variable.
            Expression current = node;
            while (current is MemberExpression m)
            {
                current = m.Expression;
            }

            return current is ConstantExpression || current == null; // null can be static member
        }

        private static object Evaluate(Expression expression)
        {
            // Compile a lambda to evaluate the expression to a runtime value.
            var objectMember = Expression.Convert(expression, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }

        private void AppendConstant(object value)
        {
            switch (value)
            {
                case null:
                    queryBuilder.Append("null");
                    break;
                case string s:
                    queryBuilder.Append($"'{s}'");
                    break;
                case bool b:
                    queryBuilder.Append(b ? "true" : "false");
                    break;
                default:
                    queryBuilder.Append(value);
                    break;
            }
        }
    }
}
