using System;
using System.IO;

namespace WpfApplication1 {
    class clsLogWriter {
        public static void LogWriter(string logMessage, string type) {
            try {
                string filename = string.Empty;
                filename = clsGloVar.str_DateToday;

                using (StreamWriter SW = File.AppendText(clsGloVar.str_TransLog + "\\" + filename + ".txt")) {
                    SW.WriteLine("{0} {1}: {2}", DateTime.Now.ToString("HH:mm:ss"), type, logMessage);
                }
            } catch (Exception ex) {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
