using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Permissions;

using Inisoft.ASP.CMS.Areas.Site.Models;

namespace Inisoft.ASP.CMS.Areas.Site.Controllers
{
    public class HomeController
    {
        private HttpContext _Context;
        public HomeController(HttpContext Context)
        {
            _Context = Context;
        }

        
        public SiteWebPageModel Index()
        {
            SiteWebPageModel _model = new SiteWebPageModel();

            /*
            BL.Repositories.Menu _menuRepository = new BL.Repositories.Menu();
            BL.Repositories.WebSite _webSiteRepository = new BL.Repositories.WebSite();
            _model.Title = "QOffice";
            _model.ViewName = ControlTypeCode.Home.ToString();

            IEnumerable<BL.Entities.Menu> _listMenu = _menuRepository.Get().Where(x=> x.Visible).OrderBy(x => x.Position).ToList();
            _model.MenuDataList.AddRange(_listMenu.Select(x => new MenuData() {Id = x.Id, Icon = x.Icon, Title = x.Title, IsActive = false, URL = x.URL }));

            BL.Entities.WebSite _webSite = _webSiteRepository.Get().FirstOrDefault();
            if (_webSite != null)
            {
                _model.YellowPage = _webSite.YellowPage;
                _model.HeadInlineJavaScript = _webSite.HeadInlineJavaScript;
                _model.HeadInlineStyle = _webSite.HeadInlineStyle;
                _model.HeadMetaData = _webSite.HeadMetaData;
                _model.HeadMetaDescription = _webSite.HeadMetaDescription;
                _model.HeadMetaKeyword = _webSite.HeadMetaKeyword;
                _model.HeadShortcutIcon = _webSite.HeadShortcutIcon;
            }

            string _normalizedUrl = GetNormalizedURL();
            if (_normalizedUrl != null)
            {
                BL.Repositories.WebPage _webPageRepository = new BL.Repositories.WebPage();
                IEnumerable<BL.Entities.WebPage> _listWebPage = _webPageRepository.Get();

                BL.Entities.WebPage _webPage = _listWebPage.Where(x => x.URL == _normalizedUrl && x.Visible).FirstOrDefault();
                if (_webPage != null)
                {
                    _model.Title = _webPage.Title;
                    ControlTypeCode _ControlTypeCode = (ControlTypeCode)_webPage.TemplateId;
                    _model.ViewName = _ControlTypeCode.ToString();
                    BL.Repositories.Article _articleRepository = new BL.Repositories.Article();
                    IEnumerable<BL.Entities.Article> _listArticle = _articleRepository.Get();
                    foreach (BL.Entities.Article _loopArticle in _listArticle)
                    {
                        if (_webPage.IsArticleId(_loopArticle.Id))
                        {
                            _model.WebPageData.Add(_loopArticle);
                        }
                    }

                    _model.MenuDataList.Where(x => x.Id == _webPage.MenuId).ToList().ForEach(delegate(MenuData data) { data.IsActive = true; });

                    if (!string.IsNullOrEmpty(_webPage.HeadMetaDescription))
                    {
                        _model.HeadMetaDescription = _webPage.HeadMetaDescription;
                    }
                    if (!string.IsNullOrEmpty(_webPage.HeadMetaKeyword))
                    {
                        _model.HeadMetaKeyword = _webPage.HeadMetaKeyword;
                    }
                }
            }
            */
            return _model;
        }

        private string GetNormalizedURL()
        {
            return _Context.Request.ServerVariables["HTTP_X_ORIGINAL_URL"];            
        }
    }
}