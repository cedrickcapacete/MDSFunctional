using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_TakeReturnItem : Page {
        public Page_TakeReturnItem() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#TAKERETURNEDITEM PAGE", "INFO");
                clsGloVar.str_Page_Name = "TAKERETURNEDITEM_WINDOW";
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
    }
}
