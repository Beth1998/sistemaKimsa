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
    /// Lógica de interacción para ActualizarProvincia.xaml
    /// </summary>
    public partial class ActualizarProvincia : Window
    {
        SqlConnection conn;
        private int idProvincia; //Es un modificador de accseso que hace el encapsulamiento de datos para no poder acceder a ella desde otras clases.
        public ActualizarProvincia(int idProvincia)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idProvincia = idProvincia;
            getPais();

        }

        private void btnActualizarProvincia_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProvincia.Text) ||
                cmbPais.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ComboBoxItem comboBoxItem = cmbPais.SelectedItem as ComboBoxItem;
            int pais = (int)comboBoxItem.Tag;

            if (Regex.IsMatch(txtProvincia.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                string actualizarprovincia = "UPDATE Provincia set Nombre = @Nombre, Pais_id = @idPais where id_Provincia = @idPro";
                SqlCommand commandprovincia = new SqlCommand(actualizarprovincia, conn);

                try //try ejecuta un codigo e intenta atrapar
                {
                    conn.Open();
                    commandprovincia.Parameters.AddWithValue("Nombre", txtProvincia.Text);
                    commandprovincia.Parameters.AddWithValue("@idPro", idProvincia);
                    commandprovincia.Parameters.AddWithValue("@idPais", pais);
                    commandprovincia.ExecuteNonQuery();
                    MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZO LA PROVINCIA CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.OK)
                    {
                        //txtProvincia.Text =""
                        this.Close();
                    }
                }
                catch (SqlException ex) //Me permite capturar excepciones al momento de ejecutar la aplicacion
                {
                    MessageBox.Show($"NO SE ACTUALIZO LA PROVINCIA CORRECTAMENTE {ex.Message}","ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show($"ERROR, POR FAVOR INGRESE LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
