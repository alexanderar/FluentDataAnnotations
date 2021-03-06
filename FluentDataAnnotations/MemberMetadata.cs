﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberMetadata.cs" company="">
//   
// </copyright>
// <summary>
//   The fluent member meta-data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace FluentDataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// The fluent member meta-data.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class MemberMetadata<T>
        where T : class
    {
        #region Fields

        /// <summary>
        ///     The _is drop down.
        /// </summary>
        protected internal bool _isDropDown;

        /// <summary>
        ///     The _select list drop down func.
        /// </summary>
        protected internal Func<IList<SelectListItem>> _selectListDropDownFunc;

        /// <summary>
        ///     The _custom data type.
        /// </summary>
        private string _customDataType;

        /// <summary>
        ///     Gets the data type.
        /// </summary>
        private DataType _dataType;

        /// <summary>
        ///     The description.
        /// </summary>
        private string _description;

        /// <summary>
        ///     The description function.
        /// </summary>
        private Func<string> _descriptionFunc;

        /// <summary>
        ///     The _display as disabled input.
        /// </summary>
        private bool _displayAsDisabledInput;

        /// <summary>
        ///     The _display format.
        /// </summary>
        private DisplayFormat _displayFormat;

        /// <summary>
        ///     The _display format function.
        /// </summary>
        private Func<DisplayFormat> _displayFormatFunc;

        /// <summary>
        ///     The display name.
        /// </summary>
        private string _displayName;

        /// <summary>
        ///     The display name function.
        /// </summary>
        private Func<string> _displayNameFunc;

        /// <summary>
        /// The _is cascade drop down.
        /// </summary>
        private bool _isCascadeDropDown;

        /// <summary>
        ///     The _is custom data type set.
        /// </summary>
        private bool _isCustomDataTypeSet;

        /// <summary>
        ///     The _is data type set.
        /// </summary>
        private bool _isDataTypeSet;

        /// <summary>
        ///     The is description set.
        /// </summary>
        private bool _isDescriptionSet;

        /// <summary>
        ///     Is display format set.
        /// </summary>
        private bool _isDisplayFormatSet;

        /// <summary>
        ///     The _is display name set.
        /// </summary>
        private bool _isDisplayNameSet;

        /// <summary>
        ///     The is read only.
        /// </summary>
        private ReadOnlyFormat _isReadOnly;

        /// <summary>
        ///     The _select list drop down.
        /// </summary>
        private IList<SelectListItem> _selectListDropDown;

        /// <summary>
        ///     The _select list drop down from model func.
        /// </summary>
        private Func<T, IEnumerable<SelectListItem>> _selectListDropDownFromModelFunc;

        /// <summary>
        ///     The is visible.
        /// </summary>
        private bool? _showForDisplay;

        /// <summary>
        ///     The is visible function.
        /// </summary>
        private Func<bool> _showForDisplayFunc;

        /// <summary>
        ///     The is editable.
        /// </summary>
        private bool? _showForEdit;

        /// <summary>
        ///     The is editable function.
        /// </summary>
        private Func<bool> _showForEditFunc;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberMetadata{T}"/> class.
        ///     Initializes a new instance of the <see cref="MemberMetadata"/> class.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        public MemberMetadata(MemberInfo member)
        {
            this.Member = member;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the cascade action param.
        /// </summary>
        public string CascadeActionParam { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether cascade disabled when parent not selected.
        /// </summary>
        public bool CascadeDisabledWhenParentNotSelected { get; set; }

        /// <summary>
        /// Gets or sets the cascade option label.
        /// </summary>
        public string CascadeOptionLabel { get; set; }

        /// <summary>
        /// Gets or sets the cascade triggered by property id.
        /// </summary>
        public string CascadeTriggeredByPropertyId { get; set; }

        private string _CascadeUrl;

        private Func<string> _CascadeUrlFunc;       

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the data type.
        /// </summary>
        internal string DataType
        {
            get
            {
                if (this._isDataTypeSet)
                {
                    return this._dataType.ToString();
                }

                if (this._isCustomDataTypeSet)
                {
                    return this._customDataType;
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the description.
        /// </summary>
        internal string Description
        {
            get
            {
                if (this._isDescriptionSet)
                {
                    return this._description;
                }

                if (this._descriptionFunc != null)
                {
                    this._description = this._descriptionFunc();
                    this._isDescriptionSet = true;
                }

                return this._description;
            }
        }

        /// <summary>
        ///     Gets the display format.
        /// </summary>
        internal DisplayFormat DisplayFormat
        {
            get
            {
                if (this._isDisplayFormatSet)
                {
                    return this._displayFormat;
                }

                if (this._displayFormatFunc != null)
                {
                    this._displayFormat = this._displayFormatFunc();
                    this._isDisplayNameSet = true;
                }

                return this._displayFormat;
            }
        }

        /// <summary>
        ///     Gets the display name.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        internal string DisplayName
        {
            get
            {
                if (this._isDisplayNameSet)
                {
                    return this._displayName;
                }

                if (this._displayNameFunc != null)
                {
                    this._displayName = this._displayNameFunc();
                    this._isDisplayNameSet = true;
                }

                return this._displayName;
            }
        }

        internal string CascadeUrl
        {
            get
            {
                if (!this._CascadeUrl.IsNullOrWhiteSpace())
                {
                    return this._CascadeUrl;
                }

                if (this._CascadeUrlFunc != null)
                {
                    this._CascadeUrl = this._CascadeUrlFunc();
                }

                return this._CascadeUrl;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether hidden input.
        /// </summary>
        internal bool HiddenInput { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether is read only.
        /// </summary>
        internal ReadOnlyFormat IsReadOnly
        {
            get
            {
                return this._isReadOnly;
            }
        }

        /// <summary>
        ///     Gets the Member.
        /// </summary>
        internal MemberInfo Member { get; private set; }

        /// <summary>
        ///     Gets the option label for drop down.
        /// </summary>
        internal string OptionLabelForDropDown { get; private set; }

        /// <summary>
        ///     Gets the select list for drop down.
        /// </summary>
        internal IList<SelectListItem> SelectListForDropDown
        {
            get
            {
                if (!this._isDropDown || (this._selectListDropDown == null && this._selectListDropDownFunc == null))
                {
                    return null;
                }

                if (this._selectListDropDownFunc != null)
                {
                    this._selectListDropDown = this._selectListDropDownFunc();
                }

                return this._selectListDropDown;
            }
        }

        /// <summary>
        ///     Gets the select list for drop down from model.
        /// </summary>
        internal Func<T, IEnumerable<SelectListItem>> SelectListForDropDownFromModel
        {
            get
            {
                if (!this._isDropDown || (this._selectListDropDownFromModelFunc == null))
                {
                    return null;
                }

                return this._selectListDropDownFromModelFunc;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether is visible on display.
        /// </summary>
        internal bool ShowForDisplay
        {
            get
            {
                if (this._showForDisplay.HasValue)
                {
                    return this._showForDisplay.Value;
                }

                if (this._showForDisplayFunc != null)
                {
                    this._showForDisplay = this._showForDisplayFunc();
                }

                return this._showForDisplay ?? true;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether is the field visible on edit.
        /// </summary>
        internal bool ShowForEdit
        {
            get
            {
                if (this._showForEdit.HasValue)
                {
                    return this._showForEdit.Value;
                }

                if (this._showForEditFunc != null)
                {
                    this._showForEdit = this._showForEditFunc();
                }

                return this._showForEdit ?? true;
            }
        }

        /// <summary>
        ///     Gets the UI hint.
        /// </summary>
        internal string UIHint { get; private set; }

        /// <summary>
        ///     Gets the value format function
        /// </summary>
        internal ValueTransform ValueTransform { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The apply value transformation. 
        /// If the property is read only and it is rendered NOT as disabled input but as display template, 
        /// the transform applied only to the display template value. 
        /// Corresponding hidden input will have the original model value
        /// </summary>
        /// <param name="valueTransformFunc">
        /// The value transform function.
        /// </param>
        /// <param name="applyTransformInEditMode">
        /// Indicates whether to apply transform in edit mode.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> ApplyValueTransform(
            Func<object, string> valueTransformFunc, 
            bool applyTransformInEditMode = true)
        {
            this.ValueTransform = new ValueTransform
                                      {
                                          ValueTransformFunc = valueTransformFunc, 
                                          ApplyTransformInEditMode = applyTransformInEditMode
                                      };
            return this;
        }

        /// <summary>
        /// The set cascading drop down.
        /// </summary>
        /// <param name="triggeredByProperty">
        /// The triggered by property.
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
        /// <typeparam name="TProp">
        /// Type of property
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberMetadata{T}"/>.
        /// </returns>
        public MemberMetadata<T> SetCascadingDropDown<TProp>(
            Expression<Func<T, TProp>> triggeredByProperty, 
            string url, 
            string actionParam, 
            string optionLabel = "", 
            bool disabledWhenParentNotSelected = false)
        {
            if (triggeredByProperty != null)
            {
                MemberInfo triggerMemberInfo = Utilities.GetMemberInfo(triggeredByProperty);
                if (triggerMemberInfo == null)
                {
                    return this;
                }

                this.CascadeTriggeredByPropertyId = triggerMemberInfo.Name;
                this._CascadeUrl = url;
                this.CascadeOptionLabel = optionLabel;
                this.CascadeActionParam = actionParam;
                this.CascadeDisabledWhenParentNotSelected = disabledWhenParentNotSelected;
                this._isCascadeDropDown = true;
            }

            return this;
        }

        /// <summary>
        /// The set cascading drop down.
        /// </summary>
        /// <param name="triggeredByProperty">
        /// The triggered by property.
        /// </param>
        /// <param name="urlFunc">
        /// The URL function.
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
        /// <typeparam name="TProp">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberMetadata{T}"/>.
        /// </returns>
        public MemberMetadata<T> SetCascadingDropDown<TProp>(
            Expression<Func<T, TProp>> triggeredByProperty,
            Func<string> urlFunc,
            string actionParam,
            string optionLabel = "",
            bool disabledWhenParentNotSelected = false)
        {
            if (triggeredByProperty != null)
            {
                MemberInfo triggerMemberInfo = Utilities.GetMemberInfo(triggeredByProperty);
                if (triggerMemberInfo == null)
                {
                    return this;
                }

                this.CascadeTriggeredByPropertyId = triggerMemberInfo.Name;
                this._CascadeUrlFunc = urlFunc;
                this.CascadeOptionLabel = optionLabel;
                this.CascadeActionParam = actionParam;
                this.CascadeDisabledWhenParentNotSelected = disabledWhenParentNotSelected;
                this._isCascadeDropDown = true;
            }

            return this;
        }

        /// <summary>
        /// The set data type.
        /// </summary>
        /// <param name="dataType">
        /// The data type.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDataType(DataType dataType)
        {
            this._dataType = dataType;
            this._isDataTypeSet = true;
            return this;
        }

        /// <summary>
        /// Sets DataType for a property.
        /// </summary>
        /// <param name="dataType">
        /// The data type.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDataType(string dataType)
        {
            this._customDataType = dataType;
            this._isCustomDataTypeSet = true;
            return this;
        }

        /// <summary>
        /// Sets description for a property
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDescription(string description)
        {
            this._description = description;
            return this;
        }

        /// <summary>
        /// Sets description for a property
        /// </summary>
        /// <param name="description">
        /// The function that returns a description.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDescription(Func<string> description)
        {
            this._descriptionFunc = description;
            return this;
        }

        /// <summary>
        /// Sets display format for a property
        /// </summary>
        /// <param name="displayFormat">
        /// The display format.
        /// </param>
        /// <param name="applyFormatInEditMode">
        /// Indicates whether to apply format in edit mode.
        /// </param>
        /// <param name="htmlEncode">
        /// Indicates whether to perform HTML encode.
        /// </param>
        /// <param name="nullDisplayText">
        /// Text that will be displayed if the value is null
        /// </param>
        /// <param name="convertEmptyStringToNull">
        /// Indicates whether to convert empty string to null.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDisplayFormat(
            string displayFormat, 
            bool applyFormatInEditMode = false, 
            bool htmlEncode = false, 
            string nullDisplayText = "", 
            bool convertEmptyStringToNull = true)
        {
            this._displayFormat = new DisplayFormat
                                      {
                                          ApplyFormatInEditMode = applyFormatInEditMode, 
                                          DisplayFormatString = displayFormat, 
                                          ConvertEmptyStringToNull = convertEmptyStringToNull, 
                                          HtmlEncode = htmlEncode, 
                                          NullDisplayText = nullDisplayText
                                      };

            return this;
        }

        /// <summary>
        /// Sets display format for a property
        /// </summary>
        /// <param name="displayFormatFunc">
        /// The function that returns display format.
        /// </param>
        /// <param name="applyFormatInEditMode">
        /// Indicates whether to apply format in edit mode.
        /// </param>
        /// <param name="htmlEncode">
        /// Indicates whether to perform HTML encode.
        /// </param>
        /// <param name="nullDisplayText">
        /// Text that will be displayed if the value is null
        /// </param>
        /// <param name="convertEmptyStringToNull">
        /// Indicates whether to convert empty string to null.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDisplayFormat(
            Func<string> displayFormatFunc, 
            bool applyFormatInEditMode = true, 
            bool htmlEncode = false, 
            string nullDisplayText = "", 
            bool convertEmptyStringToNull = true)
        {
            this._displayFormat = new DisplayFormat
                                      {
                                          ApplyFormatInEditMode = applyFormatInEditMode, 
                                          DisplayFormatStringFunc = displayFormatFunc, 
                                          ConvertEmptyStringToNull = convertEmptyStringToNull, 
                                          HtmlEncode = htmlEncode, 
                                          NullDisplayText = nullDisplayText
                                      };

            return this;
        }

        /// <summary>
        /// Sets the display name.
        /// </summary>
        /// <param name="name">
        /// The display name.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDisplayName(Func<string> name)
        {
            this._displayNameFunc = name;
            return this;
        }

        /// <summary>
        /// Sets the display name.
        /// </summary>
        /// <param name="name">
        /// The function that returns display name.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDisplayName(string name)
        {
            this._displayName = name;
            return this;
        }

        /// <summary>
        /// The set drop down.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <typeparam name="TSelect">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberMetadata{T}"/>.
        /// </returns>
        public MemberMetadata<T> SetDropDown<TSelect>(Expression<Func<T, TSelect>> property, string optionLabel = null, bool IsMultiSelct = false)
            where TSelect : IEnumerable<SelectListItem>
        {
            if (property != null)
            {
                Expression<Func<T, IEnumerable<SelectListItem>>> expr =
                    Expression.Lambda<Func<T, IEnumerable<SelectListItem>>>(property.Body, property.Parameters);
                Func<T, IEnumerable<SelectListItem>> func = expr.Compile();
                this._selectListDropDownFromModelFunc = func;
                this.OptionLabelForDropDown = optionLabel;
                this.IsMultiSelctForDropDown = IsMultiSelct;
                this._isDropDown = true;
            }

            return this;
        }

        internal bool IsMultiSelctForDropDown { get; private set; }

        /// <summary>
        /// The set drop down.
        /// </summary>
        /// <param name="selectListFunc">
        /// The select list func.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata{T}"/>.
        /// </returns>
        public MemberMetadata<T> SetDropDown(Func<IList<SelectListItem>> selectListFunc, string optionLabel = null, bool IsMultiSelct = false)
        {
            this._selectListDropDownFunc = selectListFunc;
            this.OptionLabelForDropDown = optionLabel;
            this.IsMultiSelctForDropDown = IsMultiSelct;
            this._isDropDown = true;
            return this;
        }

        /// <summary>
        /// The set drop down.
        /// </summary>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetDropDown(IList<SelectListItem> selectList, string optionLabel = null, bool IsMultiSelct = false)
        {
            this._selectListDropDown = selectList;
            this.OptionLabelForDropDown = optionLabel;
            this.IsMultiSelctForDropDown = IsMultiSelct;
            this._isDropDown = true;
            return this;
        }

        /// <summary>
        ///     Applies hidden input attribute to a property.
        /// </summary>
        /// <returns>
        ///     The <see cref="MemberMetadata" />.
        /// </returns>
        public MemberMetadata<T> SetHiddenInput()
        {
            this.HiddenInput = true;
            return this;
        }

        /// <summary>
        /// Sets the value that indicates whether the property should be displayed as read only property.
        /// </summary>
        /// <param name="isReadOnly">
        /// Function that returns a boolean that indicates whether the property should be displayed as read only property.
        /// </param>
        /// <param name="displayAsReadOnlyInput">
        /// If read only is set to true, indicates whether to render the property as disabled input.
        ///     If set to true, the property will be rendered as a disabled input tag.
        ///     If set to false, the property will be rendered by using  @Html.Encode().
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetReadOnly(Func<bool> isReadOnly, bool displayAsReadOnlyInput = true)
        {
            this._isReadOnly = new ReadOnlyFormat(isReadOnly, displayAsReadOnlyInput);

            return this;
        }

        /// <summary>
        /// Sets the value that indicates whether the property should be displayed as read only property.
        /// </summary>
        /// <param name="isReadOnly">
        /// indicates whether the property should be displayed as read only property.
        /// </param>
        /// <param name="displayAsReadOnlyInput">
        /// If read only is set to true, indicates whether to render the property as disabled input.
        ///     If set to true, the property will be rendered as a disabled input tag.
        ///     If set to false, the property will be rendered by using  @Html.Encode().
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetReadOnly(bool isReadOnly, bool displayAsReadOnlyInput = true)
        {
            this._isReadOnly = new ReadOnlyFormat(isReadOnly, displayAsReadOnlyInput);
            return this;
        }

        /// <summary>
        /// Sets the value that indicates whether to render this property in a display mode(For example when used
        ///     Html.DiplayFor()).
        /// </summary>
        /// <param name="isVisible">
        /// Indicates whether to render this property in a display mode
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetShowForDisplay(bool isVisible)
        {
            this._showForDisplay = isVisible;
            return this;
        }

        /// <summary>
        /// Sets the value that indicates whether to render this property in a display mode(For example when used
        ///     Html.DiplayFor()).
        /// </summary>
        /// <param name="isVisible">
        /// Function that returns a boolean that indicates whether to render this property in a display mode
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetShowForDisplay(Func<bool> isVisible)
        {
            this._showForDisplayFunc = isVisible;
            return this;
        }

        /// <summary>
        /// Sets the value that indicates whether to render this property in a edit mode(For example when used
        ///     Html.EditorFor()).
        /// </summary>
        /// <param name="isEditable">
        /// Function that returns a boolean that indicates whether to render this property in a edit mode
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetShowForEdit(Func<bool> isEditable)
        {
            this._showForEditFunc = isEditable;
            return this;
        }

        /// <summary>
        /// Sets the value that indicates whether to render this property in a edit mode(For example when used
        ///     Html.EditorFor()).
        /// </summary>
        /// <param name="isEditable">
        /// Indicates whether to render this property in a edit mode
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetShowForEdit(bool isEditable)
        {
            this._showForEdit = isEditable;
            return this;
        }

        /// <summary>
        /// Sets the name of the template to use to display the property.
        /// </summary>
        /// <param name="uiHint">
        /// The template name
        /// </param>
        /// <returns>
        /// The <see cref="MemberMetadata"/>.
        /// </returns>
        public MemberMetadata<T> SetUIHint(string uiHint)
        {
            this.UIHint = uiHint;
            return this;
        }

        #endregion
    }
}