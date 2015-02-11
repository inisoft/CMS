using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Object;

namespace Inisoft.Core.Interface
{
    public interface IObjectTypeRepository : IBaseRepository<ObjectType>
    {
        ObjectType Get(ObjectName objectName);

        MethodResult<ObjectType> Save(ObjectDefinition objectDefinition, User authUser);
    }
}
