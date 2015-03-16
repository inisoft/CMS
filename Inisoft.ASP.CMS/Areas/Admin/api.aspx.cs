using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Inisoft.Web;
using Inisoft.Core;
using Inisoft.Web.Membership;

namespace Inisoft.ASP.CMS.Areas.Admin
{
    public partial class api : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RenderPage();
        }

        private UrlContext _URLContext = null;
        public UrlContext UrlContext
        {
            get
            {
                if (_URLContext == null)
                {
                    _URLContext = UrlContext.Get(this.Request);
                }
                return _URLContext;
            }
        }

        protected void RedirectWithCode(string redirectUrl, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                Response.Redirect(redirectUrl, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Response.Clear();
                Response.StatusCode = (int)statusCode;
                switch (statusCode)
                {
                    case HttpStatusCode.MovedPermanently:
                        Response.AddHeader("Location", redirectUrl);
                        Response.Write("<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML 2.0//EN\">" + Environment.NewLine);
                        Response.Write("<HTML><HEAD>" + Environment.NewLine);
                        Response.Write("<TITLE>301 Moved Permanently</TITLE>" + Environment.NewLine);
                        Response.Write("</HEAD><BODY>" + Environment.NewLine);
                        Response.Write("<H1>Moved Permanently</H1>" + Environment.NewLine);
                        Response.Write("The document has moved <A HREF=\"" + redirectUrl + "\">here</A>.<P>" + Environment.NewLine);
                        Response.Write("</BODY></HTML>" + Environment.NewLine);
                        Response.Flush();
                        break;
                    case HttpStatusCode.Redirect:
                    case HttpStatusCode.RedirectMethod:
                        Response.AddHeader("Location", redirectUrl);
                        break;
                    case HttpStatusCode.Gone:
                        RenderPage();
                        break;
                }
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void RenderError(int errorID, Exception exception)
        {
            // TODO
            // throw new Exception("Error occured.", exception);
        }

        private IApiController GetApiController()
        {
            string controllerTypeName = string.Format("{0}Controller", UrlContext.View).ToLower();
            IEnumerable<Type> allApiControllers = ReflectionUtils.GetTypeCollectionForBaseType(typeof(IApiController));
            Type apiControllerType = allApiControllers.Where(x => x.IsClass && !x.IsAbstract && x.Name.ToLower() == controllerTypeName).FirstOrDefault();
            if (apiControllerType != default(Type))
            {
                return (IApiController)Activator.CreateInstance(apiControllerType);
            }
            return null;
        }

        private void RenderPage()
        {
            if (UserContext.AuthenticatedUser == null || UserContext.AuthenticatedUser.Id == Engine.DefaultUser.Id)
            {
                throw new Exception("Authentication required.");
            }

            if (IsPostBack)
            {
                // post method
            }
            else
            {
                // get method
            }

            IApiController controller = GetApiController();
            if (controller == null)
            {
                throw new Exception(string.Format("Cannot find controller {0}", UrlContext.View));
            }

            Response.Write(controller.InvokeMethod(UrlContext.Action, UrlContext).GetView());
            Context.ApplicationInstance.CompleteRequest();
        }

    }
}