Imports System
Imports System.IO
Imports FileUtils

Module modReadPSAMIDKey

#Region "Cls Variable"
    Private strTitle As String = "modReadPSAMIDKey"
    Private objINIFile As IniFile = Nothing
    Private strErrMsg As String = String.Empty
    Dim strINIPath As String = String.Empty
#End Region

#Region "INI - PSAM KEY INI"

    '[PSAMIDKEY]
    'PSAMIDKEY=""

    Private Const PSAMKEY_SECT As String = "PSAMIDKEY"
    Private Const PSAMKEY_VALUE As String = "PSAMIDKEY"

#End Region


#Region "ReadPSAMIDKeyValue"

    Public Function ReadPSAMIDKeyValue(ByVal strINIPath As String) As String
        Dim strAppPath As String = ""
        Dim strPSAMEncryptedKey As String = ""
        Dim strPSAMKeyValue As String = ""
        Dim objDecryption As New clsEnDecrytionControl
        
        Try

            'Defautl INI File Path
            strINIPath = strINIPath.Trim

            strPSAMKeyValue = ""

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = PSAMKEY_SECT
                strPSAMEncryptedKey = objINIFile.GetStrVal(PSAMKEY_VALUE, "")
                strPSAMKeyValue = objDecryption.Decrypt(strPSAMEncryptedKey)

                AppLogInfo("PSAMKEYValue=" & strPSAMKeyValue)

                Return strPSAMKeyValue
            Else
                'Ini File Path Not Found
                AppLogWarn("ReadPSAMIDKeyValue INI File Not Found.Path=" & strINIPath)
                strPSAMKeyValue = ""
                Return strPSAMKeyValue
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadPSAMIDKeyValue. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            strPSAMKeyValue = ""
            Return strPSAMKeyValue
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
            If Not (objDecryption Is Nothing) Then
                objDecryption = Nothing
            End If
        End Try
    End Function

#End Region


End Module
