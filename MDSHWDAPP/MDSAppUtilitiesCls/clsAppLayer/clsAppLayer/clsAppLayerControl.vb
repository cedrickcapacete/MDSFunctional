Imports System
Imports System.IO
Imports FileUtils
Imports clsAppMainDirectory

Public Class clsAppLayerControl


#Region "App Main Directory"
    Private objAppPath As New clsAppMainDirectoryControl
#End Region


#Region "Cls Variable"
    Private strTitle As String = "modReadAppLogCFG"
    Private objINIFile As IniFile = Nothing


    Private strFileINIPath As String = String.Empty
    Private strErrMsg As String = String.Empty
#End Region


#Region "AppIni CFG Control - Ini File Value"

    '[INICFG]
    ';INIFILEPATH1 - Hardware Control
    'INIFILEPATH1="...\AppIniFile\AppLayerIni\AppLayerCFGSetting\HWDParamCFG.INI"

    ';INIFILEPATH2 - DB Control
    'INIFILEPATH2="...\AppIniFile\AppLayerIni\AppLayerDBCFGSetting\MachineCTRCLSDB.ini"

    ';INIFILEPATH3
    'INIFILEPATH3=""

    ';INIFILEPATH4
    'INIFILEPATH4=""

    ';INIFILEPATH5
    'INIFILEPATH5=""

    ';INIFILEPATH6
    'INIFILEPATH6=""

    ';INIFILEPATH7
    'INIFILEPATH7=""

    ';INIFILEPATH8
    'INIFILEPATH8=""

    ';INIFILEPATH9
    'INIFILEPATH9=""

    ';INIFILEPATH10
    'INIFILEPATH10=""

    'Ini File - Session 
    Private Const APPLAYER_INISEC As String = "INICFG"

    'Ini File - Log Session Key 
    '0-Disable the Local App Path 1-Enable Local App Path
    Private Const AppPATHCTR_OPT As String = "AppPathCtrOpt"

    'Ini File -  Session Key 
    Private Const INIPATH1_KEY As String = "INIFILEPATH1"
    Private Const INIPATH2_KEY As String = "INIFILEPATH2"
    Private Const INIPATH3_KEY As String = "INIFILEPATH3"
    Private Const INIPATH4_KEY As String = "INIFILEPATH4"
    Private Const INIPATH5_KEY As String = "INIFILEPATH5"
    Private Const INIPATH6_KEY As String = "INIFILEPATH6"
    Private Const INIPATH7_KEY As String = "INIFILEPATH7"
    Private Const INIPATH8_KEY As String = "INIFILEPATH8"
    Private Const INIPATH9_KEY As String = "INIFILEPATH9"
    Private Const INIPATH10_KEY As String = "INIFILEPATH10"

    '[LogCFG]
    ';Log Path
    'LogPath="...\AppIniFile\AppLayerIni\AppLayerLogSetting\SCDBLayerLogCFG.INI"
    'Ini File - Session 
    Private Const APPCFGLOG_SECT As String = "LogCFG"
    'Ini File -  Session Key 
    Private Const LOGPATH_KEY As String = "LogPath"


#End Region

#Region "Class Structure"

    'App Layer Ini File Structure Configuration
    Structure ClsAppLayerIniSTR
        'Comm Function
        Dim blnEditINI1 As Boolean
        Dim strAppLayerINIPath1 As String
        Dim blnEditINI2 As Boolean
        Dim strAppLayerINIPath2 As String
        Dim blnEditINI3 As Boolean
        Dim strAppLayerINIPath3 As String
        Dim blnEditINI4 As Boolean
        Dim strAppLayerINIPath4 As String
        Dim blnEditINI5 As Boolean
        Dim strAppLayerINIPath5 As String
        Dim blnEditINI6 As Boolean
        Dim strAppLayerINIPath6 As String
        Dim blnEditINI7 As Boolean
        Dim strAppLayerINIPath7 As String
        Dim blnEditINI8 As Boolean
        Dim strAppLayerINIPath8 As String
        Dim blnEditINI9 As Boolean
        Dim strAppLayerINIPath9 As String
        Dim blnEditINI10 As Boolean
        Dim strAppLayerINIPath10 As String
        Dim blnEditINILog As Boolean
        Dim strLogINIPath As String
    End Structure

    Public udtClsAppLayerIniCFG As ClsAppLayerIniSTR

#End Region

#Region "Cls Function"



    Public Function ReadAppLayerINICFGFile(ByVal strAppLayerINIPath As String, ByVal MDSModules As clsAppMainDirectoryControl.MDSHWD) As Boolean
        Dim strFileINIPath As String = String.Empty
        Dim strtmpMainAppPath As String = String.Empty
        Dim blnAppPathCtr As Boolean


        Try

            'Check File Exists or Not
            'APPLayer\...
            strFileINIPath = GetMainSystemPathControl(MDSModules) & strAppLayerINIPath.Trim
            strFileINIPath = strFileINIPath.Trim

            If (File.Exists(strFileINIPath)) Then

                'Ini File Path
                objINIFile = New IniFile(strFileINIPath, False)

                With udtClsAppLayerIniCFG

                    'Section Key - Ini File Path
                    objINIFile.CurrentSection = APPLAYER_INISEC

                    'App Path Control
                    strtmpMainAppPath = ""
                    blnAppPathCtr = False

                    blnAppPathCtr = objINIFile.GetBoolVal(AppPATHCTR_OPT, True) 'Get APP Path Control

                    If blnAppPathCtr = True Then
                        'Section Key - Ini File Path
                        strtmpMainAppPath = GetMainSystemPathControl(MDSModules)
                        strtmpMainAppPath = strtmpMainAppPath.Trim
                    Else
                        'App Main Path
                        strtmpMainAppPath = ""
                    End If


                    .strAppLayerINIPath1 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH1_KEY, String.Empty)
                    .strAppLayerINIPath2 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH2_KEY, String.Empty)
                    .strAppLayerINIPath3 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH3_KEY, String.Empty)
                    .strAppLayerINIPath4 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH4_KEY, String.Empty)
                    .strAppLayerINIPath5 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH5_KEY, String.Empty)
                    .strAppLayerINIPath6 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH6_KEY, String.Empty)
                    .strAppLayerINIPath7 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH7_KEY, String.Empty)
                    .strAppLayerINIPath8 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH8_KEY, String.Empty)
                    .strAppLayerINIPath9 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH9_KEY, String.Empty)
                    .strAppLayerINIPath10 = strtmpMainAppPath & objINIFile.GetStrVal(INIPATH10_KEY, String.Empty)

                    'Section Key - Ini Logger
                    objINIFile.CurrentSection = APPCFGLOG_SECT
                    .strLogINIPath = strtmpMainAppPath & objINIFile.GetStrVal(LOGPATH_KEY, String.Empty)

                End With

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadAppLayerINICFGFile."
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try

    End Function


    Public Function UpdateAppLayerINICFGFile(ByVal strAppLayerINIPath As String, ByVal udtAppLayerINIValue As ClsAppLayerIniSTR, ByVal MDSModules As clsAppMainDirectoryControl.MDSHWD) As Boolean
        Dim strFileINIPath As String = String.Empty
        Try

            'Check File Exists or Not
            'APPLayer\...
            strFileINIPath = GetMainSystemPathControl(MDSModules) & strAppLayerINIPath.Trim
            strFileINIPath = strFileINIPath.Trim

            If (File.Exists(strFileINIPath)) Then

                'Ini File Path
                objINIFile = New IniFile(strFileINIPath, False)

                With udtAppLayerINIValue

                    'Section Key - Ini File Path
                    objINIFile.CurrentSection = APPLAYER_INISEC

                    If .blnEditINI1 = True Then
                        objINIFile.SetVal(INIPATH1_KEY, """" & .strAppLayerINIPath1 & """")
                    End If

                    If .blnEditINI2 = True Then
                        objINIFile.SetVal(INIPATH2_KEY, """" & .strAppLayerINIPath2 & """")
                    End If

                    If .blnEditINI3 = True Then
                        objINIFile.SetVal(INIPATH3_KEY, """" & .strAppLayerINIPath3 & """")
                    End If

                    If .blnEditINI4 = True Then
                        objINIFile.SetVal(INIPATH4_KEY, """" & .strAppLayerINIPath4 & """")
                    End If

                    If .blnEditINI5 = True Then
                        objINIFile.SetVal(INIPATH5_KEY, """" & .strAppLayerINIPath5 & """")
                    End If

                    If .blnEditINI6 = True Then
                        objINIFile.SetVal(INIPATH6_KEY, """" & .strAppLayerINIPath6 & """")
                    End If

                    If .blnEditINI7 = True Then
                        objINIFile.SetVal(INIPATH7_KEY, """" & .strAppLayerINIPath7 & """")
                    End If

                    If .blnEditINI8 = True Then
                        objINIFile.SetVal(INIPATH8_KEY, """" & .strAppLayerINIPath8 & """")
                    End If

                    If .blnEditINI9 = True Then
                        objINIFile.SetVal(INIPATH9_KEY, """" & .strAppLayerINIPath9 & """")
                    End If

                    If .blnEditINI10 = True Then
                        objINIFile.SetVal(INIPATH10_KEY, """" & .strAppLayerINIPath10 & """")
                    End If

                    'Section Key - Ini Logger
                    If .blnEditINILog = True Then
                        objINIFile.CurrentSection = APPCFGLOG_SECT
                        objINIFile.SetVal(LOGPATH_KEY, """" & .strLogINIPath & """")
                    End If

                End With

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in UpdateAppLayerINICFGFile."
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try

    End Function


#End Region


#Region "Get Main System Folder Control"

    Private Function GetMainSystemPathControl(ByVal MDSModules As clsAppMainDirectoryControl.MDSHWD) As String
        Dim strMainAppDrive As String = String.Empty
        Try
            'GET the System Main Dirve Folder and Backup
            strMainAppDrive = objAppPath.GetAppMainDirectory(MDSModules)
            Return strMainAppDrive
        Catch ex As Exception
            Return ""
            strErrMsg = "Error in GetMainSystemPathControl."
        End Try
    End Function

#End Region

End Class
