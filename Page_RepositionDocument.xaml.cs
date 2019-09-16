using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_RepositionDocument : Page {
        public Page_RepositionDocument() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#REPOSITIONDOCUMENT PAGE", "INFO");
                clsGloVar.str_Page_Name = "REPOSITIONDOCUMENT_WINDOW";
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
