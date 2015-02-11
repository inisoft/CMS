using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Provider;

namespace Inisoft.DAL.DTO
{
    public class Menu : GenericObject
    {
        public int ParentMenuId { get { return GetIntValue("ParentMenuId"); } set { SetValue("ParentMenuId", value); } }
        public string Title { get { return GetStringValue("Title"); } set { SetValue("Title", value); } }
        public string Url { get { return GetStringValue("Url"); } set { SetValue("Url", value); } }
        public string CssClass { get { return GetStringValue("CssClass"); } set { SetValue("CssClass", value); } }
        public string MenuBar { get { return GetStringValue("MenuBar"); } set { SetValue("MenuBar", value); } }
        public string ApplicationPath { get { return GetStringValue("ApplicationPath"); } set { SetValue("ApplicationPath", value); } }
    }
}