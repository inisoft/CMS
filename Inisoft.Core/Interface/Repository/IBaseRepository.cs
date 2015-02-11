using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Provider;

using Inisoft.Core.Object;

namespace Inisoft.Core.Interface
{
    public interface IBaseRepository
    {
        ObjectName ObjectName { get; }
        ObjectDefinition ObjectDefinition { get; }
    }

    public interface IBaseRepository<TGenericObject> : IBaseRepository
        where TGenericObject : GenericObject, new()
    {
        MethodResult<TGenericObject> Get(int id);
        MethodResult<TGenericObject> Save(TGenericObject obj, User authUser);
        MethodResult Delete(TGenericObject obj, User authUser);
    }
}