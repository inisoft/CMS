using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Provider;

namespace Inisoft.Core.Extension
{
    public static class GenericObjectExtension
    {
        public static void MarkModified(this GenericObject genericObject)
        {
            genericObject.State = GenericObjectState.Modified;
        }

        public static void MarkNotModified(this GenericObject genericObject)
        {
            genericObject.State = GenericObjectState.NotModified;
        }
    }
}