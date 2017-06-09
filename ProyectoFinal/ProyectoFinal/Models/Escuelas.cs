using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace ProyectoFinal.Models
{
    public class Escuelas
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public static List<Escuelas> TraerEscuelas()
        {
            List<Escuelas> ListEsc = new List<Escuelas>();
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from escuelas"), DataBaseAccess.ObtenerConexion());
            comando.ExecuteNonQuery();
            MySqlDataReader reader = comando.ExecuteReader();
            Escuelas Escuela = new Escuelas();
            while (reader.Read())
            {
                Escuela = new Escuelas();
                Escuela.ID = (int)reader["IdEscuelas"];
                Escuela.Nombre = reader["Nombre"].ToString();
                ListEsc.Add(Escuela);
            }
            return ListEsc;

        }

    }
}