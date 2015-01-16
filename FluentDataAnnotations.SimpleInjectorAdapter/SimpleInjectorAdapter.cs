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
    ///     The simple injector adapter.
    /// </summary>
    public class SimpleInjectorAdapter : IContainer
    {
        #region Fields

        /// <summary>
        ///     The _container.
        /// </summary>
        private readonly Container _container;

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

        public void Register(Type interfaceType, Func<object> instanceCreator)
        {
            throw new NotImplementedException();
        }

        public void Register<T>(Func<object> instanceCreator) where T : IFluentAnnotation
        {
            throw new NotImplementedException();
        }

        public void Register<TInt, TImpl>() where TInt : IFluentAnnotation where TImpl : IFluentAnnotation, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The is type registered.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTypeRegistered(Type type)
        {
            return this._container.GetRegistration(type, false) != null;
        }

        /// <summary>
        /// Registers specific implementation type for an interface type.
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

        #endregion
    }
}