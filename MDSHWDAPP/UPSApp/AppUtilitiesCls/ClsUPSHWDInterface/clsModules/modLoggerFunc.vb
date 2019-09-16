Imports clsAppLogger.clsAppLoggerControl.LoggerEvtTypeEnum
Imports clsAppLogger.clsAppLoggerControl.LoggerMsgTypeEnum
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Module modLoggerFunc

    'Logger Log Control
    Private strTitle As String = "clsUPSHwdInterface-modLoggerFunc"


#Region "Logger Control - InitLog,CloseLog"

    Public Sub InitLog(ByVal strAppLogIniPath As String)

        Try

            'Initialize the Application Log
            'Init Logger - define
            strAppLogIniPath = strAppLogIniPath.Trim
            objAppLog = New clsAppLogger.clsAppLoggerControl
            objAppLog.InitLogger(strAppLogIniPath, UPS)

            'App Event Log Status
            objAppLog.AppEvtLog(LOGINFO, "Log Start - clsUPSHwdInterface")


        Catch ex As Exception
            strErrMsg = "Error in clsUPSHwdInterface.InitLog. ErrInfo:" & ex.Message
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Public Sub CloseLog()
        Try

            'Logger End
            objAppLog.AppEvtLog(LOGINFO, "Log End - clsUPSHwdInterface")

            'Close the Logger
            objAppLog.CloseLogger()
            objAppLog.Dispose()
            objAppLog = Nothing

        Catch ex As Exception
            strErrMsg = "Error in clsUPSHwdInterface.CloseLog. ErrInfo:" & ex.Message
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
            strErrMsg = "Error in clsUPSHwdInterface.AppLogInfo. ErrInfo:" & ex.Message
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
            strErrMsg = "Error in clsUPSHwdInterface.AppLogWarn. ErrInfo:" & ex.Message
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    'Error
    Public Sub AppLogErr(ByVal strErrEvt As String)
        Try
            strErrEvt = strErrEvt.Trim
            objAppLog.AppEvtLog(LOGERROR, strErrEvt)
        Catch ex As Exception
            strErrMsg = "Error in clsUPSHwdInterface.AppLogErr. ErrInfo:" & ex.Message
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

#End Region


End Module
