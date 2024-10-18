using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Configuration;

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para Genero.xaml
    /// </summary>
    public partial class Genero : Window
    {
        SqlConnection conn;
        public Genero()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            mostrarGenero();
        }

        private void mostrarGenero()
        {
            string queryrGenero = "Select * from Genero";
            SqlDataAdapter adapterGenero = new SqlDataAdapter(queryrGenero, conn);
            using (adapterGenero)
            {
                DataTable dataGenero = new DataTable();
                adapterGenero.Fill(dataGenero);
                ltbGenero.DisplayMemberPath = "Nombre";
                ltbGenero.SelectedValuePath = "id_Genero";
                ltbGenero.ItemsSource = dataGenero.DefaultView;
            }
        }

        private void btnGuardarGenero_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtGenero.Text))
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Regex.IsMatch(txtGenero.Text, @"^[aA-zZ ]+$"))
            {
                string GuardarGenero = "INSERT INTO Genero (Nombre) values (@Nombre)";
                SqlCommand commaGenero = new SqlCommand(GuardarGenero, conn);
                conn.Open();
                commaGenero.Parameters.AddWithValue("@Nombre", txtGenero.Text);
                commaGenero.ExecuteNonQuery();
                conn.Close();
                mostrarGenero();
                MessageBoxResult resultado = MessageBox.Show("LA INFORMACIÓN SE GUARDO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                txtGenero.Text = "";
            }
            else
            {
                MessageBox.Show("ERROR, POR FAVOR INGRESE SOLO LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnActualizarGenero_Click(object sender, RoutedEventArgs e)
        {
            if (ltbGenero.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN GENERO PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; //RETORNA AL METODO Y NO SE SALE DE LA INTERFAZ
            }
            ActualizarGenero ActualizarGenero = new ActualizarGenero((int)ltbGenero.SelectedValue);
            string Actualizargenero = "select * from Genero where id_Genero = @idGenero";
            SqlCommand commandGenero = new SqlCommand(Actualizargenero, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandGenero);
            using (adapter)
            {
                commandGenero.Parameters.AddWithValue("@idGenero", ltbGenero.SelectedValue);
                DataTable dataGenero = new DataTable();
                adapter.Fill(dataGenero);
                ActualizarGenero.txtGenero.Text = dataGenero.Rows[0]["Nombre"].ToString();
            }
            ActualizarGenero.ShowDialog();
            mostrarGenero();
        }
    }
}
