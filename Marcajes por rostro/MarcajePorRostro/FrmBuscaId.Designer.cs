namespace MarcajePorRostro
{
    partial class FrmBuscaId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBuscaId));
            this.lbNombre = new System.Windows.Forms.ListBox();
            this.lbId = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAcepta = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresca = new System.Windows.Forms.ToolStripButton();
            this.tsbCancela = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtBusca = new System.Windows.Forms.ToolStripTextBox();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblOT = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.gbInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbNombre
            // 
            this.lbNombre.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombre.FormattingEnabled = true;
            this.lbNombre.ItemHeight = 25;
            this.lbNombre.Location = new System.Drawing.Point(0, 85);
            this.lbNombre.Name = "lbNombre";
            this.lbNombre.Size = new System.Drawing.Size(994, 404);
            this.lbNombre.TabIndex = 0;
            this.lbNombre.SelectedIndexChanged += new System.EventHandler(this.lbNombre_SelectedIndexChanged);
            this.lbNombre.DoubleClick += new System.EventHandler(this.lbNombre_DoubleClick);
            this.lbNombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbNombre_KeyUp);
            // 
            // lbId
            // 
            this.lbId.FormattingEnabled = true;
            this.lbId.Location = new System.Drawing.Point(0, 0);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(177, 17);
            this.lbId.TabIndex = 2;
            this.lbId.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAcepta,
            this.tsbRefresca,
            this.tsbCancela,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtBusca});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(994, 44);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAcepta
            // 
            this.tsbAcepta.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tsbAcepta.Image = ((System.Drawing.Image)(resources.GetObject("tsbAcepta.Image")));
            this.tsbAcepta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAcepta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAcepta.Name = "tsbAcepta";
            this.tsbAcepta.Size = new System.Drawing.Size(128, 41);
            this.tsbAcepta.Text = "&Aceptar";
            this.tsbAcepta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbAcepta.ToolTipText = "Aceptar";
            this.tsbAcepta.Click += new System.EventHandler(this.tsbAcepta_Click);
            // 
            // tsbRefresca
            // 
            this.tsbRefresca.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tsbRefresca.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresca.Image")));
            this.tsbRefresca.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbRefresca.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresca.Name = "tsbRefresca";
            this.tsbRefresca.Size = new System.Drawing.Size(143, 41);
            this.tsbRefresca.Text = "&Refrescar";
            this.tsbRefresca.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbRefresca.Click += new System.EventHandler(this.tsbRefresca_Click);
            // 
            // tsbCancela
            // 
            this.tsbCancela.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tsbCancela.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancela.Image")));
            this.tsbCancela.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancela.Name = "tsbCancela";
            this.tsbCancela.Size = new System.Drawing.Size(139, 41);
            this.tsbCancela.Text = "&Cancelar";
            this.tsbCancela.Click += new System.EventHandler(this.tsbCancela_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(93, 41);
            this.toolStripLabel1.Text = "Buscar";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // txtBusca
            // 
            this.txtBusca.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.Size = new System.Drawing.Size(250, 44);
            this.txtBusca.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBusca_KeyUp);
            // 
            // gbInfo
            // 
            this.gbInfo.BackColor = System.Drawing.Color.White;
            this.gbInfo.Controls.Add(this.pictureBox1);
            this.gbInfo.Controls.Add(this.lblInfo);
            this.gbInfo.Controls.Add(this.lblOT);
            this.gbInfo.Location = new System.Drawing.Point(12, 5);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(970, 465);
            this.gbInfo.TabIndex = 3;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "información de OT trabajada";
            this.gbInfo.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(3, 122);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(964, 340);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblInfo.Location = new System.Drawing.Point(14, 89);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(68, 30);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "label1";
            this.lblInfo.UseCompatibleTextRendering = true;
            // 
            // lblOT
            // 
            this.lblOT.AutoSize = true;
            this.lblOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOT.ForeColor = System.Drawing.Color.Maroon;
            this.lblOT.Location = new System.Drawing.Point(17, 16);
            this.lblOT.Name = "lblOT";
            this.lblOT.Size = new System.Drawing.Size(210, 73);
            this.lblOT.TabIndex = 0;
            this.lblOT.Text = "label1";
            // 
            // FrmBuscaId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 489);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lbId);
            this.Controls.Add(this.lbNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBuscaId";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmBuscaId_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBuscaId_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbNombre;
        private System.Windows.Forms.ListBox lbId;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAcepta;
        private System.Windows.Forms.ToolStripButton tsbCancela;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbRefresca;
        public System.Windows.Forms.ToolStripTextBox txtBusca;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Label lblOT;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}