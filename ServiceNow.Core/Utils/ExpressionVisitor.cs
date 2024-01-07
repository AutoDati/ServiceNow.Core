using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SNow.Core.Utils
{
    class PrintingVisitor<T> : ExpressionVisitor
    {
        public string query;
        private ParameterExpression arg;
        private List<(string prop, string attr)> jsonProperties = new List<(string, string)>();
        //private List<string> properties = new List<string>();
        

        public PrintingVisitor(Expression<Func<T, bool>> exp)
        {
            arg = exp.Parameters[0];

            query = exp.Body.ToString();
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<JsonPropertyNameAttribute>(true);
                if (attr != null)
                {
                    Debug.Print($"{prop.Name} : {attr.Name}");
                    jsonProperties.Add((prop.Name, attr.Name));
                }
                //else
                //    properties.Add(prop.Name);
            }

        }
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.TypeIs:
                    var customType = Type.GetType(node.TypeOperand.AssemblyQualifiedName);
                    var attrs = customType?.GetCustomAttributes<SnowTableAttribute>();
                    var attr = attrs?.FirstOrDefault();
                    var className = node.TypeOperand.Name;

                    if (attr == null)
                        throw new InvalidOperationException($"Model {className} missing {nameof(SnowTableAttribute)} ");

                    query = query.Replace(" Is ", " INSTANCEOF ");
                    query = query.Replace(className, attr.Name);
                    Debug.Print(attr.Name);

            break;
            }
            return base.VisitTypeBinary(node);
        }

        #region Unused Expressions

        //public override Expression Visit(Expression node)
        //{
        //    return base.Visit(node);
        //}

        //protected override Expression VisitLambda<T>(Expression<T> node)
        //{
        //    return base.VisitLambda(node);
        //}

        //protected override Expression VisitParameter(ParameterExpression node)
        //{
        //    return base.VisitParameter(node);
        //}

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var propertyExpression = node.Operand as MemberExpression;
            var result = base.VisitUnary(node);
            if (propertyExpression?.Type?.Name == "Guid")
            {
                var guidExpression = result.Reduce() as UnaryExpression;
                var plainGuid = (guidExpression.Operand as ConstantExpression).Value.ToString();
                var nomalizedGuid = plainGuid.Replace("-", "");
                query = query.Replace(plainGuid, nomalizedGuid);
            }
            return result;
        }

        //protected override Expression VisitTry(TryExpression node)
        //{
        //    return base.VisitTry(node);
        //}

        //protected override SwitchCase VisitSwitchCase(SwitchCase node)
        //{
        //    return base.VisitSwitchCase(node);
        //}

        //protected override Expression VisitBlock(BlockExpression node)
        //{
        //    return base.VisitBlock(node);
        //}

        //protected override CatchBlock VisitCatchBlock(CatchBlock node)
        //{
        //    return base.VisitCatchBlock(node);
        //}

        //protected override Expression VisitDebugInfo(DebugInfoExpression node)
        //{
        //    return base.VisitDebugInfo(node);
        //}

        //protected override Expression VisitDefault(DefaultExpression node)
        //{
        //    return base.VisitDefault(node);
        //}

        //protected override Expression VisitDynamic(DynamicExpression node)
        //{
        //    return base.VisitDynamic(node);
        //}

        //protected override ElementInit VisitElementInit(ElementInit node)
        //{
        //    return base.VisitElementInit(node);
        //}

        //protected override Expression VisitExtension(Expression node)
        //{
        //    return base.VisitExtension(node);
        //}


        //protected override Expression VisitGoto(GotoExpression node)
        //{
        //    return base.VisitGoto(node);
        //}

        //protected override Expression VisitIndex(IndexExpression node)
        //{
        //    return base.VisitIndex(node);
        //}

        //protected override Expression VisitInvocation(InvocationExpression node)
        //{
        //    return base.VisitInvocation(node);
        //}

        //protected override Expression VisitLabel(LabelExpression node)
        //{
        //    return base.VisitLabel(node);
        //}

        //protected override LabelTarget VisitLabelTarget(LabelTarget node)
        //{
        //    return base.VisitLabelTarget(node);
        //}

        //protected override Expression VisitNew(NewExpression node)
        //{
        //    return base.VisitNew(node);
        //}

        //protected override Expression VisitListInit(ListInitExpression node)
        //{
        //    return base.VisitListInit(node);
        //}
        //protected override Expression VisitLoop(LoopExpression node)
        //{
        //    return base.VisitLoop(node);
        //}
        //protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        //{
        //    return base.VisitMemberAssignment(node);
        //}
        //protected override MemberBinding VisitMemberBinding(MemberBinding node)
        //{
        //    return base.VisitMemberBinding(node);
        //}
        //protected override Expression VisitMemberInit(MemberInitExpression node)
        //{
        //    return base.VisitMemberInit(node);
        //}

        //protected override Expression VisitSwitch(SwitchExpression node)
        //{
        //    return base.VisitSwitch(node);
        //}
        //protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        //{
        //    return base.VisitRuntimeVariables(node);
        //}
        //protected override Expression VisitNewArray(NewArrayExpression node)
        //{
        //    return base.VisitNewArray(node);
        //}
        //protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        //{
        //    return base.VisitMemberMemberBinding(node);
        //}

        //protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        //{
        //    return base.VisitMemberListBinding(node);
        //}

        #endregion
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Debug.Print("Visiting Method Call {0}", node);
            Debug.Print(node.Method.Name);

            if (node.ToString().Contains(".Length"))
                throw new InvalidOperationException("use o .Length is not allowed!");

            switch (node.Method.Name)
            {
                case "Equals":
                    query = query.Replace(".Equals(", " = ");
                    break;
                case "Contains":
                    Debug.Print(" replace with LIKE");
                    query = query.Replace(".Contains(", " LIKE ");
                    break;
                case "StartsWith":
                    query = query.Replace(".StartsWith(", " STARTSWITH ");
                    Debug.Print(" replace with StartWith");
                    break;
                case "EndsWith":
                    query = query.Replace(".EndsWith(", " ENDSWITH ");
                    Debug.Print(" replace with EndsWith");
                    break;
                case "IsNullOrEmpty":
                    query = query.Replace($"Not(IsNullOrEmpty({node.Arguments[0]}))", $"{node.Arguments[0]}ISNOTEMPTY");
                    query = query.Replace($"IsNullOrEmpty({node.Arguments[0]})", $"{node.Arguments[0]}ISEMPTY");
                    Debug.Print(" replace with ISEMPTY");
                    break;
                default:
                    Debug.Print("Throw Error here????");
                    break;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {            
            Debug.Print("Visiting Conditional {0}", node);

            // Recurse down to see if we can simplify...
            var expression = Visit(node.Test);

            // IF(something, then this, or that)
            if (expression is ConstantExpression)
            {
                var container = (bool)((ConstantExpression)expression).Value;
                query = query.Split(',')[container ? 1 : 2];
            }

            return base.VisitConditional(node);
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {
            Debug.Print("Visiting Constant: {0} = {1}", node, node.Value);
            if (!node.ToString().Contains("value("))
                query = query.Replace(node.ToString(), node.Value.ToString());
       

                query = query.Replace("Guid.Empty", "");
            return base.VisitConstant(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Debug.Print("Visiting Binary {0}", node);
            var nodeString = node.ToString();
            if (nodeString[0] == '(' && nodeString[nodeString.Length - 1] == ')')
                query = query.Replace(nodeString, nodeString.Substring(1, nodeString.Length - 2));
            switch (node.NodeType)
            {
                #region Replace operator
                case ExpressionType.TypeIs:
                    //Debug.Print("Is ...");
                    break;
                case ExpressionType.And:
                    query = query.Replace(nameof(ExpressionType.And), " ^ ");
                    break;
                case ExpressionType.AndAlso:
                    query = query.Replace(nameof(ExpressionType.AndAlso), " ^ ");
                    break;
                case ExpressionType.Or:
                    query = query.Replace(nameof(ExpressionType.Or), " ^OR ");
                    break;
                case ExpressionType.OrElse:
                    query = query.Replace(nameof(ExpressionType.OrElse), " ^OR ");
                    break;
                case ExpressionType.ExclusiveOr:
                    query = query.Replace(nameof(ExpressionType.ExclusiveOr), " ^OR ");
                    break;
                #endregion
                #region Ignore
                case ExpressionType.Equal:
                    query = query.Replace("==", "=");
                    break;
                case ExpressionType.NotEqual:
                    break;
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                //TODO: check if works in SNow
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                    //Debug.Print("Replace with > < => ???");
                    //Debug.Print("Check How to handle {0}", node.NodeType);
                    break;
                #endregion
                default:
                    throw new InvalidOperationException($"{node.NodeType} not allowed in table where expression!");
            }
            return base.VisitBinary(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Debug.Print("Visiting Member: {0} => is parameter? {1}", node, node.Expression == arg);

            // Recurse down to see if we can simplify...
            var expression = Visit(node.Expression);

            // If we've ended up with a constant, and it's a property or a field,
            // we can simplify ourselves to a constant
            if (expression is ConstantExpression)
            {
                object container = ((ConstantExpression)expression).Value;
                var member = node.Member;
                if (member is FieldInfo)
                {
                    object value = ((FieldInfo)member).GetValue(container);
                    Debug.Print("Got value: {0}", value);
                    query = query.Replace((node as Expression).ToString(), value.ToString());
                    return Expression.Constant(value);
                }
                if (member is PropertyInfo)
                {
                    object value = ((PropertyInfo)member).GetValue(container, null);
                    Debug.Print("Got value 2: {0}", value);
                    return Expression.Constant(value);
                }
            }
            else
            {
                if (node.Expression == arg)
                {
                    var attributeName = jsonProperties.FirstOrDefault(x => x.prop == node.Member.Name);
                    query = query.Replace(node.ToString(), attributeName.attr != null ? attributeName.attr : node.ToString().ToLower().Split('.')[1]);
                }
            }
            return base.VisitMember(node);
        }

    }
}
