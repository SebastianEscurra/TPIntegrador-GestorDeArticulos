using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domino2
{
    public class Articulo
    {
        public int Id { get; set; }
        [DisplayName("Código de articulo")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }    
        public string UrlImagen { get; set; }
        public decimal Precio { get; set; }
    }
}
