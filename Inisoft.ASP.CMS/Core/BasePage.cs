﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inisoft.Web;

namespace Inisoft.ASP.CMS.Core
{
    public class BasePage : System.Web.UI.Page
    {

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
    }
}