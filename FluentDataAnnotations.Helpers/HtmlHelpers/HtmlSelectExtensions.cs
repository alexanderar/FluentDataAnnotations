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
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    ///     The html select extensions.
    /// </summary>
    public static class HtmlSelectExtensions
    {
        // DropDownList
        #region Public Methods and Operators

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
            IEnumerable<SelectListItem> selectList)
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
                (string)null /* optionLabel */, 
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (string)null /* optionLabel */, 
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (string)null /* optionLabel */, 
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (string)null /* optionLabel */, 
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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
                               ? MvcHtmlString.Create(string.Join((string)", ", (IEnumerable<string>)selectListItems.Select(s => s.Text)))
                               : MvcHtmlString.Empty;
                }

                htmlAttributes["disabled"] = "disabled";
            }

            return null;
        }

        #endregion
    }
}