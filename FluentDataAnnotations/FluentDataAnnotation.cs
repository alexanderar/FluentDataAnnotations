// --------------------------------------------------------------------------------------------------------------------
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
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    /// <summary>
    /// The fluent data annotation.
    /// </summary>
    /// <typeparam name="T">
    /// View model type
    /// </typeparam>
    public abstract class FluentDataAnnotation<T> : IFluentAnnotation<T>
        where T : class
    {
        #region Fields

        /// <summary>
        ///     The camel case regular expression.
        /// </summary>
        private readonly Regex _camelCaseRegex = new Regex(
            @"(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])", 
            RegexOptions.Compiled);

        /// <summary>
        ///     The _model actions.
        /// </summary>
        private readonly IList<Tuple<Func<T, bool>, Action>> _modelActions = new List<Tuple<Func<T, bool>, Action>>();

        /// <summary>
        ///     The model meta-data.
        /// </summary>
        private readonly Dictionary<string, MemberMetadata<T>> _modelMetadata =
            new Dictionary<string, MemberMetadata<T>>();

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

        ///// <summary>
        ///// The get conditional actions.
        ///// </summary>
        ///// <returns>
        ///// The <see cref="IList{T}"/>.
        ///// </returns>
        // IFluentAnnotation.IList<Tuple<Func<T, bool>, Action>> GetConditionalActions()
        // {
        // var modelTypeName = typeof(T).FullName;
        // return this._modelActions.ContainsKey(modelTypeName) ? this._modelActions[modelTypeName] : null;
        // }

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
        public MemberMetadata<T> For<TProp>(Expression<Func<T, TProp>> propSelector)
        {
            MemberMetadata<T> member = this.GetInfo(propSelector);
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
        /// The get info.
        /// </summary>
        /// <param name="propSelector">
        /// The prop selector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TProp">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberMetadata{T}"/>.
        /// </returns>
        private MemberMetadata<T> GetInfo<TProp>(Expression<Func<T, TProp>> propSelector)
        {
            var body = propSelector.Body as MemberExpression;
            return body != null ? new MemberMetadata<T>(body.Member) : null;
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
        /// The when.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        public void When(Func<T, bool> predicate, Action action)
        {
            this._modelActions.Add(new Tuple<Func<T, bool>, Action>(predicate, action));
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The get conditional actions.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        IList<Tuple<Func<bool>, Action>> IFluentAnnotation.GetConditionalActions(object target)
        {
            if (target == null)
            {
                return new List<Tuple<Func<bool>, Action>>();
            }

            var model = (T)target;
            return target.GetType() == typeof(T)
                       ? this._modelActions.Select(t => new Tuple<Func<bool>, Action>(() => t.Item1(model), t.Item2))
                             .ToList()
                       : new List<Tuple<Func<bool>, Action>>();
        }

        /// <summary>
        /// The select list for drop down.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="propName">
        ///     The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        DropDownProperties IFluentAnnotation.DropDownProperties(object target, string propName)
        {
            if (this._modelMetadata.ContainsKey(propName))
            {
                var props = new DropDownProperties
                                {
                                    OptionLabel =
                                        this._modelMetadata[propName].OptionLabelForDropDown
                                };
                if (this._modelMetadata[propName].SelectListForDropDown != null)
                {
                    props.SelectList = this._modelMetadata[propName].SelectListForDropDown;
                    return props;
                }

                if (this._modelMetadata[propName].SelectListForDropDownFromModel != null && target != null)
                {
                    var model = target as T;
                    if (model != null)
                    {
                        props.SelectList = this._modelMetadata[propName].SelectListForDropDownFromModel(model);
                        return props;
                    }
                }
            }

            return null;
        }

        #endregion

        #region Methods

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
            name = this._camelCaseRegex.Replace(name, " ");
            if (name.EndsWith(" Id"))
            {
                name = name.Substring(0, name.Length - 3);
            }

            return name;
        }

        #endregion
    }
}