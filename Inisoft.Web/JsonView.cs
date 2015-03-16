using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Web
{
    public class JsonView : IView
    {
        public string JsonObject { get; set; }

        public JsonView(string jsonObject)
        {
            JsonObject = jsonObject;
        }

        public string GetView()
        {
            return JsonObject;
        }
    }
}
