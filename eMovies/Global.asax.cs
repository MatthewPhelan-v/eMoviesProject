using eMovies;
using eMovies.App_Start.DependencyConfig;
using eMovies.Controllers;
using eMovies.Dependencies;
using eMovies.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


[assembly: PreApplicationStartMethod(typeof(MvcApplication), "InitModule")]

namespace eMovies
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void InitModule()
        {
            RegisterModule(typeof(ServiceScopeModule));
        }

        protected void Application_Start()
        {

            var services = new ServiceCollection();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureServices(services);
            ServiceScopeModule.SetServiceProvider(services.BuildServiceProvider());
            DependencyResolver.SetResolver(new ServiceProviderDependencyResolver());
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddTransient<MoviesController>();

            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddTransient<OrderController>();
        }
    }
}
