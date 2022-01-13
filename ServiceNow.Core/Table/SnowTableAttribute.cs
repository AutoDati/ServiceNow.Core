using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNow.Core
{
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
