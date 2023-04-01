using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace presentacion
{
    public partial class Catalogo : Form
    {
        public Catalogo()
        {
            InitializeComponent();
        }


        private void Catalogo_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoría");

        }

        private List<Articulo> listaArticulo;
        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;
                pbArticulo.Load(listaArticulo[0].ImagenUrl);
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "N2";


                ocultarColumnas();
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

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Marca"].Visible = false;

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();

            /* luego de confirmar el alta de un nuevo articulo
            actualizo nuevamente la grilla para ver el nuevo articulo */
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();

            cargar();

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmDetalle detalle = new frmDetalle(seleccionado);
            detalle.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;

            try
            {
                if (MessageBox.Show("¿Seguro que quieres eliminar este artículo?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                    negocio.eliminar(seleccionado.Id);

                    MessageBox.Show("¡Eliminado con exito!", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargar();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltroRapido.Text;

            listaFiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper())
                            || x.Descripcion.ToUpper().Contains(filtro.ToUpper())
                            || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper())
                            || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;

            ocultarColumnas();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCampo.SelectedItem != null)
            {
                string opcion = cboCampo.SelectedItem.ToString();

                if (opcion != "")
                {
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Comienza con");
                    cboCriterio.Items.Add("Termina con");
                    cboCriterio.Items.Add("Contiene");
                }
            }
        }

        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                lblFiltroError.Text = "Seleccione el campo para filtrar";
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                lblFiltroError.Text = "Seleccione el criterio para filtrar";
                return true;
            }
            else
                lblFiltroError.Text = "";

            return false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (validarFiltro())
                {
                    return;
                }
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboCriterio.Items.Clear();
            cboCampo.SelectedIndex = -1;
            txtFiltroAvanzado.Clear();
            txtFiltroRapido.Clear();

            cargar();
        }

        
    }
}
