Imports clsUPS.clsUPSControl

Module modGlobalVars

#Region "App Layer Default Ini File"
    'UPS Hardware
    Public Const UPSHWDLAYERINIPATH As String = "AppIniFile\UPSHWDCFG.ini"
#End Region

#Region "App Object"

    Public udtUPSHWDCFG As UPSSETTINGSTR
    Public udtUPSLOOKUPDATACFG As UPSLOOKUPDATASTR
    Public objUPS As New clsMultilinkUPS

#End Region

#Region "Cls Variable"

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppINI As New clsAppLayer.clsAppLayerControl

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    Public strAppLogINIFile As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strErrMsg As String = String.Empty

    Public strFileINIPath As String = String.Empty

    Public Const APP_DEF_TXT_FLD_SEP As String = "|"


    Public blnInitAppLayer As Boolean


#End Region


End Module
