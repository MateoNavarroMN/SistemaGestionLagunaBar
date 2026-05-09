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
    public partial class frmVenta : Form
    {
        public frmVenta()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private BindingSource bsVentas = new BindingSource();

        private void frmVenta_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            CargarEmpleados();
            CargarVentas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            cmbEmpleado.SelectedIndex = -1;
            bsVentas.Filter = string.Empty;
        }

        private void btnVistaPreviaTicket_Click(object sender, EventArgs e)
        {
            if (dgvVentas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná una venta para ver el ticket.");
                return;
            }

            var row = dgvVentas.SelectedRows[0];
            string texto = GenerarTextoTicket(row);

            frmVistaPreviaTicket vista = new frmVistaPreviaTicket();
            vista.MostrarTexto(texto);
            vista.ShowDialog();
        }

        public void CargarVentas()
        {
            conexionBD BD = new conexionBD();
            bsVentas.DataSource = BD.ObtenerVentas2(); 
            dgvVentas.DataSource = bsVentas; 
        }

        public void CargarEmpleados()
        {
            conexionBD BD = new conexionBD();
            DataTable empleados = BD.ObtenerEmpleados();

            cmbEmpleado.DataSource = empleados;
            cmbEmpleado.DisplayMember = "Nombre_Apellido";
            cmbEmpleado.ValueMember = "Nombre_Apellido";

            cmbEmpleado.SelectedIndex = -1;
        }

        private void AplicarFiltro()
        {
            string filtro = "";
            string empleado = cmbEmpleado.SelectedIndex != -1 ? cmbEmpleado.SelectedValue.ToString() : "";
            string desde = dtpDesde.Value.ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(empleado))
            {
                filtro += $"Empleado = '{empleado}'";
            }

            if (!string.IsNullOrEmpty(desde))
            {
                filtro += (filtro.Length > 0 ? " AND " : "") + $"Fecha >= '{desde}'";
            }

            bsVentas.Filter = filtro;
        }

        public string GenerarTextoTicket(DataGridViewRow row)
        {
            conexionBD BD = new conexionBD();
            int idPedido = Convert.ToInt32(row.Cells["Pedido"].Value);
            DataTable detalles = BD.ObtenerDetallePedido(idPedido);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("      LA LAGUNA BAR");
            sb.AppendLine("--------------------------");
            sb.AppendLine($"Fecha: {row.Cells["Fecha"].Value}");
            sb.AppendLine($"Empleado: {row.Cells["Empleado"].Value}");
            sb.AppendLine("\nDetalles del Pedido:");
            sb.AppendLine("Producto              Cant.      Precio");

            foreach (DataRow d in detalles.Rows)
            {
                string producto = d["Nombre"].ToString().PadRight(20);
                string cantidad = d["Cantidad"].ToString().PadLeft(4);
                string precio = $"\t\t${Convert.ToDecimal(d["Precio_Unitario"]):0.00}".PadLeft(8);

                sb.AppendLine($"{producto}{cantidad}{precio}");
            }

            sb.AppendLine($"");
            sb.AppendLine($"Total: ${row.Cells["Total"].Value}");

            sb.AppendLine("\nGracias por su visita 🍻");
            return sb.ToString();
        }

        private void AplicarEstiloGrilla()
        {
            // Estilo general
            dgvVentas.EnableHeadersVisualStyles = false;
            dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvVentas.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvVentas.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvVentas.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvVentas.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvVentas.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvVentas.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvVentas.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvVentas.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvVentas.BorderStyle = BorderStyle.None;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
