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
    /// Lógica de interacción para ActualizarEstadoCivil.xaml
    /// </summary>
    public partial class ActualizarEstadoCivil : Window
    {
        SqlConnection conn;
        private int idEstadoCivil;
        public ActualizarEstadoCivil(int idEstadoCivil)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idEstadoCivil = idEstadoCivil;
        }

        private void btnActualizarEstadoCivil_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEstadoCivil.Text))
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Regex.IsMatch(txtEstadoCivil.Text, @"^[aA-zZ ]+$"))
            {
                string queryrEstadoCivil = "UPDATE Estado_Civil set Nombre = @Nombre where id_EstadoCivil = @idEstadoCivil";
                SqlCommand commandEstadoCivil = new SqlCommand(queryrEstadoCivil, conn);
                try
                {
                    conn.Open();
                    commandEstadoCivil.Parameters.AddWithValue("@Nombre", txtEstadoCivil.Text);
                    commandEstadoCivil.Parameters.AddWithValue("@idEstadoCivil", idEstadoCivil);
                    commandEstadoCivil.ExecuteNonQuery();
                    MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZO EL ESTADO CIVIL CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"NO SE ACTUALIZO EL ESTADO CIVIL CORRECTAMENTE {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
