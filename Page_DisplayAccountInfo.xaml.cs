using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApplication1 {
    public partial class Page_DisplayAccountInfo : Page {

        //For Timer to work
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int timelimit = 15;

        public Page_DisplayAccountInfo() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#DISPACCINFO PAGE", "INFO");
                clsGloVar.str_Page_Name = "DISPACCINFO_WINDOW";

                //For Timer to work
                lblTimer.Content = timelimit.ToString();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();

                txtAccNo.Text = clsGloVar.str_AccountNumber;

            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: CONFIRM", "INFO");
                Uri uri = new Uri("Page_SelectMode.xaml", UriKind.Relative);

                dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                dispatcherTimer.Stop();
                NavigationService.Navigate(uri);
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

        //For the timer to work
        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            try {
                timelimit = timelimit - 1;
                lblTimer.Content = timelimit.ToString();

                if (timelimit == 0) {
                    clsGloFun.LogWrite("User Option: TIMEOUT - Display Account Info", "INFO");
                    Uri uri = new Uri("Page_Cancel.xaml", UriKind.Relative);

                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                    dispatcherTimer.Stop();
                    NavigationService.Navigate(uri);
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
