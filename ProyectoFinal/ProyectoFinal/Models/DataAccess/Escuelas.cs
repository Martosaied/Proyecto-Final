using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace ProyectoFinal.Models.DataAccess
{
    public class Escuelas
    {
        public static List<Escuela> TraerEscuelas()
        {
            List<Escuela> ListEsc = new List<Escuela>();
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from escuelas"), DataBaseAccess.ObtenerConexion());
            comando.ExecuteNonQuery();
            MySqlDataReader reader = comando.ExecuteReader();
            Escuela Escuela = new Escuela();
            while (reader.Read())
            {
                Escuela = new Escuela();
                Escuela.ID = (int)reader["IdEscuelas"];
                Escuela.Nombre = reader["Nombre"].ToString();
                ListEsc.Add(Escuela);
            }
            return ListEsc;

        }
    }
}