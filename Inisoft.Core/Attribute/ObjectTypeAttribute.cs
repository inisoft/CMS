using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core.Attribute
{

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ObjectTypeAttribute : System.Attribute
    {
        readonly string name;
        readonly string nameSpace;

        public ObjectTypeAttribute(string nameSpace, string name)
        {
            this.name = name;
            this.nameSpace = nameSpace;
        }

        public string Name
        {
            get { return name; }
        }
        public string Namespace
        {
            get { return nameSpace; }
        }
    }
}