using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    internal partial class SystemObjectDefinition
    {
        public static IList<ObjectDefinition> GetAll()
        {
            List<ObjectDefinition> systemObjects = new List<ObjectDefinition>();
            systemObjects.Add(ObjectDefinition_User());
            systemObjects.Add(ObjectDefinition_ObjectType());
            systemObjects.Add(ObjectDefinition_Right());

            return systemObjects;
        }
    }
}