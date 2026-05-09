using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BrooklynBar
{
    public partial class frmEditarBarrios : Form
    {
        public frmEditarBarrios()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private void frmEditarBarrios_Load(object sender, EventArgs e)
        {
            CargarBarrios();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsBarrio nuevoBarrio = new clsBarrio()
            {
                ID_Barrio = Convert.ToInt32(nudIdBarrio.Value),
                Nombre = txtNombre.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.AgregarBarrio(nuevoBarrio))
            {
                CargarBarrios();
                MessageBox.Show("Barrio agregado correctamente.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsBarrio editarBarrio = new clsBarrio()
            {
                ID_Barrio = Convert.ToInt32(nudIdBarrio.Value),
                Nombre = txtNombre.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.EditarBarrio(editarBarrio))
            {
                CargarBarrios();
                MessageBox.Show("Barrio editado correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarBarrio(Convert.ToInt32(nudEliminarBarrio.Value)))
            {
                CargarBarrios();
                MessageBox.Show("Barrio eliminado correctamente.");
            }
        }

        public void CargarBarrios()
        {
            conexionBD BD = new conexionBD();
            dgvBarrio.DataSource = BD.ObtenerBarrios();
        }

        private void AplicarEstiloGrilla()
        {
            dgvBarrio.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvBarrio.EnableHeadersVisualStyles = false;
            dgvBarrio.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvBarrio.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvBarrio.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvBarrio.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvBarrio.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvBarrio.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvBarrio.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvBarrio.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvBarrio.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvBarrio.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvBarrio.BorderStyle = BorderStyle.None;
            dgvBarrio.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
