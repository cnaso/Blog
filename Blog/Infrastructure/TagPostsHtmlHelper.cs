using Blog.ViewModels;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Blog.Infrastructure
{
    public static class TagPostsHtmlHelper
    {
        public static HtmlString TagPosts(this HtmlHelper helper, SidebarTagViewModel tag)
        {
            if (tag != null)
            {
                return helper.Partial("_TagPostsNotification", tag);
            }

            return new MvcHtmlString("");
        }
    }
}