using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApplication1 {
    public partial class Page_EnterChequeAmount : Page {

        //For Timer to work
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int timelimit = 15;

        string Amount = "";

        public Page_EnterChequeAmount() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#ENTERCHEQUEAMOUNT PAGE", "INFO");
                clsGloVar.str_Page_Name = "ENTERCHEQUEAMOUNT_WINDOW";

                //For Timer to work
                lblTimer.Content = timelimit.ToString();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
                
                Amount = "";
                txtAmount.Text = "0.00";
                txtAmount.MaxLength = clsGloVar.int_ChequeAmountLength;
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        #region 0 to 9 -----------------------------------
        private void btn0_Click(object sender, RoutedEventArgs e) {
            if (Amount != "") {
                set_Value("0");
            }
        }
        private void btn1_Click(object sender, RoutedEventArgs e) {
            set_Value("1");
        }
        private void btn2_Click(object sender, RoutedEventArgs e) {
            set_Value("2");
        }
        private void btn3_Click(object sender, RoutedEventArgs e) {
            set_Value("3");
        }
        private void btn4_Click(object sender, RoutedEventArgs e) {
            set_Value("4");
        }
        private void btn5_Click(object sender, RoutedEventArgs e) {
            set_Value("5");
        }
        private void btn6_Click(object sender, RoutedEventArgs e) {
            set_Value("6");
        }
        private void btn7_Click(object sender, RoutedEventArgs e) {
            set_Value("7");
        }
        private void btn8_Click(object sender, RoutedEventArgs e) {
            set_Value("8");
        }
        private void btn9_Click(object sender, RoutedEventArgs e) {
            set_Value("9");
        }
        private void btnClear_Click(object sender, RoutedEventArgs e) {
            restartTime();
            txtAmount.Text = "0.00";
            Amount = "";
        }
        #endregion

        private void btnBsp_Click(object sender, RoutedEventArgs e) {
            try {
                restartTime();
                if (Amount.Length > 0) {

                    Amount = Amount.Substring(0, Amount.Length - 1);
                    
                    if (Amount.Length == 0) {
                        txtAmount.Text = "0.00";
                        Amount = "";
                    } else {
                        if ((Convert.ToDecimal(Amount) / 100) < 1) {
                            txtAmount.Text = (Convert.ToDecimal(Amount) / 100).ToString("0.00#######");
                        } else {
                            txtAmount.Text = clsGloFun.CurrencyFormat(Amount, true);
                        }
                    }
                }

                
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e) {
            try {
                if (Amount != "") {
                    clsGloVar.str_TotalChequeAmount = Amount;
                    clsGloFun.LogWrite("Entered Cheque Amount: " + Amount, "INFO");

                    Uri uri = new Uri("Page_InsertItem.xaml", UriKind.Relative);

                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                    dispatcherTimer.Stop();
                    NavigationService.Navigate(uri);
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: CANCEL", "INFO");
                Uri uri = new Uri("Page_Cancel.xaml", UriKind.Relative);

                dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                dispatcherTimer.Stop();
                NavigationService.Navigate(uri);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void set_Value(string str_char) {
            try {
                restartTime();
                if (Amount.Length < clsGloVar.int_ChequeAmountLength) {
                    Amount = Amount + str_char;

                    if ((Convert.ToDecimal(Amount) / 100) < 1) {
                        txtAmount.Text = (Convert.ToDecimal(Amount) / 100).ToString("0.00#######");
                    } else {
                        txtAmount.Text = clsGloFun.CurrencyFormat(Amount, true);
                    }
                }
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
                    clsGloFun.LogWrite("User Option: TIMEOUT - Enter Cheque Amount", "INFO");
                    Uri uri = new Uri("Page_Cancel.xaml", UriKind.Relative);

                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                    dispatcherTimer.Stop();
                    NavigationService.Navigate(uri);
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void restartTime() {
            try {
                dispatcherTimer.Stop();
                timelimit = 15;
                lblTimer.Content = timelimit.ToString();
                dispatcherTimer.Start();
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
