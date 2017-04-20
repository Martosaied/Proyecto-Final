using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace ProyectoFinal.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Contraseña { get; set; }


        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database=db; Uid=root; pwd=;");
            conectar.Open();
            return conectar;
        }

       

        public static int Registrar(Usuario Usuario)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("Insert into usuarios (Nombre, Apellido, Email, Contraseña) values ('{0}','{1}','{2}', '{3}')",
                    Usuario.Nombre, Usuario.Apellido, Usuario.Email, Usuario.Contraseña), Usuario.ObtenerConexion());
                    retorno = comando.ExecuteNonQuery();
                    return retorno;
        }

        
    }

}