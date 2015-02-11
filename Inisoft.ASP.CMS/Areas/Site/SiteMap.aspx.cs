using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inisoft.ASP.CMS.Areas.Site.Controllers;

namespace Inisoft.ASP.CMS.Areas.Site
{
    public partial class SiteMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMapController _controller = new SiteMapController(this.Context);
            _controller.DoResponse();
        }
    }
}