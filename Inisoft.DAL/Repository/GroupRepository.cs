using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Provider;
using Inisoft.DAL.Interface;
using Inisoft.DAL.DTO;
using Inisoft.DAL.DTO.Definition;
using Inisoft.Core;

namespace Inisoft.DAL.Repository
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public override Core.ObjectName ObjectName
        {
            get { return GroupObjectDefinition.Name; }
        }
    }
}