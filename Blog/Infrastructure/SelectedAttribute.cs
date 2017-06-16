using System;
using System.Web.Mvc;

namespace Blog.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SelectedAttribute : ActionFilterAttribute
    {
        private readonly string _selected;

        public SelectedAttribute(string selected)
        {
            _selected = selected;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Selected = _selected;
        }
    }
}