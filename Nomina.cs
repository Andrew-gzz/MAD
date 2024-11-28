using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using MAD.DAO;
using MAD.DAO.MAD.DAO;
using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAD
{
    public partial class Nomina : Cabecera
    {
        private decimal PercepcionesGenerales = 0;//percepciones generales
        private decimal OtrasPercepciones = 0;//otras percepciones
        private decimal TotalPercepciones = 0;//total percepciones
        private decimal DeduccionesGenerales = 0;//deducciones obligadas
        private decimal OtrasDeducciones = 0;//otras deducciones
        private decimal TotalDeducciones = 0;//total deducciones
        private decimal NetoAPagar = 0; //Neto
        private decimal SumaTotal = 0;
        public Nomina()
        {
            InitializeComponent();
        }

        private void Nomina_Load(object sender, EventArgs e)
        {
            ConfigDataGridView(); //Configura el Grid
            FillDataGridView();   //Reliza los calculos de la nomina por empleado
            FillDataCorp();
        }

        private void ConfigDataGridView()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns.Add("Percepciones", "Percepciones");
            dataGridView1.Columns.Add("Deducciones", "Deducciones");
            dataGridView1.Columns.Add("Extra O.", "Extra O.");
            dataGridView1.Columns.Add("Incidencias", "Incidencias");
            dataGridView1.Columns.Add("Total", "Total");
        }

        private void CalcularTotales(Empleado empleado, int periodo, bool reingreso)
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
                if (empleado.Estatus|| reingreso) { 
                    TotalPercepciones += OtrasPercepciones;
                    TotalDeducciones += OtrasDeducciones;
                    NetoAPagar = TotalPercepciones - TotalDeducciones;
                    SumaTotal += NetoAPagar;
                    dataGridView1.Rows.Add(empleado.IdEmpleado.ToString(),empleado.Nombre,TotalPercepciones.ToString("C2"),TotalDeducciones.ToString("C2"),
                                                             OtrasPercepciones.ToString("C2"), OtrasDeducciones.ToString("C2"), NetoAPagar.ToString("C2"));
                }
                else
                {
                  
                    dataGridView1.Rows.Add(empleado.IdEmpleado.ToString(), empleado.Nombre, 0.ToString("C2"), 0.ToString("C2"),
                                                             0.ToString("C2"), 0.ToString("C2"), 0.ToString("C2"));
                }
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

        private void FillDataGridView()
        {           
            SumaTotal = 0; 
            List<Empleado> ListaEmpleados = EmpleadoDAO.ObtenerEmpleados();
            foreach (var row in ListaEmpleados)//Empleados
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
                    List<Movimientos_Empleados> ListaPeriodosEmpleados = Movimientos_EmpleadosDAO.ObtPeriodosPorEmp(row.IdEmpleado);
                    if (ListaPeriodosEmpleados != null)
                    {
                        foreach (var row4 in ListaPeriodosEmpleados)//Me da una lista de los periodos isla(tiene fecha de inicio y fecha final)Solo funciona con reingreso
                        {
                            if (row4.ID_PERIODO_ALTA <= Cabecera.idperiodo && Cabecera.idperiodo <= row4.ID_PERIODO_BAJA)
                            {
                                CalcularTotales(row, Cabecera.idperiodo, true);
                            }
                        }
                    }
                    //Para empleados que nunca han estado de baja comparar desde su fecha de ingreso
                    int LastDate = Movimientos_EmpleadosDAO.ObtIdAlta(row.IdEmpleado);
                    if (Cabecera.idperiodo >= LastDate && row.Estatus)
                    {
                        CalcularTotales(row, Cabecera.idperiodo, false);
                    }                  
            }
            dataGridView1.Rows.Add("Suma de total:", SumaTotal.ToString("C2"));           
        }

        private void FillDataCorp()
        {
            Periodo PeriodoActual = PeriodoDAO.ObtenerPeriodoActual();
            if(PeriodoActual.IdPeriodo != Cabecera.idperiodo)
            {
                button2.Enabled = false;
            }
            Periodo periodo = PeriodoDAO.ObtenerPeriodoporID(Cabecera.idperiodo);
            label13.Text = $"{periodo.FInicial:dd/MM/yyyy} - {periodo.FFin:dd/MM/yyyy}";
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

        private void Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                    saveFileDialog.Title = "Guardar Reporte de Nómina";
                    saveFileDialog.FileName = "ReporteNomina.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new ClosedXML.Excel.XLWorkbook())
                        {
                            // Crear una sola hoja para todo el contenido
                            var hoja = workbook.Worksheets.Add("Reporte del periodo");
                            int filaActual = 1;

                            // Sección: Datos de la Empresa
                            hoja.Cell(filaActual, 1).Value = "DATOS DE LA EMPRESA";
                            hoja.Range(filaActual, 1, filaActual, 2).Merge().Style.Font.Bold = true;
                            filaActual++;

                            hoja.Cell(filaActual, 1).Value = "Razón Social";
                            hoja.Cell(filaActual, 2).Value = label1.Text;
                            filaActual++;
                            hoja.Cell(filaActual, 1).Value = "RFC";
                            hoja.Cell(filaActual, 2).Value = label7.Text;
                            filaActual++;
                            hoja.Cell(filaActual, 1).Value = "Representante Legal";
                            hoja.Cell(filaActual, 2).Value = label8.Text;
                            filaActual++;
                            hoja.Cell(filaActual, 1).Value = "ID Registro Patronal";
                            hoja.Cell(filaActual, 2).Value = label9.Text;
                            filaActual++;
                            hoja.Cell(filaActual, 1).Value = "Dirección";
                            hoja.Cell(filaActual, 2).Value = label10.Text;
                            filaActual++;
                            hoja.Cell(filaActual, 1).Value = "Registro Fiscal";
                            hoja.Cell(filaActual, 2).Value = label11.Text;
                            filaActual++;
                            hoja.Cell(filaActual, 1).Value = "Periodo";
                            hoja.Cell(filaActual, 2).Value = label13.Text;
                            filaActual += 2; // Espaciado entre secciones                          

                            // Sección: DataGridView
                            ExportarDataGridViewAHoja(hoja, dataGridView1, "Calculos", ref filaActual);

                            // Guardar el archivo en la ubicación seleccionada
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("El archivo se ha guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al exportar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarDataGridViewAHoja(IXLWorksheet hoja, DataGridView dgv, string titulo, ref int filaActual)
        {
            // Título de la sección
            hoja.Cell(filaActual, 1).Value = titulo;
            hoja.Range(filaActual, 1, filaActual, dgv.Columns.Count).Merge().Style.Font.Bold = true;
            filaActual++;

            // Encabezados
            for (int col = 0; col < dgv.Columns.Count; col++)
            {
                hoja.Cell(filaActual, col + 1).Value = dgv.Columns[col].HeaderText;
                hoja.Cell(filaActual, col + 1).Style.Font.Bold = true;
            }
            filaActual++;

            // Filas de datos
            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    hoja.Cell(filaActual + row, col + 1).Value = dgv.Rows[row].Cells[col].Value?.ToString();
                }
            }
            filaActual += dgv.Rows.Count + 2; // Espaciado entre secciones
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                object cellValue = selectedRow.Cells["ID"].Value;

                if (cellValue != null && int.TryParse(cellValue.ToString(), out int selectedID))
                {
                    ReporteNómina.idempleado = selectedID;
                }
                else
                {
                    button1.Enabled = false;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ReporteNómina newWindow = new ReporteNómina();
            this.Hide();
            newWindow.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea cerrar el periodo actual?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes) { 
                Periodo NuevoPeriodo = PeriodoNuevo();
                //PeriodoDAO.InsertarPeriodo(NuevoPeriodo);
                MessageBox.Show("Se regresara a la pantalla de selección de periodo");
                PeriodoSelect newWindow = new PeriodoSelect();
                this.Hide();
                newWindow.ShowDialog();
                this.Close();
            }
        }

        private Periodo PeriodoNuevo()
        {
            Periodo PeriodoActual = PeriodoDAO.ObtenerPeriodoActual();
            DateTime FInicialActual = PeriodoActual.FInicial;

            // Obtener el primer día del mes siguiente al periodo actual
            DateTime nuevoFInicial = new DateTime(FInicialActual.Year, FInicialActual.Month, 1).AddMonths(1);

            // Obtener el último día del nuevo mes
            DateTime nuevoFFin = nuevoFInicial.AddMonths(1).AddDays(-1);

            return new Periodo
            {
                FInicial = nuevoFInicial,
                FFin = nuevoFFin
            };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PeriodoSelect newWindow = new PeriodoSelect();
            this.Hide();
            newWindow.ShowDialog();
            this.Close();
        }
    }
}
