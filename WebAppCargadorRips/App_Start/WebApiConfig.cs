using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
//El siguente link es para generar el xml de documentacion
//http://javiginer.com/summary-description-help-pages-asp-net-web-api/
//https://docs.microsoft.com/en-us/aspnet/web-api/overview/getting-started-with-aspnet-web-api/creating-api-help-pages
namespace WebAppCargadorRips
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            // Habilito los cors
            config.EnableCors();
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
