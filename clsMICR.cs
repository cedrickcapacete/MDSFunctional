using System;

namespace WpfApplication1 {
    class clsMICR {
        private static string _CheckNo;
        private static string _RTNo;
        private static string _AccountNo;
        private static string _TCD;

        public static string CheckNo {
            get { return _CheckNo; }
            set { _CheckNo = value; }
        }

        public static string RTNo {
            get { return _RTNo; }
            set { _RTNo = value; }
        }

        public static string AccountNo {
            get { return _AccountNo; }
            set { _AccountNo = value; }
        }

        public static string TCD {
            get { return _TCD; }
            set { _TCD = value; }
        }

        public static bool ParseMICRData(string pMICRData) {
            try {
                string lsChkDelimeter;
                string lsRTDelimeter1;
                string lsRTDelimeter2;
                string lsAccDelimeter;
                string lsTCDDelimeter;
                
                lsChkDelimeter = "o";
                lsRTDelimeter1 = "d";
                lsRTDelimeter2 = "t";
                lsAccDelimeter = "o";
                lsTCDDelimeter = "o";

                int liStartChk = 0;
                int liEndChk = 0;
                int liStartRT = 0;
                int liEndRT = 0;
                int liStartAcct = 0;
                int liEndAcct = 0;
                int liStartTCD = 0;

                _CheckNo = "??????????";
                _RTNo = "?????????";
                _AccountNo = "????????????";
                _TCD = "???";

                // Check No
                liStartChk = pMICRData.IndexOf(lsChkDelimeter);
                if (liStartChk >= 0) {
                    liEndChk = pMICRData.IndexOf(lsChkDelimeter, liStartChk + 1);
                    if (liEndChk >= 0) {
                        _CheckNo = pMICRData.Substring(liStartChk + 1, liEndChk - (liStartChk + 1));
                    }
                }

                // Routing No
                liStartRT = pMICRData.IndexOf(lsRTDelimeter1, liEndChk);
                if (liStartRT >= 0) {
                    _RTNo = pMICRData.Substring(liEndChk + 1, liStartRT - (liEndChk + 1));
                    liEndRT = pMICRData.IndexOf(lsRTDelimeter2, liStartRT + 1);
                    if (liEndRT >= 0) {
                        _RTNo = _RTNo + pMICRData.Substring(liStartRT + 1, liEndRT - (liStartRT + 1));
                    }
                }

                //Account No
                liStartAcct = pMICRData.IndexOf(lsAccDelimeter, liEndRT + 1);
                if (liStartAcct >= 0) {
                    _AccountNo = pMICRData.Substring(liEndRT + 1, liStartAcct - (liEndRT + 1));
                    liEndAcct = pMICRData.IndexOf(lsAccDelimeter, liEndRT + 1);
                }

                // TCD
                liStartTCD = pMICRData.IndexOf(lsTCDDelimeter, liEndAcct + 1);
                if (liStartTCD >= 0) {
                    _TCD = pMICRData.Substring(liEndAcct + 1, liEndAcct +3);
                }

                CheckNo = _CheckNo.Trim().Trim(char.Parse(" "));
                RTNo = _RTNo.Trim().Trim(char.Parse(" "));
                AccountNo = _AccountNo.Trim().Trim(char.Parse(" "));
                TCD = _TCD.Trim().Trim(char.Parse(" "));

                // Fill '?' if empty value
                if (CheckNo == "") _CheckNo = "??????????";
                if (RTNo == "") _RTNo = "?????????";
                if (AccountNo == "") _AccountNo = "????????????";
                if (TCD == "") _TCD = "???";

                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        private static string fncRemoveSpace(string pText) {
            string lsReturn = "";
            try {
                if (pText.ToString().Length > 0) {
                    for (int index = 0; index < pText.Length; index++) {
                        if (pText.Substring(index, 1) != " ") {
                            lsReturn += pText.Substring(index, 1);
                        }
                    }
                }
                return lsReturn;
            } catch (Exception ex) {
                return lsReturn;
            }
        }
    }
}
