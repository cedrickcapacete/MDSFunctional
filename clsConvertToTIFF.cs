//using System;
//using System.ServiceProcess;                // goto 'Add Reference'->System.ServiceProcess;
//using System.IO;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Drawing.Drawing2D;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.OleDb;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Runtime.InteropServices;

//using FB4API3Lib;

//namespace WpfApplication1 {
//    class clsConvertToTIFF {


//        public FB4API3Lib.FB30 FB30 = null;
//        private bool pbMachineExists = false;

//        public fImport() {
//            InitializeComponent();

//            pbMachineExists = fncINIT_FSM_OBJECT();
//        }

//        #region main
//        private void fImport_Load(object sender, EventArgs e) {
//            subConvertImage("c:\\tempsourcef.bmp", "c:\\temptargetf.tiff");

//            //this.WindowState = FormWindowState.Maximized;
//        }
//        #endregion

//        //#region button
//        //private void cmdClose_Click(object sender, EventArgs e) {
//        //    this.Close();
//        //}
//        //#endregion

//        #region function/procedure
//        private bool fncINIT_FSM_OBJECT() {
//            long lbReturnValue = 0;

//            bool lbReturn = false;
//            try {
//                FB30 = new FB30();
//                if (FB30 == null) return false;

//                lbReturn = true;
//            } catch (Exception ex) {
//                MessageBox.Show(ex.Message, "INI FSM OBJECT", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            return lbReturn;
//        }
        
//        //ex: source=*f.bmp, target=*f.tiff
//        //    source=*r.bmp, target=*r.tiff
//        //    source=*u.bmp, target=*u.tiff
//        private void subConvertImage(string psSource, string psTarget) {
//            try {
//                string lsThreshold = cAP.IniFile.gfIniReadString("TIFF", "Threshold", "180", cGV.gsSetup);
//                string lsFrontContrast = cAP.IniFile.gfIniReadString("TIFF", "Font Contrast", "35", cGV.gsSetup);
//                string lsRearContrast = cAP.IniFile.gfIniReadString("TIFF", "Rear Contrast", "30", cGV.gsSetup);
//                string lscpConstFront = cAP.IniFile.gfIniReadString("TIFF", "cpFPara", "27", cGV.gsSetup);
//                string lscpConstRear = cAP.IniFile.gfIniReadString("TIFF", "cpBPara", "25", cGV.gsSetup);
//                string lstMinimumLevel = cAP.IniFile.gfIniReadString("TIFF", "Minimum Level", "50", cGV.gsSetup);
//                string lsCompression = cAP.IniFile.gfIniReadString("TIFF", "Compression", "0", cGV.gsSetup);

//                if (File.Exists(psSource).Equals(true)) {
//                    llRetnValue = FB30.ConvertToBiFile(psSource, psTarget, 1, 0, 0, 0, 0, Convert.ToInt32(lsThreshold), Convert.ToInt32(lsFrontContrast), Convert.ToInt32(lscpConstFront), Convert.ToInt32(lstMinimumLevel), Convert.ToInt32(lsCompression));
//                }
//            } catch (Exception ex) {
//                MessageBox.Show(ex.Message, "Convert Image", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            } finally {
//                //
//            }
//        }
//        #endregion
//    }
//}