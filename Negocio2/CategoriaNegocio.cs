using AccesoDatos;
using Domino2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio2
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> categorias = new List<Categoria>();
            Datos dato = new Datos();

            try
            {

                dato.setearConsulta("select Id,Descripcion from Categorias");
                dato.ejecutarLectura();

                while (dato.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id = (int)dato.Lector["Id"];
                    aux.Descripcion = (string)dato.Lector["Descripcion"];
                    categorias.Add(aux);
                }
                return categorias;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dato.cerrarConexion();
            }
        }
    }
}
