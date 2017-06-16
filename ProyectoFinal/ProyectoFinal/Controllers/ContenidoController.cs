using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DataAccess;
using System.IO;

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
                    Cont.Ruta = archivo;
                    Cont.FechadeSubida = DateTime.Now.ToString();
                    int Correcto = Contenidos.Subir(Cont, Usuario.UsuarioConectado);
                    file.SaveAs(Server.MapPath("~/Uploads/" + archivo));
                    ViewBag.ListaArticulos = Contenidos.GetAll();
                    return RedirectToAction("Index", "Home");
                }
                return View("~/Views/Contenido/SubirContenido.cshtml");
            }
            else
            {
                return View("~/Views/Contenido/SubirContenido.cshtml");
            }
        }

 
        public ActionResult Descargar(int ID)
        {
            Contenido selected = new Contenido();
            selected = Contenidos.GetOneArticle(ID);
            Contenidos.UpdateDescargas(selected.ID, selected.Des);
            string contentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            return new FilePathResult("~/Uploads/" + selected.Ruta, contentType)
            {
                FileDownloadName = selected.Ruta,
            };
        }

        public ActionResult VerMas(int cont)
        {
            Contenido selected = new Contenido();
            selected = Contenidos.GetOneArticle(cont);
            ViewBag.URL = "http://docs.google.com/viewer?url=http://localhost:1782/Uploads/" + selected.Ruta + "&embedded=true";
            ViewBag.Title = selected.Nombre;
            Contenidos.UpdatePopularidad(selected.ID, selected.Pop);
            return View("~/Views/Contenido/VerMas.cshtml", selected);//porfavor
        }
        public ActionResult VerTodo(string Title)
        {
            List<Contenido> Lista = new List<Contenido>();
            Contenido DA = new Contenido();
            switch (Title)
            {
                case "Mas recientes":
                    Lista = Contenidos.GetAll();
                    ViewBag.ListaArticulos = Lista;
                    ViewBag.Title = Title;
                    break;
                case "Mas populares":
                    Lista = Contenidos.GetByPop();
                    ViewBag.ListaArticulos = Lista;
                    ViewBag.Title = Title;
                    break;
                case "Mas descargados":
                    Lista = Contenidos.GetByDes();
                    ViewBag.ListaArticulos = Lista;
                    ViewBag.Title = Title;
                    break;
            }
            return View("Resultados");

        }
        public ActionResult AbrirSubir()
        {
            List<TipoContenido> ListTipodecont = new List<TipoContenido>();
            ListTipodecont = TiposContenidos.TraerTipoContenidos();

            List<NivelEducativo> ListNivelEdu = new List<NivelEducativo>();
            ListNivelEdu = NivelesEducativos.TraerNivelEducativo();

            List<Escuela> ListEscuela = new List<Escuela>();
            ListEscuela = Escuelas.TraerEscuelas();

            List<Materia> ListMateria = new List<Materia>();
            ListMateria = Materias.TraerMaterias();

            ViewBag.TipodeCont = ListTipodecont;
            ViewBag.NivelEdu = ListNivelEdu;
            ViewBag.Escuelas = ListEscuela;
            ViewBag.Materia = ListMateria;
            return View("~/Views/Contenido/SubirContenido.cshtml");
        }

        protected List<string> keywords = new List<string>();
        public ActionResult Buscar(object sender, EventArgs e, Contenido Buscado)
        {
            string vkeywords = Buscado.KeyWords;
            if (vkeywords == null)
            {
                ViewBag.ListaArticulos = Contenidos.GetAll();
            }
            else
            {
                // Turn user input to a list of keywords.
                string[] keywords = Buscado.KeyWords.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                this.keywords = keywords.ToList();

                // Do search operation.
                List<Contenido> list = Contenidos.Search(this.keywords);
                ViewBag.ListaArticulos = list;

            }
            ViewBag.Title = "Resultados";
            return View("Resultados");
        }

    }
}
