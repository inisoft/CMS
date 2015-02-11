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

            IList<DAL.DTO.Menu> menuList = menuRepository.Get(UserContext.AuthenticatedUser).Data;

            this.Model = menuList.Select(x => x.ToModel()).ToList();

            DAL.DTO.Menu menuAdministracja = menuList.Where(x => x.Title == "Adminsitracja").FirstOrDefault();
            if (menuAdministracja == null)
            {
                menuAdministracja = new DAL.DTO.Menu()
                {
                    CssClass = "fa-gear",
                    MenuBar = "default",
                    Title = "Adminsitracja"
                };
                menuRepository.Save(menuAdministracja, UserContext.AuthenticatedUser);
            }

            DAL.DTO.Menu menuUser = menuList.Where(x => x.Title == "Użytkownicy").FirstOrDefault();
            if (menuUser == null)
            {
                menuUser = new DAL.DTO.Menu();
            }
            menuUser.CssClass = "fa-user";
            menuUser.MenuBar = "default";
            menuUser.Title = "Użytkownicy";
            menuUser.Url = "/admin/users";
            menuUser.ApplicationPath = "Controls/User";
            menuUser.ParentMenuId = menuAdministracja.Id;
            menuRepository.Save(menuUser, UserContext.AuthenticatedUser);

            DAL.DTO.Menu menuMenu = menuList.Where(x => x.Title == "Menu").FirstOrDefault();
            if (menuMenu == null)
            {
                menuMenu = new DAL.DTO.Menu();
            }
            menuMenu.MenuBar = "default";
            menuMenu.Title = "Menu";
            menuMenu.Url = "/admin/menu";
            menuUser.ApplicationPath = "Controls/Menu";
            menuMenu.ParentMenuId = menuAdministracja.Id;
            menuMenu.CssClass = "fa-th-list";
            menuRepository.Save(menuMenu, UserContext.AuthenticatedUser);
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