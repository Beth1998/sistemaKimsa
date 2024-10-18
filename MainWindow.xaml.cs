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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration; //Hace una conexion al gestor de la base de datos
using System.Data.SqlClient; //Me permite ingresar informacion a mi base de datos
using System.Data; // Proporciona clases e interfaces para trabajar con datos en aplicaciones.  NET. Esto incluye cosas como conectarse a bases de datos, ejecutar comandos SQL y recuperar datos de fuentes de datos.

namespace SISTEMA_KINSA
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn; //Es una instancia en la que se utiliza la libreria Sistem.Data.SqlClient 
        public MainWindow()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["SISTEMA_KINSA.Properties.Settings.SISTEMA_KINSAConnectionString"].ConnectionString;
            conn = new SqlConnection(conexion); // En esta instancia ya pasa la informacion de mi gestor de datos

        }


        private void btnRecolector_Click(object sender, RoutedEventArgs e)
        {
            Recolector addR = new Recolector();
            addR.ShowDialog();
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente addC = new Cliente();
            addC.ShowDialog();
        }

        private void btnPais_Click(object sender, RoutedEventArgs e)
        {
            Pais addP = new Pais();
            addP.ShowDialog();
        }

        private void btnProvincia_Click(object sender, RoutedEventArgs e)
        {
            Provincia addProvincia = new Provincia();
            addProvincia.ShowDialog();
        }

        private void btnCanton_Click(object sender, RoutedEventArgs e)
        {
            Canton addCanton = new Canton();
            addCanton.ShowDialog();
        }

        private void btnBarrio_Click(object sender, RoutedEventArgs e)
        {
            Barrio addBarrio = new Barrio();
            addBarrio.ShowDialog();
        }

        private void btnGenero_Click(object sender, RoutedEventArgs e)
        {
            Genero addGenero = new Genero();
            addGenero.ShowDialog();
        }

        private void btnEstadoCivil_Click(object sender, RoutedEventArgs e)
        {
            Estado_civil addestado_Civil = new Estado_civil();
            addestado_Civil.ShowDialog();
        }

        private void btnTipoResiduo_Click(object sender, RoutedEventArgs e)
        {
             TipoResiduo addtipoResiduo = new TipoResiduo();
             addtipoResiduo.ShowDialog();
        }

        private void btnDetalles_Click(object sender, RoutedEventArgs e)
        {
            Detalles addDetalles = new Detalles();
            addDetalles.ShowDialog();
        }
    }
}
