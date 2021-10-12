using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using DotNetAssignment.ServiceLayer;
using System.Web.Mvc;

namespace DotNetAssignment
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IEmployeesService, EmployeeService>();
            container.RegisterType<ILeavesService, LeaveService>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}