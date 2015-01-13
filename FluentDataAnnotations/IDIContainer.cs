// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDIContainer.cs" company="">
//   
// </copyright>
// <summary>
//   The DIContainer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentDataAnnotations
{
    /// <summary>
    /// The DIContainer interface.
    /// </summary>
    public interface IDIContainer
    {
        /// <summary>
        /// The get instance.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object GetInstance(Type t);

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="interfaceType">
        /// The interface type.
        /// </param>
        /// <param name="implementationType">
        /// The implementation type.
        /// </param>
        void Register(Type interfaceType, Type implementationType);

        bool IsTypeRegistered(Type type);
    }
}
