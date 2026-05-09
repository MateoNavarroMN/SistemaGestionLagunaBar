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
    public partial class frmEditarDetallesPedido : Form
    {
        public frmEditarDetallesPedido()
        {
            InitializeComponent();
            AplicarEstiloGrilla();
            
        }

        private BindingSource bsDetalles = new BindingSource();

        private void frmEditarDetallesPedido_Load(object sender, EventArgs e)
        {
            RecargarDatos();
            lblTotalPedido.Text = string.Empty;
            lblTotalPedido.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsDetallePedido nuevoDetalle = new clsDetallePedido()
            {
                ID_Menu = Convert.ToInt32(cmbMenu.SelectedValue),
                ID_Pedido = Convert.ToInt32(cmbPedido.SelectedValue),
                Cantidad = Convert.ToInt32(nudCantidad.Value),
                Precio_Unitario = Convert.ToDecimal(nudPrecio.Value),
            };

            conexionBD BD = new conexionBD();
            if (BD.AgregarDetallePedido(nuevoDetalle))
            {
                RecargarDatos();
                MessageBox.Show("Detalle agregado correctamente.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            clsDetallePedido editarDetalle = new clsDetallePedido()
            {
                ID_Detalle = Convert.ToInt32(nudIdDetalle.Value),
                ID_Menu = Convert.ToInt32(cmbMenu.SelectedValue),
                ID_Pedido = Convert.ToInt32(cmbPedido.SelectedValue),
                Cantidad = Convert.ToInt32(nudCantidad.Value),
                Precio_Unitario = Convert.ToDecimal(nudPrecio.Value),
            };

            conexionBD BD = new conexionBD();
            if (BD.EditarDetallePedido(editarDetalle))
            {
                RecargarDatos();
                MessageBox.Show("Detalle editado correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarDetallePedido(Convert.ToInt32(nudEliminarDetalle.Value)))
            {
                RecargarDatos();
                MessageBox.Show("Detalle eliminado correctamente.");
            }
        }

        private void cmbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMenu.SelectedIndex > -1 && !(cmbMenu.SelectedValue is DataRowView))
            {
                int idMenu = Convert.ToInt32(cmbMenu.SelectedValue);
                conexionBD BD = new conexionBD();
                DataTable menu = BD.ObtenerMenu(idMenu);
                if (menu.Rows.Count > 0)
                {
                    nudPrecio.Value = Convert.ToDecimal(menu.Rows[0]["Precio"]);
                }
            }
        }

        private void cmbVerPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            cmbVerPedido.SelectedIndex = -1;
            bsDetalles.Filter = string.Empty;
            lblTotalPedido.Text = string.Empty;
            lblTotalPedido.Visible = false;
        }

        private void CargarDetalles()
        {
            conexionBD BD = new conexionBD();
            bsDetalles.DataSource = BD.ObtenerDetallesPedido().Tables[0];
            dgvDetalle.DataSource = bsDetalles;
        }

        private void AplicarFiltro()
        {
            if (cmbVerPedido.SelectedIndex > -1)
            {
                DataRowView row = cmbVerPedido.SelectedItem as DataRowView;
                if (row != null)
                {
                    string filtro = $"ID_Pedido = {row["ID_Pedido"]}";
                    bsDetalles.Filter = filtro;

                    CalcularTotalPedido();
                    lblTotalPedido.Visible = true;
                }
            }
            else
            {
                bsDetalles.Filter = string.Empty;
                lblTotalPedido.Text = string.Empty;
                lblTotalPedido.Visible = false;
            }
        }

        private void CargarPedidos()
        {
            conexionBD BD = new conexionBD();
            DataTable pedidos = BD.ObtenerPedidos();

            cmbPedido.DataSource = pedidos;
            cmbPedido.DisplayMember = "Nombre";
            cmbPedido.ValueMember = "ID_Pedido";

            cmbVerPedido.DataSource = pedidos;
            cmbVerPedido.DisplayMember = "Nombre";
            cmbVerPedido.ValueMember = "ID_Pedido";

            cmbVerPedido.SelectedIndex = -1;
        }

        public void CargarMenus()
        {
            conexionBD BD = new conexionBD();
            DataTable menus = BD.ObtenerMenus();

            cmbMenu.DataSource = menus;
            cmbMenu.DisplayMember = "Nombre";
            cmbMenu.ValueMember = "ID_Menu";

            cmbMenu.SelectedIndex = -1;
        }

        private void RecargarDatos()
        {
            CargarDetalles();
            CargarPedidos();
            CargarMenus();
        }

        private void CalcularTotalPedido()
        {
            decimal total = 0;

            foreach (DataRowView row in bsDetalles.List)
            {
                total += Convert.ToDecimal(row["Cantidad"]) * Convert.ToDecimal(row["Precio_Unitario"]);
            }

            lblTotalPedido.Text = $"Total: $ {total:F2}";
        }

        private void AplicarEstiloGrilla()
        {
            dgvDetalle.BackgroundColor = Color.Bisque;
            // Estilo general
            dgvDetalle.EnableHeadersVisualStyles = false;
            dgvDetalle.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvDetalle.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvDetalle.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvDetalle.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvDetalle.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvDetalle.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvDetalle.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvDetalle.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvDetalle.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvDetalle.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvDetalle.BorderStyle = BorderStyle.None;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
