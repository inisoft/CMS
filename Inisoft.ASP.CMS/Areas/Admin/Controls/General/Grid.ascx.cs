using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inisoft.Core.Object;
using Inisoft.Core;
using Inisoft.Core.Provider;

namespace Inisoft.ASP.CMS.Areas.Admin.Controls.General
{
    public partial class Grid : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string TableHeader { get; set; }

        public ObjectDefinition ObjectDefinition { get; set; }

        public IList<GenericObject> Data { get; private set; }

        public void SetData(IEnumerable<GenericObject> data)
        {
            if (this.Data != null)
            {
                this.Data.Clear();
            }
            this.Data = new List<GenericObject>();
            foreach (GenericObject genericObject in data)
            {
                this.Data.Add(genericObject);
            }
        }

        private List<PropertyDefinition> displayedProperties = null;
        public List<PropertyDefinition> GetDisplayedProperties()
        {
            if (displayedProperties == null)
            {
                if (ObjectDefinition != null)
                {
                    displayedProperties = ObjectDefinition.GetProperties(PropertyDisplayFlagEnum.Grid);
                    if (displayedProperties == null || displayedProperties.Count == 0)
                    {
                        displayedProperties = new List<PropertyDefinition>();
                        displayedProperties.AddRange(ObjectDefinition.Properties);
                    }
                }
            }
            return displayedProperties;
        }
    }
}