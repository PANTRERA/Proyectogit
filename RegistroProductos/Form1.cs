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

namespace RegistroProductos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtid.Text, out int id))
            {
                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Productos WHERE idProducto = @id";
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Producto eliminado exitosamente.");
                            // Actualizar la lista de productos
                            btnlimpiar.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el producto con el ID especificado.");
                            // Actualizar la lista de productos
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un Id válido");
            }
        }
        private void btnactualizar_Click(object sender, EventArgs e)
        {
            // Capturar los valores de los textbox
            string id = txtid.Text;
            string codigo = txtcodigo.Text;
            string nombre = txtnombre.Text;
            string descripcion = txtdescripcion.Text;
            decimal precio = decimal.Parse(txtpreciop.Text);
            decimal costoc = decimal.Parse(txtcostoc.Text);
            int existencia = int.Parse(txtexistencias.Text);

            // Actualizar el registro en la base de datos
            string sql = "UPDATE productos SET codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, Costo_compra = @costoc, Precio_publico = @precio, Existencias = @existencia WHERE idProducto = @id";

            using (MySqlConnection con = Conexion.conexion())
            {
                con.Open();
                try
                {
                    MySqlCommand command = new MySqlCommand(sql, con);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@codigo", codigo);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@costoc", costoc);
                    command.Parameters.AddWithValue("@precio", precio);
                    command.Parameters.AddWithValue("@existencia", existencia);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registro actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el producto con el ID especificado.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al modificar: " + ex.Message);
                }
            }
        }

        void btnbuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtcodigo.Text;
                MySqlDataReader reader= null;
            String sql = "SELECT idProducto, codigo, Nombre, Descripcion, Costo_compra, Precio_publico, Existencias FROM productos WHERE codigo LIKE '" + codigo + "' LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion();

            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtid.Text = reader.GetInt32(0).ToString();
                        txtcodigo.Text = reader.GetString(1);
                        txtnombre.Text = reader.GetString(2);
                        txtdescripcion.Text = reader.GetString(3);
                        txtcostoc.Text = reader.GetDecimal(4).ToString();
                        txtpreciop.Text = reader.GetDecimal(5).ToString();
                        txtexistencias.Text = reader.GetInt32(6).ToString();
                        
                    }
                }
                else
                {
                    MessageBox.Show(" No se encontraron registros con ese codigo");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

            void btnagregar_Click(object sender, EventArgs e)
        {
            //capturar los valores de los textbox
            String codigo = txtcodigo.Text;
            String nombre = txtnombre.Text;
            String descripcion = txtdescripcion.Text;
            double precio = double.Parse(txtpreciop.Text);
            double costoc = double.Parse(txtcostoc.Text);
            int existencia = int.Parse(txtexistencias.Text);

            string sql = "INSERT INTO productos( codigo, Nombre, Descripcion, Costo_compra, Precio_publico, Existencias)" + "VALUES ('" + codigo + "', '" + nombre + "', '" + descripcion + "', '" + costoc + "', '" + precio + "', '" + existencia + "')";

            //INSTANCIA A LA CLASE CONEXION
            MySqlConnection con = Conexion.conexion();
            con.Open();

            try
            {
                //COMPROBAR GUARDADO EN BD
                MySqlCommand command = new MySqlCommand(sql, con);
                command.ExecuteNonQuery();
                MessageBox.Show("REGISTRO EXITOSO");
            } catch (MySqlException ex)
            {
                MessageBox.Show("ERROR AL REGISTRAR:" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        void groupBox1_Enter(object sender, EventArgs e)
        {

        }

            void btnlimpiar_Click(object sender, EventArgs e)
        {
            txtid.Clear();
            txtcodigo.Clear();
            txtnombre.Clear();
            txtpreciop.Clear();
            txtcostoc.Clear(); 
            txtdescripcion.Clear(); 
            txtexistencias.Clear();
        }
    }
}
