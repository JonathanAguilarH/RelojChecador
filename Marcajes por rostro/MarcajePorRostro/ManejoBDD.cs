namespace MarcajePorRostro
{
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Windows.Forms;

    public class ManejoBDD
    {
        public static bool bddWEB = false;
        public static string MySQLPassword;
        public static string MySQLServidor;
        public static string MySQLUsuario;
        public static string sBaseERP = "FIMTECH";
        public static string sBaseMySQL = "clubatle_FIM";
        public static string sBaseLocal = "Checador";
        public static string SQLInstancia;
        public static string SQLPassword;
        public static string SQLUsuario;
        public static string sRutaArchivos;

        //150508 Copiado de FIM.Programa.cs
        #region Variables IDs de status, departamentos y demás por default

        public static string sIdDepartamento = "9";
        public static string sIdDireccionC = "2";
        public static string sIdDireccionM = "36";
        public static string sIdStatusStandBy = "1";
        public static string sIdStatusTerminado = "2";
        public static string sIdStatusTerminadoP = "18";
        public static string sidStatusCancelado = "3";
        public static string sIdStatusPorEnviar = "4";
        public static string sIdStatusPorEnviarPar = "16";
        public static string sIdStatusPorEntregar = "19";
        public static string sIdStatusPorEntregarP = "20";

        public static string sIdStatusEnviado = "5";
        public static string sIdStatusEnviadoP = "14";
        public static string sIdStatusRecibido = "6";
        public static string sIdStatusRecibidoP = "15";
        public static string sIdStatusParcialR = "7";
        public static string sIdStatusOficinaT = "8";
        public static string sIdStatusEnPiso = "9";
        public static string sIdStatusAlmacenPT = "10";
        public static string sIDStatusRechazada = "13";
        public static string sIDStatusFaltante = "17";

        public static string sIdDeptoAlmacen = "7";
        public static string sIdOficinaTMed = "2";
        public static string sIdOficinaTCun = "29";
        public static string sIdClassMP = "61";
        public static string sResultadoSDK = "BIEN";
        public static string sCodigoClasMP = "MP";
        public static string sCodigoClasIN = "IN";

        public static string sIdMaster = "5";
        public static string sIdPruebas = "20";

        public static string sIdClasCUNC = "1";
        public static string sIdClasMidC = "2";
        public static string sIdClasCUNP = "9";
        public static string sIdClasMidP = "8";

        public static string sIdStatusEnProceso = "1";
        public static string sIdCierreEnProceso = "1";

        public static double dbPorIVAMED = 0.16;
        public static double dbPorIVACUN = 0.11;


        #endregion

        //150508 Copiado de FIM.Programa.cs
        public static string eCancun = "0";

        //150508 Copiado de FIM.Programa.cs
        #region Variables Publicas ValorB

        public static string sValorB1;
        public static string sValorB2;
        public static string sValorB3;
        public static string sValorB4;
        public static string sValorB5 = "0";
        public static string sValorB6 = "0";
        public static DateTime dtValorB1 = DateTime.Today;
        public static ArrayList ValorArray = new ArrayList();
        public static ArrayList ValorArray2 = new ArrayList();

        #endregion

        public static string DeCodifico(string Palabra)
        {
            string Salida = "";
            char tChr;
            int vChr = 0;
            for (int i = 0; i < Palabra.Length; i++)
            {
                tChr = Convert.ToChar(Palabra.Substring(i, 1));
                vChr = char.ConvertToUtf32(Palabra, i);
                if (i % 2 == 0)
                    vChr = vChr - 5;
                else
                    vChr = vChr + 5;
                Salida = Salida + Convert.ToString(char.ConvertFromUtf32(vChr));
            }
            return Salida;
        }

        public static bool AccionQuery(string StrSQL)
        {
            bool flag = false;
            try
            {
                IDbConnection connection = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
                connection.Open();
                try
                {
                    IDbCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = StrSQL;
                    command.ExecuteNonQuery();
                    flag = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + exception.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + exception2.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return flag;
        }
        public static string Cadena(string StrSQL)
        {
            string str = "";
            try
            {
                IDbConnection connection = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
                connection.Open();
                try
                {
                    // RAAN 150207 CORRECCION DE ERROR
                    //                    IDbCommand command = new SqlCommand {
                    //                        Connection = connection,
                    //                        CommandText = StrSQL
                    //                    };
                    IDbCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = StrSQL;

                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read() && !reader.IsDBNull(0))
                    {
                        str = reader.GetValue(0).ToString();
                    }
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + exception.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                connection.Close();
            }
            catch (Exception exception2)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + exception2.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return str;
        }

        public static void CargaRutas()
        {
            try
            {
                ArrayList list = new ArrayList();
                bool flag = false;
                RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\RodzSoft");
                SQLInstancia = Convert.ToString(key.GetValue("nInstancia"));
                SQLUsuario = Convert.ToString(key.GetValue("nUsuario"));
                SQLPassword = Convert.ToString(key.GetValue("ncontrase\x00f1a"));
                MySQLServidor = Convert.ToString(key.GetValue("MySQLServer"));
                MySQLUsuario = Convert.ToString(key.GetValue("MySQLUser"));
                MySQLPassword = Convert.ToString(key.GetValue("MySQLPass"));
                sBaseMySQL = Convert.ToString(key.GetValue("EmpresaMySQL"));
                //sBaseLocal = Convert.ToString(key.GetValue("dbChecador"));
                if (MySQLServidor == "")
                {
                    key.SetValue("MySQLServer", "box680.bluehost.com");
                    MySQLServidor = "box680.bluehost.com";
                }
                if (MySQLUsuario == "")
                {
                    key.SetValue("MySQLUser", "clubatle_Admin");
                    MySQLUsuario = "clubatle_Admin";
                }
                if (MySQLPassword == "")
                {
                    key.SetValue("MySQLPass", "rpfutbol");
                    MySQLPassword = "rpfutbol";
                }
                if (sBaseMySQL == "")
                {
                    key.SetValue("EmpresaMySQL", "clubatle_FIM");
                    sBaseMySQL = "clubatle_FIM";
                }
                if (key.GetValue("EmpresaERP") == null)
                {
                    sBaseERP = "OperadoraMA";
                }
                else
                {
                    sBaseERP = key.GetValue("EmpresaERP").ToString();
                    if (sBaseERP.Trim() == "")
                    {
                        sBaseERP = "FIM";
                    }
                }
                string name = @"SOFTWARE\Meyaj\FIMTECH";
                RegistryKey key2 = Registry.LocalMachine.OpenSubKey(name, false);
                name = @"SOFTWARE\Meyaj\" + sBaseERP;
                RegistryKey key3 = Registry.LocalMachine.CreateSubKey(name);
                foreach (string str2 in key3.GetValueNames())
                {
                    list.Add(str2);
                }
                if (sBaseERP != "FIM")
                {
                    foreach (string str3 in key2.GetValueNames())
                    {
                        flag = false;
                        for (int i = 0; i < list.Count; i++)
                        {
                            string str4 = list[i].ToString();
                            if (list[i].ToString().ToUpper() == str3.ToUpper())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            key3.SetValue(str3, Convert.ToString(key2.GetValue(str3)));
                        }
                    }
                }
                sRutaArchivos = Convert.ToString(key3.GetValue("RutaArchivos"));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error al cargar las rutas de las empresas de AdminPAQ y COntPAQ i", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                sBaseERP = "OperadoraMA";
            }
        }

        public static DateTime Fecha(string StrSQL)
        {
            DateTime now = DateTime.Now;
            try
            {
                IDbConnection connection = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
                connection.Open();
                try
                {
                    // RAAN 150207 CORRECCION DE ERROR
                    //                    IDbCommand command = new SqlCommand {
                    //                        Connection = connection,
                    //                        CommandText = StrSQL
                    //                    };
                    IDbCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = StrSQL;

                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read() && !reader.IsDBNull(0))
                    {
                        now = reader.GetDateTime(0);
                    }
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + exception.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                connection.Close();
            }
            catch (Exception exception2)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + exception2.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return now;
        }

        public static double NumeroReal(string StrSQL)
        {
            double num = 0.0;
            try
            {
                IDbConnection connection = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
                connection.Open();
                try
                {
                    // RAAN 150207 CORRECCION DE ERROR
                    //                    IDbCommand command = new SqlCommand {
                    //                        Connection = connection,
                    //                        CommandText = StrSQL
                    //                    };
                    IDbCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = StrSQL;

                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read() && !reader.IsDBNull(0))
                    {
                        num = Convert.ToDouble(reader.GetValue(0));
                    }
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + exception.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                connection.Close();
            }
            catch (Exception exception2)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + exception2.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return num;
        }

        //150417RAAN Tomado de FIM/Program.cs
        public static DataTable listadoDatos(string Consulta)
        {
            SqlConnection cnn;
            // SqlCommand cmd;
            DataTable dtlistado = new DataTable();
            cnn = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
            //  cnn.Open();
            // string qlistado = "select * from qAuto";
            try
            {
                SqlDataAdapter dalistado = new SqlDataAdapter(Consulta, cnn);
                dalistado.Fill(dtlistado);
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo realizar la operación");
                MessageBox.Show(e.ToString());
            }
            return dtlistado;
        }
        public static int Entero16(string StrSQL)
        {
            int num = 0;
            try
            {
                IDbConnection connection = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
                connection.Open();
                try
                {
                    IDbCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = StrSQL;

                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read() && !reader.IsDBNull(0))
                    {
                        num = Convert.ToInt16(reader.GetValue(0));
                    }
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + exception.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                connection.Close();
            }
            catch (Exception exception2)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + exception2.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return num;
        }

        public static bool TieneFilas(string StrSQL)
        {
            bool flag = false;
            try
            {
                IDbConnection connection = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
                connection.Open();
                try
                {
                    // RAAN 150207 CORRECCION DE ERROR
                    //                    IDbCommand command = new SqlCommand {
                    //                        Connection = connection,
                    //                        CommandText = StrSQL
                    //                    };
                    IDbCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = StrSQL;

                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        flag = true;
                    }
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + exception.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                connection.Close();
            }
            catch (Exception exception2)
            {
                MessageBox.Show("Ocurrio el siguiente error al intentar conectar con la base de datos:\n" + exception2.Message, "Error de conexi\x00f3n", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return flag;
        }
        public static string[] CadenaArrayFila(string StrSQL)
        {
            string[] Salida = null;

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataReader lee;

            ArrayList Listado = new ArrayList();

            cnn = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
            cnn.Open();
            
            cmd = new SqlCommand(StrSQL, cnn);
            lee = cmd.ExecuteReader();
            lee.Read();
            if (lee.FieldCount > 0)
            {
                Salida = new string[lee.FieldCount];
                for (int i = 0; i < lee.FieldCount; i++)
                {
                    try
                    {
                        Salida[i] = lee.GetValue(i).ToString();
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            lee.Close();
            return Salida;
        }
        public static Bitmap imagen(string StrSQL)
        {
            SqlConnection cnn;
            Bitmap Salida = null;
            DataTable dtImagenes = new DataTable();
            ArrayList listaImagenes = new ArrayList();
            try
            {
                cnn = new SqlConnection("Server=" + SQLInstancia + "; database=" + sBaseERP + "; user=" + SQLUsuario + "; pwd=" + SQLPassword);
                cnn.Open();
                try
                {
                    SqlDataAdapter dalistado = new SqlDataAdapter(StrSQL, cnn);
                    dalistado.Fill(dtImagenes);
                    foreach (DataRow fila in dtImagenes.Rows)
                    {
                        byte[] datos = new byte[0];
                        datos = (byte[])fila[0];
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(datos);
                        listaImagenes.Add(System.Drawing.Bitmap.FromStream(ms));
                    }
                    try
                    {
                        Salida = (Bitmap)listaImagenes[0];
                    }
                    catch
                    {
                    }
                }
                catch (Exception ex1)
                {
                    MessageBox.Show("Ocurrio el siguiente error al intentar cargar los datos:\n" + ex1.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                cnn.Close();
            }
            catch //(Exception ex)
            {
            }
            return Salida;
        }
    }
}

