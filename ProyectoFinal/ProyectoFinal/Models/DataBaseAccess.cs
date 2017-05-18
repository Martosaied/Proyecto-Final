using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace ProyectoFinal.Models
{
    public class DataBaseAccess
    {
        public static MySqlCommand GenerateSqlCommand(string cmdText)
        {
            // Read Connection String from web.config file.
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database=db; Uid=root; pwd=root;");
            MySqlCommand cmd = new MySqlCommand(cmdText, conectar);
            cmd.Connection.Open();
            return cmd;
        }

    }
}