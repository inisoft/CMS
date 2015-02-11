using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Inisoft.ASP.CMS.Core;
using Inisoft.ASP.CMS.Core.Extension;

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
            string _controlName = "Dashboard";
            string _pathPart = string.Empty;
            string _controlPart = string.Empty;
            switch (URLContext.View)
            {
                case "":
                case "index":
                    _controlName = "None";
                    break;
                case "website":
                    _pathPart = "WebSite";
                    _controlPart = URLContext.Action;
                    break;
                case "webpage":
                    _pathPart = "WebPage";
                    _controlPart = URLContext.Action;
                    break;
                case "article":
                    _pathPart = "Article";
                    _controlPart = URLContext.Action;
                    break;
                case "menu":
                    _pathPart = "Menu";
                    _controlPart = URLContext.Action;
                    break;
            }
            if (!string.IsNullOrEmpty(_controlPart))
            {
                _controlName = string.Format("{0}/{1}", _pathPart, _controlPart.UppercaseFirst());
            }

            BaseControl _control = Page.LoadControl(string.Format("Controls/{0}.ascx", _controlName)) as BaseControl;
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
