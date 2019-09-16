using System;
using System.Data.SqlClient;

namespace WpfApplication1 {
    class clsSQL {


        static clsIni Ini = new clsIni();

        private static string Conn() {
            string host = clsGloVar.str_host;
            string catalog = clsGloVar.str_catalog;
            string usrid = clsGloVar.str_UserID;
            string pword = clsGloVar.str_DBPassword;

            if (string.IsNullOrEmpty(pword) == false) {
                pword = clsCryptography.Decrypt(pword);
            }

            string cns = "Data Source=" + host + "; Initial Catalog=" + catalog + "; User ID=" + usrid + "; Password=" + pword + ";";
            return cns;
        }

        public static bool Connect() {
            try {
                using (SqlConnection cn = new SqlConnection(Conn())) {
                        cn.Open();
                        return true;
                }
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: Connect, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false; 
            } 
        }
        
        public static int Cmd(string qry) {
            try {
                using (SqlConnection cn = new SqlConnection(Conn())) {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(qry, cn)) {
                        return cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception ek) {
                clsGloFun.ErrorLogs("Cmd() " + ek.ToString() );
                return -1;
            }
        }

        //Treshold - wag na mag ganito.. CMS na mag monitor 
        //Summary Screen(Full na yung isang denomination) - 
    }
}
