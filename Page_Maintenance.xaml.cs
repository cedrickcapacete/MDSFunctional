using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;

namespace WpfApplication1 {
    public partial class Page_Maintenance : Page {

        private Font verdana10Font;
        private StreamReader reader;

        public Page_Maintenance() {
            InitializeComponent();
            Cursor = Cursors.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("#MAINTENANCE PAGE", "INFO");
                clsGloVar.str_Page_Name = "MAINTENANCE_WINDOW";
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnInitialize_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: INITIALIZE", "INFO");
                MainWindow.InitializeMDS();
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnShutDown_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: SHUTDOWN PC", "INFO");
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnCloseProgram_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: CLOSE PROGRAM", "INFO");
                MainWindow.closeMDSprogram();
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: BACK", "INFO");
                Uri uri = new Uri("Page_Advertisement.xaml", UriKind.RelativeOrAbsolute);
                NavigationService.Navigate(uri);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnCassette_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: CASSETTE", "INFO");
                Uri uri = new Uri("Page_Cassette.xaml", UriKind.RelativeOrAbsolute);
                NavigationService.Navigate(uri);
            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e) {
            try {
                clsGloFun.LogWrite("User Option: REPORT", "INFO");

                //Create a StreamReader object  
                reader = new StreamReader(clsGloVar.str_JournalPath + "//" + clsGloVar.str_DateToday + ".txt");
                //Create a Verdana font with size 10  
                verdana10Font = new Font("Verdana", 10);
                //Create a PrintDocument object  
                PrintDocument pd = new PrintDocument();
                //Add PrintPage event handler  
                pd.PrintPage += new PrintPageEventHandler(this.PrintTextFileHandler);
                //Call Print Method  
                pd.Print();
                //Close the reader  
                if (reader != null)
                    reader.Close();

            } catch (Exception ex) {
                //Go to Out of Service Page
            }
        }

        private void PrintTextFileHandler(object sender, PrintPageEventArgs ppeArgs) {
            //Get the Graphics object  
            Graphics g = ppeArgs.Graphics;
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            //Read margins from PrintPageEventArgs  
            float leftMargin = 15;//ppeArgs.MarginBounds.Left;
            float topMargin = 30;//ppeArgs.MarginBounds.Top;
            string line = null;
            //Calculate the lines per page on the basis of the height of the page and the height of the font  
            linesPerPage = ppeArgs.MarginBounds.Height / verdana10Font.GetHeight(g);
            //Now read lines one by one, using StreamReader  
            while (count < linesPerPage && ((line = reader.ReadLine()) != null)) {
                //Calculate the starting position  
                yPos = topMargin + (count * verdana10Font.GetHeight(g));
                //Draw text  
                g.DrawString(line, verdana10Font, Brushes.Black, leftMargin, yPos, new StringFormat());
                //Move to next line  
                count++;
            }
            //If PrintPageEventArgs has more pages to print  
            if (line != null) {
                ppeArgs.HasMorePages = true;
            } else {
                ppeArgs.HasMorePages = false;
            }
        }

    }
}
