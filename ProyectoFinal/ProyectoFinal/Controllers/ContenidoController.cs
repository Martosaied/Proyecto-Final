using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class ContenidoController : Controller
    {
        [HttpPost]
        public ActionResult Subir(Contenido Cont, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                    Cont.Ruta = "~/Uploads/" + archivo;
                    int Correcto = Contenido.Subir(Cont, Usuario.usuarioConectado);
                    file.SaveAs(Server.MapPath("~/Uploads/" + archivo));
                    Contenido Buscado = new Contenido();
                    ViewBag.ListaArticulos = Buscado.GetAll();
                    return View("~/Views/Home/Index.cshtml");
                }
                return View("~/Views/Contenido/SubirContenido.cshtml");
            }
            else
            {
                return View("~/Views/Contenido/SubirContenido.cshtml");
            }
        }

        public ActionResult VerMas(int cont)
        {
            Contenido selected = new Contenido();
            selected = selected.GetOneArticle(cont);
            ViewBag.URL = "http://docs.google.com/viewer?url=" + selected.Ruta + "&embedded=true";
            ViewBag.Title = selected.Nombre;
            return View("~/Views/Contenido/VerMas.cshtml", selected);//porfavor
        }

        public ActionResult AbrirSubir()
        {

            List<TipoContenido> ListTipodecont = new List<TipoContenido>();
            ListTipodecont = TipoContenido.TraerTipoContenidos();

            List<NivelEducativo> ListNivelEdu = new List<NivelEducativo>();
            ListNivelEdu = NivelEducativo.TraerNivelEducativo();

            List<Escuelas> ListEscuela = new List<Escuelas>();
            ListEscuela = Escuelas.TraerEscuelas();

            List<Materia> ListMateria = new List<Materia>();
            ListMateria = Materia.TraerMaterias();

            ViewBag.TipodeCont = ListTipodecont;
            ViewBag.NivelEdu = ListNivelEdu;
            ViewBag.Escuelas = ListEscuela;
            ViewBag.Materia = ListMateria;
            return View("SubirContenido");
        }

        protected List<string> keywords = new List<string>();
        public ActionResult Buscar(object sender, EventArgs e, Contenido Buscado)
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
                Contenido dataAccess = new Contenido();
                List<Contenido> list = dataAccess.Search(this.keywords);
                ViewBag.ListaArticulos = list;

            }
            return View("Resultados");
        }

    }
}
