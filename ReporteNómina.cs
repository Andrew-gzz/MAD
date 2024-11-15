using MAD.DAO;
using MAD.DAO.MAD.DAO;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MAD
{
    public partial class ReporteNómina : Cabecera 
    {
        public ReporteNómina()
        {
            InitializeComponent();
        }

        private void ReporteNómina_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            RellenarDataGridView();
        }
        private void ConfigurarDataGridView()
        {
            // Limpiar columnas anteriores si existen
            dataGridView1.Columns.Clear();

            // Agregar las columnas "Empleado" y "ID"           
            dataGridView1.Columns.Add("Empleado", "Empleado");
            dataGridView1.Columns.Add("ID", "ID");
            // Opcional: Ajustar ancho de columnas para mejor visualización           
            dataGridView1.Columns["Empleado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void RellenarDataGridView()
        {
            // Limpiar filas existentes en el DataGridView
            dataGridView1.Rows.Clear();

            // Obtener lista de empleados desde la base de datos
            List<Empleado> listaEmpleados = EmpleadoDAO.ObtenerEmpleados();

            // Rellenar el DataGridView con la información de empleados
            foreach (Empleado empleado in listaEmpleados)
            {
                dataGridView1.Rows.Add(empleado.Nombre, empleado.IdEmpleado);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int selectedID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                // Obtener la información del empleado
                Empleado empleado = EmpleadoDAO.ObtenerEmpleadoPorId(selectedID);

                if (empleado != null)
                {
                    // Limpiar el DataGridView2 antes de llenarlo
                    dataGridView2.Rows.Clear();
                    dataGridView2.Columns.Clear();

                    // Agregar las columnas "Periodo (ID_Periodo)", "F_Inicio", "F_Final" al dataGridView2
                    dataGridView2.Columns.Add("Periodo", "Periodo");
                    dataGridView2.Columns.Add("F_Inicio", "Fecha Inicio");
                    dataGridView2.Columns.Add("F_Final", "Fecha Fin");

                    // Obtener todos los periodos desde la fecha de ingreso del empleado
                    List<Periodo> periodos = PeriodoDAO.ObtenerPeriodosDesdeFechaIngreso(empleado.FechaDeIngreso);

                    // Llenar el DataGridView con los periodos obtenidos
                    foreach (var periodo in periodos)
                    {
                        dataGridView2.Rows.Add(periodo.IdPeriodo, periodo.FInicial.ToString("yyyy-MM-dd"), periodo.FFin.ToString("yyyy-MM-dd"));
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del empleado.");
                }
            }
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
                int selectedID = Convert.ToInt32(selectedRow.Cells["Periodo"].Value);

                // Obtener la información del empleado
                Periodo periodo = PeriodoDAO.ObtenerPeriodoporID(selectedID);

                if (periodo != null)
                {
                    dateTimePicker1.Value = periodo.FInicial;                    
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del empleado.");
                }
            }
        }
    }
}
