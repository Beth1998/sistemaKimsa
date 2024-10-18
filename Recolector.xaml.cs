using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Configuration; //Hace una conexion al gestor de la base de datos
using System.Data.SqlClient; //Me permite ingresar informacion a mi base de datos
using System.Data; // Proporciona clases e interfaces para trabajar con datos en aplicaciones.  NET. Esto incluye cosas como conectarse a bases de datos, ejecutar comandos SQL y recuperar datos de fuentes de datos.

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para Recolector.xaml
    /// </summary>
    public partial class Recolector : Window
    {
        SqlConnection conn; //Es una instancia en la que se utiliza la libreria Sistem.Data.SqlClient 
        public Recolector()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion); // En esta instancia ya pasa la informacion de mi gestor de datos
            MostrarRecolector();
            getPais();
            getProvincia();
            getCanton();
            getBarrio();
            getGenero();
            getEstadoC();
        }

        private void MostrarRecolector()
        {
            string queryRecolector = "SELECT * FROM Recolector"; //Consulta Sql
            SqlDataAdapter adapterRecolector = new SqlDataAdapter(queryRecolector, conn); //Pasamos la consulta a la conexion 

            using (adapterRecolector)
            {
                DataTable dataRecolector = new DataTable(); //Esta clase me permite representar la informacion en mi componente grafico
                adapterRecolector.Fill(dataRecolector);
                ltbRecolector.DisplayMemberPath = "Primer_Nombre";
                ltbRecolector.SelectedValuePath = "id_Recolector";
                ltbRecolector.ItemsSource = dataRecolector.DefaultView;
            }
        }

        private void btnGuardarRecolector_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrimerNombre.Text) ||
                string.IsNullOrWhiteSpace(txtSegundoNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrimerApellido.Text) ||
                string.IsNullOrWhiteSpace(txtSegundoApellido.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroCedula.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroCelularReco.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtTipoSangre.Text) ||
                string.IsNullOrWhiteSpace(txtCargasFamiliares.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(dtpckFechaNacimiento.Text) ||
                string.IsNullOrWhiteSpace(txtNombreReferencia.Text) ||
                string.IsNullOrWhiteSpace(txtCelularReferencia.Text) ||
                string.IsNullOrWhiteSpace(txtSalario.Text) ||
                string.IsNullOrWhiteSpace(dtpckFechaIngreso.Text) ||
                string.IsNullOrWhiteSpace(txtEstado.Text) ||
                string.IsNullOrWhiteSpace(txtEnfermedad.Text) ||
                string.IsNullOrWhiteSpace(txtDiscapacidad.Text) ||
                string.IsNullOrWhiteSpace(txtDescEnfermedad.Text) ||
                string.IsNullOrWhiteSpace(txtDescDiscapacidad.Text) ||
                cmbGenero.SelectedItem == null ||
                cmbBarrio.SelectedItem == null ||
                cmbCanton.SelectedItem == null ||
                cmbProvincia.SelectedItem == null ||
                cmbPais.SelectedItem == null ||
                cmbEstadoCivil.SelectedItem == null)
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
            if (!Regex.IsMatch(txtNumeroCedula.Text, @"^\d{6,11}(-\d{1,4})?$")) // NO VALIDA CON GUIONES
            {
                MessageBox.Show("POR FAVOR, INGRESE EL NÚMERO DE CÉDULA CORRECTAMENTE.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtNumeroCelularReco.Text, @"^\d{7,15}$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE LOS NÚMEROS CORRECTAMENTE EN EL CAMPO DE CELULAR.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtCorreo.Text, @"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("POR FAVOR, INGRESE UN CORREO VALIDO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtTipoSangre.Text, @"^(A|B|AB|O)[+-]?$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("POR FAVOR, INGRESE UN TIPO DE SANGRE VALIDO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtCargasFamiliares.Text, @"^\d+$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO NÚMEROS EN CARGAS FAMILIARES.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtDireccion.Text, @"^(\d+[A-Za-z0-9\s\-]*)?(?:,\s)?([A-Za-z0-9\s\-]+)?(?:,\s)?([A-Za-z\s\-]+)?(?:,\s([A-Za-z\s\-]+))?(?:,\s(\d{5}(-\d{4})?))?$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("POR FAVOR, INGRESE UNA DIRECCION VALIDA.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime fechaNacimiento = dtpckFechaNacimiento.SelectedDate.Value;
            if (fechaNacimiento > DateTime.Now)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UNA FECHA DE NACIMIENTO QUE NO SEA EN EL FUTURO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }
            if (edad < 18)
            {
                MessageBox.Show("DEBE TENER AL MENOS 18 AÑOS PARA PODER INGRESAR LA INFORMACIÓN", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Regex.IsMatch(txtNombreReferencia.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO LETRAS EN EL NOMBRE DE REFERENCIA.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtCelularReferencia.Text, @"^\d{7,15}$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE LOS NÚMEROS CORRECTAMENTE EN EL CAMPO DE CELULAR DE REFERENCIA FAMILIAR.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtSalario.Text, @"^[0-9,]+$"))
            {
                MessageBox.Show("POR FAVOR, INGRESE SOLO NÚMEROS EN EL CAMPO DE SALARIO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("EN CASO DE DECIMALES USE LA COMA ( , )", "NOTA", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (dtpckFechaIngreso.SelectedDate.HasValue && dtpckFechaIngreso.SelectedDate.Value > DateTime.Now)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UNA FECHA DE INGRESO QUE NO SEA EN EL FUTURO.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtEstado.Text, @"^(activado|desactivado)$", RegexOptions.IgnoreCase)) //VALIDACION AGREGADAAA
            {
                MessageBox.Show("POR FAVOR, ESCRIBA Activado O Desactivado EN EL ESTADO DE RECOLECTOR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtEnfermedad.Text, @"^(si|no)$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("POR FAVOR, ESCRIBA Si O No EN EL CAMPO DE ENFERMEDAD", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("EN EL CASO DE NO TENER ENFERMEDAD ESCRIBA NINGUNO EN SU DESCRIPCIÓN", "NOTA", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!Regex.IsMatch(txtDiscapacidad.Text, @"^(si|no)$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("POR FAVOR, ESCRIBA Si O No EN EL CAMPO DE DISCAPACIDAD", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("EN EL CASO DE NO TENER DISCAPACIDAD ESCRIBA NINGUNO EN SU DESCRIPCIÓN", "NOTA", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!Regex.IsMatch(txtDescEnfermedad.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                MessageBox.Show("POR FAVOR, DESCRIBA LA ENFERMEDAD", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(txtDescDiscapacidad.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]+$"))
            {
                MessageBox.Show("POR FAVOR, DESCRIBA LA DISCAPACIDAD", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
            // ID ESTADO CIVIL
            ComboBoxItem idestadoc = (ComboBoxItem)cmbEstadoCivil.SelectedValue;
            int idEstadoC = (int)idestadoc.Tag;

            DateTime Nacimiento = dtpckFechaNacimiento.SelectedDate.Value;
            DateTime Ingreso = dtpckFechaIngreso.SelectedDate.Value;

            string insertarRecolector = "INSERT INTO Recolector (Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido," +
            " Cedula, Celular, Correo, Tipo_Sangre, Cargas_Familiares, Direccion, Fecha_Nacimiento, Referencia_familiar, Referencia_Numero," +
            " Salario, Fecha_Ingreso, Estado, Descripcion_Enfermedades, Descripcion_Discapacidad, " +
            " Genero_id, Barrio_id, Canton_id, Provincia_id, Pais_id, EstadoCivil_id, Enfermedades, Discapacidad) VALUES (@Nombre1, @Nombre2, @Apellido1, @Apellido2," +
            " @Cedula, @Celular, @Correo, @TipoS, @CargasF, @Direccion, @FechaN, @Referenciaf, @ReferenciaN," +
            " @Salario, @FechaI, @Estado, @Descripcion_Enfermedades, @Descripcion_Discapacidad," +
            " @Genero, @Barrio, @Canton, @Provincia, @Pais, @EstadoC, @Enfermedades, @Discapacidad)";

            //using (conn) //conn es un variable que me permite hacer una conexion con mi base de datos
                //{     
                    using (SqlCommand commandRecolector = new SqlCommand(insertarRecolector, conn)) // Permite insertar informacion en una base de datos con parametros
                    {
                    conn.Open(); //Abre un punto de conexion para interactuar con la base de datos
                    commandRecolector.Parameters.AddWithValue("@Nombre1", txtPrimerNombre.Text); //Pasar los datos desde un componente grafico a mi base de datos
                    commandRecolector.Parameters.AddWithValue("@Nombre2", txtPrimerApellido.Text);
                    commandRecolector.Parameters.AddWithValue("@Apellido1", txtPrimerApellido.Text);
                    commandRecolector.Parameters.AddWithValue("@Apellido2", txtSegundoApellido.Text);
                    commandRecolector.Parameters.AddWithValue("@Cedula", txtNumeroCedula.Text);
                    commandRecolector.Parameters.AddWithValue("@Celular", txtNumeroCelularReco.Text);
                    commandRecolector.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    commandRecolector.Parameters.AddWithValue("@TipoS", txtTipoSangre.Text);
                    commandRecolector.Parameters.AddWithValue("@CargasF", txtCargasFamiliares.Text);
                    commandRecolector.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    commandRecolector.Parameters.AddWithValue("@FechaN", Nacimiento);
                    commandRecolector.Parameters.AddWithValue("@Referenciaf", txtNombreReferencia.Text);
                    commandRecolector.Parameters.AddWithValue("@ReferenciaN", txtCelularReferencia.Text);
                    commandRecolector.Parameters.AddWithValue("@Salario", double.Parse(txtSalario.Text));
                    commandRecolector.Parameters.AddWithValue("@FechaI", Ingreso);
                    commandRecolector.Parameters.AddWithValue("@Estado", txtEstado.Text);
                    commandRecolector.Parameters.AddWithValue("@Descripcion_Enfermedades", txtDescEnfermedad.Text);
                    commandRecolector.Parameters.AddWithValue("@Descripcion_Discapacidad", txtDescEnfermedad.Text);
                    commandRecolector.Parameters.AddWithValue("@Enfermedades", txtEnfermedad.Text);
                    commandRecolector.Parameters.AddWithValue("@Discapacidad", txtDiscapacidad.Text);
                    commandRecolector.Parameters.AddWithValue("@Genero", idGenero);
                    commandRecolector.Parameters.AddWithValue("@Barrio", idBarrio);
                    commandRecolector.Parameters.AddWithValue("@Canton", idCanton);
                    commandRecolector.Parameters.AddWithValue("@Provincia", idProvincia);
                    commandRecolector.Parameters.AddWithValue("@Pais", idPais);
                    commandRecolector.Parameters.AddWithValue("@EstadoC", idEstadoC);
                    
                    try //try ejecuta un codigo e intenta atrapar
                    {
                        commandRecolector.ExecuteNonQuery();
                        MessageBoxResult resultado = MessageBox.Show("SE GUARDÓ LA INFORMACIÓN CORRECTAMENTE", "ÉXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (resultado == MessageBoxResult.OK)
                        {
                            this.Close();
                        }
                    }
                       catch (SqlException ex) //Me permite capturar excepciones al momento de ejecutar la aplicacion
                       {
                        MessageBox.Show($"LA INFORMACIÓN NO SE HA GUARDADO CORRECTAMENTE: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                       }
                        conn.Close();
            }
                //}
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

        private void getEstadoC()
        {
            string queryEstadoC = "SELECT Nombre, id_EstadoCivil FROM Estado_Civil";
            conn.Open();
            SqlCommand commandEstadoC = new SqlCommand(queryEstadoC, conn);
            SqlDataReader readerEstadoC = commandEstadoC.ExecuteReader();

            while (readerEstadoC.Read())
            {
                string guardarEstadoC = readerEstadoC["Nombre"].ToString();
                int idEstadoC = readerEstadoC.GetInt32(1);
                ComboBoxItem itemEstadoC = new ComboBoxItem();
                itemEstadoC.Content = guardarEstadoC;
                itemEstadoC.Tag = idEstadoC;
                cmbEstadoCivil.Items.Add(itemEstadoC);
            }
            readerEstadoC.Close();
            conn.Close();
        }

        private void btnActualizarRecolector_Click(object sender, RoutedEventArgs e)
        {
            if (ltbRecolector.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE UN RECOLECTOR PARA ACTUALIZAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int idR = (int)ltbRecolector.SelectedValue;

            ActualizarRecolector actualizarrecolector = new ActualizarRecolector(idR);
            string actualizarRecolector = "SELECT * FROM Recolector WHERE id_Recolector = " + idR;
            SqlCommand commandRecolector = new SqlCommand(actualizarRecolector, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(commandRecolector); //Pasar informacion a otra ventana
            using (adapter)
            {
                //commandRecolector.Parameters.AddWithValue("@idRecolector", ltbRecolector.SelectedValue);
                DataTable dataRecolector = new DataTable();
                adapter.Fill(dataRecolector);
                actualizarrecolector.txtPrimerNombre.Text = dataRecolector.Rows[0]["Primer_Nombre"].ToString();
                actualizarrecolector.txtSegundoNombre.Text = dataRecolector.Rows[0]["Segundo_Nombre"].ToString();
                actualizarrecolector.txtPrimerApellido.Text = dataRecolector.Rows[0]["Primer_Apellido"].ToString();
                actualizarrecolector.txtSegundoApellido.Text = dataRecolector.Rows[0]["Segundo_Apellido"].ToString();
                actualizarrecolector.txtNumeroCedula.Text = dataRecolector.Rows[0]["Cedula"].ToString();
                actualizarrecolector.txtNumeroCelularReco.Text = dataRecolector.Rows[0]["Celular"].ToString();
                actualizarrecolector.txtCorreo.Text = dataRecolector.Rows[0]["Correo"].ToString();
                actualizarrecolector.txtTipoSangre.Text = dataRecolector.Rows[0]["Tipo_Sangre"].ToString();
                actualizarrecolector.txtCargasFamiliares.Text = dataRecolector.Rows[0]["Cargas_Familiares"].ToString();
                actualizarrecolector.txtDireccion.Text = dataRecolector.Rows[0]["Direccion"].ToString();
                actualizarrecolector.txtNombreReferencia.Text = dataRecolector.Rows[0]["Referencia_familiar"].ToString();
                actualizarrecolector.txtCelularReferencia.Text = dataRecolector.Rows[0]["Referencia_Numero"].ToString();
                actualizarrecolector.txtSalario.Text = dataRecolector.Rows[0]["Salario"].ToString();
                actualizarrecolector.txtEnfermedad.Text = dataRecolector.Rows[0]["Enfermedades"].ToString();              
                actualizarrecolector.txtDiscapacidad.Text = dataRecolector.Rows[0]["Discapacidad"].ToString();
                actualizarrecolector.txtDescEnfermedad.Text = dataRecolector.Rows[0]["Descripcion_Enfermedades"].ToString();
                actualizarrecolector.txtDescDiscapacidad.Text = dataRecolector.Rows[0]["Descripcion_Discapacidad"].ToString();
                actualizarrecolector.txtEstado.Text = dataRecolector.Rows[0]["Estado"].ToString();
                actualizarrecolector.cmbGenero.SelectedValue = dataRecolector.Rows[0]["Genero_id"].ToString(); 
                actualizarrecolector.cmbEstadoCivil.SelectedValue = dataRecolector.Rows[0]["EstadoCivil_id"].ToString(); 
                actualizarrecolector.cmbBarrio.SelectedValue = dataRecolector.Rows[0]["Barrio_id"].ToString(); 
                actualizarrecolector.cmbCanton.SelectedValue = dataRecolector.Rows[0]["Canton_id"].ToString(); 
                actualizarrecolector.cmbProvincia.SelectedValue = dataRecolector.Rows[0]["Provincia_id"].ToString();
                actualizarrecolector.cmbPais.SelectedValue = dataRecolector.Rows[0]["Pais_id"].ToString();
                actualizarrecolector.dtpckFechaNacimiento.SelectedDate = Convert.ToDateTime(dataRecolector.Rows[0]["Fecha_Nacimiento"]);
                actualizarrecolector.dtpckFechaIngreso.SelectedDate = Convert.ToDateTime(dataRecolector.Rows[0]["Fecha_Ingreso"]);
            }

            string queryG = "SELECT G.Nombre AS Genero FROM Genero G INNER JOIN Recolector R ON G.id_Genero = R.Genero_id WHERE id_Recolector = " + idR;

            conn.Open();

            SqlCommand commG = new SqlCommand(queryG, conn);
            SqlDataReader readerG = commG.ExecuteReader();

            while (readerG.Read())
            {
                string Genero = readerG["Genero"].ToString();
                actualizarrecolector.cmbGenero.Text = Genero;
            }
            readerG.Close();


            string queryEC = "SELECT EC.Nombre AS EstadoC FROM Estado_Civil EC INNER JOIN Recolector R ON EC.id_EstadoCivil = R.EstadoCivil_id WHERE id_Recolector = " + idR;

            SqlCommand commEC = new SqlCommand(queryEC, conn);
            SqlDataReader readerEC = commEC.ExecuteReader();

            while (readerEC.Read())
            {
                string EC = readerEC["EstadoC"].ToString();
                actualizarrecolector.cmbEstadoCivil.Text = EC;
            }
            readerEC.Close();


            string queryBarrio = "SELECT B.Nombre AS Barrio FROM Barrio B INNER JOIN Recolector R ON B.id_Barrio = R.Barrio_id WHERE id_Recolector = " + idR;

            SqlCommand commBarrio = new SqlCommand(queryBarrio, conn);
            SqlDataReader readerBarrio = commBarrio.ExecuteReader();

            while (readerBarrio.Read())
            {
                string Barrio = readerBarrio["Barrio"].ToString();
                actualizarrecolector.cmbBarrio.Text = Barrio;
            }
            readerBarrio.Close();


            string queryCanton = "SELECT C.Nombre AS Canton FROM Canton C INNER JOIN Recolector R ON C.id_Canton = R.Canton_id WHERE id_Recolector = " + idR;

            SqlCommand commCanton = new SqlCommand(queryCanton, conn);
            SqlDataReader readerCanton = commCanton.ExecuteReader();

            while (readerCanton.Read())
            {
                string Canton = readerCanton["Canton"].ToString();
                actualizarrecolector.cmbCanton.Text = Canton;
            }
            readerCanton.Close();
            string queryP = "SELECT P.Nombre AS Pais FROM Pais P INNER JOIN Recolector R ON R.Pais_id = P.id_Pais WHERE R.id_Recolector = " + idR;

            SqlCommand commP = new SqlCommand(queryP, conn);
            SqlDataReader readerP = commP.ExecuteReader();

            while (readerP.Read())
            {
                string Pais = readerP["Pais"].ToString();
                actualizarrecolector.cmbPais.Text = Pais;
            }
            readerP.Close();


            string queryPV = "SELECT P.Nombre AS Provincia FROM Recolector R INNER JOIN Provincia P ON R.Provincia_id = P.id_Provincia WHERE R.id_Recolector = " +idR;

            SqlCommand commPV = new SqlCommand(queryPV, conn);
            SqlDataReader readerPV = commPV.ExecuteReader();

            while (readerPV.Read())
            {
                string Provincia = readerPV["Provincia"].ToString();
                actualizarrecolector.cmbProvincia.Text = Provincia;
            }
            readerPV.Close();


            conn.Close();

            actualizarrecolector.ShowDialog();
            MostrarRecolector();
        }

        private void btnPago_Click(object sender, RoutedEventArgs e)
        {
            if (ltbRecolector.SelectedItem == null)
            {
                MessageBox.Show("POR FAVOR, SELECCIONE EL RECOLECTOR A PAGAR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int idRecolector = (int)ltbRecolector.SelectedValue;
            FormaPago Formapago = new FormaPago(idRecolector);
            Formapago.ShowDialog();
        }
    }
}

