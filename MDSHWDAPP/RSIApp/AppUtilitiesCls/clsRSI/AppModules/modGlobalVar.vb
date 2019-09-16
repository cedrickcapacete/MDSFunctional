Imports clsRSI.clsRSIHWDControl
Imports clsRSI.clsAppStructure

Module modGlobalVar


#Region "App Layer Default Ini File"
    'RSI - Hardware Ini File
    Public Const RSIHWDINIPATH As String = "AppIniFile\RSIHWDCFG.ini"
#End Region


#Region "App Golbal Variable/Object "

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl

    'RSI HWD Object
    Public objRSIHWD As New clsRSIHWDControl


    Public strErrMsg As String = String.Empty
    Public strLogIniPath As String = String.Empty
    Public strRSIINI1 As String = String.Empty
    Public strRSIINI2 As String = String.Empty

    'RSI Lock Control
    Public blnRSILock As Boolean = False

#End Region


#Region "Class Variable - User Define Type"

    Public udtRSIHWD As RSIHWDSTR
    Public udtRSIHwdCFG As RSISETTINGSTR
    Public udtRSIHookupCFG As RSILOOKUPDATASTR

#End Region


#Region "Function - GenDateTimeStamp"

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
            AppLogErr("Error in GenDateTimeStamp:" & ex.Message)
            Return "9999"
        End Try
    End Function


#End Region



End Module
