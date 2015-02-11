using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    public static class PropertyTypeHelper
    {

        public static PropertyTypeEnum GetPropertyTypeEnum(object value)
        {
            if (value == null || value == System.DBNull.Value || value is string)
            {
                return PropertyTypeEnum.Text;
            }

            if (value is int)
            {
                return PropertyTypeEnum.Number;
            }

            if (value is DateTime)
            {
                return PropertyTypeEnum.DateTime;
            }

            if (value is bool)
            {
                return PropertyTypeEnum.Boolean;
            }

            if (value is decimal || value is float || value is Single)
            {
                return PropertyTypeEnum.Decimal;
            }

            return PropertyTypeEnum.Text;
        }
    }
}