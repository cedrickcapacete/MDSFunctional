Imports clsAppLogger.clsAppLoggerControl.LoggerEvtTypeEnum
Imports clsAppLogger.clsAppLoggerControl.LoggerMsgTypeEnum
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Module modLoggerFunc

    'Logger Log Control

    Private strTitle As String = "clsRSIHWD-modLoggerFunc"

#Region "Logger Control - InitLog,CloseLog"

    Public Sub InitLog(ByVal strAppLogIniPath As String)
        Dim strAppLogINIFile As String = String.Empty

        Try

            'Initialize the Application Log
            'Init Logger - define
            strAppLogINIFile = strAppLogIniPath.Trim
            objAppLog = New clsAppLogger.clsAppLoggerControl
            objAppLog.InitLogger(strAppLogINIFile, RSI)

            'App Event Log Status
            objAppLog.AppEvtLog(LOGINFO, "Log Start")


        Catch ex As Exception
            strErrMsg = "Error in clsRSIHWD-InitLog."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Public Sub CloseLog()
        Try

            'Logger End
            objAppLog.AppEvtLog(LOGINFO, "Log End")

            'Close the Logger
            objAppLog.CloseLogger()
            objAppLog.Dispose()
            objAppLog = Nothing

        Catch ex As Exception
            'strErrMsg = "Error in CloseLog. ErrInfo:" & ex.Message
            'AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region

#Region "App Log Events - Info,Warning,Error"
    'Info
    Public Sub AppLogInfo(ByVal strInfoEvt As String)
        Try
            strInfoEvt = strInfoEvt.Trim
            objAppLog.AppEvtLog(LOGINFO, strInfoEvt)
        Catch ex As Exception
            strErrMsg = "Error in clsRSIHWD-AppLogInfo."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    'Warning
    Public Sub AppLogWarn(ByVal strWarnEvt As String)
        Try
            strWarnEvt = strWarnEvt.Trim
            objAppLog.AppEvtLog(LOGWARNING, strWarnEvt)
        Catch ex As Exception
            strErrMsg = "Error in clsRSIHWD-AppLogWarn."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    'Error
    Public Sub AppLogErr(ByVal strErrEvt As String)
        Try
            strErrEvt = strErrEvt.Trim
            objAppLog.AppEvtLog(LOGERROR, strErrEvt)
            'MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        Catch ex As Exception
            strErrMsg = "Error in clsRSIHWD-AppLogErr."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

#End Region

End Module
