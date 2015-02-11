using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Provider;
using Inisoft.Core.Attribute;

namespace Inisoft.Core.Object
{
    [ObjectTypeAttribute(Const.Namespace.System, Const.Name.User)]
    public class User : GenericObject
    {
        public string FirstName { get { return GetStringValue("FirstName"); } set { SetValue("FirstName", value); } }
        public string LastName { get { return GetStringValue("LastName"); } set { SetValue("LastName", value); } }
        public string Email { get { return GetStringValue("Email"); } set { SetValue("Email", value); } }
        public string Nick { get { return GetStringValue("Nick"); } set { SetValue("Nick", value); } }
        public string ApplicationName { get { return GetStringValue("ApplicationName"); } set { SetValue("ApplicationName", value); } }

        public string Password { get; set; }
        public string PasswordQuestion { get { return GetStringValue("PasswordQuestion"); } set { SetValue("PasswordQuestion", value); } }
        public string PasswordAnswer { get { return GetStringValue("PasswordAnswer"); } set { SetValue("PasswordAnswer", value); } }
        public DateTime LastPasswordChangedDate { get { return GetDateTimeValue("LastPasswordChangedDate", DateTime.MinValue); } set { SetValue("LastPasswordChangedDate", value); } }

        public bool IsApproved { get { return GetBoolValue("IsApproved"); } set { SetValue("IsApproved", value); } }
        public bool IsLockedOut { get { return GetBoolValue("IsLockedOut"); } set { SetValue("IsLockedOut", value); } }
        public string Comment { get { return GetStringValue("Comment"); } set { SetValue("Comment", value); } }

        public DateTime LastLoginDate { get { return GetDateTimeValue("LastLoginDate", DateTime.MinValue); } set { SetValue("LastLoginDate", value); } }
        public DateTime LastActivityDate { get { return GetDateTimeValue("LastActivityDate", DateTime.MinValue); } set { SetValue("LastActivityDate", value); } }
        public DateTime LastLockedOutDate { get { return GetDateTimeValue("LastLockedOutDate", DateTime.MinValue); } set { SetValue("LastLockedOutDate", value); } }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public string UserName { get { return Email; } }
        public string Login { get { return Email; } }
    }
}