using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace SNow.Core.Utils
{
    /// <summary>
    /// base on <see href="https://www.codeproject.com/Articles/1241363/Expression-Tree-Traversal-Via-Visitor-Pattern-in-P"> Expression Tree traversal </see>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryBuilder<T> : ExpressionVisitor
    {
        private static readonly Dictionary<string, string> _methodNames = new Dictionary<string, string>
        {
            ["Contains"] = "LIKE",
            ["Equals"] = "=",
            ["StartsWith"] = "STARTSWITH",
            ["EndsWith"] = "ENDSWITH",
            ["IsNullOrEmpty"] = "ISEMPTY",
        };
        private static readonly Dictionary<string, string> _negatedMethodNames = new Dictionary<string, string>
        {
            ["Contains"] = "NOT LIKE",
            ["Equals"] = "!=",
            ["IsNullOrEmpty"] = "ISNOTEMPTY",
        };
        private static readonly Dictionary<ExpressionType, string> _logicalOperators = new Dictionary<ExpressionType, string>
        {
            [ExpressionType.Not] = "!=",
            [ExpressionType.NotEqual] = "!=",
            [ExpressionType.GreaterThan] = ">",
            [ExpressionType.GreaterThanOrEqual] = ">=",
            [ExpressionType.LessThan] = "<",
            [ExpressionType.LessThanOrEqual] = "<=",
            [ExpressionType.Equal] = "=",
            [ExpressionType.And] = "^",
            [ExpressionType.AndAlso] = "^",
            [ExpressionType.Or] = "^OR",
            [ExpressionType.ExclusiveOr] = "^OR",
            [ExpressionType.OrElse] = "^OR",
            [ExpressionType.TypeIs] = "INSTANCEOF"
        };
        private static readonly Dictionary<Type, Func<object, string>> _typeConverters = new Dictionary<Type, Func<object, string>>
        {
            [typeof(string)] = x => $"{x}",
            [typeof(DateTime)] =
                  x => $"javascript:gs.dateGenerate('{((DateTime)x).ToUniversalTime():yyyy-MM-dd}','{((DateTime)x).ToUniversalTime():HH:mm:ss}')",
#if NET6_0
            [typeof(DateOnly)] =
            x => $"javascript:gs.dateGenerate('{((DateTime)x).ToUniversalTime():yyyy-MM-dd}','{((DateTime)x).ToUniversalTime():HH:mm:ss}')",
#endif
            [typeof(bool)] = x => x.ToString().ToLower(),
            [typeof(Guid)] = x => ((Guid)x).ToString("N"),
        };

        private StringBuilder _queryStringBuilder = new StringBuilder();
        private Stack<string> _fieldNames = new Stack<string>();
        private Stack<string> _expressions = new Stack<string>();
        private readonly List<(string prop, string attr)> _jsonProperties = new List<(string, string)>();
        private LambdaExpression _exp;
        private bool _wasNegated = false;

        public string query => _queryStringBuilder.ToString();

        public QueryBuilder(Expression<Func<T, bool>> exp)
        {
            _exp = exp;
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<JsonPropertyNameAttribute>(true);
                if (attr != null)
                {
                    Debug.Print($"{prop.Name} : {attr.Name}");
                    _jsonProperties.Add((prop.Name, attr.Name));
                }
            }
        }

        //entry point
        public string BuildQuery()
        {
            //Visit transfer abstract Expression to concrete method, like VisitUnary
            //it's invocation chain (at case of unary operator) approximetely looks this way:
            //inside visitor: predicate.Body.Accept(ExpressionVisitor this)
            //inside expression(visitor is this from above): visitor.VisitUnary(this) 
            //here this is Expression
            //we not pass whole predicate, just Body, because we not need predicate.Parameters: "x =>" part
            Visit(_exp.Body);

            //_queryStringBuilder.Clear();

            return query;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            //assume we only allow not (!) unary operator:
            if (node.NodeType != ExpressionType.Not)
                throw new NotSupportedException("Only not(\"!\") unary operator is supported!");
            //Console.WriteLine("should detec Contains here");
            //Console.WriteLine(node.Operand);
            var negated = _methodNames.Keys.FirstOrDefault(k => node.Operand.ToString().Contains(k)) ?? "";
            if (node.NodeType == ExpressionType.Not && negated != null)
            {
                _wasNegated = true;
            }

            Visit(node.Operand);                                               //(!expression
            //}

            //_queryStringBuilder.Append("(");                                   //(!
            //go down from a tree
            //_queryStringBuilder.Append(")");                                   //(!expression)

            //we should return expression, it will allow to create new expression based on existing one,
            //but, at our case, it is not needed, so just return initial node argument
            return node;
        }

        //corresponds to: and, or, greater than, less than, etc.
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Visit(node.Left);
            _queryStringBuilder.Append($"{_logicalOperators[node.NodeType]}");
            Visit(node.Right);

            return node;
        }

        //correspond to: is 
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            SnowTableAttribute attr = null;
            switch (node.NodeType)
            {
                case ExpressionType.TypeIs:
                    var customType = Type.GetType(node.TypeOperand.AssemblyQualifiedName);
                    var attrs = customType?.GetCustomAttributes<SnowTableAttribute>();
                    attr = attrs?.FirstOrDefault();
                    var className = node.TypeOperand.Name;

                    if (attr == null)
                        throw new InvalidOperationException($"Model {className} missing {nameof(SnowTableAttribute)} ");

                    //Console.WriteLine($"${className}  ==> ${attr.Name}");
                    //query = query.Replace(" Is ", " INSTANCEOF ");
                    //query = query.Replace(className, attr.Name);
                    Debug.Print(attr.Name);

                    break;
            }

            var vi = base.VisitTypeBinary(node);

            _queryStringBuilder.Append($"{_logicalOperators[node.NodeType]}{attr.Name}");

            return vi;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.ToString().Contains(".Length"))
                throw new InvalidOperationException("use o .Length is not allowed!");
            _expressions.Push(_wasNegated ? _negatedMethodNames[node.Method.Name] : _methodNames[node.Method.Name]);
            _wasNegated = false;
            // _queryStringBuilder.Append("Me " +node.Method.Name);


            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            //corresponds to: order.Customer, .order, today variables
            //when we pass parameters to expression via closure, CLR internally creates class:

            //class NameSpace+<>c__DisplayClass12_0
            //{
            //    public Order order;
            //    public DateTime today;
            //}

            //which contains values of parameters. When we face order.Customer, it's node.Expression
            //will not have reference to value "Tom", but instead reference to parent (.order), so we
            //will go to it via Visit(node.Expression) and also save node.Member.Name into 
            //Stack(_fieldNames) fo further usage. order.Customer has type ExpressionType.MemberAccess. 
            //.order - ExpressionType.Constant, because it's node.Expression is ExpressionType.Constant
            //(VisitConstant will be called) that is why we can get it's actual value(instance of Order). 
            //Our Stack at this point: "Customer" <- "order". Firstly we will get "order" field value, 
            //when it will be reached, on NameSpace+<>c__DisplayClass12_0 class instance
            //(type.GetField(fieldName)) then value of "Customer" property
            //(type.GetProperty(fieldName).GetValue(input)) on it. We started from 
            //order.Customer Expression then go up via reference to it's parent - "order", get it's value 
            //and then go back - get value of "Customer" property on order. Forward and backward
            //directions, at this case, reason to use Stack structure

            if (node.Expression.NodeType == ExpressionType.Constant
               ||
               node.Expression.NodeType == ExpressionType.MemberAccess)
            {

                _fieldNames.Push(node.Member.Name);
                Visit(node.Expression);
            }
            else
            {
                //corresponds to: x.Customer - just write "Customer"
                var attributeName = _jsonProperties.FirstOrDefault(x => x.prop == node.Member.Name).attr ?? ClassReflections.ConvertCamelToSnake(node.Member.Name);
                _queryStringBuilder.Append(attributeName);

                if (_expressions.Count > 0)
                {
                    _queryStringBuilder.Append(_expressions.Pop());
                }
            }
            return node;
        }

        //corresponds to: 1, "Tom", instance of NameSpace+<>c__DisplayClass12_0, instance of Order, i.e.
        //any expression with value
        protected override Expression VisitConstant(ConstantExpression node)
        {
            //just write value
            _queryStringBuilder.Append(GetValue(node.Value));
            return node;
        }

        private string GetValue(object input)
        {
            var type = input.GetType();
            //if it is not simple value
            if (type.IsClass && type != typeof(string))
            {
                //proper order of selected names provided by means of Stack structure
                var fieldName = _fieldNames.Pop();
                var fieldInfo = type.GetField(fieldName);
                object value;
                if (fieldInfo != null)
                    //get instance of order    
                    value = fieldInfo.GetValue(input);
                else
                    //get value of "Customer" property on order
                    value = type.GetProperty(fieldName).GetValue(input);
                return GetValue(value);
            }
            else
            {
                //our predefined _typeConverters
                if (_typeConverters.ContainsKey(type))
                    return _typeConverters[type](input);
                else
                    //rest types
                    return input.ToString();
            }
        }

    }
}
