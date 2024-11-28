using MAD.DAO;
using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAD
{
    public partial class Ajustes : Cabecera

    {
        public Ajustes()
        {
            InitializeComponent();
        }
        private int idemp;
        private int idper = Cabecera.idperiodo;

        private void Ajustes_Load(object sender, EventArgs e)
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
            dataGridView1.Columns["Empleado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void RellenarDataGridView()
        {
            Periodo periodo = PeriodoDAO.ObtenerPeriodoporID(Cabecera.idperiodo);
            label6.Text = $"{periodo.FInicial:dd/MM/yyyy} - {periodo.FFin:dd/MM/yyyy}";
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                rellenarcombobox(1);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                rellenarcombobox(2);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                rellenarcombobox(3);
            }
        }

        private void rellenarcombobox(int i)
        {
            comboBox1.Items.Clear();
            textBox1.Enabled = false;
            textBox1.Text = string.Empty;
            comboBox1.Text= string.Empty;
            List<Ajuste> ajustes;

            switch (i)
            {
                case 1: // Ajustes Generales (Percepcion)
                    ajustes = AjusteDAO.ObtenerAjustesPorTipo("Percepción");
                    break;
                case 2: // Ajustes Extraordinarios
                    ajustes = AjusteDAO.ObtenerAjustesPorTipo("Extraordinario");
                    break;
                case 3: // Ajustes de Incidencia
                    ajustes = AjusteDAO.ObtenerAjustesPorTipo("Incidencia");
                    break;
                default:
                    ajustes = new List<Ajuste>();
                    break;
            }

            foreach (var ajuste in ajustes)
            {
                if (ajuste.Motivo!="ISR"&& ajuste.Motivo != "Bono de Asistencia"&& ajuste.Motivo != "Puntualidad" && ajuste.Motivo != "Despensa"
                    && ajuste.Motivo != "IMSS" && ajuste.Motivo != "Infonavit" && ajuste.Motivo != "Fondo de ahorro" )
                {
                    comboBox1.Items.Add(ajuste.Motivo); // Puedes personalizar la forma en que se muestran
                }                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int selectedID = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                idemp = selectedID;               
            }
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(comboBox1.Text == "Faltas"|| comboBox1.Text == "Horas Extra" || comboBox1.Text == "Retardo")
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Text = string.Empty;
                textBox1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PeriodoDAO.ObtenerPeriodoActual().IdPeriodo != Cabecera.idperiodo)
            {
                MessageBox.Show("No puedes eliminar en un periodo cerrado");
                return;
            }
            EliminaciónAjuste newWindow = new EliminaciónAjuste();
            newWindow.IdEmp = idemp;
            newWindow.ShowDialog();
        }//Eliminar Ajuste
        private void button1_Click(object sender, EventArgs e) // Agregar Ajuste
        {
            if (!Validaciones()) return;

            string motivo = comboBox1.Text;
            int idAjuste = AjusteDAO.BuscarIdAjustePorMotivo(motivo);
            long? diasHorasIMSS = ObtenerDiasHorasIMSS();

            AjustesEmpleadoPeriodo ajusteEmpleadoPeriodo = new AjustesEmpleadoPeriodo
            {
                IdAjuste = idAjuste,
                IdEmp = idemp,
                IdPeriodo = idper,
                DiasHorasIMSS = diasHorasIMSS
            };

            if (AjustesEmpleadoPeriodoDAO.InsertarAjusteEmpleadoPeriodo(ajusteEmpleadoPeriodo) > 0)
            {
                Mensaje("Ajuste agregado exitosamente.");
            }
            else
            {
                Mensaje("Ocurrió un error al agregar el ajuste.");
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private bool Validaciones()
        {
            if (PeriodoDAO.ObtenerPeriodoActual().IdPeriodo != Cabecera.idperiodo)
            {
                Mensaje("No se puede modificar si el periodo esta cerrado");
                return false;
            }
            string motivo = comboBox1.Text;
            if (AjusteDAO.BuscarIdAjustePorMotivo(motivo) < 0)
            {
                Mensaje("No se encontró el ID_AJUSTE para el motivo especificado.");
                return false;
            }
            if (AjustesEmpleadoPeriodoDAO.VerificarRepetidos(idemp, AjusteDAO.BuscarIdAjustePorMotivo(motivo), idper))
            {
                Mensaje("Ya existe un registro con estos datos.");
                return false;
            }            
            if (idemp <= 0)
            {
                Mensaje("Selecciona un empleado o periodo válido.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                Mensaje("Selecciona un ajuste válido.");
                return false;
            }
            if (comboBox1.Text == "Faltas" && string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Mensaje("Agrega los días de falta.");
                return false;
            }
            return true;
        }
        private long? ObtenerDiasHorasIMSS()
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && long.TryParse(textBox1.Text, out long parsedValue))
            {
                return parsedValue;
            }
            return 0;
        }
        private void Mensaje(string message)
        {
            MessageBox.Show(message);
        }

    }
}
