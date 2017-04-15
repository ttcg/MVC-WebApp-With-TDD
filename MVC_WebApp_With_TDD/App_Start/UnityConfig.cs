using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using MVC_WebApp_With_TDD.Services;

namespace MVC_WebApp_With_TDD
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IStudentsService, StudentsService>();
            container.RegisterType<ICampusService, CampusService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}