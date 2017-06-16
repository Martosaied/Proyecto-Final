using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace ProyectoFinal.Models.DataAccess
{
    public class Materias
    {
        public static List<Materia> TraerMaterias()
        {
            List<Materia> ListMat = new List<Materia>();
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from materias"), DataBaseAccess.ObtenerConexion());
            comando.ExecuteNonQuery();
            MySqlDataReader reader = comando.ExecuteReader();
            Materia Materia = new Materia();
            while (reader.Read())
            {
                Materia = new Materia();
                Materia.ID = (int)reader["IdMaterias"];
                Materia.Nombre = reader["Nombre"].ToString();
                ListMat.Add(Materia);
            }
            return ListMat;

        }
    }
}