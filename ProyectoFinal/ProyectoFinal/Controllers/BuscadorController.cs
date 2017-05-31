using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class BuscadorController : Controller
    {
        // GET: Buscador
        public ActionResult Index()
        {
            return View();
        }

        protected List<string> keywords = new List<string>();
        public ActionResult Buscar(object sender, EventArgs e, Buscador Buscado)
        {
            string vkeywords = Buscado.KeyWords;
            if (vkeywords == null)
            {
                ViewBag.ListaArticulos = Buscado.GetAll();
            }
            else
            {
                // Turn user input to a list of keywords.
                string[] keywords = Buscado.KeyWords.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                this.keywords = keywords.ToList();

                // Do search operation.
                Buscador dataAccess = new Buscador();
                List<Contenido> list = dataAccess.Search(this.keywords);
                ViewBag.ListaArticulos = list;

            }
            return View("Resultados");
        }
    }
}