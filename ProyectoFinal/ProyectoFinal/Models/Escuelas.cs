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

        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database=db; Uid=root; pwd=;");
            conectar.Open();
            return conectar;
        }
        public static List<Escuelas> TraerEscuelas()
        {
            List<Escuelas> ListEsc = new List<Escuelas>();
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from escuelas"), ObtenerConexion());
            comando.ExecuteNonQuery();
            MySqlDataReader reader = comando.ExecuteReader();
            Escuelas Escuela = new Escuelas();
            while (reader.Read())
            {
                Escuela = new Escuelas();
                reader.Read();
                Escuela.ID = (int)reader["IdEscuelas"];
                Escuela.Nombre = reader["Nombre"].ToString();
                ListEsc.Add(Escuela);
            }
            return ListEsc;

        }
    }
}