using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Provider;
using Inisoft.Core.Attribute;

namespace Inisoft.Core.Object
{
    [ObjectTypeAttribute(Const.Namespace.System, Const.Name.ObjectType)]
    public class ObjectType : GenericObject
    {
        public string Name { get { return GetStringValue("Name"); } set { SetValue("Name", value); } }
        public string CodeName { get { return GetStringValue("CodeName"); } set { SetValue("CodeName", value); } }
        public string Description { get { return GetStringValue("Description"); } set { SetValue("Description", value); } }
        public string Definition { get { return GetStringValue("Definition"); } set { SetValue("Definition", value); } }
    }
}