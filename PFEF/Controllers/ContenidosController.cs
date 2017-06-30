using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PFEF.ViewModels;
using PFEF.Models;
using AutoMapper;
using System.Reflection;

namespace PFEF.Controllers
{
    public class ContenidosController : Controller
    {
        private dbEntities1 db = new dbEntities1();
        private MuestraViewModel FVM = new MuestraViewModel();
        // GET: Contenidos
        public ActionResult VerTodo(string Title)
        {
            FVM = SwitchTitle(Title);
            FVM = CargarDropsFVM(FVM);
            Session["KeyWords"] = "";
            return View("MuestraCont",FVM);
        }

        protected MuestraViewModel SwitchTitle(string Title)
        {
            switch (Title)
            {
                case "Mas recientes":
                    FVM.ListaAMostrar = db.Contenidos.OrderByDescending(x => x.Id).ToList();
                    ViewBag.Title = Title;
                    return FVM;
                case "Mas populares":
                    FVM.ListaAMostrar = db.Contenidos.OrderByDescending(x => x.IPop).ToList();
                    ViewBag.Title = Title;
                    return FVM;
                case "Mas descargados":
                    FVM.ListaAMostrar = db.Contenidos.OrderByDescending(x => x.IPop).ToList();
                    ViewBag.Title = Title;
                    return FVM;
                case "Mis subidas":
                    Usuarios User = (Usuarios)Session["User"];
                    FVM.ListaAMostrar = db.Contenidos.Where(x => x.IdUsuario == User.Id).ToList();
                    ViewBag.Title = Title;
                    return FVM;
                case "Resultados":
                    string kw = (string)Session["KeyWords"];
                    RedirectToAction("Buscar", kw);
                    return FVM;
            }
            return FVM;
        }

        public ActionResult Descargar(int ID)
        {
            Contenidos selected = db.Contenidos.Find(ID);
            UpdateIdes(false, selected);
            string contentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            return new FilePathResult("~/Content/Uploads/" + selected.Ruta, contentType)
            {
                FileDownloadName = selected.Ruta,
            };
        }

        [HttpGet]
        public ActionResult Subir()
        {
            SubirViewModel SVM = new SubirViewModel();
            SVM = CargarDropsSVM(SVM);
            return View("SubirContenidos",SVM);
        }


        [HttpPost]
        public ActionResult Subir(SubirViewModel Cont, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SubirViewModel, Contenidos>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var ContMapeado = mapper.Map<SubirViewModel, Contenidos>(Cont);

                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                    archivo = archivo.Replace(" ", "");     
                    
                    Usuarios User = new Usuarios();
                    User = (Usuarios)Session["User"];

                    ContMapeado.Ruta = archivo;
                    ContMapeado.IdUsuario = User.Id;
                    ContMapeado.FechaSubida = DateTime.Now;
                        
                    db.Contenidos.Add(ContMapeado);
                    db.SaveChanges();
                    //Cont.FechadeSubida = DateTime.Now.ToString();              
                    file.SaveAs(Server.MapPath("~/Content/Uploads/" + archivo));
                    return RedirectToAction("Index", "Home");
                }
                return View("SubirContenidos",Cont);
            }
            else
            {
                return View("SubirContenidos",Cont);
            }
        }

        [HttpGet]
        public ActionResult VerMas(int cont)
        {
            Contenidos SelectedCont = db.Contenidos.Find(cont);
            UpdateIdes(true, SelectedCont);
            string URL = SelectedCont.Ruta.Substring(SelectedCont.Ruta.Length - 4, 4);
            if (URL == ".pdf")
            {
                ViewBag.URL = "http://docs.google.com/viewer?url=http://projecteko.azurewebsites.net/Content/Uploads/" + SelectedCont.Ruta + "&embedded=true";
            }
            else
            {
                ViewBag.URL = "https://view.officeapps.live.com/op/embed.aspx?src=http://projecteko.azurewebsites.net/Content/Uploads/" + SelectedCont.Ruta;
            }
            ViewBag.Title = SelectedCont.Nombre;
            return View(SelectedCont);//porfavor
        }

        protected List<string> keywords = new List<string>();
        [HttpGet]
        public ActionResult Buscar(string Buscador)
        {
            FVM.ListaAMostrar = _Searcher(Buscador);
            FVM = CargarDropsFVM(FVM);
            Session["KeyWords"] = Buscador;
            ViewBag.Title = "Resultados: '" + Buscador.Replace(" ", "' '") + "'";
            return View("MuestraCont",FVM);
        }

        [HttpPost]
        public ActionResult Filtro(MuestraViewModel FilterParameters)
        {
            FilterParameters = CargarDropsFVM(FilterParameters);
            FilterParameters.ListaAMostrar = _Searcher((string)Session["KeyWords"]);
            FilterParameters.ListaAMostrar = _Filter(FilterParameters);
            return View("MuestraCont",FilterParameters);
        }



        public ActionResult PasarPagina(int Pagina, string Title, string kw)
        {
            if (Pagina != 0)
            {
                //SwitchTitle(Title, Pagina);
                Session["Pagina"] = Pagina;
                return View("Resultados");
            }
            else
            {
                Pagina = 1;
                //SwitchTitle(Title, Pagina);
                Session["Pagina"] = Pagina;
                return View("Resultados");
            }
        }

        #region Functions
        protected List<Contenidos> _Searcher(string Buscador)
        {
            if (Buscador == "")
            {
                FVM.ListaAMostrar = db.Contenidos.ToList();
                return FVM.ListaAMostrar;
            }
            else
            {
                string[] keywords = Buscador.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                this.keywords = keywords.ToList();
                int flag = 1;
                foreach (string item in keywords)
                {
                    if (flag == 1)
                    {
                        FVM.ListaAMostrar = db.Contenidos.Where(s => s.Nombre.Contains(item) ||
                        s.Descripcion.Contains(item) || s.Profesor.Contains(item) ||
                        s.Cursada.ToString().Contains(item) || s.Usuarios.Nombre.Contains(item) ||
                        s.Escuelas.Nombre.Contains(item) ||
                        s.NivelesEducativos.Nombre.Contains(item) ||
                        s.TiposContenidos.Nombre.Contains(item) ||
                        s.Materias.Nombre.Contains(item)).ToList();
                        flag = 0;
                    }
                    else
                    {
                        FVM.ListaAMostrar = FVM.ListaAMostrar.Where(s => s.Nombre.ToLower().Contains(item.ToLower()) ||
                        s.Descripcion.ToLower().Contains(item.ToLower()) || s.Profesor.ToLower().Contains(item.ToLower()) ||
                        s.Cursada.ToString().ToLower().Contains(item.ToLower()) || s.Usuarios.Nombre.ToLower().Contains(item.ToLower()) ||
                        s.Escuelas.Nombre.ToLower().Contains(item.ToLower()) ||
                        s.NivelesEducativos.Nombre.ToLower().Contains(item.ToLower()) ||
                        s.TiposContenidos.Nombre.ToLower().Contains(item.ToLower()) ||
                        s.Materias.Nombre.ToLower().Contains(item.ToLower())).ToList();
                    }
                }
                return FVM.ListaAMostrar;
            }
        }
        protected List<Contenidos> _Filter(MuestraViewModel Parameters)
        {
            if (Parameters.Profesor == null) Parameters.Profesor = "";
            var Lista = Parameters.ListaAMostrar
               .Where(s => s.IdEscuela == Parameters.IdEscuela || Parameters.IdEscuela == 0)
               .Where(s => s.IdMateria == Parameters.IdMateria || Parameters.IdMateria == 0)
               .Where(s => s.IdTipoContenido == Parameters.IdTipoContenido || Parameters.IdTipoContenido == 0)
               .Where(s => s.IdNivelEducativo == Parameters.IdNivelEducativo || Parameters.IdNivelEducativo == 0)
               .Where(s => s.Profesor.ToLower().Contains(Parameters.Profesor.ToLower()) || Parameters.Profesor == "")
               .ToList();
            return Lista;
        }
        protected MuestraViewModel CargarDropsFVM(MuestraViewModel FVM)
        {
            FVM.dropEscuela = db.Escuelas.ToList();
            FVM.dropMateria = db.Materias.ToList();
            FVM.dropTipoContenido = db.TiposContenidos.ToList();
            FVM.dropNivelEducativo = db.NivelesEducativos.ToList();

            return FVM;
        }
        protected SubirViewModel CargarDropsSVM(SubirViewModel SVM)
        {
            SVM.dropEscuela = db.Escuelas.ToList();
            SVM.dropMateria = db.Materias.ToList();
            SVM.dropTipoContenido = db.TiposContenidos.ToList();
            SVM.dropNivelEducativo = db.NivelesEducativos.ToList();

            return SVM;
        }
        protected void UpdateIdes(bool DesOVis, Contenidos Cont)
        {
            if (DesOVis)
            {
                Cont.IPop++;
                db.Contenidos.Attach(Cont);
                db.Entry(Cont).Property(x => x.IPop).IsModified = true;
                db.SaveChanges();
            }
            else
            {
                Cont.IDes++;
                db.Contenidos.Attach(Cont);
                db.Entry(Cont).Property(x => x.IDes).IsModified = true;
                db.SaveChanges();
            }
        }
        #endregion
    }
}