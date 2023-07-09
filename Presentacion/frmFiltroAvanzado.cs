using Negocio2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domino2;

namespace Presentacion
{
    public partial class frmFiltroAvanzado : Form
    {
        //Atributos
        private DataGridView grid;
        private List<Articulo> listaArticulo;
        //Constructor
        public frmFiltroAvanzado(DataGridView dgvArticulos,List<Articulo> listaArticulo)
        {
            InitializeComponent();
            grid = dgvArticulos;
            this.listaArticulo = listaArticulo;
        }
        //Eventos
        private void frmFiltroAvanzado_Load(object sender, EventArgs e)
        {
            cmbCampo.Items.Add("Código de articulo");
            cmbCampo.Items.Add("Nombre");
            cmbCampo.Items.Add("Marca");
            cmbCampo.Items.Add("Categoria");
            cmbCampo.Items.Add("Precio");
        }
        private void cmbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCriterio.Items.Clear();
            if (cmbCampo.SelectedItem.ToString() == "Precio")
            {
                cmbCriterio.Items.Add("Mayor a");
                cmbCriterio.Items.Add("Menor a");
                cmbCriterio.Items.Add("Igual a");
            }
            else
            {
                cmbCriterio.Items.Add("Empieze con");
                cmbCriterio.Items.Add("Termine con");
                cmbCriterio.Items.Add("Contiene");
            }
        }

        private void btnCAncelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string campo = cmbCampo.Text;
            string criterio = cmbCriterio.Text;
            string filtro = txtFiltroAvanzado.Text;
            Helper.cargarGrid(grid, ref listaArticulo,campo,criterio,filtro);
             //carga el grid pero no actuliza el valor de la lista de articulos del formulario principal
            
        }

        private void txtFiltroAvanzado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbCampo.SelectedItem.ToString() == "Precio")
            {
                if ((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
                    e.Handled = true;
                txtFiltroAvanzado.MaxLength = 38;
            }
        }
    }
}
