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

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.User
{
    public partial class Detail : BaseDetailControl<Inisoft.Core.Object.User, IUserRepository>
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