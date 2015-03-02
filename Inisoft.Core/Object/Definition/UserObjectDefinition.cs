using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Storage;
using Inisoft.Core.Interface.Storage;

namespace Inisoft.Core.Object.Definition
{
    public static class UserObjectDefinition
    {
        public static readonly Inisoft.Core.ObjectName Name = Const.Name.UserObjectName;

        public static ObjectDefinition Get()
        {
            ObjectDefinition result = new ObjectDefinition();
            result.ObjectName.Name = Const.Name.UserObjectName.Name;
            result.ObjectName.Namespace = Const.Name.UserObjectName.Namespace;
            result.Title = Const.Name.UserObjectName.Name;
            result.Properties.Add(new PropertyDefinition() { Name = "FirstName", Title = "First Name", PropertyType = PropertyTypeEnum.Text, MaxLength = 50, IsRequired = true, PropertyDisplayFlag = PropertyDisplayFlagEnum.Details | PropertyDisplayFlagEnum.Grid });
            result.Properties.Add(new PropertyDefinition() { Name = "LastName", Title = "Last Name", PropertyType = PropertyTypeEnum.Text, MaxLength = 50, IsRequired = true, PropertyDisplayFlag = PropertyDisplayFlagEnum.Details | PropertyDisplayFlagEnum.Grid });
            result.Properties.Add(new PropertyDefinition() { Name = "Email", Title = "Email", PropertyType = PropertyTypeEnum.Text, MaxLength = 150, IsRequired = true, PropertyDisplayFlag = PropertyDisplayFlagEnum.Details | PropertyDisplayFlagEnum.Grid });
            result.Properties.Add(new PropertyDefinition() { Name = "Password", Title = "Password", PropertyType = PropertyTypeEnum.Password });
            result.Properties.Add(new PropertyDefinition() { Name = "Nick", Title = "Nick", PropertyType = PropertyTypeEnum.Text, MaxLength = 50, PropertyDisplayFlag = PropertyDisplayFlagEnum.Details | PropertyDisplayFlagEnum.Grid });
            result.Properties.Add(new PropertyDefinition() { Name = "ApplicationName", Title = "Application Name", PropertyType = PropertyTypeEnum.Text, MaxLength = 50 });

            result.Properties.Add(new PropertyDefinition() { Name = "PasswordQuestion", Title = "PasswordQuestion", PropertyType = PropertyTypeEnum.LongText });
            result.Properties.Add(new PropertyDefinition() { Name = "PasswordAnswer", Title = "PasswordAnswer", PropertyType = PropertyTypeEnum.LongText });
            result.Properties.Add(new PropertyDefinition() { Name = "LastPasswordChangedDate", Title = "Last password changed date", PropertyType = PropertyTypeEnum.DateTime });

            result.Properties.Add(new PropertyDefinition() { Name = "IsApproved", Title = "Is approved", PropertyType = PropertyTypeEnum.Boolean });
            result.Properties.Add(new PropertyDefinition() { Name = "IsLockedOut", Title = "Is lockedOut", PropertyType = PropertyTypeEnum.Boolean });
            result.Properties.Add(new PropertyDefinition() { Name = "Comment", Title = "Comment", PropertyType = PropertyTypeEnum.LongText });
            result.Properties.Add(new PropertyDefinition() { Name = "LastLoginDate", Title = "Last login date", PropertyType = PropertyTypeEnum.DateTime, PropertyDisplayFlag = PropertyDisplayFlagEnum.Details | PropertyDisplayFlagEnum.Grid });
            result.Properties.Add(new PropertyDefinition() { Name = "LastLockedOutDate", Title = "Last locked out date", PropertyType = PropertyTypeEnum.DateTime });
            result.Properties.Add(new PropertyDefinition() { Name = "LastActivityDate", Title = "Last activity date", PropertyType = PropertyTypeEnum.DateTime });

            return result;
        }

        public static class QueryDefinition
        {
            public class GetByRight : StorageQueryStoredProcedure, IStorageQuery
            {
                public GetByRight(Right right)
                {
                    AddParam("RightId", right.Id);
                }

                public override string QueryText
                {
                    get { return "Proc_System_User_GetByRightId"; }
                }
            }

            public class GetByGroup : StorageQueryStoredProcedure, IStorageQuery
            {
                public GetByGroup(Group right)
                {
                    AddParam("RightId", right.Id);
                }

                public override string QueryText
                {
                    get { return "Proc_System_User_GetByRightId"; }
                }
            }
        }
    }

}
