using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_Counterfeit : Page {
        public Page_Counterfeit() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#COUNTERFEIT PAGE", "INFO");
                clsGloVar.str_Page_Name = "COUNTERFEIT_WINDOW";
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
