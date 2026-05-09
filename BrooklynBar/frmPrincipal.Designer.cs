namespace BrooklynBar
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.datosDelProyectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vermesasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ventasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.estadisticasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarDatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoriasMenusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detallesPedidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedidosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nivelesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ventasToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mesasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Bisque;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sistemaToolStripMenuItem,
            this.vermesasToolStripMenuItem,
            this.ventasToolStripMenuItem1,
            this.estadisticasToolStripMenuItem,
            this.editarDatosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(945, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            // 
            // sistemaToolStripMenuItem
            // 
            this.sistemaToolStripMenuItem.BackColor = System.Drawing.Color.Bisque;
            this.sistemaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inicioToolStripMenuItem,
            this.toolStripMenuItem1,
            this.datosDelProyectoToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.sistemaToolStripMenuItem.Name = "sistemaToolStripMenuItem";
            this.sistemaToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.sistemaToolStripMenuItem.Text = "Sistema";
            this.sistemaToolStripMenuItem.Click += new System.EventHandler(this.sistemaToolStripMenuItem_Click);
            // 
            // inicioToolStripMenuItem
            // 
            this.inicioToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.inicioToolStripMenuItem.Name = "inicioToolStripMenuItem";
            this.inicioToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.inicioToolStripMenuItem.Text = "Inicio";
            this.inicioToolStripMenuItem.Click += new System.EventHandler(this.inicioToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 6);
            // 
            // datosDelProyectoToolStripMenuItem
            // 
            this.datosDelProyectoToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.datosDelProyectoToolStripMenuItem.Name = "datosDelProyectoToolStripMenuItem";
            this.datosDelProyectoToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.datosDelProyectoToolStripMenuItem.Text = "Acerca de ...";
            this.datosDelProyectoToolStripMenuItem.Click += new System.EventHandler(this.datosDelProyectoToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // vermesasToolStripMenuItem
            // 
            this.vermesasToolStripMenuItem.Name = "vermesasToolStripMenuItem";
            this.vermesasToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.vermesasToolStripMenuItem.Text = "Mesas";
            this.vermesasToolStripMenuItem.Click += new System.EventHandler(this.vermesasToolStripMenuItem_Click);
            // 
            // ventasToolStripMenuItem1
            // 
            this.ventasToolStripMenuItem1.Name = "ventasToolStripMenuItem1";
            this.ventasToolStripMenuItem1.Size = new System.Drawing.Size(53, 20);
            this.ventasToolStripMenuItem1.Text = "Ventas";
            this.ventasToolStripMenuItem1.Click += new System.EventHandler(this.ventasToolStripMenuItem1_Click);
            // 
            // estadisticasToolStripMenuItem
            // 
            this.estadisticasToolStripMenuItem.Name = "estadisticasToolStripMenuItem";
            this.estadisticasToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.estadisticasToolStripMenuItem.Text = "Estadisticas";
            this.estadisticasToolStripMenuItem.Click += new System.EventHandler(this.estadisticasToolStripMenuItem_Click);
            // 
            // editarDatosToolStripMenuItem
            // 
            this.editarDatosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.categoriasMenusToolStripMenuItem,
            this.detallesPedidosToolStripMenuItem,
            this.empleadosToolStripMenuItem,
            this.pedidosToolStripMenuItem1,
            this.nivelesToolStripMenuItem,
            this.barriosToolStripMenuItem,
            this.ventasToolStripMenuItem2,
            this.menusToolStripMenuItem,
            this.mesasToolStripMenuItem});
            this.editarDatosToolStripMenuItem.Name = "editarDatosToolStripMenuItem";
            this.editarDatosToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.editarDatosToolStripMenuItem.Text = "Editar Datos";
            this.editarDatosToolStripMenuItem.Click += new System.EventHandler(this.editarDatosToolStripMenuItem_Click);
            // 
            // categoriasMenusToolStripMenuItem
            // 
            this.categoriasMenusToolStripMenuItem.Name = "categoriasMenusToolStripMenuItem";
            this.categoriasMenusToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.categoriasMenusToolStripMenuItem.Text = "Categorias Menus";
            this.categoriasMenusToolStripMenuItem.Click += new System.EventHandler(this.categoriasMenusToolStripMenuItem_Click);
            // 
            // detallesPedidosToolStripMenuItem
            // 
            this.detallesPedidosToolStripMenuItem.Name = "detallesPedidosToolStripMenuItem";
            this.detallesPedidosToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.detallesPedidosToolStripMenuItem.Text = "Detalles Pedidos";
            this.detallesPedidosToolStripMenuItem.Click += new System.EventHandler(this.detallesPedidosToolStripMenuItem_Click);
            // 
            // empleadosToolStripMenuItem
            // 
            this.empleadosToolStripMenuItem.Name = "empleadosToolStripMenuItem";
            this.empleadosToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.empleadosToolStripMenuItem.Text = "Empleados";
            this.empleadosToolStripMenuItem.Click += new System.EventHandler(this.empleadosToolStripMenuItem_Click);
            // 
            // pedidosToolStripMenuItem1
            // 
            this.pedidosToolStripMenuItem1.Name = "pedidosToolStripMenuItem1";
            this.pedidosToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.pedidosToolStripMenuItem1.Text = "Pedidos";
            this.pedidosToolStripMenuItem1.Click += new System.EventHandler(this.pedidosToolStripMenuItem1_Click);
            // 
            // nivelesToolStripMenuItem
            // 
            this.nivelesToolStripMenuItem.Name = "nivelesToolStripMenuItem";
            this.nivelesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.nivelesToolStripMenuItem.Text = "Niveles";
            this.nivelesToolStripMenuItem.Click += new System.EventHandler(this.nivelesToolStripMenuItem_Click);
            // 
            // barriosToolStripMenuItem
            // 
            this.barriosToolStripMenuItem.Name = "barriosToolStripMenuItem";
            this.barriosToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.barriosToolStripMenuItem.Text = "Barrios";
            this.barriosToolStripMenuItem.Click += new System.EventHandler(this.barriosToolStripMenuItem_Click);
            // 
            // ventasToolStripMenuItem2
            // 
            this.ventasToolStripMenuItem2.Name = "ventasToolStripMenuItem2";
            this.ventasToolStripMenuItem2.Size = new System.Drawing.Size(169, 22);
            this.ventasToolStripMenuItem2.Text = "Ventas";
            this.ventasToolStripMenuItem2.Click += new System.EventHandler(this.ventasToolStripMenuItem2_Click);
            // 
            // menusToolStripMenuItem
            // 
            this.menusToolStripMenuItem.Name = "menusToolStripMenuItem";
            this.menusToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.menusToolStripMenuItem.Text = "Menus";
            this.menusToolStripMenuItem.Click += new System.EventHandler(this.menusToolStripMenuItem_Click);
            // 
            // mesasToolStripMenuItem
            // 
            this.mesasToolStripMenuItem.Name = "mesasToolStripMenuItem";
            this.mesasToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.mesasToolStripMenuItem.Text = "Mesas";
            this.mesasToolStripMenuItem.Click += new System.EventHandler(this.mesasToolStripMenuItem_Click);
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.BackgroundImage = global::BrooklynBar.Properties.Resources.ChatGPT_Image_18_jun_2025__11_40_00;
            this.pnlContenedor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlContenedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(0, 24);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(945, 714);
            this.pnlContenedor.TabIndex = 1;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 738);
            this.Controls.Add(this.pnlContenedor);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "La Laguna Bar";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ventasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem estadisticasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarDatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoriasMenusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detallesPedidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pedidosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nivelesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ventasToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mesasToolStripMenuItem;
        private System.Windows.Forms.Panel pnlContenedor;
        private System.Windows.Forms.ToolStripMenuItem sistemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datosDelProyectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inicioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vermesasToolStripMenuItem;
    }
}