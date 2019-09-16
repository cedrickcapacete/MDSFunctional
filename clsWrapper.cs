// Solution Name:  ---> Cash and Cheque Deposit Machine (MDS9000 Rototype) 
// Developer Name: ---> Cedrick Capacete
// Company Name:   ---> Functional Inc.

using System;
using System.Windows;
using MDS_Wrapper_Control;
using System.IO;

namespace WpfApplication1 {
    public static class clsWrapper {
        
        static MDSControl Mds1 = new MDSControl();

        #region MDS 9000 Functions 
        //###########################################################################################################################
        //public static void setParam() {
        //    try {
        //        clsGlobalFunctions.LogWrite("Setting Parameters - START", "INFO");

        //        //1. Set LogPath ------------------------------------------------

        //        //Check if logfile path is valid
        //        if (clsGlobalFunctions.IsPathExists(clsGlobalVariables.str_LogPath) == false) {
        //            clsGlobalFunctions.LogWrite("Function: setParam. Message: Invalid Log Path", "WARN");
        //            //Go to OOS Screen
        //            return;
        //        } else {
        //            Mds1.LogPath = clsGlobalVariables.str_LogPath;
        //        }
        //        //------------------------------------------------


        //        //2. Set ImagePath ----------------------------------------------

        //        //Check if imgfile path is valid
        //        if (clsGlobalFunctions.IsPathExists(clsGlobalVariables.str_ImgPath) == false) {
        //            clsGlobalFunctions.LogWrite("Function: setParam. Message: Invalid Image Path", "WARN");
        //            //Go to OOS Screen
        //            return;
        //        } else {
        //            Mds1.ImagePath = clsGlobalVariables.str_ImgPath;
        //        }
        //        //------------------------------------------------


        //        //3. Set the Comport ---------------------------------------------
        //        Mds1.ComPort = clsGlobalVariables.ushrt_ComPort;
        //        //------------------------------------------------


        //        //4. Binding a function to the event -----------------------------
        //        Mds1.StatusUpdate += new MDSControl.StatusUpdateEvent(Mds1_StatusUpdate);
        //        Mds1.UserNotification += new MDSControl.UserNotificationEvent(Mds1_UserNotification);
        //        Mds1.TransactionCompleted += new MDSControl.TransactionCompletedEvent(Mds1_TransactionCompleted);
        //        Mds1.InitialiseCompleted += new MDSControl.InitialiseCompletedEvent(Mds1_InitialiseCompleted);
        //        //------------------------------------------------

        //        //5. Set Timeout -------------------------------------------------
        //        Mds1.SetTimeout(MDSControl.UserNotificationOptions.InsertItems, clsGlobalVariables.ushrt_InsertItems_TO);
        //        Mds1.SetTimeout(MDSControl.UserNotificationOptions.CleanFeeder, clsGlobalVariables.ushrt_CleanFeeder_TO);
        //        Mds1.SetTimeout(MDSControl.UserNotificationOptions.RepositionDocuments, clsGlobalVariables.ushrt_RepositionDocuments_TO);
        //        Mds1.SetTimeout(MDSControl.UserNotificationOptions.TakeReturnedItems, clsGlobalVariables.ushrt_TakeReturnedItems_TO);
        //        //------------------------------------------------

        //        clsGlobalFunctions.LogWrite("Setting Parameters - DONE", "INFO");
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: setParam, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}
        //public static void initialize() {
        //    try {
        //        clsGlobalFunctions.LogWrite("Initializing MDS - START", "INFO");
        //        Mds1.Initialise();
        //        clsGlobalFunctions.LogWrite("Initializing MDS - DONE", "INFO");
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: initialize, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}
        //public static void startTransactional() {
        //    try {
        //        clsGlobalFunctions.LogWrite("Start Transaction MDS - START", "INFO");
        //        if (Mds1.MDSStatus == MDSControl.MDSStatusOptions.Ok) {
        //            Mds1.Mode = clsGlobalVariables.str_TransTypeMode;
        //            Mds1.TransactionStart();
        //        }
        //        clsGlobalFunctions.LogWrite("Start Transaction MDS - DONE", "INFO");
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: startTransactional, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}
        //public static void disconnect() {
        //    try {
        //        clsGlobalFunctions.LogWrite("Disconnecting MDS - START", "INFO");
        //        if (Mds1.MDSStatus == MDSControl.MDSStatusOptions.Ok) {
        //            Mds1.CardReaderLight(MDSControl.LightStatus.Off);
        //            Mds1.TopKioskLight(MDSControl.LightStatus.Off);
        //            Mds1.Stop();
        //        } else {
        //            clsGlobalFunctions.LogWrite("MDS is Disconnected", "INFO");
        //        }
        //        clsGlobalFunctions.LogWrite("Disconnecting MDS - DONE", "INFO");
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: disconnect, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //} 
        //public static void exitProgram() {
        //    try {
        //        clsGlobalFunctions.LogWrite("Exit MDS - START", "INFO");
        //        disconnect();
        //        Mds1 = null;
        //        clsGlobalFunctions.LogWrite("Exit MDS - DONE", "INFO");
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: exitProgram, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}
        #endregion


        #region User Options
        ////###########################################################################################################################
        //public static void userYes() {
        //    try {
        //        Mds1.UserChoice(MDSControl.UserOptions.Yes);
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: userYes, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //    }
        //}
        //public static void userNo() {
        //    try {
        //        Mds1.UserChoice(MDSControl.UserOptions.No);
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: userNo, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //    }
        //}
        //public static void userCancel() {
        //    try {
        //        Mds1.UserChoice(MDSControl.UserOptions.Cancel);
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: userCancel, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //    }
        //}
        //public static void userTimeOut() {
        //    try {
        //        Mds1.UserChoice(MDSControl.UserOptions.Timeout);
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: userTimeOut, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //    }
        //}
        //#endregion


        //#region Binded Functions 
        //###########################################################################################################################
        //static void Mds1_TransactionCompleted(object sender, MDSControl.TransactionResult res) {
        //    try {
        //        clsGlobalFunctions.LogWrite("Start Mds1_TransactionCompleted - START", "INFO");

        //        if (res == MDSControl.TransactionResult.Ok) {
        //            string header = "";
        //            string content = "";

        //            header = "Transaction completed successfully\n\n";
        //            header = header + "Transaction: " + clsGlobalVariables.str_TrasactionType + "\n";
        //            header = header + "Account Type: " + clsGlobalVariables.str_AccountType + "\n";
        //            header = header + "Account No.: " + clsGlobalVariables.str_AccountNumber + "\n\n";

        //            switch (clsGlobalVariables.str_TransTypeMode) {
        //                case "CIM":
        //                    content = content + getNoteList();
        //                    break;
        //                case "IPM":
        //                    content = content + getChequeList();
        //                    break;
        //                case "MIX":
        //                    content = content + getNoteList();
        //                    content = content + getChequeList();
        //                    break;
        //                default:
        //                    Console.WriteLine("Default case");
        //                    break;
        //            }

        //            GenerateTransFile();

        //            clsGlobalFunctions.LogWrite("Transaction completed successfully", "INFO");
        //            MessageBox.Show(header + "\n" + content);

        //            clsGlobalFunctions.GoToAdvertisement();
        //        } else {
        //            clsGlobalFunctions.LogWrite("Function: Mds1_TransactionCompleted. Message: Transaction Failed", "WARN");
        //        }

        //        clsGlobalFunctions.LogWrite("Start Mds1_TransactionCompleted - DONE", "INFO");

        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: Mds1_TransactionCompleted, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}

        //static void Mds1_UserNotification(object sender, MDSControl.UserNotificationOptions uno) {
        //    try {
        //        switch (uno.ToString()) {
        //            case "Processing":
        //                clsGlobalFunctions.GoToProcessing();
        //                clsGlobalFunctions.ClosePrevWindow();
        //                break;
        //            case "WantToInsertMoreQuestion":
        //                clsGlobalFunctions.GoToSummary();
        //                clsGlobalFunctions.ClosePrevWindow();
        //                break;
        //            case "TakeReturnedItems":
        //                clsGlobalFunctions.GoToTakeReturn();
        //                clsGlobalFunctions.ClosePrevWindow();
        //                break;
        //            case "CompleteDepositQuestion":
        //                if(clsGlobalVariables.str_Page_Name == "SUMMARY_WINDOW") {
        //                    switch (clsGlobalVariables.str_SummaryAnswer) {
        //                        case "ACCEPT":
        //                            userYes();
        //                            break;
        //                        case "CANCEL":
        //                            userNo();
        //                            break;
        //                    }
        //                } else {
        //                    clsGlobalFunctions.GoToSummary();
        //                    clsGlobalFunctions.ClosePrevWindow();
        //                }
        //                break;
        //            default:
        //                MessageBox.Show(String.Format("User notification: {0}", uno.ToString()));
        //                Console.WriteLine("Default case");
        //                break;
        //        }

        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: Mds1_UserNotification, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}

        //static void Mds1_StatusUpdate(object sender) {
        //    try {                
        //        //MessageBox.Show(String.Format("MDS status: {0}", Mds1.MDSStatusText));
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: Mds1_StatusUpdate, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}

        //static void Mds1_InitialiseCompleted(object sender) {
        //    try {
        //        //MDSStatusText will resutl either "OK" or "Disconnected"
        //        if (Mds1.MDSStatusText == "OK") {
        //            if (clsGlobalVariables.str_Page_Name == "INITIAZLIATION_WINDOW") {
        //                Mds1.TopKioskLight(MDSControl.LightStatus.On);
        //                clsGlobalFunctions.GoToAdvertisement();
        //            }
        //        } else { 
        //            if (clsGlobalVariables.str_Page_Name != "OUTOFSERVICE_WINDOW" && clsGlobalVariables.str_Page_Name != "ADVERTISEMENT_WINDOW") {
        //                //clsGlobalFunctions.GoToOutofService();
        //                clsGlobalFunctions.GoToAdvertisement();
        //            } 
        //        } 
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: Mds1_InitialiseCompleted, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}
        #endregion


        #region Transaction Functions
        //public static void GenerateTransFile() {
        //    try {

        //        string Path = clsGlobalVariables.str_ImgPath + "\\DATA\\" + clsGlobalVariables.str_DateToday;   //  E:\MDS_Files\MDS Data\DATA\20181031

        //        // "01" means cash transaction followed by 10 Digit Transaction Number
        //        string TranxNo = "01" + clsGlobalFunctions.getTransactionNo().PadLeft(8,'0');   //  010000000012
        //        System.IO.Directory.CreateDirectory(Path);

        //        using (StreamWriter SW = File.AppendText(Path + "\\" + TranxNo + ".txt")) { //  E:\MDS_Files\MDS Data\DATA\20181031\010000000012.txt
        //            SW.WriteLine("Sample Data"); 
        //            //SW.WriteLine("{0} {1}: {2}", DateTime.Now.ToString("HH:mm:ss"), type, logMessage);
        //        }

        //        clsGlobalFunctions.updateTransactionNo();

        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: GenerateTransFile, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //    }
        //}
        #endregion


        #region Global MDS Functions
        //###########################################################################################################################
        //public static string getMDS_Status() {
        //    try {
        //        return Mds1.MDSStatusText;
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: Mds1_InitialiseCompleted, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //        return "";
        //    }
        //}

        //public static string getNoteList() {
        //    try {
        //        string list = "";
        //        float totalAmount = 0;

        //        foreach (MDSControl.NoteSet ns in Mds1.TransactionNoteList()) {
        //            //FORMAT: PHP -  100.00 x 3 = 
        //            //CODE:   "{0} - {2}" + ".00 x {3}"

        //            list += String.Format("{0} - {1}.00 x {2}\n", ns.Currency, ns.Value.ToString().PadLeft(5,' '), ns.Number,  ns.Level.ToString());
        //            totalAmount = totalAmount + (ns.Value * ns.Number);

        //            //list += String.Format("{0} x {1} {2} {3}\n", ns.Number, ns.Value, ns.Currency, ns.Level.ToString());
        //        }

        //        clsGlobalVariables.flt_TotalAmount = totalAmount;
        //        return list;
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: getNoteList, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //        return "";
        //    }
        //}

        //public static string getChequeList() {
        //    try {
        //        string list = "";

        //        foreach (MDSControl.ChequeSet cs in Mds1.TransactionChequeList()) {
        //            //FORMAT: 2323232323 - EscrowedGood 
        //            //CODE:   "{0} - {2}"

        //            list += String.Format("{0} - {1}\n", cs.Codeline, cs.Status);
        //        }
        //        return list;
        //    } catch (Exception ex) {
        //        clsGlobalFunctions.LogWrite("Function: getNoteList, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
        //        //Go to Out of Service Page
        //        return "";
        //    }
        //}
        #endregion
    }
}
