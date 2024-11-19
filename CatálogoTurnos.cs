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
    public partial class CatálogoTurnos : Cabecera  
    {
        public int periodoseleccionado { get; set; }
        private decimal PercepcionesGenerales = 0;//percepciones generales
        private decimal OtrasPercepciones = 0;//otras percepciones
        private decimal TotalPercepciones = 0;//total percepciones
        private decimal DeduccionesGenerales = 0;//deducciones obligadas
        private decimal OtrasDeducciones = 0;//otras deducciones
        private decimal TotalDeducciones = 0;//total deducciones
        private decimal NetoAPagar = 0; //Neto
        private decimal SumaTotal = 0;
        public CatálogoTurnos()
        {
            InitializeComponent();
        }

        private void FillDataGridView(int i, int f)
        {
            List<Turnos> ListaTurnos = TurnosDAO.ObtenerTurnos();
            foreach (var row in ListaTurnos)//Turnos
            {
                SumaTotal = 0;
                dataGridView1.Rows.Add("----------------------");
                dataGridView1.Rows.Add(row.Tipo);
                
                List<Empleado> ListaEmpleados = EmpleadoDAO.ObtEmpPorIdTurno(row.IdTurno);
                foreach (var row2 in ListaEmpleados)//Empleados
                {
                    dataGridView1.Rows.Add(row2.Nombre);
                    List<Periodo> ListaPeriodos = PeriodoDAO.ObtPeridosEnUnRango(i, f);//periodo de inicio(i),periodo final(f)
                    foreach (var row3 in ListaPeriodos)//Periodos
                    {
                        //reset a las sumas por empleado
                        PercepcionesGenerales = 0;
                        OtrasPercepciones = 0;
                        TotalPercepciones = 0;
                        DeduccionesGenerales = 0;
                        OtrasDeducciones = 0;
                        TotalDeducciones = 0;
                        NetoAPagar = 0;
                        //Para empleados que hayan reingresado alguna vez
                        List<Movimientos_Empleados> ListaPeriodosEmpleados = Movimientos_EmpleadosDAO.ObtPeriodosPorEmp(row2.IdEmpleado);
                        if (ListaPeriodosEmpleados != null)
                        {
                            foreach (var row4 in ListaPeriodosEmpleados)//Me da una lista de los periodos isla(tiene fecha de inicio y fecha final)Solo funciona con reingreso
                            {
                                if (row4.ID_PERIODO_ALTA <= row3.IdPeriodo && row3.IdPeriodo <= row4.ID_PERIODO_BAJA)
                                {
                                    CalcularTotales(row2, row3.IdPeriodo);
                                }
                            }
                        }
                        //Para empleados que nunca han estado de baja comparar desde su fecha de ingreso
                        int LastDate = Movimientos_EmpleadosDAO.ObtIdAlta(row2.IdEmpleado);
                        if (row3.IdPeriodo >= LastDate && row3.IdPeriodo <= f)//lugar de modificar
                        {
                            CalcularTotales(row2, row3.IdPeriodo);
                        }
                    }
                }
                dataGridView1.Rows.Add("Suma de total:", SumaTotal.ToString("C2"));
            }
        }
        private void ConfigDataGridView()
        {
            dataGridView1.Columns.Add("Periodo", "Periodo");
            dataGridView1.Columns.Add("Percepciones Generales", "Percepciones Generales");
            dataGridView1.Columns.Add("Otras Percepciones", "Otras Percepciones");
            dataGridView1.Columns.Add("Total Percepciones", "Total Percepciones");
            dataGridView1.Columns.Add("", "");
            dataGridView1.Columns.Add("Deducciones Generales", "Deducciones Generales");
            dataGridView1.Columns.Add("Otras Deducciones", "Otras Deducciones");
            dataGridView1.Columns.Add("Total Deducciones", "Total Deducciones");
            dataGridView1.Columns.Add("Neto", "Neto");
        }
        private void CalcularTotales(Empleado empleado, int periodo)
        {
            try
            {
                // Obtener los ajustes de tipo "Percepción"
                List<Ajuste> ajustes = AjusteDAO.ObtenerAjustesObligatorios();
                TotalPercepciones = empleado.SueldoMensual; //Suma
                PercepcionesGenerales = empleado.SueldoMensual;
                // Recorrer los ajustes y calcular el importe
                foreach (var ajuste in ajustes)
                {
                    if (ajuste.Tipo == "Percepción")
                    {
                        decimal importe = empleado.SueldoMensual * (ajuste.Porcentaje / 100);
                        PercepcionesGenerales += importe;
                        TotalPercepciones += importe;
                    }
                    else//Para deducciones generales
                    {
                        decimal importe = empleado.SueldoMensual * (ajuste.Porcentaje / 100);
                        DeduccionesGenerales += importe;
                        TotalDeducciones += importe;
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
                DeduccionesGenerales += isr;
                //
                decimal fondoAhorro = empleado.SueldoMensual < 10000 ? 500 : 1000;
                TotalDeducciones += fondoAhorro;
                DeduccionesGenerales += fondoAhorro;
                //Optener ajustes externos
                ObtenerAjustes(empleado, periodo);
                TotalPercepciones += OtrasPercepciones;
                TotalDeducciones += OtrasDeducciones;
                NetoAPagar = TotalPercepciones - TotalDeducciones;
                SumaTotal += NetoAPagar;
                dataGridView1.Rows.Add(periodo.ToString(), PercepcionesGenerales.ToString("C2"), OtrasPercepciones.ToString("C2"), TotalPercepciones.ToString("C2"), "",
                                       DeduccionesGenerales.ToString("C2"), OtrasDeducciones.ToString("C2"), TotalDeducciones.ToString("C2"), NetoAPagar.ToString("C2"));
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
        private void ObtenerAjustes(Empleado empleado, int periodo)
        {
            // Obtener los ajustes para el empleado y periodo seleccionados
            List<AjustesEmpleadoPeriodo> ajustes = AjustesEmpleadoPeriodoDAO.ObtAjustPorEmpleadoPeriodo(empleado.IdEmpleado, periodo);
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
                            importe = empleado.SueldoMensual * 0.11m;
                            OtrasPercepciones += importe;
                            break;

                        case "Vacaciones":

                            DateTime Hoy = DateTime.Now;
                            int antiguedad = Hoy.Year - empleado.FechaDeIngreso.Year;
                            importe = empleado.SalarioDiario * VacacionesDAO.ObtenerDiasVacaciones(antiguedad);
                            OtrasPercepciones += importe;
                            break;

                        case "Prima Vacacional":
                            importe = (VacacionesDAO.ObtenerDiasVacaciones(empleado.Antiguedad) * .35m) * empleado.SalarioDiario;
                            OtrasPercepciones += importe;
                            break;

                        case "Prestamo Empresa":
                            if (ajusteDetalle.Tipo == "Percepción")
                            {
                                OtrasPercepciones += 3000;
                            }
                            else
                            {
                                OtrasDeducciones += 3000;
                            }
                            break;

                        case "Aguinaldo":
                            importe = empleado.SalarioDiario * 18;
                            OtrasPercepciones += importe;
                            break;

                        case "Horas Extra":
                            importe = (empleado.SalarioDiario / 8) * 2;
                            OtrasPercepciones += importe;
                            break;

                        case "Pension Alimenticia":
                            MessageBox.Show("Pension Alimenticia");
                            break;

                        case "Retardo":
                            decimal diasHorasIMSS = ajuste.DiasHorasIMSS ?? 0m; // Asignar 0 si es nulo
                            importe = (empleado.SalarioDiario / 8m) * diasHorasIMSS;
                            OtrasDeducciones += importe;
                            break;

                        case "Enfermedad General":
                            break;

                        case "Faltas":
                            diasHorasIMSS = ajuste.DiasHorasIMSS ?? 0m; // Asignar 0 si es nulo
                            importe = empleado.SalarioDiario * diasHorasIMSS;
                            OtrasDeducciones += importe;
                            break;

                        case "Maternidad":
                            break;

                        case "Accidente trabajo":
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
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechaInicial = dateTimePicker1.Value;
            DateTime fechaFinal = dateTimePicker2.Value;

            // Obtener los IDs de los períodos correspondientes a las fechas
            var periodos = PeriodoDAO.ObtenerPeriodosPorFechas(fechaInicial, fechaFinal);

            if (periodos.IDPeriodoInicial.HasValue && periodos.IDPeriodoFinal.HasValue)
            {
                // Validar que las fechas están dentro de un rango válido
                if (ValidarDiferenciaMeses(fechaInicial, fechaFinal))
                {
                    dataGridView1.Rows.Clear();
                    FillDataGridView(periodos.IDPeriodoInicial.Value, periodos.IDPeriodoFinal.Value);
                }
                else
                {
                    MessageBox.Show("El rango de meses entre las fechas es inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Manejar el caso donde no se encontraron períodos para las fechas
                MessageBox.Show("No se encontraron períodos para las fechas seleccionadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidarDiferenciaMeses(DateTime fechaInicial, DateTime fechaFinal)
        {
            int diferenciaMeses = ((fechaFinal.Year - fechaInicial.Year) * 12) + fechaFinal.Month - fechaInicial.Month;

            // Validar que haya al menos 1 mes y no más de 10 meses de diferencia
            if (diferenciaMeses >= 1 && diferenciaMeses <= 12)
            {
                return true;
            }
            else
            {
                MessageBox.Show("La diferencia entre las fechas debe ser de al menos 1 mes y no más de 10 meses.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void CatálogoTurnos_Load(object sender, EventArgs e)
        {
            ConfigDataGridView();
        }
    }
}
