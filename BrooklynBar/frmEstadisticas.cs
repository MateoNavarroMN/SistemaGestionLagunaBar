using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrooklynBar
{
    public partial class frmEstadisticas : Form
    {
        public frmEstadisticas()
        {
            InitializeComponent();
        }

        private void frmEstadisticas_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Ventas";
            tabPage2.Text = "Menus";
            tabPage3.Text = "Empleados";

            conexionBD BD = new conexionBD();
            BD.GraficarVenta(chartVentasPorDia);
            BD.GraficarTopMenus(chartMenusPopulares);
            BD.GraficarVentasPorEmpleado(chartVentasPorEmpleado);
        }
    }
}
