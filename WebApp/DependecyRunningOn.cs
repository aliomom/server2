using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using System.Web;
using WebService.Dependecy;

namespace WebApp
{
    public class DependecyRunningOn
    {

        public static void Initialise()
        {
            var container = BuildUnityContainer();

            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            // register dependency resolver for WebAPI RC
            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            //Component initialization via MEF
            ComponentLoader.LoadContainer(container, ".\\bin", "WebApp.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "WebService.dll");
            
        }
    }
}