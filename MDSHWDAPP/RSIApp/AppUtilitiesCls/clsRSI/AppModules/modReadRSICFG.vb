Imports System
Imports System.IO
Imports FileUtils
Imports clsRSI.clsAppStructure

Module modReadRSICFG

#Region "Cls Variable"
    Private strTitle As String = "modReadRSICFG"
    Private objINIFile As IniFile = Nothing
    Private strErrMsg As String = ""
    Dim strINIPath As String = ""
#End Region


#Region "Ini - File Info Session - RSI"

#Region "RSI SETTING"

    Private Const RSI_HWD_SEC As String = "RSIHWDCFG"

    'Ini File - Log Session Key 
    Private Const RSI_HWD_KEY_RSIENABLE As String = "RSIENABLE"

    ';Number of RSI Init Comport
    'NUMRSIComport=2
    Private Const RSI_HWD_KEY_NUMPORT As String = "NUMRSIComport"

    ';Comport CONTROL
    ';Comport 1
    ';Comport Init - 1=Enable 0=Disable
    ';Comport Number
    ';0-None 1-Odd Parity 2-Even Parity, 3-Make 4-Space
    'Comport1Num=5
    'Baudrate1=9600
    'Parity1=0
    'Databit1=8
    'Stopbits1=1

    Private Const RSI_HWD_KEY_COMPORT1 As String = "Comport1Num"
    Private Const RSI_HWD_KEY_BAUDRATE1 As String = "Baudrate1"
    Private Const RSI_HWD_KEY_PARITY1 As String = "Parity1"
    Private Const RSI_HWD_KEY_DATABITS1 As String = "Databits1"
    Private Const RSI_HWD_KEY_STOPBITS1 As String = "Stopbits1"

    ';Comport 2
    ';Comport Init - 1=Enable 0=Disable
    ';Comport Number
    ';0-None 1-Odd Parity 2-Even Parity, 3-Make 4-Space
    'Comport2Num=6
    'Baudrate2=9600
    'Parity2=0
    'Databit2=8
    'Stopbits2=1

    Private Const RSI_HWD_KEY_COMPORT2 As String = "Comport2Num"
    Private Const RSI_HWD_KEY_BAUDRATE2 As String = "Baudrate2"
    Private Const RSI_HWD_KEY_PARITY2 As String = "Parity2"
    Private Const RSI_HWD_KEY_DATABITS2 As String = "Databits2"
    Private Const RSI_HWD_KEY_STOPBITS2 As String = "Stopbits2"


#End Region

#Region "RSI LOOKUP DATA"

    Private Const PRTLOOKUPDATA_SEC As String = "RSILOOKUPDATA"
    'Ini File - Log Session Key 
    Private Const INI_KEY_DEVICE_GRAPHIC_ID As String = "DEVICE_GRAPHIC_ID"
    Private Const INI_KEY_DEVICE_NAME As String = "DEVICE_NAME"
    Private Const INI_KEY_TXN_STATUS_RSI_SUCCESS As String = "TXN_ST_RSI_SUCCESS"
    Private Const INI_KEY_TXN_STATUS_RSI_FAILED As String = "TXN_ST_RSI_FAILED"
    Private Const INI_KEY_ERR_SEV_OK As String = "ERST_RSI_OK"
    Private Const INI_KEY_ERR_SEV_ERROR As String = "ERST_RSI_ERROR"
    Private Const INI_KEY_ERR_SEV_WARNING As String = "ERST_RSI_WARNING"
    Private Const INI_KEY_ERR_SEV_FATAL As String = "ERST_RSI_FATAL"
    Private Const INI_KEY_DIAGNOS_MSTATUS As String = "M_STATUS"
    Private Const INI_KEY_DIAGNOS_MSTATUSDATA As String = "M_STATUS_DATA"
    Private Const INI_KEY_SOUND_ON As String = "SOUND_ON"
    Private Const INI_KEY_SOUND_OFF As String = "SOUND_OFF"
    Private Const INI_KEY_DEVICESTATE_RSION As String = "DST_RSIONSTATE"
    Private Const INI_KEY_DEVICESTATE_RSIOFF As String = "DST_RSIOFFSTATE"
    Private Const INI_KEY_EVT_DEVICE_ERROR As String = "EVTTYPE_DEVICEERROR"
    Private Const INI_KEY_EVT_DEVICE_READY As String = "EVTTYPE_DEVICEWRAP"
    Private Const INI_KEY_EVT_DEVICE_WRAP As String = "EVTTYPE_DEVICEREADY"

#End Region

#End Region


#Region "Clear INI Structure Setting"

    Private Sub CLSRSIHWDSTR()
        Try

            'Init HWD Setting Structure
            With udtRSIHwdCFG

                .blnEnableRSI = False

                .strNumofRSI = "2"

                .strC1Comport = "5"
                .strC1Baudrate = "9600"
                .strC1Parity = "0"
                .strC1Databits = "8"
                .strC1Stopbits = "1"

                .strC2Comport = "6"
                .strC2Baudrate = "9600"
                .strC2Parity = "0"
                .strC2Databits = "8"
                .strC2Stopbits = "1"

            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSRSIHWDSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub


    Private Sub CLSRSILOOKUPDATASTR()
        Try
            'Clear RSI Lookup Data Setting Structure
            With udtRSIHookupCFG
                .DeviceGraphicID = "Z"
                .DeviceName = "RSI"

                .TxnStatusRSISoundOK = "0"
                .TxnStatusRSISoundFailed = "1"

                .ErrSvrtyPrtOk = "1"
                .ErrSvrtyPrtError = "2"
                .ErrSvrtyPrtWarning = "3"
                .ErrSvrtyPrtFatal = "4"

                .MStatus = ""
                .MStatusData = ""

                .RSISuplyStatus = "1"
                .SoundOn = "2"
                .SoundOff = "3"

                .DeviceStateRSIOff = "0"
                .DeviceStateRSIOn = "1"
                .EvtDeviceErr = ""
                .EvtDeviceReady = ""
                .EvtDeviceWrap = ""
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSRSILOOKUPDATASTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region


#Region "Read Ini File"

    Public Function ReadRSIHWDSetting(ByVal strRSIHWDIniPath As String) As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear RSI Structure
            CLSRSIHWDSTR()

            'Defautl INI File Path - RSIHWDSETTING.INI.INI
            strINIPath = strRSIHWDIniPath.Trim

            If (File.Exists(strINIPath)) Then

                objINIFile = New IniFile(strINIPath, False)

                objINIFile.CurrentSection = RSI_HWD_SEC

                With udtRSIHwdCFG

                    .blnEnableRSI = objINIFile.GetBoolVal(RSI_HWD_KEY_RSIENABLE, False)

                    .strNumofRSI = objINIFile.GetStrVal(RSI_HWD_KEY_NUMPORT, "2")

                    .strC1Comport = objINIFile.GetStrVal(RSI_HWD_KEY_COMPORT1, "5")
                    .strC1Baudrate = objINIFile.GetStrVal(RSI_HWD_KEY_BAUDRATE1, "9600")
                    .strC1Parity = objINIFile.GetStrVal(RSI_HWD_KEY_PARITY1, "0")
                    .strC1Databits = objINIFile.GetStrVal(RSI_HWD_KEY_DATABITS1, "8")
                    .strC1Stopbits = objINIFile.GetStrVal(RSI_HWD_KEY_STOPBITS1, "1")

                    .strC2Comport = objINIFile.GetStrVal(RSI_HWD_KEY_COMPORT2, "6")
                    .strC2Baudrate = objINIFile.GetStrVal(RSI_HWD_KEY_BAUDRATE2, "9600")
                    .strC2Parity = objINIFile.GetStrVal(RSI_HWD_KEY_PARITY2, "0")
                    .strC2Databits = objINIFile.GetStrVal(RSI_HWD_KEY_DATABITS2, "8")
                    .strC2Stopbits = objINIFile.GetStrVal(RSI_HWD_KEY_STOPBITS2, "1")

                End With

                'Log the RSI CFG
                AppLogInfo("== RSI Hardware Setting ==")

                With udtRSIHwdCFG
                    AppLogInfo("Enable RSI=" & .blnEnableRSI)

                    AppLogInfo("RSI No of Comport=" & .strNumofRSI.Trim)

                    AppLogInfo("Comport 1=" & .strC1Comport.Trim)
                    AppLogInfo("Baudrate=" & .strC1Baudrate.Trim)
                    AppLogInfo("Parity=" & .strC1Parity.Trim)
                    AppLogInfo("Databits=" & .strC1Databits.Trim)
                    AppLogInfo("Stopbits=" & .strC1Stopbits.Trim)

                    AppLogInfo("Comport 2=" & .strC2Comport.Trim)
                    AppLogInfo("Baudrate=" & .strC2Baudrate.Trim)
                    AppLogInfo("Parity=" & .strC2Parity.Trim)
                    AppLogInfo("Databits=" & .strC2Databits.Trim)
                    AppLogInfo("Stopbits=" & .strC2Stopbits.Trim)
                End With

                AppLogInfo("== RSI Hardware Setting End==")


                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("RSI INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadRSIHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function


    Public Function UpdateRSIHWDSetting(ByVal strRSIEnable As String, ByVal strNumofRSI As String, ByVal strComport1 As String, ByVal strComport2 As String, ByVal strRSIHWDIniPath As String) As Boolean
        Try

            'Defautl INI File Path - RSIHWDSETTING.INI
            strINIPath = strRSIHWDIniPath.Trim

            If (File.Exists(strINIPath)) Then

                objINIFile = New IniFile(strINIPath, False)

                With objINIFile
                    .CurrentSection = RSI_HWD_SEC
                    .SetVal(RSI_HWD_KEY_RSIENABLE, strRSIEnable.Trim)
                    .SetVal(RSI_HWD_KEY_NUMPORT, strNumofRSI.Trim)
                    .SetVal(RSI_HWD_KEY_COMPORT1, strComport1.Trim)
                    .SetVal(RSI_HWD_KEY_COMPORT2, strComport2.Trim)
                End With

                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("RSI INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in UpdateRSIHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try

    End Function




#End Region

#Region "RSI Lookup Value"

    Public Function ReadRSILookupDataSetting(ByVal strRSILOOKUPDATAIniPath As String) As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear RSI Lookup Data Structure
            CLSRSILOOKUPDATASTR()

            'Defautl INI File Path - RSILOOKUPDATACFG.INI
            strINIPath = strRSILOOKUPDATAIniPath.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = PRTLOOKUPDATA_SEC
                With udtRSIHookupCFG
                    .DeviceGraphicID = objINIFile.GetStrVal(INI_KEY_DEVICE_GRAPHIC_ID, "")
                    .DeviceName = objINIFile.GetStrVal(INI_KEY_DEVICE_NAME, "")

                    'TRANSACTION STATUS
                    .TxnStatusRSISoundOK = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_RSI_SUCCESS, "")
                    .TxnStatusRSISoundFailed = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_RSI_FAILED, "")

                    'ERROR SEVERITY
                    .ErrSvrtyPrtOk = objINIFile.GetStrVal(INI_KEY_ERR_SEV_OK, "")
                    .ErrSvrtyPrtError = objINIFile.GetStrVal(INI_KEY_ERR_SEV_ERROR, "")
                    .ErrSvrtyPrtWarning = objINIFile.GetStrVal(INI_KEY_ERR_SEV_WARNING, "")
                    .ErrSvrtyPrtFatal = objINIFile.GetStrVal(INI_KEY_ERR_SEV_FATAL, "")

                    'DIAGNOSIS STATUS
                    .MStatus = objINIFile.GetStrVal(INI_KEY_DIAGNOS_MSTATUS, "")
                    .MStatusData = objINIFile.GetStrVal(INI_KEY_DIAGNOS_MSTATUSDATA, "")

                    'SUPPLY STATUS
                    'SOUND
                    .SoundOn = objINIFile.GetStrVal(INI_KEY_SOUND_ON, "")
                    .SoundOff = objINIFile.GetStrVal(INI_KEY_SOUND_OFF, "")

                    'DEVICE STATE
                    .DeviceStateRSIOff = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_RSIOFF, "")
                    .DeviceStateRSIOn = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_RSION, "")

                    'EVENT TYPE
                    .EvtDeviceErr = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_ERROR, "")
                    .EvtDeviceReady = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_READY, "")
                    .EvtDeviceWrap = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_WRAP, "")
                End With
                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("RSI INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadRSILookupDataSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

#End Region


End Module
