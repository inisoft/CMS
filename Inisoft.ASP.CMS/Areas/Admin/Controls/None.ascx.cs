using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inisoft.ASP.CMS.Core;

namespace Inisoft.ASP.CMS.Areas.Admin.Controls
{
    public partial class None : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override string HeadTitle
        {
            get
            {
                return "Administracja";
            }
        }
    }
}