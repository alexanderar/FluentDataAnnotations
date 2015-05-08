// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlSelectExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The html select extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace FluentDataAnnotations.Helpers.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    /// <summary>
    ///     The HTML select extensions.
    /// </summary>
    public static class HtmlSelectExtensions
    {
        // DropDownList
        #region Constants

        /// <summary>
        ///     The pure JavaScript  format.
        /// </summary>
        /// <remarks>
        ///     0 - triggerMemberInfo.Name
        ///     1 - URL
        ///     2 - actionParam
        ///     3 - optionLabel
        ///     4 - dropdownElementId
        ///     5 - Preselected Value
        ///     6 - if element should be disabled when parent not selected, will contain setAttribute('disabled','disabled')
        ///     command
        ///     7 - if element was initially disabled, will contain removeAttribute('disabled') command
        /// </remarks>
        private const string PureJsScriptFormat = @"<script>       
    function initCascadeDropDownFor{4}() {{
        var triggerElement = document.getElementById('{0}');
        var targetElement = document.getElementById('{4}');
        var preselectedValue = '{5}';
        triggerElement.addEventListener('change', function(e) {{
            {7}
            var value = triggerElement.value;
            var items = '<option value="""">{3}</option>';            
            if (!value) {{
                targetElement.innerHTML = items;
                targetElement.value = '';                
                var event = document.createEvent('HTMLEvents');
                event.initEvent('change', true, false);
                targetElement.dispatchEvent(event);
                {6}
                return;
            }}
            var url = '{1}?{2}=' + value;
            var request = new XMLHttpRequest();
            request.open('GET', url, true);
            var isSelected = false;
            request.onload = function () {{
                if (request.status >= 200 && request.status < 400) {{
                    // Success!
                    var data = JSON.parse(request.responseText);
                    if (data) {{                        
                        data.forEach(function(item, i) {{                              
                            items += '<option value=""' + item.Value + '"">' + item.Text + '</option>';                                
                        }});
                        targetElement.innerHTML = items;  
                        if(preselectedValue)
                        {{                           
                            targetElement.value = preselectedValue;                            
                            preselectedValue = null;                           
                        }}  
                        var event = document.createEvent('HTMLEvents');
                        event.initEvent('change', true, false);
                        targetElement.dispatchEvent(event);                                                                                          
                    }}
                }} else {{
                    console.log(request.statusText);
                }}
            }};

            request.onerror = function (error) {{
                console.log(error);
            }};

            request.send();
        }});
        if(triggerElement.value && !targetElement.value)
        {{
            var event = document.createEvent('HTMLEvents');
            event.initEvent('change', true, false);
            triggerElement.dispatchEvent(event);           
        }} 
    }};

    if (document.readyState != 'loading') {{
        initCascadeDropDownFor{4}();
    }} else {{
        document.addEventListener('DOMContentLoaded', initCascadeDropDownFor{4});
    }}
</script>";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The cascading drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="triggeredByProperty">
        /// The triggered by property.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="actionParam">
        /// The action param.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="disabledWhenParentNotSelected">
        /// The disabled when parent not selected.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static MvcHtmlString CascadingDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Expression<Func<TModel, TProperty>> triggeredByProperty, 
            string url, 
            string actionParam, 
            string optionLabel = "", 
            bool disabledWhenParentNotSelected = false, 
            object htmlAttributes = null)
        {
            MemberInfo triggerMemberInfo = Utilities.GetMemberInfo(triggeredByProperty);
            if (triggerMemberInfo == null)
            {
                throw new ArgumentException("triggeredByProperty argument is invalid");
            }

            return CascadingDropDownListFor(
                htmlHelper, 
                expression, 
                triggerMemberInfo.Name, 
                url, 
                actionParam, 
                optionLabel, 
                disabledWhenParentNotSelected, 
                htmlAttributes);
        }

        /// <summary>
        /// The cascading drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The property.
        /// </param>
        /// <param name="inputId">the Input id</param>
        /// <param name="triggeredByProperty">
        /// The triggered by property.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="actionParam">
        /// The action param.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="disabledWhenParentNotSelected">
        /// The disabled when parent not selected.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static MvcHtmlString CascadingDropDownList<TModel, TProperty>(
            this HtmlHelper htmlHelper,
            string name,
            string inputId,
            Expression<Func<TModel, TProperty>> triggeredByProperty,
            string url,
            string actionParam,
            string optionLabel = "",
            bool disabledWhenParentNotSelected = false,
            object htmlAttributes = null)
        {
            MemberInfo triggerMemberInfo = Utilities.GetMemberInfo(triggeredByProperty);
            if (triggerMemberInfo == null)
            {
                throw new ArgumentException("triggeredByProperty argument is invalid");
            }

            return CascadingDropDownList(
                htmlHelper,
                name,
                inputId,
                triggerMemberInfo.Name,
                url,
                actionParam,
                optionLabel,
                disabledWhenParentNotSelected,
                htmlAttributes);
        }

        /// <summary>
        /// The cascading drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The property.
        /// </param>
        /// <param name="inputId"></param>
        /// <param name="triggeredByProperty">
        /// The triggered by property.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="actionParam">
        /// The action param.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="disabledWhenParentNotSelected">
        /// The disabled when parent not selected.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString CascadingDropDownList(
            this HtmlHelper htmlHelper,
            string name,
            string inputId,
            string triggeredByProperty,
            string url,
            string actionParam,
            string optionLabel = "",
            bool disabledWhenParentNotSelected = false,
            object htmlAttributes = null)
        {
            RouteValueDictionary dictionary = htmlAttributes != null ? 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) : new RouteValueDictionary();

            return CascadingDropDownList(
                htmlHelper,
                name,
                inputId,
                triggeredByProperty,
                url,
                actionParam,
                optionLabel,
                disabledWhenParentNotSelected,
                dictionary);
        }

        public static MvcHtmlString CascadingDropDownList(
            this HtmlHelper htmlHelper,
            string name,
            string inputId,
            string triggeredByProperty,
            string url,
            string actionParam,
            string optionLabel = "",
            bool disabledWhenParentNotSelected = false,
            RouteValueDictionary htmlAttributes = null)
        {
            if (disabledWhenParentNotSelected)
            {
                if (htmlAttributes == null)
                {
                    htmlAttributes = new RouteValueDictionary();
                }

                htmlAttributes.Add("disabled", "disabled");
            }

            var defaultDropDownHtml = htmlHelper.SmartDropDownList(
                name,
                new List<SelectListItem>(),
                optionLabel,
                htmlAttributes);

            string script;

            var type = htmlHelper.ViewData.Model.GetType();
            var defaultVal = type.IsValueType ? Activator.CreateInstance(type) : null;
            var modelValue = string.Empty;
            if (defaultVal != null || htmlHelper.ViewData.Model != null)
            {
                if (defaultVal != null &&
                    !htmlHelper.ViewData.Model.ToString().Equals(defaultVal.ToString(), StringComparison.Ordinal))
                {
                    modelValue = htmlHelper.ViewData.Model.ToString();
                }
            }

            if (disabledWhenParentNotSelected)
            {
                script = string.Format(
                    PureJsScriptFormat,
                    triggeredByProperty,
                    url,
                    actionParam,
                    optionLabel,
                    inputId,
                    modelValue,
                    "targetElement.setAttribute('disabled','disabled');",
                    "targetElement.removeAttribute('disabled');");
            }
            else
            {
                script = string.Format(
                    PureJsScriptFormat,
                    triggeredByProperty,
                    url,
                    actionParam,
                    optionLabel,
                    inputId,
                    modelValue,
                    string.Empty,
                    string.Empty);
            }

            var spanEventHandler = "<span id='" + inputId + "evenhHandler'></span>";

            var cascadingDropDownString = spanEventHandler + Environment.NewLine + defaultDropDownHtml + Environment.NewLine + script;

            return new MvcHtmlString(cascadingDropDownString);
        }

        /// <summary>
        /// The cascading drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The HTML helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="triggeredByPropertyWithId">
        /// The triggered By Property With Id.
        /// </param>
        /// <param name="url">
        /// The URL.
        /// </param>
        /// <param name="actionParam">
        /// The action parameter.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="disabledWhenParentNotSelected">
        /// The disabled when parent not selected.
        /// </param>
        /// <param name="htmlAttributes">
        /// The HTML attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// Type of model
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// Type of property in model
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown in case that one of the arguments is invalid
        /// </exception>
        public static MvcHtmlString CascadingDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            string triggeredByPropertyWithId, 
            string url,
            string actionParam, 
            string optionLabel = "", 
            bool disabledWhenParentNotSelected = false, 
            object htmlAttributes = null)
        {

            MemberInfo dropDownElement = Utilities.GetMemberInfo(expression);

            if (dropDownElement == null)
            {
                throw new ArgumentException("expression argument is invalid");
            }

            var dropDownElementId = dropDownElement.Name;

            return CascadingDropDownList(
               htmlHelper,
               dropDownElementId,
               dropDownElementId,
               triggeredByPropertyWithId,
               url,
               actionParam,
               optionLabel,
               disabledWhenParentNotSelected,
               htmlAttributes);            
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(this HtmlHelper htmlHelper, string name)
        {
            return SmartDropDownList(
                htmlHelper, 
                name, 
                (IEnumerable<SelectListItem>)null /* selectList */, 
                null /* optionLabel */, 
                null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(this HtmlHelper htmlHelper, string name, string optionLabel)
        {
            return SmartDropDownList(
                htmlHelper, 
                name, 
                (IEnumerable<SelectListItem>)null /* selectList */, 
                optionLabel, 
                null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        ///     The html helper.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="selectList">
        ///     The select list.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return SmartDropDownList(htmlHelper, name, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList)
        {
            return SmartDropDownList(htmlHelper, name, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList, 
            object htmlAttributes)
        {
            return SmartDropDownList(
                htmlHelper, 
                name, 
                selectList, 
                null /* optionLabel */, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            IEnumerable<SelectListItem> selectList, 
            object htmlAttributes)
        {
            return SmartDropDownList(
                htmlHelper, 
                name, 
                selectList, 
                null /* optionLabel */, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartDropDownList(htmlHelper, name, selectList, null /* optionLabel */, htmlAttributes);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            IEnumerable<SelectListItem> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartDropDownList(htmlHelper, name, selectList, null /* optionLabel */, htmlAttributes);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList, 
            string optionLabel)
        {
            return SmartDropDownList(htmlHelper, name, selectList, optionLabel, null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            IEnumerable<SelectListItem> selectList, 
            string optionLabel)
        {
            return SmartDropDownList(htmlHelper, name, selectList, optionLabel, null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            IEnumerable<SelectListItem> selectList, 
            string optionLabel, 
            object htmlAttributes)
        {
            return SmartDropDownList(
                htmlHelper, 
                name, 
                selectList, 
                optionLabel, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList, 
            string optionLabel, 
            object htmlAttributes)
        {
            return SmartDropDownList(
                htmlHelper, 
                name, 
                selectList, 
                optionLabel, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            IEnumerable<SelectListItem> selectList, 
            string optionLabel, 
            IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromStringExpression(name, htmlHelper.ViewData);

            IList<SelectListItem> selectListItems = selectList as IList<SelectListItem> ?? selectList.ToList();
            var selectedItem = selectListItems.FirstOrDefault(i => i.Value == htmlHelper.ViewData.Model.ToString());
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }
            return GetReadonlyValue(metadata, selectListItems, htmlAttributes)
                   ?? htmlHelper.DropDownList(name, selectListItems, optionLabel, htmlAttributes);
        }

        /// <summary>
        /// The smart drop down list.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownList(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList, 
            string optionLabel, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartDropDownList(
                htmlHelper, 
                name, 
                selectList != null ? selectList() : null, 
                optionLabel, 
                htmlAttributes);
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList)
        {
            return SmartDropDownListFor(
                htmlHelper, 
                expression, 
                selectList, 
                null /* optionLabel */, 
                null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList)
        {
            return SmartDropDownListFor(
                htmlHelper, 
                expression, 
                selectList, 
                null /* optionLabel */, 
                null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList, 
            object htmlAttributes)
        {
            return SmartDropDownListFor(
                htmlHelper, 
                expression, 
                selectList, 
                null /* optionLabel */, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList, 
            object htmlAttributes)
        {
            return SmartDropDownListFor(
                htmlHelper, 
                expression, 
                selectList, 
                null /* optionLabel */, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartDropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartDropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList, 
            string optionLabel)
        {
            return SmartDropDownListFor(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList, 
            string optionLabel)
        {
            return SmartDropDownListFor(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList, 
            string optionLabel, 
            object htmlAttributes)
        {
            return SmartDropDownListFor(
                htmlHelper, 
                expression, 
                selectList, 
                optionLabel, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList, 
            string optionLabel, 
            object htmlAttributes)
        {
            return SmartDropDownListFor(
                htmlHelper, 
                expression, 
                selectList, 
                optionLabel, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList, 
            string optionLabel, 
            IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            IList<SelectListItem> selectListItems = selectList as IList<SelectListItem> ?? selectList.ToList();

            return GetReadonlyValue(metadata, selectListItems, htmlAttributes)
                   ?? htmlHelper.DropDownListFor(expression, selectListItems, optionLabel, htmlAttributes);
        }

        /// <summary>
        /// The smart drop down list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList, 
            string optionLabel, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartDropDownListFor(
                htmlHelper, 
                expression, 
                selectList != null ? selectList() : null, 
                optionLabel, 
                htmlAttributes);
        }

        // ListBox

        /// <summary>
        /// The smart list box.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBox(this HtmlHelper htmlHelper, string name)
        {
            return SmartListBox(
                htmlHelper, 
                name, 
                (IEnumerable<SelectListItem>)null /* selectList */, 
                null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart list box.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBox(
            this HtmlHelper htmlHelper, 
            string name, 
            IEnumerable<SelectListItem> selectList)
        {
            return SmartListBox(htmlHelper, name, selectList, null);
        }

        /// <summary>
        /// The smart list box.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBox(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList)
        {
            return SmartListBox(htmlHelper, name, selectList, null);
        }

        /// <summary>
        /// The smart list box.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBox(
            this HtmlHelper htmlHelper, 
            string name, 
            IEnumerable<SelectListItem> selectList, 
            object htmlAttributes)
        {
            return SmartListBox(
                htmlHelper, 
                name, 
                selectList, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart list box.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBox(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList, 
            object htmlAttributes)
        {
            return SmartListBox(
                htmlHelper, 
                name, 
                selectList, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart list box.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBox(
            this HtmlHelper htmlHelper, 
            string name, 
            IEnumerable<SelectListItem> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromStringExpression(name, htmlHelper.ViewData);

            IList<SelectListItem> selectListItems = selectList as IList<SelectListItem> ?? selectList.ToList();

            return GetReadonlyValue(metadata, selectListItems, htmlAttributes)
                   ?? htmlHelper.ListBox(name: name, selectList: selectListItems, htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// The smart list box.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBox(
            this HtmlHelper htmlHelper, 
            string name, 
            Func<IEnumerable<SelectListItem>> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartListBox(htmlHelper, name, selectList != null ? selectList() : null, htmlAttributes);
        }

        /// <summary>
        /// The smart list box for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList)
        {
            return SmartListBoxFor(htmlHelper, expression, selectList, null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart list box for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList)
        {
            return SmartListBoxFor(htmlHelper, expression, selectList, null /* htmlAttributes */);
        }

        /// <summary>
        /// The smart list box for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList, 
            object htmlAttributes)
        {
            return SmartListBoxFor(
                htmlHelper, 
                expression, 
                selectList, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart list box for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList, 
            object htmlAttributes)
        {
            return SmartListBoxFor(
                htmlHelper, 
                expression, 
                selectList, 
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// The smart list box for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static MvcHtmlString SmartListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            IList<SelectListItem> selectListItems = selectList as IList<SelectListItem> ?? selectList.ToList();

            return GetReadonlyValue(metadata, selectListItems, htmlAttributes)
                   ?? htmlHelper.ListBoxFor(expression, selectListItems, htmlAttributes);
        }

        /// <summary>
        /// The smart list box for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SmartListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            Func<IEnumerable<SelectListItem>> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartListBoxFor(htmlHelper, expression, selectList != null ? selectList() : null, htmlAttributes);
        }

        #endregion

        #region Methods


        /// <summary>
        /// The get readonly value.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        private static MvcHtmlString GetReadonlyValue(
            ModelMetadata metadata, 
            IEnumerable<SelectListItem> selectList, 
            IDictionary<string, object> htmlAttributes)
        {
            if (!metadata.ShowForEdit)
            {
                return MvcHtmlString.Empty;
            }

            if (metadata.IsReadOnly)
            {
                if (!metadata.AdditionalValues.ContainsKey(Utilities.DisplayAsDisabledInputKey))
                {
                    IEnumerable<SelectListItem> selected = selectList.Where(i => i.Selected);
                    IList<SelectListItem> selectListItems = selected as IList<SelectListItem> ?? selected.ToList();

                    return selectListItems.Any()
                               ? MvcHtmlString.Create(string.Join(", ", selectListItems.Select(s => s.Text)))
                               : MvcHtmlString.Empty;
                }

                htmlAttributes["readonly"] = "readonly";
            }

            return null;
        }

        #endregion
    }
}