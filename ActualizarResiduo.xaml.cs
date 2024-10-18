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
    /// Lógica de interacción para ActualizarResiduo.xaml
    /// </summary>
    public partial class ActualizarResiduo : Window
    {
        SqlConnection conn;
        private int idTipoR;
        public ActualizarResiduo(int idTipoR)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idTipoR = idTipoR;
            getCategoria();
        }

        private void btnActualizarTipoResiduo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTipoResiduo.Text) ||
                cmbCategoriaR.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ComboBoxItem comboBoxItem = cmbCategoriaR.SelectedItem as ComboBoxItem;
            int SubCategoria = (int)comboBoxItem.Tag;

            if (Regex.IsMatch(txtTipoResiduo.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                string queryrResiduo = "UPDATE Tipo_Residuo set Nombre_Residuo = @Nombre, id_Sub_CategoriaR = @SubCategoria where id_TipoResiduo = @idTipoR";
                SqlCommand commandResiduo = new SqlCommand(queryrResiduo, conn);
                try
                {
                    conn.Open();
                    commandResiduo.Parameters.AddWithValue("@Nombre", txtTipoResiduo.Text);
                    commandResiduo.Parameters.AddWithValue("@SubCategoria", SubCategoria);
                    commandResiduo.Parameters.AddWithValue("@idTipoR", idTipoR);
                    commandResiduo.ExecuteNonQuery();
                    MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZO EL RESIDUO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK);

                    if (resultado == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"NO SE ACTUALIZO EL RESIDUO CORRECTAMENTE {ex.Message}");
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show($"ERROR, POR FAVOR INGRESE LETRAS.");
            }

        }
        private void getCategoria()
        {
            string querySubCategoria = "SELECT Nombre, id_Sub_CategoriaR FROM Sub_CategoriaR"; //Hacemos la consulta
            conn.Open();//Abrimos la conexion con SQL
            SqlCommand commandSubCategoria = new SqlCommand(querySubCategoria, conn); //Creamos una instancia para llamar a un metodo
            SqlDataReader readerSubCategoria = commandSubCategoria.ExecuteReader(); // Metodo ExecuteReader al que llamamos para leer la informacion

            while (readerSubCategoria.Read())
            {
                string guardarSubCategoria = readerSubCategoria["Nombre"].ToString();
                int idSubCategoria = readerSubCategoria.GetInt32(1);
                ComboBoxItem itemSubCategoria = new ComboBoxItem();//Esta instancia me permite llenar informacion
                itemSubCategoria.Content = guardarSubCategoria;
                itemSubCategoria.Tag = idSubCategoria;
                cmbCategoriaR.Items.Add(itemSubCategoria);
            }
            readerSubCategoria.Close();
            conn.Close();
        }
    }
}
