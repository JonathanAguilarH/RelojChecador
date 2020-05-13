using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

//150511RAAN COPIADA DE FIM
//ACTUALMENTE SOLO USA LA OPCION TipoB=17
//SE REGRESO LA LLAMADA A CargaOPpiso() CON LA OPCION TipoB=17
//SE AGREGO MAS DESCRIPCION A LA OP EN LA CONSULTA
namespace MarcajePorRostro
{
    public partial class FrmBuscaId : Form
    {
        public FrmBuscaId()
        {
            InitializeComponent();
        }

        public bool bStatus;
        public bool bvacio = true;
        public string vidEquipo="0";
        public int TipoB = 0;
        public string sEncabezado = "";
        public string sIdResponsable = "0";
        public string IdArea = "0",IdProceso="0",IdEquipo="0";

        public string sIdRegistro { get; set; }
        public string sNombre { get; set; }
        public string CadenaSQL = "";

        private void lbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbId.SelectedIndex = lbNombre.SelectedIndex;
        }

        private void CargaStatus()
        {
//            SqlConnection cnn;
  //          SqlCommand cmd;
    //        SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();


                StrSQL = "SELECT IDSTATUS, Nombre FROM STATUSOP ORDER BY Nombre";
                //cmd = new SqlCommand(StrSQL, cnn);
                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    if (lee.GetValue(0).ToString() != ManejoBDD.sIdStatusStandBy)
                    {
                        lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                        lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));
                    }
                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaResponsables()
        {
//            SqlConnection cnn;
  //          SqlCommand cmd;
    //        SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                string sFiltro = "";
                if (sIdResponsable != "0")
                    sFiltro = " AND IDUSUARIO = " + sIdResponsable;

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDEMPLEADO, snproduccion FROM EMPLEADOS WHERE IDDEPTO = " + ManejoBDD.sIdDepartamento + sFiltro + " ORDER BY snproduccion";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));
                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaMateriales()
        {
 //           SqlConnection cnn;
   //         SqlCommand cmd;
     //       SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                //  StrSQL = "SELECT IDMATERIAL, DESCRIPCION FROM MATERIALA ORDER BY DESCRIPCION";
                StrSQL = "SELECT IDPRODUCTO,DESCRIPCION FROM  PRODUCTO WHERE TIPO2 IN(4,12) AND IDEQUIPO="+IdEquipo ;
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaPuestos()
        {
    //        SqlConnection cnn;
      //      SqlCommand cmd;
        //    SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDPUESTO, Nombre FROM PUESTOS ORDER BY Nombre";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaActividades()
        {
  //          SqlConnection cnn;
    //        SqlCommand cmd;
      //      SqlDataReader lee;

            string StrSQL="";
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();

                    if (bvacio == true)
                    {
                        StrSQL = "SELECT IDACTIVIDAD, DESCRIPCION FROM ACTIVIDADES ORDER BY DESCRIPCION ";
                    }
                    else
                    {
                        StrSQL = "SELECT IDACTIVIDAD, DESCRIPCION FROM ACTIVIDADES  WHERE IDEQUIPO=" + vidEquipo + " ORDER BY DESCRIPCION ";
                    }


                  //  StrSQL = "select ACTIVIDADES.IDACTIVIDAD, ACTIVIDADES.DESCRIPCION,EQUIPOS.CODIGO from ACTIVIDADES inner join EQUIPOS on ACTIVIDADES.IDEQUIPO=EQUIPOS.IDEQUIPO";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaDepartamentos()
        {
   //         SqlConnection cnn;
     //       SqlCommand cmd;
       //     SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDDEPTO, Nombre FROM DEPARTAMENTOS ORDER BY Nombre";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;

                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaProcesosNoCero()
        {
   //         SqlConnection cnn;
     //       SqlCommand cmd;
       //     SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDPROCESO, DESCRIPCION FROM PROCESOS WHERE IDPROCESO IN (SELECT IDPROCESO FROM COSTEOPROCESO WHERE TOTAL != 0) ORDER BY DESCRIPCION";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;

                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaDeptosNoCero()
        {
    //        SqlConnection cnn;
      //      SqlCommand cmd;
        //    SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDDEPTO, Nombre FROM DEPARTAMENTOS WHERE IDDEPTO IN (SELECT IDDEPTO FROM COSTOPROCESO WHERE COSTO != 0) ORDER BY Nombre";
                //cmd = new SqlCommand(StrSQL, cnn);
      
                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaProcesos()
        {
    //        SqlConnection cnn;
      //      SqlCommand cmd;
        //    SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDPROCESO, DESCRIPCION FROM PROCESOS ORDER BY DESCRIPCION";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaEquipos()
        {
            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDEQUIPO, CODIGO FROM EQUIPOS ORDER BY CODIGO";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));
                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaUnidades()
        {
     //       SqlConnection cnn;
       //     SqlCommand cmd;
         //   SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDUNIDAD, Nombre FROM UNIDADES ORDER BY Nombre";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaClasificaciones()
        {
    //        SqlConnection cnn;
      //      SqlCommand cmd;
        //    SqlDataReader lee;

            string StrSQL = "";
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                switch (TipoB)
                {
                    case 14:
                        StrSQL = "SELECT IDCLASS, Nombre FROM CLASIFICACIONES WHERE FAMILIA = 0 AND ACTIVO=1 ORDER BY Nombre";
                        break;
                    case 15:
                        StrSQL = "SELECT IDCLASS, Nombre FROM CLASIFICACIONES WHERE FAMILIA = 4 AND ACTIVO=1 ORDER BY Nombre";
                        break;
                    case 16:

                        //anterior    SELECT IDCLASS, Nombre FROM CLASIFICACIONES WHERE FAMILIA = 2 ORDER BY Nombre
                        //despues    select id,nombre from sustratosprod order by nombre
                        StrSQL = "SELECT IDCLASS, Nombre FROM CLASIFICACIONES WHERE FAMILIA = 2 AND ACTIVO=1 ORDER BY Nombre";
                        break;
                    case 26:
                         StrSQL = "SELECT IDCLASS, Nombre FROM CLASIFICACIONES WHERE FAMILIA = 3 AND ACTIVO=1 ORDER BY Nombre";
                         break;
                    case 27:
                         StrSQL = "SELECT IDCLASS, Nombre FROM CLASIFICACIONES WHERE FAMILIA = 4 AND IDRELACION=" + ManejoBDD.sValorB4 + " AND ACTIVO=1 ORDER BY Nombre";
                        break;
                }
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //falta codificar
        private void CargaEquiposPro()
        {
            //        SqlConnection cnn;
            //      SqlCommand cmd;
            //    SqlDataReader lee;

            string StrSQL = "";
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();
                StrSQL = "select idequipo,codigo from equipos  where idarea=" + IdArea + " and idproceso=" + IdProceso + " order by codigo";
                cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = StrSQL;

                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaEmpleados()
        {
     //       SqlConnection cnn;
       //     SqlCommand cmd;
         //   SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDEMPLEADO, Nombre FROM EMPLEADOS WHERE Codigo >= '011' AND Codigo NOT LIKE 'C%' ORDER BY Nombre";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaUsuarios()
        {
            //       SqlConnection cnn;
            //     SqlCommand cmd;
            //   SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                StrSQL = "SELECT IDUSUARIO, NOMBRE FROM USUARIOS ORDER BY Nombre";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaOP()
        {
  //          SqlConnection cnn;
    //        SqlCommand cmd;
      //      SqlDataReader lee;

            string StrSQL;
            string sTemp;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();


                  //  StrSQL = "SELECT DISTINCT IDDOCUMENTO, SERIE, FOLIO FROM MAESTRO WHERE IDSTATUS == " + ManejoBDD.sIdStatusEnPiso + " AND IDEXTRA NOT IN (SELECT IDEXTRA FROM LEVANTAMIENTOS) ORDER BY SERIE, FOLIO";
               
                StrSQL = "SELECT DISTINCT IDDOCUMENTO, SERIE, FOLIO FROM MAESTRO WHERE IDSTATUS == " + ManejoBDD.sIdStatusEnPiso + " AND IDEXTRA NOT IN (SELECT IDEXTRA FROM LEVANTAMIENTOS) ORDER BY SERIE, FOLIO";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    sTemp = Convert.ToString(lee.GetValue(2));
                    if (sTemp.Length == 1)
                        sTemp = "00" + sTemp;
                    if (sTemp.Length == 2)
                        sTemp = "0" + sTemp;
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)) + "-" + sTemp);

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaSigueOP()
        {
  //          SqlConnection cnn;
    //        SqlCommand cmd;
      //      SqlDataReader lee;

            string StrSQL;
            string sTemp;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();

                    StrSQL = "";

                    StrSQL = " SELECT LISTAREMISION.IDDOCUMENTO,MAESTRO.SERIE,MAESTRO.FOLIO,MAESTRO.CLIENTE FROM LISTAREMISION INNER JOIN MAESTRO ON LISTAREMISION.IDDOCUMENTO=MAESTRO.IDDOCUMENTO WHERE  IDREMISION=0 GROUP BY LISTAREMISION.IDDOCUMENTO,MAESTRO.SERIE,MAESTRO.FOLIO,MAESTRO.CLIENTE ORDER BY MAESTRO.SERIE,MAESTRO.FOLIO";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    sTemp = Convert.ToString(lee.GetValue(2));
                    if (sTemp.Length == 1)
                        sTemp = "00" + sTemp;
                    if (sTemp.Length == 2)
                        sTemp = "0" + sTemp;
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)) + "-" + sTemp);

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaOPpiso()
        {
            string StrSQL;
            string sTemp;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();
                StrSQL = "select IdMaestro, IDDOCUMENTO, SERIE, FOLIO, PRODUCTO, OT from MAESTRO order by FOLIO desc, SERIE, OT";

                cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    sTemp = Convert.ToString(lee.GetValue(3));
                    if (sTemp.Length == 1)
                        sTemp = "000" + sTemp;
                    if (sTemp.Length == 2)
                        sTemp = "00" + sTemp;
                    if (sTemp.Length == 3)
                        sTemp = "0" + sTemp;
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(2)) + sTemp + "-" + lee.GetValue(5) + "        " + Convert.ToString(lee.GetValue(4)));
                }
                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargaMantenimiento()
        {
            //          SqlConnection cnn;
            //        SqlCommand cmd;
            //      SqlDataReader lee;

            string StrSQL;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;


                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();

                //Se l quito este candado de las OPS pára enviar        , " + ManejoBDD.sIdStatusPorEnviar + "
                StrSQL = "select m1.Folio,a1.Descripcion from mantenimientos m1 inner join actividades a1 on m1.idactividad=a1.idactividad  where m1.terminada=0 order by m1.folio,a1.descripcion";
                //cmd = new SqlCommand(StrSQL, cnn);

                cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = StrSQL;

                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(0)) + "-" + Convert.ToString(lee.GetValue(1)));
                }
                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargaOPLevantamientos()
        {
    //        SqlConnection cnn;
      //      SqlCommand cmd;
        //    SqlDataReader lee;

            string StrSQL;
            string sTemp;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;
                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();
                StrSQL = "SELECT DISTINCT IDDOCUMENTO, SERIE, FOLIO FROM MAESTRO WHERE IDSTATUS != " + ManejoBDD.sIdStatusTerminado + " AND IDDOCUMENTO IN (SELECT IDDOCUMENTO FROM LEVANTAMIENTOS) ORDER BY SERIE, FOLIO";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    sTemp = Convert.ToString(lee.GetValue(2));
                    if (sTemp.Length == 1)
                        sTemp = "00" + sTemp;
                    if (sTemp.Length == 2)
                        sTemp = "0" + sTemp;
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)) + "-" + sTemp);

                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargaOPTemp()
        {
//            SqlConnection cnn;
  //          SqlCommand cmd;
    //        SqlDataReader lee;

      //      SqlConnection cn1;
        //    SqlCommand cm1;
          //  SqlDataReader le1;

            string StrSQL;
            string sTemp;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                IDbConnection cnn;
                IDbCommand cmd;
                IDataReader lee;

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();
                

                IDbConnection cn1;
                IDbCommand cm1;
                IDataReader le1;

                cn1 = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cn1.Open();
                

                StrSQL = "SELECT DISTINCT IDDOCUMENTO FROM TLEVANTAMIENTOS";
                //cmd = new SqlCommand(StrSQL, cnn);

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = StrSQL;
                
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    StrSQL = "SELECT SERIE, FOLIO FROM DOCUMENTOS WHERE IDDOCUMENTO = " + Convert.ToString(lee.GetValue(0));
                    //cm1 = new SqlCommand(StrSQL, cn1);

                        cm1 = new SqlCommand();
                        cm1.Connection = cn1;
                        cm1.CommandText = StrSQL;
                    

                    le1 = cm1.ExecuteReader();
                    if (le1.Read())
                    {
                        //le1.Read();
                        //Combos
                        lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                        sTemp = Convert.ToString(le1.GetValue(1));
                        if (sTemp.Length == 1)
                            sTemp = "00" + sTemp;
                        if (sTemp.Length == 2)
                            sTemp = "0" + sTemp;
                        lbNombre.Items.Add(Convert.ToString(le1.GetValue(0)) + "-" + sTemp);
                    }
                    le1.Close();
                }

                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaUserVta()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataReader lee;

            string StrSQL = "SELECT IDUSUARIO, NOMBRE FROM EMPLEADOS WHERE IDDEPTO IN (SELECT IDDEPTO FROM DEPARTAMENTOS WHERE NOMBRE = 'VENTAS') AND IDUSUARIO != 0";

            try
            {
                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();

                try
                {
                    cmd = new SqlCommand(StrSQL, cnn);
                    lee = cmd.ExecuteReader();
                    while (lee.Read())
                    {
                        //Combos
                        lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                        lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                    }
                    lee.Close();
                }
                catch (Exception ex1)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + ex1.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargaStatusAvance()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataReader lee;

            string StrSQL = "SELECT IDAVANCE, AVANCE FROM STATUSAVANCE ORDER BY IDAVANCE";

            try
            {
                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();

                try
                {
                    cmd = new SqlCommand(StrSQL, cnn);
                    lee = cmd.ExecuteReader();
                    while (lee.Read())
                    {
                        //Combos
                        lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                        lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));
                    }
                    lee.Close();
                }
                catch (Exception ex1)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + ex1.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargaEmpresas()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataReader lee;

            string StrSQL = "SELECT IDEMPRESA, NOMBRE FROM EMPRESAS ORDER BY NOMBRE";

            try
            {
                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=GeneralUser; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();

                try
                {
                    cmd = new SqlCommand(StrSQL, cnn);
                    lee = cmd.ExecuteReader();
                    while (lee.Read())
                    {
                        //Combos
                        lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                        lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)));

                    }
                    lee.Close();
                }
                catch (Exception ex1)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + ex1.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargaOC()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataReader lee;

            string StrSQL;
            string sTemp;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                    cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                    cnn.Open();

                StrSQL = "SELECT IDDOCUMENTO, SERIE, FOLIO FROM DOCUMENTOS WHERE TIPO = 17 AND ENVIA = " + ManejoBDD.eCancun + " AND IDDOCUMENTO NOT IN (SELECT IDDOCUMENTO FROM MOVIMIENTOS WHERE IDMOVTO IN (SELECT IDMOVPADRE FROM MOVIMIENTOS)) ORDER BY SERIE, FOLIO";
                cmd = new SqlCommand(StrSQL, cnn);
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    sTemp = Convert.ToInt16(lee.GetValue(2)).ToString("000");
                    lbNombre.Items.Add(Convert.ToString(lee.GetValue(1)) + "-" + sTemp);
                }
                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaCosteos()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataReader lee;

            string StrSQL;
            string sTemp;
            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();

                StrSQL = "SELECT IDCOSTO, FOLIO FROM PRODUCTOCOSTEO WHERE IDCOSTO IN (SELECT IDCOSTO FROM RELACIONCOSTEO WHERE IDDOCUMENTO = " +  sIdResponsable + ")";
                cmd = new SqlCommand(StrSQL, cnn);
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(Convert.ToString(lee.GetValue(0)));
                    sTemp = Convert.ToInt16(lee.GetValue(1)).ToString("000");
                    lbNombre.Items.Add("Costeo " + sTemp);
                }
                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaGeneral()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataReader lee;

            try
            {
                //Combos
                lbNombre.Items.Clear();
                lbId.Items.Clear();

                cnn = new SqlConnection("Server=" + ManejoBDD.SQLInstancia + "; database=" + ManejoBDD.sBaseERP + "; user=" + ManejoBDD.SQLUsuario + "; pwd=" + ManejoBDD.SQLPassword);
                cnn.Open();

                cmd = new SqlCommand(CadenaSQL, cnn);
                lee = cmd.ExecuteReader();
                while (lee.Read())
                {
                    //Combos
                    lbId.Items.Add(lee.GetValue(0).ToString());
                    lbNombre.Items.Add(lee.GetValue(1).ToString());
                }
                lee.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmBuscaId_Load(object sender, EventArgs e)
        {
            carga_datos();
        }

        private void carga_datos()
        {
            this.Text = sEncabezado;

            sIdRegistro = "0";
            sNombre = "";

            switch (TipoB)
            {
                case 0:
                    if (bStatus == true)
                    {
                        CargaStatus();
                    }
                    else
                    {
                        CargaResponsables();
                    }
                    break;
                case 1:
                    CargaMateriales();
                    break;
                case 2:
                    CargaActividades();
                    break;
                case 3:
                    CargaOP();
                    break;
                case 4:
                    CargaOPLevantamientos();
                    break;
                case 5:
                    CargaSigueOP();
                    break;
                case 6:
                    CargaDepartamentos();
                    break;
                case 7:
                    CargaEmpleados();
                    break;
                case 8:
                    CargaOPTemp();
                    break;
                case 9:
                    CargaDeptosNoCero();
                    break;
                case 10:
                    CargaProcesos();
                    break;
                case 11:
                    CargaProcesosNoCero();
                    break;
                case 12:
                    CargaPuestos();
                    break;
                case 13:
                    CargaUnidades();
                    break;
                case 14:
                    CargaClasificaciones();
                    break;
                case 15:
                    CargaClasificaciones();
                    break;
                case 16:
                    CargaClasificaciones();
                    break;
                case 17:
                    CargaOPpiso();
                    //CargaOPexterminada();
                    break;
                case 18:
                    CargaUserVta();
                    break;
                case 19:
                    CargaStatusAvance();
                    break;
                case 20:
                    CargaEmpresas();
                    break;
                case 21:
                    CargaEquipos();
                    break;
                case 22:
                    carga_empresas();
                    break;
                case 23:
                    CargaUsuarios();
                    break;
                case 24:
                    CargaOC();
                    break;
                case 25:
                    CargaCosteos();
                    break;
                case 26:
                    CargaClasificaciones();
                    break;
                case 27:
                    CargaClasificaciones();
                    break;
                case 28:
                    CargaEquiposPro();
                    break;
                case 29:
                    CargaMantenimiento();
                    break;
                default:
                    CargaGeneral();
                    break;
            }
            toolStrip1.Focus();
            txtBusca.Focus();
        }

        private void carga_empresas()
        {
            //150511 CONTENIDO ELIMINADO
            /*long lRet;
            TSdkListaEmpresas empresas = new TSdkListaEmpresas();

            lbNombre.Items.Clear();
            lbId.Items.Clear();
            lRet = empresas.buscaPrimero();

            while (lRet == 1)
            {
                lbNombre.Items.Add(empresas.Nombre);
                lbId.Items.Add(empresas.NombreBDD);
                lRet = empresas.buscaSiguiente();
            }

             */
        }

        private void tsbCancela_Click(object sender, EventArgs e)
        {
            ManejoBDD.sValorB3 = "";
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tsbAcepta_Click(object sender, EventArgs e)
        {
            ManejoBDD.sValorB3 = lbId.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lbNombre_DoubleClick(object sender, EventArgs e)
        {
            ManejoBDD.sValorB3 = lbId.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtBusca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtBusca.Text.Substring(0, 2) == "'M" | txtBusca.Text.Substring(0, 2) == "''")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        ManejoBDD.sValorB1 = lbId.Text;
                        ManejoBDD.sValorB2 = lbNombre.Text;
                    }
                }
                else
                {
                    string sRet = "";
                    for (int i = 0; i < lbNombre.Items.Count; i++)
                    {
                        lbNombre.SelectedIndex = i;
                        sRet = lbNombre.Text;
                        if (sRet.Contains(txtBusca.Text))
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        private void FrmBuscaId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oem4)
            {
                txtBusca.Focus();
                txtBusca.Text = "'";
                txtBusca.Select(txtBusca.Text.Length, 0);
            }
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string busqueda = txtBusca.Text;
                    busqueda = busqueda.Replace("''", "'");
                    try
                    {
                        if (busqueda.Substring(0, 2) == "'M")
                        {
                            ManejoBDD.sValorB3 = busqueda.Substring(2); //idMaestro
                        }
                    }
                    catch
                    {
                        ManejoBDD.sValorB3 = lbId.Text; //idMaestro
                        this.Close();
                    }
                    string[] datos = ManejoBDD.CadenaArrayFila("select SERIE + cast(FOLIO as varchar(5)) + '-' + OT as 'folio', PRODUCTO, iddocumento from MAESTRO where IdMaestro = " + ManejoBDD.sValorB3);
                    try
                    {
                        Bitmap imagenMostrar = null;
                        if (datos[0].ToUpper().Substring(0, 3) == "OPR")
                        {
                            imagenMostrar = ManejoBDD.imagen("select imagen from imagenIncidencia where idotr = " + ManejoBDD.sValorB3 + " order by idImagen desc");
                        }
                        else
                        {
                            imagenMostrar = ManejoBDD.imagen("select imagen from imagenesGenerales where idMaestro = " + ManejoBDD.sValorB3 + " order by idImagen desc");
                        }
                        pictureBox1.Image = imagenMostrar;
                    }
                    catch
                    {
                    }
                    lblOT.Text = datos[0];
                    lblInfo.Text = datos[1];
                    gbInfo.Visible = true;
                    this.Refresh();
                    if (pictureBox1.Image == null)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Thread.Sleep(2500);
                    }
                    gbInfo.Visible = false;
                    ManejoBDD.sValorB1 = datos[2]; //idDocumento
                    pictureBox1.Image = null;
                    this.Close();
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch
            {
            }
        }

        private void lbNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ManejoBDD.sValorB1 = lbId.Text;
                ManejoBDD.sValorB2 = lbNombre.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
        }
        private void tsbRefresca_Click(object sender, EventArgs e)
        {
            carga_datos();
        }
    }
}