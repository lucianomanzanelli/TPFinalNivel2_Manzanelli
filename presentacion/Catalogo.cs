using negocio;
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
    public partial class Catalogo : Form
    {
        public Catalogo()
        {
            InitializeComponent();
        }


        private void Catalogo_Load(object sender, EventArgs e)
        {
            cargar();
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


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
    }
}
