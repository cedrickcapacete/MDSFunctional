Imports System
Imports System.IO
Imports FileUtils

Module modReadINI

#Region "Cls Variable"
    Private strTitle As String = "modReadINI"
    Private objINIFile As IniFile = Nothing
    Private strFileINIPath As String = ""
    Private strErrMsg As String = ""
#End Region


#Region "App Mode Control"

    '[APPCFG]
    ';App Control
    ';1=Maintenance Mode
    ';0=System Debug Mode
    ';Default Value = 1
    'AppMode=1

    Private Const APPCFG_SEC As String = "APPCFG"
    'Ini File - Refersh Session Key 
    Private Const APP_MODE As String = "AppMode"

#End Region


#Region "Read App Mode"

    Public Function ReadAPPMode(ByVal strINIPath As String) As String
        Dim strTempAppMode As String = ""
        Try

            strINIPath = strINIPath.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = APPCFG_SEC

                strTempAppMode = objINIFile.GetStrVal(APP_MODE, "1")
                'Log the Beeper CFG
                AppLogInfo("App Mode =" & strTempAppMode)

                Return strTempAppMode
            Else
                'Ini File Path Not Found
                AppLogWarn("App Mode INI File:" & strINIPath & " Not Found")
                Return "1"
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadAppMode. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return "1"
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

#End Region


End Module
