using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApplication1 {
    public partial class Page_Cancel : Page {

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public Page_Cancel() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#CANCEL PAGE", "INFO");
                clsGloVar.str_Page_Name = "CANCEL_WINDOW";

                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
                dispatcherTimer.Start();

                //Log Journal that transaction has been cancelled
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
            dispatcherTimer.Stop();

            Uri uri = new Uri("Page_Advertisement.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
    }
}
