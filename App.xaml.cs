using System.Windows;

namespace WpfApplication1 { 
    public partial class App : Application {
        protected override void OnExit(ExitEventArgs e) {
            try {
                //Put your special code here
            } finally {
                base.OnExit(e);
            }
        }
    }
}
