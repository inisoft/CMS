using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface;
using Inisoft.DAL.DTO;
using Inisoft.Core;
using Inisoft.Core.Object;

namespace Inisoft.DAL.Interface
{
    public interface IMenuRepository : IBaseRepository<Menu>
    {
        /// <summary>
        /// Lista dostępnego menu dla uzutkownika
        /// </summary>
        /// <param name="authUser"></param>
        /// <returns></returns>
        MethodResult<IList<Menu>> Get(User authUser);

        /// <summary>
        /// Menu dla danego adresu URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        MethodResult<Menu> GetByUrl(string url);
    }
}
