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
    public partial class Sesion : Form
    {
        public Sesion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Usuario = textBox1.Text;
            string contrasena = textBox2.Text;

            // Verificar las credenciales del usuario
            bool autenticado = UsuarioDAO.AutenticarUsuario(Usuario, contrasena);

            if (!autenticado)
            {
                MessageBox.Show("Credenciales incorrectas. Intenta nuevamente.");
            }
            else
            {
                Empleados newWindow = new Empleados();
                this.Hide();
                newWindow.ShowDialog();
                this.Close();
            }
        }

        private void Sesion_Load(object sender, EventArgs e)
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
            dataGridView1.Columns.Add("Empresa", "Empresa");
        }
        private void RellenarDataGridView()
        {
            // Limpiar filas existentes en el DataGridView
            dataGridView1.Rows.Clear();

            // Obtener lista de empleados desde la base de datos
            List <Empresa> listaEmpresa = EmpresaDAO.ObtenerEmpresa();
            // Rellenar el DataGridView con la información de empleados
            foreach (Empresa empresa in listaEmpresa)
            {
                dataGridView1.Rows.Add(empresa.IdEmpresa,empresa.Ra_S);
            }
        }
    }
}
