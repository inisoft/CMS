using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inisoft.ASP.CMS.Core
{
    public class BasePage : System.Web.UI.Page
    {

        private URLContext _URLContext = null;
        public URLContext URLContext
        {
            get
            {
                if (_URLContext == null)
                {
                    _URLContext = URLContext.Get(this.Request);
                }
                return _URLContext;
            }
        }
    }
}