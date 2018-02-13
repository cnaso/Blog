using Blog.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(PostsController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" }, namespaces);

            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" }, namespaces);

            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" }, namespaces);

            routes.MapRoute("PostRoute", "post/{idAndSlug}", new { controller = "Posts", action = "Show" }, namespaces);

            routes.MapRoute("Post", "post/{id}-{slug}", new { controller = "Posts", action = "Show" }, namespaces);

            routes.MapRoute("Sidebar", "", new { controller = "Sidebar", action = "Sidebar" }, namespaces);

            routes.MapRoute("TagRoute", "tag/{idAndSlug}", new { controller = "Posts", action = "TagPosts" }, namespaces);

            routes.MapRoute("TagPosts", "tag/{id}-{slug}", new { controller = "Posts", action = "TagPosts" }, namespaces);
        }
    }
}
