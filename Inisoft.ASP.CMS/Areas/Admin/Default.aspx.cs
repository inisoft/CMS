using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Inisoft.ASP.CMS.Core;
using Inisoft.ASP.CMS.Core.Extension;
using Inisoft.DAL.Interface;
using Inisoft.Core;

namespace Inisoft.ASP.CMS
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            NavbarTopLinksPlaceHolder.Controls.Add(Page.LoadControl("Controls/Menu/NavbarTop.ascx"));
            NavbarDefaultContainer.Controls.Add(Page.LoadControl("Controls/Menu/NavbarDefault.ascx"));
            LoadViewControl();
        }

        private void LoadViewControl()
        {
            string _controlName = "Controls/Dashboard.ascx";

            IMenuRepository menuRepository = RepositoryServiceLocator.Get<IMenuRepository>();
            DAL.DTO.Menu menu = menuRepository.GetByUrl(string.Format("/{0}/{1}", URLContext.Area, URLContext.View)).Data;
            if (menu != null && !string.IsNullOrEmpty(menu.ApplicationPath))
            {
                _controlName = string.Format("{0}/{1}.ascx", menu.ApplicationPath, string.IsNullOrEmpty(URLContext.Action) ? "List" : URLContext.Action.UppercaseFirst());
            }

            BaseControl _control = Page.LoadControl(_controlName) as BaseControl;
            if (_control != null)
            {
                ControlTitle = _control.Title;
                ControlHeadTitle = _control.HeadTitle;
                this.contentContainer.Controls.Add(_control);
            }
        }

        protected string ControlTitle { get; set; }
        protected string ControlHeadTitle { get; set; }
    }
}
