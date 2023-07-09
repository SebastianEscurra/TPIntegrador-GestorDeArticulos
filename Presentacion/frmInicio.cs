using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Presentacion
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
            lblDescripcion.Text = "@GestionDeArticulos es una app diseñada para gestionar\r\n " +
                                  "los articulos de tu comercio, a tu gusto.\r\n \r\n" +
                                   "                       En ella podras:\r\n" +
                                   "-Ver el listado de todos tus articulos.\r\n" +
                                   "-Fitrar busquedas por campos y criterios.\r\n" +
                                   "-Agregar, modificar o eliminar cada articulo.\r\n" +
                                   "-Visualizar cada uno detalladamente.\r\n" +
                                   "\r\n¡Gestiona tu comenrcio de la mano de esta app!";
        }
        //Eventos
        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            frmPrincipal principal = new frmPrincipal();
            this.Visible = false;
            crearCarpetaLocalParaImagenes();
            principal.ShowDialog();
            this.Close();
        }
        //Metodos
        private void crearCarpetaLocalParaImagenes() 
        {
            string direccionDeCarpeta = "C:\\gestionArticulos-app";// El problema podria ser que no tenga disco con nombre C....

            if (!Directory.Exists(direccionDeCarpeta))
            {
                Directory.CreateDirectory(direccionDeCarpeta);
                MessageBox.Show("carpeta para guardar archivos locales creada en " + direccionDeCarpeta);
            }
        }
    }
}
