using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para Detalles.xaml
    /// </summary>
    public partial class Detalles : Window
    {
        SqlConnection conn;
        public Detalles()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            MostrarDetalles();
            getCliente();
            getTipoR();
            getRecolector();
        }

        private void MostrarDetalles()
        {
            string queryDetalles = "SELECT * FROM Detalles";
            SqlDataAdapter adapterDetalles = new SqlDataAdapter(queryDetalles, conn);

            using (adapterDetalles)
            {
                DataTable dataDetalles = new DataTable();
                adapterDetalles.Fill(dataDetalles);
                ltbDetalles.DisplayMemberPath = "Fecha_Entrega";
                ltbDetalles.SelectedValuePath = "id_Detalles";
                ltbDetalles.ItemsSource = dataDetalles.DefaultView;
            }
        }

        private void btnGuardarDetalles_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPeso.Text) ||
                string.IsNullOrWhiteSpace(dtpckFechaEntrega.Text) ||
                cmbCliente.SelectedItem == null ||
                cmbTipoResiduo.SelectedItem == null ||
                cmbRecolector.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACÍO. POR FAVOR, COMPLETE TODOS LOS CAMPOS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }        
            if (!Regex.IsMatch(txtPeso.Text, @"^[0-9,]+$")) //GUARDA EN DECIMAL
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO NÚMEROS EN EL CAMPO DE PESO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ID CLIENTE
            ComboBoxItem idcliente = (ComboBoxItem)cmbCliente.SelectedValue;
            int idCliente = (int)idcliente.Tag;
            // ID TIPO RESIDUO
            ComboBoxItem idtipoR = (ComboBoxItem)cmbTipoResiduo.SelectedValue;
            int idTipoResiduo = (int)idtipoR.Tag;
            // ID RECOLECTOR
            ComboBoxItem idrecolector = (ComboBoxItem)cmbRecolector.SelectedValue;
            int idRecolector = (int)idrecolector.Tag;

            DateTime Entrega = dtpckFechaEntrega.SelectedDate.Value;

           /* string DescripcionPago = "SELECT CONCAT(d.Fecha_Entrega, ' - ', tr.Nombre_Residuo, ' - ', r.Primer_Nombre) " +
                "AS detalles FROM Detalles d INNER JOIN Tipo_Residuo tr ON d.TipoResiduo_id = tr.id_TipoResiduo INNER JOIN " +
                "Recolector r ON d.Recolector_id = r.id_Recolector WHERE tr.id_TipoResiduo =" +
                 idTipoResiduo + " AND r.id_Recolector =" + idTipoResiduo;

            SqlCommand commandDetallesPago;
            using (SqlCommand commandDetallesPago = new SqlCommand(DescripcionPago, conn))
            {
                commandDetallesPago.Parameters.AddWithValue("@idTR", idTipoResiduo);
                commandDetallesPago.Parameters.AddWithValue("@idTR", idTipoResiduo);
            }

            SqlDataReader reader = commandDP.ExecuteReader();

            string Detalle = null;

            while (reader.Read())
            {
                string Detalle = reader["Nombre"].ToString();
            }
            reader.Close();
            conn.Close();*/

            string insertarDetalles = "INSERT INTO Detalles (Peso, Fecha_Entrega, Cliente_id, TipoResiduo_id, Recolector_id, DescripcionPago) " +
                "VALUES(@Peso, @FechaE, @Cliente, @TipoR, @Recolector, @DescripcionPago)";

            using (SqlCommand commandDetalles = new SqlCommand(insertarDetalles, conn))
            {
                try
                {
                    conn.Open();
                    commandDetalles.Parameters.AddWithValue("@Peso", double.Parse(txtPeso.Text));
                    commandDetalles.Parameters.AddWithValue("@FechaE", Entrega);
                    commandDetalles.Parameters.AddWithValue("@Cliente", idCliente);
                    commandDetalles.Parameters.AddWithValue("@TipoR", idTipoResiduo);
                    commandDetalles.Parameters.AddWithValue("@Recolector", idRecolector);

                    // Ejecución del comando SQL
                    commandDetalles.ExecuteNonQuery();
                    MessageBox.Show("SE GUARDÓ LA INFORMACIÓN CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
            }
        }
        private void getCliente()
        {
            string queryCliente = "SELECT Primer_Nombre, id_Cliente FROM Cliente";
            conn.Open();
            SqlCommand commandCliente = new SqlCommand(queryCliente, conn);
            SqlDataReader readerCliente = commandCliente.ExecuteReader();

            while (readerCliente.Read())
            {
                string guardarCliente = readerCliente["Primer_Nombre"].ToString();
                int idCliente = readerCliente.GetInt32(1);
                ComboBoxItem itemCliente = new ComboBoxItem();
                itemCliente.Content = guardarCliente;
                itemCliente.Tag = idCliente;
                cmbCliente.Items.Add(itemCliente);
            }
            readerCliente.Close();
            conn.Close();
        }

        private void getTipoR()
        {
            string queryTipoR = "SELECT Nombre_Residuo, id_TipoResiduo FROM Tipo_Residuo";
            conn.Open();
            SqlCommand commandTipoR = new SqlCommand(queryTipoR, conn);
            SqlDataReader readerTipoR = commandTipoR.ExecuteReader();

            while (readerTipoR.Read())
            {
                string guardarTipoR = readerTipoR["Nombre_Residuo"].ToString();
                int idTipoR = readerTipoR.GetInt32(1);
                ComboBoxItem itemTipoR = new ComboBoxItem();
                itemTipoR.Content = guardarTipoR;
                itemTipoR.Tag = idTipoR;
                cmbTipoResiduo.Items.Add(itemTipoR);
            }
            readerTipoR.Close();
            conn.Close();
        }

        private void getRecolector()
        {
            string queryRecolector = "SELECT Primer_Nombre, id_Recolector FROM Recolector";
            conn.Open();
            SqlCommand commandRecolector = new SqlCommand(queryRecolector, conn);
            SqlDataReader readerRecolector = commandRecolector.ExecuteReader();

            while (readerRecolector.Read())
            {
                string guardarRecolector = readerRecolector["Primer_Nombre"].ToString();
                int idRecolector = readerRecolector.GetInt32(1);
                ComboBoxItem itemRecolector = new ComboBoxItem();
                itemRecolector.Content = guardarRecolector;
                itemRecolector.Tag = idRecolector;
                cmbRecolector.Items.Add(itemRecolector);
            }
            readerRecolector.Close();
            conn.Close();
        }

        private void btnActualizarDetalles_Click(object sender, RoutedEventArgs e)
        {
            if (ltbDetalles.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN DETALLE PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int idDe = (int)ltbDetalles.SelectedValue;

            ActualizarDetalles actualizardetalles = new ActualizarDetalles(idDe);
            string actualizarDetalles = "SELECT * FROM Detalles WHERE id_Detalles = " + idDe;
            SqlCommand commandDetalles = new SqlCommand(actualizarDetalles, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandDetalles); //Pasar informacion a otra ventana
            using (adapter)
            {
                DataTable dataDetalles = new DataTable();
                adapter.Fill(dataDetalles);
                actualizardetalles.txtPeso.Text = dataDetalles.Rows[0]["Peso"].ToString();
                actualizardetalles.dtpckFechaEntrega.SelectedDate = Convert.ToDateTime(dataDetalles.Rows[0]["Fecha_Entrega"]);
                actualizardetalles.cmbCliente.SelectedValue = dataDetalles.Rows[0]["Cliente_id"].ToString();
                actualizardetalles.cmbTipoResiduo.SelectedValue = dataDetalles.Rows[0]["TipoResiduo_id"].ToString();
                actualizardetalles.cmbRecolector.SelectedValue = dataDetalles.Rows[0]["Recolector_id"].ToString();
            }

            string queryCliente = "SELECT C.Primer_Nombre AS Cliente FROM Cliente C INNER JOIN  Detalles D ON C.id_Cliente = D.Cliente_id WHERE D.id_Detalles = " + idDe;
            conn.Open();

            SqlCommand commCliente = new SqlCommand(queryCliente, conn);
            SqlDataReader readerCliente = commCliente.ExecuteReader();

            while (readerCliente.Read())
            {
                string Cliente = readerCliente["Cliente"].ToString();
                actualizardetalles.cmbCliente.Text = Cliente;
            }
            readerCliente.Close();


            string queryRecolector = "SELECT R.Primer_Nombre AS Recolector FROM Recolector R INNER JOIN  Detalles D ON R.id_Recolector = D.Recolector_id WHERE D.id_Detalles = " + idDe;

            SqlCommand commRecolector = new SqlCommand(queryRecolector, conn);
            SqlDataReader readerRecolector = commRecolector.ExecuteReader();

            while (readerRecolector.Read())
            {
                string Recolector = readerRecolector["Recolector"].ToString();
                actualizardetalles.cmbRecolector.Text = Recolector;
            }
            readerRecolector.Close();


            string queryTipoR = "SELECT T.Nombre_Residuo AS Residuo FROM Tipo_Residuo T INNER JOIN  Detalles D ON T.id_TipoResiduo = D.TipoResiduo_id WHERE D.id_Detalles = " + idDe;

            SqlCommand commTipoR = new SqlCommand(queryTipoR, conn);
            SqlDataReader readerTipoR = commTipoR.ExecuteReader();

            while (readerTipoR.Read())
            {
                string TipoR = readerTipoR["Residuo"].ToString();
                actualizardetalles.cmbTipoResiduo.Text = TipoR;
            }
            readerTipoR.Close();



            conn.Close();
            actualizardetalles.ShowDialog();
            MostrarDetalles();
        }
    }
}

