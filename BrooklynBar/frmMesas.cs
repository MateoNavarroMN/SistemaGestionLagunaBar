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
    public partial class frmMesas : Form
    {
        public frmMesas()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        BindingSource bsMesas = new BindingSource();
        string estadoMesa = "";

        private void frmMesas_Load(object sender, EventArgs e)
        {
            cmbEmpleado.Enabled = false;
            CargarMesas();
            CargarEmpleados();
        }

        private void btnAbrirPedido_Click(object sender, EventArgs e)
        {
            int idMesa = Convert.ToInt32(cmbMesa.SelectedValue);

            if (estadoMesa == "Disponible" && cmbEmpleado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un empleado para abrir el pedido.");
                return;
            }

            string dniEmpleado = estadoMesa == "Disponible" ? cmbEmpleado.SelectedValue.ToString() : "";

            AbrirPedido(idMesa, dniEmpleado);
        }

        private void cmbMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMesa.SelectedIndex != -1)
            {
                cmbEmpleado.Enabled = true;
                DataRowView row = cmbMesa.SelectedItem as DataRowView;
                if (row != null)
                {
                    int idMesa = Convert.ToInt32(row["ID_Mesa"]);
                    conexionBD BD = new conexionBD();
                    DataTable mesaInfo = BD.ObtenerEstadoMesa(idMesa);

                    if (mesaInfo.Rows.Count > 0)
                    {
                        string estado = mesaInfo.Rows[0]["Estado"].ToString();
                        MostrarEstadoMesa(estado);

                        if (estado == "Ocupada" && mesaInfo.Rows[0]["DNI_Empleado"] != DBNull.Value)
                        {
                            cmbEmpleado.SelectedValue = mesaInfo.Rows[0]["DNI_Empleado"].ToString();
                        }
                        else
                        {
                            cmbEmpleado.SelectedIndex = -1;
                        }
                    }
                }
            }
            else
            {
                cmbEmpleado.Enabled = false;
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            cmbEstado.SelectedIndex = -1;
            bsMesas.Filter = string.Empty;
        }

        public void CargarEmpleados()
        {
            conexionBD BD = new conexionBD();
            cmbEmpleado.DataSource = BD.ObtenerEmpleados();
            cmbEmpleado.DisplayMember = "Nombre_Apellido";
            cmbEmpleado.ValueMember = "DNI_Empleado";

            cmbEmpleado.SelectedIndex = -1;
        }

        public void CargarMesas()
        {
            conexionBD BD = new conexionBD();
            bsMesas.DataSource = BD.ObtenerMesas2().Tables[0];

            dgvMesas.DataSource = bsMesas;

            cmbMesa.DataSource = BD.ObtenerMesas();
            cmbMesa.DisplayMember = "ID_Mesa";
            cmbMesa.ValueMember = "ID_Mesa";

            cmbMesa.SelectedIndex = -1;
            bsMesas.AllowNew = false;

        }

        private void AplicarFiltro()
        {
            string estadoFiltro = cmbEstado.SelectedIndex != -1 ? cmbEstado.SelectedItem.ToString() : "";
            bsMesas.Filter = string.IsNullOrEmpty(estadoFiltro) ? "" : $"Estado = '{estadoFiltro}'";
        }

        private void AbrirPedido(int idMesa, string dniEmpleado)
        {
            conexionBD BD = new conexionBD();
            DataTable pedidoExistente = BD.ObtenerPedidoMesa(idMesa);

            frmPedido pedidoForm;

            if (pedidoExistente.Rows.Count > 0)
            {
                int idPedido = Convert.ToInt32(pedidoExistente.Rows[0]["ID_Pedido"]);
                pedidoForm = new frmPedido(idPedido);
            }
            else
            {
                clsPedido nuevoPedido = new clsPedido
                {
                    ID_Mesa = idMesa,
                    Fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    Estado = "Abierto",
                    DNI_Empleado = dniEmpleado
                };

                pedidoForm = new frmPedido(BD.AgregarPedidoRetornarID(nuevoPedido));
            }

            pedidoForm.ShowDialog();
            CargarMesas();
        }

        private void MostrarEstadoMesa(string estado)
        {
            estadoMesa = estado;
            lblEstadoMesa.Text = estado;

            if (estado == "Ocupada")
            {
                lblEstadoMesa.Text = "🔴" + estado;
                lblEstadoMesa.ForeColor = Color.Red;
            }
            else
            {
                lblEstadoMesa.Text = "🟢" + estado;
                lblEstadoMesa.ForeColor = Color.Green;
            }
        }

        private void AplicarEstiloGrilla()
        {
            dgvMesas.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvMesas.EnableHeadersVisualStyles = false;
            dgvMesas.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvMesas.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvMesas.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvMesas.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvMesas.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvMesas.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvMesas.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvMesas.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMesas.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvMesas.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvMesas.BorderStyle = BorderStyle.None;
            dgvMesas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
