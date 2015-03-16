using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Web
{
    public interface IApiController
    {
        IView InvokeMethod(string methodName, UrlContext urlContext);
    }
}
