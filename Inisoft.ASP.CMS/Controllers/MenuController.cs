using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inisoft.Web;
using Inisoft.DAL.Interface;
using Inisoft.Core;
using Newtonsoft.Json;

namespace Inisoft.ASP.CMS.Controllers
{
    public class MenuController : ApiController
    {

        public JsonView GetMenu()
        {
            IMenuRepository repository = RepositoryServiceLocator.Get<IMenuRepository>();
            var model = repository.Get().Data;

            return new JsonView(JsonConvert.SerializeObject(model));
        }
    }
}