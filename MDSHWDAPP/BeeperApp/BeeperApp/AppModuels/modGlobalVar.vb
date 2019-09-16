Module modGlobalVar

#Region "App Layer Default Ini File"
    'MainApp Ini File
    Public Const BEEPERMAINAPPINIPATH As String = "AppIniFile\BEEPERMAINAPPCFG.ini"
#End Region

#Region "App Golbal Variable/Object "

    'Application Variable

    Dim strTitle As String = "modGolbalAppCFG"

    Public strErrMsg As String = ""
    Public strLogEvt As String = ""
    Public strLogMsg As String = ""
    Public strSelMsgErr As String = ""

    'App Default Ini File Name
    Public strAppLogINIFile As String = ""

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    'App Housekeeping Cfg Object
    Public objAppHSKCfg As New clsReadAppCfg
  
    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl


#End Region



End Module
