using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inisoft.ASP.CMS.Areas.Admin.Models.Menu
{
    public class NavbarDefaultModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Css { get; set; }
    }

    public static class MenuExtension
    {
        public static NavbarDefaultModel ToModel(this DAL.DTO.Menu menu)
        {
            return new NavbarDefaultModel()             
            { 
                Id = menu.Id,
                ParentId = menu.ParentMenuId,
                Css = menu.CssClass,
                Title = menu.Title,
                Url = menu.Url
            };
        }
    }
}