using MAD.DAO;
using MAD.DAO.MAD.DAO;
using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAD
{
    public partial class Empleados : Cabecera
    {
        public DateTime Fecha_Baja { get; set; }
        private DateTime Zucaritas = DateTime.MinValue;
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
                    Zucaritas = empleado.FechaDeIngreso;
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
        private void button1_Click(object sender, EventArgs e) //Agregar empleado
        {

            if (Validaciones(false))
            {
                
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    int i = Movimientos_EmpleadosDAO.ObtenerIdMovimientoActivo(int.Parse(textBox1.Text));
                    DialogResult result = MessageBox.Show("El empleado está de baja. ¿Desea darlo de alta de nuevo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Empleado empleado = EmpleadoDAO.ObtenerEmpleadoPorId(int.Parse(textBox1.Text));//Validar
                        DateTime UltimaBaja = Movimientos_EmpleadosDAO.ObtenerUltimaFechaBaja(empleado.IdEmpleado);
                        if (dateTimePicker2.Value > UltimaBaja)
                        {
                            Movimientos_Empleados nuevoMovimiento = new Movimientos_Empleados
                            {
                                ID_EMPLEADO = int.Parse(textBox1.Text),
                                F_ALTA = dateTimePicker2.Value,
                                ID_PERIODO_ALTA = PeriodoDAO.ObtIdPerPorF_Ingreso(dateTimePicker2.Value)
                            };
                            Movimientos_EmpleadosDAO.InsertarMovimiento(nuevoMovimiento);
                            empleado.FechaDeIngreso = dateTimePicker2.Value;
                            empleado.Estatus = true;
                            EmpleadoDAO.ReingresoEmpleado(empleado);
                            MessageBox.Show("Empleado a sido dado de alta");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("No puedes darlo de alta fechas antes de su ultima baja");
                            return;
                        }
                    }
                    return;
                }

                // Llama a la función para obtener los datos del empleado
                Empleado nuevoEmpleado = ObtenerDatosEmpleado();

                // Validación de duplicados
                if (!EmpleadoDAO.EsDatoUnico(nuevoEmpleado.Imss, nuevoEmpleado.Curp, nuevoEmpleado.Telefono, nuevoEmpleado.Rfc))
                {
                    MessageBox.Show("IMSS, CURP, Teléfono o RFC ya están registrados en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir si hay duplicados
                }

                // Inserta al empleado y obtiene su ID
                int resultadoEmpleado = EmpleadoDAO.InsertarEmpleado(nuevoEmpleado);

                if (resultadoEmpleado > 0)
                {
                    // Busca el ID del empleado por su CURP
                    int idEmpleado = EmpleadoDAO.ObtIdPorCURP(nuevoEmpleado.Curp);

                    // Busca el ID del periodo por su fecha de ingreso
                    int idPeriodo = PeriodoDAO.ObtIdPerPorF_Ingreso(nuevoEmpleado.FechaDeIngreso);

                    // Crea el objeto de movimiento
                    Movimientos_Empleados nuevoMovimiento = new Movimientos_Empleados
                    {
                        ID_EMPLEADO = idEmpleado,
                        F_ALTA = nuevoEmpleado.FechaDeIngreso,
                        ID_PERIODO_ALTA = idPeriodo
                    };

                    // Inserta el movimiento
                    int resultadoMovimiento = Movimientos_EmpleadosDAO.InsertarMovimiento(nuevoMovimiento);

                    if (resultadoMovimiento > 0)
                    {
                        MessageBox.Show("Empleado y movimiento agregados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Empleado agregado, pero ocurrió un error al registrar el movimiento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al agregar el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)//Modificar empleado
        {
            if (Validaciones(true))
            {
                if (Zucaritas != dateTimePicker2.Value) {
                    MessageBox.Show("Por politicas no puedes modificar la fecha de ingreso");
                    return;
                }
                int i = Movimientos_EmpleadosDAO.ObtenerIdMovimientoActivo(int.Parse(textBox1.Text));
                if (i < 0)
                {
                    MostrarMensajeValidacion("El usuario esta dado de baja, no puedes modificar datos");
                    return;
                }
                // Llama a la función para obtener los datos del empleado
                Empleado nuevoEmpleado = ObtenerDatosEmpleado();
                // Validación de duplicados
                if (!EmpleadoDAO.EsDatoUnico(nuevoEmpleado.Imss, nuevoEmpleado.Curp, nuevoEmpleado.Telefono, nuevoEmpleado.Rfc, int.Parse(textBox1.Text)))
                {
                    MessageBox.Show("IMSS, CURP, Teléfono o RFC ya están registrados en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir si hay duplicados
                }
                // Decide si es una inserción o una actualización              
                EmpleadoDAO.ActualizarEmpleado(nuevoEmpleado); // Método para actualizar el empleado
                int idmov = Movimientos_EmpleadosDAO.ObtenerIdMovimientoActivo(nuevoEmpleado.IdEmpleado);
                int idperiodo = PeriodoDAO.ObtIdPerPorF_Ingreso(nuevoEmpleado.FechaDeIngreso);
                Movimientos_EmpleadosDAO.ActualizarMov(nuevoEmpleado.FechaDeIngreso, idperiodo, idmov);
                MessageBox.Show("Empleado actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button3_Click(object sender, EventArgs e)//Dar de baja
        {
            try
            {
                if (Validaciones(false))
                {
                    int i = Movimientos_EmpleadosDAO.ObtenerIdMovimientoActivo(int.Parse(textBox1.Text));
                    if (i < 0)
                    {
                        MostrarMensajeValidacion("El usuario ya esta dado de baja");
                        return;
                    }
                    //Optencion de datos
                    BajaEmpleado newWindow = new BajaEmpleado(this);
                    newWindow.ShowDialog();
                    if (Fecha_Baja != DateTime.MinValue)//Tipo DateTime verificar si no esta vacio
                    {
                        int id_per = PeriodoDAO.ObtIdPerPorF_Ingreso(Fecha_Baja);
                        int id_mov = Movimientos_EmpleadosDAO.ObtenerIdMovimientoActivo(int.Parse(textBox1.Text));
                        //Updates            
                        int update1 = EmpleadoDAO.BajaEmpleado(int.Parse(textBox1.Text));
                        int update2 = Movimientos_EmpleadosDAO.BajaEmpleado(Fecha_Baja, id_per, id_mov);
                    }
                }               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private Empleado ObtenerDatosEmpleado()
        {
            string name = textBox2.Text; // Nombre
            long imss = long.Parse(textBox3.Text); // IMSS
            string curp = textBox4.Text; // CURP
            DateTime f_nac = dateTimePicker1.Value; // Fecha de Nacimiento
            string correo = textBox5.Text; // Correo
            string genero = radioButton1.Checked ? radioButton1.Text : radioButton2.Text; // Género
            long telefono = long.Parse(textBox6.Text); // Teléfono
            string rfc = textBox7.Text; // RFC
            string domicilio = textBox8.Text; // Dirección
            decimal sd = decimal.Parse(textBox9.Text); // Salario Diario
            decimal sm = decimal.Parse(textBox10.Text); // Sueldo Mensual
            decimal sdi = decimal.Parse(textBox11.Text); // Salario Diario Integrado
            DateTime f_ingreso = dateTimePicker2.Value; // Fecha de Ingreso
            DateTime f_actual = DateTime.Now;

            // Calcular antigüedad
            int antiguedad = f_actual.Year - f_ingreso.Year;
            if (f_actual.Month < f_ingreso.Month || (f_actual.Month == f_ingreso.Month && f_actual.Day < f_ingreso.Day))
            {
                antiguedad--; // Resta 1 año si no se cumplió aún
            }
            antiguedad = Math.Max(0, antiguedad);

            int idpuesto = puestosDAO.ObtIdPorPuesto(comboBox1.Text); // ID_PUESTO
            int iddep = departamentoDAO.ObtIdPorDep(comboBox2.Text); // ID_DEP
            int idturno = TurnosDAO.ObtIdPorTipo(comboBox3.Text); // ID_TURNO
            bool estatus = true;
            int idisr = int.Parse(textBox12.Text); // ID_ISR

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                // Crear el objeto Empleado
                return new Empleado
                {
                    IdEmpleado = int.Parse(textBox1.Text),
                    Imss = imss,
                    Curp = curp,
                    Nombre = name,
                    FechaNacimiento = f_nac,
                    Correo = correo,
                    Genero = genero,
                    Telefono = telefono,
                    Rfc = rfc,
                    Direccion = domicilio,
                    SalarioDiario = sd,
                    SueldoMensual = sm,
                    SalarioDiarioIntegrado = sdi,
                    FechaDeIngreso = f_ingreso,
                    Antiguedad = antiguedad,
                    IdPuesto = idpuesto,
                    IdDep = iddep,
                    IdTurno = idturno,
                    Estatus = estatus,
                    ID_ISR = idisr
                };
            }
            else { 
                return new Empleado
                {                   
                    Imss = imss,
                    Curp = curp,
                    Nombre = name,
                    FechaNacimiento = f_nac,
                    Correo = correo,
                    Genero = genero,
                    Telefono = telefono,
                    Rfc = rfc,
                    Direccion = domicilio,
                    SalarioDiario = sd,
                    SueldoMensual = sm,
                    SalarioDiarioIntegrado = sdi,
                    FechaDeIngreso = f_ingreso,
                    Antiguedad = antiguedad,
                    IdPuesto = idpuesto,
                    IdDep = iddep,
                    IdTurno = idturno,
                    Estatus = estatus,
                    ID_ISR = idisr
                };
            }
        }
        private bool Validaciones(bool btnmod)
        {

            int PeriodoActual = PeriodoDAO.ObtenerPeriodoActual().IdPeriodo;
            if (PeriodoActual != Cabecera.idperiodo) {
                MessageBox.Show("No puedes modificar datos en periodos cerrados");
                return false;
            }
            // Validar TextBox
            if (!ValidarTextBox(textBox2, "Nombre")) return false;
            if (!ValidarTextBoxNumerico(textBox3, "IMSS")) return false;
            if (!ValidarTextBox(textBox4, "CURP")) return false;
            if (!ValidarTextBox(textBox5, "Correo")) return false;
            if (!ValidarTextBoxNumerico(textBox6, "Teléfono")) return false;
            if (!ValidarTextBox(textBox7, "RFC")) return false;
            if (!ValidarTextBox(textBox8, "Dirección")) return false;
            if (!ValidarTextBoxDecimal(textBox9, "Salario Diario")) return false;
            if (!ValidarTextBoxDecimal(textBox10, "Sueldo Mensual")) return false;
            if (!ValidarTextBoxDecimal(textBox11, "Salario Diario Integrado")) return false;
            if (!ValidarTextBoxNumerico(textBox12, "ID ISR")) return false;

            // Validar RadioButton
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MostrarMensajeValidacion("Debe seleccionar un género.");
                return false;
            }

            // Validar ComboBox
            if (!ValidarComboBox(comboBox1, "puesto")) return false;
            if (!ValidarComboBox(comboBox2, "departamento")) return false;
            if (!ValidarComboBox(comboBox3, "turno")) return false;

            // Validar DateTimePickers
            if (!ValidarDateTimePicker(dateTimePicker1)) return false;//fecha de nacimiento
            if (!btnmod)
            {
                if (!ValidarDateTimePicker(dateTimePicker2, "La fecha de ingreso no puede ser futura.")) return false;
            }
            //Validar si el empleado esta dado de baja
            return true;
        }
        private bool ValidarTextBox(TextBox textBox, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MostrarMensajeValidacion($"El campo '{nombreCampo}' no puede estar vacío.");
                return false;
            }
            return true;
        }
        private bool ValidarTextBoxNumerico(TextBox textBox, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) || !long.TryParse(textBox.Text, out _))
            {
                MostrarMensajeValidacion($"El campo '{nombreCampo}' debe tener un valor numérico válido.");
                return false;
            }
            return true;
        }
        private bool ValidarTextBoxDecimal(TextBox textBox, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) || !decimal.TryParse(textBox.Text, out _))
            {
                MostrarMensajeValidacion($"El campo '{nombreCampo}' debe tener un valor decimal válido.");
                return false;
            }
            return true;
        }
        private bool ValidarComboBox(ComboBox comboBox, string nombreCampo)
        {
            if (comboBox.SelectedIndex == -1 || string.IsNullOrWhiteSpace(comboBox.Text))
            {
                MostrarMensajeValidacion($"Debe seleccionar un {nombreCampo}.");
                return false;
            }
            return true;
        }
        private bool ValidarDateTimePicker(DateTimePicker dateTimePicker, string mensajeError)
        {
            Periodo PeriodoActual = PeriodoDAO.ObtenerPeriodoActual();
            
            if (dateTimePicker.Value.Date < PeriodoActual.FInicial.Date || dateTimePicker.Value.Date > PeriodoActual.FFin.Date)//Validar que se ingrese en el periodo actual
            {
                MostrarMensajeValidacion("La fecha de ingreso debe ser dentro del periodo actual");
                return false;
            }
            return true;
        }
        private bool ValidarDateTimePicker(DateTimePicker dateTimePicker)
        {
            DateTime Hoy = DateTime.Now;
            DateTime FechaNacimiento = dateTimePicker.Value;
            int Edad = Hoy.Year - FechaNacimiento.Year;

            if (FechaNacimiento > Hoy.AddYears(-Edad))
            {
                Edad--;
            }

            if (Edad < 18)
            {
                MostrarMensajeValidacion("Tiene que ser mayor de edad");
                return false;
            }

            return true;
        }
        private void MostrarMensajeValidacion(string mensaje)
        {
            MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }       
    }
}

