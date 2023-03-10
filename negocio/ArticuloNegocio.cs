using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select a.Id, a.Codigo, a.Nombre, a.Descripcion, a.ImagenUrl, a.Precio, c.Descripcion categoria, a.IdCategoria, a.IdMarca, m.Descripcion marca from ARTICULOS A, CATEGORIAS C, MARCAS M where a.IdCategoria = c.Id and a.IdMarca = m.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = Convert.ToString(datos.Lector["codigo"]);
                    aux.Nombre = Convert.ToString(datos.Lector["nombre"]);
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca(); 
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);

                }

                datos.cerrarConexion();
                return lista;

            }
            catch (Exception)
            {

                throw;
            }



        }



    }
}
