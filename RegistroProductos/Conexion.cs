using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroProductos
{
    internal class Conexion
    {
        //conexion a la BD
        public static MySqlConnection conexion()
        {
            //variables de conexion
            String servidor = "localhost";
            String bd = "proyecto1";
            String usuario = "root";
            String pass = "Mihuella1";
            String sentenciasql = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + pass + "";


            try
            {
                MySqlConnection BDconexion = new MySqlConnection(sentenciasql);
                return BDconexion;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error : " + e.Message);
                return null;
            }

        }
    }
}