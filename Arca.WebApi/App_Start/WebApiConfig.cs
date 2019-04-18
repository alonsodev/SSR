using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Arca.WebApi.App_Start;
using Arca.WebApi.Security;

namespace Arca.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            // Configuración de rutas y servicios de API
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new TokenValidationHandler());
            config.Services.Add(typeof(IExceptionLogger), new TraceExceptionLogger());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
