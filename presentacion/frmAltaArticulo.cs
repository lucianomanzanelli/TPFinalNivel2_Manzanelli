using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class frmAltaArticulo : Form
    {

        private Articulo articulo = null;


        public frmAltaArticulo()
        {
            InitializeComponent();
            lblTitulo.Text = "Alta de Artículo";
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            lblTitulo.Text = "Modificar Artículo";
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo.ToString();
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;

                    txtUrl.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);

                    txtPrecio.Text = articulo.Precio.ToString();
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbArticulo.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }
        }

        private void txtUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrl.Text);
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (articulo == null)
                {
                    articulo = new Articulo();
                }

                if (!(txtCodigo.Text == ""))
                {
                    articulo.Codigo = txtCodigo.Text;
                    lblCodigoError.Text = "";
                }
                else
                    lblCodigoError.Text = "Debe ingresar un código";

                if (!(txtNombre.Text == ""))
                {
                    articulo.Nombre = txtNombre.Text;
                    lblNombreError.Text = "";
                }
                else
                    lblNombreError.Text = "Debe ingresar el nombre";

                
                articulo.Descripcion = txtDescripcion.Text;
                articulo.ImagenUrl = txtUrl.Text;

                bool resultado = false;
                if (!(txtPrecio.Text == ""))
                {
                    
                    decimal precio;
                    if (decimal.TryParse(txtPrecio.Text, out precio))
                    {
                        articulo.Precio = precio;
                        lblPrecioError.Text = "";
                        resultado = true;
                    }
                    else
                    {
                        lblPrecioError.Text = "El precio debe ser un número válido";
                        resultado = false;
                    }
                }
                else
                {
                    resultado = false;
                    lblPrecioError.Text = "Debe ingresar el precio";
                }


                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Marca = (Marca)cboMarca.SelectedItem;

                
                if (articulo.Id != 0)
                {
                    if (txtCodigo.Text == "" || txtNombre.Text == "" || resultado == false)
                    {
                        lblError.Text = "Debe completar los datos";
                    }
                    else
                    {
                        lblError.Text = "";

                        negocio.modificar(articulo);
                        MessageBox.Show("¡Modificado con exito!", "Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //una vez guardados los cambios cierra el formulario
                        Close();
                    }
                }
                else
                {
                    if (txtCodigo.Text == "" || txtNombre.Text == "" || resultado == false)
                    {
                        lblError.Text = "Debe completar los datos";
                    }
                    else
                    {
                        lblError.Text = "";

                        negocio.agregar(articulo);
                        MessageBox.Show("¡Agregado con exito!", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //una vez guardados los cambios cierra el formulario
                        Close();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        

        
    }
}
