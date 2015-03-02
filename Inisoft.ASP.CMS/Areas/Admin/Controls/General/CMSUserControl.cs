using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inisoft.Web;

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.General
{
    public class CMSUserControl : System.Web.UI.UserControl
    {
        private UrlContext _URLContext = null;
        public UrlContext UrlContext
        {
            get
            {
                if (_URLContext == null)
                {
                    _URLContext = UrlHelper.GetUrlContext();
                }
                return _URLContext;
            }
        }
    }
}