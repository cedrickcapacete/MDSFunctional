Imports System
Imports System.IO
Imports FileUtils

Module modReadBeeperCFG

#Region "Cls Variable"
    'Private strTitle As String = "modReadBeeperCFG"
    Private objINIFile As IniFile = Nothing
    Private strErrMsg As String = String.Empty
    Dim strINIPath As String = String.Empty
#End Region


#Region "Ini - File Info Session - Beeper"

#Region "Beeper SETTING"

    Private Const BEEPER_HWD_SEC As String = "BEEPERHWDCFG"
    'Ini File - Log Session Key 
    Private Const BEEPER_HWD_KEY_BEEPERENABLE As String = "BEEPERENABLE"
    Private Const BEEPER_HWD_KEY_COMPORT As String = "ComportNum"
    Private Const BEEPER_HWD_KEY_BAUDRATE As String = "Baudrate"
    Private Const BEEPER_HWD_KEY_PARITY As String = "Parity"
    Private Const BEEPER_HWD_KEY_DATABITS As String = "Databits"
    Private Const BEEPER_HWD_KEY_STOPBITS As String = "Stopbits"


#End Region

#Region "Beeper LOOKUP DATA"

    Private Const PRTLOOKUPDATA_SEC As String = "BEEPERLOOKUPDATA"
    'Ini File - Log Session Key 
    Private Const INI_KEY_DEVICE_GRAPHIC_ID As String = "DEVICE_GRAPHIC_ID"
    Private Const INI_KEY_DEVICE_NAME As String = "DEVICE_NAME"
    Private Const INI_KEY_TXN_STATUS_BEEPER_SUCCESS As String = "TXN_ST_BEEPER_SUCCESS"
    Private Const INI_KEY_TXN_STATUS_BEEPER_FAILED As String = "TXN_ST_BEEPER_FAILED"
    Private Const INI_KEY_ERR_SEV_OK As String = "ERST_BEEPER_OK"
    Private Const INI_KEY_ERR_SEV_ERROR As String = "ERST_BEEPER_ERROR"
    Private Const INI_KEY_ERR_SEV_WARNING As String = "ERST_BEEPER_WARNING"
    Private Const INI_KEY_ERR_SEV_FATAL As String = "ERST_BEEPER_FATAL"
    Private Const INI_KEY_DIAGNOS_MSTATUS As String = "M_STATUS"
    Private Const INI_KEY_DIAGNOS_MSTATUSDATA As String = "M_STATUS_DATA"
    Private Const INI_KEY_SOUND_ON As String = "SOUND_ON"
    Private Const INI_KEY_SOUND_OFF As String = "SOUND_OFF"
    Private Const INI_KEY_DEVICESTATE_BEEPERON As String = "DST_BEEPERONSTATE"
    Private Const INI_KEY_DEVICESTATE_BEEPEROFF As String = "DST_BEEPEROFFSTATE"
    Private Const INI_KEY_EVT_DEVICE_ERROR As String = "EVTTYPE_DEVICEERROR"
    Private Const INI_KEY_EVT_DEVICE_READY As String = "EVTTYPE_DEVICEWRAP"
    Private Const INI_KEY_EVT_DEVICE_WRAP As String = "EVTTYPE_DEVICEREADY"

#End Region

#End Region


#Region "Clear INI Structure Setting"

    Private Sub CLSBEEPERHWDSTR()
        Try
            'Clear CUSTOM Printer HWD Setting Structure
            With udtBeeperHwdCFG

                .blnEnableBeeper = False
                .strComport = "4"
                .strBaudrate = "9600"
                .strParity = "0"
                .strDatabits = "8"
                .strStopbits = "1"
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSBEEPERHWDSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub


    Private Sub CLSBEEPERLOOKUPDATASTR()
        Try
            'Clear Beeper Lookup Data Setting Structure
            With udtBeeperHookupCFG
                .DeviceGraphicID = "Z"
                .DeviceName = "BEEPER"

                .TxnStatusBeeperSoundOK = "0"
                .TxnStatusBeeperSoundFailed = "1"

                .ErrSvrtyPrtOk = "1"
                .ErrSvrtyPrtError = "2"
                .ErrSvrtyPrtWarning = "3"
                .ErrSvrtyPrtFatal = "4"

                .MStatus = ""
                .MStatusData = ""

                .BeeperSuplyStatus = "1"
                .SoundOn = "2"
                .SoundOff = "3"

                .DeviceStateBeeperOff = "0"
                .DeviceStateBeeperOn = "1"
                .EvtDeviceErr = ""
                .EvtDeviceReady = ""
                .EvtDeviceWrap = ""
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSBEEPERLOOKUPDATASTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region


#Region "Read Ini File"

    Public Function ReadBeeperHWDSetting(ByVal strBEEPERHWDIniPath As String) As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear Beeper Structure
            CLSBEEPERHWDSTR()

            'Defautl INI File Path - BEEPERHWDSETTING.INI.INI
            strINIPath = strBEEPERHWDIniPath.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = BEEPER_HWD_SEC
                With udtBeeperHwdCFG
                    .blnEnableBeeper = objINIFile.GetBoolVal(BEEPER_HWD_KEY_BEEPERENABLE, False)
                    .strComport = objINIFile.GetStrVal(BEEPER_HWD_KEY_COMPORT, "4")
                    .strBaudrate = objINIFile.GetStrVal(BEEPER_HWD_KEY_BAUDRATE, "9600")
                    .strParity = objINIFile.GetStrVal(BEEPER_HWD_KEY_PARITY, "0")
                    .strDatabits = objINIFile.GetStrVal(BEEPER_HWD_KEY_DATABITS, "8")
                    .strStopbits = objINIFile.GetStrVal(BEEPER_HWD_KEY_STOPBITS, "1")
                End With

                'Log the Beeper CFG
                AppLogInfo("== Beeper Hardware Setting ==")
                With udtBeeperHwdCFG
                    AppLogInfo("Enable Beeper=" & .blnEnableBeeper)
                    AppLogInfo("Comport=" & .strComport.Trim)
                    AppLogInfo("Baudrate=" & .strBaudrate.Trim)
                    AppLogInfo("Parity=" & .strParity.Trim)
                    AppLogInfo("Databits=" & .strDatabits.Trim)
                    AppLogInfo("Stopbits=" & .strStopbits.Trim)
                End With
                AppLogInfo("== Beeper Hardware Setting End==")

                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("Beeper INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadBeeperHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function


    Public Function UpdateBeeperHWDSetting(ByVal strBeeperEnable As String, ByVal strComport As String, ByVal strBEEPERHWDIniPath As String) As Boolean
        Try

            'Defautl INI File Path - BEEPERHWDSETTING.INI.INI
            strINIPath = strBEEPERHWDIniPath.Trim

            If (File.Exists(strINIPath)) Then

                objINIFile = New IniFile(strINIPath, False)

                With objINIFile
                    .CurrentSection = BEEPER_HWD_SEC
                    .SetVal(BEEPER_HWD_KEY_BEEPERENABLE, strBeeperEnable.Trim)
                    .SetVal(BEEPER_HWD_KEY_COMPORT, strComport.Trim)
                End With

                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("Beeper INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in UpdateBeeperHWDSetting.ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try

    End Function

#End Region

#Region "Beeper Lookup Value"

    Public Function ReadBeeperLookupDataSetting(ByVal strBEEPERLOOKUPDATAIniPath As String) As Boolean
        Dim strAppPath As String = ""
        Dim strAppName As String = ""

        Try

            'Clear Beeper Lookup Data Structure
            CLSBEEPERLOOKUPDATASTR()

            'Defautl INI File Path - BEEPERLOOKUPDATACFG.INI
            strINIPath = strBEEPERLOOKUPDATAIniPath.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = PRTLOOKUPDATA_SEC
                With udtBeeperHookupCFG
                    .DeviceGraphicID = objINIFile.GetStrVal(INI_KEY_DEVICE_GRAPHIC_ID, "")
                    .DeviceName = objINIFile.GetStrVal(INI_KEY_DEVICE_NAME, "")

                    'TRANSACTION STATUS
                    .TxnStatusBeeperSoundOK = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_BEEPER_SUCCESS, "")
                    .TxnStatusBeeperSoundFailed = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_BEEPER_FAILED, "")

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
                    .DeviceStateBeeperOff = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_BEEPEROFF, "")
                    .DeviceStateBeeperOn = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_BEEPERON, "")

                    'EVENT TYPE
                    .EvtDeviceErr = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_ERROR, "")
                    .EvtDeviceReady = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_READY, "")
                    .EvtDeviceWrap = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_WRAP, "")
                End With
                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("Beeper INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadBeeperLookupDataSetting. ErrInfo:" & ex.Message
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
