using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_Initialization : Page {
        public Page_Initialization() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#INITIAZLIATION PAGE", "INFO");
                clsGloVar.str_Page_Name = "INITIAZLIATION_WINDOW";

                MainWindow.InitializeMDS();
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        
    }
}
