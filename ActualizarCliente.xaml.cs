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
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para ActualizarCliente.xaml
    /// </summary>
    public partial class ActualizarCliente : Window
    {
        SqlConnection conn;
        private int idCliente;
        public ActualizarCliente(int idCliente)
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            this.idCliente = idCliente;
            getGenero();
            getBarrio();
            getCanton();
            getProvincia();
            getPais();
        }

        private void btnActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrimerNombre.Text) ||
                string.IsNullOrWhiteSpace(txtSegundoNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrimerApellido.Text) ||
                string.IsNullOrWhiteSpace(txtSegundoApellido.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroCelular.Text) ||
                string.IsNullOrWhiteSpace(txtNacionalidad.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionDomiciliaria.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroCedula.Text) ||
                string.IsNullOrWhiteSpace(txtEstado.Text) ||
                cmbGenero.SelectedItem == null ||
                cmbBarrio.SelectedItem == null ||
                cmbCanton.SelectedItem == null ||
                cmbProvincia.SelectedItem == null ||
                cmbPais.SelectedItem == null)
            {
                MessageBox.Show("NINGUN CAMPO PUEDE IR VACÍO. POR FAVOR, COMPLETE TODOS LOS CAMPOS.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(txtPrimerNombre.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO LETRAS EN EL PRIMER NOMBRE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtSegundoNombre.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO LETRAS EN EL SEGUNDO NOMBRE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtPrimerApellido.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO LETRAS EN EL PRIMER APELLIDO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtSegundoApellido.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO LETRAS EN EL SEGUNDO APELLIDO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtNumeroCelular.Text, @"^\d{7,15}$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE LOS NÚMEROS CORRECTAMENTE EN EL CAMPO DE CELULAR.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtNacionalidad.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO LETRAS EN EL CAMPO DE NACIONALIDAD.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtCorreo.Text, @"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("POR FAVOR, INGRESE UN CORREO VALIDO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtDireccionDomiciliaria.Text, @"^(\d+[A-Za-z0-9\s\-]*)?(?:,\s)?([A-Za-z0-9\s\-]+)?(?:,\s)?([A-Za-z\s\-]+)?(?:,\s([A-Za-z\s\-]+))?(?:,\s(\d{5}(-\d{4})?))?$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("POR FAVOR, INGRESE UNA DIRECCION VALIDA.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtNumeroCedula.Text, @"^\d{6,11}(-\d{1,4})?$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO NÚMEROS EN EL CAMPO DE CÉDULA.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtEstado.Text, @"^(activado|desactivado)$", RegexOptions.IgnoreCase)) //VALIDACION AGREGADAAA
            {
                MessageBox.Show("POR FAVOR, ESCRIBA Activado O Desactivado EN EL ESTADO DE RECOLECTOR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ID GENERO
            ComboBoxItem idgenero = (ComboBoxItem)cmbGenero.SelectedValue;
            int idGenero = (int)idgenero.Tag;
            // ID BARRIO
            ComboBoxItem idbarrio = (ComboBoxItem)cmbBarrio.SelectedValue;
            int idBarrio = (int)idbarrio.Tag;
            // ID CANTON
            ComboBoxItem idcanton = (ComboBoxItem)cmbCanton.SelectedValue;
            int idCanton = (int)idcanton.Tag;
            // ID PROVINCIA
            ComboBoxItem idprovincia = (ComboBoxItem)cmbProvincia.SelectedValue;
            int idProvincia = (int)idprovincia.Tag;
            // ID PAIS
            ComboBoxItem idpais = (ComboBoxItem)cmbPais.SelectedValue;
            int idPais = (int)idpais.Tag;
       
                string actualizarcliente = "UPDATE Cliente set Primer_Nombre = @Nombre1, " +
                    "Segundo_Nombre = @Nombre2, Primer_Apellido = @Apellido1, " +
                    "Segundo_Apellido = @Apellido2, Celular = @Celular, Nacionalidad = @Nacionalidad, " +
                    "Correo = @Correo, Direccion = @Direccion, Cedula = @Cedula, Genero_id = @Genero, " +
                    "Barrio_id = @Barrio, Canton_id = @Canton, Provincia_id = @Provincia, " +
                    "Pais_id = @Pais, Estado = @Estado where id_Cliente = " + idCliente;
                SqlCommand commandCliente = new SqlCommand(actualizarcliente, conn);
                conn.Open();
                commandCliente.Parameters.AddWithValue("@Nombre1", txtPrimerNombre.Text);
                commandCliente.Parameters.AddWithValue("@Nombre2", txtSegundoNombre.Text);
                commandCliente.Parameters.AddWithValue("@Apellido1", txtPrimerApellido.Text);
                commandCliente.Parameters.AddWithValue("@Apellido2", txtSegundoApellido.Text);
                commandCliente.Parameters.AddWithValue("@Celular", txtNumeroCelular.Text);
                commandCliente.Parameters.AddWithValue("@Nacionalidad", txtNacionalidad.Text);
                commandCliente.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                commandCliente.Parameters.AddWithValue("@Direccion", txtDireccionDomiciliaria.Text);
                commandCliente.Parameters.AddWithValue("@Cedula", txtNumeroCedula.Text);
                commandCliente.Parameters.AddWithValue("@Genero", idGenero);
                commandCliente.Parameters.AddWithValue("@Barrio", idBarrio);
                commandCliente.Parameters.AddWithValue("@Canton", idCanton);
                commandCliente.Parameters.AddWithValue("@Provincia", idProvincia);
                commandCliente.Parameters.AddWithValue("@Pais", idPais);
            commandCliente.Parameters.AddWithValue("@Estado", txtEstado.Text);

            try //try ejecuta un codigo e intenta atrapar
                {
                    commandCliente.ExecuteNonQuery();
                    MessageBoxResult resultado = MessageBox.Show("SE ACTUALIZÓ LA INFORMACIÓN CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.OK)
                    {
                        //txtProvincia.Text =""
                        this.Close();
                    }
                }
                 catch (SqlException ex) //Me permite capturar excepciones al momento de ejecutar la aplicacion
                {
                     MessageBox.Show($"NO SE ACTUALIZO EL CLIENTE CORRECTAMENTE: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void getCanton()
        {
            string queryCanton = "SELECT Nombre, id_Canton FROM Canton";
            conn.Open();
            SqlCommand commandCanton = new SqlCommand(queryCanton, conn);
            SqlDataReader readerCanton = commandCanton.ExecuteReader();

            while (readerCanton.Read())
            {
                string guardarCanton = readerCanton["Nombre"].ToString();
                int idCanton = readerCanton.GetInt32(1);
                ComboBoxItem itemCanton = new ComboBoxItem();
                itemCanton.Content = guardarCanton;
                itemCanton.Tag = idCanton;
                cmbCanton.Items.Add(itemCanton);
            }
            readerCanton.Close();
            conn.Close();
        }

        private void getBarrio()
        {
            string queryBarrio = "SELECT Nombre, id_Barrio FROM Barrio";
            conn.Open();
            SqlCommand commandBarrio = new SqlCommand(queryBarrio, conn);
            SqlDataReader readerBarrio = commandBarrio.ExecuteReader();

            while (readerBarrio.Read())
            {
                string guardarBarrio = readerBarrio["Nombre"].ToString();
                int idBarrio = readerBarrio.GetInt32(1);
                ComboBoxItem itemBarrio = new ComboBoxItem();
                itemBarrio.Content = guardarBarrio;
                itemBarrio.Tag = idBarrio;
                cmbBarrio.Items.Add(itemBarrio);
            }
            readerBarrio.Close();
            conn.Close();
        }

        private void getGenero()
        {
            string queryGenero = "SELECT Nombre, id_Genero FROM Genero";
            conn.Open();
            SqlCommand commandGenero = new SqlCommand(queryGenero, conn);
            SqlDataReader readerGenero = commandGenero.ExecuteReader();

            while (readerGenero.Read())
            {
                string guardarGenero = readerGenero["Nombre"].ToString();
                int idGenero = readerGenero.GetInt32(1);
                ComboBoxItem itemGenero = new ComboBoxItem();
                itemGenero.Content = guardarGenero;
                itemGenero.Tag = idGenero;
                cmbGenero.Items.Add(itemGenero);
            }
            readerGenero.Close();
            conn.Close();
        }
    }
}
