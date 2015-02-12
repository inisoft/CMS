using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    public class PropertyDefinition
    {
        public PropertyDefinition()
        {
            PropertyType = PropertyTypeEnum.Text;
            PropertyDisplayFlag = PropertyDisplayFlagEnum.Details;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public PropertyTypeEnum PropertyType { get; set; }
        public string EditorControlName { get; set; }
        public int MaxLength { get; set; }
        public object DefaultValue { get; set; }
        public bool IsRequired { get; set; }
        public bool IsPK { get; set; }

        public PropertyDisplayFlagEnum PropertyDisplayFlag { get; set; }

        public bool CheckDisplayFlag(PropertyDisplayFlagEnum propertyDisplayFlag)
        {
            return (PropertyDisplayFlag & propertyDisplayFlag) == propertyDisplayFlag;
        }
    }
}
