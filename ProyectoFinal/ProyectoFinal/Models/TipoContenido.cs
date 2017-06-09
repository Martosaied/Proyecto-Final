using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace ProyectoFinal.Models
{
    public class TipoContenido
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public static List<TipoContenido> TraerTipoContenidos()
        {
            List<TipoContenido> ListMat = new List<TipoContenido>();
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from tipodecontenido"), DataBaseAccess.ObtenerConexion());
            comando.ExecuteNonQuery();
            MySqlDataReader reader = comando.ExecuteReader();
            TipoContenido TipoContenido = new TipoContenido();
            while (reader.Read())
            {
                TipoContenido = new TipoContenido();
                TipoContenido.ID = (int)reader["IdTipodecont"];
                TipoContenido.Nombre = reader["NombreTipo"].ToString();
                ListMat.Add(TipoContenido);
            }
            return ListMat;

        }
    }
}