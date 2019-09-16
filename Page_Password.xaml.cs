using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApplication1 {
    public partial class Page_Password : Page {

        //For Timer to work
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int timelimit = 15;

        string Password = "";

        public Page_Password() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#ENTERPASSWORD PAGE", "INFO");
                clsGloVar.str_Page_Name = "ENTERPASSWORD_WINDOW";

                //For Timer to work
                lblTimer.Content = timelimit.ToString();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        #region 0 to 9 -----------------------------------
        private void btn0_Click(object sender, RoutedEventArgs e) {
            set_Value("0");
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
            txtPassword.Text = "";
            Password = "";
        }
        #endregion

        private void btnBsp_Click(object sender, RoutedEventArgs e) {
            try {
                restartTime();
                if (Password.Length > 0) {
                    Password = Password.Substring(0, Password.Length - 1);
                    txtPassword.Text = Password;
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e) {
            try {
                Uri uri = new Uri("Page_Maintenance.xaml", UriKind.Relative);

                //check if password is correct
                if (Password == "123456") {
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                    dispatcherTimer.Stop();
                    NavigationService.Navigate(uri);
                } else {
                    restartTime();
                    txtPassword.Text = "";
                    Password = "";
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

                //if (Password.Length < clsGloVar.int_AccountNoLength) {
                    Password = Password + str_char;
                    txtPassword.Text = Password;
                //}
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
                    clsGloFun.LogWrite("User Option: TIMEOUT - Enter Account Number", "INFO");
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
