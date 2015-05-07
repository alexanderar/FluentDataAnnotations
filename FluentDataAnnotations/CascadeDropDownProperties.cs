// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CascadeDropDownProperties.cs" company="">
//   
// </copyright>
// <summary>
//   The cascade drop down properties.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace FluentDataAnnotations
{
    /// <summary>
    ///     The cascade drop down properties.
    /// </summary>
    public class CascadeDropDownProperties
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the action param.
        /// </summary>
        public string ActionParam { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether disabled when parent not selected.
        /// </summary>
        public bool DisabledWhenParentNotSelected { get; set; }

        /// <summary>
        ///     Gets or sets the option label.
        /// </summary>
        public string OptionLabel { get; set; }

        /// <summary>
        ///     Gets or sets the triggered by property id.
        /// </summary>
        public string TriggeredByPropertyId { get; set; }

        /// <summary>
        ///     Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        #endregion
    }
}