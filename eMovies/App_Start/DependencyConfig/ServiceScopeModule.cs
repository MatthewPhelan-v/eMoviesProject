using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMovies.App_Start.DependencyConfig
{
    public class ServiceScopeModule : IHttpModule
    {
        private static ServiceProvider _serviceProvider;

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
            context.EndRequest += Context_EndRequest;
        }

        private void Context_EndRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            if (context.Items[typeof(IServiceScope)] is IServiceScope scope)
            {
                scope.Dispose();
            }
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            context.Items[typeof(IServiceScope)] = _serviceProvider.CreateScope();
        }

        public static void SetServiceProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}