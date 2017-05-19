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

        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database=db; Uid=root; pwd=;");
            conectar.Open();
            return conectar;
        }
        public static List<TipoContenido> TraerTipoContenidos()
        {
            List<TipoContenido> ListMat = new List<TipoContenido>();
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from niveleducativo"), ObtenerConexion());
            comando.ExecuteNonQuery();
            MySqlDataReader reader = comando.ExecuteReader();
            TipoContenido TipoContenido = new TipoContenido();
            while (reader.Read())
            {
                TipoContenido = new TipoContenido();
                TipoContenido.ID = (int)reader["IdNivel"];
                TipoContenido.Nombre = reader["NombreNivel"].ToString();
                ListMat.Add(TipoContenido);
            }
            return ListMat;

        }
    }
}