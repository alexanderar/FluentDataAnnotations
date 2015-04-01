// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleContainer.cs" company="">
//   
// </copyright>
// <summary>
//   The simple container.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FluentDataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// The simple container.
    /// </summary>
    public class SimpleContainer : IContainer
    {
        #region Fields

        /// <summary>
        /// The _container.
        /// </summary>
        private readonly Dictionary<Type, Func<object>> _container = new Dictionary<Type, Func<object>>();

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
            return t != null && this.IsTypeRegistered(t) ? this._container[t]() : null;
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
            return type != null && this._container.ContainsKey(type);
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
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public void Register(Type interfaceType, Type implementationType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException("interfaceType");
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException("implementationType");
            }

            ConstructorInfo ctor = implementationType.GetConstructor(Type.EmptyTypes);

            if (ctor == null)
            {
                throw new Exception(string.Format(Resource.Error_NoDefaultCtorFormat, "ImplementationType"));
            }

            if (!typeof(IFluentAnnotation).IsAssignableFrom(implementationType))
            {
                throw new Exception(
                    string.Format(Resource.Error_HasToImplementAnInterface, "ImplementationType", "IFluentAnnotation"));
            }

            if (!typeof(IFluentAnnotation<>).IsAssignableFrom(interfaceType))
            {
                throw new Exception(
                    string.Format(Resource.Error_HasToImplementAnInterface, "InterfaceType", "IFluentAnnotation<>"));
            }

            Func<object> del = Expression.Lambda<Func<object>>(Expression.New(ctor)).Compile();
            this._container[interfaceType] = del;
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="interfaceType">
        /// The interface type.
        /// </param>
        /// <param name="instanceCreator">
        /// The instance creator.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public void Register(Type interfaceType, Func<object> instanceCreator)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException("interfaceType");
            }

            if (!typeof(IFluentAnnotation<>).IsAssignableFrom(interfaceType))
            {
                throw new Exception(
                    string.Format(Resource.Error_HasToImplementAnInterface, "InterfaceType", "IFluentAnnotation<>"));
            }

            if (instanceCreator == null)
            {
                throw new ArgumentNullException("instanceCreator");
            }

            this._container[interfaceType] = instanceCreator;
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="instanceCreator">
        /// The instance creator.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public void Register<T>(Func<object> instanceCreator) where T : IFluentAnnotation
        {
            if (!typeof(IFluentAnnotation<>).IsAssignableFrom(typeof(T)))
            {
                throw new Exception(
                    string.Format(Resource.Error_HasToImplementAnInterface, "InterfaceType", "IFluentAnnotation<>"));
            }

            if (instanceCreator == null)
            {
                throw new ArgumentNullException("instanceCreator");
            }

            this._container[typeof(T)] = instanceCreator;
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <typeparam name="TInt">
        /// </typeparam>
        /// <typeparam name="TImpl">
        /// </typeparam>
        /// <exception cref="Exception">
        /// </exception>
        public void Register<TInt, TImpl>() where TInt : IFluentAnnotation where TImpl : IFluentAnnotation, new()
        {
            if (!typeof(IFluentAnnotation<>).IsAssignableFrom(typeof(TInt)))
            {
                throw new Exception(
                    string.Format(Resource.Error_HasToImplementAnInterface, "TInt", "IFluentAnnotation<>"));
            }

            this._container[typeof(TInt)] = () => new TImpl();
        }

        #endregion
    }
}