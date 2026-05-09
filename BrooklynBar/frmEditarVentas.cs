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
    public partial class frmEditarVentas : Form
    {
        public frmEditarVentas()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private BindingSource bsVentas = new BindingSource();

        private void frmEditarVentas_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
            dtpFechaFiltro.Value = DateTime.Now;
            RecargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsVenta nuevaVenta = new clsVenta()
            {
                ID_Pedido = Convert.ToInt32(nudIDPedido.Value),
                DNI_Empleado = Convert.ToString(cmbEmpleado.SelectedValue),
                Fecha = Convert.ToDateTime(dtpFecha.Value.ToString("yyyy-MM-dd")),
                Total = Convert.ToDecimal(nudTotal.Value)
            };

            conexionBD BD = new conexionBD();
            if (BD.AgregarVenta(nuevaVenta))
            {
                RecargarDatos();
                MessageBox.Show("Venta agregada correctamente.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsVenta editarVenta = new clsVenta()
            {
                ID_Venta = Convert.ToInt32(nudIDVenta.Value),
                ID_Pedido = Convert.ToInt32(nudIDPedido.Value),
                DNI_Empleado = Convert.ToString(cmbEmpleado.SelectedValue),
                Fecha = Convert.ToDateTime(dtpFecha.Value.ToString("yyyy-MM-dd")),
                Total = Convert.ToDecimal(nudTotal.Value)
            };

            conexionBD BD = new conexionBD();
            if (BD.EditarVenta(editarVenta))
            {
                RecargarDatos();
                MessageBox.Show("Venta editada correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarVenta(Convert.ToInt32(nudEliminarVenta.Value)))
            {
                RecargarDatos();
                MessageBox.Show("Venta eliminada correctamente.");
            }
        }

        private void dtpFechaFiltro_ValueChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            bsVentas.Filter = string.Empty;
        }

        private void CargarVentas()
        {
            conexionBD BD = new conexionBD();
            bsVentas.DataSource = BD.ObtenerVentas();
            dgvVenta.DataSource = bsVentas;
        }

        private void AplicarFiltro()
        {
            string filtro = "";

            DateTime fechaSeleccionada = dtpFechaFiltro.Value.Date;

            filtro = $"Fecha = '{fechaSeleccionada:yyyy-MM-dd}'";

            bsVentas.Filter = filtro;
        }

        public void CargarEmpleados()
        {
            conexionBD BD = new conexionBD();
            DataTable empleados = BD.ObtenerEmpleados();

            cmbEmpleado.DataSource = empleados;
            cmbEmpleado.DisplayMember = "Nombre_Apellido";
            cmbEmpleado.ValueMember = "DNI_Empleado";

            cmbEmpleado.SelectedIndex = -1;
        }

        private void RecargarDatos()
        {
            CargarVentas();
            CargarEmpleados();
        }

        private void AplicarEstiloGrilla()
        {
            dgvVenta.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvVenta.EnableHeadersVisualStyles = false;
            dgvVenta.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvVenta.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvVenta.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvVenta.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvVenta.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvVenta.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvVenta.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvVenta.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvVenta.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvVenta.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvVenta.BorderStyle = BorderStyle.None;
            dgvVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
