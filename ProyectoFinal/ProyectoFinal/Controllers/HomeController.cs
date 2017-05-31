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
            Buscador DA = new Buscador();
            ListaTodo = DA.GetAll();
            ViewBag.ListaArticulos = ListaTodo;
            return View("Index");
        }

        public ActionResult LogOff()
        {
            Usuario.usuarioConectado = null;
            List<Contenido> ListaTodo = new List<Contenido>();
            Buscador DA = new Buscador();
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
