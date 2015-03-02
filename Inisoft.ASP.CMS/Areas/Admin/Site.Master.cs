using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Inisoft.ASP.CMS.Core;
using Inisoft.Web;

namespace Inisoft.ASP.CMS
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UrlContext _urlContext = UrlContext.Get(this.Request);
            mainForm.Action = _urlContext.OryginalURL;
        }

        private string LoginURL
        {
            get { return FormsAuthentication.LoginUrl; }
        }
    }
}
