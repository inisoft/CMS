using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace Inisoft.ASP.CMS.Core
{
    /// <summary>
    /// Klasa pozwalajaca na rozpoznanie odpowiednich czesci URLa
    /// </summary>
    public class URLContext
    {
        protected URLContext()
        {
        }

        public string Area { get; private set; }
        public string View { get; private set; }
        public string Action { get; private set; }
        public string[] Params { get; private set; }
        public string QueryString { get; private set; }
        public string OryginalURL { get; private set; }
        public NameValueCollection NameValueCollection { get; private set; }

        public static URLContext Get(HttpRequest Request)
        {
            URLContext _result = new URLContext();
            string _url = Request.ServerVariables["HTTP_X_ORIGINAL_URL"];
            _result.OryginalURL = _url;
            if (_url != null)
            {
                int _indexOf = _url.IndexOf("?");
                if (_indexOf > 0)
                {
                    _result.QueryString = _url.Substring(_indexOf);
                    _result.NameValueCollection = HttpUtility.ParseQueryString(_result.QueryString);
                    _url = _url.Substring(0, _indexOf);
                }

                if (_url.StartsWith("/"))
                {
                    _url = _url.Substring(1);
                }

                string[] _parts = _url.Split('/');
                if (_parts.Length > 0)
                {
                    _result.Area = _parts[0];
                }
                if (_parts.Length > 1)
                {
                    _result.View = _parts[1];
                }
                if (_parts.Length > 2)
                {
                    _result.Action = _parts[2];
                }
                if (_parts.Length > 3)
                {
                    _result.Params = new string[_parts.Length - 3];
                    int _index = 3;
                    while (_index < _parts.Length)
                    {
                        _result.Params[_index - 3] = _parts[_index];
                        _index++;
                    }
                }
            }
            return _result;
        }
    }
}