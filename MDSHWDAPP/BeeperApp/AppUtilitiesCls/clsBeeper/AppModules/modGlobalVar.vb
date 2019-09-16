Imports clsBeeperHWD.clsBeeperHWDControl
Imports clsBeeperHWD.clsAppStructure

Module modGlobalVar

#Region "App Layer Default Ini File"
    'Beeper - Hardware Ini File
    Public Const BEEPERHWDINIPATH As String = "AppIniFile\BEEPERHWDCFG.ini"
#End Region


#Region "App Golbal Variable/Object "

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl

    'BEEPER HWD Object
    Public objBeeperHWD As New clsBeeperHWDControl


    Public strErrMsg As String = String.Empty
    Public strLogIniPath As String = String.Empty
    Public strBeeperINI1 As String = String.Empty
    Public strBeeperINI2 As String = String.Empty

    'Beeper Lock Control
    Public blnBeeperLock As Boolean = False

#End Region


#Region "Class Variable - User Define Type"

    Public udtBeeperHWD As BeeperHWDSTR
    Public udtBeeperHwdCFG As BEEPERSETTINGSTR
    Public udtBeeperHookupCFG As BEEPERLOOKUPDATASTR

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
