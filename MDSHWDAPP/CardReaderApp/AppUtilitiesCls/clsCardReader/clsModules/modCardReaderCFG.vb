Imports System
Imports System.IO
Imports FileUtils
Imports clsCardReader.clsCardReaderControl

Module modCardReaderCFG

#Region "Cls Variable"
    Private strTitle As String = "modReadCardReaderCFG"
    Private objINIFile As IniFile = Nothing
    Private strErrMsg As String = String.Empty
    Dim strINIPath As String = String.Empty
#End Region


#Region "Ini - File Info Session - CardReader"

    '[CARDREADERSETTING]
    ';CARDREADER Checking
    ';1 - Enable, 0 - Disable
    'ENABLE=1

    Private Const CARDREADERCFG_SEC As String = "CARDREADERSETTING"
    'Ini File - Log Session Key 
    Private Const CARDREADER_ENABLE As String = "ENABLE"

    ';CARDREADER Comport Setting
    'Comport=5
    'Baudrate=9600

    Private Const CARDREADERCOMPORT_NUM As String = "Comport"
    Private Const CARDREADERCOMPORT_BOUNDRATE As String = "Baudrate"

    ';Hardware Timeout Control
    ';1 SEC = 1000
    'HWDTIMEOUTINTERVAL=30000
    Private Const CARDREADERCMD_TIMEOUT As String = "CARDREADERCOMMANDTIMEOUT"
    Private Const CARDREADERCHECK_TIMEOUT As String = "CARDREADERCHECKTIMEOUT"
    Private Const CARDREADEREJECT_TIMEOUT As String = "CARDREADEREJECTTIMEOUT"

    Private Const CARDREADERMAXCAPTURECOUNT As String = "CARDCAPTUREMAXCOUNT"

    Private Const PMPCPINFILEPATH As String = "PMPCPINFilePath"
#End Region


#Region "Main Directory Control"

    Private objMainINIFile As IniFile = Nothing
    Dim strMainINIDirectoryPath As String = String.Empty
    Dim strMainDirectory As String = String.Empty

#End Region

#Region "Clear INI Structure Setting"

    Private Sub CLSCARDREADERHWDSTR()
        Try
            'Clear CardReader Hardware Structure
            With udtCARDREADERHWDCFG

                .blnCARDREADERENABLE = True

                .CARDREADERPortNum = "1"
                .CARDREADERBaudRate = "19200"

                'Default 30 sec
                .CARDREADERCMDTimeout = 30
                .CARDREADERCheckTimeout = 3
                .CARDREADEREjectTimeout = 10

                .CARDREADERMaxCaptureCount = 0
                .PMPCPINFilePath = String.Empty

            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSCARDREADERHWDSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region


#Region "Read Ini File"


    Private Sub GetMainDirectory()
        Try

            'Default Path
            'E:\HWDAPP\MDSHWD\MDSHWDCFG.INI
            strMainINIDirectoryPath = "E:\MDSHWDAPP\MDSHWD\MDSHWDCFG.INI"
            objMainINIFile = New IniFile(strMainINIDirectoryPath, False)

            'Section Key - Ini File Path
            objMainINIFile.CurrentSection = "MDSHWDMODULES"
            strMainDirectory = objMainINIFile.GetStrVal("CARDREADER", String.Empty)

        Catch ex As Exception
            'Error in the GetMainDirectory
        Finally
            If Not (objMainINIFile Is Nothing) Then
                objMainINIFile = Nothing
            End If
        End Try
    End Sub

    Public Function clsReadCARDREADERSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty
        Try

            GetMainDirectory()

            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File 
                objINIFile.CurrentSection = CARDREADERCFG_SEC
                With udtCARDREADERHWDCFG
                    .blnCARDREADERENABLE = objINIFile.GetBoolVal(CARDREADER_ENABLE, True)

                    .CARDREADERPortNum = objINIFile.GetStrVal(CARDREADERCOMPORT_NUM, "1")
                    .CARDREADERBaudRate = objINIFile.GetStrVal(CARDREADERCOMPORT_BOUNDRATE, "19200")

                    .CARDREADERCMDTimeout = objINIFile.GetLongVal(CARDREADERCMD_TIMEOUT, 30)
                    .CARDREADERCheckTimeout = objINIFile.GetLongVal(CARDREADERCHECK_TIMEOUT, 3)
                    .CARDREADEREjectTimeout = objINIFile.GetLongVal(CARDREADEREJECT_TIMEOUT, 10)

                    .CARDREADERMaxCaptureCount = objINIFile.GetIntVal(CARDREADERMAXCAPTURECOUNT, 3)

                    '.PMPCPINFilePath = strMainDirectory & objINIFile.GetStrVal(PMPCPINFILEPATH, "PSAM\PSAMID.txt")

                    .PMPCPINFilePath = objINIFile.GetStrVal(PMPCPINFILEPATH, "E:\MDSHWDAPP\MDSHWD\CardReaderApp\PSAM\PSAMID.ini")

                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadCardReaderSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function


    Public Function UpdateCRSetting(ByVal strCREnableOpt As String, ByVal strCRComport As String, ByVal strCRCmdTMOut As String, ByVal strCRChkTMOut As String, ByVal strCREjtTMOut As String, ByVal strPSAMIDPath As String) As Boolean
        Try

            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File 
                With objINIFile
                    .CurrentSection = CARDREADERCFG_SEC

                    .SetVal(CARDREADER_ENABLE, strCREnableOpt)

                    .SetVal(CARDREADERCOMPORT_NUM, strCRComport)

                    .SetVal(CARDREADERCMD_TIMEOUT, strCRCmdTMOut)
                    .SetVal(CARDREADERCHECK_TIMEOUT, strCRChkTMOut)
                    .SetVal(CARDREADEREJECT_TIMEOUT, strCREjtTMOut)

                    .SetVal(PMPCPINFILEPATH, strPSAMIDPath)

                End With

                AppLogInfo("UpdateCRSetting Success")
                Return True
            Else
                AppLogWarn("File Not Exist:" & strINIPath)
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in UpdateCRSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function






    Public Sub WriteCardInsertCheckTimeout(ByVal value As Integer)
        Try
            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File 
                objINIFile.CurrentSection = CARDREADERCFG_SEC
                objINIFile.SetVal(CARDREADERCHECK_TIMEOUT, value)
                AppLogInfo("WriteCardInsertCheckTimeout Success")
            Else
                AppLogWarn("File Not Exist:" & strINIPath)
            End If
        Catch ex As Exception
            AppLogErr("Error In WriteCardInsertCheckTimeout: " & ex.Message)
        End Try
    End Sub

    Public Sub WriteCardAtGateTimeout(ByVal value As Integer)
        Try
            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File 
                objINIFile.CurrentSection = CARDREADERCFG_SEC
                objINIFile.SetVal(CARDREADEREJECT_TIMEOUT, value)
                AppLogInfo("WriteCardAtGateTimeout Success")
            Else
                AppLogWarn("File Not Exist:" & strINIPath)
            End If
        Catch ex As Exception
            AppLogErr("Error In WriteCardAtGateTimeout: " & ex.Message)
        End Try
    End Sub

    Public Sub WriteCardCommandTimeout(ByVal value As Integer)
        Try
            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File 
                objINIFile.CurrentSection = CARDREADERCFG_SEC
                objINIFile.SetVal(CARDREADERCMD_TIMEOUT, value)
                AppLogInfo("WriteCardCommandTimeout Success")
            Else
                AppLogWarn("File Not Exist:" & strINIPath)
            End If
        Catch ex As Exception
            AppLogErr("Error In WriteCardCommandTimeout: " & ex.Message)
        End Try
    End Sub


#End Region

End Module
