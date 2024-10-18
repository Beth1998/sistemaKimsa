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
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para FormaPago.xaml
    /// </summary>
    public partial class FormaPago : Window
    {
        SqlConnection conn;
        private int idRecolector;
        public FormaPago(int idRecolector)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idRecolector = idRecolector;
            getDetalles();
        }

        private void btnGenerarPago_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPagoHora.Text) ||
                string.IsNullOrWhiteSpace(txtHorasTrabajadas.Text) ||
                string.IsNullOrWhiteSpace(txtHorasExtras.Text) ||
                string.IsNullOrWhiteSpace(txtFormaPago.Text) ||
                string.IsNullOrWhiteSpace(dtpckFechaPago.Text) ||
                cmbFormaPago.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACÍO. POR FAVOR, COMPLETE TODOS LOS CAMPOS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!Regex.IsMatch(txtPagoHora.Text, @"^[0-9,]+$")) 
            {
                MessageBox.Show("POR FAVOR, INGRESE UN NÚMERO ENTERO O DECIMAL EN EL CAMPO DE PAGO POR HORA.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("EN CASO DE DECIMALES USE LA COMA ( , )", "NOTA", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!Regex.IsMatch(txtHorasTrabajadas.Text, @"^\d+$")) //VALOR ENTERO
            {
                MessageBox.Show("POR FAVOR, INGRESE UN NÚMERO ENTERO PARA LAS HORAS TRABAJADAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtHorasExtras.Text, @"^\d+$"))  //VALOR ENTERO
            {
                MessageBox.Show("POR FAVOR, INGRESE UN NÚMERO ENTERO PARA LAS HORAS EXTRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtFormaPago.Text, @"^[1-2]$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE 1 O 2 SEGÚN SU FORMA DE PAGO", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("1: PAGO POR HORA\n" +
                                "2: PAGO POR PESO", "FORMA DE PAGO", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (dtpckFechaPago.SelectedDate.HasValue && dtpckFechaPago.SelectedDate.Value > DateTime.Now)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UNA FECHA DE PAGO QUE NO SEA EN EL FUTURO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double Cantidad = 0;
            int HorasTrabajadas = int.Parse(txtHorasTrabajadas.Text);
            int HorasExtras = int.Parse(txtHorasExtras.Text);
            double PagoHora = double.Parse(txtPagoHora.Text);
            Cantidad = (HorasTrabajadas + HorasExtras) * PagoHora;

            
            ComboBoxItem iddetallep = (ComboBoxItem)cmbFormaPago.SelectedValue;
            int idDetallep = (int)iddetallep.Tag;

            DateTime FechaPago = dtpckFechaPago.SelectedDate.Value;
            
            
            string GuardarPago = "INSERT INTO Detalle_Pago (id_Recolector, Horas_Trabajo," +
                "Horas_Extras, Forma_Pago, Fecha_Pago, Cantidad, id_Detalles_Recolector_Pago ) values (@id_Recolector, @Horas_Trabajo," +
                "@Horas_Extras, @Forma_Pago, @Fecha_Pago, @Cantidad, @id_DetallePago)";
            SqlCommand commandPago = new SqlCommand(GuardarPago, conn);
            conn.Open();
            commandPago.Parameters.AddWithValue("@id_Recolector", idRecolector);
            commandPago.Parameters.AddWithValue("@Horas_Trabajo", HorasTrabajadas);
            commandPago.Parameters.AddWithValue("@Horas_Extras", HorasExtras);
            commandPago.Parameters.AddWithValue("@Forma_Pago", txtFormaPago.Text);
            commandPago.Parameters.AddWithValue("@Fecha_Pago", FechaPago);
            commandPago.Parameters.AddWithValue("@Cantidad", Cantidad);
            commandPago.Parameters.AddWithValue("@id_DetallePago", idDetallep);
            try
            {
                commandPago.ExecuteNonQuery();
                MessageBoxResult resultado = MessageBox.Show("SE GUARDÓ LA INFORMACIÓN CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                if (resultado == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            catch (SqlException ex) 
            {
                MessageBox.Show($"LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void getDetalles()
        {
            string queryFP = "SELECT CONCAT(d.Fecha_Entrega, ' - ', tr.Nombre_Residuo, ' - ', r.Primer_Nombre) " +
                "AS detalles, d.id_Detalles FROM Detalles d INNER JOIN Tipo_Residuo tr ON d.TipoResiduo_id = tr.id_TipoResiduo " +
                "INNER JOIN Recolector r ON d.Recolector_id = r.id_Recolector";

            conn.Open();
            SqlCommand commandFP = new SqlCommand(queryFP, conn);
            SqlDataReader readerFP = commandFP.ExecuteReader();
            while (readerFP.Read())
            {
                string FP = readerFP["detalles"].ToString();
                int idFP = readerFP.GetInt32(1);
                ComboBoxItem itemFP = new ComboBoxItem();
                itemFP.Content = FP;
                itemFP.Tag = idFP;
                cmbFormaPago.Items.Add(itemFP);

            }
            readerFP.Close();
            conn.Close();
        }
    }
}
