
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{
    class Conexion
    {
        public static MySqlConnection conexion()
        {
            String servidor = "remotemysql.com";
            String puerto = "3306";
            String usuario = "v9J9PFbkOe";
            String password = "theolG54Rd";
            String BD = "v9J9PFbkOe";
            /*
             String servidor = "127.0.0.1";
            String puerto = "3306";
            String usuario = "root";
            String password = "root";
            String BD = "emeger";
             */
            String connection = ";datasource=" + servidor + ";port=" + puerto + ";username=" + usuario + ";password=" + password + ";database=" + BD;
            //string connection = "datasource=127.0.0.1; port=3306; username=admin; password=admin; persistsecurityinfo=True; database=emeger; SslMode=none;"; 


            try
            {
                MySqlConnection connectionDB = new MySqlConnection(connection);
                Console.WriteLine("Conectando a la BD");
                return connectionDB;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
