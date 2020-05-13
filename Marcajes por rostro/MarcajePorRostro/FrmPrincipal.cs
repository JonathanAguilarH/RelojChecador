namespace MarcajePorRostro
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Collections;
    using System.Data.SqlClient;
    using System.Data;
    using System.Threading;

    public class FrmPrincipal : Form
    {
        private bool borrar = false;
        private int CamHandle = 0;
        private IContainer components = null;
        private int iDelay = 0;
        private Label label1;
        private Label lHora;
        private Label lNombre;
        private PictureBox pbFoto;
        private System.Windows.Forms.Timer reloj;
        private TextBox txtCodigo;
        private const int WM_CAP_CONNECT = 0x40a;
        private const int WM_CAP_COPY = 0x41e;
        private const int WM_CAP_DISCONNECT = 0x40b;
        private const int WM_CAP_GET_FRAME = 0x43c;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_START = 0x400;
        private Label label5;
        private Label lHAcum;
        private Label lFaltas;
        private Label label6;
        private Label lRetraso;
        private Label label7;
        private Label label3;
        private Label label8;
        private Label label9;
        private const int WM_USER = 0x400;
        private PictureBox pictureBox1;
        private DataGridView gridRegistros;
        private Label lblInfoQuincena;
        public bool guardaBOOL = true;
        private GroupBox groupBox1;
        private Label label2;
        public bool mostrarListaOP = true;
        double num;
        public int turnoI = -1;
        string[] datosPeriodo;
        private Panel pBonos;
        private GroupBox groupBox2;
        private Label lblBonoA;
        private Label lblBonoP;
        private GroupBox groupBox3;
        private Label lblMensaje;
        private DataGridViewTextBoxColumn date;
        private DataGridViewTextBoxColumn Fecha;
        private DataGridViewTextBoxColumn Entrada;
        private DataGridViewTextBoxColumn Descanso;
        private DataGridViewTextBoxColumn Reanudar;
        private DataGridViewTextBoxColumn Salida;
        private DataGridViewTextBoxColumn HorasL;
        private Label lblF5;
        public string[] datosEmpleado;
        delegate void pintaRegistros();
        delegate void compruebaDatos();
        delegate void delegadoF5();
        delegate void delegadoInfoQuincena();
        bool continuar = true;
        public FrmPrincipal()
        {
            this.InitializeComponent();
            ManejoBDD.CargaRutas();
            ManejoBDD.sBaseERP = "FIMTECH";
        }
        public void ObtienePeriodo()
        {
            while (continuar)
            {
                this.datosPeriodo = ManejoBDD.CadenaArrayFila("SELECT IDPERIODO, NPERIODO, FECHAINICIO, FECHAFIN, ANTICIPO FROM PERIODOS WHERE FECHAINICIO <= '" + Program.FormateoFecha(DateTime.Now) + "' and FECHAFIN >= '" + Program.FormateoFecha(DateTime.Now) + "' AND ANTICIPO = 0 ORDER BY IDPERIODO");
                if (this.datosPeriodo.Length < 1 | this.datosPeriodo[0] == null)
                {
                    MessageBox.Show("No se encuentra información del período actual, es posible que no esté dado deRR alta en el sistema. Contacte al administrador del sistema. El reloj checador se cerrará.", "No se encuentra el período", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    try
                    {
                        this.Visible = false;
                    }
                    catch(Exception ex)
                    {
                        Thread.CurrentThread.Abort();
                        this.Close();
                        this.Dispose();
                    }
                }
                else
                {
                    this.num = int.Parse(datosPeriodo[0]);
                    if (gridRegistros.InvokeRequired)
                    {
                        delegadoInfoQuincena d2 = new delegadoInfoQuincena(modificainfo);
                        this.Invoke(d2);
                    }
                    else
                    {
                        lblInfoQuincena.Text = "PERÍODO " + datosPeriodo[1] + " del " + Convert.ToDateTime(datosPeriodo[2]).ToLongDateString().ToUpper().Replace(",", "") + " al " + Convert.ToDateTime(datosPeriodo[3]).ToLongDateString().ToUpper().Replace(",", "") + "\n         Horas laborales del período: (Tiempo completo) " + calculaHoras(Convert.ToDateTime(datosPeriodo[2]), Convert.ToDateTime(datosPeriodo[3]));
                        lblInfoQuincena.Visible = true;
                    }
                    if (this.lblF5.InvokeRequired)
                    {
                        delegadoF5 d1 = new delegadoF5(modificaF5);
                        this.Invoke(d1);
                    }
                    else
                    {
                        lblF5.Text = "Última revisión de período: " + DateTime.Now.Date.ToString();
                    }
                }
                Thread.Sleep(3600000);
            }
        }
        public void modificaF5()
        {
            lblF5.Text = "Última revisión de período: " + DateTime.Now.ToString();
        }
        public void modificainfo()
        {
            lblInfoQuincena.Text = "PERÍODO " + datosPeriodo[1] + " del " + Convert.ToDateTime(datosPeriodo[2]).ToLongDateString().ToUpper().Replace(",", "") + " al " + Convert.ToDateTime(datosPeriodo[3]).ToLongDateString().ToUpper().Replace(",", "") + "\n         Horas laborales del período: (Tiempo completo) " + calculaHoras(Convert.ToDateTime(datosPeriodo[2]), Convert.ToDateTime(datosPeriodo[3]));
            lblInfoQuincena.Visible = true;
        }
        [DllImport("avicap32.dll")]
        public static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);
        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescriptionA(int wdriver, string lpszName, int cbName, string lpszVer, int cbVer);
        private void CapturarFoto(bool bFoto)
        {
            try
            {
                EmptyClipboard();
            }
            catch
            {
            }
        }

        [DllImport("user32.dll")]
        protected static extern bool DestroyWindow(int hwnd);
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DllImport("user32.dll")]
        public static extern int EmptyClipboard();
        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.continuar = false;
            SendMessage(this.CamHandle, 0x40b, 0, 0);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.reloj.Enabled = true;  //150417RAAN
            this.lHora.Text = DateTime.Now.ToLongTimeString();
            ThreadStart checaPeriodo = new ThreadStart(ObtienePeriodo);
            Thread hilo = new Thread(checaPeriodo);
            hilo.Start();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.reloj = new System.Windows.Forms.Timer(this.components);
            this.lHora = new System.Windows.Forms.Label();
            this.pbFoto = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lNombre = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lHAcum = new System.Windows.Forms.Label();
            this.lFaltas = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lRetraso = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gridRegistros = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descanso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reanudar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Salida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HorasL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblInfoQuincena = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.pBonos = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBonoA = new System.Windows.Forms.Label();
            this.lblBonoP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblF5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRegistros)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pBonos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // reloj
            // 
            this.reloj.Interval = 1000;
            this.reloj.Tick += new System.EventHandler(this.reloj_Tick);
            // 
            // lHora
            // 
            this.lHora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lHora.BackColor = System.Drawing.Color.White;
            this.lHora.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 65.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lHora.Location = new System.Drawing.Point(353, 9);
            this.lHora.Name = "lHora";
            this.lHora.Size = new System.Drawing.Size(729, 116);
            this.lHora.TabIndex = 0;
            this.lHora.Text = "00:00:00 A.M.";
            this.lHora.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbFoto
            // 
            this.pbFoto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbFoto.BackColor = System.Drawing.Color.White;
            this.pbFoto.Location = new System.Drawing.Point(6, 152);
            this.pbFoto.Name = "pbFoto";
            this.pbFoto.Size = new System.Drawing.Size(273, 445);
            this.pbFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFoto.TabIndex = 2;
            this.pbFoto.TabStop = false;
            this.pbFoto.Click += new System.EventHandler(this.pbFoto_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(5, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Código";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(5, 102);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(166, 44);
            this.txtCodigo.TabIndex = 4;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // lNombre
            // 
            this.lNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lNombre.BackColor = System.Drawing.Color.White;
            this.lNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lNombre.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lNombre.Location = new System.Drawing.Point(177, 102);
            this.lNombre.Name = "lNombre";
            this.lNombre.Size = new System.Drawing.Size(887, 44);
            this.lNombre.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(850, 557);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "A pagar";
            // 
            // lHAcum
            // 
            this.lHAcum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lHAcum.BackColor = System.Drawing.Color.White;
            this.lHAcum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lHAcum.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lHAcum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lHAcum.Location = new System.Drawing.Point(934, 554);
            this.lHAcum.Name = "lHAcum";
            this.lHAcum.Size = new System.Drawing.Size(96, 40);
            this.lHAcum.TabIndex = 13;
            this.lHAcum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lFaltas
            // 
            this.lFaltas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lFaltas.BackColor = System.Drawing.Color.White;
            this.lFaltas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lFaltas.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lFaltas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lFaltas.Location = new System.Drawing.Point(371, 557);
            this.lFaltas.Name = "lFaltas";
            this.lFaltas.Size = new System.Drawing.Size(73, 40);
            this.lFaltas.TabIndex = 15;
            this.lFaltas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(301, 557);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Faltas";
            // 
            // lRetraso
            // 
            this.lRetraso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lRetraso.BackColor = System.Drawing.Color.White;
            this.lRetraso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lRetraso.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRetraso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lRetraso.Location = new System.Drawing.Point(655, 557);
            this.lRetraso.Name = "lRetraso";
            this.lRetraso.Size = new System.Drawing.Size(128, 40);
            this.lRetraso.TabIndex = 17;
            this.lRetraso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(561, 557);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 25);
            this.label7.TabIndex = 16;
            this.label7.Text = "Retrasos";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1036, 571);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "hr.";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(789, 578);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 19);
            this.label8.TabIndex = 19;
            this.label8.Text = "min.";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(450, 578);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "Días";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(335, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // gridRegistros
            // 
            this.gridRegistros.AllowUserToAddRows = false;
            this.gridRegistros.AllowUserToDeleteRows = false;
            this.gridRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRegistros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRegistros.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridRegistros.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridRegistros.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.gridRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRegistros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.Fecha,
            this.Entrada,
            this.Descanso,
            this.Reanudar,
            this.Salida,
            this.HorasL});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRegistros.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridRegistros.Enabled = false;
            this.gridRegistros.Location = new System.Drawing.Point(285, 152);
            this.gridRegistros.MultiSelect = false;
            this.gridRegistros.Name = "gridRegistros";
            this.gridRegistros.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRegistros.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridRegistros.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.gridRegistros.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridRegistros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRegistros.Size = new System.Drawing.Size(779, 399);
            this.gridRegistros.TabIndex = 22;
            // 
            // date
            // 
            this.date.HeaderText = "date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Visible = false;
            // 
            // Fecha
            // 
            this.Fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Fecha.HeaderText = "FECHA";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // Entrada
            // 
            this.Entrada.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Entrada.HeaderText = "ENTRADA";
            this.Entrada.Name = "Entrada";
            this.Entrada.ReadOnly = true;
            this.Entrada.Width = 84;
            // 
            // Descanso
            // 
            this.Descanso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Descanso.HeaderText = "DESCANSO";
            this.Descanso.Name = "Descanso";
            this.Descanso.ReadOnly = true;
            this.Descanso.Width = 91;
            // 
            // Reanudar
            // 
            this.Reanudar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Reanudar.HeaderText = "REGRESO";
            this.Reanudar.Name = "Reanudar";
            this.Reanudar.ReadOnly = true;
            this.Reanudar.Width = 85;
            // 
            // Salida
            // 
            this.Salida.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Salida.HeaderText = "SALIDA";
            this.Salida.Name = "Salida";
            this.Salida.ReadOnly = true;
            this.Salida.Width = 70;
            // 
            // HorasL
            // 
            this.HorasL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.HorasL.HeaderText = "POR PAGAR";
            this.HorasL.Name = "HorasL";
            this.HorasL.ReadOnly = true;
            this.HorasL.Width = 95;
            // 
            // lblInfoQuincena
            // 
            this.lblInfoQuincena.AutoSize = true;
            this.lblInfoQuincena.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoQuincena.ForeColor = System.Drawing.Color.Red;
            this.lblInfoQuincena.Location = new System.Drawing.Point(7, 16);
            this.lblInfoQuincena.Name = "lblInfoQuincena";
            this.lblInfoQuincena.Size = new System.Drawing.Size(277, 22);
            this.lblInfoQuincena.TabIndex = 23;
            this.lblInfoQuincena.Text = "Registros del período actual:";
            this.lblInfoQuincena.Visible = false;
            this.lblInfoQuincena.Click += new System.EventHandler(this.lblRegistros_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.pBonos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pbFoto);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.lblInfoQuincena);
            this.groupBox1.Controls.Add(this.gridRegistros);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lNombre);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lRetraso);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lHAcum);
            this.groupBox1.Controls.Add(this.lFaltas);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1070, 603);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblMensaje);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(53, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(990, 80);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mensaje del sistema";
            this.groupBox3.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(6, 25);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(756, 33);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "No se encontró datos del usuario, por favor reintente.";
            // 
            // pBonos
            // 
            this.pBonos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pBonos.Controls.Add(this.groupBox2);
            this.pBonos.Location = new System.Drawing.Point(6, 503);
            this.pBonos.Name = "pBonos";
            this.pBonos.Size = new System.Drawing.Size(438, 100);
            this.pBonos.TabIndex = 25;
            this.pBonos.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblBonoA);
            this.groupBox2.Controls.Add(this.lblBonoP);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Estatus de bonos";
            // 
            // lblBonoA
            // 
            this.lblBonoA.AutoSize = true;
            this.lblBonoA.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonoA.Location = new System.Drawing.Point(6, 57);
            this.lblBonoA.Name = "lblBonoA";
            this.lblBonoA.Size = new System.Drawing.Size(361, 25);
            this.lblBonoA.TabIndex = 1;
            this.lblBonoA.Text = "Califica a BONO DE ASISTENCIA";
            // 
            // lblBonoP
            // 
            this.lblBonoP.AutoSize = true;
            this.lblBonoP.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonoP.Location = new System.Drawing.Point(6, 23);
            this.lblBonoP.Name = "lblBonoP";
            this.lblBonoP.Size = new System.Drawing.Size(386, 25);
            this.lblBonoP.TabIndex = 0;
            this.lblBonoP.Text = "Califica a BONO DE PUNTUALIDAD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(169, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 31);
            this.label2.TabIndex = 24;
            this.label2.Text = "Nombre";
            // 
            // lblF5
            // 
            this.lblF5.AutoSize = true;
            this.lblF5.Location = new System.Drawing.Point(88, 112);
            this.lblF5.Name = "lblF5";
            this.lblF5.Size = new System.Drawing.Size(0, 13);
            this.lblF5.TabIndex = 25;
            // 
            // FrmPrincipal
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1094, 743);
            this.Controls.Add(this.lblF5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lHora);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASISTENCIAS FIMTECH";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRegistros)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.pBonos.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void reloj_Tick(object sender, EventArgs e)
        {
            this.lHora.Text = DateTime.Now.ToLongTimeString();
            //this.CapturarFoto(false);
            if ((this.iDelay >= 1) & this.borrar)
            {
                this.txtCodigo.Text = "";
                this.lNombre.Text = "";
                this.lHAcum.Text = "";
                this.lFaltas.Text = "";
                this.lRetraso.Text = "";
                this.pbFoto.Image = null;
                this.iDelay = 0;
                this.borrar = false;
                Thread.Sleep(1500);
                lblInfoQuincena.Visible = false;
                gridRegistros.Visible = false;
                this.guardaBOOL = true;
                this.mostrarListaOP = true;
            }
            else
            {
                this.iDelay++;
            }
        }

        //150502RAAN ASIGNA OP
        private bool asignaOP(string sIdEmpleado, string sIdPeriodo, TimeSpan Diferencia)
        {
            try
            {
                if (Diferencia > TimeSpan.Zero)
                {
                    double dImporte = 0;
                    ManejoBDD.sValorB1 = "";  //idDocumento
                    ManejoBDD.sValorB2 = "";  //Descripción
                    ManejoBDD.sValorB3 = "";  //idMaestro
                    FrmBuscaId vIdOP = new FrmBuscaId();
                    vIdOP.TipoB = 17;   //En piso o entregadas parcialmente
                    vIdOP.txtBusca.Focus();
                    vIdOP.txtBusca.Focus();
                    vIdOP.ShowDialog();
                    vIdOP.Dispose();
                    if (ManejoBDD.sValorB3 != "")
                    {
                        if (ManejoBDD.sValorB1 == "")
                        {
                            ManejoBDD.sValorB1 = ManejoBDD.Cadena("Select iddocumento from maestro where idMaestro = '" + ManejoBDD.sValorB3 + "'");
                        }
                        dImporte = Math.Round(Diferencia.TotalHours * double.Parse(this.datosEmpleado[6]), 2);
                        ManejoBDD.AccionQuery("set dateformat dmy insert into asignaop (fecha, Op, Horas, Referencia, idperiodo, idempleado, Importe, idOT) VALUES ('" + DateTime.Today.ToShortDateString() + "'," + ManejoBDD.sValorB1 + "," + Diferencia.TotalHours + ",'" + ManejoBDD.sValorB2 + "'," + sIdPeriodo + "," + sIdEmpleado + "," + dImporte.ToString() + ", " + ManejoBDD.sValorB3 + ")");
                    }
                    else
                    {
                        FrmPideUsuario ventana = new FrmPideUsuario();
                        ventana.bSoloValida = true;
                        ventana.sMensaje = "Registrar salida sin asignar el tiempo a una OP";
                        ventana.ShowDialog();
                        if (ventana.dIdUsuario == 0)  //No se autorizó salida sin OP
                            return false;
                        ventana.Dispose();
                    }
                }
            }
            catch
            {
            }
            if (ManejoBDD.sValorB3 != null & ManejoBDD.sValorB3 != "")
            {
                guardaTotal(totalMdeO(ManejoBDD.sValorB3), 2, ManejoBDD.sValorB3);
            }
            ManejoBDD.sValorB1 = "";
            ManejoBDD.sValorB2 = "";
            ManejoBDD.sValorB3 = "";
            return true;
        }
        public void guardaTotal(double cantidad, int tipo, string sIdOT)
        {
            //Tipo :
            //      Materia prima = 0
            //      Componentes = 1
            //      Procesos y mano de obra = 2
            //      Servicios = 3
            //      Otros Gastos = 4
            if (ManejoBDD.TieneFilas("select * from costoOT where idMaestro = " + sIdOT + " and tipoCosto = " + tipo))
            {
                ManejoBDD.AccionQuery("Update costoOT set actual = " + cantidad + " where tipoCosto = " + tipo + " and idmaestro = " + sIdOT);
            }
            else
            {
                ManejoBDD.AccionQuery("insert into costoOT (idMaestro, idDocumento, tipoCosto, planeado, actual, proyeccion) values (" + sIdOT + ", " + ManejoBDD.sValorB1 + ", " + tipo + ", 0, " + cantidad + ", 0)");
            }
        }
        public double totalMdeO(string sIdOT)
        {
            return Math.Round(ManejoBDD.NumeroReal("select SUM(Importe) from AsignaOP where idOT=" + sIdOT), 2);
        }
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtCodigo.Text = txtCodigo.Text.Trim().ToUpper();
                    if (txtCodigo.Text.Substring(0, 2) == "'E")
                    {
                        txtCodigo.Text = txtCodigo.Text.Substring(2);
                    }
                    txtCodigo.Refresh();
                    this.reloj.Enabled = false;
                    this.txtCodigo.ReadOnly = true;
                    try
                    {
                        string[] datosEmpleado = ManejoBDD.CadenaArrayFila("SELECT IDEMPLEADO, NOMBRE, IDTURNO, FOTO, PRODUCTIVO, horaEntrada, salariohora, alta FROM EMPLEADOS WHERE CODIGO = '" + this.txtCodigo.Text + "'");
                        if (datosEmpleado.Length > 0)
                        {
                            if (datosEmpleado[0] != null)
                            {
                                if (datosEmpleado[7] == "True")
                                {
                                    this.lNombre.Text = datosEmpleado[1];
                                    string str = datosEmpleado[0];
                                    if (guardaBOOL)
                                    {
                                        if (ManejoBDD.AccionQuery("INSERT INTO ASISTENCIAS (IDEMPLEADO, IDPERIODO, FECHAHORA) VALUES (" + str + ", " + num.ToString() + ", '" + FormateaFecha.aFechaHoraUniversal(DateTime.Now) + "')"))
                                        {
                                            this.guardaBOOL = false;
                                            this.mostrarListaOP = true;
                                            pintaFechas(DateTime.Parse(datosPeriodo[2]), DateTime.Parse(datosPeriodo[3]), int.Parse(datosEmpleado[2]), datosEmpleado);
                                        }
                                        else
                                        {
                                            mensajeUsuario("Ocurrió un error, por favor reintente.");
                                        }
                                    }
                                }
                                else
                                {
                                    mensajeUsuario("Este usuario ha sido dado de baja.");
                                    this.mostrarListaOP = false;
                                }
                            }
                            else
                            {
                                mensajeUsuario("No se encontró datos del usuario, por favor reintente.");
                            }
                        }
                    }
                    catch
                    {
                        this.guardaBOOL = true;
                    }
                    this.txtCodigo.ReadOnly = false;
                    this.reloj.Enabled = true;
                    txtCodigo.Text = "";
                    txtCodigo.Focus();
                }
            }
            catch
            {
            }
            ManejoBDD.sValorB3 = "";
        }

        public void mensajeUsuario(string mensaje)
        {
            lblMensaje.ForeColor = Color.White;
            lblMensaje.Text = mensaje;
            groupBox3.BackColor = Color.Red;
            groupBox3.ForeColor = Color.White;
            groupBox3.Visible = true;
            this.Refresh();
            Thread.Sleep(3000);
            groupBox3.Visible = false;
            this.Refresh();
        }
        public void iniciaHilo()
        {
            string[] datosEmpleado;
            if (this.datosEmpleado == null)
            {
                datosEmpleado = ManejoBDD.CadenaArrayFila("SELECT IDEMPLEADO, NOMBRE, IDTURNO, FOTO, PRODUCTIVO, horaEntrada FROM EMPLEADOS WHERE CODIGO = '" + this.txtCodigo.Text + "'");
            }
            else
            {
                datosEmpleado = this.datosEmpleado;
            }
            if (gridRegistros.InvokeRequired)
            {
                pintaRegistros datosAsistencia = new pintaRegistros(cargarDatos);
                this.Invoke(datosAsistencia);
            }
            else
            {
                cargarDatos();
            }
        }
        public void cargarDatos() 
        {
            //[0] IDEMPLEADO
            //[1] NOMBRE
            //[2] IDTURNO
            //[3] FOTO
            //[4] PRODUCTIVO
            //[5] HORAENTRADA

            //int i = 0;
            int registros = 0;
            int conteo = 0;
            int falta = 0;
            int regExtra;
            double aPagoDia = 0, aPagoTotal = 0;
            try
            {
                pbFoto.Image = Image.FromFile(datosEmpleado[3]);
                this.Refresh();
            }
            catch
            {
                pbFoto.Image = null;
            }
            string[] dias = ManejoBDD.CadenaArrayFila("select lunes, martes, miercoles, jueves, viernes, sabado, domingo from turnos where idturno = " + this.datosEmpleado[2]);
            TimeSpan retraso = TimeSpan.Zero, retrasoActual = TimeSpan.Zero, acumulado = TimeSpan.Zero;
            DataTable datos = ManejoBDD.listadoDatos("select FechaHora from Asistencias where IdEmpleado = " + this.datosEmpleado[0] + " and idPeriodo = " + this.datosPeriodo[0] + " order by FechaHora asc");

            string fechaString;
            foreach (DataGridViewRow fila in gridRegistros.Rows)
            {
                regExtra = 0;
                registros = 0;
                DateTime entradaActual = (DateTime)fila.Cells[0].Value;
                int diaActual = entradaActual.DayOfYear;
                if (entradaActual <= DateTime.Today)
                {
                    fechaString = DateTime.Parse(fila.Cells[1].Value.ToString()).ToLongDateString().ToUpper().Replace(",", "");
                    try
                    {
                        while (DateTime.Parse(datos.Rows[conteo][0].ToString()).ToLongDateString().ToUpper().Replace(",", "") == fechaString)
                        {
                            registros++;
                            if (registros > 4)
                            {
                                fila.DefaultCellStyle.BackColor = Color.DarkRed;
                                fila.DefaultCellStyle.ForeColor = Color.White;
                                conteo++;
                                regExtra++;
                                registros = 4;
                            }
                            else
                            {
                                if (fila.Cells[registros + 1].Value.ToString() == "")
                                {
                                    fila.Cells[registros + 1].Value = DateTime.Parse(datos.Rows[conteo][0].ToString()).ToShortTimeString().ToUpper();
                                    conteo++;
                                    if (registros == 1)
                                    {
                                        retrasoActual = DateTime.Parse(this.datosEmpleado[5]).TimeOfDay - DateTime.Parse(datos.Rows[conteo - 1][0].ToString()).TimeOfDay;
                                        if (retrasoActual > TimeSpan.Zero)
                                        {
                                            retrasoActual = TimeSpan.Zero;
                                        }
                                        retraso = retraso + retrasoActual;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    if (registros > 0)
                    {
                        TimeSpan TTotal;
                        switch (registros)
                        {
                            case 1:
                                if (entradaActual.Date != DateTime.Today.Date)
                                {
                                    fila.DefaultCellStyle.BackColor = Color.LightYellow;
                                }
                                //retraso = retraso - retrasoActual;
                                break;
                            case 2:
                                if (DateTime.Parse(datos.Rows[conteo - 2][0].ToString()).TimeOfDay < DateTime.Parse(this.datosEmpleado[5]).TimeOfDay)
                                {
                                    TTotal = DateTime.Parse(datos.Rows[conteo -1][0].ToString()).TimeOfDay - DateTime.Parse(this.datosEmpleado[5]).TimeOfDay;
                                }
                                else
                                {
                                    TTotal = DateTime.Parse(datos.Rows[conteo -1][0].ToString()).TimeOfDay - DateTime.Parse(datos.Rows[conteo - 2][0].ToString()).TimeOfDay;
                                }
                                if (entradaActual.Date != DateTime.Today.Date)
                                {
                                    fila.Cells[5].Value = fila.Cells[3].Value.ToString();
                                    fila.Cells[3].Value = "";
                                }
                                else
                                {
                                    if (this.datosEmpleado[4] == "True")
                                    {
                                        if (!asignaOP(this.datosEmpleado[0], this.datosPeriodo[0], TTotal))
                                        {
                                            mensajeUsuario("No se autorizó el registro SIN OP, reintente.");
                                            ManejoBDD.AccionQuery(" delete from Asistencias where IdAsistencia = (select top(1) IdAsistencia from Asistencias where IdEmpleado = " + this.datosEmpleado[0] + " and IdPeriodo = " + this.datosPeriodo[0] + " order by IdAsistencia desc)");
                                        }
                                    }
                                }
                                acumulado = acumulado + TTotal;
                                //fila.Cells[6].Value = TTotal.ToString().Substring(0, TTotal.ToString().Length -3) + " hrs";
                                aPagoDia = guardaPagar(entradaActual, this.datosEmpleado[0], TTotal);
                                aPagoTotal = aPagoDia + aPagoTotal;
                                fila.Cells[6].Value = aPagoDia + " hrs";
                                break;
                            case 3:
                                if (DateTime.Parse(datos.Rows[conteo - 3][0].ToString()).TimeOfDay < DateTime.Parse(this.datosEmpleado[5]).TimeOfDay)
                                {
                                    TTotal = DateTime.Parse(datos.Rows[conteo - 1][0].ToString()).TimeOfDay - DateTime.Parse(this.datosEmpleado[5]).TimeOfDay;
                                }
                                else
                                {
                                    TTotal = DateTime.Parse(datos.Rows[conteo - 1][0].ToString()).TimeOfDay - DateTime.Parse(datos.Rows[conteo - 3][0].ToString()).TimeOfDay;
                                }
                                acumulado = acumulado + TTotal;
                                if (entradaActual.Date != DateTime.Today.Date)
                                {
                                    fila.Cells[5].Value = fila.Cells[4].Value.ToString();
                                    fila.Cells[4].Value = "";
                                    fila.Cells[4].Style.BackColor = Color.Red;
                                    fila.Cells[5].Style.BackColor = Color.Red;
                                    fila.Cells[4].Style.ForeColor = Color.White;
                                    fila.Cells[5].Style.ForeColor = Color.White;
                                }
                                //fila.Cells[6].Value = TTotal.ToString().Substring(0, TTotal.ToString().Length - 3) + " hrs";
                                aPagoDia = guardaPagar(entradaActual, this.datosEmpleado[0], TTotal);
                                aPagoTotal = aPagoDia + aPagoTotal;
                                fila.Cells[6].Value = aPagoDia + " hrs";
                                break;
                            case 4:
                                TimeSpan TTotal1 = TimeSpan.Zero;
                                if (DateTime.Parse(datos.Rows[conteo - 4 - regExtra][0].ToString()).TimeOfDay < DateTime.Parse(this.datosEmpleado[5]).TimeOfDay)
                                {
                                    TTotal = DateTime.Parse(datos.Rows[conteo - 3 - regExtra][0].ToString()).TimeOfDay - DateTime.Parse(this.datosEmpleado[5]).TimeOfDay;
                                }
                                else
                                {
                                    TTotal = DateTime.Parse(datos.Rows[conteo - 3 - regExtra][0].ToString()).TimeOfDay - DateTime.Parse(datos.Rows[conteo - 4 - regExtra][0].ToString()).TimeOfDay;
                                }
                                TTotal1 = DateTime.Parse(datos.Rows[conteo - 1 - regExtra][0].ToString()).TimeOfDay - DateTime.Parse(datos.Rows[conteo - 2 - regExtra][0].ToString()).TimeOfDay;
                                TTotal = TTotal + TTotal1;
                                if (entradaActual.Date == DateTime.Today.Date)
                                {
                                    if (this.datosEmpleado[4] == "True")
                                    {
                                        if (!asignaOP(this.datosEmpleado[0], this.datosPeriodo[0], TTotal1))
                                        {
                                            mensajeUsuario("No se autorizó el registro SIN OP, reintente.");
                                            ManejoBDD.AccionQuery(" delete from Asistencias where IdAsistencia = (select top(1) IdAsistencia from Asistencias where IdEmpleado = " + this.datosEmpleado[0] + " and IdPeriodo = " + this.datosPeriodo[0] + " order by IdAsistencia desc)");
                                        }
                                    }
                                }
                                acumulado = acumulado + TTotal;
                                //fila.Cells[6].Value = TTotal.ToString().Substring(0, TTotal.ToString().Length - 3) + " hrs";
                                aPagoDia = guardaPagar(entradaActual, this.datosEmpleado[0], TTotal);
                                aPagoTotal = aPagoDia + aPagoTotal;
                                fila.Cells[6].Value = aPagoDia + " hrs";
                                break;
                        }
                    }
                    else // ver si es falta
                    {
                        bool esFalta = false;
                        DateTime fecha = (DateTime)fila.Cells[0].Value;
                        switch (fecha.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                if (dias[0] == "True")
                                {
                                    esFalta = true;
                                }
                                break;
                            case DayOfWeek.Tuesday:
                                if (dias[1] == "True")
                                {
                                    esFalta = true;
                                }
                                break;
                            case DayOfWeek.Wednesday:
                                if (dias[2] == "True")
                                {
                                    esFalta = true;
                                }
                                break;
                            case DayOfWeek.Thursday:
                                if (dias[3] == "True")
                                {
                                    esFalta = true;
                                }
                                break;
                            case DayOfWeek.Friday:
                                if (dias[4] == "True")
                                {
                                    esFalta = true;
                                }
                                break;
                            case DayOfWeek.Saturday:
                                if (dias[5] == "True")
                                {
                                    esFalta = true;
                                }
                                break;
                            case DayOfWeek.Sunday:
                                if (dias[6] == "True")
                                {
                                    esFalta = true;
                                }
                                break;
                        }
                        if (esFalta)
                        {
                            falta++;
                            fila.DefaultCellStyle.BackColor = Color.Red;
                            fila.DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            this.guardaBOOL = true;
            if (this.datosEmpleado[4] == "True")
            {
                if (falta != 0)
                {
                    lblBonoA.Text = "NO califica a BONO DE ASISTENCIA";
                    lblBonoA.ForeColor = Color.Red;
                }
                else
                {
                    lblBonoA.Text = "Califica a BONO DE ASISTENCIA";
                    lblBonoA.ForeColor = Color.Green;
                }
                if (retraso.TotalMinutes > -11 & falta == 0)
                {
                    lblBonoP.Text = "Califica a BONO DE PUNTUALIDAD";
                    lblBonoP.ForeColor = Color.Green;
                }
                else
                {
                    lblBonoP.Text = "NO califica a BONO DE PUNTUALIDAD";
                    lblBonoP.ForeColor = Color.Red;
                }
                pBonos.Visible = true;
            }
            lFaltas.Text = falta.ToString();
            lRetraso.Text = (retraso.TotalMinutes * (-1)).ToString();
            //lHAcum.Text = Convert.ToInt16(acumulado.TotalHours).ToString() + ":" + acumulado.Minutes.ToString();
            lHAcum.Text = aPagoTotal.ToString();
            gridRegistros.ClearSelection();
            this.Refresh();
            Thread.Sleep(3000);
            pbFoto.Image = null;
            lNombre.Text = "";
            pBonos.Visible = false;
            this.Refresh();
        }
        private void pbFoto_Click(object sender, EventArgs e)
        {

        }

        private void lblRegistros_Click(object sender, EventArgs e)
        {

        }
        public double calculaHoras(DateTime inicio, DateTime final)
        {
            double horas = 0;
            string[] horasArray = ManejoBDD.CadenaArrayFila("select lunT - (((lunD * .01) * 100)/60), marT - (((marD * .01) * 100)/60), mieT - (((mieD * .01) * 100)/60), jueT - (((jueD * .01) * 100)/60), vieT - (((vieD * .01) * 100)/60), sabT - (((sabD * .01) * 100)/60), domT - (((domD * .01) * 100)/60) from detalleTurnos where idTurno = 1");
            try
            {
                if (horasArray[0] != null)
                {
                    while (inicio <= final)
                    {
                        switch (inicio.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                horas = horas + double.Parse(horasArray[0]);
                                break;
                            case DayOfWeek.Tuesday:
                                horas = horas + double.Parse(horasArray[1]);
                                break;
                            case DayOfWeek.Wednesday:
                                horas = horas + double.Parse(horasArray[2]);
                                break;
                            case DayOfWeek.Thursday:
                                horas = horas + double.Parse(horasArray[3]);
                                break;
                            case DayOfWeek.Friday:
                                horas = horas + double.Parse(horasArray[4]);
                                break;
                            case DayOfWeek.Saturday:
                                horas = horas + double.Parse(horasArray[5]);
                                break;
                            case DayOfWeek.Sunday:
                                horas = horas + double.Parse(horasArray[6]);
                                break;
                        }
                        inicio = inicio.AddDays(1);
                    }
                }
                else
                {
                    MessageBox.Show("No se han encontrado de talles de horas en el turno seleccionado, se usarán las guardadas con anterioridad", "Sin detalles de turno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    horas = ManejoBDD.NumeroReal("select horas from turnos where idturno = 1");
                }
            }
            catch
            {

            }
            return horas;
        }
        public void pintaFechas(DateTime inicio, DateTime final, int idTurno, string[] datosEmpleado)
        {
            if (idTurno != -1)
            {
                double horas = 0;
                string[] horasArray = ManejoBDD.CadenaArrayFila("select lunT - (((lunD * .01) * 100)/60), marT - (((marD * .01) * 100)/60), mieT - (((mieD * .01) * 100)/60), jueT - (((jueD * .01) * 100)/60), vieT - (((vieD * .01) * 100)/60), sabT - (((sabD * .01) * 100)/60), domT - (((domD * .01) * 100)/60) from detalleTurnos where idTurno = " + idTurno);
                try
                {
                    gridRegistros.Rows.Clear();
                    string fechaTexto;
                    int index = 0;
                    if (horasArray[0] != null)
                    {
                        while (inicio <= final)
                        {
                            gridRegistros.Rows.Add();
                            fechaTexto = inicio.ToLongDateString().ToUpper().Replace(",", "");
                            gridRegistros.Rows[index].Cells[0].Value = inicio;
                            gridRegistros.Rows[index].Cells[1].Value = fechaTexto;
                            gridRegistros.Rows[index].Cells[2].Value = "";
                            gridRegistros.Rows[index].Cells[3].Value = "";
                            gridRegistros.Rows[index].Cells[4].Value = "";
                            gridRegistros.Rows[index].Cells[5].Value = "";
                            gridRegistros.Rows[index].Cells[6].Value = "";
                            switch (inicio.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    horas = horas + double.Parse(horasArray[0]);
                                    if (double.Parse(horasArray[0]) == 0)
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                                    }
                                    else
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.White;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    break;
                                case DayOfWeek.Tuesday:
                                    horas = horas + double.Parse(horasArray[1]);
                                    if (double.Parse(horasArray[1]) == 0)
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                                    }
                                    else
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.White;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    break;
                                case DayOfWeek.Wednesday:
                                    horas = horas + double.Parse(horasArray[2]);
                                    if (double.Parse(horasArray[2]) == 0)
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                                    }
                                    else
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.White;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    break;
                                case DayOfWeek.Thursday:
                                    horas = horas + double.Parse(horasArray[3]);
                                    if (double.Parse(horasArray[3]) == 0)
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                                    }
                                    else
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.White;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    break;
                                case DayOfWeek.Friday:
                                    horas = horas + double.Parse(horasArray[4]);
                                    if (double.Parse(horasArray[4]) == 0)
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                                    }
                                    else
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.White;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    break;
                                case DayOfWeek.Saturday:
                                    horas = horas + double.Parse(horasArray[5]);
                                    if (double.Parse(horasArray[5]) == 0)
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                                    }
                                    else
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.White;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    break;
                                case DayOfWeek.Sunday:
                                    horas = horas + double.Parse(horasArray[6]);
                                    if (double.Parse(horasArray[6]) == 0)
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                                    }
                                    else
                                    {
                                        gridRegistros.Rows[index].DefaultCellStyle.BackColor = Color.White;
                                        gridRegistros.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                                    }
                                    break;
                            } 
                            inicio = inicio.AddDays(1);
                            index++;
                        }
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No se han encontrado de talles de horas en el turno seleccionado, se usarán las guardadas con anterioridad", "Sin detalles de turno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        horas = ManejoBDD.NumeroReal("select horas from turnos where idturno = 1");
                    }
                    this.datosEmpleado = datosEmpleado;
                    ThreadStart delegado = new ThreadStart(iniciaHilo);
                    Thread hilo = new Thread(delegado);
                    hilo.Start();
                }
                catch (Exception ex)
                {

                }
            }
        }
        public double guardaPagar(DateTime fecha, string idEmpleado, TimeSpan lapsoTiempo)
        {
            double horaPago = lapsoTiempo.Hours;
            if (lapsoTiempo.Minutes >= 50)
            {
                horaPago++;
            }
            int idPago = 0;
            try
            {
                idPago = ManejoBDD.Entero16("select idPagoHora from pagoHora where idEmpleado = " + idEmpleado + " and Fecha = '" + Program.FormateoFecha(fecha) + "'");
            }
            catch
            {
                idPago = 0;
            }
            if (idPago != 0)
            {
                string[] datosPago = ManejoBDD.CadenaArrayFila("select horaPago, modificado from pagoHora where idPagoHora = " + idPago);
                if (datosPago[1] == "1")
                {
                    horaPago = double.Parse(datosPago[0].ToString());
                }
                else
                {
                    ManejoBDD.AccionQuery("update pagoHora set horaPago = " + horaPago + " where idPagoHora = " + idPago);
                }
            }
            else
            {
                ManejoBDD.AccionQuery("insert into pagoHora (Fecha, idEmpleado, horaPago, modificado) values ('" + Program.FormateoFecha(fecha) + "', " + idEmpleado + ", " + horaPago + ", 0)");
            }
            return horaPago;
        }
    }
}