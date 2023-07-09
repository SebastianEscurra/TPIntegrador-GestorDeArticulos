using Domino2;
using Negocio2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Presentacion
{
    internal static class Helper
    {
        private static ArticuloNegocio negocio;
       
        static Helper()
        {
            negocio = new ArticuloNegocio();
        }
        public static void cargarGrid(DataGridView grid, ref List<Articulo> listaArticulos)
        {
            listaArticulos = negocio.listar();
            grid.DataSource = listaArticulos;
            grid.Columns["Id"].Visible = false;
            grid.Columns["UrlImagen"].Visible = false;
        }
        public static void cargarGrid(DataGridView grid, ref List<Articulo> listaArticulos, string campo, string criterio, string filtro)// Carga con el filtro
        {
            listaArticulos = negocio.filtrar(campo, criterio, filtro);
            grid.DataSource = listaArticulos;
            grid.Columns["Id"].Visible = false;
            grid.Columns["UrlImagen"].Visible = false;
        }
        public static void cargarImagen(PictureBox pictureBox, string direccion)
        {
            try
            {
                pictureBox.Load(direccion);
            }
            catch (Exception)
            {
                pictureBox.Load("https://www.puroverso.com.uy/images/virtuemart/product/9788430531325.jpg");
            }
        }
        public static void eliminarImagenSinReferencia()
        {
            string direccion = ConfigurationManager.AppSettings["gestionArticulos-app"];
            bool borrar;
            foreach (var imagenLocal in Directory.GetFiles(direccion))
            {
                borrar = true;
                foreach (var articulo in negocio.listar())
                {
                    if (!articulo.UrlImagen.ToUpper().Contains("HTTP") && articulo.UrlImagen == imagenLocal)
                        borrar = false;
                }

                if (borrar)
                    File.Delete(imagenLocal);
            }
        }
        public static void colorearBoton(Button btn,ComboBox cmbCategoria,ComboBox cmbMarca,TextBox txtPrecio)
        {
            if (cmbCategoria.SelectedIndex >= 0 && cmbMarca.SelectedIndex >= 0 && txtPrecio.Text != "")
            {
                btn.BackColor = Color.FromArgb(182, 218, 245);
                btn.FlatStyle = FlatStyle.Popup;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Black;

            }
            else
            {
                btn.BackColor =  Color.FromArgb(87, 104, 117);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SystemColors.Control;
            }
        }
        public static void colorearBoton(Button btn,DataGridView grid)
        {
            if(grid.CurrentRow!=null)
            {
                btn.BackColor = Color.Silver;
                btn.FlatStyle = FlatStyle.Popup;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.DimGray;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SystemColors.Control;
            }
        }

    }
}
