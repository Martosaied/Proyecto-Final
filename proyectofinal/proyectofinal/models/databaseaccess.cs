using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data.Entity;

namespace ProyectoFinal.Models
{
    public class DataBaseAccess : DbContext
    {
        public DbSet<Contenido> Contenidos { get; set; }
        public DbSet<Usuario> Users { get; set; }
        public DbSet<Escuela>Escuela{ get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<NivelEducativo> NivelEdu { get; set; }
        public DbSet<TipoContenido> TipoCont { get; set; }



        public static MySqlCommand GenerateSqlCommand(string cmdText)
        {
            // Read Connection String from web.config file.
            MySqlConnection conectar = new MySqlConnection("Database=localdb;Data Source=127.0.0.1:51609;User Id=azure;Password=6#vWHD_$; Allow Zero Datetime=True;");
            MySqlCommand cmd = new MySqlCommand(cmdText, conectar);
            cmd.Connection.Open();
            return cmd;
        }
        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database=db; Uid=root; pwd=;");
            conectar.Open();
            return conectar;
        }

    }
}