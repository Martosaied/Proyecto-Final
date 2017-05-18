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
            List<Contenido> ListaTodo = new List<Contenido>();
            Buscador DA = new Buscador();
            ListaTodo = DA.GetAll();
            ViewBag.ListaArticulos = ListaTodo;
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
            List<Escuelas> ListEscuela = new List<Escuelas>();
            ListEscuela = Escuelas.TraerEscuelas();
            List<Materia> ListMateria = new List<Materia>();
            ListMateria = Materia.TraerMaterias();
            ViewBag.Escuelas = ListEscuela;
            ViewBag.Materia = ListMateria;
            return View("SubirContenido");
        }

        public ActionResult AbrirSubidas()
        {
            Buscador Buscado = new Buscador();
            ViewBag.ListaArticulos = Buscado.GetArticle(Usuario.usuarioConectado.ID);
            if(ViewBag.ListaArticulos == null)
            {
                List<Contenido> ListVacia = new List<Contenido>();
                ViewBag.ListaArticulos = ListVacia;
                return View("Subidas");
            }
            return View("Subidas");
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
                    Buscador Buscado = new Buscador();
                    ViewBag.ListaArticulos = Buscado.GetArticle(Usuario.usuarioConectado.ID);
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

                    return View("HomeUsuario");
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
            this.keywords = keywords.ToList();

            // Do search operation.
            Buscador dataAccess = new Buscador();
            List<Contenido> list = dataAccess.Search(this.keywords);
            ViewBag.Articulos = list;
            return View("Resultados");
        }

    }

    }
