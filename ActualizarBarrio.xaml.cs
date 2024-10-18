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
    /// Lógica de interacción para ActualizarBarrio.xaml
    /// </summary>
    public partial class ActualizarBarrio : Window
    {
        SqlConnection conn;
        private int idBarrio;
        public ActualizarBarrio(int idBarrio)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idBarrio = idBarrio;
        }

        private void btnActualizarBarrio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIngreseBarrio.Text))
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Regex.IsMatch(txtIngreseBarrio.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                string queryrBarrio = "UPDATE Barrio set Nombre = @Nombre where id_Barrio = @idBarrio";
                SqlCommand commandBarrio = new SqlCommand(queryrBarrio, conn);
                try
                {
                    conn.Open();
                    commandBarrio.Parameters.AddWithValue("@Nombre", txtIngreseBarrio.Text);
                    commandBarrio.Parameters.AddWithValue("@idBarrio", idBarrio);
                    commandBarrio.ExecuteNonQuery();
                    MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZO EL BARRIO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"NO SE ACTUALIZO EL PAIS CORRECTAMENTE {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
