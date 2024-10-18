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
    /// Lógica de interacción para Estado_civil.xaml
    /// </summary>
    public partial class Estado_civil : Window
    {
        SqlConnection conn;
        public Estado_civil()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            mostrarEstadoC();
        }

        private void mostrarEstadoC()
        {
            string queryrEstadoC = "Select * from Estado_Civil";
            SqlDataAdapter adapterEstadoC = new SqlDataAdapter(queryrEstadoC, conn);
            using (adapterEstadoC)
            {
                DataTable dataEstadoC = new DataTable();
                adapterEstadoC.Fill(dataEstadoC);
                ltbEstadoCivil.DisplayMemberPath = "Nombre";
                ltbEstadoCivil.SelectedValuePath = "id_EstadoCivil";
                ltbEstadoCivil.ItemsSource = dataEstadoC.DefaultView;
            }
        }

        private void btnGuardarEstadoCivil_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEstadoCivil.Text))
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Regex.IsMatch(txtEstadoCivil.Text, @"^[aA-zZ ]+$"))
            {
                string GuardarEstadoC = "INSERT INTO Estado_Civil (Nombre) values (@Nombre)";
                SqlCommand commaEstadoC = new SqlCommand(GuardarEstadoC, conn);
                conn.Open();
                commaEstadoC.Parameters.AddWithValue("@Nombre", txtEstadoCivil.Text);
                commaEstadoC.ExecuteNonQuery();
                conn.Close();
                mostrarEstadoC();
                MessageBoxResult resultado = MessageBox.Show("LA INFORMACIÓN SE GUARDO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                txtEstadoCivil.Text = "";
            }
            else
            {
                MessageBox.Show("ERROR, POR FAVOR INGRESE SOLO LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnActualizarEstadoCivil_Click(object sender, RoutedEventArgs e)
        {
            if (ltbEstadoCivil.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN ESTADO PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; //RETORNA AL METODO Y NO SE SALE DE LA INTERFAZ
            }
            ActualizarEstadoCivil ActualizarEstadoCivil = new ActualizarEstadoCivil((int)ltbEstadoCivil.SelectedValue);
            string Actualizarestadocivil = "select * from Estado_Civil where id_EstadoCivil = @idEstadoCivil";
            SqlCommand commandEstadoCivil = new SqlCommand(Actualizarestadocivil, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandEstadoCivil);
            using (adapter)
            {
                commandEstadoCivil.Parameters.AddWithValue("@idEstadoCivil", ltbEstadoCivil.SelectedValue);
                DataTable dataEstadoCivil = new DataTable();
                adapter.Fill(dataEstadoCivil);
                ActualizarEstadoCivil.txtEstadoCivil.Text = dataEstadoCivil.Rows[0]["Nombre"].ToString();
            }
            ActualizarEstadoCivil.ShowDialog();
            mostrarEstadoC();
        }
    }
}
