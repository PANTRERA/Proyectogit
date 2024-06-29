using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P1_Conexion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnConectar_Click(object sender, EventArgs e)
        {
            String servidor = txtServidor.Text;
            String puerto = txtPuerto.Text;
            String usuario = txtUsuario.Text;
            String password = txtPassword.Text;
            String bd = txtBD.Text;

            //Sentencia de conexion
            String cadenadeconexion = "Database=" + bd + ";DataSource=" + servidor + ";Port=" + puerto + ";User id=" + usuario + ";Password=" + password;

            //Comandos de conexion
            MySqlConnection conexion = new MySqlConnection(cadenadeconexion);
            MySqlDataReader reader = null;
            String data = null;
        
            try{
                String consulta = "HA CONECTADO A LA BASE DE DATOS CORRECTAMENTE";
                MySqlCommand cmd = new MySqlCommand(consulta);
                cmd.Connection = conexion;
                conexion.Open();
                reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    data += reader.GetString(0);
                }
                MessageBox.Show(data);
            }catch (MySqlException ex) 
            {
                MessageBox.Show("Error al conectar"+ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
