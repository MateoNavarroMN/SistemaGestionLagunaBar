using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrooklynBar
{
    public partial class frmPrincipal : Form
    {
        string nivel {  get; set; }

        public frmPrincipal(string nivel)
        {
            InitializeComponent();
            this.nivel = nivel;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            EstablecerNivel();
        }

        #region Mostrar ventanas hijas
        public void AbrirFromHija(object formHija)
        {
            if (this.pnlContenedor.Controls.Count > 0)
            {
                this.pnlContenedor.Controls.RemoveAt(0);
            }
            Form fh = formHija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(fh);
            this.pnlContenedor.Tag = fh;
            fh.Show();
        }
        #endregion

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmMesas());
        }

        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmVenta());
        }

        private void estadisticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEstadisticas());
        }

        private void editarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void categoriasMenusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarCategorias());
        }

        private void detallesPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarDetallesPedido());
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarEmpleados());
        }

        private void pedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarPedidos());
        }

        private void nivelesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarNiveles());
        }

        private void barriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarBarrios());
        }

        private void ventasToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarVentas());
        }

        private void menusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarMenus());
        }

        private void mesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmEditarMesas());
        }

        private void datosDelProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcercaDe frm = new frmAcercaDe();
            frm.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlContenedor.Controls.Clear();
        }

        private void vermesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFromHija(new frmMesas());
        }

        #region Mover ventana
        //codigo para poder mover la Ventana (https://youtu.be/eCSbUCL4teE?si=7f6B2CBHkkiQO9n-&t=563)
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        public void EstablecerNivel()
        {
            switch (nivel)
            {
                case "Administrador":
                    sistemaToolStripMenuItem.Visible = true;
                    vermesasToolStripMenuItem.Visible = true;
                    ventasToolStripMenuItem1.Visible = true;
                    estadisticasToolStripMenuItem.Visible = true;
                    editarDatosToolStripMenuItem.Visible = true;
                    break;
                default:
                    sistemaToolStripMenuItem.Visible = true;
                    vermesasToolStripMenuItem.Visible = true;
                    ventasToolStripMenuItem1.Visible = true;
                    estadisticasToolStripMenuItem.Visible = false;
                    editarDatosToolStripMenuItem.Visible = false;
                    break;
            }
        }
    }
}
