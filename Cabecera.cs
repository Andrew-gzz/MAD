﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAD
{
    public partial class Cabecera : Form
    {
        public Cabecera()
        {
            InitializeComponent();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Empleados newWindow = new Empleados();
            this.Hide();
            newWindow.ShowDialog();
            this.Close();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sesion newWindow = new Sesion();
            this.Hide();
            newWindow.ShowDialog();
            this.Close();
        }
        private void CalculoNominaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AjustesObligados newWindow = new AjustesObligados();
            this.Hide();
            newWindow.ShowDialog();
            this.Close();
        }

        private void reporteDeNóminaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteNómina newWindow = new ReporteNómina();
            this.Hide();
            newWindow.ShowDialog();
            this.Close();
        }

        private void empleadosSueldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatálogoEmpleadosSalarios newWindow = new CatálogoEmpleadosSalarios();
            this.Hide();
            newWindow.ShowDialog();
            this.Close();
        }

        private void Cabecera_Load(object sender, EventArgs e)
        {

        }

        private void ajustesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ajustes newWindow = new Ajustes();
            this.Hide();
            newWindow.ShowDialog();
            this.Close();
        }
    }
}
