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

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.Right
{
    public partial class Detail : BaseDetailControl<Inisoft.Core.Object.Right, IRightRepository>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupControl();
        }

        protected override void SetupControl()
        {
            base.SetupControl();

            if (this.Model != null)
            {
                Grid grid = Page.LoadControl("Controls/General/Grid.ascx") as Grid;
                this.Controls.Add(grid);
                IUserRepository userRepository = RepositoryServiceLocator.Get<IUserRepository>();

                grid.TableHeader = "Lista użytkowników";
                grid.ObjectDefinition = userRepository.ObjectDefinition;
                grid.SetData(userRepository.Get(this.Model).Data);
            }
        }
    }
}