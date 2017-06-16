using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DataAccess;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Contenido> Lista = new List<Contenido>();
            Contenido DA = new Contenido();
            Lista = Contenidos.GetAll();                                                            
            ViewBag.ListaArticulos = CreateSliderVecs(Lista);
            Lista = Contenidos.GetByPop();
            ViewBag.ListaArticulosPop = CreateSliderVecs(Lista);
            Lista = Contenidos.GetByDes();
            ViewBag.ListaArticulosDes = CreateSliderVecs(Lista);
            if (ViewBag.ListaArticulosDes[0] == null)
            {
                return View("Login");
            }
            else
            {
                return View("Index");
            }
        }

        public Contenido[] CreateSliderVecs(List<Contenido> List)
        {
            Contenido[] vcSlider = new Contenido[9];
            int i = 0;
            foreach (Contenido item in List)
            {
                vcSlider[i] = item;
                i++;
                if (i == 9)
                    break;
            }
            return vcSlider;
        }
        public ActionResult LogOff()
        {
            Usuario.UsuarioConectado = null;
            List<Contenido> Lista = new List<Contenido>();
            Contenido DA = new Contenido();
            Lista = Contenidos.GetAll();
            ViewBag.ListaArticulos = CreateSliderVecs(Lista);
            Lista = Contenidos.GetByPop();
            ViewBag.ListaArticulosPop = CreateSliderVecs(Lista);
            Lista = Contenidos.GetByDes();
            ViewBag.ListaArticulosDes = CreateSliderVecs(Lista);
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult AbrirLogin()
        {
            return View("Login");
        }
         public ActionResult AbrirRegistracion()
        {
            return View("Registracion");
        }
        public ActionResult AbrirHomeDI()
        {
            return View("Index");
        }

  

    }

    }
