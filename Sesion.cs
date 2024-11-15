using MAD.DAO;
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

            if (!autenticado){
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

        }
    }
}
