Module modLoggerFunc

    'Logger Log Control

    'Private strTitle As String = "modLoggerFunc"

#Region "Logger Control - InitLog,CloseLog"

    Public Sub InitLog(ByVal strAppLogIniPath As String)
        Try
            'Initialize the Application Log
            'Init Logger - define
            strAppLogINIFile = strAppLogIniPath.Trim
            objAppLog = New clsAppLoggerControl
            objAppLog.InitLogger(strAppLogINIFile)

            'App Event Log Status
            strLogEvt = "Log Start"
            objAppLog.AppEvtLog(clsAppLoggerControl.LoggerMsgTypeEnum.LOGINFO, strLogEvt)


        Catch ex As Exception
            'strErrMsg = "Error in InitLog. ErrInfo:" & ex.Message
            'AppLogErr(strErrMsg)
        End Try
    End Sub

    Public Sub CloseLog()
        Try

            'Logger End
            strLogEvt = "Log End"
            objAppLog.AppEvtLog(clsAppLoggerControl.LoggerMsgTypeEnum.LOGINFO, strLogEvt)

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
            strLogEvt = strInfoEvt.Trim
            objAppLog.AppEvtLog(clsAppLoggerControl.LoggerMsgTypeEnum.LOGINFO, strLogEvt)
        Catch ex As Exception
            strErrMsg = "Error in AppLogInfo."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "modLoggerFunc")
        End Try
    End Sub

    'Warning
    Public Sub AppLogWarn(ByVal strWarnEvt As String)
        Try
            strLogEvt = strWarnEvt.Trim
            objAppLog.AppEvtLog(clsAppLoggerControl.LoggerMsgTypeEnum.LOGWARNING, strLogEvt)
        Catch ex As Exception
            strErrMsg = "Error in AppLogWarn."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "modLoggerFunc")
        End Try
    End Sub

    'Error
    Public Sub AppLogErr(ByVal strErrEvt As String)
        Try
            strLogEvt = strErrEvt.Trim
            objAppLog.AppEvtLog(clsAppLoggerControl.LoggerMsgTypeEnum.LOGERROR, strLogEvt)
            'MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        Catch ex As Exception
            strErrMsg = "Error in AppLogErr."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "modLoggerFunc")
        End Try
    End Sub

#End Region

End Module
