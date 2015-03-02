using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Inisoft.Core.Provider;

namespace Inisoft.Web
{
    public static class UrlHelper
    {
        private const string Key_ObjectID = "id";

        public static int GetObjectID()
        {
            string value = HttpContext.Current.Request.QueryString[Key_ObjectID];
            if (!string.IsNullOrEmpty(value))
            {
                int result = 0;
                if (int.TryParse(value, out result))
                {
                    return result;
                }
            }
            else
            {
                UrlContext context = GetUrlContext();
                if (context != null && context.Action == "detail" && context.Params.Length > 0)
                {
                    int result = 0;
                    if (int.TryParse(context.Params[0], out result))
                    {
                        return result;
                    }
                }
            }
            return 0;
        }

        public static UrlContext GetUrlContext()
        {
            return UrlContext.Get(HttpContext.Current.Request);
        }

        public static string GetDetailUrl(GenericObject genericObject)
        {
            return GetDetailUrl(GetUrlContext(), genericObject);
        }

        public static string GetDetailUrl(UrlContext urlContext, GenericObject genericObject)
        {
            return string.Format("/{0}/{1}/detail/{2}", urlContext.Area, urlContext.View, genericObject.Id);
        }

        public static string GetEditUrl(UrlContext urlContext, GenericObject genericObject)
        {
            return string.Format("/{0}/{1}/edit/{2}", urlContext.Area, urlContext.View, genericObject.Id);
        }

        public static string GetListUrl(UrlContext urlContext)
        {
            return string.Format("/{0}/{1}", urlContext.Area, urlContext.View);
        }
    }
}