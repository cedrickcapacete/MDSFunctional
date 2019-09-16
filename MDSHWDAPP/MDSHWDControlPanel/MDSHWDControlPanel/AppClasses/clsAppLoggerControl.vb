Imports System
Imports System.IO


Public Class clsAppLoggerControl

#Region "LOGGER INIKeys"

    ' INI Configuration for AppLogCFG.INI
    Private Const INI_LOGCFG_SECT_SETTING As String = "LOGCFG"
    Private Const INI_LOGCFG_PATH_KEY As String = "LogPath"

    ' INI Configuration for AppLogCFG.INI
    Private Const INI_LOGOPTION_SECT_SETTING As String = "LOGOPTION"
    Private Const INI_APPEVT_KEY As String = "AppEvtLOG"

    'Ini File - Log Session Key 
    '0-Disable the Local App Path 1-Enable Local App Path
    Private Const AppPATHCTR_OPT As String = "AppPathCtrOpt"

    ' INI Configuration for AppLogCFG.INI
    Private Const INI_LOGNM_SECT_SETTING As String = "LogFileName"
    Private Const INI_APPEVTLOGNM_KEY As String = "AppLOGNM"

#End Region

#Region "Default Log File Name"
    'Default Log File Name
    Private Const APPEVTLOGFILENAME As String = "4.MDSHWDControlPanelLog.LOG"
#End Region

#Region "CLS Variable"

    'Ini Path Variable
    Private m_strAppLogPath As String
    Private m_strAppEvtLogFileNM As String
    Private m_blnAppLogOption As Boolean

    'Private strTitle As String = "clsLoggerControl"
    Private m_IniObj As clsINIFunc
    Private m_strINIFullPath As String
    'Private strErrMsg As String

    Dim strFolderNm As String
    Dim strLOGFolder As String
    Dim strAppLogNM As String

    'Text Log File Variable
    Private m_strTextLogFile As String = String.Empty
    Private m_objAppEvtTextLog As StreamWriter

#End Region

#Region "Main Directory Control"

    Private objMainINIFile As clsINIFunc = Nothing
    Dim strMainINIDirectoryPath As String = String.Empty
    Dim strMainDirectory As String = String.Empty

#End Region



#Region "CLS Property"

    'Log Store Path
    Property AppLogPath() As String
        Get
            Return m_strAppLogPath
        End Get
        Set(ByVal value As String)
            m_strAppLogPath = value
        End Set
    End Property

    'Log File Name Path
    Property AppLogFileName() As String
        Get
            Return m_strAppEvtLogFileNM
        End Get
        Set(ByVal value As String)
            m_strAppEvtLogFileNM = value
        End Set
    End Property

#End Region


#Region "CLS Enum"
    Public Enum LoggerEvtTypeEnum
        APPEvtLog = 1
        SysEvtLog = 2
        HardwareEvtLog = 3
    End Enum

    Public Enum LoggerMsgTypeEnum
        'Warning Error Information
        LOGINFO = 1
        LOGERROR = 2
        LOGWARNING = 3
    End Enum

#End Region

#Region "Read App Log Ini File Path"

    Public Sub LoadLogCfgSection(ByVal strIniPath As String)
        Dim strMAINPATH As String = String.Empty
        Dim blnAppPathCtr As Boolean

        Dim strLogFolder As String = String.Empty

        Try

            'Get User Value

            m_strINIFullPath = strIniPath
            If (m_IniObj Is Nothing) Then
                m_IniObj = New clsINIFunc(m_strINIFullPath, False)
            End If

            'App Path Control
            strMAINPATH = ""
            blnAppPathCtr = False

            m_IniObj.CurrentSection = INI_LOGOPTION_SECT_SETTING
            blnAppPathCtr = m_IniObj.GetBoolVal(AppPATHCTR_OPT, True) 'Get APP Path Control

            If blnAppPathCtr = True Then

                m_IniObj.CurrentSection = INI_LOGCFG_SECT_SETTING
                strLogFolder = m_IniObj.GetStrVal(INI_LOGCFG_PATH_KEY, "MDSHWDControlPanel\Log")

                'Main App Folder
                strMAINPATH = GetMainSystemPathControl()
                strMAINPATH = strMAINPATH.Trim & strLogFolder
            Else
                'App Main Path
                m_IniObj.CurrentSection = INI_LOGCFG_SECT_SETTING
                strMAINPATH = m_IniObj.GetStrVal(INI_LOGCFG_PATH_KEY, "F:\MDSLog")
            End If


            m_strAppLogPath = strMAINPATH

            m_IniObj.CurrentSection = INI_LOGOPTION_SECT_SETTING
            m_blnAppLogOption = m_IniObj.GetBoolVal(INI_APPEVT_KEY, False)

            m_IniObj.CurrentSection = INI_LOGNM_SECT_SETTING
            m_strAppEvtLogFileNM = m_IniObj.GetStrVal(INI_APPEVTLOGNM_KEY, APPEVTLOGFILENAME)

        Catch ex As Exception
            'strErrMsg = "Error in Loading Log Cfg Section from INI."
            'strErrMsg = strErrMsg & ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Exclamation, strTitle)
        End Try
    End Sub


#End Region


#Region "CLS Function"

    Public Sub Dispose()
        If Not (m_IniObj Is Nothing) Then
            m_IniObj = Nothing
        End If
    End Sub


    Public Function InitLogger(ByVal strIniPath As String) As Boolean
        Try

            'Load Log Setting
            LoadLogCfgSection(strIniPath)

            strFolderNm = Format(DateTime.Today, "ddMMyyyy")
            strLOGFolder = m_strAppLogPath & "\" & strFolderNm

            If (Not Directory.Exists(strLOGFolder)) Then
                Directory.CreateDirectory(strLOGFolder)
            End If

            If m_blnAppLogOption = True Then 'Log enable and disable flag
                If m_strAppEvtLogFileNM.Trim.Length > 0 Then
                    strAppLogNM = strLOGFolder & "\" & m_strAppEvtLogFileNM
                    m_strTextLogFile = Trim(strAppLogNM)
                    m_objAppEvtTextLog = New StreamWriter(m_strTextLogFile, True)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If


        Catch ex As Exception
            Return False
            'strErrMsg = "Error in Created Log File."
            'strErrMsg = strErrMsg & ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Exclamation, "clsLogger - InitLogger")
        End Try
    End Function


    Public Function CloseLogger() As Boolean
        Try

            If (Not IsNothing(m_objAppEvtTextLog)) Then
                m_objAppEvtTextLog.Close()
            End If

            Return True

        Catch ex As Exception
            Return False
            'strErrMsg = "Error in Close Log File."
            'strErrMsg = strErrMsg & ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Exclamation, "clsLogger - CloseLogger")

        End Try
    End Function

    Public Sub AppEvtLog(ByVal LOGMsgType As LoggerMsgTypeEnum, ByVal LogMessages As String)
        Try
            If m_blnAppLogOption = True Then
                TxtLogWriteLine(m_objAppEvtTextLog, LogMessages, LOGMsgType)
            End If
        Catch ex As Exception
            'strErrMsg = "Error in Write AppEvt Log File."
            'strErrMsg = strErrMsg & ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Exclamation, "clsLogger - AppEvtLog")
        End Try
    End Sub

    Private Sub TxtLogWriteLine(ByRef m_objTextLog As StreamWriter, ByVal strLogMsg As String, ByVal EvtType As LoggerMsgTypeEnum)
        Try
            If (Not IsNothing(m_objTextLog)) Then

                Select Case EvtType
                    Case LoggerMsgTypeEnum.LOGINFO
                        strLogMsg = Today & "|" & TimeOfDay & "|" & "Info" & "|" & strLogMsg
                    Case LoggerMsgTypeEnum.LOGERROR
                        'strLogMsg = Format(DateTime.Now(), "G") & "|" & "Error" & "|" & strLogMsg
                        strLogMsg = Today & "|" & TimeOfDay & "|" & "Erro" & "|" & strLogMsg
                    Case LoggerMsgTypeEnum.LOGWARNING
                        'strLogMsg = Format(DateTime.Now(), "G") & "|" & "Warning" & "|" & strLogMsg
                        strLogMsg = Today & "|" & TimeOfDay & "|" & "Warn" & "|" & strLogMsg
                End Select

                m_objTextLog.WriteLine(strLogMsg)
                m_objTextLog.Flush()

            End If
        Catch ex As Exception
            'strErrMsg = "Error in creating an instance of Logger class" & ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Information, "clsLogger - TxtLogWriteLine")
        End Try
    End Sub

#End Region


End Class
