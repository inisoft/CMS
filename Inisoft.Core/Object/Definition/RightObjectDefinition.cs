using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Storage;
using Inisoft.Core.Interface.Storage;

namespace Inisoft.Core.Object.Definition
{
    public static class RightObjectDefinition
    {
        public static readonly Inisoft.Core.ObjectName Name = Const.Name.RightName;

        public static ObjectDefinition Get()
        {
            ObjectDefinition result = new ObjectDefinition();
            result.ObjectName.Name = Const.Name.RightName.Name;
            result.ObjectName.Namespace = Const.Name.RightName.Namespace;
            result.Title = Const.Name.RightName.Name;
            result.Properties.Add(new PropertyDefinition() { Name = "Name", Title = "Name", PropertyType = PropertyTypeEnum.Text, MaxLength = 50, IsRequired = true, PropertyDisplayFlag = PropertyDisplayFlagEnum.Details | PropertyDisplayFlagEnum.Grid });
            result.Properties.Add(new PropertyDefinition() { Name = "CodeName", Title = "Code Name", PropertyType = PropertyTypeEnum.Text, MaxLength = 50, IsRequired = true, PropertyDisplayFlag = PropertyDisplayFlagEnum.Details | PropertyDisplayFlagEnum.Grid });
            result.Properties.Add(new PropertyDefinition() { Name = "Description", Title = "Description", PropertyType = PropertyTypeEnum.LongText });
            return result;
        }

        public static class QueryDefinition
        {
            /*
            public class GetByCodeName : StorageQueryStoredProcedure, IStorageQuery
            {
                public GetByCodeName(string codeName)
                {
                    AddParam("CodeName", codeName);
                }

                public override string QueryText
                {
                    get { return "Proc_Admin_Role_GetByCodeName"; }
                }
            }
             */
        }
    }

}
