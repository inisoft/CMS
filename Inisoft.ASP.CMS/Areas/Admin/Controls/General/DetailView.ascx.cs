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
    public partial class DetailView : CMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public ObjectDefinition ObjectDefinition { get; set; }

        public GenericObject Data { get; private set; }

        public void SetData(GenericObject data)
        {
            this.Data = data;
        }

        private List<PropertyDefinition> displayedProperties = null;
        public List<PropertyDefinition> GetDisplayedProperties()
        {
            if (displayedProperties == null)
            {
                if (ObjectDefinition != null)
                {
                    displayedProperties = ObjectDefinition.GetProperties(PropertyDisplayFlagEnum.Details);
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