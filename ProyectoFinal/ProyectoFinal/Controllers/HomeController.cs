using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Contenido> ListaTodo = new List<Contenido>();
            Contenido DA = new Contenido();
            ListaTodo = DA.GetAll();
            Contenido[] vcSlider = new Contenido[9];
            int i = 0;
            foreach (Contenido item in ListaTodo)
            {
                vcSlider[i] = item;
                i++;
                if (i == 9)
                    break;
            }                                                             
             ViewBag.ListaArticulos = vcSlider;
            return View("Index");
        }

        public ActionResult LogOff()
        {
            Usuario.UsuarioConectado = null;
            List<Contenido> ListaTodo = new List<Contenido>();
            Contenido DA = new Contenido();
            ListaTodo = DA.GetAll();
            ViewBag.ListaArticulos = ListaTodo;
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
