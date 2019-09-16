using WpfApplication1;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1 {
    public partial class Page_Processing : Page {
        public Page_Processing() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloVar.str_Page_Name = "PROCESSING_WINDOW";
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }
        
    }
}
