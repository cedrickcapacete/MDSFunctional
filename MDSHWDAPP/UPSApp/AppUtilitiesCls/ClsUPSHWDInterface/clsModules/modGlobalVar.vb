Imports ClsUPSHWDInterface.clsHardwareLayer
Imports ClsUPSHWDInterface.clsAppStructure

Module modGlobalVar

#Region "App Layer Default Ini File"
    'Hardware
    Public Const HWDLAYERINIPATH As String = "AppIniFile\UPSHWDCFG.ini"
#End Region

#Region "App Object"

    Public objUPS As New clsMultilinkUPS


    'App Default Ini File Name
    Public objAppINI As New clsAppLayer.clsAppLayerControl
    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl


#End Region


#Region "App Structure"

    Public udtUPSHWDCFG As UPSSETTINGSTR
    Public udtUPSLOOKUPDATACFG As UPSLOOKUPDATASTR
    Public udtUPSHWD As UPSHWDSTR

#End Region


#Region "App Golbal Variable/Object "

    Public UPSStatusCode As Integer = 0

    Public strErrMsg As String = String.Empty

    Public strLogIniPath As String = String.Empty
    Public strIniPath1 As String = String.Empty
    Public strIniPath2 As String = String.Empty

    'UPS Lock Control
    Public blnLockUPS As Boolean = False

    'UPS Trace ID
    Public strGeniDeviceTrace As String = ""


    Public UPSReplyCode As String = ""
    Public UPSReplyCDDetails As String = ""


#End Region

#Region "App Function"

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
