using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PFEF.Models;

namespace PFEF.ViewModels
{
    public class DropsCharger
    {
        public List<Escuelas> dropEscuela { get; set; }
        public List<Materias> dropMateria { get; set; }
        public List<TiposContenidos> dropTipoContenido { get; set; }
        public List<NivelesEducativos> dropNivelEducativo { get; set; }
    }
    public class BuscadorViewModel
    {
        public string KeyWords { get; set; }
    }
    public class SubirViewModel : DropsCharger
    {


        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string Ruta { get; set; }
        [Required]
        public string Profesor { get; set; }
        [Required]
        public string Cursada { get; set; }

        public int IdUsuario { get; set; }
        [Required]
        public int IdEscuela { get; set; }
        [Required]
        public int IdMateria { get; set; }
        [Required]
        public int IdTipoContenido { get; set; }
        [Required]
        public int IdNivelEducativo { get; set; }
    }
    public class FiltrarViewModel : DropsCharger
    {
        public string Nombre { get; set; }
        public string Profesor { get; set; }
        public string Cursada { get; set; }
        public int IdUsuario { get; set; }
        public int IdEscuela { get; set; }
        public int IdMateria { get; set; }
        public int IdTipoContenido { get; set; }
        public int IdNivelEducativo { get; set; }


        //Con esto llenamos los drop en un objeto y mostrar las listas desde un objeto creado sin la necesidad de utilizar viewbags

    }
}