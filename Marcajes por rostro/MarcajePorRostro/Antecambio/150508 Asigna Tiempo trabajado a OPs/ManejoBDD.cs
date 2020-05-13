namespace MarcajePorRostro
{
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;

    public class ManejoBDD
    {
        public static bool bddWEB = false;
        public static string MySQLPassword;
        public static string MySQLServidor;
        public static string MySQLUsuario;
        public static string sBaseERP = "FIM";
        public static string sBaseMySQL = "clubatle_FIM";
        public static string SQLInstancia;
        public static string SQLPassword;
        public static string SQLUsuario;
        public static string sRutaArchivos;

        public static bool AccionQuery(string StrSQL)
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
                string name = @"SOFTWARE\Meyaj\fim";
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
    }
}

