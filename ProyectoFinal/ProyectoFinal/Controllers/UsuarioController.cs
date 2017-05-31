using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult AbrirSubidas()
        {
            Buscador Buscado = new Buscador();
            ViewBag.ListaArticulos = Buscado.GetArticle(Usuario.usuarioConectado.ID);
            if (ViewBag.ListaArticulos == null)
            {
                List<Contenido> ListVacia = new List<Contenido>();
                ViewBag.ListaArticulos = ListVacia;
                return View("Subidas");
            }
            return View("~/Views/Usuario/Subidas.cshtml");
        }

        public ActionResult Registracion(Usuario Registrando)
        {
            if (ModelState.IsValid)
            {
                int Correcto = Usuario.Registrar(Registrando);
                if (Correcto == 1)
                {

                    return View("~/Views/Home/RegistradoExito.cshtml");
                }
                return View("~/Views/Home/Registracion.cshtml");
            }
            else
            {
                return View("~/Views/Home/Registracion.cshtml");
            }
        }

        public ActionResult Login(Usuario Logueando)
        {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Contraseña"))
            {

                Usuario User = Usuario.Loguear(Logueando);
                if (User != null)
                {
                    Usuario.Logueado = true;
                    ViewBag.Usuario = User;
                    Usuario.usuarioConectado = User;

                    return View("HomeUsuario");
                }
                return View("~/Views/Home/Login.cshtml");
            }
            else
            {
                return View("~/Views/Home/Login.cshtml");
            }
        }

    }
}