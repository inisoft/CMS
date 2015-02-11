using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Object;

namespace Inisoft.Core.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        MethodResult<User> Get(string userName, string applicationName);
        MethodResult<User> GetByEmail(string email, string applicationName);
        MethodResult<IList<User>> Get(string applicationName);
    }
}