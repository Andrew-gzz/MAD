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
    public partial class CatálogoEmpleadosSalarios : Cabecera
    {
        public CatálogoEmpleadosSalarios()
        {
            InitializeComponent();
        }

        private void CatálogoEmpleadosSalarios_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            RellenarDataGridView();
        }
        private void ConfigurarDataGridView()
        {
            // Limpiar columnas anteriores si existen
            dataGridView1.Columns.Clear();

            // Agregar las columnas "Empleado" y "ID"           
            dataGridView1.Columns.Add("ID", "ID"); 
            dataGridView1.Columns.Add("Nombre", "Nombre");            
            dataGridView1.Columns.Add("Estatus", "Estatus");
            dataGridView1.Columns.Add("Fecha de Ingreso", "Fecha de Ingreso");            
            dataGridView1.Columns.Add("Tipo de salario", "Tipo de salario");
            dataGridView1.Columns.Add("Salario", "Salario");
            dataGridView1.Columns.Add("SDI", "SDI");
            dataGridView1.Columns.Add("Sueldo Bruto Mensual", "Sueldo Bruto Mensual");
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
                if (empleado.Estatus) { 
                    dataGridView1.Rows.Add(empleado.IdEmpleado, empleado.Nombre, "Activo", empleado.FechaDeIngreso, "Fijo", empleado.SalarioDiario, empleado.SalarioDiarioIntegrado, empleado.SueldoMensual);
                }
                else
                {
                    dataGridView1.Rows.Add(empleado.IdEmpleado, empleado.Nombre, "Inactivo", empleado.FechaDeIngreso, "Fijo", empleado.SalarioDiario, empleado.SalarioDiarioIntegrado, empleado.SueldoMensual);
                }
            }

        }
    }
}
