using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using Expressions.Task3.E3SQueryProvider.Helpers;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;
        readonly List<string> _queries;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
            _queries = new List<string>();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        public List<string> TranslateToRequest(Expression exp)
        {
            Visit(exp);

            return _queries;
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Expression constant;

            switch(node.Method.Name)
            {
                case "Where" when node.Method.DeclaringType == typeof(Queryable):
                    var predicate = node.Arguments[1];
                    Visit(predicate);

                    return node;
                case "Contains":
                    constant = node.Arguments[0];
                    Visit(node.Object as MemberExpression);

                    _resultStringBuilder.Append($"(*");
                    Visit(constant);
                    _resultStringBuilder.Append("*)");

                    return node;
                case "StartsWith":
                    constant = node.Arguments[0];
                    Visit(node.Object as MemberExpression);

                    _resultStringBuilder.Append($"(");
                    Visit(constant);
                    _resultStringBuilder.Append("*)");

                    return node;
                case "EndsWith":
                    constant = node.Arguments[0];
                    Visit(node.Object as MemberExpression);

                    _resultStringBuilder.Append($"(*");
                    Visit(constant);
                    _resultStringBuilder.Append(")");

                    return node;
                case "Equals":
                    constant = node.Arguments[0];
                    Visit(node.Object as MemberExpression);

                    _resultStringBuilder.Append($"(");
                    Visit(constant);
                    _resultStringBuilder.Append(")");

                    return node;
                default: return base.VisitMethodCall(node);
            }
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    var field = node.Left.NodeType == ExpressionType.MemberAccess ? node.Left : node.Right;
                    var constant = node.Left.NodeType == ExpressionType.Constant ? node.Left : node.Right;

                    Visit(field);
                    _resultStringBuilder.Append("(");
                    Visit(constant);
                    _resultStringBuilder.Append(")");
                    break;
                case ExpressionType.AndAlso:
                    Visit(node.Left);
                    var currentExp = _resultStringBuilder.ToString();
                    _queries.Add(currentExp);
                    Visit(node.Right);
                    var rightExp = _resultStringBuilder.ToString().Substring(currentExp.Length);
                    _queries.Add(rightExp);
                    break;
                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        #endregion
    }
}
