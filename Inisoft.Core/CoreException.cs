using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    public class CoreException : Exception
    {
        public CoreException(string className, string methodName, string message)
            : base(string.Format("Class: {0}\r\nMethod: {1}\r\nMessage:{2}", className, methodName, message))
        {

        }
    }
}
