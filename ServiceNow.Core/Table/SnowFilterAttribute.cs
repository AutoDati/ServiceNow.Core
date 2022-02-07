using SNow.Core.Models;
using SNow.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SNow.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SnowFilterAttribute : Attribute
    {
        public readonly string Query;

        public SnowFilterAttribute(string query)
        {            
            Query = query;
        }
    }
}
