using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_CleanFeeder : Page {
        public Page_CleanFeeder() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#CLEANFEEDER PAGE", "INFO");
                clsGloVar.str_Page_Name = "CLEANFEEDER_WINDOW";
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
