namespace MarcajePorRostro
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Collections;
    using System.Data.SqlClient;

    public class FrmPrincipal : Form
    {
        private bool borrar = false;
        private int CamHandle = 0;
        private IContainer components = null;
        private int iDelay = 0;
        private Label label1;
        private Label label2;
        private Label label4;
        private ListBox lbMensajes;
        private Label lHora;
        private Label lMarcaje;
        private Label lNombre;
        private PictureBox pbFoto;
        private PictureBox pbToma;
        private Timer reloj;
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

        public FrmPrincipal()
        {
            this.InitializeComponent();
        }

        [DllImport("avicap32.dll")]
        public static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);
        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescriptionA(int wdriver, string lpszName, int cbName, string lpszVer, int cbVer);
        private void CapturarFoto(bool bFoto)
        {
            try
            {
                SendMessage(this.CamHandle, 0x43c, 0, 0);
                SendMessage(this.CamHandle, 0x41e, 0, 0);
                if (bFoto)
                {
                    this.pbToma.Image = Clipboard.GetImage();
                }
                else
                {
                    this.pbFoto.Image = Clipboard.GetImage();
                }
                EmptyClipboard();
            }
            catch
            {
                this.lbMensajes.Items.Add("No se puede Iniciar la WebCam");
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
            SendMessage(this.CamHandle, 0x40b, 0, 0);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            ManejoBDD.CargaRutas();
            ManejoBDD.sBaseERP = "FORJARTE";
            EmptyClipboard();
            this.CamHandle = capCreateCaptureWindowA("Captura Imagen", 0, 0, 0, 0, 0, this.pbFoto.Handle.ToInt32(), 0);
            if (this.CamHandle != 0)
            {
                if (SendMessage(this.CamHandle, 0x40a, 0, 0) != 0)
                {
                    SendMessage(this.CamHandle, 0x434, 30, 0);
                    SendMessage(this.CamHandle, 0x432, 0, 0);
                    //150417RAAN NO INICI EL RELOJ SIN NO HAY CAMARA this.reloj.Enabled = true;
                }
                else
                {
                    this.lbMensajes.Items.Add("No se encontr\x00f3 ninguna camara");
                    DestroyWindow(this.CamHandle);
                }
            }
            else
            {
                this.lbMensajes.Items.Add("No se encontr\x00f3 ninguna camara");
            }
            this.reloj.Enabled = true;  //150417RAAN
            this.lHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.reloj = new System.Windows.Forms.Timer(this.components);
            this.lHora = new System.Windows.Forms.Label();
            this.pbFoto = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lNombre = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lMarcaje = new System.Windows.Forms.Label();
            this.lbMensajes = new System.Windows.Forms.ListBox();
            this.pbToma = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lHAcum = new System.Windows.Forms.Label();
            this.lFaltas = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lRetraso = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbToma)).BeginInit();
            this.SuspendLayout();
            // 
            // reloj
            // 
            this.reloj.Interval = 1000;
            this.reloj.Tick += new System.EventHandler(this.reloj_Tick);
            // 
            // lHora
            // 
            this.lHora.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lHora.BackColor = System.Drawing.Color.White;
            this.lHora.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 89.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lHora.Location = new System.Drawing.Point(12, 7);
            this.lHora.Name = "lHora";
            this.lHora.Size = new System.Drawing.Size(994, 148);
            this.lHora.TabIndex = 0;
            this.lHora.Text = "00:00:00 A.M.";
            this.lHora.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbFoto
            // 
            this.pbFoto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbFoto.BackColor = System.Drawing.Color.White;
            this.pbFoto.Location = new System.Drawing.Point(12, 159);
            this.pbFoto.Name = "pbFoto";
            this.pbFoto.Size = new System.Drawing.Size(280, 280);
            this.pbFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFoto.TabIndex = 2;
            this.pbFoto.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(298, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "Código";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(306, 217);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(166, 62);
            this.txtCodigo.TabIndex = 4;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(476, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 46);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nombre";
            // 
            // lNombre
            // 
            this.lNombre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lNombre.BackColor = System.Drawing.Color.White;
            this.lNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lNombre.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lNombre.Location = new System.Drawing.Point(484, 217);
            this.lNombre.Name = "lNombre";
            this.lNombre.Size = new System.Drawing.Size(522, 60);
            this.lNombre.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(298, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 46);
            this.label4.TabIndex = 7;
            this.label4.Text = "Marcaje";
            // 
            // lMarcaje
            // 
            this.lMarcaje.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lMarcaje.BackColor = System.Drawing.Color.White;
            this.lMarcaje.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lMarcaje.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lMarcaje.Location = new System.Drawing.Point(306, 327);
            this.lMarcaje.Name = "lMarcaje";
            this.lMarcaje.Size = new System.Drawing.Size(166, 40);
            this.lMarcaje.TabIndex = 8;
            this.lMarcaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMensajes
            // 
            this.lbMensajes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbMensajes.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMensajes.FormattingEnabled = true;
            this.lbMensajes.ItemHeight = 37;
            this.lbMensajes.Location = new System.Drawing.Point(306, 461);
            this.lbMensajes.Name = "lbMensajes";
            this.lbMensajes.Size = new System.Drawing.Size(700, 263);
            this.lbMensajes.TabIndex = 9;
            // 
            // pbToma
            // 
            this.pbToma.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbToma.BackColor = System.Drawing.Color.White;
            this.pbToma.Location = new System.Drawing.Point(12, 445);
            this.pbToma.Name = "pbToma";
            this.pbToma.Size = new System.Drawing.Size(280, 280);
            this.pbToma.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbToma.TabIndex = 10;
            this.pbToma.TabStop = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(498, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 36);
            this.label5.TabIndex = 11;
            this.label5.Text = "Acumulado";
            // 
            // lHAcum
            // 
            this.lHAcum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lHAcum.BackColor = System.Drawing.Color.White;
            this.lHAcum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lHAcum.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lHAcum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lHAcum.Location = new System.Drawing.Point(504, 327);
            this.lHAcum.Name = "lHAcum";
            this.lHAcum.Size = new System.Drawing.Size(100, 40);
            this.lHAcum.TabIndex = 13;
            this.lHAcum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lFaltas
            // 
            this.lFaltas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lFaltas.BackColor = System.Drawing.Color.White;
            this.lFaltas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lFaltas.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lFaltas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lFaltas.Location = new System.Drawing.Point(877, 327);
            this.lFaltas.Name = "lFaltas";
            this.lFaltas.Size = new System.Drawing.Size(60, 40);
            this.lFaltas.TabIndex = 15;
            this.lFaltas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(871, 292);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 36);
            this.label6.TabIndex = 14;
            this.label6.Text = "Faltas";
            // 
            // lRetraso
            // 
            this.lRetraso.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lRetraso.BackColor = System.Drawing.Color.White;
            this.lRetraso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lRetraso.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRetraso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lRetraso.Location = new System.Drawing.Point(700, 327);
            this.lRetraso.Name = "lRetraso";
            this.lRetraso.Size = new System.Drawing.Size(80, 40);
            this.lRetraso.TabIndex = 17;
            this.lRetraso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(694, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 36);
            this.label7.TabIndex = 16;
            this.label7.Text = "Retrasos";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(610, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "Horas";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(786, 340);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 19);
            this.label8.TabIndex = 19;
            this.label8.Text = "Minutos";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(943, 340);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "Días";
            // 
            // FrmPrincipal
            // 
            this.ClientSize = new System.Drawing.Size(1018, 736);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lRetraso);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lFaltas);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lHAcum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbToma);
            this.Controls.Add(this.lbMensajes);
            this.Controls.Add(this.lMarcaje);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lNombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbFoto);
            this.Controls.Add(this.lHora);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Marcaje 1.1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbToma)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void reloj_Tick(object sender, EventArgs e)
        {
            this.lHora.Text = DateTime.Now.ToLongTimeString();
            this.CapturarFoto(false);
            if ((this.iDelay >= 1) & this.borrar)
            {
                this.txtCodigo.Text = "";
                this.lNombre.Text = "";
                this.lMarcaje.Text = "";
                this.lHAcum.Text = "";
                this.lFaltas.Text = "";
                this.lRetraso.Text = "";
                this.pbToma.Image = null;
                this.iDelay = 0;
                this.borrar = false;
            }
            else
            {
                this.iDelay++;
            }
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.reloj.Enabled = false;
                    this.txtCodigo.ReadOnly = true;
                    try
                    {
                        if (ManejoBDD.TieneFilas("SELECT NOMBRE, IDTURNO FROM EMPLEADOS WHERE CODIGO = '" + this.txtCodigo.Text + "'"))
                        {
                            this.lNombre.Text = ManejoBDD.Cadena("SELECT NOMBRE FROM EMPLEADOS WHERE CODIGO = '" + this.txtCodigo.Text + "'");
                            string str = ManejoBDD.Cadena("SELECT IDEMPLEADO FROM EMPLEADOS WHERE CODIGO = '" + this.txtCodigo.Text + "'");
                            DateTime now = DateTime.Now;
                            this.lMarcaje.Text = now.ToLongTimeString();
                            double num = ManejoBDD.NumeroReal("SELECT IDPERIODO FROM PERIODOS WHERE FECHAINICIO <= '" + FormateaFecha.aFechaUniversal(now) + "' AND FECHAFIN >= '" + FormateaFecha.aFechaUniversal(now) + "'");
                            ManejoBDD.AccionQuery("INSERT INTO ASISTENCIAS (IDEMPLEADO, IDPERIODO, FECHAHORA) VALUES (" + str + ", " + num.ToString() + ", '" + FormateaFecha.aFechaHoraUniversal(now) + "')");

                            //> DESPLIEGA TIEMPO ACUMULADO 150209 AMILCAR
                            this.Cursor = Cursors.WaitCursor;
                            //SqlConnection cnn;
                            //SqlCommand cmd;
                            //SqlDataReader lee;

                            SqlConnection cn1;
                            SqlCommand cm1;
                            SqlDataReader le1;

                            string StrSQL = "";

                            
                            //ManejoBDD.AccionQuery("delete from reportehoras"); NO ES NECESARIO PORQUE NO SE GENERA EL REPORTE

                            try
                            {
                                //cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                                //cnn.Open();

                                cn1 = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                                cn1.Open();

                                try
                                {
                                    // VARIABLES PARA DESPLEGAR EL TIEMPO TRABAJADO
                                    //barra.Maximum = Convert.ToInt16(ExtraeDato.NumeroReal(StrSQL));
                                    //barra.Value = 0;

                                    
                                    DateTime Inicio = DateTime.Now;
                                    DateTime Final = DateTime.Now;

                                    DateTime dtTemp = Inicio;
                                    DateTime dtTem2;
                                    DateTime dtTem3;
                                    DateTime dtEntrada = DateTime.Now;
                                    DateTime dtEntradaC = DateTime.Now;
                                    DateTime dtSalida = DateTime.Now;
                                    DateTime dtSalidaC = DateTime.Now;

                                    string sIdEmpleado = "0";
                                    string sIdTurno = "0";

                                    ArrayList Horas;
                                    ArrayList Corte;
                                    TimeSpan Diferencia;
                                    decimal HorasTurno = 0;
                                    decimal HorasDia = 0;
                                    decimal HorasT = 0;     //Total de horas
                                    decimal HorasE = 0;     //Horas Extras
                                    int HoraIndex = 0;

                                    decimal HorasAcum = 0;  //Total de horas por empleado en el período
                                    decimal Retraso = 0;
                                    decimal Faltas = 0;

                                    decimal HorasDesc = 0;
                                    bool bLun;
                                    bool bMar;
                                    bool bMie;
                                    bool bJue;
                                    bool bVie;
                                    bool bSab;
                                    bool bDom;
                                    
                                    sIdEmpleado = str;
                                    Inicio = ManejoBDD.Fecha("SELECT FECHAINICIO FROM PERIODOS WHERE IDPERIODO = '" + num + "'");
                                    //150417RAAN No es necesario validar hasta el fin del período Final = ManejoBDD.Fecha("SELECT FECHAFIN FROM PERIODOS WHERE IDPERIODO = '" + num + "'");
                                    dtTemp = Inicio;
                                    sIdTurno = ManejoBDD.Cadena("SELECT IDTURNO FROM EMPLEADOS WHERE CODIGO = '" + this.txtCodigo.Text + "'");
                                    HorasTurno = Convert.ToDecimal(ManejoBDD.NumeroReal("select horadia from turnos where idturno = " + sIdTurno));
                                    HorasDesc = Convert.ToInt32(ManejoBDD.NumeroReal("select MinutosDesc from TURNOS where idturno = " + sIdTurno)) / (Decimal)60;
                                    bLun = ManejoBDD.Entero16("select LUNES from TURNOS where idturno = " + sIdTurno) == 1;
                                    bMar = ManejoBDD.Entero16("select MARTES from TURNOS where idturno = " + sIdTurno) == 1;
                                    bMie = ManejoBDD.Entero16("select MIERCOLES from TURNOS where idturno = " + sIdTurno) == 1;
                                    bJue = ManejoBDD.Entero16("select JUEVES from TURNOS where idturno = " + sIdTurno) == 1;
                                    bVie = ManejoBDD.Entero16("select VIERNES from TURNOS where idturno = " + sIdTurno) == 1;
                                    bSab = ManejoBDD.Entero16("select SABADO from TURNOS where idturno = " + sIdTurno) == 1;
                                    bDom = ManejoBDD.Entero16("select DOMINGO from TURNOS where idturno = " + sIdTurno) == 1;
                                    
                                    // CARGA EN Corte LAS LOS HORARIOS DE ENTRADA, SALIDA, ETC PARA EL TURNO
                                    Corte = new ArrayList();
                                    StrSQL = "SELECT VALORHORA FROM PARAMETROSASIS WHERE IDTURNO = " + sIdTurno + "ORDER BY TIPO";
                                    cm1 = new SqlCommand(StrSQL, cn1);
                                    le1 = cm1.ExecuteReader();
                                    while (le1.Read())
                                    {
                                        Corte.Add(le1.GetValue(0));
                                    }
                                    le1.Close();

                                    //CARGA HORAS TRABAJADAS POR FECHA
                                    while (dtTemp <= Final)
                                    {
                                        Horas = new ArrayList();
                                        HoraIndex = 0;
                                        //ASISTENCIAS POR DIA
                                        //CARGA LAS HORAS DE REGISTRO EL EMPLEADO PARA LA FECHA CORRESPONDIENTE
                                        StrSQL = "SELECT FECHAHORA FROM ASISTENCIAS WHERE FECHAHORA >= '" + Program.FormateoFechaHora(new DateTime(dtTemp.Year, dtTemp.Month, dtTemp.Day, 0, 0, 0)) + "' AND FECHAHORA <= '" + Program.FormateoFechaHora(new DateTime(dtTemp.Year, dtTemp.Month, dtTemp.Day, 23, 59, 59)) + "' AND IDEMPLEADO = " + sIdEmpleado + " ORDER BY FECHAHORA";
                                        cm1 = new SqlCommand(StrSQL, cn1);
                                        le1 = cm1.ExecuteReader();
                                        while (le1.Read())
                                        {
                                            #region Avanzo Barra
                                            //if (barra.Value < barra.Maximum)
                                            //    barra.Value = barra.Value + 1;
                                            //else
                                            //    barra.Value = 0;
                                            //barra.Refresh();
                                            //barra.Refresh();
                                            //barra.Refresh();
                                            //barra.Refresh();
                                            //barra.Refresh();
                                            #endregion
                                            //APLICA CRITERIOS DE HORARIO SEGUN EL TURNO Y CARGA LOS REGISTROS EN EL ARREGLO Horas
                                            switch (HoraIndex)
                                            {
                                                case 0:
                                                    Horas.Add(le1.GetValue(0));
                                                    HoraIndex = HoraIndex + 1;
                                                    break;
                                                case 1:
                                                    dtTem2 = Convert.ToDateTime(Corte[0]);
                                                    dtTem3 = Convert.ToDateTime(le1.GetValue(0));
                                                    dtTem2 = new DateTime(dtTem3.Year, dtTem3.Month, dtTem3.Day, dtTem2.Hour, dtTem2.Minute, dtTem2.Second);
                                                    dtTem2 = dtTem2.AddMinutes(5);
                                                    if (Convert.ToDateTime(le1.GetValue(0)) > dtTem2)
                                                    {
                                                        Horas.Add(le1.GetValue(0));
                                                        HoraIndex = HoraIndex + 1;
                                                    }
                                                    break;
                                                case 2:
                                                    if (Corte.Count < 4)
                                                    {
                                                        Horas[1] = le1.GetValue(0);
                                                    }
                                                    else
                                                    {
                                                        dtTem2 = Convert.ToDateTime(Corte[1]);
                                                        dtTem3 = Convert.ToDateTime(le1.GetValue(0));
                                                        dtTem2 = new DateTime(dtTem3.Year, dtTem3.Month, dtTem3.Day, dtTem2.Hour, dtTem2.Minute, dtTem2.Second);
                                                        dtTem2 = dtTem2.AddMinutes(5);
                                                        if (Convert.ToDateTime(le1.GetValue(0)) > dtTem2)
                                                        {
                                                            Horas.Add(le1.GetValue(0));
                                                            HoraIndex = HoraIndex + 1;
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    if (Corte.Count < 4)
                                                    {
                                                        Horas[1] = le1.GetValue(0);
                                                    }
                                                    else
                                                    {
                                                        dtTem2 = Convert.ToDateTime(Corte[2]);
                                                        dtTem3 = Convert.ToDateTime(le1.GetValue(0));
                                                        dtTem2 = new DateTime(dtTem3.Year, dtTem3.Month, dtTem3.Day, dtTem2.Hour, dtTem2.Minute, dtTem2.Second);
                                                        dtTem2 = dtTem2.AddMinutes(5);
                                                        if (Convert.ToDateTime(le1.GetValue(0)) > dtTem2)
                                                        {
                                                            Horas.Add(le1.GetValue(0));
                                                            HoraIndex = HoraIndex + 1;
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    Horas[Horas.Count - 1] = le1.GetValue(0);
                                                    break;
                                            }
                                        }
                                        le1.Close();
                                        if (Horas.Count > 0)
                                        {
                                            switch (Horas.Count)
                                            {
                                                // Se calcula el tiempo trabajado en Diferencia, restando a la hora de salida la hora de entrada
                                                // Se combierte la Diferencia en Decimal asignandola a HorasT
                                                // Se calculan las horas trabajadas con el turno correspondiente (HorasTurno) y las horas extras (arriba de media hora)

                                                case 1:     // Un sólo registro, no hay suficiente información para determinar el tiempo trabajado
                                                    HorasT = 0;
                                                    HorasE = 0;
                                                    //ManejoBDD.AccionQuery("INSERT INTO REPORTEHORAS (IDEMPLEADO, FECHA, ENTRADA, HORAS, EXTRAS, TOTAL,ADJUNTO) VALUES (" + sIdEmpleado + ", '" + Program.FormateoFecha(dtTemp) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[0])) + "', 0, 0, 0,0)");
                                                    HorasAcum = HorasAcum + 0;
                                                    break;
                                                case 2:     // 2 regisros. Turno corrido
                                                    Diferencia = Convert.ToDateTime(Horas[1]) - Convert.ToDateTime(Horas[0]);
                                                    HorasT = Convert.ToDecimal(Diferencia.Hours) + (Convert.ToDecimal(Diferencia.Minutes) / Convert.ToDecimal(60));
                                                    if (HorasT > HorasTurno)
                                                        HorasDia = HorasTurno;
                                                    else
                                                        HorasDia = HorasT;

                                                    if (HorasT > HorasDia & HorasT - HorasDia > (decimal)0.5)
                                                        HorasE = HorasT - HorasDia;
                                                    else
                                                        HorasE = 0;
                                                    //ManejoBDD.AccionQuery("INSERT INTO REPORTEHORAS (IDEMPLEADO, FECHA, ENTRADA, SALIDA, HORAS, EXTRAS, TOTAL,ADJUNTO) VALUES (" + sIdEmpleado + ", '" + Program.FormateoFechaHora(dtTemp) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[0])) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[1])) + "', " + HorasDia.ToString() + ", " + HorasE.ToString() + ", " + HorasT.ToString() + ",0)");
                                                    HorasAcum = HorasAcum + HorasT;
                                                    break;
                                                case 3:      // 3 regisros. Turno corrido, se descarta el tercer registro
                                                    Diferencia = Convert.ToDateTime(Horas[1]) - Convert.ToDateTime(Horas[0]);
                                                    HorasT = Convert.ToDecimal(Diferencia.Hours) + (Convert.ToDecimal(Diferencia.Minutes) / Convert.ToDecimal(60));
                                                    if (HorasT > HorasTurno)
                                                        HorasDia = HorasTurno;
                                                    else
                                                        HorasDia = HorasT;

                                                    if (HorasT > HorasDia & HorasT - HorasDia > (decimal)0.5)
                                                        HorasE = HorasT - HorasDia;
                                                    else
                                                        HorasE = 0;
                                                    //ManejoBDD.AccionQuery("INSERT INTO REPORTEHORAS (IDEMPLEADO, FECHA, ENTRADA, SALIDAC, ENTRADAC, HORAS, EXTRAS, TOTAL,ADJUNTO) VALUES (" + sIdEmpleado + ", '" + Program.FormateoFechaHora(dtTemp) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[0])) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[1])) + "',  '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[2])) + "', " + HorasDia.ToString() + ", " + HorasE.ToString() + ", " + HorasT.ToString() + ",0)");
                                                    HorasAcum = HorasAcum + HorasT;
                                                    break;
                                                case 4:      // 4 regisros. Turno partido, se resta el tiempo de comida
                                                    Diferencia = Convert.ToDateTime(Horas[1]) - Convert.ToDateTime(Horas[0]);
                                                    HorasT = Convert.ToDecimal(Diferencia.Hours) + (Convert.ToDecimal(Diferencia.Minutes) / Convert.ToDecimal(60));
                                                    Diferencia = Convert.ToDateTime(Horas[3]) - Convert.ToDateTime(Horas[2]);
                                                    HorasT = HorasT + (Convert.ToDecimal(Diferencia.Hours) + (Convert.ToDecimal(Diferencia.Minutes) / Convert.ToDecimal(60)));
                                                    if (HorasT > HorasTurno)
                                                        HorasDia = HorasTurno;
                                                    else
                                                        HorasDia = HorasT;

                                                    if (HorasT > HorasDia & HorasT - HorasDia > (decimal)0.5)
                                                        HorasE = HorasT - HorasDia;
                                                    else
                                                        HorasE = 0;
                                                    //ManejoBDD.AccionQuery("INSERT INTO REPORTEHORAS (IDEMPLEADO, FECHA, ENTRADA, SALIDAC, ENTRADAC, SALIDA, HORAS, EXTRAS, TOTAL,ADJUNTO) VALUES (" + sIdEmpleado + ", '" + Program.FormateoFechaHora(dtTemp) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[0])) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[1])) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[2])) + "', '" + Program.FormateoFechaHora(Convert.ToDateTime(Horas[3])) + "', " + HorasDia.ToString() + ", " + HorasE.ToString() + ", " + HorasT.ToString() + ",0)");
                                                    HorasAcum = HorasAcum + HorasT;
                                                    break;
                                            }
                                            //> DESPLIEGA TIEMPO ACUMULADO DE RETRASO Y FALTAS 150408 AMILCAR
                                            //150417RAAN if (HorasAcum > (decimal)0.5) HorasAcum = HorasAcum - (decimal)0.5;     // DESCUENTA MEDIA HORA DE COMIDA POR DIA
                                            if (HorasAcum > HorasDesc) HorasAcum = HorasAcum - HorasDesc;    //DESCUENTA TIEMPO DE DESCANSO POR DIA
                                            // CALCULA MINUTOS DE RETRASO
                                            dtTem2 = Convert.ToDateTime(Horas[0]);              //ENTRADA
                                            //dtTem2 = new DateTime(dtTem2.Year, dtTem2.Month, dtTem2.Day, 8, 0, 0); //ASIGNA FECHA DE REGISTRO A LA HORA DE ENTRADA 8:00 AM
                                            if (Convert.ToDecimal(dtTem2.Minute) < (decimal)30) //ASIGNA FECHA DE REGISTRO A LA HORA DE ENTRADA
                                                dtTem2 = new DateTime(dtTem2.Year, dtTem2.Month, dtTem2.Day, dtTem2.Hour, 0, 0); //ASIGNA LA HORA DE ENTRADA 
                                            else
                                                dtTem2 = new DateTime(dtTem2.Year, dtTem2.Month, dtTem2.Day, dtTem2.Hour + 1, 0, 0); //ASIGNA LA HORA DE ENTRADA 
                                            if (Convert.ToDateTime(Horas[0]) > dtTem2)
                                            {
                                                Diferencia = Convert.ToDateTime(dtTem2) - Convert.ToDateTime(Horas[0]);   //ENTRADA - SALIDA
                                                Retraso = Retraso + ((Convert.ToDecimal(Diferencia.Hours) * Convert.ToDecimal(60)) + Convert.ToDecimal(Diferencia.Minutes));
                                            }
                                        }
                                        else    //SIN REGISTO EN EL DIA
                                        {
                                            /*150417RAAN if (dtTemp.DayOfWeek == DayOfWeek.Saturday || dtTemp.DayOfWeek == DayOfWeek.Sunday)
                                            { } else Faltas = Faltas + 1; */
                                            switch (dtTemp.DayOfWeek)
                                            {
                                                case DayOfWeek.Monday:
                                                    if (bLun) Faltas = Faltas + 1;
                                                    break;
                                                case DayOfWeek.Tuesday:
                                                    if (bMar) Faltas = Faltas + 1;
                                                    break;
                                                case DayOfWeek.Wednesday:
                                                    if (bMie) Faltas = Faltas + 1;
                                                    break;
                                                case DayOfWeek.Thursday:
                                                    if (bJue) Faltas = Faltas + 1;
                                                    break;
                                                case DayOfWeek.Friday:
                                                    if (bVie) Faltas = Faltas + 1;
                                                    break;
                                                case DayOfWeek.Saturday:
                                                    if (bSab) Faltas = Faltas + 1;
                                                    break;
                                                case DayOfWeek.Sunday:
                                                    if (bDom) Faltas = Faltas + 1;
                                                    break;
                                            }
                                        } 
                                        dtTemp = dtTemp.AddDays(1);
                                    }
                                    //this.lbMensajes.Items.Add("Horas Acumuladas en el período: " + HorasAcum.ToString());
                                    this.lHAcum.Text = HorasAcum.ToString();
                                    if (Faltas > 0) this.lFaltas.Text = Faltas.ToString();
                                    if (Retraso < 0) this.lRetraso.Text = (Retraso*-1).ToString();
                                    //< DESPLIEGA TIEMPO ACUMULADO DE RETRASO Y FALTAS 150408 AMILCAR
                                }
                                catch (Exception ex1)
                                {
                                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + ex1.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                //cnn.Close();
                                cn1.Close();
                                }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            this.Cursor = Cursors.Default;
                            //< DESPLIEGA TIEMPO ACUMULADO 150211
                            this.iDelay = 0;
                            this.borrar = true;
                            this.CapturarFoto(true);
                        }
                    }
                    catch
                    {
                    }
                    this.txtCodigo.ReadOnly = false;
                    this.reloj.Enabled = true;
                }
            }
            catch
            {
            }
        }
    }
}

