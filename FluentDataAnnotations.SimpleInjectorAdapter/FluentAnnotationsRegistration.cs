// Summary:
// File: WebApplication1/WebApplication1/FluentAnnotationsRegistration.cs 
// Created at: 02/01/2015    22:04
// Created by: 

#region



#endregion

namespace FluentDataAnnotations.SimpleInjectorAdapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Compilation;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;

    public static class FluentAnnotationsRegistration
    {
        public static void RegisterFluentAnnotations(this Container container)
        {
            var assemblies = BuildManager.GetReferencedAssemblies().OfType<Assembly>().ToArray();

            var displayAnnotationTypes = GetAnnotationTypes(assemblies);

            var genericAnnotationType = typeof (IFluentAnnotation<>);

            foreach (var type in displayAnnotationTypes)
            {
                var interfaceType = type.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType
                                         && x.GetGenericTypeDefinition() == genericAnnotationType);

                 var lifesicle = new WebRequestLifestyle(true);
                 container.Register(interfaceType, type, lifesicle);
            }
        }

        private static IEnumerable<Type> GetAnnotationTypes(IEnumerable<Assembly> assemblies)
        {
            var t = typeof (IFluentAnnotation);
            return assemblies.Where(x => !x.IsDynamic).SelectMany(x => x.GetExportedTypes())
                .Where(x => t.IsAssignableFrom(x) && !x.IsAbstract && !x.IsGenericTypeDefinition)
                .ToArray();
        }
    }
}