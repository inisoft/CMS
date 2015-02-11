using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Interface;
using Inisoft.Core.Object;

namespace Inisoft.Core.Helpers
{
    public static class UserHelper 
    {
        private static IUserRepository userProvider = null;
        public static IUserRepository Provider
        {
            get
            {
                if (userProvider == null)
                {
                    userProvider = RepositoryServiceLocator.Get<IUserRepository>();
                }
                return userProvider;
            }
        }

        public static MethodResult<User> Get(string userName, string applicationName)
        {
            return Provider.Get(userName, applicationName);
        }

        public static MethodResult<User> GetByEmail(string email, string applicationName)
        {
            return Provider.Get(email, applicationName);
        }

        public static MethodResult<IList<User>> Get(string applicationName)
        {
            return Provider.Get(applicationName);
        }

        public static MethodResult Delete(User user, User authUser)
        {
            return Provider.Delete(user, authUser);
        }

        public static MethodResult<User> Save(User user, User authUser)
        {
            return Provider.Save(user, authUser);
        }

    }
}