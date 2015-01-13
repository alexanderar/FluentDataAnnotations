// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleInjectorAdapter.cs" company="">
//   
// </copyright>
// <summary>
//   The simple injector adapter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FluentDataAnnotations.SimpleInjectorAdapter
{
    using System;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;

    /// <summary>
    /// The simple injector adapter.
    /// </summary>
    public class SimpleInjectorAdapter : IDIContainer
    {
        #region Fields

        /// <summary>
        /// The _container.
        /// </summary>
        private readonly SimpleInjector.Container _container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorAdapter"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public SimpleInjectorAdapter(Container container)
        {
            this._container = container;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetInstance(Type t)
        {
            return this._container.GetInstance(t);
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="interfaceType">
        /// The interface type.
        /// </param>
        /// <param name="implementationType">
        /// The implementation type.
        /// </param>
        public void Register(Type interfaceType, Type implementationType)
        {
            var lifesicle = new WebRequestLifestyle(true);
            this._container.Register(interfaceType, implementationType, lifesicle);
        }

        public bool IsTypeRegistered(Type type)
        {
            return this._container.GetRegistration(type, false) != null;
        }

        #endregion
    }
}