using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Object;
using Inisoft.Core.Attribute;
using Inisoft.Core.Interface;

namespace Inisoft.Core.Provider
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public override ObjectName ObjectName
        {
            get { return Const.Name.UserObjectName; }
        }

        public MethodResult<User> Get(string userName, string applicationName)
        {
            if (applicationName == null)
            {
                applicationName = string.Empty;
            }
            User user = storageProvider.Select<User>(ObjectDefinition).Where(x => x.Email == userName && x.ApplicationName == applicationName).FirstOrDefault();
            return new MethodResult<User>() { Data = user };
        }

        public MethodResult<User> GetByEmail(string email, string applicationName)
        {
            return Get(email, applicationName);
        }

        public MethodResult<IList<User>> Get(string applicationName)
        {
            return new MethodResult<IList<User>>() { Data = storageProvider.Select<User>(ObjectDefinition).ToList() };
        }
        
        protected override MethodResult<User> DoSave(User user, User authUser)
        {
            MethodResult<User> existingUser = Get(user.Email, user.ApplicationName);
            if (user.IsNew)
            {
                if (existingUser.Success && existingUser.Data != null)
                {
                    return new MethodResult<User>() { Success = false, Message = "Cannot create user. User with email already exists.", Data = user };
                }
            }
            else
            {
                if (existingUser.Success && existingUser.Data != null)
                {
                    if (user.Id != existingUser.Data.Id)
                    {
                        return new MethodResult<User>() { Success = false, Message = "Cannot update user. User with email already exists.", Data = user };
                    }
                }
            }
            return base.DoSave(user, authUser);
        }

    }
}