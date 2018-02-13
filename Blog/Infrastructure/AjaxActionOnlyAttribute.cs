using System.Web.Mvc;

namespace Blog.Infrastructure
{
    public class AjaxActionOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.Redirect("/error/404");
            }
        }
    }
}