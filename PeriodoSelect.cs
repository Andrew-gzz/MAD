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
    public partial class PeriodoSelect : Form
    {
        public PeriodoSelect()
        {
            InitializeComponent();
            DataConfig();
            FillData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime periodofecha = dateTimePicker1.Value.Date;
            int periodoid = PeriodoDAO.ObtIdPerPorFInicial(periodofecha);
            Cabecera.idperiodo = periodoid;
            if (periodoid!=0)
            {
                Nomina newWindow = new Nomina();
                this.Hide();
                newWindow.ShowDialog();
                this.Close();
            }
        }
        private void DataConfig()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Fecha Inicial", "Fecha Inicial");
            dataGridView1.Columns.Add("Fecha Final", "Fecha Final");
            
        }
        private void FillData()
        {
            Periodo periodoactual = PeriodoDAO.ObtenerPeriodoActual();
            dateTimePicker1.Value = periodoactual.FInicial;
            List<Periodo> rows = PeriodoDAO.ObtenerPeriodos();
            foreach (var row in rows)
            {
                dataGridView1.Rows.Add(row.IdPeriodo.ToString(), row.FInicial.ToString("yyyy-MM-dd"), row.FFin.ToString("yyyy-MM-dd")); 
            }
            dataGridView1.Sort(dataGridView1.Columns["Fecha Inicial"], System.ComponentModel.ListSortDirection.Descending);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                label2.Text = "Periodo Seleccionado:";
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int idper = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                Periodo periodo = PeriodoDAO.ObtenerPeriodoporID(idper);
                dateTimePicker1.Value = periodo.FInicial;
            }
        }
    }
}
