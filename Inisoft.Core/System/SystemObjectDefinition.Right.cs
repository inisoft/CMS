using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    internal partial class SystemObjectDefinition
	{
        internal static ObjectDefinition ObjectDefinition_Right()
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
	}
}