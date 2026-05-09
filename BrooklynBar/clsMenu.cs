using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrooklynBar
{
    internal class clsMenu
    {
        private int idMenu;
        private int idCategoria;
        private string nombre;
        private decimal precio;
        private string descripcion;

        public int ID_Menu { get => idMenu; set => idMenu = value; }
        public int ID_Categoria { get => idCategoria; set => idCategoria = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
