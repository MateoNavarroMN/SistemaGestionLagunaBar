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
    public partial class frmEditarCategorias : Form
    {
        public frmEditarCategorias()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private void frmEditarCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
        }
        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsCategoriaMenu nuevaCategoria = new clsCategoriaMenu()
            {
                Nombre = txtNombre.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.AgregarCategoriaMenu(nuevaCategoria))
            {
                CargarCategorias();
                MessageBox.Show("Categoria agregada correctamente.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsCategoriaMenu editarCategoria = new clsCategoriaMenu()
            {
                ID_Categoria = Convert.ToInt32(nudIdCategoria.Value),
                Nombre = txtNombre.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.EditarCategoriaMenu(editarCategoria))
            {
                CargarCategorias();
                MessageBox.Show("Categoria editada correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarCategoriaMenu(Convert.ToInt32(cmbCategoriaEliminar.SelectedValue)))
            {
                CargarCategorias();
                MessageBox.Show("Menú eliminado correctamente.");
            }
        }

        private void CargarCategorias()
        {
            conexionBD BD = new conexionBD();
            DataTable categorias = BD.ObtenerCategoriasMenus();

            dgvCategoria.DataSource = categorias;

            cmbCategoriaEliminar.DataSource = categorias;
            cmbCategoriaEliminar.DisplayMember = "Nombre";
            cmbCategoriaEliminar.ValueMember = "ID_Categoria";
        }

        private void AplicarEstiloGrilla()
        {
            dgvCategoria.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvCategoria.EnableHeadersVisualStyles = false;
            dgvCategoria.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvCategoria.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvCategoria.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvCategoria.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvCategoria.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvCategoria.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvCategoria.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvCategoria.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvCategoria.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvCategoria.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvCategoria.BorderStyle = BorderStyle.None;
            dgvCategoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
