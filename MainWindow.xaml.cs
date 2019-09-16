
//PAGE NAME = "MAIN_WINDOW"

using System;
using System.Windows;
using MDS_Wrapper_Control;
using System.Windows.Navigation;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace WpfApplication1 {
    public partial class MainWindow : Window {

        static MDSControl Mds1 = new MDSControl();
        
        public MainWindow() {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {

                DateTime ekek = Convert.ToDateTime("05/15/2017");

                //Set the date today
                clsGloVar.str_DateToday = DateTime.Now.ToString("yyyyMMdd");
                clsGloFun.LogWrite("MDS STARTING..", "INFO");
                
                //Log the Current Page
                clsGloVar.str_Page_Name = "MAIN_WINDOW";
                clsGloFun.LogWrite("#MAIN WINDOW PAGE", "INFO");
                
                //Set Global Variable value
                if (!clsGloFun.SetGlobalVariables()) {
                    NavigateToPage("Page_OutOfService.xaml");
                    return;
                }
                
                //Set Bank Param value
                if (!clsGloFun.SetBankParameters()) {
                    NavigateToPage("Page_OutOfService.xaml");
                    return;
                }

                //Set the Parameters of the MDS
                if (!setMDSParam()) {
                    NavigateToPage("Page_OutOfService.xaml");
                    return;
                }

                
                //For Centralized Monitoring System
                ////Log in the DB that the machine got started
                //if (clsGloVar.uint_useDB == 1) {//Check if need to connect to DB
                //    if (clsSQL.Connect()) {
                //        string qr = "insert into tblStatus (ipaddress,macaddress,machineid,pcname,location,errorcode,errordesc,datetime) values ('" + clsGloVar.str_ipaddress + "','" + clsGloVar.str_macaddress + "','" + clsGloVar.str_MachineID + "','" + clsGloVar.str_pcname + "','Makati','111','Machine Started','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "')";

                //        //string qr = "insert into tblStatus (ipaddress,macaddress,machineid,pcname,location,errorcode,errordesc,datetime) values ('" + clsGloVar.str_ipaddress + "','" + clsGloVar.str_macaddress + "','" + clsGloVar.str_MachineID + "','" + clsGloVar.str_pcname + "','Makati','EROR 404','Sample error description','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "')";
                //        //string qr = "insert into tblTransaction (datetime,machineid,billsaccno,depoaccno,denomination,cassetno,count,transactionno) values ('" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "','meralco','acountkoto','" + clsGloVar.str_AccountNumber + "','DENO50','CASSETTE5','3','Transacione5')";
                //        int rows = clsSQL.Cmd(qr);
                //    }
                //}

                FrameWithinGrid.NavigationUIVisibility = NavigationUIVisibility.Hidden;
                NavigateToPage("Page_Initialization.xaml");
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: Window_Loaded, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                //Go to Out of Service Screen
            }
        }


        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",typeof(string));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private void Window_Closed(object sender, EventArgs e) {
            try {
                //MessageBox.Show(Mds1.MDSStatusText);
                if (Mds1.MDSStatusText == "OK") {
                    Mds1_Lights(1);
                    Mds1.Stop();
                }
            } catch (Exception ex) {
                //Go to Out of Service Screen
            }
        }

        public static void closeMDSprogram() {
            try {
                Application.Current.Shutdown();
            } catch (Exception ex) {
                //Go to Out of Service Screen
            }
        }

        public void NavigateToPage(string pageName) {
            FrameWithinGrid.Navigate(new Uri(pageName, UriKind.RelativeOrAbsolute));
        }

        #region MDS Functions -----------------------------------------------------------------------------------------------------------------
        public bool setMDSParam() {
            try {
                clsGloFun.LogWrite("-----", "INFO");
                clsGloFun.LogWrite("Setting MDS Parameter - START","INFO");

                //1. Log Path Constant Value
                Mds1.LogPath = clsGloVar.str_LogPath;
                clsGloFun.LogWrite("MDS LogPath: " + Mds1.LogPath, "INFO");

                //2. Tranx Path Value
                if (clsGloFun.IsPathExists(clsGloVar.str_TrnxPath) == false) {
                    clsGloFun.LogWrite("Function: setParam. Message: Invalid Image Path", "WARN");
                    return false;
                } else {
                    Directory.CreateDirectory(clsGloVar.str_TrnxPath + "\\IMG\\");
                    Mds1.ImagePath = clsGloVar.str_TrnxPath + "\\IMG\\";
                    clsGloFun.LogWrite("MDS ImgPath: " + Mds1.ImagePath, "INFO");
                }

                // Journal Path Value
                if (clsGloFun.IsPathExists(clsGloVar.str_JournalPath) == false) {
                    clsGloFun.LogWrite("Function: setParam. Message: Invalid Journal Path", "WARN");
                    return false;
                } else {
                    Directory.CreateDirectory(clsGloVar.str_JournalPath);
                    clsGloFun.LogWrite("MDS JournalPath: " + Mds1.ImagePath, "INFO");
                }

                //3. Comport Value
                Mds1.ComPort = clsGloVar.ushrt_ComPort;
                clsGloFun.LogWrite("MDS ComPort: " + Mds1.ComPort, "INFO");

                //4. Binding a function to the event
                Mds1.StatusUpdate += new MDSControl.StatusUpdateEvent(Mds1_StatusUpdate);
                Mds1.UserNotification += new MDSControl.UserNotificationEvent(Mds1_UserNotification);
                Mds1.TransactionCompleted += new MDSControl.TransactionCompletedEvent(Mds1_TransactionCompleted);
                Mds1.InitialiseCompleted += new MDSControl.InitialiseCompletedEvent(Mds1_InitialiseCompleted);

                Mds1.Mode = "CIM";
                clsGloFun.LogWrite("MDS Mode: " + Mds1.Mode, "INFO");

                //5. Timeout Value
                Mds1.SetTimeout(MDSControl.UserNotificationOptions.InsertItems, clsGloVar.ushrt_InsertItems_TO);
                clsGloFun.LogWrite("MDS InsertItems Timeout: " + clsGloVar.ushrt_InsertItems_TO, "INFO");

                Mds1.SetTimeout(MDSControl.UserNotificationOptions.CleanFeeder, clsGloVar.ushrt_CleanFeeder_TO);
                clsGloFun.LogWrite("MDS CleanFeeder Timeout: " + clsGloVar.ushrt_CleanFeeder_TO, "INFO");

                Mds1.SetTimeout(MDSControl.UserNotificationOptions.RepositionDocuments, clsGloVar.ushrt_RepositionDocuments_TO);
                clsGloFun.LogWrite("MDS RepositionDocuments Timeout: " + clsGloVar.ushrt_RepositionDocuments_TO, "INFO");

                Mds1.SetTimeout(MDSControl.UserNotificationOptions.TakeReturnedItems, clsGloVar.ushrt_TakeReturnedItems_TO);
                clsGloFun.LogWrite("MDS TakeReturnedItems Timeout: " + clsGloVar.ushrt_TakeReturnedItems_TO, "INFO");
                
                clsGloFun.LogWrite("Setting MDS Parameter - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: setMDSParam, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }

        public static void InitializeMDS() {
            try {
                Mds1.Initialise();
            } catch (Exception ex) {
                //Go to Out of Service Page
           } 
        }

        public static void StartMDSTrans() {
            try {
                if (Mds1.MDSStatus == MDSControl.MDSStatusOptions.Ok) {
                    Mds1.Mode = clsGloVar.str_TransTypeMode;

                    if (clsGloVar.str_TransTypeMode == "IPM" || clsGloVar.str_TransTypeMode == "MIX") {
                        Mds1.TemplateFile = clsGloVar.str_templatePathFile;
                        clsGloFun.LogWrite("MDS1.TemplateFile: " + Mds1.TemplateFile, "INFO");
                        clsGloFun.LogWrite("str_templatePathFile: " + clsGloVar.str_templatePathFile, "INFO");
                    }

                    Mds1.TransactionStart();
                } else {
                    //Go to Out of Service Page
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }


        public static bool GetMDSNoteList() {
            try {
                clsGloFun.LogWrite("GetMDSNoteList - START", "INFO");

                long totalAmount = 0;
                uint totalNoOfCash = 0;
                //uint cassetteID = 0; //HINDI ACCURATE TO ----------------------------------------

                //The first test of gitasdfasdf

                clsGloVar.str_CashEscrow = "";

                //test for all checking
                foreach (MDSControl.NoteSet ns in Mds1.TransactionNoteList()) {
                    if (ns.Level.ToString() == "Good") {
                        
                        totalAmount = totalAmount + (ns.Value * ns.Number);
                        totalNoOfCash = totalNoOfCash + ns.Number;
                        
                        clsGloVar.str_CashEscrow = clsGloVar.str_CashEscrow + @"
1," + ns.Value.ToString() + "," + ns.Number.ToString() + "," + ns.Currency.ToString(); //DO NOT INDENT!!!!

                        
                        clsGloVar.lst_CashEscrow.Add(ns.Currency.ToString() + "," + ns.Value.ToString() + "," + ns.Number.ToString() + "," + (ns.Value * ns.Number));
                        
                        ////DATABASE!!
                        //if (clsGloVar.uint_useDB == 1) {
                        //    if (clsSQL.Connect()) {
                        //        string qr = "insert into tblTransaction (datetime,machineid,billsaccno,depoaccno,denomination,count,transactionno) values ('" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "','meralco','" + clsGloVar.str_AccountNumber + "','" + clsGloVar.str_AccountNumber + "','" + ns.Value + "','" + ns.Number + "','" + clsGloFun.getTransactionNo() + "')";
                        //        int rows = clsSQL.Cmd(qr);
                        //    }
                        //}
                    }
                }

                clsGloVar.uint_TotalNoOfCash = totalNoOfCash;
                clsGloVar.str_TotalCashAmount = totalAmount.ToString() + "00";


                clsGloFun.LogWrite("GetMDSNoteList - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: GetMDSNoteList, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }
        
        public static string GetMDSNoteListSummary() {
            try {
                string list = "";
                long totalAmount = 0;

                foreach (MDSControl.NoteSet ns in Mds1.TransactionNoteList()) {
                    list += string.Format("{0} - {1}.00 x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number);
                    totalAmount = totalAmount + (ns.Value * ns.Number);
                }
                clsGloVar.str_TotalCashAmount = totalAmount.ToString() + "00";
                return list;
            } catch (Exception ex) {
                //Go to Out of Service Page
                return "";
            }
        }

        public static bool GetMDSChequeList() {
            try {
                clsGloFun.LogWrite("GetMDSChequeList - START", "INFO");

                string frontFileName = "";
                string backImgFilename = "";
                string uvImgFilename = "";
                string chkno = "???????";
                string destinationPath = clsGloVar.str_TrnxPath + "\\" + clsGloVar.str_DateToday + "\\02" + clsGloFun.getTransactionNo().PadLeft(10, '0');

                uint totalNoOfGOODCheque = 0;
                clsGloVar.str_ChequeEscrow = "";
                int tempSeqNo;

                foreach (MDSControl.ChequeSet cs in Mds1.TransactionChequeList()) {
                    string MICRLine = "??????????";
                    if (cs.Status.ToString() == "Stored") {
                        if (clsMICR.ParseMICRData(cs.Codeline)) {
                            MICRLine = clsMICR.CheckNo + " " + clsMICR.RTNo + " " + clsMICR.AccountNo;
                            chkno = clsMICR.CheckNo;
                        }
                        
                        clsGloVar.lst_CheckEscrow.Add(cs.ImagePath + ',' + MICRLine);

                        frontFileName = destinationPath + "\\" + Path.GetFileName(cs.ImagePath);
                        backImgFilename = destinationPath + "\\" + Path.GetFileName(cs.ImagePath).Substring(0, Path.GetFileName(cs.ImagePath).Length - 5) + "R.bmp";
                        uvImgFilename = destinationPath + "\\" + Path.GetFileName(cs.ImagePath).Substring(0, Path.GetFileName(cs.ImagePath).Length - 5) + "U.bmp";
                        
                        tempSeqNo = Convert.ToInt32(clsGloFun.getSequenceNo());

                        clsGloVar.str_ChequeEscrow = clsGloVar.str_ChequeEscrow + @"
1," + cs.Codeline + "," + clsGloVar.str_FixID + clsGloVar.str_MachineID + clsGloVar.str_DateToday.Substring(clsGloVar.str_DateToday.Length-2) + tempSeqNo.ToString() + "," + frontFileName + "," + backImgFilename + "," + uvImgFilename;

                        clsGloFun.JournalWrite(tempSeqNo.ToString() + ": " + MICRLine);

                        clsGloFun.updateSequenceNo();
                        totalNoOfGOODCheque = totalNoOfGOODCheque + 1;
                    }
                }

                ////FOR Centralized Management System
                //if (clsGloVar.uint_useDB == 1) {
                //    if (clsSQL.Connect()) {
                //        string qr = "insert into tblTransaction (datetime,machineid,billsaccno,depoaccno,denomination,count,transactionno) values ('" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "','meralco','" + clsGloVar.str_AccountNumber + "','" + clsGloVar.str_AccountNumber + "','CHQ','" + totalNoOfGOODCheque + "','" + clsGloFun.getTransactionNo() + "')";
                //        int rows = clsSQL.Cmd(qr);
                //    }
                //}

                clsGloFun.LogWrite("Escrow Count: " + clsGloVar.lst_CheckEscrow.Count, "INFO");

                clsGloVar.uint_TotalNoOfGoodCheque = totalNoOfGOODCheque;
                clsGloFun.LogWrite("GetMDSChequeList - DONE", "INFO");

                return true;
            } catch (Exception ex) {
                //Go to Out of Service Page
                clsGloFun.LogWrite("Function: GetMDSChequeList, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }
        
        public static string GetMDSChequeListSummary() {
            string list = "";
            string chkno;
            uint count = 1;
            
            foreach (MDSControl.ChequeSet cs in Mds1.TransactionChequeList()) {
                chkno = "??????????";

                if (clsMICR.ParseMICRData(cs.Codeline)) {
                    chkno = clsMICR.CheckNo;
                }

                list += string.Format("{0}) {1} ({2})\n", count, chkno, cs.Status);
                count = count + 1;
            }
            return list;
        }

        public static void GetMDSChequeListPrintText() {
            clsGloFun.LogWrite("GetMDSChequeListPrintText - START", "INFO");

            int tempSeqNo = Convert.ToInt32(clsGloFun.getSequenceNo());       
            string print = DateTime.Now.ToString("MMddyy") + " " + clsGloVar.str_BRSTN.Insert(8, "-") + " REG PHP " + clsGloVar.str_FixID + clsGloVar.str_MachineID + clsGloVar.str_DateToday.Substring(clsGloVar.str_DateToday.Length - 2);
            foreach (MDSControl.ChequeSet cs in Mds1.TransactionChequeList()) {
                if (cs.Status.ToString() == "EscrowedGood") {
                    Mds1.PrintText(cs.ID, print + tempSeqNo + " NON-NEGOTIABLE");
                    tempSeqNo = tempSeqNo + 1;
                }
            }

            clsGloFun.LogWrite("GetMDSChequeListPrintText - END", "INFO");
        }

        public static void GetChequesInBox() {
            uint checkinbox = Mds1.ChequesInBox();

            clsGloFun.LogWrite("Cheques in box: " + checkinbox.ToString(), "INFO");

            ////FOR Centralized Management System
            //if (clsGloVar.uint_useDB == 1) {
            //    if (clsSQL.Connect()) {
            //        string qr = "insert into tblCheckBox (machineid,datetime,checkbox) values ('" + clsGloVar.str_MachineID + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "','" + checkinbox + "')";
            //        int rows = clsSQL.Cmd(qr);
            //    }
            //}
        }

        public static void GetNotesInBox() {
            long[] cassette = new long[5];
            string meowmeowmeow = "";
            
            foreach (MDSControl.NoteSet ns in Mds1.NotesInBox(0)) {
                clsGloFun.LogWrite("Notes in box 0: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString()), "INFO");
                meowmeowmeow = meowmeowmeow + "\nNotes in box 0: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString());
            }

            foreach (MDSControl.NoteSet ns in Mds1.NotesInBox(1)) {
                clsGloFun.LogWrite("Notes in box 1: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString()), "INFO");
                meowmeowmeow = meowmeowmeow + "\nNotes in box 1: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString());
            }

            foreach (MDSControl.NoteSet ns in Mds1.NotesInBox(2)) {
                clsGloFun.LogWrite("Notes in box 2: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString()), "INFO");
                meowmeowmeow = meowmeowmeow + "\nNotes in box 2: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString());
            }

            foreach (MDSControl.NoteSet ns in Mds1.NotesInBox(3)) {
                clsGloFun.LogWrite("Notes in box 3: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString()), "INFO");
                meowmeowmeow = meowmeowmeow + "\nNotes in box 3: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString());
            }

            foreach (MDSControl.NoteSet ns in Mds1.NotesInBox(4)) {
                clsGloFun.LogWrite("Notes in box 4: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString()), "INFO");
                meowmeowmeow = meowmeowmeow + "\nNotes in box 4: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString());
            }

            foreach (MDSControl.NoteSet ns in Mds1.NotesInBox(5)) {
                clsGloFun.LogWrite("Notes in box 5: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString()), "INFO");
                meowmeowmeow = meowmeowmeow + "\nNotes in box 5: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString());
            }

            foreach (MDSControl.NoteSet ns in Mds1.NotesInBox(6)) {
                clsGloFun.LogWrite("Notes in box 6: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString()), "INFO");
                meowmeowmeow = meowmeowmeow + "\nNotes in box 6: " + string.Format("{0} - {1} x {2}\n", ns.Currency, ns.Value.ToString(), ns.Number.ToString());
            }
            
            ////FOR Centralized Management System
            //if (clsGloVar.uint_useDB == 1) {
            //    if (clsSQL.Connect()) {
            //        string qr = "insert into tblCashBox (machineid,datetime,cassette1,cassette2,cassette3,cassette4,cassette5) values ('" + clsGloVar.str_MachineID + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "','" + cassette[0] + "','" + cassette[1] + "','" + cassette[2] + "','" + cassette[3] + "','" + cassette[4] + "')";
            //        int rows = clsSQL.Cmd(qr);
            //    }
            //}
        }
        #endregion

        #region User Options
        //###########################################################################################################################
        public static void userYes() {
            try {
                Mds1.UserChoice(MDSControl.UserOptions.Yes);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        public static void userNo() {
            try {
                Mds1.UserChoice(MDSControl.UserOptions.No);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        public static void userCancel() {
            try {
                Mds1.UserChoice(MDSControl.UserOptions.Cancel);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        public static void userTimeOut() {
            try {
                Mds1.UserChoice(MDSControl.UserOptions.Timeout);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        #endregion

        public static void Mds1_Lights(uint status) {
            if (Mds1.MDSStatusText == "OK") {
                switch (status) {
                    case 0:
                        Mds1.TopKioskLight(MDSControl.LightStatus.On);
                        break;
                    case 1:
                        Mds1.TopKioskLight(MDSControl.LightStatus.Off);
                        break;
                    case 2:
                        Mds1.TopKioskLight(MDSControl.LightStatus.FlashingFast);
                        break;
                    case 3:
                        Mds1.TopKioskLight(MDSControl.LightStatus.FlashingSlow);
                        break;
                }
            }
            
        }

        #region MDS Events --------------------------------------------------------------------------------------------------------------------
        void Mds1_StatusUpdate(object sender) {
            try {
                clsGloFun.LogWrite(String.Format("MDS status changed: {0}", Mds1.MDSStatusText), "INFO");
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        
        void Mds1_InitialiseCompleted(object sender) {
            try {
                //MDSStatusText will resutl either "OK" or "Disconnected"
                if (Mds1.MDSStatusText == "OK") {
                    if (clsGloVar.str_Page_Name == "INITIAZLIATION_WINDOW") {
                        Mds1_Lights(0);
                        NavigateToPage("Page_Advertisement.xaml");
                    } else {
                        if (clsGloVar.str_Page_Name == "MAINTENANCE_WINDOW") {
                            Mds1_Lights(0);
                            NavigateToPage("Page_Advertisement.xaml");
                        }
                    }
                } else {
                    NavigateToPage("Page_OutOfService.xaml");
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        void Mds1_UserNotification(object sender, MDSControl.UserNotificationOptions uno) {
            try {
                clsGloFun.LogWrite("Mds1_UserNotification: " + uno.ToString(), "INFO");

                switch (uno.ToString()) {
                    case "InsertItems":
                        //DO NOTHING
                        break;
                    case "CleanFeeder":
                        NavigateToPage("Page_CleanFeeder.xaml");
                        break;
                    case "RepositionDocuments":
                        NavigateToPage("Page_RepositionDocument.xaml");
                        break;
                    case "Processing":
                        NavigateToPage("Page_Processing.xaml");
                        break;
                    case "WantToInsertMoreQuestion":
                        if(clsGloVar.bl_TimeOutBa) {
                            userNo();
                            NavigateToPage("Page_TimeOut.xaml");
                            return;
                        }

                        NavigateToPage("Page_Summary.xaml");
                        break;
                    case "TakeReturnedItems":
                        NavigateToPage("Page_TakeReturnItem.xaml");
                        break;
                    case "NotifyCounterfeit":
                        NavigateToPage("Page_Counterfeit.xaml");
                        break;
                    case "CompleteDepositQuestion":
                        NavigateToPage("Page_Summary.xaml");

                        if(clsGloVar.str_SummaryAnswer == "ACCEPT") {
                            // IF check get default endoresement text
                            if (clsGloVar.str_TransTypeMode == "IPM") {
                                if (clsGloVar.ushrt_Endorsement == 1) {
                                    GetMDSChequeListPrintText();
                                }
                            }

                            userYes();
                        } else {
                            userNo();
                        }

                        break;
                    default:
                        MessageBox.Show(string.Format("User notification: {0}", uno.ToString()));
                        Console.WriteLine("Default case");
                        break;
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
         }
        
        void Mds1_TransactionCompleted(object sender, MDSControl.TransactionResult res) {
            try {
                clsGloFun.LogWrite("Mds1_TransactionCompleted - START", "INFO");
                if (res == MDSControl.TransactionResult.Ok) {
                    if (clsGloVar.str_SummaryAnswer == "ACCEPT") {
                        if (clsGloVar.str_TransTypeMode == "IPM") { //CHEQUE
                            if (!GetMDSChequeList()) {
                                clsGloFun.LogWrite("Function: GetMDSChequeList, Message: Transfer Cheque Image Failed", "WARN");
                            }

                            if (!TransferChequeImages()) {
                                clsGloFun.LogWrite("Function: TransferChequeImages, Message: Transfer Cheque Image Failed", "WARN");
                            }

                            if (!ConvertToTiff()) {
                                clsGloFun.LogWrite("Function: ConvertToTiff, Message: Image Convertion to TIFF Failed", "WARN");
                            }
                        } else {
                            GetMDSNoteList(); // CASH
                        }

                        if (!GenerateTransFile()) {
                            clsGloFun.LogWrite("Function: GenerateTransFile, Message: Printing Receipt Failed", "WARN");
                        }

                        clsGloFun.updateTransactionNo();

                        if (!PrintReceipt()) {
                            clsGloFun.LogWrite("Function: PrintReceipt, Message: Printing Receipt Failed", "WARN");
                        }

                        clsGloFun.LogWrite("Transaction completed successfully", "INFO");
                        NavigateToPage("Page_Advertisement.xaml");
                    } else {
                        if (clsGloVar.str_SummaryAnswer == "" || clsGloVar.str_SummaryAnswer == "ADD") {
                            clsGloFun.LogWrite("User Option: TIMEOUT - Summary", "INFO");
                            userTimeOut();
                            NavigateToPage("Page_TimeOut.xaml");
                        } else {
                            clsGloFun.LogWrite("Transaction was cancelled", "INFO");
                            NavigateToPage("Page_Advertisement.xaml");
                        }
                    }
                    
                } else {
                    clsGloFun.LogWrite("Function: Mds1_TransactionCompleted, Message: Transaction Failed","WARN");
                }

                clsGloFun.LogWrite("Mds1_TransactionCompleted - DONE", "INFO");

            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: Mds1_TransactionCompleted, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                //Go to Out of Service Page
            }
        }
        #endregion

        #region Transaction Functions
        public static bool TransferChequeImages() {
            try {
                clsGloFun.LogWrite("TransferChequeImages - START", "INFO");

                string mainFilePath = "";
                string mainFileName = "";
                string backImgFilename = "";
                string uvImgFilename = "";
                string destinationPath = clsGloVar.str_TrnxPath + "\\" + clsGloVar.str_DateToday + "\\02" + clsGloFun.getTransactionNo().PadLeft(10, '0');

                Directory.CreateDirectory(destinationPath);

                foreach (MDSControl.ChequeSet cs in Mds1.TransactionChequeList()) {
                    if (cs.Status.ToString() == "Stored") {

                        mainFilePath = Path.GetDirectoryName(cs.ImagePath);
                        clsGloFun.LogWrite("mainFilePath: " + mainFilePath, "INFO"); //E:\MDS_Files\MDS Data\IMG

                        mainFileName = Path.GetFileName(cs.ImagePath);
                        clsGloFun.LogWrite("mainFileName: " + mainFileName, "INFO"); //MDS19021516143101IF.bmp

                        backImgFilename = mainFileName.Substring(0, mainFileName.Length - 5) + "R.bmp";
                        clsGloFun.LogWrite("backImgFilename: " + backImgFilename, "INFO"); //MDS19021516143101IR.bmp
                        uvImgFilename = mainFileName.Substring(0, mainFileName.Length - 5) + "U.bmp";
                        clsGloFun.LogWrite("uvImgFilename: " + uvImgFilename, "INFO"); //MDS19021516143101IU.bmp

                        if (TransferImage(mainFilePath + "\\" + mainFileName, destinationPath + "\\" + mainFileName)) {
                            //LOG NA NA-COPY YUNG IMAGE
                            clsGloFun.LogWrite("Front IMG successfully transfered", "INFO");
                        } else {
                            //LOG NA HINDI NA-COPY YUNG IMAGE
                            clsGloFun.LogWrite("Front IMG was not transfered", "INFO");
                        }

                        if (TransferImage(mainFilePath + "\\" + backImgFilename, destinationPath + "\\" + backImgFilename)) {
                            //LOG NA NA-COPY YUNG IMAGE
                            clsGloFun.LogWrite("Back IMG successfully transfered", "INFO");
                        } else {
                            //LOG NA HINDI NA-COPY YUNG IMAGE
                            clsGloFun.LogWrite("Back IMG was not transfered", "INFO");
                        }

                        if (TransferImage(mainFilePath + "\\" + uvImgFilename, destinationPath + "\\" + uvImgFilename)) {
                            //LOG NA NA-COPY YUNG IMAGE
                            clsGloFun.LogWrite("UV IMG successfully transfered", "INFO");
                        } else {
                            //LOG NA HINDI NA-COPY YUNG IMAGE
                            clsGloFun.LogWrite("UV IMG was not transfered", "INFO");
                        }
                    }
                }

                clsGloFun.LogWrite("TransferChequeImages - END", "INFO");
                return true;
            } catch (Exception ex) {
                //Go to Out of Service Page
                return false;
            }
        }

        public static bool GenerateTransFile() {
            try {
                clsGloFun.LogWrite("GenerateTransFile - START", "INFO");
                
                string PATH = clsGloVar.str_TrnxPath + "\\" + clsGloVar.str_DateToday;   //  E:\MDS_Files\MDS Data\20181031
                string HEADER = "H" + clsGloVar.str_DateToday; //H 20191131 0000000009
                string TRAILER = "T";
                string BODY = "";
                string TranxFileName = "";
                string amount = "";
                string quantity = "";

                switch (clsGloVar.str_TransTypeMode) { //TRANSACTION NUMBER
                    case "CIM":
                        HEADER = HEADER + clsGloFun.getTransactionNo().PadLeft(10, '0') + "01"; //CASH
                        TranxFileName = "01" + clsGloFun.getTransactionNo().PadLeft(10, '0');
                        BODY = clsGloVar.str_CashEscrow;
                        amount = clsGloVar.str_TotalCashAmount;
                        quantity = Convert.ToString(clsGloVar.uint_TotalNoOfCash);
                        break;
                    case "IPM":
                        HEADER = HEADER + clsGloFun.getTransactionNo().PadLeft(10, '0') + "02"; //CHEQUE
                        TranxFileName = "02" + clsGloFun.getTransactionNo().PadLeft(10, '0');
                        BODY = clsGloVar.str_ChequeEscrow;
                        amount = clsGloVar.str_TotalChequeAmount;
                        quantity = Convert.ToString(clsGloVar.uint_TotalNoOfGoodCheque);
                        break;
                    case "MIX":
                        //HEADER = HEADER + clsGloFun.getTransactionNo().PadLeft(10, '0') + "03";
                        break;
                    default:
                        //Do Nothing
                        break;
                }

                switch (clsGloVar.str_TrasactionType) { //TRANSACTION TYPE
                    case "DEP":
                        HEADER = HEADER + "01" + clsGloVar.ushrt_IsCutOff;

                        switch(clsGloVar.str_AccountType) {
                            case "SAV":
                                TRAILER = TRAILER + "01";
                                break;
                            case "CUR":
                                TRAILER = TRAILER + "02";
                                break;
                            default:
                                //Do Nothing
                                break;
                        }

                        break;
                    case "BPM":
                        HEADER = HEADER + "02" + clsGloVar.ushrt_IsCutOff;
                        TRAILER = TRAILER + "00";
                        break;
                    default:
                        //Do Nothing
                        break;
                }

                TRAILER = TRAILER + clsGloVar.str_BillerAccountNumber.PadLeft(16, '0') + clsGloVar.str_AccountNumber.PadLeft(16, '0') + amount.PadLeft(11, '0') + quantity.PadLeft(7,'0');
                
                Directory.CreateDirectory(PATH);

                using (StreamWriter SW = File.AppendText(PATH + "\\" + TranxFileName + ".txt")) { //  E:\MDS_Files\20181031\010000000012.txt
                    SW.WriteLine(HEADER + BODY); //HEADER
                    SW.WriteLine(TRAILER); //TRAILER
                }

                clsGloFun.LogWrite("GenerateTransFile - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: GenerateTransFile, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }
        
        public static bool ConvertToTiff() {
            try {
                clsGloFun.LogWrite("ConvertToTiff - START", "INFO");

                //Convertion of BMP to TIFF--------------------------------
                ImageCodecInfo myImageCodecInfoTIFF;
                ImageCodecInfo myImageCodecInfoJPEG;
                EncoderParameters myEncoderParameters;

                myEncoderParameters = new EncoderParameters(2);
                myEncoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)EncoderValue.CompressionCCITT4); //Compression Type
                myEncoderParameters.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, (long)4); //Bit Rate
                myImageCodecInfoTIFF = GetEncoderInfo("image/tiff");
                myImageCodecInfoJPEG = GetEncoderInfo("image/jpeg");

                foreach (string line in clsGloVar.lst_CheckEscrow) {
                    string[] parts = line.Split(',');
                    
                    //Convert to TIFF and JPEG FRONT IMAGE
                    var frontimage = Image.FromFile(parts[0]);
                    frontimage.Save(parts[0].Substring(0, parts[0].Length - 4) + ".tif", myImageCodecInfoTIFF, myEncoderParameters); 
                    frontimage.Save(parts[0].Substring(0, parts[0].Length - 4) + ".jpg", myImageCodecInfoJPEG, myEncoderParameters); 
                    frontimage.Dispose();

                    //Convert to TIFF and JPEG BACK IMAGE
                    var backimage = Image.FromFile(parts[0].Substring(0, parts[0].Length - 5) + "R.bmp");
                    backimage.Save(parts[0].Substring(0, parts[0].Length - 5) + "R.tif", myImageCodecInfoTIFF, myEncoderParameters);
                    backimage.Save(parts[0].Substring(0, parts[0].Length - 5) + "R.jpg", myImageCodecInfoJPEG, myEncoderParameters);
                    backimage.Dispose();

                    //Convert to TIFF UV IMAGE
                    var uvimage = Image.FromFile(parts[0].Substring(0, parts[0].Length - 5) + "U.bmp");
                    uvimage.Save(parts[0].Substring(0, parts[0].Length - 5) + "U.tif", myImageCodecInfoTIFF, myEncoderParameters);
                    uvimage.Dispose();
                }
                
                clsGloFun.LogWrite("ConvertToTiff - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: ConvertToTiff, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
                }
            }

        public static bool PrintReceipt() {
            try {
                clsGloFun.LogWrite("PrintReceipt - START", "INFO");
                int xpos = 15;
                int ypos = 1;
                int spacing = 10;
                int chequespacing = 20;
                int fontsize = 8;
                int logoheight = 70;
                int logowidth = 265;
                int chequeheight = 110;
                int chequewidth = 265;
                string transMode = "";
                string transType = "";
                string AccType = "";

                string fontfamily = "Arial Unicode MS";
                int transNo = Convert.ToInt32(clsGloFun.getTransactionNo()) - 1;
                
                PrintDocument p = new PrintDocument();
                PrintController printController = new StandardPrintController();
                p.PrintController = printController;

                transType = (clsGloVar.str_TrasactionType == "DEP") ? "DEPOSIT" : "BILLSPAYMENT";
                transMode = (clsGloVar.str_TransTypeMode == "IPM") ? "CHEQUE" : "CASH";
                
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1) {
                    Image img = Image.FromFile(@"E:\MDS_APPINI\IMG\ReceiptHeader.bmp");
                    e1.Graphics.DrawImage(img, xpos, ypos, logowidth, logoheight); //x-pos, y-pos, width, height
                    ypos = ypos + logoheight + spacing;

                    img.Dispose();

                    //Date Time
                    e1.Graphics.DrawString("Date and Time: " + DateTime.Now.ToString("MMMM-dd-yyyy HH:mm:ss"), new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                    ypos = ypos + fontsize + spacing;

                    //Machine ID
                    e1.Graphics.DrawString("Machine ID: " + clsGloVar.str_MachineID, new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                    ypos = ypos + fontsize + spacing + fontsize + spacing;

                    //Transaction Type (DEPOSIT or BILLS PAYMENT)
                    e1.Graphics.DrawString("Transaction Type: " + transType, new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                    ypos = ypos + fontsize + spacing;

                    //Transaction Mode (CASH / CHEQUE)
                    e1.Graphics.DrawString("Transaction Mode: " + transMode, new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                    ypos = ypos + fontsize + spacing;

                    if (clsGloVar.str_TrasactionType == "DEP") {
                        //Account Number
                        string tempaccno = clsGloVar.str_AccountNumber;

                        if (tempaccno.Length > 4) {
                            tempaccno = string.Concat("".PadLeft(6, '*'), tempaccno.Substring(tempaccno.Length - 4));
                        } 

                        e1.Graphics.DrawString("Account Number: " + tempaccno, new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                        ypos = ypos + fontsize + spacing;

                        AccType = (clsGloVar.str_AccountType == "SAV") ? "SAVINGS" : "CURRENT";

                        //Account Type (Savings/Current)
                        e1.Graphics.DrawString("Account Type: " + AccType, new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                        ypos = ypos + fontsize + spacing;
                    } else {
                        //Subscriber's Account Number
                        e1.Graphics.DrawString("Subscriber's Account Number: " + clsGloVar.str_AccountNumber, new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                        ypos = ypos + fontsize + spacing;

                        //Biller's Account Number
                        e1.Graphics.DrawString("Biller's Account Number: " + clsGloVar.str_BillerAccountNumber, new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                        ypos = ypos + fontsize + spacing;
                    }

                    //Transaction Number
                    e1.Graphics.DrawString("Transaction Number: " + transNo.ToString(), new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                    ypos = ypos + fontsize + spacing + fontsize + spacing;



                    //Print if there is a Cheque
                    if (clsGloVar.str_TransTypeMode == "IPM") {
                        int bilang = 1;

                        e1.Graphics.DrawString("Amount: " + clsGloFun.CurrencyFormat(clsGloVar.str_TotalChequeAmount,true), new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                        ypos = ypos + fontsize + spacing + fontsize + spacing;

                        foreach (string line in clsGloVar.lst_CheckEscrow) {
                            string[] parts = line.Split(',');

                            //Cheque MICR
                            e1.Graphics.DrawString(bilang + ") " + parts[1], new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                            ypos = ypos + fontsize + spacing;

                            //Set limit to the IMAGE of Checks on the receipt
                            if (clsGloVar.uint_TotalNoOfGoodCheque <= clsGloVar.uint_CheckLimit) {
                                //Cheque Front Image
                                Image tempIMG = Image.FromFile(parts[0]);
                                e1.Graphics.DrawImage(tempIMG, xpos, ypos, chequewidth, chequeheight); //x-pos, y-pos, width, height
                                ypos = ypos + chequeheight + chequespacing;

                                tempIMG.Dispose();
                            }

                            bilang = bilang + 1;
                        }
                    } else {
                        if (clsGloVar.str_TransTypeMode == "CIM") {                            
                            foreach (string line in clsGloVar.lst_CashEscrow) {
                                string[] parts = line.Split(',');
                                
                                e1.Graphics.DrawString(parts[0] + " " + clsGloFun.CurrencyFormat(parts[1],false) + " x " + parts[2] + " = " + clsGloFun.CurrencyFormat(parts[3],false), new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                                ypos = ypos + fontsize + spacing;
                                
                            }

                            ypos = ypos + fontsize + spacing;

                            e1.Graphics.DrawString("Amount: " + clsGloFun.CurrencyFormat(clsGloVar.str_TotalCashAmount,true), new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                            ypos = ypos + fontsize + spacing + fontsize + spacing;
                        }
                    }


                    //Notice to Customers
                    ypos = ypos + spacing + spacing + spacing;
                    e1.Graphics.DrawString("NOTICE TO CUSTOMERS", new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                    ypos = ypos + fontsize + spacing;

                    //Enumeration
                    ypos = ypos + spacing + spacing;
                    e1.Graphics.DrawString("1. Please retain receipt for your record.", new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                    ypos = ypos + fontsize + spacing;

                    //Page Number
                    ypos = ypos + spacing + spacing + spacing;
                    e1.Graphics.DrawString("Page 1/1", new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                    ypos = ypos + fontsize + spacing;

                    e1.Graphics.DrawString("_", new Font(fontfamily, fontsize), new SolidBrush(Color.Black), xpos, ypos);
                };
                
                p.Print();
                p.Dispose();
                
                clsGloFun.LogWrite("PrintReceipt - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: PrintReceipt, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }
        #endregion


        #region MDS Exchange Functions
        public static bool MDS_StartExchange() {
            try {
                clsGloFun.LogWrite("MDS_StartExchange - START", "INFO");
                Mds1.StartExchange();
                clsGloFun.LogWrite("MDS_StartExchange - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: MDS_StartExchange, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }

        public static bool MDS_EndExchange() {
            try {
                clsGloFun.LogWrite("MDS_EndExchange - START", "INFO");
                Mds1.EndExchange();
                clsGloFun.LogWrite("MDS_EndExchange - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: MDS_EndExchange, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }
        #endregion


        #region Misc Functions
        private static ImageCodecInfo GetEncoderInfo(String mimeType) {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j) {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public static bool SetDefaultText() {
            try {
                clsGloFun.LogWrite("SetDefaultText - START", "INFO");
                Mds1.ChequeDefaultText = clsGloVar.str_chequeDefaultText;
                clsGloFun.LogWrite("SetDefaultText - DONE", "INFO");
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: SetDefaultText, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }

        public static bool TransferImage(string sf, string df) {
            try {
                File.Copy(sf, df, true);
                return true;
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: TransferImage, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                return false;
            }
        }
        #endregion
    }
}