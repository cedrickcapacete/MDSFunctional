using System;
using System.IO;
using System.Linq;

namespace WpfApplication1 {
    static class clsGloFun {
        
        static clsIni Ini = new clsIni();

        public static void LogWrite(string text, string type) {
            clsLogWriter.LogWriter(text, type); // INFO, WARN, EROR 
        }

        public static void JournalWrite(string text) {
            clsJournal.JournalWriter(text);
        }

        public static string CurrencyFormat(string input, bool WithDecimal) {
            string output = "";
            //input.All(char.IsNumber);
            int dec = 2;

            if (WithDecimal) dec = 0;

            if (input.Length > 8 - dec) {
                output = input.Insert((input.Length - (8 - dec)), ",");
                input = output;
            }

            if (input.Length > 5 - dec) {
                output = input.Insert((input.Length - (5 - dec)), ",");
                input = output;
            }

            if(WithDecimal) {
                output = input.Insert((input.Length - (2 - dec)), ".");
            } else {
                output = input + ".00";
            }
            
            return output;
        }

        public static bool IsPathExists(string pth) {
            try {
                if (Directory.Exists(pth) == true) {
                    return true;
                } else {
                    return false;
                }
            } catch {
                return false;
            }
        }

        public static void ErrorLogs(string xerr)  {
            string app = Environment.CurrentDirectory;
            if (app.EndsWith("\\") == false) app += "\\";

            if (CreateFolder(app + "logs") == false) return;

            string flog = app +"logs\\"+ DateTime.Now.ToString("yyyyMMdd") + ".txt";

            try {
                using (StreamWriter sw = new StreamWriter(flog, true)) {
                    sw.WriteLine("[" + DateTime.Now.ToString() + "] " + xerr);
                }
            } catch { }
        }

        public static bool CreateFolder(string xfolder) {
            try {
                if (Directory.Exists(xfolder) == false) 
                    { Directory.CreateDirectory(xfolder); return true; } 
                else return true;
                
            }
            catch { return false; }
        }

        public static bool SetGlobalVariables() {
            try {
                LogWrite("-----", "INFO");
                LogWrite("Setting Global Variables - START", "INFO");

                //CONSTANT PATH OF THE CONFIG FILE
                Ini.IniFile(clsGloVar.str_configPathFile);

                //Properties of the MDS Settings
                clsGloVar.str_LogPath = Ini.IniReadValue("PROPERTIES", "LogPath"); //Set LogPath
                LogWrite("Log Path: " + clsGloVar.str_LogPath, "INFO");

                clsGloVar.str_TrnxPath = Ini.IniReadValue("PROPERTIES", "TrnxPath"); //Set TrnxPath
                LogWrite("Trnx Path: " + clsGloVar.str_TrnxPath, "INFO");
                
                clsGloVar.str_JournalPath = Ini.IniReadValue("PROPERTIES", "JournalPath"); //Set JournalPath
                LogWrite("Journal Path: " + clsGloVar.str_JournalPath, "INFO");
                
                clsGloVar.str_chequeDefaultText = Ini.IniReadValue("PROPERTIES", "ChequeDefaultText"); //Set Default Text
                LogWrite("Cheque Def Text: " + clsGloVar.str_chequeDefaultText, "INFO");

                //Set FIX ID
                if (Ini.IniReadValue("PROPERTIES", "FixID") != "") {
                    clsGloVar.str_FixID = Ini.IniReadValue("PROPERTIES", "FixID");
                    LogWrite("Fix ID: " + clsGloVar.str_FixID, "INFO");
                } else {
                    LogWrite("Function: SetGlobalVariables, Error Message: Fix ID not found", "EROR");
                    return false;
                }

                //Set Machine ID
                if (Ini.IniReadValue("PROPERTIES", "MachineID") != "") {
                    clsGloVar.str_MachineID = Ini.IniReadValue("PROPERTIES", "MachineID");
                    LogWrite("Machine ID: " + clsGloVar.str_MachineID, "INFO");
                } else {
                    LogWrite("Function: SetGlobalVariables, Error Message: Machine ID not found", "EROR");
                    return false;
                }

                //Set ComPort
                if (Ini.IniReadValue("PROPERTIES", "ComPort") != "") { 
                    clsGloVar.ushrt_ComPort = Convert.ToUInt16(Ini.IniReadValue("PROPERTIES", "ComPort"));
                    LogWrite("Comport: " + clsGloVar.ushrt_ComPort, "INFO");
                } else {
                    LogWrite("Function: SetGlobalVariables, Error Message: ComPort not found", "EROR");
                    return false;
                }

                //Set Endorsement Setting
                if (Ini.IniReadValue("PROPERTIES", "Endorsement") != "") {
                    clsGloVar.ushrt_Endorsement = Convert.ToUInt16(Ini.IniReadValue("PROPERTIES", "Endorsement"));
                    LogWrite("Endorsement: " + clsGloVar.ushrt_Endorsement, "INFO");
                } else {
                    LogWrite("Function: SetGlobalVariables, Error Message: Endorsement not found", "EROR");
                    return false;
                }

                //Set Cheque Printing Limit in Receipt
                if (Ini.IniReadValue("PROPERTIES", "CheckLimit") != "") {
                    clsGloVar.uint_CheckLimit = Convert.ToUInt16(Ini.IniReadValue("PROPERTIES", "CheckLimit"));
                    LogWrite("CheckLimit: " + clsGloVar.uint_CheckLimit, "INFO");
                } else {
                    LogWrite("Function: SetGlobalVariables, Error Message: CheckLimit not found", "EROR");
                    return false;
                }

                //Timeout settings
                if (Ini.IniReadValue("TIMEOUT", "InsertItems") != "")
                    clsGloVar.ushrt_InsertItems_TO = Convert.ToUInt16(Ini.IniReadValue("TIMEOUT", "InsertItems")); //Set Insert Item Timeout
                if (Ini.IniReadValue("TIMEOUT", "CleanFeeder") != "")
                    clsGloVar.ushrt_CleanFeeder_TO = Convert.ToUInt16(Ini.IniReadValue("TIMEOUT", "CleanFeeder")); //Set Clean Feeder Timeout
                if (Ini.IniReadValue("TIMEOUT", "RepositionDocuments") != "")
                    clsGloVar.ushrt_RepositionDocuments_TO = Convert.ToUInt16(Ini.IniReadValue("TIMEOUT", "RepositionDocuments")); //Set Reposition Doc Timeout
                if (Ini.IniReadValue("TIMEOUT", "TakeReturnedItems") != "")
                    clsGloVar.ushrt_TakeReturnedItems_TO = Convert.ToUInt16(Ini.IniReadValue("TIMEOUT", "TakeReturnedItems")); //Set Take Return Item Timeout
                LogWrite("InsertItems: " + clsGloVar.ushrt_InsertItems_TO, "INFO");
                LogWrite("CleanFeeder: " + clsGloVar.ushrt_CleanFeeder_TO, "INFO");
                LogWrite("RepositionDocuments: " + clsGloVar.ushrt_RepositionDocuments_TO, "INFO");
                LogWrite("TakeReturnedItems: " + clsGloVar.ushrt_TakeReturnedItems_TO, "INFO");
                
                //Timeout for the pages before cancelling the transaction
                if (Ini.IniReadValue("TIMEOUT", "PageTimeout") != "")
                    clsGloVar.ushrt_Page_TO = Convert.ToUInt16(Ini.IniReadValue("TIMEOUT", "PageTimeout")); //Set Page Timeout
                LogWrite("PageTimeout: " + clsGloVar.ushrt_Page_TO, "INFO");


                //Set Machine Variables
                clsPC pc = new clsPC();
                pc.WorkstationInfo();

                clsGloVar.str_ipaddress = pc.GetIPAddress;
                clsGloVar.str_macaddress = pc.GetMAC;
                clsGloVar.str_pcname = pc.GetPCname;

                //SetDB
                clsGloVar.uint_useDB = Convert.ToUInt16(Ini.IniReadValue("DATABASE", "UseDB"));
                clsGloVar.str_host = Ini.IniReadValue("DATABASE", "Host");
                clsGloVar.str_catalog = Ini.IniReadValue("DATABASE", "Catalog");
                clsGloVar.str_UserID = Ini.IniReadValue("DATABASE", "UserID");
                clsGloVar.str_DBPassword = Ini.IniReadValue("DATABASE", "Password");

                LogWrite("Setting Global Variables - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                LogWrite("Function: SetGlobalVariables, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }

        public static bool SetBankParameters() {
            try {
                LogWrite("-----", "INFO");
                LogWrite("Setting Bank Parameters - START", "INFO");

                //CONSTANT PATH OF THE CONFIG FILE
                Ini.IniFile(clsGloVar.str_bankParamPathFile);

                //Properties of the MDS Settings
                clsGloVar.str_BankName = Ini.IniReadValue("BANKINFO", "BankName");
                LogWrite("BankName: " + clsGloVar.str_BankName, "INFO");

                clsGloVar.str_BankAbbr = Ini.IniReadValue("BANKINFO", "BankABBR");
                LogWrite("Bank Abbr: " + clsGloVar.str_BankAbbr, "INFO");

                clsGloVar.str_BranchName = Ini.IniReadValue("BANKINFO", "BranchName");
                LogWrite("Branch Name: " + clsGloVar.str_BranchName, "INFO");

                clsGloVar.str_BRSTN = Ini.IniReadValue("BANKINFO", "BRSTN");
                LogWrite("BRSTN: " + clsGloVar.str_BRSTN, "INFO");

                clsGloVar.str_BRSTN = Ini.IniReadValue("BANKINFO", "BRSTN");
                LogWrite("BRSTN: " + clsGloVar.str_BRSTN, "INFO");

                clsGloVar.str_CutOffTime = Ini.IniReadValue("CUTOFF", "CutOffTime");
                LogWrite("CutOffTime: " + clsGloVar.str_CutOffTime, "INFO");
                
                LogWrite("Setting Bank Parameters - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                LogWrite("Function: SetBankParameters, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }

        public static bool ResetGlobalVariables() {
            try {
                LogWrite("Resetting Global Variables - START", "INFO");

                clsGloVar.str_TransTypeMode = "CIM"; //Default TransMode (Either "CIM" - CASH / "IMP" - CHEQUE / "MIX" - MIX)
                clsGloVar.str_CardType = "NONCARD"; //Default CardType (NONCARD/CARD)
                clsGloVar.str_TrasactionType = "DEP"; //Default TransType (Either "DEP" - Deposit / "BPM" - Bills Payment)

                clsGloVar.str_AccountType = "SAV"; //Default AccType (Either "SAV" - SAVINGS / "CUR" - CURRENT)
                clsGloVar.str_AccountNumber = "";
                clsGloVar.str_BillerAccountNumber = "";

                clsGloVar.str_TotalCashAmount = "";
                clsGloVar.str_TotalChequeAmount = "";

                clsGloVar.uint_TotalNoOfCash = 0;
                clsGloVar.uint_TotalNoOfCheque = 0;
                clsGloVar.uint_TotalNoOfGoodCheque = 0;

                clsGloVar.uint_ChequeInBox = 0;
                clsGloVar.uint_NotesInBox1 = 0;
                clsGloVar.uint_NotesInBox2 = 0;
                clsGloVar.uint_NotesInBox3 = 0;
                clsGloVar.uint_NotesInBox4 = 0;
                clsGloVar.uint_NotesInBox5 = 0;

                clsGloVar.str_CashEscrow = "";
                clsGloVar.str_ChequeEscrow = "";
                clsGloVar.str_Content = "";

                clsGloVar.lst_CheckEscrow.Clear();
                clsGloVar.lst_CashEscrow.Clear();

                clsGloVar.str_SummaryAnswer = ""; // ACCEPT/ADD/CANCEL/TO
                clsGloVar.ushrt_IsCutOff = 0;
                clsGloVar.bl_TimeOutBa = false;

                LogWrite("Resetting Global Variables - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                LogWrite("Function: ResetGlobalVariables, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }



        public static string getTransactionNo() {
            try {
                LogWrite("Getting Transaction Number  - START", "INFO");
                string TranxNo = string.Empty;

                Ini.IniFile(clsGloVar.str_transPathFile);
                TranxNo = Ini.IniReadValue("CONSTANT", "TranNo");
                LogWrite("Transaction Number: " + TranxNo, "INFO");

                LogWrite("Getting Transaction Number  - DONE", "INFO");
                return TranxNo;
            } catch (Exception ex) {
                LogWrite("Function: getTransactionNo, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return "";
            }
        }

        public static void updateTransactionNo() {

            try {
                LogWrite("Updating Transaction Number  - START", "INFO");
                int TranxNo = Convert.ToInt32(getTransactionNo()); 
                    
                if(TranxNo == 999999) {
                    TranxNo = 1;
                } else {
                    TranxNo = TranxNo + 1;
                }
                
                LogWrite("Updated Transaction Number: " + TranxNo, "INFO");
                Ini.IniFile(clsGloVar.str_transPathFile);
                Ini.IniWriteValue("CONSTANT", "TranNo", TranxNo.ToString());

                LogWrite("Updating Transaction Number  - DONE", "INFO");
            } catch (Exception ex) {
                LogWrite("Function: updateTransactionNo, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
            }

        }

        public static string getSequenceNo() {
            try {
                LogWrite("Getting Sequence Number  - START", "INFO");
                string SeqNo = string.Empty;

                Ini.IniFile(clsGloVar.str_transPathFile);
                SeqNo = Ini.IniReadValue("CONSTANT", "SeqNo");
                LogWrite("Sequence Number: " + SeqNo, "INFO");

                LogWrite("Getting Sequence Number  - DONE", "INFO");
                return SeqNo;
            } catch (Exception ex) {
                LogWrite("Function: getSequenceNo, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return "";
            }
        }

        public static void updateSequenceNo() {
            try {
                LogWrite("Updating Sequence Number  - START", "INFO");
                int SeqNo = Convert.ToInt32(getSequenceNo());
                
                if (SeqNo == 999999) {
                    SeqNo = 1;
                } else {
                    SeqNo = SeqNo + 1;
                }

                LogWrite("Updated Transaction Number: " + SeqNo, "INFO");
                Ini.IniFile(clsGloVar.str_transPathFile);
                Ini.IniWriteValue("CONSTANT", "SeqNo", SeqNo.ToString());

                LogWrite("Updating Sequence Number  - DONE", "INFO");
            } catch (Exception ex) {
                LogWrite("Function: updateSequenceNo, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
            }

        }
    }
}
