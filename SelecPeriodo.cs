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
    public partial class SelecPeriodo : Form
    {

        public DateTime periodo { get; set; }
        public int idempleado { get; set; }

        private ReporteNómina _reportenomina;
        public SelecPeriodo(ReporteNómina reportenomina)
        {
            InitializeComponent();
            _reportenomina = reportenomina;
        }

        private void SelecPeriodo_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Agregar las columnas "Periodo (ID_Periodo)", "F_Inicio", "F_Final" al dataGridView2
            dataGridView1.Columns.Add("Periodo", "Periodo");
            dataGridView1.Columns.Add("F_Inicio", "Fecha Inicio");
            dataGridView1.Columns.Add("F_Final", "Fecha Fin");

            // Obtener todos los periodos desde la fecha de ingreso del empleado
            List<Movimientos_Empleados> lista = Movimientos_EmpleadosDAO.ObtPeriodosPorEmp(idempleado);
            // Llenar el DataGridView con los periodos obtenidos
            foreach (var row in lista)
            {
                List<Periodo> periodos = PeriodoDAO.ObtPeridosEnUnRango(row.ID_PERIODO_ALTA, row.ID_PERIODO_BAJA);
                foreach (var periodo in periodos) { 

                    dataGridView1.Rows.Add(periodo.IdPeriodo, periodo.FInicial.ToString("yyyy-MM-dd"), periodo.FFin.ToString("yyyy-MM-dd"));
                }
            }
            int idperiodo = Movimientos_EmpleadosDAO.ObtPeriodoAlta(idempleado);
            if (idperiodo > 0) {
                List<Periodo> Periodos = PeriodoDAO.PeriodoEnAdelante(idperiodo);
                foreach (var periodo in Periodos)
                {

                    dataGridView1.Rows.Add(periodo.IdPeriodo, periodo.FInicial.ToString("yyyy-MM-dd"), periodo.FFin.ToString("yyyy-MM-dd"));
                }
            }
        }
        public int idper;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                idper = Convert.ToInt32(selectedRow.Cells["Periodo"].Value);
            }
        }
        private void button1_Click(object sender, EventArgs e)//Boton de aceptar y devolver el id
        {
            if (idper!=0)
            {
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecciona un periodo o cierra la ventana para cancelar");
            }
        }
    }
}
