using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using ProyectoFinal.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;


namespace ProyectoFinal.Models
{
    public class Contenido
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public string IdEscuelas { get; set; }
        public string IdMateria { get; set; }
        public string IdTipodeCont { get; set; }
        public string IdNivelEdu { get; set; }
        public string Profesor { get; set; }
        public string FechadeSubida { get; set; }
        public int Pop { get; set; }
        public int Des { get; set; }
        public string KeyWords { get; set; }
        public static int Subir(Contenido Contenido,Usuario User)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("Insert into contenido (Nombre, ruta, IdUsuario, Descripcion,IdEscuela,IdMateria,Profesor, NivelEdu,TipoCont,Fechadesubida) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}', '{7}', '{8}','{9}')",
            Contenido.Nombre, Contenido.Ruta, User.ID, Contenido.Descripcion,Contenido.IdEscuelas,Contenido.IdMateria,Contenido.Profesor, Contenido.IdNivelEdu, Contenido.IdTipodeCont, Contenido.FechadeSubida), DataBaseAccess.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }
        public void UpdatePopularidad(int id, int pop)
        {
            pop++;
            MySqlCommand cmd = new MySqlCommand(string.Format("UPDATE contenido SET IPopularidad = '{0}' WHERE idContenido = '{1}'",pop, id), DataBaseAccess.ObtenerConexion());
            cmd.ExecuteNonQuery();
        }
        public void UpdateDescargas(int id, int des)
        {
            des++;
            MySqlCommand cmd = new MySqlCommand(string.Format("UPDATE contenido SET IDescargas = '{0}' WHERE idContenido = '{1}'", des, id), DataBaseAccess.ObtenerConexion());
            cmd.ExecuteNonQuery();
        }
        string GetGeneric = "SELECT contenido.IDescargas, contenido.IPopularidad, contenido.Fechadesubida, usuarios.Nombre AS Usuario, contenido.IdContenido, contenido.Ruta, contenido.Nombre, "
                + "contenido.Descripcion, materias.Nombre AS NombreMat, escuelas.Nombre AS NombreEsc, contenido.Profesor," +
                "niveleducativo.NombreNivel AS NombreNivel, tipodecontenido.Nombretipo AS NombreTipo FROM contenido "
                + "INNER JOIN usuarios ON contenido.IdUsuario = usuarios.idUsuario "
                + " INNER JOIN escuelas ON contenido.IdEscuela = escuelas.IdEscuelas INNER JOIN Materias ON "
                + " contenido.IdMateria = materias.IdMaterias INNER JOIN niveleducativo ON " +
                " contenido.NivelEdu = niveleducativo.IdNivel INNER JOIN tipodecontenido ON " +
                "contenido.TipoCont = tipodecontenido.IdTipodecont ";

        public List<Contenido> GetArticleUser(int id)
        {
            List<Contenido> articles = CrearLista(GetGeneric + "where contenido.IdUsuario = " + id);

            // Only return the first record.
            if (articles.Count > 0)
            {
                return articles;
            }
            return null;
        }
        public Contenido GetOneArticle(int id)
        {
            Contenido article = new Contenido();
            string comando = GetGeneric + "where contenido.IdContenido = " + id;
            MySqlCommand cmd = DataBaseAccess.GenerateSqlCommand(comando);
            // Only return the first record.
            using (cmd.Connection)
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        article = ReadArticle(reader);
                        return article;
                    }
                }
            }
            return null;
        }
        public List<Contenido> GetAll()
        {
            string cmd = GetGeneric;
            return CrearLista(cmd);
        }
        public List<Contenido> GetByPop()
        {
            string cmd = GetGeneric + "ORDER BY contenido.IPopularidad DESC";
            return CrearLista(cmd);
        }
        public List<Contenido> GetByDes()
        {
            string cmd = GetGeneric + "ORDER BY contenido.IDescargas DESC";
            return CrearLista(cmd);
        }
        public List<Contenido> Search(List<string> keywords)
        {
            // Generate a complex Sql command.
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(GetGeneric + " where ");

            foreach (string item in keywords)
            {

                sqlBuilder.AppendFormat("(contenido.Nombre like '%{0}%' or Descripcion like '%{0}%'or escuelas.Nombre like '%{0}%' or Materias.Nombre like '%{0}%' or Profesor like '%{0}%' or niveleducativo.NombreNivel like '%{0}%' or tipodecontenido.NombreTipo like '%{0}%') and ", item);
            }

            // Remove unnecessary string " and " at the end of the command.
            string sql = sqlBuilder.ToString(0, sqlBuilder.Length - 5);

            return CrearLista(sql);
        }
        protected Contenido ReadArticle(MySqlDataReader reader)
        {
            Contenido article = new Contenido();

            article.ID = (int)reader["IdContenido"];
            article.Nombre = (string)reader["Nombre"];
            article.Ruta = (string)reader["Ruta"];
            article.Descripcion = (string)reader["Descripcion"];
            article.IdMateria = (string)reader["NombreMat"];
            article.IdEscuelas = (string)reader["NombreEsc"];
            article.Profesor = (string)reader["Profesor"];
            article.Usuario = (string)reader["Usuario"];
            article.IdNivelEdu = (string)reader["NombreNivel"];
            article.IdTipodeCont = (string)reader["NombreTipo"];
            article.FechadeSubida = reader["Fechadesubida"].ToString();
            article.Pop = (int)reader["IPopularidad"];

            return article;
        }
        protected List<Contenido> CrearLista(string Comando)
        {
            List<Contenido> articles = new List<Contenido>();

            MySqlCommand cmd = DataBaseAccess.GenerateSqlCommand(Comando);
            using (cmd.Connection)
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        articles.Add(ReadArticle(reader));
                    }
                }
            }
            return articles;
        }




    }
}