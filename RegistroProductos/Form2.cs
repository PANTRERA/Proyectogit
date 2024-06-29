using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroProductos
{
    public partial class Form2 : Form
    {
        private string productoId;
        private string codigo;
        private string nombre;
        private string descripcion;
        private decimal precio;
        private decimal costoc;
        private int existencias;

  
            public Form2(string id, string codigo, string nombre, string descripcion, decimal precio, decimal costoc, int existencias)
        {
            InitializeComponent();
            this.productoId = id;
            this.codigo = codigo;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            this.costoc = costoc;
            this.existencias = existencias;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string idUsuario = txtIdUsuario.Text.Trim();
            if (string.IsNullOrEmpty(idUsuario))
            {
                MessageBox.Show("Por favor, ingrese un codigo de usuario.");
                return;
            }
            bool codeValid = CheckUserCode(idUsuario);
            if (!codeValid)
            {
                MessageBox.Show("Código inválido.  NO Puede actualizar los productos.");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            using (MySqlConnection conn = Conexion.conexion())
            {


                try
                {
                    conn.Open();

                    var query = "UPDATE productos SET Nombre =@nombre, Precio_publico = @Precio, Costo_compra = @Costo, Existencias = @Existencias WHERE idProductos = @id";
                  var command = new MySqlCommand(query, conn);

                    command.Parameters.AddWithValue("@id", productoId);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Precio", precio);
                    command.Parameters.AddWithValue("@Costo", costoc);
                    command.Parameters.AddWithValue("@Existencias", existencias);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Producto actualizado correctamente.");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el producto con el ID especificado.");
                        this.DialogResult = DialogResult.Cancel;
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar: " + ex.Message);
                }
            }
        }

        private bool CheckUserCode(string idUsuario)
        {
            using (MySqlConnection conn = Conexion.conexion())
            {
                conn.Open();
                string userQuery = "SELECT COUNT(*) FROM Usuarios WHERE IdUsuario = @IdUsuario AND TienePermisos = 1";
                MySqlCommand userCommand = new MySqlCommand(userQuery, conn);
                userCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);

                int userCount = Convert.ToInt32(userCommand.ExecuteScalar());
                return userCount > 0;
            }
        }
    }
}


