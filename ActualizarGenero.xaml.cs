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
    /// Lógica de interacción para ActualizarGenero.xaml
    /// </summary>
    public partial class ActualizarGenero : Window
    {
        SqlConnection conn;
        private int idGenero;
        public ActualizarGenero(int idGenero)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idGenero = idGenero;
        }

        private void btnActualizarGenero_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtGenero.Text))
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Regex.IsMatch(txtGenero.Text, @"^[aA-zZ ]+$"))
            {
                string queryrGenero = "UPDATE Genero set Nombre = @Nombre where id_Genero = @idGenero";
                SqlCommand commandGenero = new SqlCommand(queryrGenero, conn);
                try
                {
                    conn.Open();
                    commandGenero.Parameters.AddWithValue("@Nombre", txtGenero.Text);
                    commandGenero.Parameters.AddWithValue("@idGenero", idGenero);
                    commandGenero.ExecuteNonQuery();
                    MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZO EL GENERO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"NO SE ACTUALIZO EL GENERO CORRECTAMENTE {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
