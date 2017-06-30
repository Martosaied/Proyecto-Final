using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PFEF.Models;
using PFEF.ViewModels;
using AutoMapper;

namespace PFEF.Controllers
{
    public class UsuariosController : Controller
    {

        private dbEntities1 db = new dbEntities1();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }


        // GET: Usuarios/Create
        public ActionResult Registracion()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registracion([Bind(Include = "Nombre,Apellido,Email,Contraseña,confirmarContraseña")] RegistracionViewModel NewUser)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<RegistracionViewModel, Usuarios>();
                });
                IMapper mapper = config.CreateMapper();
                var User = mapper.Map<RegistracionViewModel, Usuarios>(NewUser);
                db.Usuarios.Add(User);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(NewUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel objUser)
        {
            if (ModelState.IsValid)
            {
                using (dbEntities1 db = new dbEntities1())
                {
                    var obj = db.Usuarios.Where(a => a.Email.Equals(objUser.Email) && a.Contraseña.Equals(objUser.Contraseña)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["User"] = obj;
                        return RedirectToAction("HomeUsuario");
                    }
                }
            }
            return View(objUser);
        }

        [HttpGet]
        public ActionResult HomeUsuario()
        {
            return View("HomeUsuario");
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
