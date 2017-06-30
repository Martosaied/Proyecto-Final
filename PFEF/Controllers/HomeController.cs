using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PFEF.Models;

namespace PFEF.Controllers
{
    public class HomeController : Controller
    {
        private dbEntities1 db = new dbEntities1();

        public ActionResult Index()
        {

            ViewBag.ListaArticulos = db.Contenidos.OrderByDescending(x => x.Id).Take(9).ToList().ToArray();
            ViewBag.ListaArticulosPop = db.Contenidos.OrderByDescending(x => x.IPop).Take(9).ToList().ToArray();
            ViewBag.ListaArticulosDes = db.Contenidos.OrderByDescending(x => x.IDes).Take(9).ToList().ToArray();
                return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}