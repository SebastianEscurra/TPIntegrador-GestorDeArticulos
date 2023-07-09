using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio2;
using Domino2;


namespace Presentacion
{
    public partial class frmPrincipal : Form
    {
        //Atributos
        private ArticuloNegocio articuloNegocio;
        private List<Articulo> listaArticulos;
        //constructor
        public frmPrincipal()
        {
            InitializeComponent();
            articuloNegocio = new ArticuloNegocio();
        }
        //Eventos
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            Helper.cargarGrid(dgvArticulos,ref listaArticulos);
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmGestorDeElementos agregar = new frmGestorDeElementos();
            agregar.ShowDialog();
            Helper.cargarGrid(dgvArticulos,ref listaArticulos);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmGestorDeElementos modificar = new frmGestorDeElementos(seleccionado);
                modificar.ShowDialog();
            }
            else
                MessageBox.Show("No se selecciono un archivo para Modificar.");

            Helper.cargarGrid(dgvArticulos,ref listaArticulos);
            Helper.eliminarImagenSinReferencia();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                DialogResult resultado = MessageBox.Show("¿Eliminar el Articulo?","Eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) ;;
                if (resultado == DialogResult.Yes)
                {
                    articuloNegocio.eliminarFisico(seleccionado.Id);
                    Helper.cargarGrid(dgvArticulos,ref listaArticulos);
                    Helper.eliminarImagenSinReferencia();
                }
            }else
                MessageBox.Show("No se selecciono un archivo para Eliminar.");
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmDetalles detalles = new frmDetalles(seleccionado);
                detalles.ShowDialog();
            }
            else
                MessageBox.Show("No se selecciono un archivo para mostrar Detalles.");
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada=new List<Articulo>();
            string Filtro = txtFiltroRapido.Text;

           
            if (Filtro.Length > 1) 
                listaFiltrada= listaArticulos.FindAll(x => x.Codigo.ToUpper().Contains(Filtro.ToUpper()) || x.Nombre.ToUpper().Contains(Filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(Filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(Filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(Filtro.ToUpper()));
            else
                listaFiltrada = listaArticulos;
            
               
            dgvArticulos.DataSource = listaFiltrada;

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            frmFiltroAvanzado filtroAvanzado = new frmFiltroAvanzado(dgvArticulos,listaArticulos);
            filtroAvanzado.ShowDialog();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo actual = new Articulo();
            if (!(dgvArticulos.CurrentRow is null))
            {
                actual = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                Helper.cargarImagen(pbxArticulos, actual.UrlImagen);
            }
        }

        private void dgvArticulos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            Helper.colorearBoton(btnModificar, dgvArticulos);
            Helper.colorearBoton(btnEliminar,dgvArticulos);
            Helper.colorearBoton(btnDetalles,dgvArticulos);
        }
    }
}
