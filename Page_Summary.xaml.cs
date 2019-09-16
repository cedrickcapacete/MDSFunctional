using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1 {
    public partial class Page_Summary : Page {

        //For Timer to work
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int timelimit = 15;

        public Page_Summary() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                string content = "";

                clsGloFun.LogWrite("#SUMMARY PAGE", "INFO");
                clsGloVar.str_Page_Name = "SUMMARY_WINDOW";

                clsGloVar.str_SummaryAnswer = "";
                content = "Transaction: " + clsGloVar.str_TrasactionType + "\n";
                content = content + "Account Type: " + clsGloVar.str_AccountType + "\n";

                //IF TRANSACTION TYPE IS BMP - "BILLS PAYMENT" Add the Biller's Account number Field
                if (clsGloVar.str_TrasactionType == "BPM") {
                    content = content + "Biller's Account No.: " + clsGloVar.str_BillerAccountNumber + "\n";
                    content = content + "Subscriber's Account No.: " + clsGloVar.str_AccountNumber + "\n\n";
                } else {
                    content = content + "Account No.: " + clsGloVar.str_AccountNumber + "\n\n";
                }


                //IDENTIFY THE TRANSACTION TYPE MODE TO KNOW WHICH LIST TO DISPLAY
                switch (clsGloVar.str_TransTypeMode) {
                    case "CIM":
                        content = content + MainWindow.GetMDSNoteListSummary();
                        content = content + "\nTotal Amount: " + clsGloFun.CurrencyFormat(clsGloVar.str_TotalCashAmount,true);
                        break;
                    case "IPM":
                        content = content + MainWindow.GetMDSChequeListSummary();
                        content = content + "\nTotal Amount: " + clsGloFun.CurrencyFormat(clsGloVar.str_TotalChequeAmount,true);
                        break;
                    case "MIX":
                        //Do nothing
                        break;
                    default:
                        //Go to Out of Service Page
                        break;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(content);

                RichTextBox1.SelectAll();
                RichTextBox1.Selection.Text = sb.ToString();


                //For Timer to work
                lblTimer.Content = timelimit.ToString();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: ACCEPT", "INFO");
                clsGloVar.str_SummaryAnswer = "ACCEPT";
                MainWindow.userNo();
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: ADD", "INFO");
                clsGloVar.str_SummaryAnswer = "ADD";
                
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                dispatcherTimer.Stop();

                MainWindow.userYes();
                Uri uri = new Uri("Page_InsertItem.xaml", UriKind.Relative);
                NavigationService.Navigate(uri);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: CANCEL", "INFO");
                clsGloVar.str_SummaryAnswer = "CANCEL";
                
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                dispatcherTimer.Stop();

                MainWindow.userNo();
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        //For the timer to work
        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            try {
                timelimit = timelimit - 1;
                lblTimer.Content = timelimit.ToString();

                if (timelimit == 0) {
                    clsGloFun.LogWrite("User Option: TIMEOUT - Summary", "INFO");

                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                    dispatcherTimer.Stop();

                    MainWindow.userTimeOut();
                    
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
