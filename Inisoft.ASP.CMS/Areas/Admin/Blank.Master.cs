using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inisoft.ASP.CMS.Core;
using System.Web.Security;

namespace Inisoft.ASP.CMS.Areas.Admin
{
    public partial class Blank : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            URLContext _urlContext = URLContext.Get(this.Request);
            mainForm.Action = _urlContext.OryginalURL;
        }

        private string LoginURL
        {
            get { return FormsAuthentication.LoginUrl; }
        }
    }
}