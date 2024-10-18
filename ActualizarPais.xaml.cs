using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Lógica de interacción para ActualizarPais.xaml
    /// </summary>
    public partial class ActualizarPais : Window
    {
        SqlConnection conn;
        private int idPais;
        public ActualizarPais(int idPais)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idPais = idPais;
            
        }

        private void btnActualizarPais_Click(object sender, RoutedEventArgs e)
        {
            //LBLP.Content = idPais.ToString();   
            if (string.IsNullOrEmpty(txtPaisActualizar.Text))
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Regex.IsMatch(txtPaisActualizar.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                string queryrPais = "UPDATE Pais set Nombre = @Nombre where id_Pais = @idPais";
                SqlCommand commandPais = new SqlCommand(queryrPais, conn);
            try
            {
                conn.Open();
                commandPais.Parameters.AddWithValue("@Nombre", txtPaisActualizar.Text);
                commandPais.Parameters.AddWithValue("@idPais", idPais);
                commandPais.ExecuteNonQuery();
                MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZO EL PAIS CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);

                if (resultado == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"NO SE ACTUALIZO EL PAIS CORRECTAMENTE {ex.Message}");
            }

            conn.Close();
            }
            else
            {
                MessageBox.Show($"ERROR, POR FAVOR INGRESE LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}


