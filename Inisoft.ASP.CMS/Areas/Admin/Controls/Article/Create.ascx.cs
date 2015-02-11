using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inisoft.ASP.CMS.Core;
using Inisoft.ASP.CMS.Areas.Admin.Models;

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.Article
{
    public partial class Create : BaseViewControl<ArticleModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Model = new ArticleModel();
        }

        protected override void OnSMCSPostBack()
        {
            base.OnSMCSPostBack();
            if (Html.IsValid)
            {
                switch (URLContext.Action.ToLower())
                {
                    case "create":
                        BL.Repositories.Article _repository = new BL.Repositories.Article();
                        bool _result = _repository.Save(Model.ToEntity());
                        if (_result)
                        {
                            this.Response.Redirect("/admin/article/index/", true);
                        }
                        else
                        {
                            this.Html.AddValidationResult(Model, "Nie można zapisać danych");
                        }
                        break;
                }
            }
        }

        public override string HeadTitle
        {
            get { return "Artykuły"; }
        }
    }
}