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

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.Menu
{
    public partial class NavbarDefault : BaseAdminControl<List<NavbarDefaultModel>>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IMenuRepository menuRepository = RepositoryServiceLocator.Get<IMenuRepository>();

            this.Model = menuRepository.Get(UserContext.AuthenticatedUser).Data.Select(x => x.ToModel()).ToList();
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