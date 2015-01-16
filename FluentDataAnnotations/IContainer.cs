// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IContainer.cs" company="">
//   
// </copyright>
// <summary>
//   The DIContainer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;

namespace FluentDataAnnotations
{
    /// <summary>
    ///     The DIContainer interface.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        ///     The get instance.
        /// </summary>
        /// <param name="t">
        ///     The t.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        object GetInstance(Type t);

        /// <summary>
        ///     The register.
        /// </summary>
        /// <param name="interfaceType">
        ///     The interface type.
        /// </param>
        /// <param name="implementationType">
        ///     The implementation type.
        /// </param>
        void Register(Type interfaceType, Type implementationType);

        void Register(Type interfaceType, Func<object> instanceCreator);

        void Register<T>(Func<object> instanceCreator) where T : IFluentAnnotation;

        void Register<TInt, TImpl>() 
            where TImpl : IFluentAnnotation, new()
            where TInt : IFluentAnnotation;

        bool IsTypeRegistered(Type type);
    }
}