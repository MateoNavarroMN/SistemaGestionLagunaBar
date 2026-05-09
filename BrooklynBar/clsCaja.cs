using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrooklynBar
{
    internal class clsCaja
    {
        private int idCaja;
        private DateTime fechaHoraInicio;
        private DateTime fechaHoraCierre;
        private double montoInicial;
        private double montoFinal;

        public int ID_Caja { get => idCaja; set => idCaja = value; }
        public DateTime Fecha_Hora_Inicio { get => fechaHoraInicio; set => fechaHoraInicio = value; }
        public DateTime Fecha_Hora_Cierre { get => fechaHoraCierre; set => fechaHoraCierre = value; }
        public double Monto_Inicial { get => montoInicial; set => montoInicial = value; }
        public double Monto_Final { get => montoFinal; set => montoFinal = value; }
    }
}
