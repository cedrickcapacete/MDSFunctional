Module modGlobalVar

#Region "App Layer Default Ini File"
    'Hardware
    Public Const HWDLAYERINIPATH As String = "AppIniFile\EPPHOOKUPCFG.ini"
#End Region


#Region "App Golbal Variable/Object "

   'Application Variable

    Dim strTitle As String = "modGolbalAppCFG"

    Public strErrMsg As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strLogMsg As String = String.Empty
    Public strSelMsgErr As String = String.Empty

    Public strLogIniPath As String = String.Empty

    'App Default Ini File Name
    Public objAppINI As New clsAppLayer.clsAppLayerControl
    Public strAppLogINIFile As String = String.Empty

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl


#End Region

#Region "Variable"

    Public blnLockKeypad As Boolean = False
    Public strGeniDeviceTrace As String = ""
    Public RawData As String = ""
    Public ControlKey As String = ""
    Public TestData As String = ""
    Public EncData As String = ""
    Public HideData As String = ""
    Public objSagemEPP As clsKeypad.clsEPPKeypad
    Public blnReturn As Boolean = False
    Public intStartCount As Integer = 0

#End Region



End Module
