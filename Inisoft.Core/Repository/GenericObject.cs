using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core.Provider
{
    public enum GenericObjectState
    {
        NotModified = 0,
        Modified = 1,
    }

    public abstract class GenericObject
    {
        protected Dictionary<string, object> values = new Dictionary<string, object>();

        public int Id { get { return GetIntValue("Id"); } set { SetValue("Id", value); } }

        public DateTime CreateDate { get { return GetDateTimeValue("CreateDate", DateTime.MinValue); } set { SetValue("CreateDate", value); } }
        public string CreateUser { get { return GetStringValue("CreateUser"); } set { SetValue("CreateUser", value); } }
        public int CreateUserId { get { return GetIntValue("CreateUserId"); } set { SetValue("CreateUserId", value); } }

        public DateTime UpdateDate { get { return GetDateTimeValue("UpdateDate", DateTime.MinValue); } set { SetValue("UpdateDate", value); } }
        public string UpdateUser { get { return GetStringValue("UpdateUser"); } set { SetValue("UpdateUser", value); } }
        public int UpdateUserId { get { return GetIntValue("UpdateUserId"); } set { SetValue("UpdateUserId", value); } }

        public int Version { get { return GetIntValue("Version"); } set { SetValue("Version", value); } }

        public GenericObjectState State { get; internal set; }

        public bool IsNew
        {
            get { return Id == 0; }
        }

        public object GetValue(string propertyName, object defaultValue = null)
        {
            if (values.ContainsKey(propertyName))
            {
                return values[propertyName];
            }
            return defaultValue;
        }

        public void SetValue(string propertyName, object value)
        {
            if (value == System.DBNull.Value)
            {
                value = null;
            }
            if (values.ContainsKey(propertyName))
            {
                if (values[propertyName] != value)
                {
                    values[propertyName] = value;
                    State = GenericObjectState.Modified;
                }
            }
            else
            {
                values[propertyName] = value;
                State = GenericObjectState.Modified;
            }
        }

        public int GetIntValue(string propertyName, int defaultValue = 0)
        {
            object value = GetValue(propertyName, defaultValue);
            if (ValueIsNull(value))
            {
                return defaultValue;
            }
            return Convert.ToInt32(value);
        }

        public string GetStringValue(string propertyName, string defaultValue = null)
        {
            object value = GetValue(propertyName, defaultValue);
            if (ValueIsNull(value))
            {
                return defaultValue;
            }
            return Convert.ToString(value);
        }

        public DateTime GetDateTimeValue(string propertyName, DateTime defaultValue)
        {
            object value = GetValue(propertyName, defaultValue);
            if (ValueIsNull(value))
            {
                return defaultValue;
            }
            return Convert.ToDateTime(value);
        }

        public bool GetBoolValue(string propertyName)
        {
            object value = GetValue(propertyName, false);
            if (ValueIsNull(value))
            {
                return false;
            }
            return Convert.ToBoolean(value);
        }

        public bool ValueIsNull(object value)
        {
            return value == null || value == System.DBNull.Value;
        }
    }
}