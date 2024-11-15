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
    public partial class EliminaciónAjuste : Form
    {
        public int IdEmp { get; set; }

        
        public EliminaciónAjuste()
        {
            InitializeComponent();
            InicializarDataGridView();
        }
        public void LlenarDataGridView(int idEmp)
        {
            List<AjustesEmpleadoPeriodo> ajustesEmpleado = AjustesEmpleadoPeriodoDAO.ObtenerAjustesPorEmpleado(idEmp);

            foreach (var ajuste in ajustesEmpleado)
            {
                // Obtener Motivo del ajuste
                Ajuste ajusteInfo = AjusteDAO.ObtenerAjustePorId(ajuste.IdAjuste);
                string motivo = ajusteInfo != null ? ajusteInfo.Motivo : "";

                // Obtener las fechas del periodo
                Periodo periodoInfo = PeriodoDAO.ObtenerPeriodoporID(ajuste.IdPeriodo);
                DateTime? fInicio = periodoInfo?.FInicial;
                DateTime? fFin = periodoInfo?.FFin;

                // Agregar al DataGridView
                dataGridView1.Rows.Add(
                    ajuste.IdAjuste,
                    motivo,
                    ajuste.IdEmp,
                    ajuste.IdPeriodo,
                    fInicio.HasValue ? fInicio.Value.ToString("yyyy-MM-dd") : "",
                    fFin.HasValue ? fFin.Value.ToString("yyyy-MM-dd") : "",
                    ajuste.DiasHorasIMSS
                );
            }
        }

        private void EliminaciónAjuste_Load(object sender, EventArgs e)
        {
            LlenarDataGridView(IdEmp);
        }

        private void InicializarDataGridView()
        {
            // Configuración de columnas
            dataGridView1.Columns.Add("ID_AJUSTE", "ID_AJUSTE");
            dataGridView1.Columns.Add("Motivo", "Motivo");
            dataGridView1.Columns.Add("ID_EMP", "ID_EMP");
            dataGridView1.Columns.Add("ID_PERIODO", "ID_PERIODO");
            dataGridView1.Columns.Add("F_Inicio", "F_Inicio");
            dataGridView1.Columns.Add("F_Final", "F_Final");
            dataGridView1.Columns.Add("DiasHorasIMSS", "DiasHorasIMSS");

            // Configura el ancho de las columnas si es necesario
            dataGridView1.Columns["ID_AJUSTE"].Width = 100;
            dataGridView1.Columns["Motivo"].Width = 150;
            dataGridView1.Columns["ID_EMP"].Width = 100;
            dataGridView1.Columns["ID_PERIODO"].Width = 100;
            dataGridView1.Columns["F_Inicio"].Width = 100;
            dataGridView1.Columns["F_Final"].Width = 100;
            dataGridView1.Columns["DiasHorasIMSS"].Width = 100;

            // Opcional: Configura si las columnas son solo de lectura
            dataGridView1.Columns["ID_AJUSTE"].ReadOnly = true;
            dataGridView1.Columns["Motivo"].ReadOnly = true;
            dataGridView1.Columns["ID_EMP"].ReadOnly = true;
            dataGridView1.Columns["ID_PERIODO"].ReadOnly = true;
            dataGridView1.Columns["F_Inicio"].ReadOnly = true;
            dataGridView1.Columns["F_Final"].ReadOnly = true;
            dataGridView1.Columns["DiasHorasIMSS"].ReadOnly = true;
        }

        private int idemp, idper, idajs;
        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar que los ID sean válidos
            if (idajs > 0 && idemp > 0 && idper > 0)
            {
                // Mostrar un cuadro de diálogo de confirmación
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este ajuste?", "Confirmación de eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool exito = AjustesEmpleadoPeriodoDAO.EliminarAjusteEmpleado(idajs, idemp, idper);

                    if (exito)
                    {
                        MessageBox.Show("Ajuste eliminado correctamente.");
                        dataGridView1.Rows.Clear();
                        // Opcional: Recargar el dataGridView1 después de eliminar
                        LlenarDataGridView(IdEmp);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el ajuste.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un ajuste válido antes de intentar eliminarlo.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se haya seleccionado una fila válida
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Asignar los valores de las celdas a las variables correspondientes
                idajs = Convert.ToInt32(row.Cells["ID_AJUSTE"].Value);
                idemp = Convert.ToInt32(row.Cells["ID_EMP"].Value);
                idper = Convert.ToInt32(row.Cells["ID_PERIODO"].Value);
            }
        }

    }
}
