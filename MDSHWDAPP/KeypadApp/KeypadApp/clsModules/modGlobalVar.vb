Module modGlobalVar

#Region "App Layer Default Ini File"
    'MainApp Ini File
    Public Const MAINAPPINIPATH As String = "AppIniFile\EPPMAINAPPCFG.ini"

    Public Const DefaultInputTextLen As String = "6"

#End Region

#Region "App Golbal Variable/Object "


    'Application Variable

    Dim strTitle As String = "modGolbalAppCFG"

    Public strErrMsg As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strLogMsg As String = String.Empty
    Public strSelMsgErr As String = String.Empty
    Public strEvtTxtMsg As String = String.Empty
    Public strEvtMsg As String = String.Empty
    Public strLogIniPath As String = String.Empty

    'App Default Ini File Name
    Public strAppLogINIFile As String = String.Empty

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    'App Housekeeping Cfg Object
    Public objAppHSKCfg As New clsReadAppCfg
    Public strAppHSKCfgIniFile As String = String.Empty

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl

  

#End Region


    'Dim udtFormDataKeypad As New ClsKeypadHWDInterface.clsHardwareLayer.KeypadHWDSTR

    'global variable
    Public KEY_STROKE_ENTER As String = ""
    Public KEY_ALL As String = ""
    Public PIN_FORMAT_TYPE As String = ""


End Module
