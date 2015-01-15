// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="FluentAnnotationsRegistration.cs">
//   
// </copyright>
// <summary>
//   The fluent annotations registration.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace FluentDataAnnotations.SimpleInjectorAdapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Compilation;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;

    /// <summary>
    /// The fluent annotations registration.
    /// </summary>
    public static class FluentAnnotationsRegistration
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register fluent annotations.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public static void RegisterFluentAnnotations(this Container container)
        {
            Assembly[] assemblies = BuildManager.GetReferencedAssemblies().OfType<Assembly>().ToArray();

            IEnumerable<Type> displayAnnotationTypes = GetAnnotationTypes(assemblies);

            Type genericAnnotationType = typeof(IFluentAnnotation<>);

            foreach (Type type in displayAnnotationTypes)
            {
                Type interfaceType =
                    type.GetInterfaces()
                        .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericAnnotationType);

                var lifesicle = new WebRequestLifestyle(true);
                container.Register(interfaceType, type, lifesicle);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get annotation types.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<Type> GetAnnotationTypes(IEnumerable<Assembly> assemblies)
        {
            Type t = typeof(IFluentAnnotation);
            return
                assemblies.Where(x => !x.IsDynamic)
                    .SelectMany(x => x.GetExportedTypes())
                    .Where(x => t.IsAssignableFrom(x) && !x.IsAbstract && !x.IsGenericTypeDefinition)
                    .ToArray();
        }

        #endregion
    }
}