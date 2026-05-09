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
    public partial class frmPedido : Form
    {
        public int idPedido { get; set; }

        public frmPedido(int idPedido)
        {
            this.idPedido = idPedido;
            InitializeComponent();
            AplicarEstiloGrilla();
            CargarCategorias();
            CargarMenus();
            this.Text = $"Pedido: {idPedido}";
            this.MaximizeBox = false;
        }

        private BindingSource bsMenus = new BindingSource();
        decimal totalVenta;

        private void frmPedido_Load(object sender, EventArgs e)
        {
            CargarDetalles();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltroMenu();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            DataTable menu = BD.ObtenerMenu(Convert.ToInt32(cmbMenu.SelectedValue));

            clsDetallePedido nuevoDetalle = new clsDetallePedido()
            {
                ID_Menu = Convert.ToInt32(cmbMenu.SelectedValue),
                ID_Pedido = idPedido,
                Cantidad = Convert.ToInt32(nudCantidad.Value),
                Precio_Unitario = Convert.ToDecimal(menu.Rows[0]["Precio"])
            };

            if (BD.AgregarDetallePedido(nuevoDetalle))
            {
                CargarDetalles();
                MessageBox.Show("Detalle agregado correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexionBD BD = new conexionBD();
            if (BD.EliminarDetallePedido(idPedido, Convert.ToInt32(cmbEliminarMenu.SelectedValue)))
            {
                CargarDetalles();
                MessageBox.Show("Detalle eliminado correctamente.");
            }
        }

        private void btnCerrarPedido_Click(object sender, EventArgs e)
        {
            CerrarPedido(idPedido);
        }

        private void ActualizarTotalPedido()
        {
            conexionBD BD = new conexionBD();
            DataTable detallesPedido = BD.ObtenerDetallePedido(idPedido);

            decimal total = detallesPedido.AsEnumerable().Sum(row => Convert.ToDecimal(row["Precio_Unitario"]) * Convert.ToInt32(row["Cantidad"]));
            totalVenta = total;
            lblTotalPedido.Text = $"Total: ${total:F2}";
        }

        private void CargarDetalles()
        {
            conexionBD BD = new conexionBD();
            dgvDetalles.DataSource = BD.ObtenerDetallePedido(idPedido);
            ActualizarTotalPedido();
        }

        private void CargarCategorias()
        {
            conexionBD BD = new conexionBD();
            cmbCategoria.DataSource = BD.ObtenerCategoriasMenus();
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "ID_Categoria";
        }

        private void CargarMenus()
        {
            conexionBD BD = new conexionBD();
            DataTable menus = BD.ObtenerMenus();

            bsMenus.DataSource = menus;
            cmbMenu.DataSource = bsMenus;
            cmbMenu.DisplayMember = "Nombre";
            cmbMenu.ValueMember = "ID_Menu";

            cmbEliminarMenu.DataSource = menus;
            cmbEliminarMenu.DisplayMember = "Nombre";
            cmbEliminarMenu.ValueMember = "ID_Menu";
        }

        private void AplicarFiltroMenu()
        {
            string filtro = "";

            if (cmbCategoria.SelectedIndex != -1)
            {
                DataRowView row = cmbCategoria.SelectedItem as DataRowView;
                if (row != null)
                {
                    filtro = $"ID_Categoria = {row["ID_Categoria"]}";
                }
            }

            bsMenus.Filter = filtro;
        }

        private void CerrarPedido(int idPedido)
        {
            conexionBD BD = new conexionBD();

            DataTable pedidoInfo = BD.ObtenerPedido(idPedido);
            if (pedidoInfo.Rows.Count == 0)
            {
                MessageBox.Show("Error: No se encontró el pedido.");
                return;
            }

            int idMesa = Convert.ToInt32(pedidoInfo.Rows[0]["ID_Mesa"]);
            string dniEmpleado = pedidoInfo.Rows[0]["DNI_Empleado"].ToString();

            BD.EditarPedido(new clsPedido
            {
                ID_Pedido = idPedido,
                ID_Mesa = idMesa,
                Fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                Estado = "Cerrado",
                DNI_Empleado = dniEmpleado
            });

            BD.LiberarMesa(idMesa);

            clsVenta nuevaVenta = new clsVenta
            {
                ID_Pedido = idPedido,
                DNI_Empleado = dniEmpleado,
                Fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                Total = totalVenta
            };

            BD.AgregarVenta(nuevaVenta);

            MessageBox.Show("Pedido cerrado correctamente. La venta ha sido registrada.");
            this.Close();
        }

        private void AplicarEstiloGrilla()
        {
            // Estilo general
            dgvDetalles.EnableHeadersVisualStyles = false;
            dgvDetalles.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvDetalles.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Estilo de filas normales
            dgvDetalles.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvDetalles.DefaultCellStyle.ForeColor = Color.Black;

            // Estilo de filas alternadas
            dgvDetalles.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvDetalles.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Estilo de selección
            dgvDetalles.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvDetalles.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvDetalles.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvDetalles.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Borde y apariencia más limpia
            dgvDetalles.BorderStyle = BorderStyle.None;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.BackgroundColor = Color.Bisque;
        }
    }
}