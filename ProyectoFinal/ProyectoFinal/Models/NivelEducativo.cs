using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace ProyectoFinal.Models
{
    public class NivelEducativo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database=db; Uid=root; pwd=;");
            conectar.Open();
            return conectar;
        }
        public static List<NivelEducativo> TraerNivelEducativo()
        {
            List<NivelEducativo> ListMat = new List<NivelEducativo>();
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from niveleducativo"), ObtenerConexion());
            comando.ExecuteNonQuery();
            MySqlDataReader reader = comando.ExecuteReader();
            NivelEducativo NivelEducativo = new NivelEducativo();
            while (reader.Read())
            {
                NivelEducativo = new NivelEducativo();
                NivelEducativo.ID = (int)reader["IdNivel"];
                NivelEducativo.Nombre = reader["NombreNivel"].ToString();
                ListMat.Add(NivelEducativo);
            }
            return ListMat;

        }
    }
}