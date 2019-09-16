Module modGlobalVar

#Region "App Layer Default Ini File"
    'Hardware
    Public Const HWDLAYERINIPATH As String = "AppIniFile\PRINTERHOOKUPCFG.ini"
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

    Public blnLockPrinter As Boolean = False
    Public strGeniDeviceTrace As String = ""
    Public ReceiptData As String = ""
    Public ReceiptImg As String = ""
    Public objCustomPrinter As clsReceiptPrinter.clsCustomPrinter
    Public strReceiptData1 As String


#End Region

#Region "Function"


    Public Function GenDateTimeStamp() As String
        Dim strDateTimeStamp As String = String.Empty
        Dim strDay As String = String.Empty
        Dim strMonth As String = String.Empty
        Dim strYear As String = String.Empty

        Dim strHH As String = String.Empty
        Dim strMM As String = String.Empty
        Dim strSS As String = String.Empty

        Dim strTmpdate As String = String.Empty
        Dim strTmptime As String = String.Empty

        Try

            strDay = DateTime.Now.Day.ToString("00")
            strMonth = DateTime.Now.Month.ToString("00")
            strYear = DateTime.Now.Year.ToString("0000")

            strHH = DateTime.Now.Hour.ToString("00")
            strMM = DateTime.Now.Minute.ToString("00")
            strSS = DateTime.Now.Second.ToString("00")

            strTmpdate = strDay & strMonth & strYear
            strTmptime = strHH & strMM & strSS

            strDateTimeStamp = strTmpdate.Trim & strTmptime.Trim

            Return strDateTimeStamp
        Catch ex As Exception
            Return "9999"
        End Try


    End Function



#End Region


End Module
