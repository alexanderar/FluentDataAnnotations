// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utilities.cs" company="">
//   
// </copyright>
// <summary>
//   The utilities.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace FluentDataAnnotations
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    ///     The utilities.
    /// </summary>
    public static class Utilities
    {
        #region Constants

        /// <summary>
        /// The cascade drop down properties key.
        /// </summary>
        public const string CascadeDropDownPropertiesKey = "FluentDataAnnotations.CascadeDropDownProperties";

        /// <summary>
        ///     The display as disabled input key.
        /// </summary>
        public const string DisplayAsDisabledInputKey = "FluentDataAnnotations.DisplayAsDisabledInput";

        /// <summary>
        /// The drop down.
        /// </summary>
        public const string DropDown = "FluentDataAnnotations.DropDown";

        /// <summary>
        /// The drop down properties key.
        /// </summary>
        public const string DropDownPropertiesKey = "FluentDataAnnotations.DropDownProperties";

        /// <summary>
        ///     The show label for display key.
        /// </summary>
        public const string ShowLabelForDisplayKey = "FluentDataAnnotations.ShowLabelForDisplay";

        /// <summary>
        ///     The show label for edit key.
        /// </summary>
        public const string ShowLabelForEditKey = "FluentDataAnnotations.ShowLabelForEdit";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get member info.
        /// </summary>
        /// <param name="propSelector">
        /// The prop selector.
        /// </param>
        /// <typeparam name="TModel">
        /// Type of model
        /// </typeparam>
        /// <typeparam name="TProp">
        /// Type of property in model
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberInfo"/>.
        /// </returns>
        public static MemberInfo GetMemberInfo<TModel, TProp>(Expression<Func<TModel, TProp>> propSelector)
        {
            var body = propSelector.Body as MemberExpression;
            return body != null ? body.Member : null;
        }

        /// <summary>
        /// The is null or white space.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        #endregion
    }
}