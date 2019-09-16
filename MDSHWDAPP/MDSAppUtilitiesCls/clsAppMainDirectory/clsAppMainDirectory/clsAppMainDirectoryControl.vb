Imports System
Imports System.IO
Imports FileUtils

Public Class clsAppMainDirectoryControl

#Region "INI Object"
    Private objMainINIFile As IniFile = Nothing
#End Region

#Region "Default Setting Value"
    'Default INI File Name
    Dim MDSINIFILENAME As String = "MDSHWDCFG.INI"

    Dim strDefaultMainINIDirectoryPath As String = "E:\MDSHWDAPP\MDSHWD\MDSHWDCFG.INI"

    'Default Path
    Dim strMainINIDirectoryPath As String = String.Empty
#End Region

#Region "Variable"

    Dim strMainDirectory As String = String.Empty
    Dim strINISectionKey As String = String.Empty

#End Region

#Region "MDSHardware Enum"

    Public Enum MDSHWD
        CARDREADER = 1
        MDS = 2
        EPP = 3
        THERMALPRINTER = 4
        UPS = 5
        BARCODE = 6
        BEEPER = 7
        RSI = 8
    End Enum

#End Region



#Region "Main Directory Control"

    Public Function GetAppMainDirectory(ByVal MDSHWDModules As MDSHWD) As String
        Try

            'Default Path

            strMainINIDirectoryPath = GetMDSPATH() & MDSINIFILENAME

            If strMainINIDirectoryPath.Trim.Length = 0 Then
                'if error - path not found
                strMainINIDirectoryPath = strDefaultMainINIDirectoryPath.Trim
            End If

            objMainINIFile = New IniFile(strMainINIDirectoryPath, False)

            'Section Key - Ini File Path
            objMainINIFile.CurrentSection = "MDSHWDMODULES"

            'Item to change to the particular hardware
            strINISectionKey = ProcessMDSEnum(MDSHWDModules)
            strMainDirectory = objMainINIFile.GetStrVal(strINISectionKey, String.Empty)

            Return strMainDirectory

        Catch ex As Exception
            'Error in the GetMainDirectory
            Return ""
        Finally
            If Not (objMainINIFile Is Nothing) Then
                objMainINIFile = Nothing
            End If
        End Try
    End Function

    Private Function ProcessMDSEnum(ByVal MDSHWDModules As MDSHWD) As String
        Dim strKeyValue As String = String.Empty
        Try

            Select Case MDSHWDModules
                Case MDSHWD.CARDREADER
                    strKeyValue = "CARDREADER"
                Case MDSHWD.MDS
                    strKeyValue = "MDS"
                Case MDSHWD.EPP
                    strKeyValue = "EPP"
                Case MDSHWD.THERMALPRINTER
                    strKeyValue = "THERMALPRINTER"
                Case MDSHWD.UPS
                    strKeyValue = "UPS"
                Case MDSHWD.BARCODE
                    strKeyValue = "BARCODE"
                Case MDSHWD.BEEPER
                    strKeyValue = "BEEPER"
                Case MDSHWD.RSI
                    strKeyValue = "RSI"
                Case Else
                    strKeyValue = ""
            End Select

            Return strKeyValue

        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Function GetMDSPATH() As String
        Dim strMDSPath As String = String.Empty
        Try
            'get the system environment setting
            strMDSPath = Environment.GetEnvironmentVariable("MDSPATH")
            Return strMDSPath
        Catch ex As Exception
            Return ""
        End Try
    End Function


#End Region



End Class
