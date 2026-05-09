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
    public partial class frmVistaPreviaTicket : Form
    {
        public frmVistaPreviaTicket()
        {
            InitializeComponent();
        }

        private string contenidoTicket;

        private void frmVistaPreviaTicket_Load(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            dlg.Document = printDocument1;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font fuente = new Font("Consolas", 10);
            float y = 20;
            float left = 20;

            foreach (string linea in contenidoTicket.Split('\n'))
            {
                e.Graphics.DrawString(linea, fuente, Brushes.Black, left, y);
                y += fuente.GetHeight(e.Graphics) + 2;
            }
        }

        public void MostrarTexto(string texto)
        {
            rtbVistaPrevia.Font = new Font("Consolas", 10);
            rtbVistaPrevia.Text = texto;
        }
    }
}
