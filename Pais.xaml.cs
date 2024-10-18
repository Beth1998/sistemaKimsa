using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para Pais.xaml
    /// </summary>
    public partial class Pais : Window
    {
        SqlConnection conn;
        public Pais()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            mostrarPais();

        }

        private void mostrarPais()
        {
            string queryrPais = "Select Nombre, id_Pais from Pais";
            SqlDataAdapter adapterPais = new SqlDataAdapter(queryrPais, conn);
            using (adapterPais)
            {
                DataTable dataPais = new DataTable();
                adapterPais.Fill(dataPais);
                ltbPais.DisplayMemberPath = "Nombre";
                ltbPais.SelectedValuePath = "id_Pais";
                ltbPais.ItemsSource = dataPais.DefaultView;
            }
        }
        private void btnGuardarPais_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPais.Text))
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Regex.IsMatch(txtPais.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                string queryrPais = "INSERT INTO Pais (Nombre) values (@Nombre)";
                SqlCommand commandPais = new SqlCommand(queryrPais, conn);
                conn.Open();
                commandPais.Parameters.AddWithValue("@Nombre", txtPais.Text);
                commandPais.ExecuteNonQuery();
                conn.Close();
                mostrarPais();
                MessageBoxResult resultado = MessageBox.Show("LA INFORMACIÓN SE GUARDO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPais.Text = "";
            }
            else
            {
                MessageBox.Show("ERROR, POR FAVOR INGRESE SOLO LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnActualizarPais_Click(object sender, RoutedEventArgs e)
        {
            if (ltbPais.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN PAIS PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; //RETORNA AL METODO Y NO SE SALE DE LA INTERFAZ
            }
            ActualizarPais ActualizarPais = new ActualizarPais((int)ltbPais.SelectedValue);
            string Actualizarpais = "select * from Pais where id_Pais = @idPais";
            SqlCommand commandPais = new SqlCommand(Actualizarpais, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandPais);
            using (adapter)
            {
                commandPais.Parameters.AddWithValue("@idPais", ltbPais.SelectedValue);
                DataTable dataPais = new DataTable();
                adapter.Fill(dataPais);
                ActualizarPais.txtPaisActualizar.Text = dataPais.Rows[0]["Nombre"].ToString();
            }
            ActualizarPais.ShowDialog();
            mostrarPais();
        }
    }
}
