using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrooklynBar
{
    internal class clsDetallePedido
    {
        private int idDetalle;
        private int idMenu;
        private int idPedido;
        private int cantidad;
        private decimal precioUnitario;

        public int ID_Detalle { get => idDetalle; set => idDetalle = value; }
        public int ID_Menu { get => idMenu; set => idMenu = value; }
        public int ID_Pedido { get => idPedido; set => idPedido = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal Precio_Unitario { get => precioUnitario; set => precioUnitario = value; }
    }
}
