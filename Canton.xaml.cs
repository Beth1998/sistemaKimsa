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
    /// Lógica de interacción para Canton.xaml
    /// </summary>
    public partial class Canton : Window
    {
        SqlConnection conn;
        public Canton()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            mostrarCanton();
            getPais();
            getProvincia();
        }

        private void mostrarCanton()
        {
            string queryrCanton = "Select * from Canton";
            SqlDataAdapter adapterCanton = new SqlDataAdapter(queryrCanton, conn);
            using (adapterCanton)
            {
                DataTable dataCanton = new DataTable();
                adapterCanton.Fill(dataCanton);
                ltbCanton.DisplayMemberPath = "Nombre";
                ltbCanton.SelectedValuePath = "id_Canton";
                ltbCanton.ItemsSource = dataCanton.DefaultView;
            }
        }

        private void btnGuardarCanton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCanton.Text) ||
                cmbProvincia.SelectedItem == null ||
                cmbPais.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Regex.IsMatch(txtCanton.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                // ID PROVINCIA
                ComboBoxItem idprovincia = (ComboBoxItem)cmbProvincia.SelectedValue;
                int idProvincia = (int)idprovincia.Tag;
                // ID PAIS
                ComboBoxItem idpais = (ComboBoxItem)cmbPais.SelectedValue;
                int idPais = (int)idpais.Tag;

                string GuardarCanton = "INSERT INTO Canton (Nombre, Provincia_id, Pais_id) values (@Nombre, @Provincia_id, @Pais_id)";
                SqlCommand commaCanton = new SqlCommand(GuardarCanton, conn);
                conn.Open();
                commaCanton.Parameters.AddWithValue("@Nombre", txtCanton.Text);
                commaCanton.Parameters.AddWithValue("@Provincia_id", idProvincia);
                commaCanton.Parameters.AddWithValue("@Pais_id", idPais);
                commaCanton.ExecuteNonQuery();
                conn.Close();
                mostrarCanton();
                MessageBoxResult resultado = MessageBox.Show("LA INFORMACIÓN SE GUARDO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCanton.Text = "";
                cmbProvincia.Text = "";
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

        private void getProvincia()
        {
            string queryProvincia = "SELECT Nombre, id_Provincia FROM Provincia";
            conn.Open();
            SqlCommand commandProvincia = new SqlCommand(queryProvincia, conn);
            SqlDataReader readerProvincia = commandProvincia.ExecuteReader();

            while (readerProvincia.Read())
            {
                string guardarProvincia = readerProvincia["Nombre"].ToString();
                int idProvincia = readerProvincia.GetInt32(1);
                ComboBoxItem itemProvincia = new ComboBoxItem();
                itemProvincia.Content = guardarProvincia;
                itemProvincia.Tag = idProvincia;
                cmbProvincia.Items.Add(itemProvincia);
            }
            readerProvincia.Close();
            conn.Close();
        }

        private void btnActualizarCanton_Click(object sender, RoutedEventArgs e)
        {
            if (ltbCanton.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN CANTON PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; //RETORNA AL METODO Y NO SE SALE DE LA INTERFAZ
            }

            int idC = (int)ltbCanton.SelectedValue;

            ActualizarCanton actualizarcanton = new ActualizarCanton(idC);
            string actualizarCanton = "SELECT * FROM Canton WHERE id_Canton = " + idC;
            SqlCommand commandCanton = new SqlCommand(actualizarCanton, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandCanton); //Pasar informacion a otra ventana
            using (adapter)
            {
                DataTable dataCanton = new DataTable();
                adapter.Fill(dataCanton);
                actualizarcanton.txtCanton.Text = dataCanton.Rows[0]["Nombre"].ToString();
            }

            string queryP = "SELECT P.Nombre AS Pais FROM Canton C INNER JOIN Pais P ON C.Pais_id = P.id_Pais WHERE C.id_Canton = " + idC;
            
            conn.Open();

            SqlCommand commP = new SqlCommand(queryP, conn);
            SqlDataReader readerP = commP.ExecuteReader();

            while (readerP.Read())
            {
                string Pais = readerP["Pais"].ToString();
                actualizarcanton.cmbPais.Text = Pais;
            }

            readerP.Close();

            string queryPV = "SELECT PV.Nombre AS Provincia FROM Canton C INNER JOIN Provincia PV ON C.Provincia_id = PV.id_Provincia WHERE C.id_Canton = " + idC;

            SqlCommand commPV = new SqlCommand(queryPV, conn);
            SqlDataReader readerPV = commPV.ExecuteReader();

            while (readerPV.Read())
            {
                string Provincia = readerPV["Provincia"].ToString();
                actualizarcanton.cmbProvincia.Text = Provincia;
            }

            readerPV.Close();

            conn.Close();
            actualizarcanton.ShowDialog();
            mostrarCanton();
        }
    }
}

