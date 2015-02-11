using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Specialized;
using System.Reflection;
using System.Globalization;
using System.Collections;
using System.ComponentModel;

namespace Inisoft.ASP.CMS.Core
{
    public class BaseControl : System.Web.UI.UserControl
    {
        public BaseControl()
            : base()
        {

        }

        private string _headTitle = string.Empty;

        public virtual string HeadTitle
        {
            get { return _headTitle; }
        }
        public virtual string Title
        {
            get { return HeadTitle; }
        }

        private URLContext _URLContext = null;
        public URLContext URLContext
        {
            get
            {
                if (_URLContext == null)
                {
                    _URLContext = URLContext.Get(this.Request);
                }
                return _URLContext;
            }
        }
    }

    public class BaseViewControl<TModel> : BaseControl
    {
        public BaseViewControl()
            : base()
        {
            Html = new HtmlHelper<TModel>();
            Html.ParentControl = this;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Model != null && IsPostBack)
            {
                OnSCMSEvaluateModelFromPostback();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Model != null && IsPostBack)
            {
                OnSCMSOnPreRender();
            }
            base.OnPreRender(e);
        }

        protected virtual void OnSCMSPreValidate()
        {
        }

        protected virtual void OnSMCSPostBack()
        {
        }

        protected virtual void OnSCMSEvaluateModelFromPostback()
        {
            this.Html.EvaluateFromPostbackForm(Model, Request.Form);
        }

        protected virtual void OnSCMSOnPreRender()
        {
            OnSCMSPreValidate();
            this.Html.Validate(Model);
            OnSMCSPostBack();
        }

        public HtmlHelper<TModel> Html
        {
            get;
            private set;
        }

        public TModel Model
        {
            get;
            set;
        }
    }

    public class HtmlHelper<TModel>
    {
        public bool IsValid { get; private set; }
        public BaseViewControl<TModel> ParentControl { get; internal set; }
        public ValidationContext ValidationContext { get; private set; }
        public List<ValidationResult> ValidationResultList { get; private set; }

        public void AddValidationResult(TModel Model, string Message)
        {
            AddValidationResult(Model, Message, null);
        }

        public void AddValidationResult(TModel Model, string Message, string PropertyName)
        {
            InitValidationData(Model);
            if (PropertyName == null) PropertyName = string.Empty;
            ValidationResultList.Add(new ValidationResult(Message, new string[] { PropertyName }));
        }

        public void EvaluateFromPostbackForm(TModel Model, NameValueCollection FormValues)
        {
            InitValidationData(Model);
            Type _booleanType = typeof(bool);
            Type _type = Model.GetType();
            PropertyInfo[] _members = _type.GetProperties();
            foreach (PropertyInfo _loopPropertyInfo in _members)
            {
                if (IsBindable(_loopPropertyInfo))
                {
                    try
                    {
                        var underlyingType = Nullable.GetUnderlyingType(_loopPropertyInfo.PropertyType);
                        if (underlyingType == null)
                        {
                            Type _genericListType = GetGenericListType(_loopPropertyInfo.PropertyType);
                            if (_genericListType != null)
                            {
                                Type _listType = typeof(List<>);
                                Type concreteType = _listType.MakeGenericType(_genericListType);
                                IList newList = (IList)Activator.CreateInstance(concreteType);

                                string _formValues = FormValues[_loopPropertyInfo.Name];
                                if (!string.IsNullOrEmpty(_formValues))
                                {
                                    foreach (string _loopFormValue in _formValues.Split(','))
                                    {
                                        if (!string.IsNullOrEmpty(_loopFormValue))
                                        {
                                            newList.Add(Convert.ChangeType(_loopFormValue, _genericListType, CultureInfo.InvariantCulture));
                                        }
                                    }
                                }
                                _loopPropertyInfo.SetValue(Model, newList, null);
                            }
                            else
                            {
                                if (_booleanType == _loopPropertyInfo.PropertyType && FormValues[_loopPropertyInfo.Name] == null)
                                {
                                    _loopPropertyInfo.SetValue(Model, false, null);
                                }
                                else
                                {
                                    _loopPropertyInfo.SetValue(Model, Convert.ChangeType(FormValues[_loopPropertyInfo.Name], _loopPropertyInfo.PropertyType, CultureInfo.InvariantCulture), null);
                                }
                            }
                        }
                        else
                        {
                            if (_booleanType == underlyingType && FormValues[_loopPropertyInfo.Name] == null)
                            {
                                _loopPropertyInfo.SetValue(Model, false, null);
                            }
                            else
                            {
                                _loopPropertyInfo.SetValue(Model, Convert.ChangeType(FormValues[_loopPropertyInfo.Name], underlyingType, CultureInfo.InvariantCulture), null);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        AddValidationResult(Model, e.Message, _loopPropertyInfo.Name);
                        IsValid = false;
                    }
                }
            }
        }

        public bool Validate(TModel Model)
        {
            InitValidationData(Model);
            if (Validator.TryValidateObject(Model, ValidationContext, ValidationResultList, true))
            {
                Type _type = Model.GetType();
                PropertyInfo[] _members = _type.GetProperties();
                object[] _attributes = null;
                object _valueObj = null;
                foreach (PropertyInfo _loopMember in _members)
                {
                    _attributes = _loopMember.GetCustomAttributes(true);
                    if (_attributes != null && _attributes.Length != 0)
                    {
                        _valueObj = _loopMember.GetValue(Model, null);
                        foreach (object _loopAttribute in _attributes)
                        {
                            if (_loopAttribute is ValidationAttribute)
                            {
                                ValidationAttribute _ValidationAttribute = (_loopAttribute as ValidationAttribute);
                                try
                                {
                                    _ValidationAttribute.Validate(_valueObj, _loopMember.Name);
                                }
                                catch (Exception e)
                                {
                                    AddValidationResult(Model, e.Message, _loopMember.Name);
                                    _ValidationAttribute.ErrorMessage = e.Message;
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                IsValid = false;
            }
            return IsValid;
        }

        private void InitValidationData(TModel Model)
        {
            if (ValidationContext != null)
            {
                return;
            }
            IsValid = true;
            ValidationContext = new ValidationContext(Model, null, null);
            ValidationResultList = new List<ValidationResult>();
        }

        private Type GetGenericListType(Type ListType)
        {
            foreach (Type intType in ListType.GetInterfaces())
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;
        }

        private bool IsBindable(PropertyInfo Property)
        {
            object[] _attributes = Property.GetCustomAttributes(true);
            if (_attributes != null && _attributes.Length != 0)
            {
                foreach (object _loopAttribute in _attributes)
                {
                    if (_loopAttribute is BindableAttribute)
                    {
                        BindableAttribute _ValidationAttribute = (_loopAttribute as BindableAttribute);
                        return _ValidationAttribute.Bindable;
                    }
                }
            }
            return true;
        }
    }
}