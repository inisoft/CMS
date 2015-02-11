using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inisoft.ASP.CMS.Areas.Admin.Services;
using System.Web.Security;

namespace Inisoft.ASP.CMS.Areas.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoginUser.DestinationPageUrl = this.Request.QueryString["ReturnUrl"];
            this.LoginUser.LoggingIn += new LoginCancelEventHandler(LoginUser_LoggingIn);
        }

        void LoginUser_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            e.Cancel = true;
            IMembershipService MembershipService = new AccountMembershipService();
            if (this.LoginUser.DestinationPageUrl.Contains("register"))
            {
                MembershipService.CreateUser(this.LoginUser.UserName, this.LoginUser.Password, this.LoginUser.UserName);
            }
            if (MembershipService.ValidateUser(this.LoginUser.UserName, this.LoginUser.Password))
            {
                IFormsAuthenticationService FormsService = new FormsAuthenticationService();
                FormsService.SignIn(this.LoginUser.UserName, this.LoginUser.RememberMeSet);
                if (!String.IsNullOrEmpty(this.LoginUser.DestinationPageUrl))
                {
                    this.Response.Redirect(this.LoginUser.DestinationPageUrl, true);
                }
                else if (!String.IsNullOrEmpty(FormsAuthentication.DefaultUrl))
                {
                    this.Response.Redirect(FormsAuthentication.DefaultUrl, true);
                }
                else
                {
                    this.Response.Redirect("~/", true);
                }
            }
            else
            {
                this.Response.Redirect("~/", true);
            }
        }
    }
}