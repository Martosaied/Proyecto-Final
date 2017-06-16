using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DataAccess;

namespace ProyectoFinal.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult AbrirSubidas()
        {
            ViewBag.ListaArticulos = Contenidos.GetArticleUser(Usuario.UsuarioConectado.ID);
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
                int Correcto = Usuarios.Registrar(Registrando);
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

                Usuario User = Usuarios.Loguear(Logueando);
                if (User != null)
                {
                    ViewBag.Usuario = User;
                    Usuario.UsuarioConectado = User;

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