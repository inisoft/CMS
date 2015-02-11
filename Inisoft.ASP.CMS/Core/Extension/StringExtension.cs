using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inisoft.ASP.CMS.Core.Extension
{
    public static class StringExtension
    {
        public static string UppercaseFirst(this string Source)
        {
            if (string.IsNullOrEmpty(Source))
            {
                return string.Empty;
            }
            char[] a = Source.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}