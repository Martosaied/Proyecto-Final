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
        [DataType(DataType.EmailAddress, ErrorMessage = "Email no valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        public static bool Logueado { get; set; }
     


        public static bool Autenticacion()
        {
            if(Usuario.Logueado)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        public static Usuario Loguear(Usuario Usuario)
        {
            int retorno;
            retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from usuarios WHERE Email='{0}' and Contraseña='{1}'", Usuario.Email, Usuario.Contraseña), Usuario.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            MySqlDataReader reader = comando.ExecuteReader();
            Usuario MiUsuario = new Usuario();

            while (reader.Read())
            { 
                
                reader.Read();
                MiUsuario.ID = (int)reader["IdUsuario"];
                MiUsuario.Nombre = reader["Nombre"].ToString();
                MiUsuario.Apellido = reader["Apellido"].ToString();
                MiUsuario.Email = Usuario.Email;
                MiUsuario.Contraseña = Usuario.Contraseña;
                return MiUsuario;
            }
                MiUsuario = null;
                return MiUsuario;

            
        }

        
    }

}