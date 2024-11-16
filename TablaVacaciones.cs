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
    public partial class TablaVacaciones : Cabecera
    {
        public TablaVacaciones()
        {
            InitializeComponent();
        }

        private void TablaVacaciones_Load(object sender, EventArgs e)
        {
            ConfigDataGrid();
            FillDataGrid();
        }
        private void ConfigDataGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("Antiguedad(AÑOS)", "Antiguedad(AÑOS)");
            dataGridView1.Columns.Add("Dias de Vacaciones", "Dias de Vacaciones");
        }
        private void FillDataGrid()
        {
            dataGridView1.Rows.Clear();
            List<Vacaciones> vacaciones = VacacionesDAO.ListarVacaciones();
            foreach(var row  in vacaciones)
            {
                string years = row.Antiguedad.ToString();
                string days = row.DiasVacaciones.ToString();
                dataGridView1.Rows.Add(years, days);
            }
        }
    }
}
