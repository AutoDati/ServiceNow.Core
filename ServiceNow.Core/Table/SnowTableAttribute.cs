using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNow.Core
{
    /// <summary>
    /// Set the table name where those class properties come from
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SnowTableAttribute : Attribute
    {
        public readonly string Name;

        public SnowTableAttribute(string name)
        {
            Name = name;
        }
    }
}
