using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrooklynBar
{
    internal class clsPedido
    {
        private int idPedido;
        private int idMesa;
        private DateTime fecha;
        private string estado;
        private string dniEmpleado;

        public int ID_Pedido { get => idPedido; set => idPedido = value; }
        public int ID_Mesa { get => idMesa; set => idMesa = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Estado { get => estado; set => estado = value; }
        public string DNI_Empleado { get => dniEmpleado; set => dniEmpleado = value; }
    }
}
