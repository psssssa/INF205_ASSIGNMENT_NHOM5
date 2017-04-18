using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using PagedList;


namespace MvcApplication3.Controllers
{
    public class HomeController : Controller
    {
        private INF205_ASS1_NHOM_5Entities db = new INF205_ASS1_NHOM_5Entities();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var products = db.products.Include
                (p => p.category).OrderBy(i => i.id).Take(4);
            return View(products.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
