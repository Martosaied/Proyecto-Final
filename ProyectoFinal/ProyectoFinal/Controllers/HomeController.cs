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
        protected List<string> keywords = new List<string>();
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
                    return View("Subidas");
                }
                return View("SubirContenido");
            }
            else
            {
                return View("SubirContenido");
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

        public ActionResult Buscar(object sender, EventArgs e,Buscador Buscado)
        {
            // Turn user input to a list of keywords.
            string[] keywords = Buscado.KeyWords.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            // The basic validation.
            if (keywords.Length <= 0)
            {
                //lbAlert.Text = "Please input keyword.";
                //return;
            }
            this.keywords = keywords.ToList();

            // Do search operation.
            Buscador dataAccess = new Buscador();
            List<Contenido> list = dataAccess.Search(this.keywords);
            ViewBag.Articulos = list;
            return View("Buscando");
        }

    }

    }
