using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrooklynBar
{
    internal class clsMesa
    {
        private int idMesa;
        private string dniEmpleado;
        private string estado;

        public int ID_Mesa { get => idMesa; set => idMesa = value; }
        public string DNI_Empleado { get => dniEmpleado; set => dniEmpleado = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
