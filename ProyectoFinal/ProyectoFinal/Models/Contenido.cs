using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;


namespace ProyectoFinal.Models
{
    public class Contenido
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuario { get; set; }

        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database=db; Uid=root; pwd=;");
            conectar.Open();
            return conectar;
        }

        public static int Subir(Contenido Contenido,Usuario User)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(string.Format("Insert into contenido (Nombre, ruta, IdUsuario) values ('{0}','{1}','{2}')",
                            Contenido.Nombre, Contenido.Ruta, User.ID), Contenido.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }


    }
}