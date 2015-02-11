using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inisoft.ASP.CMS.Core;
using Inisoft.ASP.CMS.Areas.Site.Models;
using System.Security.Permissions;
using Inisoft.Core;
using Inisoft.Core.Object;
using Inisoft.Core.Interface;

namespace Inisoft.ASP.CMS.Areas.Site
{
    public partial class Index : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //string sql = StorageProviderContainer.Create(Const.DEFAULT_STORAGE_PROVIDER_NAME).Select<User>(ProviderServiceLocator.Get<IUserProvider>().ObjectDefinition).Where(x => (x.Email == "ABC") && ((x.FirstName.StartsWith("s") || string.IsNullOrEmpty(x.Comment)))).GetQuery();


            Controllers.HomeController _controller = new Controllers.HomeController(this.Context);
            Model = _controller.Index();
            ///Model.YellowPage = sql;

            Site.Controls.Menu _menuControl = Page.LoadControl("Controls/Menu.ascx") as Site.Controls.Menu;
            _menuControl.Model = this.Model;
            menuContainer.Controls.Add(_menuControl);

            //BaseViewControl<SiteWebPageModel> _modelControl = Page.LoadControl(string.Format("Controls/{0}.ascx", Model.ViewName)) as BaseViewControl<SiteWebPageModel>;
            //_modelControl.Model = this.Model;
            //contentContainer.Controls.Add(_modelControl);
        }

        public SiteWebPageModel Model { get; private set; }

        public string RewritePath
        {
            get { return this.Request.QueryString["path"]; }
        }
    }
}