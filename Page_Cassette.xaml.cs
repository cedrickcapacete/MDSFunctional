using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApplication1 {
    public partial class Page_Cassette : Page {

        bool ExchangeStatus = false;

        public Page_Cassette() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#CASSETTE PAGE", "INFO");
                clsGloVar.str_Page_Name = "CASSETTE_WINDOW";
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        
        private void btnBack_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: BACK", "INFO");

                if (!ExchangeStatus) {
                    Uri uri = new Uri("Page_Maintenance.xaml", UriKind.RelativeOrAbsolute);
                    NavigationService.Navigate(uri);
                }
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnStrExchange_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: START EXCHANGE", "INFO");

                //Check muna status ng MDS kung online ba
                if(MainWindow.MDS_StartExchange()) {
                    ExchangeStatus = true;
                } else {

                }
                
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnEndExchange_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: END EXCHANGE", "INFO");

                //Check muna status ng MDS kung online ba
                if (MainWindow.MDS_EndExchange()) {
                    ExchangeStatus = false;
                } else {

                }

            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnGetValue_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: GET VALUE", "INFO");

                //Check muna status ng MDS kung online ba
                MainWindow.GetChequesInBox();
                MainWindow.GetNotesInBox();

            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
