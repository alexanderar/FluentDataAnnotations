// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFluentAnnotation.cs" company="">
//   
// </copyright>
// <summary>
//   The FluentAnnotation interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace FluentDataAnnotations
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The FluentAnnotation interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IFluentAnnotation<T>
        where T : class
    {
    }

    /// <summary>
    ///     The FluentAnnotation interface.
    /// </summary>
    public interface IFluentAnnotation
    {
        #region Public Methods and Operators

        /// <summary>
        /// The data type.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string DataType(string propName);

        /// <summary>
        /// The description.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string Description(string propName);

        /// <summary>
        /// The display format.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        DisplayFormat DisplayFormat(string propName);

        /// <summary>
        /// The display name.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string DisplayName(string propName);

        /// <summary>
        /// The hidden input.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool HiddenInput(string propName);

        /// <summary>
        /// The is read only.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        ReadOnlyFormat IsReadOnly(string propName);

        /// <summary>
        /// The show for display.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ShowForDisplay(string propName);

        /// <summary>
        /// The mask pattern.
        /// </summary>
        /// <param name="propName">
        ///     The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="Regex"/>.
        /// </returns>
        ValueTransform ValueTransform(string propName);

        /// <summary>
        /// The show for edit.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ShowForEdit(string propName);

        /// <summary>
        /// The ui hint.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string UIHint(string propName);

        #endregion
    }
}