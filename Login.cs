using ProyectoED.ProyectoDS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoED
{
    public partial class Login : Form
    {
        private string User = "user1";
        private string password = "User123";
        private int attempts =0;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() == string.Empty || txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Debe completar los campos solicitados", "Advertencia");
                return;
            }
            if (txtUser.Text == User && txtPassword.Text == password)
            {
                MessageBox.Show("¡Inicio de sesión exitoso!", "Bienvenido");
                RegistroOFC registro = new RegistroOFC();
                registro.Show();
                this.Hide();
            }
            else
            {
                attempts++;
                if (attempts >= 3)
                {
                    MessageBox.Show("Número de intentos excedido. La aplicación se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos. Inténtalo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUser.Focus();
                }
            }

        }
    }
}
