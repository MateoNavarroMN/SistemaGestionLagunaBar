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
    public partial class frmEditarPedidos : Form
    {
        public frmEditarPedidos()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private BindingSource bsPedidos = new BindingSource();

        private void frmEditarPedidos_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
            dtpFechaFiltro.Value = DateTime.Now;
            RecargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsPedido nuevoPedido = new clsPedido()
            {
                ID_Mesa = Convert.ToInt32(cmbIdMesa.SelectedValue),
                Fecha = dtpFecha.Value,
                Estado = cmbEstado.SelectedItem.ToString(),
                DNI_Empleado = Convert.ToString(cmbEmpleado.SelectedValue)
            };

            conexionBD BD = new conexionBD();
            if (BD.AgregarPedido(nuevoPedido))
            {
                RecargarDatos();
                MessageBox.Show("Pedido agregado correctamente.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsPedido editarPedido = new clsPedido()
            {
                ID_Pedido = Convert.ToInt32(nudIDPedido.Value),
                ID_Mesa = Convert.ToInt32(cmbIdMesa.SelectedValue),
                Fecha = dtpFecha.Value,
                Estado = cmbEstado.SelectedItem.ToString(),
                DNI_Empleado = Convert.ToString(cmbEmpleado.SelectedValue)
            };

            conexionBD BD = new conexionBD();
            if (BD.EditarPedido(editarPedido))
            {
                RecargarDatos();
                MessageBox.Show("Pedido editado correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarPedido(Convert.ToInt32(nudEliminarPedido.Value)))
            {
                RecargarDatos();
                MessageBox.Show("Pedido eliminado correctamente.");
            }
        }

        private void cmbVerEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            cmbVerEstado.SelectedIndex = -1;
            dtpFechaFiltro.Value = DateTime.Today;
            bsPedidos.Filter = string.Empty;
        }

        private void dtpFechaFiltro_ValueChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        public void CargarPedidos()
        {
            conexionBD BD = new conexionBD();
            bsPedidos.DataSource = BD.ObtenerPedidos();
            dgvPedido.DataSource = bsPedidos;
        }

        private void AplicarFiltro()
        {
            string estado = cmbVerEstado.SelectedIndex != -1 ? cmbVerEstado.SelectedItem.ToString() : "";
            string fecha = dtpFechaFiltro.Value.ToString("yyyy-MM-dd");

            string filtro = "";
            if (!string.IsNullOrEmpty(estado))
                filtro += $"Estado = '{estado}'";
            if (!string.IsNullOrEmpty(fecha))
                filtro += (filtro.Length > 0 ? " AND " : "") + $"Fecha >= '{fecha}'";

            bsPedidos.Filter = filtro;
        }

        public void CargarMesas()
        {
            conexionBD BD = new conexionBD();
            cmbIdMesa.DataSource = BD.ObtenerMesas();
            cmbIdMesa.DisplayMember = "ID_Mesa";
            cmbIdMesa.ValueMember = "ID_Mesa";
        }

        public void CargarEmpleados()
        {
            conexionBD BD = new conexionBD();
            cmbEmpleado.DataSource = BD.ObtenerEmpleados();
            cmbEmpleado.DisplayMember = "Nombre_Apellido";
            cmbEmpleado.ValueMember = "DNI_Empleado";
        }

        private void RecargarDatos()
        {
            CargarPedidos();
            CargarMesas();
            CargarEmpleados();
        }

        private void AplicarEstiloGrilla()
        {
            dgvPedido.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvPedido.EnableHeadersVisualStyles = false;
            dgvPedido.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvPedido.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvPedido.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvPedido.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvPedido.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvPedido.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvPedido.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvPedido.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvPedido.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvPedido.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvPedido.BorderStyle = BorderStyle.None;
            dgvPedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
