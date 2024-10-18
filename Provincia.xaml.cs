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
    /// Lógica de interacción para Provincia.xaml
    /// </summary>
    public partial class Provincia : Window
    {
        SqlConnection conn;
        public Provincia()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            mostrarProvincia();
            getPais();
        }

        private void mostrarProvincia()
        {
            string queryrProvincia = "Select * from Provincia";
            SqlDataAdapter adapterProvincia = new SqlDataAdapter(queryrProvincia, conn);
            using (adapterProvincia)
            {
                DataTable dataProvincia = new DataTable();
                adapterProvincia.Fill(dataProvincia);
                ltbProvincia.DisplayMemberPath = "Nombre";
                ltbProvincia.SelectedValuePath = "id_Provincia";
                ltbProvincia.ItemsSource = dataProvincia.DefaultView;
            }

        }
       
        private void btnGuardarProvincia_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProvincia.Text) ||
                cmbPais.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Regex.IsMatch(txtProvincia.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                ComboBoxItem idpais = (ComboBoxItem)cmbPais.SelectedValue;
                int idPais = (int)idpais.Tag;

                string GuardarProvincia = "INSERT INTO Provincia (Nombre, Pais_id) values (@Nombre, @Pais_id)";
                SqlCommand commaProvincia = new SqlCommand(GuardarProvincia, conn);
                conn.Open();
                commaProvincia.Parameters.AddWithValue("@Nombre", txtProvincia.Text);
                commaProvincia.Parameters.AddWithValue("@Pais_id", idPais);
                commaProvincia.ExecuteNonQuery();
                conn.Close();
                mostrarProvincia();
                MessageBoxResult resultado = MessageBox.Show("LA INFORMACIÓN SE GUARDO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                txtProvincia.Text = "";
                cmbPais.Text = "";
            }
            else
            {
                MessageBox.Show("ERROR, POR FAVOR INGRESE SOLO LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void getPais()
        {
            string queryPais = "SELECT Nombre, id_Pais FROM Pais";
            conn.Open();
            SqlCommand commandPais = new SqlCommand(queryPais, conn); 
            SqlDataReader readerPais = commandPais.ExecuteReader(); 

            while (readerPais.Read())
            {
                string guardarPais = readerPais["Nombre"].ToString();
                int idPais = readerPais.GetInt32(1);
                ComboBoxItem itemPais = new ComboBoxItem();
                itemPais.Content = guardarPais;
                itemPais.Tag = idPais;
                cmbPais.Items.Add(itemPais);
            }
            readerPais.Close();
            conn.Close();
        }

        private void btnActualizarProvincia_Click(object sender, RoutedEventArgs e)
        {
            if (ltbProvincia.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UNA PROVINCIA PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; //RETORNA AL METODO Y NO SE SALE DE LA INTERFAZ
            }

            int idPV = (int)ltbProvincia.SelectedValue;

            ActualizarProvincia actualizarprovincia = new ActualizarProvincia(idPV);
            string actualizarProvincia = "SELECT * FROM Provincia WHERE id_Provincia = " + idPV;
            SqlCommand commandProvincia = new SqlCommand(actualizarProvincia, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandProvincia); //Pasar informacion a otra ventana
            using (adapter)
            {
                DataTable dataProvincia = new DataTable();
                adapter.Fill(dataProvincia);
                actualizarprovincia.txtProvincia.Text = dataProvincia.Rows[0]["Nombre"].ToString();                
            }

            string queryP = "SELECT P.Nombre AS Pais FROM Provincia PV INNER JOIN Pais P ON PV.Pais_id = P.id_Pais WHERE PV.id_Provincia = " + idPV;

            conn.Open();

            SqlCommand commP = new SqlCommand(queryP, conn);
            SqlDataReader readerP = commP.ExecuteReader();

            while (readerP.Read())
            {
                string Pais = readerP["Pais"].ToString();
                actualizarprovincia.cmbPais.Text = Pais;
            }
            readerP.Close();

            conn.Close();
            actualizarprovincia.ShowDialog();
            mostrarProvincia();
        }

        private void btnBorrarProvincia_Click(object sender, RoutedEventArgs e)
        {
            string borrarProvincia = "DELETE FROM Provincia WHERE id_Provincia = @idProvincia";

            SqlCommand commandProvincia = new SqlCommand(borrarProvincia, conn);
            conn.Open();
            commandProvincia.Parameters.AddWithValue("@idProvincia", ltbProvincia.SelectedValue); // SE CREA UNA INSTANCIA
            commandProvincia.ExecuteNonQuery(); //EJECUTA LA CONSULTA SQL
            try
            {
                commandProvincia.BeginExecuteNonQuery();
                MessageBoxResult resultado = MessageBox.Show("LA PROVINCIA SE HA ELIMINADO CON ÉXITO");

                if (resultado == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"LA PROVINCIA NO SE BORRO CORRECTAMENTE{ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }



    }
}
