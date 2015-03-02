using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inisoft.ASP.CMS.Core;
using Inisoft.ASP.CMS.Areas.Admin.Models.Menu;
using Inisoft.DAL.Interface;
using Inisoft.Core;
using Inisoft.Web.Membership;
using Inisoft.Web.Data;

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.Menu
{
    public partial class NavbarDefault : BaseAdminControl<List<NavbarDefaultModel>>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuInitializer.CreateData();

            IMenuRepository menuRepository = RepositoryServiceLocator.Get<IMenuRepository>();

            IList<DAL.DTO.Menu> menuList = menuRepository.Get(UserContext.AuthenticatedUser).Data;

            this.Model = menuList.Select(x => x.ToModel()).ToList();
        }

        protected override void OnSCMSEvaluateModelFromPostback()
        {
            //nothing;
        }

        protected override void OnSCMSOnPreRender()
        {
            //nothing;
        }
    }
}