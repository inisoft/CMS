using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Object;
using System.Web;
using Inisoft.Core;
using Inisoft.Core.Interface;

namespace Inisoft.Web.Membership
{
    public static class UserContext
    {
        private static string lastUserName = null;
        private static User authenticatedUser = null;
        public static User AuthenticatedUser
        {
            get
            {
                if (authenticatedUser == null)
                {
                    authenticatedUser = Engine.DefaultUser;
                }
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null )
                {
                    if (lastUserName != HttpContext.Current.User.Identity.Name)
                    {
                        IUserRepository provider = RepositoryServiceLocator.Get<IUserRepository>();
                        authenticatedUser = provider.Get(HttpContext.Current.User.Identity.Name, string.Empty).Data;
                        lastUserName = HttpContext.Current.User.Identity.Name;
                    }
                }
                return authenticatedUser;
            }
        }
    }
}