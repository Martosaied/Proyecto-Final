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

        public string foto { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email no valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]{8,10}$", ErrorMessage = "La contraseña debe tener de 8 a 10 caracteres, al menos un numero y una mayuscula")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden ")]
        public string confirmarContraseña { get; set; }

        public static Usuario usuarioConectado { get; set;} 
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
            Usuario.foto = "~/ Content / Documentos / defecto.png";
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