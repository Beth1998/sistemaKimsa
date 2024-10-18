using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para Barrio.xaml
    /// </summary>
    public partial class Barrio : Window
    {
        SqlConnection conn;
        public Barrio()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            mostrarBarrio();
        }
        private void mostrarBarrio()
        {
            string queryrBarrio = "Select * from Barrio";
            SqlDataAdapter adapterBarrio = new SqlDataAdapter(queryrBarrio, conn);
            using (adapterBarrio)
            {
                DataTable dataBarrio = new DataTable();
                adapterBarrio.Fill(dataBarrio);
                ltbBarrio.DisplayMemberPath = "Nombre";
                ltbBarrio.SelectedValuePath = "id_Barrio";
                ltbBarrio.ItemsSource = dataBarrio.DefaultView;
            }
        }

        private void btnGuardarBarrio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIngreseBarrio.Text))
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Regex.IsMatch(txtIngreseBarrio.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                string GuardarBarrio = "INSERT INTO Barrio (Nombre) values (@Nombre)";
                SqlCommand commaBarrio = new SqlCommand(GuardarBarrio, conn);
                conn.Open();
                commaBarrio.Parameters.AddWithValue("@Nombre", txtIngreseBarrio.Text);
                commaBarrio.ExecuteNonQuery();
                conn.Close();
                mostrarBarrio();
                MessageBoxResult resultado = MessageBox.Show("LA INFORMACIÓN SE GUARDO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                txtIngreseBarrio.Text = "";
            }
            else
            {
                MessageBox.Show("ERROR, POR FAVOR INGRESE SOLO LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnActualizarBarrio_Click(object sender, RoutedEventArgs e)
        {
            if (ltbBarrio.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN BARRIO PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; //RETORNA AL METODO Y NO SE SALE DE LA INTERFAZ
            }
            ActualizarBarrio ActualizarBarrio = new ActualizarBarrio((int)ltbBarrio.SelectedValue);
            string Actualizarbarrio = "select * from Barrio where id_Barrio = @idBarrio";
            SqlCommand commandBarrio = new SqlCommand(Actualizarbarrio, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandBarrio);
            using (adapter)
            {
                commandBarrio.Parameters.AddWithValue("@idBarrio", ltbBarrio.SelectedValue);
                DataTable dataBarrio = new DataTable();
                adapter.Fill(dataBarrio);
                ActualizarBarrio.txtIngreseBarrio.Text = dataBarrio.Rows[0]["Nombre"].ToString();
            }
            ActualizarBarrio.ShowDialog();
            mostrarBarrio();
        }
    }
}
