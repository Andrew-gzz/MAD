﻿using MAD.DAO;
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

            EliminaciónAjuste newWindow = new EliminaciónAjuste();
            newWindow.IdEmp = idemp;
            newWindow.ShowDialog();
        }//Eliminar Ajuste
        private void button1_Click(object sender, EventArgs e)//Agregar Ajuste
        {
            string motivo = comboBox1.Text; // Sacar motivo de comboBox1
            int idAjuste = AjusteDAO.BuscarIdAjustePorMotivo(motivo); // Buscar el ID_AJUSTE por motivo
            if (idemp<=0)
            {
                MessageBox.Show("Selecciona un empleado o periodo valido");
                return;
            }
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Selecciona un ajuste valido");
                return;
            }
            if (comboBox1.Text == "Faltas" && string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Agrega los dias de falta");
                return;
            }
            if (idAjuste != -1)
            {
                bool repetido = AjustesEmpleadoPeriodoDAO.VerificarRepetidos(idemp, idAjuste, idper);
                if (repetido)
                {
                    MessageBox.Show("Ya existe un registro con estos datos.");
                }
                else
                {
                    // Verificar si el valor de textBox1 es un número válido para long
                    long? diasHorasIMSS = null;
                    if (!string.IsNullOrWhiteSpace(textBox1.Text) && long.TryParse(textBox1.Text, out long parsedValue))
                    {
                        diasHorasIMSS = parsedValue;
                    }
                    else
                    {
                        diasHorasIMSS = 0;
                    }
                    AjustesEmpleadoPeriodo ajusteEmpleadoPeriodo = new AjustesEmpleadoPeriodo
                    {
                        IdAjuste = idAjuste,
                        IdEmp = idemp,
                        IdPeriodo = idper,
                        DiasHorasIMSS = diasHorasIMSS
                    };

                    int resultado = AjustesEmpleadoPeriodoDAO.InsertarAjusteEmpleadoPeriodo(ajusteEmpleadoPeriodo);
                    if (resultado > 0)
                    {
                        MessageBox.Show("Ajuste agregado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al agregar el ajuste.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No se encontró el ID_AJUSTE para el motivo especificado.");
            }

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
