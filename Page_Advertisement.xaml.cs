using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_Advertisement : Page {
        public Page_Advertisement() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#ADVERTISEMENT PAGE", "INFO");
                clsGloVar.str_Page_Name = "ADVERTISEMENT_WINDOW";

                if(!clsGloFun.ResetGlobalVariables()) {
                    //OOS
                    return;
                }
                
                //Check MDS Status

            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void Window_MouseDown(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("*Start Transaction*", "INFO");
                clsGloFun.LogWrite("Card Type: NONCARD", "INFO");
                
                ////Check mo if cutoff na ba
                TimeSpan start = TimeSpan.Parse("00:00"); // 12 aM
                TimeSpan end = TimeSpan.Parse(clsGloVar.str_CutOffTime); // 5 PM
                TimeSpan now = DateTime.Now.TimeOfDay;
                
                clsGloVar.str_CardType = "NONCARD";
                ////Log mo yung journal

                //Check if Ilan laman ng CheckBox
                MainWindow.GetChequesInBox();

                //Check if ilan laman ng CashBox
                MainWindow.GetNotesInBox();

                ////Check mo if may receipt

                if (now >= start && now <= end) {
                    clsGloVar.ushrt_IsCutOff = 0;

                    Uri uri = new Uri("Page_SelectTrans.xaml", UriKind.RelativeOrAbsolute);
                    NavigationService.Navigate(uri);
                } else {
                    clsGloVar.ushrt_IsCutOff = 1;

                    Uri uri = new Uri("Page_SelectTrans.xaml", UriKind.RelativeOrAbsolute);
                    NavigationService.Navigate(uri);
                }

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
