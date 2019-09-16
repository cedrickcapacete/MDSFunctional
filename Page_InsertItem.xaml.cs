using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_InsertItem : Page {

        public Page_InsertItem() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                bool fromSummary = false;

                clsGloFun.LogWrite("#INSERTITEM PAGE", "INFO");
                if (clsGloVar.str_Page_Name == "SUMMARY_WINDOW") fromSummary = true;
                clsGloVar.str_Page_Name = "INSERTITEM_WINDOW";

                if (!fromSummary) {
                    MainWindow.StartMDSTrans();
                }
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: Window_Loaded, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                //Go to Out of Service Page
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: CANCEL - Insert Item Page", "INFO");
                MainWindow.userCancel();
            } catch (Exception ex) {
                clsGloFun.LogWrite("Function: btnCancel_Click, Error Message: " + ex.Message + ", Stack Trace: " + ex.StackTrace, "EROR");
                //Go to Out of Service Page
            }
        }
    }
}
