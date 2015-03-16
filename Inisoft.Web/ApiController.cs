using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Inisoft.Web
{
    public abstract class ApiController : IApiController
    {
        public IView InvokeMethod(string methodName, UrlContext urlContext)
        {
            methodName = methodName.ToLower();

            MethodInfo method = this.GetType().GetMethods().FirstOrDefault(x => x.IsPublic && x.Name.ToLower() == methodName);
            if (method == null)
            {
                throw new Exception(string.Format("ApiController method not found: {0}.{1}", this.GetType().Name, methodName));
            }

            IView viewResult = method.Invoke(this, null) as IView;
            if (viewResult == null)
            {
            }
            return viewResult;
        }
    }
}
