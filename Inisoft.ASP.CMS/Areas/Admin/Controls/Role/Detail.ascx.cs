using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inisoft.ASP.CMS.Core;
using Inisoft.DAL.Interface;
using Inisoft.Core;
using Inisoft.ASP.CMS.Areas.Admin.Controls.General;
using Inisoft.Core.Interface;

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.Role
{
    public partial class Detail : BaseDetailControl<DAL.DTO.Role, IRoleRepository>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupControl();
        }

        protected override void SetupControl()
        {
            base.SetupControl();
        }
    }
}