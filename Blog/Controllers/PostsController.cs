using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}