using dominio;
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
    public partial class frmDetalle : Form
    {
        public frmDetalle()
        {
            InitializeComponent();
        }

        private Articulo articulo = null;
        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                if (articulo != null)
                {
                    lblId.Text = articulo.Id.ToString();
                    lblCodigo.Text = articulo.Codigo.ToString();
                    lblNombre.Text = articulo.Nombre;
                    lblDescripcion.Text = articulo.Descripcion;
                    linkLblUrl.Text = articulo.ImagenUrl;
                    lblPrecio.Text = articulo.Precio.ToString("$0");
                    lblCategoria.Text = articulo.Categoria.Descripcion;
                    lblMarca.Text = articulo.Marca.Descripcion;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void linkLblUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception)
            {
                MessageBox.Show("No podés abrir este link.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VisitLink()
        {
            // Cambia el color del link para marcarlo como visitado
            linkLblUrl.LinkVisited = true;
            //Abre el navegador predeterminado con la URL
            string url = articulo.ImagenUrl;
            System.Diagnostics.Process.Start(url);
        }
    }
}
