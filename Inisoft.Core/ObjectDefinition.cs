using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    public class ObjectDefinition
    {
        public ObjectDefinition()
        {
            ObjectName = new Core.ObjectName();
            Properties = new List<PropertyDefinition>();
        }

        public ObjectName ObjectName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public List<PropertyDefinition> Properties { get; set; }

        public List<PropertyDefinition> GetProperties(PropertyDisplayFlagEnum propertyDisplayFlag)
        {
            return Properties.Where(x => x.CheckDisplayFlag(propertyDisplayFlag) == true).ToList();
        }
    }
}