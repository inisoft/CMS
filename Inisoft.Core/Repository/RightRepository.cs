using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface;
using Inisoft.Core.Object;
using Inisoft.Core.Object.Definition;
using Inisoft.Core.Provider;

namespace Inisoft.Core.Repository
{
    public class RightRepository : BaseRepository<Right>, IRightRepository
    {
        protected override ObjectDefinition ReadObjectDefinition()
        {
            return RightObjectDefinition.Get();
        }

        public override ObjectName ObjectName
        {
            get { return Const.Name.ObjectTypeName; }
        }
    }
}