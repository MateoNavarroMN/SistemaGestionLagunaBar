using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrooklynBar
{
    internal class clsCategoriaMenu
    {
        private int idCategoria;
        private string nombre;

        public int ID_Categoria { get => idCategoria; set => idCategoria = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
