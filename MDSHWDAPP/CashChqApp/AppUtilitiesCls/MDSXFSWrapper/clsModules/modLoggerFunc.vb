Imports clsAppLogger.clsAppLoggerControl.LoggerEvtTypeEnum
Imports clsAppLogger.clsAppLoggerControl.LoggerMsgTypeEnum
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Module modLoggerFunc

    'Logger Log Control
    Private strTitle As String = "modLoggerFunc"


#Region "Logger Control - InitLog,CloseLog"

    Public Sub InitLog(ByVal strAppLogIniPath As String)

        Try

            'Initialize the Application Log
            'Init Logger - define
            strAppLogINIFile = strAppLogIniPath.Trim
            objAppLog = New clsAppLogger.clsAppLoggerControl
            objAppLog.InitLogger(strAppLogINIFile, MDS)

            'App Event Log Status
            strLogEvt = "Log Start  - ClsMDSXFS"
            objAppLog.AppEvtLog(LOGINFO, strLogEvt)

        Catch ex As Exception
            strErrMsg = "Error in InitLog - ClsMDSXFS. ErrInfo:" & ex.Message
            'AppLogErr(strErrMsg)
        End Try
    End Sub

    Public Sub CloseLog()
        Try

            'Logger End
            strLogEvt = "Log End - clsMDSXFS"
            objAppLog.AppEvtLog(LOGINFO, strLogEvt)

            'Close the Logger
            objAppLog.CloseLogger()
            objAppLog.Dispose()
            objAppLog = Nothing

        Catch ex As Exception
            strErrMsg = "Error in CloseLog - ClsMDSXFS. ErrInfo:" & ex.Message
            'AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region

#Region "App Log Events - Info,Warning,Error"
    'Info
    Public Sub AppLogInfo(ByVal strInfoEvt As String)
        Try
            strLogEvt = strInfoEvt.Trim
            objAppLog.AppEvtLog(LOGINFO, strLogEvt)
        Catch ex As Exception
            strErrMsg = "Error in AppLogInfo - ClsMDSXFS. ErrInfo:" & ex.Message
            objAppLog.AppEvtLog(LOGERROR, strErrMsg)
            'strErrMsg += ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    'Warning
    Public Sub AppLogWarn(ByVal strWarnEvt As String)
        Try
            strLogEvt = strWarnEvt.Trim
            objAppLog.AppEvtLog(LOGWARNING, strLogEvt)
        Catch ex As Exception
            strErrMsg = "Error in AppLogWarn - ClsMDSXFS. ErrInfo:" & ex.Message
            objAppLog.AppEvtLog(LOGERROR, strErrMsg)
            'strErrMsg += ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    'Error
    Public Sub AppLogErr(ByVal strErrEvt As String)
        Try
            strLogEvt = strErrEvt.Trim
            objAppLog.AppEvtLog(LOGERROR, strLogEvt)
        Catch ex As Exception
            strErrMsg = "Error in AppLogErr - ClsMDSXFS. ErrInfo:" & ex.Message
            objAppLog.AppEvtLog(LOGERROR, strErrMsg)
            'strErrMsg += ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

#End Region

End Module
