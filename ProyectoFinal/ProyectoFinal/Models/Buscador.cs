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

namespace ProyectoFinal
{

    public class Buscador
    {
        public string KeyWords { get; set; }

        public List<Contenido> GetArticle(int id)
        {
            List<Contenido> articles = CrearLista("select contenido.IdUsuario, contenido.IdContenido, contenido.Ruta, contenido.Nombre, contenido.Descripcion, materias.Nombre AS NombreMat, escuelas.Nombre AS NombreEsc, contenido.Profesor from contenido INNER JOIN escuelas ON contenido.IdEscuela = escuelas.IdEscuelas INNER JOIN Materias ON contenido.IdMateria = materias.IdMaterias INNER JOIN niveleducativo ON contenido.NivelEdu = niveleducativo.IdNivel INNER JOIN tipodecontenido ON contenido.TipoCont = tipodecontenido.IdTipodeCont where contenido.IdUsuario = " + id);

            // Only return the first record.
            if (articles.Count > 0)
            {
                return articles;
            }
            return null;
        }


        public List<Contenido> GetAll()
        {
            return CrearLista("SELECT contenido.IdUsuario, contenido.IdContenido, contenido.Ruta, contenido.Nombre, "
                + "contenido.Descripcion, materias.Nombre AS NombreMat, escuelas.Nombre AS NombreEsc, contenido.Profesor," +
                "niveleducativo.NombreNivel AS NombreNivel, tipodecontenido.Nombretipo AS NombreTipo  from contenido " +
                " INNER JOIN escuelas ON contenido.IdEscuela = escuelas.IdEscuelas INNER JOIN Materias ON " 
                + " contenido.IdMateria = materias.IdMaterias INNER JOIN niveleducativo ON "+
                " contenido.NivelEdu = niveleducativo.IdNivel INNER JOIN tipodecontenido ON "+
                "contenido.TipoCont = tipodecontenido.IdTipodecont ");
        }


        public List<Contenido> Search(List<string> keywords)
        {
            // Generate a complex Sql command.
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT contenido.IdUsuario, contenido.IdContenido, contenido.Ruta, contenido.Nombre, "
                +"contenido.Descripcion, materias.Nombre AS NombreMat, escuelas.Nombre AS NombreEsc, contenido.Profesor,"
                +" niveleducativo.NombreNivel AS NombreNivel, tipodecontenido.Nombretipo AS NombreTipo from contenido "
                +"INNER JOIN escuelas ON contenido.IdEscuela = escuelas.IdEscuelas INNER JOIN Materias ON contenido.IdMateria "
                +"= materias.IdMaterias INNER JOIN niveleducativo ON contenido.NivelEdu = niveleducativo.IdNivel INNER JOIN "
                +"tipodecontenido ON contenido.TipoCont = tipodecontenido.IdTipodeCont  where ");

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
            article.Descripcion = (string)reader["Descripcion"];
            article.IdMateria = (string)reader["NombreMat"];
            article.IdEscuelas = (string)reader["NombreEsc"];
            article.Profesor = (string)reader["Profesor"];
            article.IdUsuario = (int)reader["IdUsuario"];
            article.IdNivelEdu = (string)reader["NombreNivel"];
            article.IdTipodeCont = (string)reader["NombreTipo"];

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