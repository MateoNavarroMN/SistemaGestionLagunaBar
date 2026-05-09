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
    public partial class frmEditarMesas : Form
    {
        public frmEditarMesas()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private void frmEditarMesas_Load(object sender, EventArgs e)
        {
            CargarMesas();
            CargarEmpleados();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.AgregarMesa(cmbEstado.SelectedItem.ToString()))
            {
                CargarMesas();
                MessageBox.Show("Mesa agregado correctamente.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsMesa editarMesa = new clsMesa()
            {
                ID_Mesa = Convert.ToInt32(nudIdMesa.Value),
                DNI_Empleado = cmbEmpleado.SelectedValue.ToString(),
                Estado = cmbEstado.SelectedItem.ToString()
            };

            conexionBD BD = new conexionBD();
            if (BD.EditarMesa(editarMesa))
            {
                CargarMesas();
                MessageBox.Show("Mesa editada correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarMesa (Convert.ToInt32(cmbEliminarMesa.SelectedValue)))
            {
                CargarMesas();
                MessageBox.Show("Mesa eliminada correctamente.");
            }
        }

        public void CargarMesas()
        {
            conexionBD BD = new conexionBD();
            DataTable mesas = BD.ObtenerMesas();

            dgvMesa.DataSource = mesas;

            cmbEliminarMesa.DataSource = mesas;
            cmbEliminarMesa.DisplayMember = "Nombre";
            cmbEliminarMesa.ValueMember = "ID_Mesa";
        }

        public void CargarEmpleados()
        {
            conexionBD BD = new conexionBD();
            cmbEmpleado.DataSource = BD.ObtenerEmpleados();
            cmbEmpleado.DisplayMember = "Nombre_Apellido";
            cmbEmpleado.ValueMember = "DNI_Empleado";

            cmbEmpleado.SelectedIndex = -1;
        }

        private void AplicarEstiloGrilla()
        {
            dgvMesa.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvMesa.EnableHeadersVisualStyles = false;
            dgvMesa.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvMesa.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvMesa.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvMesa.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvMesa.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvMesa.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvMesa.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvMesa.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMesa.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvMesa.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvMesa.BorderStyle = BorderStyle.None;
            dgvMesa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
