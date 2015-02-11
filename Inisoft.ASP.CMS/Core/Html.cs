using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Inisoft.ASP.CMS.Core
{
    /// <summary>
    /// Klasa narzedziowa do generowania (tworzenia) odpowiednich wstawek HTML (tagow)
    /// </summary>
    public static class HtmlHelperExtension
    {
        private static MemberInfo GetMemberInfoOfExpression<TModel>(TModel target, Expression exp)
        {
            if (exp.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = (MemberExpression)exp;
                if (memberExpression.Member is PropertyInfo)
                    return ((PropertyInfo)memberExpression.Member);
                else
                    return ((FieldInfo)memberExpression.Member);
            }
            else
            {
                throw new ArgumentException("The expression must contain only member access calls.", "exp");
            }
        }

        private static object GetMemberValue<TModel>(TModel target, MemberInfo MemberInfo)
        {
            if (MemberInfo is PropertyInfo)
            {
                PropertyInfo _PropertyInfo = (MemberInfo as PropertyInfo);
                return _PropertyInfo.GetValue(target, null);
            }            
            if (MemberInfo is FieldInfo)
            {
                FieldInfo _FieldInfo = (MemberInfo as FieldInfo);
                return _FieldInfo.GetValue(target);
            }
            return null;
        }

        
        public static string LabelFor<TModel, TValue>(this HtmlHelper<TModel> Source, Expression<Func<TModel, TValue>> expression)
        {
            string _caption = string.Empty;
            MemberInfo _MemberInfo = GetMemberInfoOfExpression<TModel>(Source.ParentControl.Model, expression.Body);
            object[] _attributes = _MemberInfo.GetCustomAttributes(true);
            if (_attributes != null && _attributes.Length != 0)
            {
                foreach (object _loopAttribute in _attributes)
                {
                    if (_loopAttribute is DisplayNameAttribute)
                    {
                        _caption = (_loopAttribute as DisplayNameAttribute).DisplayName;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(_caption))
            {
                _caption = _MemberInfo.Name;
            }

            return string.Format("<label>{0}</label>", _caption);
        }

        public static string TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> Source, Expression<Func<TModel, TValue>> expression)
        {
            string _value = string.Empty;
            int _maxLength = 0;
            MemberInfo _MemberInfo = GetMemberInfoOfExpression<TModel>(Source.ParentControl.Model, expression.Body);
            object[] _attributes = _MemberInfo.GetCustomAttributes(true);
            object _valueObj = GetMemberValue<TModel>(Source.ParentControl.Model, _MemberInfo);
            if (_valueObj != null)
            {
                _value = _valueObj.ToString();
            }
            if (_attributes != null && _attributes.Length != 0)
            {
                foreach (object _loopAttribute in _attributes)
                {
                    if (_loopAttribute is StringLengthAttribute)
                    {
                        _maxLength = (_loopAttribute as StringLengthAttribute).MaximumLength;
                        break;
                    }
                }
            }

            return string.Format("<input type=\"textbox\" id=\"{0}\" name=\"{0}\" value=\"{1}\" />", _MemberInfo.Name, _value);
        }

        public static string TextAreaFor<TModel, TValue>(this HtmlHelper<TModel> Source, Expression<Func<TModel, TValue>> expression, int Rows, int Cols)
        {
            string _value = string.Empty;
            int _maxLength = 0;
            MemberInfo _MemberInfo = GetMemberInfoOfExpression<TModel>(Source.ParentControl.Model, expression.Body);
            object[] _attributes = _MemberInfo.GetCustomAttributes(true);
            object _valueObj = GetMemberValue<TModel>(Source.ParentControl.Model, _MemberInfo);
            if (_valueObj != null)
            {
                _value = _valueObj.ToString();
            }
            if (_attributes != null && _attributes.Length != 0)
            {
                foreach (object _loopAttribute in _attributes)
                {
                    if (_loopAttribute is StringLengthAttribute)
                    {
                        _maxLength = (_loopAttribute as StringLengthAttribute).MaximumLength;
                        break;
                    }
                }
            }

            return string.Format("<textarea rows=\"{2}\" cols=\"{3}\" id=\"{0}\" name=\"{0}\">{1}</textarea>", _MemberInfo.Name, _value, Rows, Cols);
        }

        public static string CheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> Source, Expression<Func<TModel, TValue>> expression, List<KeyValuePair<string, string>> Data)
        {
            MemberInfo _MemberInfo = GetMemberInfoOfExpression<TModel>(Source.ParentControl.Model, expression.Body);
            object[] _attributes = _MemberInfo.GetCustomAttributes(true);
            object _valueObj = GetMemberValue<TModel>(Source.ParentControl.Model, _MemberInfo);
            List<string> _values = new List<string>();
            if (_valueObj != null)
            {
                if (_valueObj is IEnumerable)
                {
                    foreach (object _loopObj in (_valueObj as IEnumerable))
                    {
                        _values.Add(_loopObj.ToString());
                    }
                }
                else
                {
                    _values.Add(_valueObj.ToString());
                }
            }
            string _inputFormat = "<input type=\"checkbox\" id=\"{0}_{1}\" name=\"{0}\" value=\"{2}\" {3}/><label for=\"{0}_{1}\">{4}</label>";

            StringBuilder _html = new StringBuilder();
            _html.Append("<div class=\"check-list\">");
            foreach (KeyValuePair<string, string> _loopPair in Data)
            {
                _html.AppendFormat(_inputFormat, _MemberInfo.Name, _loopPair.Key, _loopPair.Key, _values.Contains(_loopPair.Key) ? "checked=\"checked\"" : string.Empty, _loopPair.Value);
            }
            _html.Append("</div>");
            return _html.ToString();
        }

        public static string CheckBoxFor<TModel, TValue>(this HtmlHelper<TModel> Source, Expression<Func<TModel, TValue>> expression)
        {
            MemberInfo _MemberInfo = GetMemberInfoOfExpression<TModel>(Source.ParentControl.Model, expression.Body);
            object[] _attributes = _MemberInfo.GetCustomAttributes(true);
            object _valueObj = GetMemberValue<TModel>(Source.ParentControl.Model, _MemberInfo);
            string _value = string.Empty;
            string _checked = string.Empty;
            if (_valueObj != null)
            {
                bool _bool = false;
                if (Boolean.TryParse(_valueObj.ToString().Trim().ToLower(), out _bool))
                {
                    _checked = _bool ? "checked=\"checked\"" : string.Empty;
                }
            }
            return string.Format("<input type=\"checkbox\" id=\"{0}\" name=\"{0}\" value=\"true\" {1}/>", _MemberInfo.Name, _checked);
        }
        
        public static string SelectFor<TModel, TValue>(this HtmlHelper<TModel> Source, Expression<Func<TModel, TValue>> expression, List<KeyValuePair<string, string>> Data)
        {
            MemberInfo _MemberInfo = GetMemberInfoOfExpression<TModel>(Source.ParentControl.Model, expression.Body);
            object[] _attributes = _MemberInfo.GetCustomAttributes(true);
            object _valueObj = GetMemberValue<TModel>(Source.ParentControl.Model, _MemberInfo);
            string _value = string.Empty;
            if (_valueObj != null)
            {
                _value = _valueObj.ToString();
            }
            string _optionFormat = "<option value=\"{0}\" {2}>{1}</option>";

            StringBuilder _html = new StringBuilder();
            _html.AppendFormat("<select id=\"{0}\" name=\"{0}\">", _MemberInfo.Name);
            foreach (KeyValuePair<string, string> _loopPair in Data)
            {
                _html.AppendFormat(_optionFormat, _loopPair.Key, _loopPair.Value, _value == _loopPair.Key ? "selected=\"selected\"" : string.Empty);
            }
            _html.Append("</select>");
            return _html.ToString();
        }        

        public static string ValidationMessageFor<TModel>(this HtmlHelper<TModel> Source)
        {
            if (!Source.ParentControl.IsPostBack)
            {
                return string.Empty;
            }

            if (Source.ValidationResultList == null || !Source.ValidationResultList.Any())
            {
                return string.Empty;
            }

            List<string> _errorMessages = new List<string>();
            foreach (ValidationResult _loopValidationResult in Source.ValidationResultList)
            {
                if (!_loopValidationResult.MemberNames.Any())
                {
                    _errorMessages.Add(_loopValidationResult.ErrorMessage);
                }
            }

            if (_errorMessages.Count == 0)
            {
                return string.Empty;
            }
            return string.Format("<label class=\"failureNotification\">{0}</label>", string.Join(". ", _errorMessages.ToArray()));
        }

        public static string ValidationMessageFor<TModel, TValue>(this HtmlHelper<TModel> Source, Expression<Func<TModel, TValue>> expression)
        {
            if (!Source.ParentControl.IsPostBack)
            {
                return string.Empty;
            }

            if (Source.ValidationResultList == null || !Source.ValidationResultList.Any())
            {
                return string.Empty;
            }

            MemberInfo _MemberInfo = GetMemberInfoOfExpression<TModel>(Source.ParentControl.Model, expression.Body);
            List<string> _errorMessages = new List<string>();

            foreach (ValidationResult _loopValidationResult in Source.ValidationResultList)
            {
                if (_loopValidationResult.MemberNames.Contains(_MemberInfo.Name))
                {
                    _errorMessages.Add(_loopValidationResult.ErrorMessage);
                }
            }

            if (_errorMessages.Count == 0)
            {
                return string.Empty;
            }
            return string.Format("<label class=\"failureNotification\">{0}</label>", string.Join(". ", _errorMessages.ToArray()));
        }

        public static string Link<TModel>(this HtmlHelper<TModel> Source, string Area, string View, string Title, string SelectedClassName)
        {
            return Source.Link(Area, View, null, Title, null, null, null, SelectedClassName);
        }

        public static string Link<TModel>(this HtmlHelper<TModel> Source, string Area, string View, string Action, string Title, string SelectedClassName)
        {
            return Source.Link(Area, View, Action, Title, null, null, null, SelectedClassName);
        }

        public static string Link<TModel>(this HtmlHelper<TModel> Source, string Area, string View, string Action, string Title, string QueryString, string ClassName, string Target, string SelectedClassName)
        {
            string _href = "/";
            if (!string.IsNullOrEmpty(Area))
            {
                _href = string.Format("{0}{1}/", _href, Area);
            }
            if (!string.IsNullOrEmpty(View))
            {
                _href = string.Format("{0}{1}/", _href, View);
            }
            if (!string.IsNullOrEmpty(Action))
            {
                _href = string.Format("{0}{1}/", _href, Action);
            }

            if (!string.IsNullOrEmpty(SelectedClassName))
            {
                string _url = Source.ParentControl.Request.ServerVariables["HTTP_X_ORIGINAL_URL"].ToLower();
                string _hrefLower = _href.ToLower();
                if (_url == _hrefLower || _url.StartsWith(_hrefLower))
                {
                    if (string.IsNullOrEmpty(ClassName))
                    {
                        ClassName = string.Empty;
                    }
                    ClassName = string.Format("{0} {1}", ClassName, SelectedClassName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Area) && !string.IsNullOrEmpty(View))
                    {
                        _hrefLower = string.Format("/{0}/{1}", Area, View).ToLower();
                        if (_url == _hrefLower || _url.StartsWith(_hrefLower))
                        {
                            if (string.IsNullOrEmpty(ClassName))
                            {
                                ClassName = string.Empty;
                            }
                            ClassName = string.Format("{0} {1}", ClassName, SelectedClassName);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(QueryString))
            {
                if (QueryString.StartsWith("?"))
                {
                    _href = string.Format("{0}{1}", _href, QueryString);
                }
                else
                {
                    _href = string.Format("{0}?{1}", _href, QueryString);
                }
            }
            string _class = string.Empty;
            if (!string.IsNullOrEmpty(ClassName))
            {
                _class = string.Format("class=\"{0}\"", ClassName);
            }
            string _target = string.Empty;
            if (!string.IsNullOrEmpty(Target))
            {
                _target = string.Format("target=\"{0}\"", Target);
            }

            return string.Format("<a href=\"{0}\" {1} {2} title=\"{3}\">{3}</a>", _href, _class, _target, Title);
        }


        public static string Link_Index<TModel>(this HtmlHelper<TModel> Source, string View)
        {
            return Source.Link("admin", View, "index", "Lista", null, "action-index", null, null);
        }
        public static string Link_Create<TModel>(this HtmlHelper<TModel> Source, string View)
        {
            return Source.Link("admin", View, "create", "Nowy", null, "action-index", null, null);
        }
        public static string Link_Edit<TModel>(this HtmlHelper<TModel> Source, string View, int Id)
        {
            return Source.Link("admin", View, "edit", "Edytuj", "id=" + Id, null, null, null);
        }
        public static string Link_Delete<TModel>(this HtmlHelper<TModel> Source, string View, int Id)
        {
            return Source.Link("admin", View, "delete", "Kasuj", "id=" + Id, null, null, null);
        }
    }
}