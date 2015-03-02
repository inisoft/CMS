using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inisoft.ASP.CMS.Core;
using Inisoft.Core.Interface;
using Inisoft.Core.Provider;
using Inisoft.Core;
using Inisoft.Web;


namespace Inisoft.ASP.CMS.Areas.Admin.Controls.General
{
    public class BaseDetailControl<TObject, TRepository> : BaseAdminControl<TObject>
        where TRepository : IBaseRepository<TObject>
        where TObject : GenericObject, new()
    {
        protected DetailView DetailView
        {
            get;
            private set;
        }

        private int objectID = 0;
        public int ObjectID
        {
            get
            {
                if (objectID == 0)
                {
                    objectID = UrlHelper.GetObjectID();
                }
                return objectID;
            }
        }

        protected virtual void SetupControl()
        {
            this.DetailView = Page.LoadControl("Controls/General/DetailView.ascx") as DetailView;
            this.Controls.Add(this.DetailView);
            TRepository repository = RepositoryServiceLocator.Get<TRepository>();
            this.Model = repository.Get(ObjectID).Data;

            this.DetailView.ObjectDefinition = repository.ObjectDefinition;
            this.DetailView.SetData(this.Model);
        }
    }

}