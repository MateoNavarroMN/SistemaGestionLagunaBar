using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BrooklynBar
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            string dni = txtDNI.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();
            conexionBD BD = new conexionBD();
            DataSet ds = BD.ValidarLogin(dni, contraseña);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow datos = ds.Tables[0].Rows[0];

                // Guardar sesión
                clsSesion Sesion = new clsSesion();
                Sesion.DNI = datos["DNI_Empleado"].ToString();
                Sesion.Nombre = datos["Nombre_Apellido"].ToString();
                Sesion.Nivel = datos["Nivel"].ToString();

                // Abrir el principal
                this.Hide();
                frmPrincipal principal = new frmPrincipal(Sesion.Nivel);
                principal.ShowDialog();
                txtDNI.Clear();
                txtContraseña.Clear();
                this.Show();
            }
            else
            {
                MessageBox.Show("DNI o Contraseña incorrectas.");
                txtContraseña.Clear();
                txtContraseña.Focus();
            }
        }
    }
}
