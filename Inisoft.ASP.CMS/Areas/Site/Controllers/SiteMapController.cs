using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Inisoft.ASP.CMS.Areas.Site.Controllers
{
    public class SiteMapController
    {
        private HttpContext _Context;
        public SiteMapController(HttpContext Context)
        {
            _Context = Context;
        }

        public void DoResponse()
        {
            /*
            BL.Repositories.WebSite _webSiteRepository = new BL.Repositories.WebSite();
            BL.Entities.WebSite _webSite = _webSiteRepository.Get().FirstOrDefault();
            BL.Repositories.WebPage _webPageRepository = new BL.Repositories.WebPage();
            IEnumerable<BL.Entities.WebPage> _listWebPage = _webPageRepository.Get().Where(x=> x.Visible).ToList();

            StringBuilder _xml = new StringBuilder(1024);
            _xml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            _xml.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            foreach (BL.Entities.WebPage _loopWebPage in _listWebPage)
            {
                _xml.AppendFormat("<url><loc>{0}{1}</loc></url>", _webSite.URL, _loopWebPage.URL);
            }
            _xml.AppendLine("</urlset>");

            _Context.Response.Clear();
            _Context.Response.Write(_xml.ToString());
            _Context.Response.Flush();
            _Context.ApplicationInstance.CompleteRequest();
             */
        }
    }
}