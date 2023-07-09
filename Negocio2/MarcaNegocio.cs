using AccesoDatos;
using Domino2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio2
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> Marcas = new List<Marca>();
            Datos dato = new Datos();

            try
            {

                dato.setearConsulta("select Id,Descripcion from Marcas");
                dato.ejecutarLectura();

                while (dato.Lector.Read())
                {
                    Marca aux = new Marca();

                    aux.Id =(int) dato.Lector["Id"];
                    aux.Descripcion = (string)dato.Lector["Descripcion"];
                    Marcas.Add(aux);
                }
                return Marcas;
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
