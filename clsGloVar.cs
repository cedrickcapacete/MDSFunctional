using System.Collections.Generic;

namespace WpfApplication1 {
    public class clsGloVar {

        #region Main Setting Variables 
        //###########################################################################################################################
        public static string str_configPathFile = @"E:\MDS_APPINI\config.ini"; //THIS IS CONSTANT 
        public static string str_transPathFile = @"E:\MDS_APPINI\transaction.ini"; //THIS IS CONSTANT 
        public static string str_bankParamPathFile = @"E:\MDS_APPINI\bankparam.ini"; //THIS IS CONSTANT 


        public static string str_templatePathFile = @"E:\MDS_APPINI\default.rbd"; //THIS IS CONSTANT 
        public static string str_TransLog = @"E:\MDS_Files\TransLogs\"; //THIS IS CONSTANT 

        public static string str_chequeDefaultText = "";
        public static string str_LogPath = @"";
        public static string str_TrnxPath = @"";
        public static string str_JournalPath = @"";
        public static string str_FixID = ""; //Cannot Be EMPTY
        public static string str_MachineID = ""; //Cannot Be EMPTY
        public static ushort ushrt_ComPort = 1; //Cannot Be EMPTY
        public static string str_CutOffTime = "15"; //Cannot Be EMPTY
        public static ushort ushrt_Endorsement = 0; //Cannot Be EMPTY
        public static ushort uint_CheckLimit = 0; //Cannot Be EMPTY
        #endregion

        #region Machine Parameters
        public static string str_ipaddress = "";
        public static string str_macaddress = "";
        public static string str_pcname = "";
        #endregion

        #region Database
        public static uint uint_useDB = 0;
        public static string str_host = "";
        public static string str_catalog = "";
        public static string str_UserID = "";
        public static string str_DBPassword = "";
        #endregion

        #region Bank Parameters
        public static string str_BankName = "";
        public static string str_BankAbbr = "";
        public static string str_BranchName = "";
        public static string str_BRSTN = "";
        #endregion

        #region Others Settings
        //###########################################################################################################################
        public static int int_AccountNoLength = 12; //Sample Only
        public static int int_BillerAccountNoLength = 12; //Sample Only
        public static int int_ChequeAmountLength = 11; //Sample Only
        #endregion

        #region Time Out Variables
        //###########################################################################################################################
        //Default Time Out Value is 30
        public static ushort ushrt_InsertItems_TO = 30;
        public static ushort ushrt_CleanFeeder_TO = 30;
        public static ushort ushrt_RepositionDocuments_TO = 30;
        public static ushort ushrt_TakeReturnedItems_TO = 30;
        public static ushort ushrt_Page_TO = 25;
        #endregion

        #region Transaction Variables 
        //###########################################################################################################################
        public static string str_DateToday = "20180101"; //Default Date
        public static string str_TransTypeMode = "CIM"; //Default TransMode (Either "CIM" - CASH / "IMP" - CHEQUE / "MIX" - MIX)
        public static string str_CardType = "NONCARD"; //Default CardType (NONCARD/CARD)
        public static string str_TrasactionType = "DEP"; //Default TransType (Either "DEP" - Deposit / "BPM" - Bills Payment)
        public static string str_AccountType = "SAV"; //Default AccType (Either "SAV" - SAVINGS / "CUR" - CURRENT)
        public static string str_AccountNumber = "";
        public static string str_BillerAccountNumber = "";

        public static string str_SummaryAnswer = ""; // ACCEPT/ADD/CANCEL/TO

        public static string str_TotalCashAmount = "";
        public static string str_TotalChequeAmount = "";

        public static uint uint_TotalNoOfCash = 0;
        public static uint uint_TotalNoOfCheque = 0;
        public static uint uint_TotalNoOfGoodCheque = 0;

        public static uint uint_ChequeInBox = 0;
        public static uint uint_NotesInBox1 = 0;
        public static uint uint_NotesInBox2 = 0;
        public static uint uint_NotesInBox3 = 0;
        public static uint uint_NotesInBox4 = 0;
        public static uint uint_NotesInBox5 = 0;

        public static ushort ushrt_IsCutOff = 0;

        public static string str_CashEscrow = "";
        public static string str_ChequeEscrow = "";
        public static string str_Content = "";

        public static List<string> lst_CheckEscrow = new List<string>();
        public static List<string> lst_CashEscrow = new List<string>();
        #endregion

        #region Page Variables 
        //###########################################################################################################################
        public static bool bl_Process = false; 
        public static bool bl_TimeOutBa = false;
        public static string str_Page_Name = ""; //Default Mode
        #endregion
    }
}
