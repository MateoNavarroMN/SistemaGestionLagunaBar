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
    public partial class frmEditarEmpleados : Form
    {
        public frmEditarEmpleados()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
        }

        private BindingSource bsEmpleados = new BindingSource();

        private void frmEditarEmpleados_Load(object sender, EventArgs e)
        {
            RecargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsEmpleado nuevoEmpleado = new clsEmpleado()
            {
                DNI_Empleado = txtDNI.Text,
                Nombre_Apellido = txtNombreApellido.Text,
                Sexo = cmbSexo.SelectedItem.ToString(),
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                ID_Barrio = Convert.ToInt32(cmbBarrio.SelectedValue),
                Contacto_Emergencia = txtContactoEmg.Text,
                ID_Nivel = Convert.ToInt32(cmbNivel.SelectedValue),
                Contrasena = txtContraseña.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.AgregarEmpleado(nuevoEmpleado))
            {
                RecargarDatos();
                MessageBox.Show("Empleado agregado correctamente.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsEmpleado editarEmpleado = new clsEmpleado()
            {
                DNI_Empleado = txtDNI.Text,
                Nombre_Apellido = txtNombreApellido.Text,
                Sexo = cmbSexo.SelectedText,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                ID_Barrio = Convert.ToInt32(cmbBarrio.SelectedValue),
                Contacto_Emergencia = txtContactoEmg.Text,
                ID_Nivel = Convert.ToInt32(cmbNivel.SelectedValue),
                Contrasena = txtContraseña.Text
            };

            conexionBD BD = new conexionBD();
            if (BD.EditarEmpleado(editarEmpleado))
            {
                RecargarDatos();
                MessageBox.Show("Empleado editado correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarEmpleado(Convert.ToInt32(cmbEliminarEmpleado.SelectedValue)))
            {
                RecargarDatos();
                MessageBox.Show("Empleado eliminado correctamente.");
            }
        }

        private void cmbVerNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            cmbVerNivel.SelectedIndex = -1;
            bsEmpleados.Filter = string.Empty;
        }

        public void CargarEmpleados()
        {
            conexionBD BD = new conexionBD();
            bsEmpleados.DataSource = BD.ObtenerEmpleados();
            dgvEmpleado.DataSource = bsEmpleados;

            cmbEliminarEmpleado.DataSource = bsEmpleados;
            cmbEliminarEmpleado.DisplayMember = "Nombre";
            cmbEliminarEmpleado.ValueMember = "DNI_Empleado";
        }

        private void AplicarFiltro()
        {
            string filtro = "";

            if (cmbVerNivel.SelectedIndex != -1)
            {
                DataRowView row = cmbVerNivel.SelectedItem as DataRowView;
                if (row != null)
                {
                    filtro = $"ID_Nivel = {row["ID_Nivel"]}";
                }
            }

            bsEmpleados.Filter = filtro;
        }

        public void CargarNivel()
        {
            conexionBD BD = new conexionBD();
            DataTable niveles = BD.ObtenerNiveles();

            cmbNivel.DataSource = niveles;
            cmbNivel.DisplayMember = "Nombre";
            cmbNivel.ValueMember = "ID_Nivel";

            cmbVerNivel.DataSource = niveles;
            cmbVerNivel.DisplayMember = "Nombre";
            cmbVerNivel.ValueMember = "ID_Nivel";
        }

        public void CargarBarrios()
        {
            conexionBD BD = new conexionBD();
            DataTable barrios = BD.ObtenerBarrios();

            cmbBarrio.DataSource = barrios;
            cmbBarrio.DisplayMember = "Nombre";
            cmbBarrio.ValueMember = "ID_Barrio";
        }

        private void RecargarDatos()
        {
            CargarNivel();
            CargarEmpleados();
            CargarBarrios();
        }

        private void AplicarEstiloGrilla()
        {
            dgvEmpleado.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvEmpleado.EnableHeadersVisualStyles = false;
            dgvEmpleado.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvEmpleado.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvEmpleado.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvEmpleado.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvEmpleado.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvEmpleado.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvEmpleado.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvEmpleado.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvEmpleado.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvEmpleado.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvEmpleado.BorderStyle = BorderStyle.None;
            dgvEmpleado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
