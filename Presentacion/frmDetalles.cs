using Domino2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmDetalles : Form
    {
        private Articulo seleccionado;
        public frmDetalles(Articulo seleccionado)
        {
            InitializeComponent();
            this.seleccionado = seleccionado;
        }

        private void frmDetalles_Load(object sender, EventArgs e)
        {
            lblNombre.Text = seleccionado.Nombre;
            lblCodigo.Text += seleccionado.Codigo;
            Helper.cargarImagen(pbxArticulo,seleccionado.UrlImagen);
            lblPrecio.Text = "$"+ decimal.Parse(seleccionado.Precio.ToString("0.00"));
            lblCategoria.Text += seleccionado.Categoria.Descripcion;
            lblMarca.Text += seleccionado.Marca.Descripcion;
            lblDescripcion.Text += seleccionado.Descripcion;
        }
    }
}
