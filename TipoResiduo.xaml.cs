using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para TipoResiduo.xaml
    /// </summary>
    public partial class TipoResiduo : Window
    {
        SqlConnection conn;
        public TipoResiduo()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            mostrarTipoR();
            getCategoria();
           
        }

        private void mostrarTipoR()
        {
            string queryrTipoR = "Select * from Tipo_Residuo";
            SqlDataAdapter adapterTipoR = new SqlDataAdapter(queryrTipoR, conn);
            using (adapterTipoR)
            {
                DataTable dataTipoR = new DataTable();
                adapterTipoR.Fill(dataTipoR);
                ltbTipoResiduo.DisplayMemberPath = "Nombre_Residuo";
                ltbTipoResiduo.SelectedValuePath = "id_TipoResiduo";
                ltbTipoResiduo.ItemsSource = dataTipoR.DefaultView;
            }
        }

        private void btnGuardarTipoResiduo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTipoResiduo.Text) ||
                cmbCategoriaR.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Regex.IsMatch(txtTipoResiduo.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                ComboBoxItem idcategoria = cmbCategoriaR.SelectedItem as ComboBoxItem;
                int idCategoria = (int)idcategoria.Tag;

                string GuardarTipoR = "INSERT INTO Tipo_Residuo (Nombre_Residuo, id_Sub_CategoriaR) values (@Nombre, @SubCategoria)";
                SqlCommand commaTipoR = new SqlCommand(GuardarTipoR, conn);
                conn.Open();
                commaTipoR.Parameters.AddWithValue("@Nombre", txtTipoResiduo.Text);
                commaTipoR.Parameters.AddWithValue("@SubCategoria", idCategoria);
                commaTipoR.ExecuteNonQuery();
                conn.Close();
                mostrarTipoR();
                MessageBoxResult resultado = MessageBox.Show("LA INFORMACIÓN SE GUARDO CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                txtTipoResiduo.Text = "";
                cmbCategoriaR.Text = "";
            }
            else
            {
                MessageBox.Show("ERROR, POR FAVOR INGRESE SOLO LETRAS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnActualizarTipoResiduo_Click(object sender, RoutedEventArgs e)
        {
            if (ltbTipoResiduo.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN RESIDUO PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; //RETORNA AL METODO Y NO SE SALE DE LA INTERFAZ
            }
            int idTR = (int)ltbTipoResiduo.SelectedValue;

            ActualizarResiduo ActualizarResiduo = new ActualizarResiduo((int)ltbTipoResiduo.SelectedValue);
            string Actualizarresiduo = "select * from Tipo_Residuo where id_TipoResiduo = " + idTR;
            SqlCommand commandResiduo = new SqlCommand(Actualizarresiduo, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandResiduo);
            using (adapter)
            {
                DataTable dataResiduo = new DataTable();
                adapter.Fill(dataResiduo);
                ActualizarResiduo.txtTipoResiduo.Text = dataResiduo.Rows[0]["Nombre_Residuo"].ToString();
            } 

            string querySubCategoria = "SELECT SUB.Nombre AS Sub_Categoria FROM Tipo_Residuo TR INNER JOIN Sub_CategoriaR SUB ON TR.id_Sub_CategoriaR = SUB.id_Sub_CategoriaR WHERE TR.id_TipoResiduo =" + idTR;

            conn.Open();

            SqlCommand commSubCategoria = new SqlCommand(querySubCategoria, conn);
            SqlDataReader readerSubCategoria = commSubCategoria.ExecuteReader();

            while (readerSubCategoria.Read())
            {
                string SubCategoria = readerSubCategoria["Sub_Categoria"].ToString();
                ActualizarResiduo.cmbCategoriaR.Text = SubCategoria;
            }
            readerSubCategoria.Close();

            conn.Close();
            ActualizarResiduo.ShowDialog();
            mostrarTipoR();
        }
    }
}
