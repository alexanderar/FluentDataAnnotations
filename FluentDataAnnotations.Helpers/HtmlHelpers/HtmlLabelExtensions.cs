// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlLabelExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The html label extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FluentDataAnnotations.Helpers.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    ///     The html label extensions.
    /// </summary>
    public static class HtmlLabelExtensions
    {
        #region Public Methods and Operators

        public static MvcHtmlString LabelForDisplay<TModel, TValue>(
           this HtmlHelper<TModel> html,
           Expression<Func<TModel, TValue>> expression)
        {
            return SmartLabelFor(html, expression, new RouteValueDictionary(), true);
        }

        /// <summary>
        /// The label for display.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString LabelForDisplay<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            object htmlAttributes)
        {
            return SmartLabelFor(html, expression, new RouteValueDictionary(htmlAttributes), false);
        }

        /// <summary>
        /// The label for display.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString LabelForDisplay<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartLabelFor(html, expression, htmlAttributes, false);
        }

        /// <summary>
        /// The label for.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString LabelForEdit<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            object htmlAttributes)
        {
            return SmartLabelFor(html, expression, new RouteValueDictionary(htmlAttributes), true);
        }

        public static MvcHtmlString LabelForEdit<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            return SmartLabelFor(html, expression, new RouteValueDictionary(), true);
        }

        /// <summary>
        /// The label for.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString LabelForEdit<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            IDictionary<string, object> htmlAttributes)
        {
            return SmartLabelFor(html, expression, htmlAttributes, true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The smart label for.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <param name="isForEdit">
        /// The is for edit.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="System.Web.Mvc.MvcHtmlString"/>.
        /// </returns>
        private static MvcHtmlString SmartLabelFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            IDictionary<string, object> htmlAttributes, 
            bool isForEdit)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var key = isForEdit ? Utilities.ShowLabelForEditKey : Utilities.ShowLabelForDisplayKey;
            if (metadata.AdditionalValues.ContainsKey(key) && (bool)metadata.AdditionalValues[key] == false)
            {
                return MvcHtmlString.Empty;
            }

            var labelText = metadata.GetDisplayName();
            if (string.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            var span = new TagBuilder("span");
            span.SetInnerText(labelText);

            // assign <span> to <label> inner html
            tag.InnerHtml = span.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        #endregion
    }
}