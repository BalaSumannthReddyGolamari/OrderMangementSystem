using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using OrderingSystem.Models;

namespace OrderingSystem.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var bldr = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(bldr);
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            var container = bldr.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder bldr)
        {
            bldr.RegisterType<OrderSystemEntities>()
              .InstancePerRequest();

            bldr.RegisterType<OrderRepository>()
              .As<IOrderRepository>()
              .InstancePerRequest();
        }
    }
}