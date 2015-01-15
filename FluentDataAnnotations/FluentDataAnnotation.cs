﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="FluentDataAnnotation.cs">
//   
// </copyright>
// <summary>
//   The fluent data annotation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FluentDataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The fluent data annotation.
    /// </summary>
    /// <typeparam name="T">
    /// View model type
    /// </typeparam>
    public abstract class FluentDataAnnotation<T> : IFluentAnnotation, IFluentAnnotation<T>
    {
        #region Fields

        /// <summary>
        ///     The model meta-data.
        /// </summary>
        private readonly Dictionary<string, MemberMetadata> _modelMetadata = new Dictionary<string, MemberMetadata>();

        /// <summary>
        /// The _model actions.
        /// </summary>
        private readonly Dictionary<string, IList<Tuple<Func<T, bool>, Action>>> _modelActions
            = new Dictionary<string, IList<Tuple<Func<T, bool>, Action>>>();

        /// <summary>
        ///     The camel case regular expression.
        /// </summary>
        private readonly Regex _camelCaseRegex = new Regex(@"\B\p{Lu}\p{Ll}", RegexOptions.Compiled);

        #endregion

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
        public string DataType(string propName)
        {
            return this._modelMetadata.ContainsKey(propName) ? this._modelMetadata[propName].DataType : null;
        }

        /// <summary>
        /// The description.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Description(string propName)
        {
            return this._modelMetadata.ContainsKey(propName) ? this._modelMetadata[propName].Description : null;
        }

        /// <summary>
        /// The display format.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="DisplayFormat"/>.
        /// </returns>
        public DisplayFormat DisplayFormat(string propName)
        {
            return this._modelMetadata.ContainsKey(propName) ? this._modelMetadata[propName].DisplayFormat : null;
        }

        /// <summary>
        /// The get name.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DisplayName(string propName)
        {
            if (this._modelMetadata.ContainsKey(propName)
                && !this._modelMetadata[propName].DisplayName.IsNullOrWhiteSpace())
            {
                return this._modelMetadata[propName].DisplayName;
            }

            return this.DisplayNameFromCamelCase(propName);
        }

        /// <summary>
        /// The hidden input.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool HiddenInput(string propName)
        {
            return this._modelMetadata.ContainsKey(propName) && this._modelMetadata[propName].HiddenInput;
        }

        /// <summary>
        /// The for.
        /// </summary>
        /// <param name="propSelector">
        /// The prop selector.
        /// </param>
        /// <typeparam name="TProp">
        /// Type of property
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata For<TProp>(Expression<Func<T, TProp>> propSelector)
        {
            var member = GetInfo(propSelector);
            if (member == null)
            {
                throw new ArgumentException("propSelector");
            }

            if (this._modelMetadata.ContainsKey(member.Member.Name) == false)
            {
                this._modelMetadata[member.Member.Name] = member;
            }

            return this._modelMetadata[member.Member.Name];
        }

        /// <summary>
        /// Defines a condition for setting the annotations 
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        public void When(Func<T, bool> predicate, Action action)
        {
            var modelTypeName = typeof(T).FullName;
            if (!this._modelActions.ContainsKey(modelTypeName))
            {
                this._modelActions.Add(modelTypeName, new List<Tuple<Func<T, bool>, Action>>());
            }

            this._modelActions[modelTypeName].Add(new Tuple<Func<T, bool>, Action>(predicate, action));
        }

        /// <summary>
        /// The get actions.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Tuple<Func<T, bool>, Action>> GetConditionalActions()
        {
            var modelTypeName = typeof(T).FullName;
            return this._modelActions.ContainsKey(modelTypeName) ? this._modelActions[modelTypeName] : null;
        }

        /// <summary>
        /// The is read only.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public ReadOnlyFormat IsReadOnly(string propName)
        {
            return this._modelMetadata.ContainsKey(propName) ? this._modelMetadata[propName].IsReadOnly : null;
        }

        /// <summary>
        /// The is visible on display.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ShowForDisplay(string propName)
        {
            if (this._modelMetadata.ContainsKey(propName))
            {
                return this._modelMetadata[propName].ShowForDisplay;
            }

            return true;
        }

        /// <summary>
        /// The value transform.
        /// </summary>
        /// <param name="propName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTransform"/>.
        /// </returns>
        public ValueTransform ValueTransform(string propName)
        {
            if (this._modelMetadata.ContainsKey(propName))
            {
                return this._modelMetadata[propName].ValueTransform;
            }

            return null;
        }


        /// <summary>
        /// The is visible on edit.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ShowForEdit(string propName)
        {
            if (this._modelMetadata.ContainsKey(propName))
            {
                return this._modelMetadata[propName].ShowForEdit;
            }

            return true;
        }

        /// <summary>
        /// The UI hint.
        /// </summary>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string UIHint(string propName)
        {
            return this._modelMetadata.ContainsKey(propName) ? this._modelMetadata[propName].UIHint : null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get info.
        /// </summary>
        /// <param name="propSelector">
        /// The prop selector.
        /// </param>
        /// <typeparam name="TProp">
        /// Property type
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        private static MemberMetadata GetInfo<TProp>(Expression<Func<T, TProp>> propSelector)
        {
            var body = propSelector.Body as MemberExpression;
            return body != null ? new MemberMetadata(body.Member) : null;
        }

        /// <summary>
        /// The display name from camel case.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string DisplayNameFromCamelCase(string name)
        {
            name = this._camelCaseRegex.Replace(name, " $0");
            if (name.EndsWith(" Id"))
            {
                name = name.Substring(0, name.Length - 3);
            }

            return name;
        }

        #endregion
    }
}