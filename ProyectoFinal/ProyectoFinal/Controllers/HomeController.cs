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

        public ActionResult LogOff()
        {
            Usuario.Logueado = false;
            return View("HomeDeInicio");
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
        public ActionResult AbrirSubir()
        {
            return View("SubirContenido");
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
                return View("Registracion");
            }
            else
            {
                return View("Registracion");
            }
        }

        

        public ActionResult Login(Usuario Logueando)
        {
            if(ModelState.IsValidField("Email") && ModelState.IsValidField("Contraseña"))
            {
                
                Usuario User = Usuario.Loguear(Logueando);
                if (User != null)
                {
                    Usuario.Logueado = true;
                    ViewBag.Usuario = User;
                    Usuario.usuarioConectado = User;
                    return View("HomeUsuario", User);
                }
                return View("Login");
            }
            else
            {
                return View("Login");
            }
        }
        }

    }
