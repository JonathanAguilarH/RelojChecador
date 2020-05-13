namespace MarcajePorRostro
{
    using System;

    public class FormateaFecha
    {
        public static string fechaLetras(string fecha)
        {
            string strFecha = "1 de Enero del 2017";
            string dia = fecha.Substring(6, 2);
            string mes = fecha.Substring(4, 2);
            string year = fecha.Substring(0, 4);
            return strFecha = dia + "/" + mes + "/" + year;
        }
        public static string aFechaConDiagonales(DateTime tFecha)
        {
            try
            {
                string str2 = Convert.ToString(tFecha.Day);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                string str = str2 + "/";
                str2 = Convert.ToString(tFecha.Month);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                return (str + str2 + "/" + Convert.ToString(tFecha.Year));
            }
            catch
            {
                return "01/01/2009";
            }
        }

        public static string aFechaHoraconDiagonales(DateTime tFecha)
        {
            try
            {
                string str2 = Convert.ToString(tFecha.Day);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                string str = str2 + "/";
                str2 = Convert.ToString(tFecha.Month);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                str = str + str2 + "/" + Convert.ToString(tFecha.Year);
                str2 = Convert.ToString(tFecha.Hour);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                str = str + " " + str2 + ":";
                str2 = Convert.ToString(tFecha.Minute);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                return (str + str2);
            }
            catch
            {
                return "01/01/2009 08:00";
            }
        }

        public static string aFechaHoraUniversal(DateTime tFecha)
        {
            try
            {
                string str = Convert.ToString(tFecha.Year);
                string str2 = Convert.ToString(tFecha.Month);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                str = str + str2;
                str2 = Convert.ToString(tFecha.Day);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                str = str + str2 + " ";
                str2 = Convert.ToString(tFecha.Hour);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                str = str + str2 + ":";
                str2 = Convert.ToString(tFecha.Minute);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                return (str + str2 + ":00");
            }
            catch
            {
                return "20090101 12:00:00";
            }
        }

        public static string aFechaUniversal(DateTime tFecha)
        {
            try
            {
                string str = Convert.ToString(tFecha.Year);
                string str2 = Convert.ToString(tFecha.Month);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                str = str + str2;
                str2 = Convert.ToString(tFecha.Day);
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                return (str + str2);
            }
            catch
            {
                return "20090101";
            }
        }
    }
}

