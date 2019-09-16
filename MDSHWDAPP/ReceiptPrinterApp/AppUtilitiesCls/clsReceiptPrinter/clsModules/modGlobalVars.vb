Imports System.Drawing
Imports System.Drawing.Printing
Imports clsReceiptPrinter.clsReceiptPrinterControl


Module modGlobalVars

#Region "App Layer Default Ini File"
    'UPS Hardware
    Public Const HWDLAYERINIPATH As String = "AppIniFile\PRINTERHWDCFG.ini"
#End Region


#Region "Cls Variable"

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    Public strAppLogINIFile As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strErrMsg As String = String.Empty

    Public strFileINIPath As String = String.Empty

    Public Const APP_DEF_TXT_FLD_SEP As String = "|"


    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl

    Public blnInitAppLayer As Boolean


#End Region

#Region "App Object"

    Public udtRECEIPTPRINTERHWDCFG As RECEIPTPRINTERHWDSTR
    Public udtRECEIPTPRINTERSETTINGCFG As RECEIPTPRINTERSETTINGSTR
    Public udtRECEIPTPRINTERPROPERTIESCFG As RECEIPTPRINTERPROPERTIESSTR
    Public udtRECEIPTPRINTERLOOKUPDATACFG As RECEIPTPRINTERLOOKUPDATASTR
    Public objCustomPrinter As clsCustomPrinter
    Public objPrinter As clsReceiptPrinterControl.RECEIPTPRINTERHWDSTR
#End Region

End Module
