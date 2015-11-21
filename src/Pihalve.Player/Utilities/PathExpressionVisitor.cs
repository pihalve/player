using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pihalve.Player.Utilities
{
    public class PathExpressionVisitor : ExpressionVisitor
    {
        private readonly List<string> _path = new List<string>();

        public static string[] GetPath<TSource, TResult>(Expression<Func<TSource, TResult>> expression)
        {
            var visitor = new PathExpressionVisitor();
            visitor.Visit(expression.Body);
            return Enumerable.Reverse(visitor._path).ToArray();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _path.Add(node.Member.Name);
            return base.VisitMember(node);
        }
    }
}
