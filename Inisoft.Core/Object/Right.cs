using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Provider;

namespace Inisoft.Core.Object
{
    public class Right : GenericObject
    {
        public static readonly Right Run = new Right() { Name = "Run", CodeName = "Run", Description = "Allow run" };
        public static readonly Right Edit = new Right() { Name = "Edit", CodeName = "Edit", Description = "Allow edit" };
        public static readonly Right Delete = new Right() { Name = "Delete", CodeName = "Delete", Description = "Allow delete" };

        public static readonly Right[] All = new Right[] { Run, Edit, Delete };

        public string Name { get { return GetStringValue("Name"); } set { SetValue("Name", value); } }
        public string CodeName { get { return GetStringValue("CodeName"); } set { SetValue("CodeName", value); } }
        public string Description { get { return GetStringValue("Description"); } set { SetValue("Description", value); } }
    }
}