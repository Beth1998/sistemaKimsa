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

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para ActualizarCanton.xaml
    /// </summary>
    public partial class ActualizarCanton : Window
    {
        SqlConnection conn;
        private int idCanton;
        public ActualizarCanton(int idCanton)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idCanton = idCanton;
            getProvincia();
            getPais();
        }

        private void btnActualizarCanton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCanton.Text) ||
                cmbProvincia.SelectedItem == null ||
                cmbPais.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ComboBoxItem comboBoxItem = cmbProvincia.SelectedItem as ComboBoxItem;
            int prov = (int)comboBoxItem.Tag;
            ComboBoxItem comboBoxItem2 = cmbPais.SelectedItem as ComboBoxItem;
            int pais = (int)comboBoxItem2.Tag;

            if (Regex.IsMatch(txtCanton.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                string actualizarcanton = "UPDATE Canton set Nombre = @Nombre, Pais_id = @idPa, Provincia_id = @idProv where id_Canton = @idCan";
                SqlCommand commandcanton = new SqlCommand(actualizarcanton, conn);

                try //try ejecuta un codigo e intenta atrapar
                {
                    conn.Open();
                    commandcanton.Parameters.AddWithValue("Nombre", txtCanton.Text);
                    commandcanton.Parameters.AddWithValue("@idCan", idCanton);
                    commandcanton.Parameters.AddWithValue("@idProv", prov);
                    commandcanton.Parameters.AddWithValue("@idPa", pais);
                    commandcanton.ExecuteNonQuery();
                    MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZO LA PROVINCIA CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.OK)
                    {
                        //txtProvincia.Text =""
                        this.Close();
                    }
                }
                catch (SqlException ex) //Me permite capturar excepciones al momento de ejecutar la aplicacion
                {
                    MessageBox.Show($"NO SE ACTUALIZO EL CANTON CORRECTAMENTE {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show($"ERROR, POR FAVOR INGRESE LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void getPais()
        {
            string queryPais = "SELECT Nombre, id_Pais FROM Pais"; //Hacemos la consulta
            conn.Open();//Abrimos la conexion con SQL
            SqlCommand commandPais = new SqlCommand(queryPais, conn); //Creamos una instancia para llamar a un metodo
            SqlDataReader readerPais = commandPais.ExecuteReader(); // Metodo ExecuteReader al que llamamos para leer la informacion

            while (readerPais.Read())
            {
                string guardarPais = readerPais["Nombre"].ToString();
                int idPais = readerPais.GetInt32(1);
                ComboBoxItem itemPais = new ComboBoxItem();//Esta instancia me permite llenar informacion
                itemPais.Content = guardarPais;
                itemPais.Tag = idPais;
                cmbPais.Items.Add(itemPais);
            }
            readerPais.Close();
            conn.Close();
        }
    }
}
