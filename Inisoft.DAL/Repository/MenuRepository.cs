using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Provider;
using Inisoft.DAL.Interface;
using Inisoft.DAL.DTO;
using Inisoft.DAL.DTO.Definition;
using Inisoft.Core;

namespace Inisoft.DAL.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public override Core.ObjectName ObjectName
        {
            get { return MenuObjectDefinition.Name; }
        }

        public MethodResult<IList<Menu>> Get(Core.Object.User authUser)
        {
            MethodResult<IList<Menu>> result = new MethodResult<IList<Menu>>();
            result.Data = storageProvider.Select<Menu>(ObjectDefinition).ByQuery(new MenuObjectDefinition.QueryDefinition.GetByUser(authUser.Id)).ToList();
            return result;
        }

        public MethodResult<Menu> GetByUrl(string url)
        {
            MethodResult<Menu> result = new MethodResult<Menu>();
            result.Data = storageProvider.Select<Menu>(ObjectDefinition).ByQuery(new MenuObjectDefinition.QueryDefinition.GetByUrl(url)).FirstOrDefault();
            return result;
        }
    }
}