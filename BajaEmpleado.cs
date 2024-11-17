using System;
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
    public partial class BajaEmpleado : Form
    {
        public DateTime periodo { get; set; }
        public DateTime FechaIngreso { get; set; }

        private Empleados _empleado;
        public BajaEmpleado(Empleados empleado)
        {
            InitializeComponent();
            _empleado = empleado;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BajaEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _empleado.Fecha_Baja = dateTimePicker1.Value;
            this.Close();
        }
    }
}
