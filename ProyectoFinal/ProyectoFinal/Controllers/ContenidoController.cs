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
                    Buscador Buscado = new Buscador();
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

        public PartialViewResult TraerSeleccionado(Contenido cont)
        {

            return PartialView("_DatosView", cont);
        }
    }
}
