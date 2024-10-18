using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : Window
    {
        SqlConnection conn;
        public Cliente()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion);
            MostrarCliente();
            getGenero();
            getPais();
            getProvincia();
            getCanton();
            getBarrio();
        }
        private void MostrarCliente()
        {
            string queryrCliente = "Select * from Cliente";
            SqlDataAdapter adapterCliente = new SqlDataAdapter(queryrCliente, conn);
            using (adapterCliente)
            {
                DataTable dataCliente = new DataTable();
                adapterCliente.Fill(dataCliente);
                ltbCliente.DisplayMemberPath = "Primer_Nombre";
                ltbCliente.SelectedValuePath = "id_Cliente";
                ltbCliente.ItemsSource = dataCliente.DefaultView;
            }
        }

        private void btnGuardarCliente_Click(object sender, RoutedEventArgs e)
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

            string insertarCliente = "INSERT INTO Cliente (Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido," +
                "Celular, Nacionalidad, Correo, Direccion, Cedula, Genero_id," +
                "Barrio_id, Canton_id, Provincia_id, Pais_id, Estado) VALUES (@Nombre1, @Nombre2, @Apellido1, @Apellido2," +
                "@Celular, @Nacionalidad, @Correo, @Direccion, @Cedula, @Genero, @Barrio, @Canton, @Provincia, @Pais, @Estado)";

            using (SqlCommand commandCliente = new SqlCommand(insertarCliente, conn))
            {
                try
                {
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

                    // Ejecución del comando SQL
                    commandCliente.ExecuteNonQuery();
                    MessageBox.Show("SE GUARDÓ LA INFORMACIÓN CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                     
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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

        private void btnActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (ltbCliente.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN CLIENTE PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int idC = (int)ltbCliente.SelectedValue;

            ActualizarCliente actualizarcliente = new ActualizarCliente(idC);
            string actualizarCliente = "SELECT * FROM Cliente WHERE id_Cliente = " + idC;
            SqlCommand commandCliente = new SqlCommand(actualizarCliente, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandCliente); //Pasar informacion a otra ventana
            using (adapter)
            {
                DataTable dataCliente = new DataTable();
                adapter.Fill(dataCliente);
                actualizarcliente.txtPrimerNombre.Text = dataCliente.Rows[0]["Primer_Nombre"].ToString();
                actualizarcliente.txtSegundoNombre.Text = dataCliente.Rows[0]["Segundo_Nombre"].ToString();
                actualizarcliente.txtPrimerApellido.Text = dataCliente.Rows[0]["Primer_Apellido"].ToString();
                actualizarcliente.txtSegundoApellido.Text = dataCliente.Rows[0]["Segundo_Apellido"].ToString();
                actualizarcliente.txtNumeroCelular.Text = dataCliente.Rows[0]["Celular"].ToString();
                actualizarcliente.txtNacionalidad.Text = dataCliente.Rows[0]["Nacionalidad"].ToString();
                actualizarcliente.txtCorreo.Text = dataCliente.Rows[0]["Correo"].ToString();
                actualizarcliente.txtDireccionDomiciliaria.Text = dataCliente.Rows[0]["Direccion"].ToString();
                actualizarcliente.txtNumeroCedula.Text = dataCliente.Rows[0]["Cedula"].ToString();
                actualizarcliente.cmbGenero.SelectedValue = dataCliente.Rows[0]["Genero_id"].ToString();
                actualizarcliente.cmbBarrio.SelectedValue = dataCliente.Rows[0]["Barrio_id"].ToString();
                actualizarcliente.cmbCanton.SelectedValue = dataCliente.Rows[0]["Canton_id"].ToString();
                actualizarcliente.cmbProvincia.SelectedValue = dataCliente.Rows[0]["Provincia_id"].ToString();
                actualizarcliente.cmbPais.SelectedValue = dataCliente.Rows[0]["Pais_id"].ToString();
                actualizarcliente.txtEstado.Text = dataCliente.Rows[0]["Estado"].ToString();
            }

            string queryG = "SELECT G.Nombre AS Genero FROM Genero G INNER JOIN Cliente CT ON G.id_Genero = CT.Genero_id WHERE id_Cliente = " + idC;

            conn.Open();

            SqlCommand commG = new SqlCommand(queryG, conn);
            SqlDataReader readerG = commG.ExecuteReader();

            while (readerG.Read())
            {
                string Genero = readerG["Genero"].ToString();
                actualizarcliente.cmbGenero.Text = Genero;
            }
            readerG.Close();


            string queryBarrio = "SELECT B.Nombre AS Barrio FROM Barrio B INNER JOIN Cliente CT ON B.id_Barrio = CT.Barrio_id WHERE id_Cliente = " + idC;

            SqlCommand commBarrio = new SqlCommand(queryBarrio, conn);
            SqlDataReader readerBarrio = commBarrio.ExecuteReader();

            while (readerBarrio.Read())
            {
                string Barrio = readerBarrio["Barrio"].ToString();
                actualizarcliente.cmbBarrio.Text = Barrio;
            }
            readerBarrio.Close();


            string queryCanton = "SELECT C.Nombre AS Canton FROM Canton C INNER JOIN Cliente CT ON C.id_Canton = CT.Canton_id WHERE id_Cliente = " + idC;

            SqlCommand commCanton = new SqlCommand(queryCanton, conn);
            SqlDataReader readerCanton = commCanton.ExecuteReader();

            while (readerCanton.Read())
            {
                string Canton = readerCanton["Canton"].ToString();
                actualizarcliente.cmbCanton.Text = Canton;
            }
            readerCanton.Close();


            string queryP = "SELECT P.Nombre AS Pais FROM Cliente CT INNER JOIN Pais P ON CT.Pais_id = P.id_Pais WHERE CT.id_Cliente = " + idC;

            SqlCommand commP = new SqlCommand(queryP, conn);
            SqlDataReader readerP = commP.ExecuteReader();

            while (readerP.Read())
            {
                string Pais = readerP["Pais"].ToString();
                actualizarcliente.cmbPais.Text = Pais;
            }
            readerP.Close();


            string queryPV = "SELECT PV.Nombre AS Provincia FROM Cliente CT INNER JOIN Provincia PV ON CT.Provincia_id = PV.id_Provincia WHERE CT.id_Cliente = " + idC;

            SqlCommand commPV = new SqlCommand(queryPV, conn);
            SqlDataReader readerPV = commPV.ExecuteReader();

            while (readerPV.Read())
            {
                string Provincia = readerPV["Provincia"].ToString();
                actualizarcliente.cmbProvincia.Text = Provincia;
            }
            readerPV.Close();

            conn.Close();
            actualizarcliente.ShowDialog();
            MostrarCliente();
        }
    }
}

