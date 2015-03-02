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
    public class BaseGridControl<TObject, TRepository> : BaseAdminControl<IList<TObject>>
        where TRepository : IBaseRepository<TObject>
        where TObject : GenericObject, new()
    {
        protected Grid Grid
        {
            get;
            private set;
        }

        public string TableHeader
        {
            get { return this.Grid != null ? this.Grid.TableHeader : null; }
            set { if (this.Grid != null) { this.Grid.TableHeader = value; } }
        }

        protected virtual void SetupControl()
        {
            this.Grid = Page.LoadControl("Controls/General/Grid.ascx") as Grid;
            this.Controls.Add(this.Grid);
            TRepository repository = RepositoryServiceLocator.Get<TRepository>();
            this.Model = repository.Get().Data;

            this.Grid.TableHeader = "Lista menu";
            this.Grid.ObjectDefinition = repository.ObjectDefinition;
            this.Grid.SetData(this.Model);
        }
    }
}