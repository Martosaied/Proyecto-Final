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
    /// <summary>
    /// This class is used to access database.
    /// </summary>
    public class Buscador
    {
        public string KeyWords { get; set; }
        /// <summary>
        /// Retrieve an individual record from database.
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns>A found record</returns>
        public Contenido GetArticle(int id)
        {
            List<Contenido> articles = QueryList("select * from [Contenido] where [ID] = " + id.ToString());

            // Only return the first record.
            if (articles.Count > 0)
            {
                return articles[0];
            }
            return null;
        }

        /// <summary>
        /// Retrieve all records from database.
        /// </summary>
        /// <returns></returns>
        public List<Contenido> GetAll()
        {
            return QueryList("select * from [Contenido]");
        }

        /// <summary>
        /// Search records from database.
        /// </summary>
        /// <param name="keywords">the list of keywords</param>
        /// <returns>all found records</returns>
        public List<Contenido> Search(List<string> keywords)
        {
            // Generate a complex Sql command.
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * from Contenido where ");
            foreach (string item in keywords)
            {
                
                sqlBuilder.AppendFormat("(Nombre like '{0}' or Descripcion like '{0}') and ", item);
            }

            // Remove unnecessary string " and " at the end of the command.
            string sql = sqlBuilder.ToString(0, sqlBuilder.Length - 5);

            return QueryList(sql);
        }

        #region Helpers

        /// <summary>
        /// Create a connected SqlCommand object.
        /// </summary>
        /// <param name="cmdText">Command text</param>
        /// <returns>SqlCommand object</returns>
        protected MySqlCommand GenerateSqlCommand(string cmdText)
        {
            // Read Connection String from web.config file.
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database=db; Uid=root; pwd=;");
            MySqlCommand cmd = new MySqlCommand(cmdText, conectar);
            cmd.Connection.Open();
            return cmd;
        }


        /// <summary>
        /// Create an Article object from a SqlDataReader object.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected Contenido ReadArticle(MySqlDataReader reader)
        {
            Contenido article = new Contenido();

            article.ID = (int)reader["IdContenido"];
            article.Nombre = (string)reader["Nombre"];
            article.Descripcion = (string)reader["Descripcion"];

            return article;
        }

        /// <summary>
        /// Excute a Sql command.
        /// </summary>
        /// <param name="cmdText">Command text</param>
        /// <returns></returns>
        protected List<Contenido> QueryList(string cmdText)
        {
            List<Contenido> articles = new List<Contenido>();

            MySqlCommand cmd = GenerateSqlCommand(cmdText);
            using (cmd.Connection)
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                // Transform records to a list.
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

        #endregion
    }
}