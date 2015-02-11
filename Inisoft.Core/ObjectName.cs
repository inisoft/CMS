using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    public class ObjectName
    {
        public string Namespace { get; set; }
        public string Name { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", Namespace, Name); }
        }

        public string CodeName
        {
            get { return string.Format("{0}.{1}", Namespace, Name); }
        }
    }

}