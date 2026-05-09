using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrooklynBar
{
    internal class clsEmpleado
    {
        private string dniEmpleado;
        private string nombreApellido;
        private string sexo;
        private string telefono;
        private string direccion;
        private int idBarrio;
        private string contactoEmergencia;
        private int idNivel;
        private string contrasena;

        public string DNI_Empleado { get => dniEmpleado; set => dniEmpleado = value; }
        public string Nombre_Apellido { get => nombreApellido; set => nombreApellido = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int ID_Barrio { get => idBarrio; set => idBarrio = value; }
        public string Contacto_Emergencia { get => contactoEmergencia; set => contactoEmergencia = value; }
        public int ID_Nivel { get => idNivel; set => idNivel = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }
    }
}
