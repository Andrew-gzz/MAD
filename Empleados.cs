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

namespace MAD
{
    public partial class Empleados : Cabecera
    {
        public Empleados()
        {
            InitializeComponent();
        }

        private void Empleados_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            RellenarDataGridView();
            RellenarComboBoxes();
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
        private void RellenarDataGridView() {
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
        private void RellenarComboBoxes()
        {
            // Limpiar ComboBoxes
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            // Llenar comboBox1 con los puestos
            List<puestos> listaPuestos = puestosDAO.ObtenerPuestos();
            foreach (puestos puesto in listaPuestos)
            {
                comboBox1.Items.Add(new { Text = puesto.Puesto, Value = puesto.IdPuesto });
            }
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            // Llenar comboBox2 con los departamentos
            List<departamentos> listaDepartamentos = departamentoDAO.ObtenerDepartamentos();
            foreach (departamentos departamento in listaDepartamentos)
            {
                comboBox2.Items.Add(new { Text = departamento.Departamento, Value = departamento.IdDep });
            }
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";

            // Llenar comboBox3 con los turnos
            List<Turnos> listaTurnos = TurnosDAO.ObtenerTurnos();
            foreach (Turnos turno in listaTurnos)
            {
                comboBox3.Items.Add(new { Text = turno.Tipo, Value = turno.IdTurno });
            }
            comboBox3.DisplayMember = "Text";
            comboBox3.ValueMember = "Value";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int selectedID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                // Obtener la información del empleado
                Empleado empleado = EmpleadoDAO.ObtenerEmpleadoPorId(selectedID);

                if (empleado != null)
                {
                    // Llenar los campos de la interfaz con la información del empleado
                    textBox1.Text = empleado.IdEmpleado.ToString();
                    textBox3.Text = empleado.Imss.ToString();
                    textBox4.Text = empleado.Curp;
                    textBox2.Text = empleado.Nombre;
                    dateTimePicker1.Value = empleado.FechaNacimiento;
                    textBox5.Text = empleado.Correo;
                    radioButton1.Checked = empleado.Genero == "Masculino";
                    radioButton2.Checked = empleado.Genero == "Femenino";
                    textBox6.Text = empleado.Telefono.ToString();
                    textBox7.Text = empleado.Rfc;
                    textBox8.Text = empleado.Direccion;
                    textBox9.Text = empleado.SalarioDiario.ToString("F2");
                    textBox10.Text = empleado.SueldoMensual.ToString("F2");
                    textBox11.Text = empleado.SalarioDiarioIntegrado.ToString("F2");
                    dateTimePicker2.Value = empleado.FechaDeIngreso;
                    // Obtener los nombres a partir de los IDs
                    string puesto = puestosDAO.ObtenerNombrePuestoPorId(empleado.IdPuesto);
                    comboBox1.Text = puesto;
                    string departamento = departamentoDAO.ObtenerNombreDepartamentoPorId(empleado.IdDep);
                    comboBox2.Text = departamento;
                    string turno = TurnosDAO.ObtenerTipoTurnoPorId(empleado.IdTurno);
                    comboBox3.Text = turno;
                    if (empleado.Estatus)
                    {
                        radioButton3.Checked = true;
                        radioButton4.Checked = false;
                    }
                    else
                    {
                        radioButton4.Checked = true;
                        radioButton3.Checked = false;
                    }
                    textBox12.Text = empleado.ID_ISR.ToString();
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del empleado.");
                }
            }
        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox9.Text))
            {
                // Intentar convertir el texto a un valor double
                if (double.TryParse(textBox9.Text, out double SD))
                {
                    textBox10.Text = (SD * 30.41).ToString("F2"); // Salario mensual
                    textBox11.Text = (SD * 1.0493).ToString("F2"); // Salario Diario Integrado

                    // Obtener el salario mensual convertido a decimal para el cálculo de ISR
                    decimal salarioMensual = Convert.ToDecimal(textBox10.Text);

                    // Obtener el año actual para la consulta de ISR (o puedes especificar otro año)
                    int anioActual = DateTime.Now.Year;

                    // Obtener el ISR aplicable
                    ISR isrAplicable = ISRDAO.ObtenerISRPorSalario(salarioMensual, anioActual);

                    if (isrAplicable != null)
                    {
                        // Mostrar el ID del ISR en el textBox11
                        textBox12.Text = isrAplicable.ID_ISR.ToString();
                    }
                    else
                    {
                        // En caso de que no se encuentre un ISR aplicable
                        MessageBox.Show("No se encontró un ISR aplicable para el salario mensual ingresado.");
                    }
                }
            }
        }

    }
}

