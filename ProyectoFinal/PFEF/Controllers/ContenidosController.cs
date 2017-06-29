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

        // GET: Contenidos
        public ActionResult VerTodo(string Title)
        {
            SwitchTitle(Title);
            FiltrarViewModel FVM = new FiltrarViewModel();
            FVM = CargarDropsFVM(FVM);
            return View("MuestraCont",FVM);
        }

        protected void SwitchTitle(string Title)
        {
            switch (Title)
            {
                case "Mas recientes":
                    var Lista = db.Contenidos.OrderByDescending(x => x.Id).ToList();
                    ViewBag.Contenido = Lista;
                    ViewBag.Title = Title;
                    break;
                case "Mas populares":
                    Lista = db.Contenidos.OrderByDescending(x => x.IPop).ToList();
                    ViewBag.Contenido = Lista;
                    ViewBag.Title = Title;
                    break;
                case "Mas descargados":
                    Lista = db.Contenidos.OrderByDescending(x => x.IPop).ToList();
                    ViewBag.Contenido = Lista;
                    ViewBag.Title = Title;
                    break;
                case "Mis subidas":
                    Usuarios User = (Usuarios)Session["User"];
                    Lista = db.Contenidos.Where(x => x.IdUsuario == User.Id).ToList();
                    ViewBag.Contenido = Lista;
                    ViewBag.Title = Title;
                    break;
                case "Resultados":
                    string kw = (string)Session["KeyWords"];
                    RedirectToAction("Buscar", kw);
                    break;
            }
        }

        public ActionResult Descargar(int ID)
        {
            Contenidos selected = new Contenidos();
            selected = db.Contenidos.Where(x => x.Id == ID).SingleOrDefault();
            //Contenidos.UpdateDescargas(selected.ID, selected.Des);
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

        protected FiltrarViewModel CargarDropsFVM (FiltrarViewModel FVM)
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
            FiltrarViewModel FVM = new FiltrarViewModel();
            if (Buscador == "")
            {
                var Lista = db.Contenidos.ToList();
                FVM = CargarDropsFVM(FVM);
                ViewBag.Contenido = Lista.ToList();
                return View("MuestraCont",FVM);
            }
            else
            {
                // Turn user input to a list of keywords.
                string[] keywords = Buscador.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                this.keywords = keywords.ToList();
                var Listamaster = Enumerable.Empty<Contenidos>();
                int flag = 1;
                foreach (string item in keywords)
                {
                    if (flag == 1)
                    {
                        Listamaster = db.Contenidos.Where(s => s.Nombre.Contains(item) ||
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
                        Listamaster = Listamaster.Where(s => s.Nombre.ToLower().Contains(item.ToLower()) || 
                        s.Descripcion.ToLower().Contains(item.ToLower()) || s.Profesor.ToLower().Contains(item.ToLower()) || 
                        s.Cursada.ToString().ToLower().Contains(item.ToLower()) || s.Usuarios.Nombre.ToLower().Contains(item.ToLower()) || 
                        s.Escuelas.Nombre.ToLower().Contains(item.ToLower()) ||
                        s.NivelesEducativos.Nombre.ToLower().Contains(item.ToLower()) || 
                        s.TiposContenidos.Nombre.ToLower().Contains(item.ToLower()) || 
                        s.Materias.Nombre.ToLower().Contains(item.ToLower())).ToList();
                    }
                }
                List<Contenidos> lista = Listamaster.ToList();
                FVM = CargarDropsFVM(FVM);
                ViewBag.Contenido = lista;
                Session["KeyWords"] = Buscador;
                ViewBag.Title = "Resultados: " + Buscador.Replace(" ", "' '");
                return View("MuestraCont",FVM);
            }
        }

        [HttpGet]
        public ActionResult Filtro(FiltrarViewModel FilterParameters)
        {
            var result = db.Contenidos
                .Where(s => s.IdEscuela == FilterParameters.IdEscuela || FilterParameters.IdEscuela == 0)
                .Where(s => s.IdMateria == FilterParameters.IdMateria || FilterParameters.IdMateria == 0)
                .Where(s => s.IdTipoContenido == FilterParameters.IdTipoContenido || FilterParameters.IdTipoContenido == 0)
                .Where(s => s.IdNivelEducativo == FilterParameters.IdNivelEducativo || FilterParameters.IdNivelEducativo == 0)
                .ToList();
            FilterParameters = CargarDropsFVM(FilterParameters);
            ViewBag.Contenido = result;
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
    }
}