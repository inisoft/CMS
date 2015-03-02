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
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public override Core.ObjectName ObjectName
        {
            get { return GroupObjectDefinition.Name; }
        }
    }
}
