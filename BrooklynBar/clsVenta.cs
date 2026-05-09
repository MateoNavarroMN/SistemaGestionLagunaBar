using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrooklynBar
{
    internal class clsVenta
    {
        private int idVenta;
        private int idPedido;
        private string dniEmpleado;
        private DateTime fecha;
        private decimal total;

        public int ID_Venta { get => idVenta; set => idVenta = value; }
        public int ID_Pedido { get => idPedido; set => idPedido = value; }
        public string DNI_Empleado { get => dniEmpleado; set => dniEmpleado = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public decimal Total { get => total; set => total = value; }
    }
}
