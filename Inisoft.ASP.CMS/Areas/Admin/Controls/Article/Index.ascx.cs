using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inisoft.ASP.CMS.Core;
using Inisoft.ASP.CMS.Areas.Admin.Models;
using Inisoft.ASP.CMS.Areas.Admin.Controls;

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.Article
{
    public partial class Index : BaseViewControl<List<ArticleModel>>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BL.Repositories.Article _repository = new BL.Repositories.Article();
            this.Model = _repository.Get().Select(x => x.ToModel()).ToList();
        }

        protected override void OnSCMSEvaluateModelFromPostback()
        {
            //nothing;
        }

        protected override void OnSCMSOnPreRender()
        {
            //nothing;
        }

        public override string HeadTitle
        {
            get { return "Artykuły"; }
        }
    }
}