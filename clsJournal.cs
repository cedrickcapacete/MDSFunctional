using System;
using System.IO;

namespace WpfApplication1 {
    class clsJournal {
        public static void JournalWriter(string journalMessage) {
            try {
                string filename = string.Empty;
                filename = clsGloVar.str_DateToday;

                using (StreamWriter SW = File.AppendText(clsGloVar.str_JournalPath + "\\" + filename + ".txt")) {
                    SW.WriteLine("{0}: {1}", DateTime.Now.ToString("HH:mm:ss"), journalMessage);
                }
            } catch (Exception ex) {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}

