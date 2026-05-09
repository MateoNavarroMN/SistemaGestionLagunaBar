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
    public partial class frmEditarNiveles : Form
    {
        public frmEditarNiveles()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsNivel nuevoNivel = new clsNivel()
            {
                Nombre = txtNombre.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.AgregarNivel(nuevoNivel))
            {
                CargarNivel();
                MessageBox.Show("Nivel agregado correctamente.");
            }
        }

        private void frmEditarNiveles_Load(object sender, EventArgs e)
        {
            CargarNivel();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsNivel editarNivel = new clsNivel()
            {
                ID_Nivel = Convert.ToInt32(cmbNivel.SelectedValue),
                Nombre = txtNombre.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.EditarNivel(editarNivel))
            {
                CargarNivel();
                MessageBox.Show("Nivel editado correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarNivel(Convert.ToInt32(cmbVerNivel.SelectedValue)))
            {
                CargarNivel();
                MessageBox.Show("Menú eliminado correctamente.");
            }
        }

        public void CargarNivel()
        {
            conexionBD BD = new conexionBD();
            DataTable niveles = BD.ObtenerNiveles();

            dgvNivel.DataSource = niveles;

            cmbNivel.DataSource = niveles;
            cmbNivel.DisplayMember = "ID_Nivel";
            cmbNivel.ValueMember = "ID_Nivel";

            cmbVerNivel.DataSource = niveles;
            cmbVerNivel.DisplayMember = "Nombre";
            cmbVerNivel.ValueMember = "ID_Nivel";
        }

        private void AplicarEstiloGrilla()
        {
            dgvNivel.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvNivel.EnableHeadersVisualStyles = false;
            dgvNivel.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvNivel.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvNivel.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvNivel.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvNivel.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvNivel.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvNivel.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvNivel.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvNivel.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvNivel.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvNivel.BorderStyle = BorderStyle.None;
            dgvNivel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
