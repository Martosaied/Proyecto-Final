using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
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
                    int Correcto = Contenido.Subir(Cont, Usuario.UsuarioConectado);
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

        public ActionResult Descargar(int ID)
        {
            Contenido selected = new Contenido();
            selected = selected.GetOneArticle(ID);
            selected.UpdateDescargas(selected.ID, selected.Des);
            string contentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            return new FilePathResult("~/Uploads/" + selected.Ruta, contentType)
            {
                FileDownloadName = selected.Ruta,
            };
        }

        public ActionResult VerMas(int cont)
        {
            Contenido selected = new Contenido();
            selected = selected.GetOneArticle(cont);
            ViewBag.URL = "http://docs.google.com/viewer?url=" + selected.Ruta + "&embedded=true";
            ViewBag.Title = selected.Nombre;
            selected.UpdatePopularidad(selected.ID, selected.Pop);
            return View("~/Views/Contenido/VerMas.cshtml", selected);//porfavor
        }
        public ActionResult VerTodo(string Title)
        {
            List<Contenido> Lista = new List<Contenido>();
            Contenido DA = new Contenido();
            switch (Title)
            {
                case "Mas recientes":
                    Lista = DA.GetAll();
                    ViewBag.ListaArticulos = Lista;
                    ViewBag.Title = Title;
                    break;
                case "Mas populares":
                    Lista = DA.GetByPop();
                    ViewBag.ListaArticulos = Lista;
                    ViewBag.Title = Title;
                    break;
                case "Mas descargados":
                    Lista = DA.GetByDes();
                    ViewBag.ListaArticulos = Lista;
                    ViewBag.Title = Title;
                    break;
            }
            return View("Resultados");

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
            ViewBag.Title = "Resultados";
            return View("Resultados");
        }

    }
}
