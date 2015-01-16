using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1
{
    using FluentDataAnnotations;
    using FluentDataAnnotations.SimpleInjectorAdapter;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //var container = new SimpleContainer();

            //container.Register<IFluentAnnotation<ChangePasswordViewModel>, ChangePasswordViewModelAnnotations>();

            var container = new Container();

            //container.RegisterFluentAnnotations();
            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

           

            ModelMetadataProviders.Current = new FluentModelMetadataProvider(new SimpleInjectorAdapter(container));

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
