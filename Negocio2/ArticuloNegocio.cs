using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Domino2;

namespace Negocio2
{
    
    public class ArticuloNegocio
    {
        private Datos datos;
        private string consultaLectura = "select a.Id,a.Codigo,a.Nombre,a.Descripcion,m.Descripcion Marca,m.Id IdMarca,c.Descripcion Categoria,c.Id IdCategoria,a.ImagenUrl,a.Precio from ARTICULOS a,MARCAS m,CATEGORIAS c where a.IdMarca=m.Id and a.IdCategoria= c.Id";
        public ArticuloNegocio()
        {
            datos = new Datos();
        }
        public List<Articulo> listar()
        {
            List<Articulo> articulos = new List<Articulo>();

            try
            {
                datos.setearConsulta(consultaLectura);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    articulos.Add(aux);
                }

                return articulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Articulo articuloNuevo)
        {
            try
            {
                datos.setearConsulta("insert into ARTICULOS values (@codigo,@nombre,@descripcion,@idMarca,@idcategoria,@url,@precio)");

                datos.setearParametros("@codigo", articuloNuevo.Codigo);
                datos.setearParametros("@nombre", articuloNuevo.Nombre);
                datos.setearParametros("@descripcion", articuloNuevo.Descripcion);
                datos.setearParametros("@idMarca", articuloNuevo.Marca.Id);
                datos.setearParametros("@idCategoria", articuloNuevo.Categoria.Id);
                datos.setearParametros("@url", articuloNuevo.UrlImagen);
                datos.setearParametros("@precio", articuloNuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void modificar(Articulo articuloModificado)
        {
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo=@codigo,Nombre=@nombre,Descripcion=@descripcion,IdMarca=@idMarca,IdCategoria=@idCategoria,ImagenUrl=@url,Precio=@precio where Id=@id");

                datos.setearParametros("@codigo", articuloModificado.Codigo);
                datos.setearParametros("@nombre", articuloModificado.Nombre);
                datos.setearParametros("@descripcion", articuloModificado.Descripcion);
                datos.setearParametros("@idMarca", articuloModificado.Marca.Id);
                datos.setearParametros("@idCategoria", articuloModificado.Categoria.Id);
                datos.setearParametros("@url", articuloModificado.UrlImagen);
                datos.setearParametros("@precio", articuloModificado.Precio);
                datos.setearParametros("@id", articuloModificado.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarFisico(int id)
        {
            //maxi me daba error de que cada consulta debe tener nombre diferente,
            //por eso concatene el id que siempre es diferente, el la practica de pokemon no me paso esa exception.
            try
            {
                datos.setearConsulta("delete ARTICULOS where Id=@id" + id.ToString()); 

                datos.setearParametros("@id" + id.ToString(), id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            try
            {
                string consultaFiltrar = consultaLectura + " and ";

                switch (campo)
                {
                    case "Código de articulo":
                        switch (criterio)
                        {
                            case "Empiece con":
                                consultaFiltrar += "Codigo LIKE '" + filtro + "%'";
                                break;
                            case "Termine con":
                                consultaFiltrar += "Codigo LIKE '%" + filtro + "'";
                                break;
                            default:
                                consultaFiltrar += "Codigo LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Nombre":
                        switch (criterio)
                        {
                            case "Empiece con":
                                consultaFiltrar += "Nombre LIKE '" + filtro + "%'";
                                break;
                            case "Termine con":
                                consultaFiltrar += "Nombre LIKE '%" + filtro + "'";
                                break;
                            default:
                                consultaFiltrar += "Nombre LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Marca":
                        switch (criterio)
                        {
                            case "Empiece con":
                                consultaFiltrar += "m.Descripcion LIKE '" + filtro + "%'";
                                break;
                            case "Termine con":
                                consultaFiltrar += "m.Descripcion LIKE '%" + filtro + "'";
                                break;
                            default:
                                consultaFiltrar += "m.Descripcion LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Categoria":
                        switch (criterio)
                        {
                            case "Empiece con":
                                consultaFiltrar += "c.Descripcion LIKE '" + filtro + "%'";
                                break;
                            case "Termine con":
                                consultaFiltrar += "c.Descripcion LIKE '%" + filtro + "'";
                                break;
                            default:
                                consultaFiltrar += "c.Descripcion LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;

                    default:
                        switch (criterio)
                        {
                            case "Mayor a":
                                consultaFiltrar += "Precio > " + filtro;
                                break;
                            case "Menor a":
                                consultaFiltrar += "Precio < " + filtro;

                                break;
                            default:
                                consultaFiltrar += "Precio = " + filtro;
                                break;
                        }
                        break;
                }
                datos.setearConsulta(consultaFiltrar);
                datos.ejecutarLectura();

                List<Articulo> articulosFiltrados = new List<Articulo>();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    articulosFiltrados.Add(aux);
                }
                return articulosFiltrados;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
