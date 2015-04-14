// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentModelMetadataProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The custom model metadata provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace FluentDataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    ///     The custom model meta-data provider.
    /// </summary>
    public class FluentModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        #region Fields

        /// <summary>
        ///     The _container.
        /// </summary>
        private readonly IContainer _container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentModelMetadataProvider"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public FluentModelMetadataProvider(IContainer container)
        {
            this._container = container;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create metadata.
        /// </summary>
        /// <param name="attributes">
        /// The attributes.
        /// </param>
        /// <param name="containerType">
        /// The container type.
        /// </param>
        /// <param name="modelAccessor">
        /// The model accessor.
        /// </param>
        /// <param name="modelType">
        /// The model type.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.ModelMetadata"/>.
        /// </returns>
        protected override ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes, 
            Type containerType, 
            Func<object> modelAccessor, 
            Type modelType, 
            string propertyName)
        {
            var attributesList = new List<Attribute>(attributes);

            if (containerType != null)
            {
                IFluentAnnotation metadataContainer = this.GetFluentAnnotationContainer(
                    containerType.FullName, 
                    containerType.Assembly.FullName);

                if (metadataContainer != null)
                {
                    if (modelAccessor != null)
                    {

                        var target = modelAccessor.Target;
                        var field = target.GetType().GetField("container");
                        if (field != null)
                        {
                            var model = field.GetValue(target);
                            var actions = metadataContainer.GetConditionalActions(model);
                            foreach (var a in actions)
                            {
                                if (a.Item1())
                                {
                                    a.Item2();
                                }
                            }
                        }                        
                    }
                    
                    this.SetDataTypeAndDisplayFormat(attributesList, metadataContainer, propertyName, modelAccessor);
                    this.SetHiddenInput(attributesList, metadataContainer, propertyName);
                    this.SetUIHint(attributesList, metadataContainer, propertyName);    
                }
            }

            ModelMetadata metadata = base.CreateMetadata(
                attributesList, 
                containerType, 
                modelAccessor, 
                modelType, 
                propertyName);

            return metadata;
        }

        /// <summary>
        /// The get model.
        /// </summary>
        /// <param name="modelAccessor">
        /// The model accessor.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object GetModel(Func<object> modelAccessor)
        {
            if (modelAccessor != null)
            {
                var target = modelAccessor.Target;
                var field = target.GetType().GetField("container");
                if (field != null)
                {
                    var model = field.GetValue(target);
                    return model;
                }
            }

            return null;
        }

        /// <summary>
        /// The get metadata for property.
        /// </summary>
        /// <param name="modelAccessor">
        /// The model accessor.
        /// </param>
        /// <param name="containerType">
        /// The container type.
        /// </param>
        /// <param name="propertyDescriptor">
        /// The property descriptor.
        /// </param>
        /// <returns>
        /// The <see cref="System.Web.Mvc.ModelMetadata"/>.
        /// </returns>
        protected override ModelMetadata GetMetadataForProperty(
            Func<object> modelAccessor, 
            Type containerType, 
            PropertyDescriptor propertyDescriptor)
        {
            ModelMetadata metadata = base.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);
            IFluentAnnotation metadataContainer = this.GetFluentAnnotationContainer(
                containerType.FullName, 
                containerType.Assembly.FullName);

            if (metadataContainer != null)
            {
                this.SetDescription(metadata, metadataContainer, propertyDescriptor.Name);
                this.SetDisplayName(metadata, metadataContainer, propertyDescriptor.Name);
                this.SetIsReadOnly(metadata, metadataContainer, propertyDescriptor.Name);
                this.SetShowForDisplay(metadata, metadataContainer, propertyDescriptor.Name);
                this.SetShowForEdit(metadata, metadataContainer, propertyDescriptor.Name);
                var model = this.GetModel(modelAccessor);
                this.SetMetadataForDropDown(model, metadata, metadataContainer, propertyDescriptor.Name);
            }

            return metadata;
        }

        /// <summary>
        /// The get fluent annotation container.
        /// </summary>
        /// <param name="modelName">
        /// The model name.
        /// </param>
        /// <param name="assemblyName">
        /// The assembly name.
        /// </param>
        /// <returns>
        /// The <see cref="FluentDataAnnotations.IFluentAnnotation"/>.
        /// </returns>
        private IFluentAnnotation GetFluentAnnotationContainer(string modelName, string assemblyName)
        {
            try
            {
                Type interfaceType = typeof(IFluentAnnotation<>);
                Type implmentationType =
                    Type.GetType(string.Format("{0}[[{1}, {2}]]", interfaceType.FullName, modelName, assemblyName));
                if (implmentationType != null && this._container.IsTypeRegistered(implmentationType))
                {
                    return (IFluentAnnotation)this._container.GetInstance(implmentationType);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// The set data type and display format.
        /// </summary>
        /// <param name="attributes">
        /// The attributes.
        /// </param>
        /// <param name="metadataContainer">
        /// The metadata container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="modelAccessor">
        /// The model accessor.
        /// </param>
        private void SetDataTypeAndDisplayFormat(
            IList<Attribute> attributes, 
            IFluentAnnotation metadataContainer, 
            string propertyName, 
            Func<object> modelAccessor)
        {
            if (modelAccessor == null)
            {
                return;
            }

            if (!attributes.OfType<DataTypeAttribute>().Any())
            {
                string dataType = metadataContainer.DataType(propertyName);
                if (!dataType.IsNullOrWhiteSpace())
                {
                    DataType dataTypeEnum;
                    Enum.TryParse(dataType, true, out dataTypeEnum);
                    var dataTypeAttribute = new DataTypeAttribute(dataTypeEnum);
                    attributes.Add(dataTypeAttribute);
                }
            }

            if (!attributes.OfType<DisplayFormatAttribute>().Any())
            {
                DisplayFormat displFormat = metadataContainer.DisplayFormat(propertyName);
                var valueTransform = metadataContainer.ValueTransform(propertyName);
                if (displFormat == null && valueTransform == null)
                {
                    return;
                }

                var displayFormatAttr = new DisplayFormatAttribute();
                if (displFormat != null)
                {
                    displayFormatAttr.ApplyFormatInEditMode = displFormat.ApplyFormatInEditMode;
                    displayFormatAttr.ConvertEmptyStringToNull = displFormat.ConvertEmptyStringToNull;
                    displayFormatAttr.HtmlEncode = displFormat.HtmlEncode;
                    displayFormatAttr.NullDisplayText = displFormat.NullDisplayText;
                    displayFormatAttr.DataFormatString = displFormat.GetDisplayFormatString();
                }
                else
                {
                    displayFormatAttr.ConvertEmptyStringToNull = true;
                    displayFormatAttr.NullDisplayText = string.Empty;
                }

                if (valueTransform != null)
                {
                    object model = modelAccessor();
                    if (model is string && valueTransform.ValueTransformFunc != null)
                    {
                        string value = valueTransform.ValueTransformFunc(model.ToString());
                        displayFormatAttr.ApplyFormatInEditMode = valueTransform.ApplyTransformInEditMode;
                        displayFormatAttr.DataFormatString = displFormat != null
                                                                 ? string.Format(
                                                                     displFormat.GetDisplayFormatString(), 
                                                                     value)
                                                                 : value;
                    }
                }

                attributes.Add(displayFormatAttr);
            }
        }

        /// <summary>
        /// The set description.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="metadataContainer">
        /// The metadata container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void SetDescription(ModelMetadata metadata, IFluentAnnotation metadataContainer, string propertyName)
        {
            metadata.Description = metadataContainer.Description(propertyName);
        }

        /// <summary>
        /// The set display name.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="metadataContainer">
        /// The metadata container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void SetDisplayName(ModelMetadata metadata, IFluentAnnotation metadataContainer, string propertyName)
        {
            metadata.DisplayName = metadata.DisplayName == null
                                       ? metadataContainer.DisplayName(propertyName)
                                       : metadata.GetDisplayName();
        }

        /// <summary>
        /// The set hidden input.
        /// </summary>
        /// <param name="attributes">
        /// The attributes.
        /// </param>
        /// <param name="metadataContainer">
        /// The metadata container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void SetHiddenInput(
            List<Attribute> attributes, 
            IFluentAnnotation metadataContainer, 
            string propertyName)
        {
            if (!attributes.OfType<HiddenInputAttribute>().Any())
            {
                bool isHidden = metadataContainer.HiddenInput(propertyName);
                if (isHidden)
                {
                    var hiddenInputAttribute = new HiddenInputAttribute();
                    attributes.Add(hiddenInputAttribute);
                }
            }
        }

        /// <summary>
        /// The set is read only.
        /// </summary>
        /// <param name="metadata">
        /// The meta-data.
        /// </param>
        /// <param name="metadataContainer">
        /// The meta-data container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void SetIsReadOnly(ModelMetadata metadata, IFluentAnnotation metadataContainer, string propertyName)
        {
            ReadOnlyFormat readOnlyFormat = metadataContainer.IsReadOnly(propertyName);
            if (readOnlyFormat != null)
            {
                metadata.IsReadOnly = metadata.IsReadOnly || readOnlyFormat.IsReadOnly;
                if (readOnlyFormat.DisplayAsReadOnlyInput)
                {
                    metadata.AdditionalValues[Utilities.DisplayAsDisabledInputKey] = true;
                }
            }
            else
            {
                metadata.IsReadOnly = metadata.IsReadOnly;
            }
        }

        /// <summary>
        /// The set show for display.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="metadataContainer">
        /// The metadata container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void SetShowForDisplay(ModelMetadata metadata, IFluentAnnotation metadataContainer, string propertyName)
        {
            metadata.ShowForDisplay = metadata.ShowForDisplay && metadataContainer.ShowForDisplay(propertyName);
            if (metadata.ShowForDisplay == false)
            {
                metadata.AdditionalValues[Utilities.ShowLabelForDisplayKey] = false;
            }

            if (!metadata.TemplateHint.IsNullOrWhiteSpace()
                && metadata.TemplateHint.Equals("HiddenInput", StringComparison.OrdinalIgnoreCase))
            {
                metadata.HideSurroundingHtml = true;
                metadata.AdditionalValues[Utilities.ShowLabelForDisplayKey] = false;
            }
        }

        /// <summary>
        /// The set show for edit.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="metadataContainer">
        /// The metadata container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void SetShowForEdit(ModelMetadata metadata, IFluentAnnotation metadataContainer, string propertyName)
        {
            metadata.ShowForEdit = metadata.ShowForEdit && metadataContainer.ShowForEdit(propertyName);
            if (metadata.ShowForEdit == false)
            {
                metadata.AdditionalValues[Utilities.ShowLabelForEditKey] = false;
            }

            if (!metadata.TemplateHint.IsNullOrWhiteSpace()
                && metadata.TemplateHint.Equals("HiddenInput", StringComparison.OrdinalIgnoreCase))
            {
                metadata.HideSurroundingHtml = true;
                metadata.AdditionalValues[Utilities.ShowLabelForEditKey] = false;
            }
        }

        /// <summary>
        /// Set Metadata for DropDown.
        /// </summary>
        /// <param name="metadataContainer">
        /// The metadata container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void SetMetadataForDropDown(object model, ModelMetadata metadata, IFluentAnnotation metadataContainer, string propertyName)
        {
            var selectList = metadataContainer.SelectListForDropDown(model, propertyName);
            if (selectList != null)
            {
                metadata.TemplateHint = Utilities.DropDown;
                metadata.AdditionalValues[propertyName + Utilities.SelectListKey] = selectList;
            }           
        }

        /// <summary>
        /// The set ui hint.
        /// </summary>
        /// <param name="attributes">
        /// The attributes.
        /// </param>
        /// <param name="metadataContainer">
        /// The metadata container.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void SetUIHint(IList<Attribute> attributes, IFluentAnnotation metadataContainer, string propertyName)
        {
            if (!attributes.OfType<UIHintAttribute>().Any())
            {
                string uiHint = metadataContainer.UIHint(propertyName);
                if (!uiHint.IsNullOrWhiteSpace())
                {
                    var uiHintAttribute = new UIHintAttribute(uiHint);
                    attributes.Add(uiHintAttribute);
                }
            }
        }

        #endregion
    }
}