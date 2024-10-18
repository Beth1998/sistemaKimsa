using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para ActualizarDetalles.xaml
    /// </summary>
    public partial class ActualizarDetalles : Window
    {
        SqlConnection conn;
        private int idDetalles;
        public ActualizarDetalles(int idDetalles)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idDetalles = idDetalles;
            getCliente();
            getTipoR();
            getRecolector();
        }

        private void btnActualizarDetalles_Click(object sender, RoutedEventArgs e)
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
            if (!Regex.IsMatch(txtPeso.Text, @"^[0-9,]+$")) //EL PESO NO SE GUARDA CON DECIMAL
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
           // Decimal Peso = decimal.TryParse(txtPeso.Text, out peso);

                string actualizardetalles = "UPDATE Detalles set Peso = @Peso, " +
                    "Fecha_Entrega = @FechaE, Cliente_id = @Cliente_id, " +
                    "TipoResiduo_id = @TipoResiduo_id, Recolector_id = @Recolector_id where id_Detalles = @idDetalles";

            SqlCommand commandDetalles = new SqlCommand(actualizardetalles, conn);

            try //try ejecuta un codigo e intenta atrapar
            {
                conn.Open();
                commandDetalles.Parameters.AddWithValue("@Peso", double.Parse(txtPeso.Text));
                commandDetalles.Parameters.AddWithValue("@FechaE", Entrega);
                commandDetalles.Parameters.AddWithValue("@Cliente_id", idCliente);
                commandDetalles.Parameters.AddWithValue("@TipoResiduo_id", idTipoResiduo);
                commandDetalles.Parameters.AddWithValue("@Recolector_id", idRecolector);
                commandDetalles.Parameters.AddWithValue("@idDetalles", idDetalles);
                commandDetalles.ExecuteNonQuery();
                MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZÓ LA INFORMACIÓN CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);

                if (resultado == MessageBoxResult.OK)
                {
                    //txtProvincia.Text =""
                    this.Close();
                }
            }
            catch (SqlException ex) //Me permite capturar excepciones al momento de ejecutar la aplicacion
            {
                MessageBox.Show($"NO SE ACTUALIZARON LOS DETALLES CORRECTAMENTE {ex.Message}");
            }
            conn.Close();
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
    }
}
