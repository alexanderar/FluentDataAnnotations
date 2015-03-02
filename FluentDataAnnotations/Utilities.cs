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
    /// <summary>
    ///     The utilities.
    /// </summary>
    public static class Utilities
    {
        #region Constants

        /// <summary>
        /// The display as disabled input key.
        /// </summary>
        public const string DisplayAsDisabledInputKey = "FluentDataAnnotations.DisplayAsDisabledInput";

        /// <summary>
        /// The show label for display key.
        /// </summary>
        public const string ShowLabelForDisplayKey = "FluentDataAnnotations.ShowLabelForDisplay";

        /// <summary>
        /// The show label for edit key.
        /// </summary>
        public const string ShowLabelForEditKey = "FluentDataAnnotations.ShowLabelForEdit";

        public const string SelectListKey = "FluentDataAnnotations.SelectListKey";

        public const string DropDown = "FluentDataAnnotations.DropDown";

        #endregion

        #region Public Methods and Operators

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