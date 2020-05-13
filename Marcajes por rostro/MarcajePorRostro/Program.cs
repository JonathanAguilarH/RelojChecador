namespace MarcajePorRostro
{
    using System;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipal());
        }

        // FUNCION COPIADA DE PROYECTO FIM.Programa Ln5384 el 150211 PARA DESPLEGAR TIEMPO TRABAJADO 


        // FUNCION COPIADA DE PROYECTO FIM.Programa 150209 PARA DESPLEGAR TIEMPO TRABAJADO 
        public static string FormateoFecha(DateTime tFecha)
        {
            try
            {
                string sFecha;
                string tStr;

                sFecha = Convert.ToString(tFecha.Year);
                tStr = Convert.ToString(tFecha.Month);
                if (tStr.Length == 1)
                    tStr = "0" + tStr;
                sFecha = sFecha + tStr;
                tStr = Convert.ToString(tFecha.Day);
                if (tStr.Length == 1)
                    tStr = "0" + tStr;
                sFecha = sFecha + tStr;

                return sFecha;
            }
            catch
            {
                return "20090101";
            }
        }

        // FUNCION COPIADA DE PROYECTO FIM.Programa 150209 PARA DESPLEGAR TIEMPO TRABAJADO 
        public static string FormateoFechaHora(DateTime tFecha)
        {
            try
            {
                string sFecha;
                string tStr;

                sFecha = Convert.ToString(tFecha.Year);
                tStr = Convert.ToString(tFecha.Month);
                if (tStr.Length == 1)
                    tStr = "0" + tStr;
                sFecha = sFecha + tStr;
                tStr = Convert.ToString(tFecha.Day);
                if (tStr.Length == 1)
                    tStr = "0" + tStr;
                sFecha = sFecha + tStr + " ";
                tStr = Convert.ToString(tFecha.Hour);
                if (tStr.Length == 1)
                    tStr = "0" + tStr;
                sFecha = sFecha + tStr + ":";
                tStr = Convert.ToString(tFecha.Minute);
                if (tStr.Length == 1)
                    tStr = "0" + tStr;
                sFecha = sFecha + tStr + ":00";

                return sFecha;
            }
            catch
            {
                return "20090101 12:00:00";
            }
        }
    }
}

