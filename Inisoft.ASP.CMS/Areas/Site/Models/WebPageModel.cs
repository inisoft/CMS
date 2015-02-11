using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Inisoft.ASP.CMS.Areas.Site.Models
{
    public class SiteWebPageModel
    {
        public SiteWebPageModel()
        {
            MenuDataList = new List<MenuData>();
        }

        public string Title { get; set; }
        public string ViewName { get; set; }
        public string YellowPage { get; set; }

        public string HeadInlineJavaScript { get; set; }
        public string HeadInlineStyle { get; set; }
        public string HeadMetaData { get; set; }
        public string HeadShortcutIcon { get; set; }
        public string HeadMetaDescription { get; set; }
        public string HeadMetaKeyword { get; set; }

        public List<MenuData> MenuDataList { get; set; }
    }

    public class MenuData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
    }
}