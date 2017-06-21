using Microsoft.Owin;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.OData.Extensions;
using Microsoft.Practices.Unity;

[assembly: OwinStartup(typeof(Messages.Server.Startup))]

namespace Messages.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            //app.UseWelcomePage();
            var config = new HttpConfiguration();

            var container = new UnityContainer();
            container.RegisterType<ILinq2Db, Linq2Db>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
            config.AddODataQueryFilter();
            app.UseWebApi(config);
        }
    }
}