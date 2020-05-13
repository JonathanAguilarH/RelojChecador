using System;
using System.Windows.Forms;

namespace MarcajePorRostro
{
    public partial class FrmPideUsuario : Form
    {
        public bool bSoloValida = false;
        public double dIdUsuario = 0;
        public string sMensaje = "";

        public FrmPideUsuario()
        {
            InitializeComponent();
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            dIdUsuario = 0;
            this.Close();
        }

        private void ValidaPass()
        {
            try
            {
                string sPass = ManejoBDD.Cadena("Select password from usuarios where Nombre = '" + txtNombre.Text + "'").Trim();
                if (sPass!="" && ManejoBDD.DeCodifico(sPass) == txtpass.Text)
                {
                    //ManejoBDD.sValorB1 = txtNombre.Text;
                    dIdUsuario = ManejoBDD.NumeroReal("Select IdUsuario from usuarios where Nombre = '" + txtNombre.Text + "'");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El Usuario o la Contraseña es incorrecta, intente de nuevo", "Validación de usuarios");
                    txtpass.Focus();
                }

            }
            catch
            {

            }
        }

        private void btnAcepta_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim()!="")
                ValidaPass();
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ValidaPass();
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtpass.Focus();
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.Alt) + Convert.ToInt32(Keys.F11) | Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.Alt) + Convert.ToInt32(Keys.M))
            {
                this.Close();
            }
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.Alt) + Convert.ToInt32(Keys.F12))
            {
                this.Close();
            }
        }

        private void FrmIngreso_Load(object sender, EventArgs e)
        {
            txtMensaje.Text = sMensaje;
        }
    }
}
