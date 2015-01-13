// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayFormat.cs" company="">
//   
// </copyright>
// <summary>
//   The display format.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FluentDataAnnotations
{
    using System;

    /// <summary>
    ///     The display format.
    /// </summary>
    public class DisplayFormat
    {
        #region Fields

        /// <summary>
        ///     The is display format set.
        /// </summary>
        private bool _isDisplayFormatSet;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether apply format in edit mode.
        /// </summary>
        internal bool ApplyFormatInEditMode { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether convert empty string to null.
        /// </summary>
        internal bool ConvertEmptyStringToNull { get; set; }

        /// <summary>
        ///     Gets or sets the display format string.
        /// </summary>
        internal string DisplayFormatString { private get; set; }

        /// <summary>
        ///     Gets or sets the display format string function.
        /// </summary>
        internal Func<string> DisplayFormatStringFunc { private get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether html encode.
        /// </summary>
        internal bool HtmlEncode { get; set; }

        /// <summary>
        ///     Gets or sets the null display text.
        /// </summary>
        internal string NullDisplayText { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     The get display format string.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        internal string GetDisplayFormatString()
        {
            if (this._isDisplayFormatSet)
            {
                return this.DisplayFormatString;
            }

            if (this.DisplayFormatStringFunc != null)
            {
                this.DisplayFormatString = this.DisplayFormatStringFunc();
                this._isDisplayFormatSet = true;
            }

            return this.DisplayFormatString;
        }

        #endregion
    }
}