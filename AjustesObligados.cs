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
    public partial class AjustesObligados : Cabecera
    {
        public AjustesObligados()
        {
            InitializeComponent();
        }
        private void CalculoNomina_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            RellenarDataGridView();
        }
        private void ConfigurarDataGridView()
        {
            // Limpiar columnas anteriores si existen
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
            // Agregar las columnas "Empleado" y "ID"           
            dataGridView1.Columns.Add("Empleado", "Empleado");
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns["Empleado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Agregar columnas de Percepciones
            dataGridView2.Columns.Add("ConceptoPercepcion", "Concepto");
            dataGridView2.Columns.Add("ValorPercepcion", "Valor");
            dataGridView2.Columns.Add("ImportePercepcion", "Importe");

            // Agregar columnas de Deducciones
            dataGridView2.Columns.Add("ConceptoDeduccion", "Concepto");
            dataGridView2.Columns.Add("ValorDeduccion", "Valor");
            dataGridView2.Columns.Add("ImporteDeduccion", "Importe");

            // Ajustar el ancho de las columnas
            dataGridView2.Columns["ConceptoPercepcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["ValorPercepcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView2.Columns["ImportePercepcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView2.Columns["ConceptoDeduccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["ValorDeduccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView2.Columns["ImporteDeduccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

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
                    if (!empleado.Estatus) {
                        MessageBox.Show("El empleado esta Inactivo");
                    } 
                    // Llenar los campos de la interfaz con la información del empleado
                    textBox1.Text = empleado.IdEmpleado.ToString();
                    textBox2.Text = empleado.Nombre;
                    textBox5.Text = empleado.SalarioDiario.ToString("F2");
                    string departamento = departamentoDAO.ObtenerNombreDepartamentoPorId(empleado.IdDep);
                    textBox3.Text = departamento;
                    string puesto = puestosDAO.ObtenerNombrePuestoPorId(empleado.IdPuesto);
                    textBox4.Text = puesto;
                    LlenarDataGridViewPercepcionesDeducciones(empleado);
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del empleado.");
                }
            }
        }
        private void LlenarDataGridViewPercepcionesDeducciones(Empleado empleado)
        {
            dataGridView2.Rows.Clear();

            // Cálculo de Percepciones
            decimal sueldo = empleado.SalarioDiario * 30.41m;
            decimal bonoAsistencia = sueldo * 0.07m;
            decimal puntualidad = sueldo * 0.05m;
            decimal despensa = sueldo * 0.15m;

            // Cálculo del total de percepciones
            decimal sumaPercepciones = sueldo + bonoAsistencia + puntualidad + despensa;

            // Cálculo de Deducciones
            decimal porcentajeISR = ObtenerPorcentajeISR(empleado.ID_ISR);
            decimal isr = sueldo * porcentajeISR;
            decimal imss = empleado.SalarioDiarioIntegrado * 0.05m;
            decimal prestamoInfo = empleado.SalarioDiarioIntegrado * 0.15m;
            decimal fondoAhorro = sueldo < 10000 ? 500 : 1000;

            // Cálculo del total de deducciones
            decimal sumaDeducciones = isr + imss + prestamoInfo + fondoAhorro;

            // Calcular el neto a pagar
            decimal netoAPagar = sumaPercepciones - sumaDeducciones;

            // Agregar filas combinadas de Percepciones y Deducciones
            dataGridView2.Rows.Add("Sueldo", "", sueldo.ToString("C2"), "I.S.R", $"{porcentajeISR:P2}", isr.ToString("C2"));
            dataGridView2.Rows.Add("Bono de Asistencia", "7%", bonoAsistencia.ToString("C2"), "I.M.S.S", "5%", imss.ToString("C2"));
            dataGridView2.Rows.Add("Puntualidad", "5%", puntualidad.ToString("C2"), "Prestamo Info", "15%", prestamoInfo.ToString("C2"));
            dataGridView2.Rows.Add("Despensa", "15%", despensa.ToString("C2"), "Fondo Ahorro", "", fondoAhorro.ToString("C2"));

            // Agregar fila de totales al final
            dataGridView2.Rows.Add("Suma de percepciones", "", sumaPercepciones.ToString("C2"), "Suma de deducciones", "", sumaDeducciones.ToString("C2"));
            dataGridView2.Rows.Add("Neto a pagar", "", netoAPagar.ToString("C2"), "", "", "");
        }
        private decimal ObtenerPorcentajeISR(int empleadoId)
        {
            // Llama al método ObtenerISRPorid para obtener la lista de registros ISR asociados al ID
            List<ISR> listaISR = ISRDAO.ObtenerISRPorid(empleadoId);

            // Si hay registros de ISR, devuelve el primer porcentaje encontrado (ajustar si se requiere lógica adicional)
            if (listaISR.Count > 0)
            {
                return listaISR[0].Porcentaje / 100; // Dividir entre 100 para obtener el valor decimal del porcentaje
            }
            else
            {
                MessageBox.Show("No se encontró el registro de ISR para el empleado.");
                return 0; // Retorna 0 si no se encuentra el ISR para evitar errores de cálculo
            }
        }

    }
}
