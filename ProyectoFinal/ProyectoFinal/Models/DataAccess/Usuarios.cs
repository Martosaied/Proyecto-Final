using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.DataAccess
{
    public class Usuarios
    {
        public static int Registrar(Usuario Usuario)
        {
            int retorno = 0;
            Usuario.Foto = "~/ Content / Documentos / defecto.png";
            MySqlCommand comando = new MySqlCommand(string.Format("Insert into usuarios (Nombre, Apellido, Email, Contraseña) values ('{0}','{1}','{2}', '{3}')",
                            Usuario.Nombre, Usuario.Apellido, Usuario.Email, Usuario.Contraseña), DataBaseAccess.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static Usuario Loguear(Usuario Usuario)
        {
            int retorno;
            retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("SELECT * from usuarios WHERE Email='{0}' and Contraseña='{1}'", Usuario.Email, Usuario.Contraseña), DataBaseAccess.ObtenerConexion());
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