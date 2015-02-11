using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core;
using Inisoft.Core.Interface.Storage;
using Inisoft.Core.Storage;

namespace Inisoft.DAL.DTO.Definition
{
    public static class MenuObjectDefinition
    {
        public static readonly Inisoft.Core.ObjectName Name = new ObjectName() { Name = "Menu", Namespace = "Admin" };

        public static ObjectDefinition Get()
        {
            ObjectDefinition result = new ObjectDefinition();
            result.ObjectName.Name = Name.Name;
            result.ObjectName.Namespace = Name.Namespace;
            result.Title = Name.Name;
            result.Properties.Add(new PropertyDefinition() { Name = "ParentMenuId", Title = "Parent Menu", PropertyType = PropertyTypeEnum.Number });
            result.Properties.Add(new PropertyDefinition() { Name = "Title", Title = "Title", PropertyType = PropertyTypeEnum.Text, MaxLength = 50, IsRequired = true });
            result.Properties.Add(new PropertyDefinition() { Name = "Url", Title = "Url", PropertyType = PropertyTypeEnum.Text, MaxLength = 250 });
            result.Properties.Add(new PropertyDefinition() { Name = "CssClass", Title = "CssClass", PropertyType = PropertyTypeEnum.Text, MaxLength = 250 });
            result.Properties.Add(new PropertyDefinition() { Name = "MenuBar", Title = "MenuBar", PropertyType = PropertyTypeEnum.Text, MaxLength = 250 });
            return result;
        }

        public static class QueryDefinition
        {
            public class GetByUser : StorageQueryStoredProcedure, IStorageQuery
            {
                public GetByUser(int userID)
                {
                    AddParam("UserID", userID);
                }

                public override string QueryText
                {
                    get { return "Proc_Admin_Menu_GetByUser"; }
                }
            }
        }
    }
}