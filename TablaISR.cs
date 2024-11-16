using MAD.DAO;
using MAD.Tablas;
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
    public partial class TablaISR : Cabecera
    {
        public TablaISR()
        {
            InitializeComponent();
            ConfigDataGrid();
        }

        private void TablaISR_Load(object sender, EventArgs e)
        {
            
            FillDataGrid();
        }

        private void ConfigDataGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("L_Inferior", "L_Inferior");
            dataGridView1.Columns.Add("Cuota", "Cuota");
            dataGridView1.Columns.Add("Porcentaje", "Porcentaje");
            dataGridView1.Columns.Add("Año", "Año");
        }
        private void FillDataGrid()
        {
            dataGridView1.Rows.Clear();
            List<ISR> list = ISRDAO.ObtenerISR();
            foreach (var row in list) {
                dataGridView1.Rows.Add(row.ID_ISR.ToString(), row.L_Inferior.ToString(), row.Cuota.ToString(), row.Porcentaje.ToString(), row.Year.ToString("yyyy"));
            }
        }
    }
}
