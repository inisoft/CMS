using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Attribute;
using Inisoft.Core.Object;
using Inisoft.Core.Interface;
using Newtonsoft.Json;

namespace Inisoft.Core.Provider
{
    public class RightRepository : BaseRepository<Right>, IRightRepository
    {
        protected override ObjectDefinition ReadObjectDefinition()
        {
            return SystemObjectDefinition.ObjectDefinition_Right();
        }

        public override ObjectName ObjectName
        {
            get { return Const.Name.ObjectTypeName; }
        }
    }
}