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
    public partial class frmEditarMenus : Form
    {
        public frmEditarMenus()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private BindingSource bsMenus = new BindingSource();
        
        private void frmEditarMenus_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarMenus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsMenu nuevoMenu = new clsMenu()
            {
                ID_Categoria = Convert.ToInt32(cmbCategoria.SelectedValue),
                Nombre = txtNombre.Text,
                Precio = Convert.ToDecimal(nudPrecio.Value),
                Descripcion = txtDescripcion.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.AgregarMenu(nuevoMenu))
            {
                CargarMenus();
                MessageBox.Show("Menú agregado correctamente.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsMenu menuEdit = new clsMenu()
            {
                ID_Menu = Convert.ToInt32(nudIDMenu.Value),
                ID_Categoria = Convert.ToInt32(cmbCategoria.SelectedValue),
                Nombre = txtNombre.Text,
                Precio = Convert.ToDecimal(nudPrecio.Value),
                Descripcion = txtDescripcion.Text
            };
            
            conexionBD BD = new conexionBD();
            if (BD.EditarMenu(menuEdit))
            {
                CargarMenus();
                MessageBox.Show("Menú editado correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarMenu(Convert.ToInt32(cmbEliminarMenu.SelectedValue)))
            {
                CargarMenus();
                MessageBox.Show("Menú eliminado correctamente.");
            }
        }

        private void cmbVerCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            cmbVerCategoria.SelectedIndex = -1;
            bsMenus.Filter = string.Empty;
        }

        private void CargarCategorias()
        {
            conexionBD BD = new conexionBD();
            DataTable categorias = BD.ObtenerCategoriasMenus();

            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "ID_Categoria";

            cmbVerCategoria.DataSource = categorias;
            cmbVerCategoria.DisplayMember = "Nombre";
            cmbVerCategoria.ValueMember = "ID_Categoria";
        }

        public void CargarMenus()
        {
            conexionBD BD = new conexionBD();
            bsMenus.DataSource = BD.ObtenerMenus();
            dgvMenu.DataSource = bsMenus;

            cmbEliminarMenu.DataSource = bsMenus;
            cmbEliminarMenu.DisplayMember = "Nombre";
            cmbEliminarMenu.ValueMember = "ID_Menu";
        }

        private void AplicarFiltro()
        {
            string filtro = "";

            if (cmbVerCategoria.SelectedIndex != -1)
            {
                DataRowView row = cmbVerCategoria.SelectedItem as DataRowView;
                if (row != null)
                {
                    filtro = $"ID_Categoria = {row["ID_Categoria"]}";
                }
            }

            bsMenus.Filter = filtro;
        }

        private void AplicarEstiloGrilla()
        {
            dgvMenu.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvMenu.EnableHeadersVisualStyles = false;
            dgvMenu.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvMenu.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvMenu.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvMenu.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvMenu.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvMenu.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvMenu.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvMenu.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvMenu.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvMenu.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvMenu.BorderStyle = BorderStyle.None;
            dgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
