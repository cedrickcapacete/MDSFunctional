﻿Imports clsAppLogger.clsAppLoggerControl.LoggerEvtTypeEnum
Imports clsAppLogger.clsAppLoggerControl.LoggerMsgTypeEnum
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Module modLoggerFunc

    'Logger Log Control

    Private strTitle As String = "clsCashHWDInterface-modLoggerFunc"
    Private strClassNm As String = "clsCashHWDInterface"


#Region "Logger Control - InitLog,CloseLog"

    Public Sub InitLog(ByVal strAppLogIniPath As String)
        Dim strAppLogINIFile As String = String.Empty

        Try

            'Initialize the Application Log
            'Init Logger - define
            strAppLogINIFile = strAppLogIniPath.Trim
            objAppLog = New clsAppLogger.clsAppLoggerControl
            objAppLog.InitLogger(strAppLogINIFile, MDS)

            'App Event Log Status
            strLogEvt = "Log Start - clsCashHWDInterface"
            objAppLog.AppEvtLog(LOGINFO, strLogEvt)


        Catch ex As Exception
            strErrMsg = "Error in clsCashHWDInterface-InitLog."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Public Sub CloseLog()
        Try

            'Logger End
            strLogEvt = "Log End - clsCashHWDInterface"
            objAppLog.AppEvtLog(LOGINFO, strLogEvt)

            'Close the Logger
            objAppLog.CloseLogger()
            objAppLog.Dispose()
            objAppLog = Nothing

        Catch ex As Exception
            strErrMsg = "Error in clsCashHWDInterface-CloseLog. ErrInfo:" & ex.Message
            'AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region


#Region "App Log Events - Info,Warning,Error"
    'Info
    Public Sub AppLogInfo(ByVal strInfoEvt As String)
        Try
            strLogEvt = strClassNm & " - " & strInfoEvt.Trim
            objAppLog.AppEvtLog(LOGINFO, strLogEvt)
        Catch ex As Exception
            strErrMsg = "Error in clsCashHWDInterface-AppLogInfo."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    'Warning
    Public Sub AppLogWarn(ByVal strWarnEvt As String)
        Try
            strLogEvt = strClassNm & " - " & strWarnEvt.Trim
            objAppLog.AppEvtLog(LOGWARNING, strLogEvt)
        Catch ex As Exception
            strErrMsg = "Error in clsCashHWDInterface-AppLogWarn."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    'Error
    Public Sub AppLogErr(ByVal strErrEvt As String)
        Try
            strLogEvt = strClassNm & " - " & strErrEvt.Trim
            objAppLog.AppEvtLog(LOGERROR, strLogEvt)
            'MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        Catch ex As Exception
            strErrMsg = "Error in clsCashHWDInterface-AppLogErr."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

#End Region


End Module
