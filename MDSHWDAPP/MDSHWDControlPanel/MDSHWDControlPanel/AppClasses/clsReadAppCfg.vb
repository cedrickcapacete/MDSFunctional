Imports System
Imports System.IO

Public Class clsReadAppCfg


#Region "Cls Variable"
    'Private strTitle As String = "clsReadAppCfg"
    Private objINIFile As clsINIFunc = Nothing
    Private strFileINIPath As String = String.Empty
#End Region

#Region "Ini - File Info Session"

    '[HSKCFG]
    ';Housekeeping Log Path
    'SysLogFilePath=D:\NetUtilitiesApp\QConsoleControlPanel\Log
    ';Housekeeping Days Control
    ';Log File Housekeeping
    'LogFileHSKDay=30
    ';AutoHousekeeping During App Loading
    ';1=Enable HouseKeeping 0=Disable Keeping
    'AutoHSKLOG=1

    Private Const HSK_SECT_INI As String = "HSKCFG"
    'Ini File - Refersh Session Key 
    Private Const HSK_Path As String = "SysLogFilePath"
    Private Const HSK_Mode As String = "AutoHSKLOG"
    Private Const HSK_Days As String = "LogFileHSKDay"

    'Ini File - Log Session Key 
    '0-Disable the Local App Path 1-Enable Local App Path
    Private Const AppPATHCTR_OPT As String = "AppPathCtrOpt"


#End Region

#Region "Ini File Property - Housekeeping"

    Structure AppHSKSTR
        Dim HSKLogFilePath As String
        Dim blnHSKLogMode As Boolean
        Dim dblHSKLogDay As Double
    End Structure

    Public udtAppHSKCfg As AppHSKSTR

#End Region

#Region "Read App Ini File Path - ReadAppIniFile"

    Public Sub Dispose()
        If Not (objINIFile Is Nothing) Then
            objINIFile = Nothing
        End If
    End Sub

    Public Function InitAppHSKCFGFile(ByVal strIniPath As String) As Boolean
        Try
            strFileINIPath = strIniPath
            ReadAppCFGFile()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ReadAppCFGFile()

        Dim strMainAppPath As String = String.Empty
        Dim blnAppPathCtr As Boolean


        Try
            'Init Variable
            With udtAppHSKCfg
                .HSKLogFilePath = ""
                .blnHSKLogMode = False
                .dblHSKLogDay = LOGFILEHSKDAY
            End With

            If (File.Exists(strFileINIPath)) Then
                objINIFile = New clsINIFunc(strFileINIPath, False)

                With udtAppHSKCfg
                    'Log Housekeeping
                    objINIFile.CurrentSection = HSK_SECT_INI


                    'App Path Control
                    strMainAppPath = ""
                    blnAppPathCtr = False

                    blnAppPathCtr = objINIFile.GetBoolVal(AppPATHCTR_OPT, True) 'Get APP Path Control
                    If blnAppPathCtr = True Then
                        'App Main Path
                        strMainAppPath = GetMainSystemPathControl()
                    Else
                        'App Main Path
                        strMainAppPath = ""
                    End If


                    .HSKLogFilePath = strMainAppPath & objINIFile.GetStrVal(HSK_Path, String.Empty)
                    .blnHSKLogMode = objINIFile.GetBoolVal(HSK_Mode, True)
                    .dblHSKLogDay = objINIFile.GetDblVal(HSK_Days, LOGFILEHSKDAY)

                End With

            End If

        Catch ex As Exception
            'strErrMsg = "Error in ReadAppINIFile."
        End Try
    End Sub

#End Region


End Class
