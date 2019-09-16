Module modGlobalVar

#Region "App Layer Default Ini File"
    'MainApp Ini File
    Public Const UPSMAINAPPINIPATH As String = "AppIniFile\UPSMAINAPPCFG.ini"
#End Region

#Region "App Golbal Variable/Object "

    'Application Variable

    Dim strTitle As String = "modGolbalAppCFG"

    Public strErrMsg As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strLogMsg As String = String.Empty
    Public strSelMsgErr As String = String.Empty

    'App Default Ini File Name
    Public strAppLogINIFile As String = String.Empty

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    'App Housekeeping Cfg Object
    Public objAppHSKCfg As New clsReadAppCfg
    Public strAppHSKCfgIniFile As String = String.Empty

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl

    Public strEvtTxtMsg As String = String.Empty
    Public strEvtMsg As String = String.Empty


    Public udtFormDataUPS As New ClsUPSHWDInterface.clsAppStructure

#End Region



#Region "Golbal APP Structure"

    Structure APPSYSPATHSTR
        Dim APPMAINDIRVE As String
    End Structure

    Public udtAPPSYSPATH As APPSYSPATHSTR
    Public strReceiptImage As String = ""

#End Region





End Module
