using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace ProyectoFinal.Models
{
    public class Materia
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

    }
}