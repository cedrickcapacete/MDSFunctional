using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_OutOfService : Page {
        public Page_OutOfService() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#OUTOFSERVICE PAGE", "INFO");
                clsGloVar.str_Page_Name = "OUTOFSERVICE_WINDOW";
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnMaintenance_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: Maintenance", "INFO");
                Uri uri = new Uri("Page_Password.xaml", UriKind.RelativeOrAbsolute);
                NavigationService.Navigate(uri);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
