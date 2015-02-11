using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core.Extension
{
    public static class ObjectDefinitionExtension
    {
        public static List<PropertyDefinition> GetSystemProperties(this ObjectDefinition objectDefinition)
        {
            List<PropertyDefinition> result = new List<PropertyDefinition>();
            result.Add(new PropertyDefinition() { Name = "Id", Title = "Id", PropertyType = PropertyTypeEnum.Number, IsRequired = true, IsPK = true });
            result.Add(new PropertyDefinition() { Name = "CreateDate", Title = "CreateDate", PropertyType = PropertyTypeEnum.DateTime, IsRequired = true });
            result.Add(new PropertyDefinition() { Name = "CreateUser", Title = "CreateUser", PropertyType = PropertyTypeEnum.Text, MaxLength = 100, IsRequired = true });
            result.Add(new PropertyDefinition() { Name = "CreateUserId", Title = "CreateUserId", PropertyType = PropertyTypeEnum.Number });
            result.Add(new PropertyDefinition() { Name = "UpdateDate", Title = "UpdateDate", PropertyType = PropertyTypeEnum.DateTime });
            result.Add(new PropertyDefinition() { Name = "UpdateUser", Title = "UpdateUser", PropertyType = PropertyTypeEnum.Text, MaxLength = 100 });
            result.Add(new PropertyDefinition() { Name = "UpdateUserId", Title = "UpdateUserId", PropertyType = PropertyTypeEnum.Number });
            result.Add(new PropertyDefinition() { Name = "Version", Title = "Version", PropertyType = PropertyTypeEnum.Number });
            return result;
        }
    }
}
