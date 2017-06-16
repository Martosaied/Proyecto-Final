using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using ProyectoFinal.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;


namespace ProyectoFinal.Models
{
    public class Contenido
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public string IdEscuelas { get; set; }
        public string IdMateria { get; set; }
        public string IdTipodeCont { get; set; }
        public string IdNivelEdu { get; set; }
        public string Profesor { get; set; }
        public string FechadeSubida { get; set; }
        public int Pop { get; set; }
        public int Des { get; set; }
        public string KeyWords { get; set; }



    }
}