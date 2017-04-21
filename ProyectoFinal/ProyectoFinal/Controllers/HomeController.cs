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
            return View();
        }

        public ActionResult AbrirLogin()
        {
            return View("Login");
        }
         public ActionResult AbrirRegistracion()
        {
            return View("Registracion");
        }

        public ActionResult Registracion(Usuario Registrando)
        {
            if (ModelState.IsValid)
            {
                int Correcto = Usuario.Registrar(Registrando);
                if (Correcto == 1)
                {
                    return View("RegistradoExito");
                }
                return View("Index");
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Login(Usuario Logueando)
        {
            if(ModelState.IsValidField("Email") && ModelState.IsValidField("Contraseña"))
            {
                Usuario Usuario = Usuario.Loguear(Logueando);
                if (Usuario != null)
                {
                    return View("HomeUsuario");
                }
                return View("Index");
            }
            else
            {
                return View("Index");
            }
        }
        }

    }
