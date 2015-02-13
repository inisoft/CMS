using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Provider;

namespace Inisoft.DAL.DTO
{
    public class Role : GenericObject
    {
        public string Name { get { return GetStringValue("Name"); } set { SetValue("Name", value); } }
        public string CodeName { get { return GetStringValue("CodeName"); } set { SetValue("CodeName", value); } }
        public string Description { get { return GetStringValue("Description"); } set { SetValue("Description", value); } }
    }
}