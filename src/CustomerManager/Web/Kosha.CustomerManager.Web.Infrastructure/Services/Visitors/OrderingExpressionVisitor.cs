using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Visitors;

internal sealed class OrderingExpressionVisitor : ExpressionVisitor
{
    private readonly IEnumerable<string> _orderingMethodNames =
        [
            nameof(Queryable.OrderBy),
            nameof(Queryable.OrderByDescending),
            nameof(Queryable.ThenBy),
            nameof(Queryable.ThenByDescending)
        ];

    internal bool HasOrdering { get; private set; }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        if (
            node.Method.DeclaringType == typeof(Queryable) &&
            OrderingMethodNamesContains(node.Method.Name)
        )
        {
            HasOrdering = true;
        }

        return base.VisitMethodCall(node);
    }

    private bool OrderingMethodNamesContains(string name)
    {
        bool result = false;

        foreach (string orderingMethodName in _orderingMethodNames)
        {
            result = string.Equals(orderingMethodName, name, StringComparison.OrdinalIgnoreCase);

            if (result)
                break;
        }

        return result;
    }
}