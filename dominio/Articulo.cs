using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public double Precio { get; set; }

        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
    }
}
