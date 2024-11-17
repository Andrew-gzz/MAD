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

        public int periodoseleccionado { get; set; }
        private decimal TotalPercepciones = 0;
        private decimal TotalDeducciones = 0;
        private decimal NetoAPagar = 0;

        public ReporteNómina()
        {
            InitializeComponent();
        }

        private void ReporteNómina_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            RellenarDataGridView();
            FillDataCorp();
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
            //Percepciones
            dataGridView2.Columns.Add("Concepto", "Concepto");
            dataGridView2.Columns.Add("Valor", "Valor");
            dataGridView2.Columns.Add("Importe", "Importe");
            //Deducciones
            dataGridView3.Columns.Add("Concepto", "Concepto");
            dataGridView3.Columns.Add("Valor", "Valor");
            dataGridView3.Columns.Add("Importe", "Importe");
            //Extraordinarios
            dataGridView4.Columns.Add("Concepto", "Concepto");
            dataGridView4.Columns.Add("Valor", "Valor");
            dataGridView4.Columns.Add("Importe", "Importe");
            //Incidencias
            dataGridView5.Columns.Add("Concepto", "Concepto");
            dataGridView5.Columns.Add("Valor", "Valor");
            dataGridView5.Columns.Add("Importe", "Importe");
            //Total a pagar
            dataGridView6.Columns.Add("", "");
            dataGridView6.Columns.Add("Cantidad", "Cantidad");
        }
        //Rellenar empleados
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
        //Selecciona al empleado
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
                    SelecPeriodo newWindow = new SelecPeriodo(this);
                    newWindow.idempleado = empleado.IdEmpleado;
                    newWindow.ShowDialog();
                    if (periodoseleccionado != 0)
                    {
                        MessageBox.Show($"Periodo seleccionado: {periodoseleccionado}");
                        label12.Text = empleado.IdEmpleado.ToString();
                        label13.Text = empleado.Nombre;
                        label23.Text = empleado.Rfc;
                        label25.Text = empleado.Curp;
                        label26.Text = empleado.FechaDeIngreso.ToString("dd/MM/yyyy");
                        label27.Text = TurnosDAO.ObtenerTipoTurnoPorId(empleado.IdTurno);
                        label24.Text = empleado.Imss.ToString();
                        label21.Text = empleado.Direccion;
                        Periodo periodo = PeriodoDAO.ObtenerPeriodoporID(periodoseleccionado);
                        if (periodo != null)
                        {
                            label40.Text = $"{periodo.FInicial:dd/MM/yyyy} - {periodo.FFin:dd/MM/yyyy}";
                            label38.Text =$" {periodo.FFin:dd/MM/yyyy}";
                        }
                        label37.Text = "30.41";
                        label39.Text = departamentoDAO.ObtenerNombreDepartamentoPorId(empleado.IdDep);
                        label36.Text = puestosDAO.ObtenerNombrePuestoPorId(empleado.IdPuesto);
                        Empresa empresa = EmpresaDAO.ObtenerEmpresaPorId(1);
                        if(empresa != null)
                        {
                            label35.Text = empresa.Re_Fiscal;
                        }
                        RellenarPercepciones(empleado.SueldoMensual, empleado);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del empleado.");
                }
            }
        }
        //Rellena datos de la empresa
        private void FillDataCorp()
        {
            Empresa empresa = EmpresaDAO.ObtenerEmpresaPorId(1);
            if (empresa != null)
            {
                label1.Text = empresa.Ra_S;
                label7.Text = empresa.RFC;
                label8.Text = empresa.RepLegal;
                label9.Text = empresa.Id_RP.ToString();
                label10.Text = empresa.Direccion;
                label11.Text = empresa.Re_Fiscal;
            }
        }
        private void RellenarPercepciones(decimal sueldoMensual, Empleado empleado)
        {
            try
            {
                // Limpiar las filas existentes
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                dataGridView4.Rows.Clear();
                dataGridView5.Rows.Clear();
                dataGridView6.Rows.Clear();
                // Obtener los ajustes de tipo "Percepción"
                List<Ajuste> ajustes = AjusteDAO.ObtenerAjustesObligatorios();
                dataGridView2.Rows.Add("Sueldo", " ", sueldoMensual.ToString("C2"));
                TotalPercepciones = sueldoMensual; //Suma
                // Recorrer los ajustes y calcular el importe
                foreach (var ajuste in ajustes)
                {
                    if(ajuste.Tipo == "Percepción")
                    {
                        decimal importe = sueldoMensual * (ajuste.Porcentaje / 100);
                        // Agregar una nueva fila al DataGridView
                        dataGridView2.Rows.Add(ajuste.Motivo, $"{ajuste.Porcentaje.ToString()}%", importe.ToString("C2"));
                        TotalPercepciones += importe;
                    }
                    else
                    {
                        decimal importe = sueldoMensual * (ajuste.Porcentaje / 100);
                        // Agregar una nueva fila al DataGridView
                        dataGridView3.Rows.Add(ajuste.Motivo, $"{ajuste.Porcentaje.ToString()}%", importe.ToString("C2"));
                        TotalDeducciones+= importe;
                    }                    
                }
                //calculos generales               
                decimal porcentajeISR = ObtenerPorcentajeISR(empleado.ID_ISR);
                decimal isr = 0;
                List<ISR> listaISR = ISRDAO.ObtenerISRPorid(empleado.ID_ISR);
                foreach (var ajuste in listaISR)
                {
                    isr = empleado.SueldoMensual - ajuste.L_Inferior;
                    isr = isr * porcentajeISR;
                    isr = isr + ajuste.Cuota;
                }
                TotalDeducciones += isr;
                dataGridView3.Rows.Add("I.S.R", $"{porcentajeISR:P2}", isr.ToString("C2"));
                decimal fondoAhorro = sueldoMensual < 10000 ? 500 : 1000;
                TotalDeducciones += fondoAhorro;
                dataGridView3.Rows.Add("Fondo de ahorro", " ", fondoAhorro.ToString("C2"));
                //Optener ajustes externos
                ObtenerAjustes(empleado);
                NetoAPagar = TotalPercepciones - TotalDeducciones;
                dataGridView6.Rows.Add("Total de Percepciones:",TotalPercepciones.ToString("C2"));
                dataGridView6.Rows.Add("Total de Deducciones:", TotalDeducciones.ToString("C2"));
                dataGridView6.Rows.Add("Total a Pagar:", NetoAPagar.ToString("C2"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al llenar los ajustes: {ex.Message}");
            }
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
        private void ObtenerAjustes(Empleado empleado)
        {
            // Obtener los ajustes para el empleado y periodo seleccionados
            List<AjustesEmpleadoPeriodo> ajustes = AjustesEmpleadoPeriodoDAO.ObtAjustPorEmpleadoPeriodo(empleado.IdEmpleado, periodoseleccionado);
            decimal importe;
            foreach (var ajuste in ajustes)
            {
                // Obtener el nombre del motivo del ajuste por su ID
                Ajuste ajusteDetalle = AjusteDAO.NombrePorId(ajuste.IdAjuste);
                if (ajusteDetalle != null) // Validar que el ajuste no sea nulo
                {
                    string nombre = ajusteDetalle.Motivo;
                    switch (nombre)
                    {
                        case "Productividad":
                            importe = empleado.SueldoMensual *0.11m;
                            dataGridView2.Rows.Add(ajusteDetalle.Motivo, "11%", importe.ToString("C2"));//Percepciones
                            TotalPercepciones += importe;
                            break;

                        case "Vacaciones":
                            importe = empleado.SalarioDiario * VacacionesDAO.ObtenerDiasVacaciones(empleado.Antiguedad);
                            dataGridView2.Rows.Add(ajusteDetalle.Motivo, " ", importe.ToString("C2"));//Percepciones
                            TotalPercepciones += importe;
                            break;

                        case "Prima Vacacional":
                            importe = (VacacionesDAO.ObtenerDiasVacaciones(empleado.Antiguedad) * .35m) * empleado.SalarioDiario;
                            dataGridView2.Rows.Add(ajusteDetalle.Motivo, " ", importe.ToString("C2"));//Percepciones
                            TotalPercepciones += importe;
                            break;

                        case "Prestamo Empresa":
                            if (ajusteDetalle.Tipo == "Percepción")
                            {
                                dataGridView2.Rows.Add(ajusteDetalle.Motivo, " ", "3000");//Percepciones
                            }
                            else
                            {
                                dataGridView3.Rows.Add(ajusteDetalle.Motivo, " ", "1000");//Deduccion
                            }
                            break;

                        case "Aguinaldo":
                            importe = empleado.SalarioDiario * 18;
                            dataGridView4.Rows.Add(ajusteDetalle.Motivo, "18xSD", importe.ToString("C2"));//Percepciones extraordinario                            
                            TotalPercepciones += importe;
                            break;

                        case "Horas Extra":
                            importe = (empleado.SalarioDiario / 8) * 2;
                            dataGridView4.Rows.Add(ajusteDetalle.Motivo, "(SD/8)*2", importe.ToString("C2"));//Percepciones extraordinario 
                            TotalPercepciones += importe;
                            break;

                        case "Pension Alimenticia":
                            MessageBox.Show("Pension Alimenticia");
                            break;

                        case "Retardo":
                            decimal diasHorasIMSS = ajuste.DiasHorasIMSS ?? 0m; // Asignar 0 si es nulo
                            importe = (empleado.SalarioDiario / 8m) * diasHorasIMSS;
                            dataGridView4.Rows.Add(ajusteDetalle.Motivo, "(SD/8)*Horas", importe.ToString("C2"));//Percepciones extraordinario
                            TotalDeducciones += importe;                                                                                                                 
                            break;

                        case "Enfermedad General":
                            dataGridView5.Rows.Add(ajusteDetalle.Motivo, "IMSS","");//Incidencias
                            break;

                        case "Faltas":
                            diasHorasIMSS = ajuste.DiasHorasIMSS ?? 0m; // Asignar 0 si es nulo
                            importe = empleado.SalarioDiario * diasHorasIMSS;
                            dataGridView4.Rows.Add(ajusteDetalle.Motivo, "SD*Dias", importe.ToString("C2"));//Percepciones Incidencias 
                            TotalDeducciones += importe;
                            break;

                        case "Maternidad":
                            dataGridView5.Rows.Add(ajusteDetalle.Motivo, "IMSS","");//Incidencias
                            break;

                        case "Accidente trabajo":
                            dataGridView5.Rows.Add(ajusteDetalle.Motivo, "IMSS","");//Incidencias
                            break;

                        default:
                            MessageBox.Show($"Ajuste desconocido: {nombre}");
                            break;
                    }

                }
                else
                {
                    MessageBox.Show($"No se encontró el motivo para el ajuste con ID: {ajuste.IdAjuste}");
                }
            }
        }

    }
}
