using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.DAL.Interface;
using Inisoft.Core.Object;
using Inisoft.Core;
using Inisoft.Web.Membership;

namespace Inisoft.Web.Data
{
    public static class MenuInitializer
    {

        public static void CreateData()
        {
            IMenuRepository menuRepository = RepositoryServiceLocator.Get<IMenuRepository>();

            IList<DAL.DTO.Menu> menuList = menuRepository.Get(UserContext.AuthenticatedUser).Data;

            DAL.DTO.Menu menuAdministracja = EnsureExists(menuRepository, menuList, "Adminsitracja", string.Empty, string.Empty, "fa-gear", "default", 0);

            EnsureExists(menuRepository, menuList, "Użytkownicy", "/admin/users", "Controls/User", "fa-user", "default", menuAdministracja.Id);
            EnsureExists(menuRepository, menuList, "Menu", "/admin/menu", "Controls/Menu", "fa-th-list", "default", menuAdministracja.Id);
            EnsureExists(menuRepository, menuList, "Object Type", "/admin/objecttype", "Controls/ObjectType", "fa-table", "default", menuAdministracja.Id);
            EnsureExists(menuRepository, menuList, "Role", "/admin/role", "Controls/Role", "fa-group", "default", menuAdministracja.Id);
            EnsureExists(menuRepository, menuList, "Grupy", "/admin/group", "Controls/Group", "fa-group", "default", menuAdministracja.Id);
            EnsureExists(menuRepository, menuList, "Prawa", "/admin/right", "Controls/Right", "fa-user", "default", menuAdministracja.Id);
        }

        private static DAL.DTO.Menu EnsureExists(
            IMenuRepository menuRepository,
            IList<DAL.DTO.Menu> menuList,
            string title,
            string url,
            string appPath,
            string css,
            string menuBar,
            int parentId)
        {
            DAL.DTO.Menu menu = menuList.Where(x => x.Title == title).FirstOrDefault();
            if (menu == null)
            {
                menu = new DAL.DTO.Menu();
            }
            menu.MenuBar = menuBar;
            menu.Title = title;
            menu.Url = url;
            menu.ApplicationPath = appPath;
            menu.ParentMenuId = parentId;
            menu.CssClass = css;
            menuRepository.Save(menu, UserContext.AuthenticatedUser);
            return menu;
        }
    }
}