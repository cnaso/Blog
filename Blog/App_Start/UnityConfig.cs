using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Data.Entity;
using Blog.Persistence;

namespace Blog
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<DbContext, BlogContext>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}