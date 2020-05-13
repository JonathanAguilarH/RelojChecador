namespace MarcajePorRostro
{
    partial class FrmPideUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPideUsuario));
            this.txtpass = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancela = new System.Windows.Forms.Button();
            this.btnAcepta = new System.Windows.Forms.Button();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtpass
            // 
            this.txtpass.Font = new System.Drawing.Font("Arial", 24F);
            this.txtpass.Location = new System.Drawing.Point(189, 182);
            this.txtpass.Name = "txtpass";
            this.txtpass.PasswordChar = '*';
            this.txtpass.Size = new System.Drawing.Size(290, 44);
            this.txtpass.TabIndex = 1;
            this.txtpass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpass_KeyDown);
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Arial", 24F);
            this.txtNombre.Location = new System.Drawing.Point(189, 132);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(290, 44);
            this.txtNombre.TabIndex = 0;
            this.txtNombre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombre_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 24F);
            this.label2.Location = new System.Drawing.Point(12, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 36);
            this.label2.TabIndex = 7;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 24F);
            this.label1.Location = new System.Drawing.Point(66, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 36);
            this.label1.TabIndex = 6;
            this.label1.Text = "Usuario";
            // 
            // btnCancela
            // 
            this.btnCancela.Font = new System.Drawing.Font("Arial", 24F);
            this.btnCancela.Image = ((System.Drawing.Image)(resources.GetObject("btnCancela.Image")));
            this.btnCancela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancela.Location = new System.Drawing.Point(113, 244);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(173, 63);
            this.btnCancela.TabIndex = 3;
            this.btnCancela.Text = "Cancelar";
            this.btnCancela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // btnAcepta
            // 
            this.btnAcepta.Font = new System.Drawing.Font("Arial", 24F);
            this.btnAcepta.Image = ((System.Drawing.Image)(resources.GetObject("btnAcepta.Image")));
            this.btnAcepta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAcepta.Location = new System.Drawing.Point(306, 244);
            this.btnAcepta.Name = "btnAcepta";
            this.btnAcepta.Size = new System.Drawing.Size(173, 63);
            this.btnAcepta.TabIndex = 2;
            this.btnAcepta.Text = "Aceptar";
            this.btnAcepta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAcepta.UseVisualStyleBackColor = true;
            this.btnAcepta.Click += new System.EventHandler(this.btnAcepta_Click);
            // 
            // txtMensaje
            // 
            this.txtMensaje.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.txtMensaje.Location = new System.Drawing.Point(12, 12);
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(467, 95);
            this.txtMensaje.TabIndex = 8;
            // 
            // FrmPideUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 319);
            this.ControlBox = false;
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.btnAcepta);
            this.Controls.Add(this.btnCancela);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPideUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usuario";
            this.Load += new System.EventHandler(this.FrmIngreso_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Button btnAcepta;
        private System.Windows.Forms.TextBox txtMensaje;
    }
}