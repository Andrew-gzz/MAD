namespace MAD
{
    partial class Cabecera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cabecera));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajustesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nominasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reciboDeNominaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeNóminaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catálogosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosSueldosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.departamentosSueldosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.puesotsSueldosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vacacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iSRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inicioToolStripMenuItem,
            this.empleadosToolStripMenuItem,
            this.ajustesToolStripMenuItem,
            this.nominasToolStripMenuItem,
            this.catálogosToolStripMenuItem,
            this.tablasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(830, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inicioToolStripMenuItem
            // 
            this.inicioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesiónToolStripMenuItem});
            this.inicioToolStripMenuItem.Name = "inicioToolStripMenuItem";
            this.inicioToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.inicioToolStripMenuItem.Text = "Inicio";
            this.inicioToolStripMenuItem.Click += new System.EventHandler(this.inicioToolStripMenuItem_Click);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // empleadosToolStripMenuItem
            // 
            this.empleadosToolStripMenuItem.Name = "empleadosToolStripMenuItem";
            this.empleadosToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.empleadosToolStripMenuItem.Text = "Empleados";
            this.empleadosToolStripMenuItem.Click += new System.EventHandler(this.empleadosToolStripMenuItem_Click);
            // 
            // ajustesToolStripMenuItem
            // 
            this.ajustesToolStripMenuItem.Name = "ajustesToolStripMenuItem";
            this.ajustesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.ajustesToolStripMenuItem.Text = "Ajustes";
            this.ajustesToolStripMenuItem.Click += new System.EventHandler(this.ajustesToolStripMenuItem_Click);
            // 
            // nominasToolStripMenuItem
            // 
            this.nominasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reciboDeNominaToolStripMenuItem,
            this.reporteDeNóminaToolStripMenuItem});
            this.nominasToolStripMenuItem.Name = "nominasToolStripMenuItem";
            this.nominasToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.nominasToolStripMenuItem.Text = "Nóminas";
            // 
            // reciboDeNominaToolStripMenuItem
            // 
            this.reciboDeNominaToolStripMenuItem.Name = "reciboDeNominaToolStripMenuItem";
            this.reciboDeNominaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.reciboDeNominaToolStripMenuItem.Text = "DyP Obligados";
            this.reciboDeNominaToolStripMenuItem.Click += new System.EventHandler(this.CalculoNominaToolStripMenuItem_Click);
            // 
            // reporteDeNóminaToolStripMenuItem
            // 
            this.reporteDeNóminaToolStripMenuItem.Name = "reporteDeNóminaToolStripMenuItem";
            this.reporteDeNóminaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.reporteDeNóminaToolStripMenuItem.Text = "Reporte de Nómina";
            this.reporteDeNóminaToolStripMenuItem.Click += new System.EventHandler(this.reporteDeNóminaToolStripMenuItem_Click);
            // 
            // catálogosToolStripMenuItem
            // 
            this.catálogosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empleadosSueldosToolStripMenuItem,
            this.departamentosSueldosToolStripMenuItem,
            this.puesotsSueldosToolStripMenuItem,
            this.turnosToolStripMenuItem});
            this.catálogosToolStripMenuItem.Name = "catálogosToolStripMenuItem";
            this.catálogosToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.catálogosToolStripMenuItem.Text = "Catálogos";
            // 
            // empleadosSueldosToolStripMenuItem
            // 
            this.empleadosSueldosToolStripMenuItem.Name = "empleadosSueldosToolStripMenuItem";
            this.empleadosSueldosToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.empleadosSueldosToolStripMenuItem.Text = "Empleados(Sueldos)";
            this.empleadosSueldosToolStripMenuItem.Click += new System.EventHandler(this.empleadosSueldosToolStripMenuItem_Click);
            // 
            // departamentosSueldosToolStripMenuItem
            // 
            this.departamentosSueldosToolStripMenuItem.Name = "departamentosSueldosToolStripMenuItem";
            this.departamentosSueldosToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.departamentosSueldosToolStripMenuItem.Text = "Departamentos(Sueldos)";
            this.departamentosSueldosToolStripMenuItem.Click += new System.EventHandler(this.departamentosSueldosToolStripMenuItem_Click);
            // 
            // puesotsSueldosToolStripMenuItem
            // 
            this.puesotsSueldosToolStripMenuItem.Name = "puesotsSueldosToolStripMenuItem";
            this.puesotsSueldosToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.puesotsSueldosToolStripMenuItem.Text = "Puestos(Sueldos)";
            this.puesotsSueldosToolStripMenuItem.Click += new System.EventHandler(this.puesotsSueldosToolStripMenuItem_Click);
            // 
            // turnosToolStripMenuItem
            // 
            this.turnosToolStripMenuItem.Name = "turnosToolStripMenuItem";
            this.turnosToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.turnosToolStripMenuItem.Text = "Turnos(Sueldos)";
            this.turnosToolStripMenuItem.Click += new System.EventHandler(this.turnosToolStripMenuItem_Click);
            // 
            // tablasToolStripMenuItem
            // 
            this.tablasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vacacionesToolStripMenuItem,
            this.iSRToolStripMenuItem});
            this.tablasToolStripMenuItem.Name = "tablasToolStripMenuItem";
            this.tablasToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.tablasToolStripMenuItem.Text = "Tablas";
            // 
            // vacacionesToolStripMenuItem
            // 
            this.vacacionesToolStripMenuItem.Name = "vacacionesToolStripMenuItem";
            this.vacacionesToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.vacacionesToolStripMenuItem.Text = "Vacaciones";
            this.vacacionesToolStripMenuItem.Click += new System.EventHandler(this.vacacionesToolStripMenuItem_Click);
            // 
            // iSRToolStripMenuItem
            // 
            this.iSRToolStripMenuItem.Name = "iSRToolStripMenuItem";
            this.iSRToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.iSRToolStripMenuItem.Text = "I.S.R";
            this.iSRToolStripMenuItem.Click += new System.EventHandler(this.iSRToolStripMenuItem_Click);
            // 
            // Cabecera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 564);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Cabecera";
            this.Text = "Cabecera";
            this.Load += new System.EventHandler(this.Cabecera_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inicioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajustesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nominasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reciboDeNominaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeNóminaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catálogosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empleadosSueldosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vacacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iSRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem departamentosSueldosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem puesotsSueldosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnosToolStripMenuItem;
    }
}