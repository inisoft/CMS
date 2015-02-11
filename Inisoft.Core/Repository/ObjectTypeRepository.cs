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
    public class ObjectTypeRepository : BaseRepository<ObjectType>, IObjectTypeRepository
    {
        protected override ObjectDefinition ReadObjectDefinition()
        {
            return SystemObjectDefinition.ObjectDefinition_ObjectType();
        }

        public override ObjectName ObjectName
        {
            get { return Const.Name.ObjectTypeName; }
        }

        public ObjectType Get(ObjectName objectName)
        {
            return storageProvider.Select<ObjectType>(ObjectDefinition).Where(x => x.CodeName == objectName.CodeName).FirstOrDefault();
        }


        public MethodResult<ObjectType> Save(ObjectDefinition objectDefinition, User authUser)
        {
            ObjectType objectType = Get(objectDefinition.ObjectName);
            if (objectType == null)
            {
                objectType = new ObjectType();
                objectType.Name = objectDefinition.ObjectName.FullName;
                objectType.CodeName = objectDefinition.ObjectName.CodeName;
            }
            objectType.Definition = JsonConvert.SerializeObject(objectDefinition);

            return this.Save(objectType, authUser);
        }
    }
}