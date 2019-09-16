Imports System
Imports System.IO
Imports FileUtils


Module modReadUPSCFG

#Region "Cls Variable"
    Private strTitle As String = "modReadUPSCFG"
    Private objINIFile As IniFile = Nothing
    Private strErrMsg As String = String.Empty
    Dim strINIPath As String = String.Empty
#End Region


#Region "Ini - File Info Session - UPS"

#Region "UPS SETTING"

    Private Const UPS_HWD_SEC As String = "UPSHWDCFG"
    'Ini File - Log Session Key 
    Private Const UPS_HWD_KEY_UPSENABLE As String = "UPSENABLE"
    Private Const UPS_HWD_KEY_MONITORINGPATH As String = "MONITORINGPATH"
    Private Const UPS_HWD_KEY_CHECKINTERVAL As String = "CHECKINTERVAL"
    Private Const UPS_HWD_KEY_TEXTSPLITTER As String = "MONITORINGSPLITTER"
    Private Const UPS_HWD_KEY_UPSCOMMUNICATIONLOST As String = "UPSCommunicationLost"
    Private Const UPS_HWD_KEY_UPSCOMMUNICATIONESTABLISHED As String = "UPSCommunicationEstablished"
    Private Const UPS_HWD_KEY_UPSSERVICESTART As String = "UPSServiceStart"
    Private Const UPS_HWD_KEY_UPSSERVICESTOP As String = "UPSServiceStop"
    Private Const UPS_HWD_KEY_UPSBATTERYMODE As String = "UPSBatteryMode"
    Private Const UPS_HWD_KEY_UPSPOWERSUPPLYUP As String = "UPSPowerSupplyUp"

#End Region

#Region "UPS LOOKUP DATA"

    Private Const UPSLOOKUPDATA_SEC As String = "UPSLOOKUPDATA"
    'Ini File - Log Session Key 
    Private Const INI_KEY_DEVICE_GRAPHIC_ID As String = "DEVICE_GRAPHIC_ID"
    Private Const INI_KEY_DEVICE_NAME As String = "DEVICE_NAME"
    Private Const INI_KEY_TXN_STATUS_ACPOWER_MODE As String = "TXN_ST_UPS_ACPOWER_MODE"
    Private Const INI_KEY_TXN_STATUS_BATTERY_MODE As String = "TXN_ST_UPS_BATTERY_MODE"
    'Private Const INI_KEY_TXN_STATUS_DEVICE_NOT_CFG As String = "TXN_ST_DEVICE_NOT_CFG"
    'Private Const INI_KEY_TXN_STATUS_CANCEL_SIDEWAY As String = "TXN_ST_CANCEL_SIDEWAY"
    Private Const INI_KEY_ERR_SEV_OK As String = "ERST_UPS_OK"
    Private Const INI_KEY_ERR_SEV_ERROR As String = "ERST_UPS_ERROR"
    Private Const INI_KEY_ERR_SEV_WARNING As String = "ERST_UPS_WARNING"
    Private Const INI_KEY_ERR_SEV_FATAL As String = "ERST_UPS_FATAL"
    Private Const INI_KEY_DIAGNOS_MSTATUS As String = "M_STATUS"
    Private Const INI_KEY_DIAGNOS_MSTATUSDATA As String = "M_STATUS_DATA"
    Private Const INI_KEY_POWER_UP As String = "POWER_UP"
    Private Const INI_KEY_POWER_ONBATTERY As String = "POWER_ONBATTERY"
    Private Const INI_KEY_POWER_DOWN As String = "POWER_DOWN"
    Private Const INI_KEY_DEVICESTATE_POWERSUPPLY As String = "DST_POWERSUPPLYSTATE"
    Private Const INI_KEY_DEVICESTATE_BATTERY As String = "DST_BATTERYSTATE"
    Private Const INI_KEY_EVT_DEVICE_ERROR As String = "EVTTYPE_DEVICEERROR"
    Private Const INI_KEY_EVT_DEVICE_READY As String = "EVTTYPE_DEVICEWRAP"
    Private Const INI_KEY_EVT_DEVICE_WRAP As String = "EVTTYPE_DEVICEREADY"

#End Region

#End Region


#Region "Clear INI Structure Setting"

    Private Sub CLSUPSHWDSTR()
        Try
            'Clear UPS HWD Setting Structure
            With udtUPSHWDCFG
                .blnEnableUPS = False

                .strMonitoringPath = "E:\MultiLink\user\user.log"

                .dblCheckInterval = 5000
                .strTextSplitter = ","

                'UPSServiceStart = 1
                'UPSCommunicationEstablished = 2
                'UPSServiceStop = 3
                'UPSCommunicationLost = 4
                'UPSBatteryMode = 5
                'UPSPowerSupplyUp = 6

                .intUPSServiceStart = 1
                .intUPSCommEstablished = 2
                .intUPSServiceStop = 3
                .intUPSCommLost = 4
                .intUPSBatteryMode = 5
                .intUPSPowerSupplyUp = 6

            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSUPSHWDSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub


    Private Sub CLSUPSLOOKUPDATASTR()
        Try
            'Clear Beeper Lookup Data Setting Structure
            With udtUPSLOOKUPDATACFG
                .DeviceGraphicID = ""
                .DeviceName = ""

                .TxnStatusACPowerMode = ""
                .TxnStatusBatteryMode = ""
                '.TxnStatusDeviceNotCfg = ""
                '.TxnStatusCancelSideway = ""

                .ErrSvrtyUPSOk = ""
                .ErrSvrtyUPSError = ""
                .ErrSvrtyUPSWarning = ""
                .ErrSvrtyUPSFatal = ""

                .MStatus = ""
                .MStatusData = ""

                .PowerUP = ""
                .PowerOnBattery = ""
                .PowerDown = ""

                .DeviceStatePowerSupply = ""
                .DeviceStateBattery = ""
                .EvtDeviceErr = ""
                .EvtDeviceReady = ""
                .EvtDeviceWrap = ""
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSUPSLOOKUPDATASTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region


#Region "Read Ini File"

    Public Function ReadUPSHWDSetting(ByVal strUPSCFGINIPath As String) As Boolean

        Try

            'Clear UPS Structure
            CLSUPSHWDSTR()

            'Defautl INI File Path - UPSHWDSETTING.INI
            strINIPath = strUPSCFGINIPath.Trim

            If (File.Exists(strINIPath)) Then

                objINIFile = New IniFile(strINIPath, False)

                'App Info Ini File
                objINIFile.CurrentSection = UPS_HWD_SEC

                With udtUPSHWDCFG

                    .blnEnableUPS = objINIFile.GetBoolVal(UPS_HWD_KEY_UPSENABLE, True)

                    '"C:\MultiLink\user\user.log"
                    .strMonitoringPath = objINIFile.GetStrVal(UPS_HWD_KEY_MONITORINGPATH, "E:\MultiLink\user\user.log")

                    .dblCheckInterval = objINIFile.GetDblVal(UPS_HWD_KEY_CHECKINTERVAL, 5000)

                    .strTextSplitter = objINIFile.GetStrVal(UPS_HWD_KEY_TEXTSPLITTER, ",")

                    'UPSServiceStart = 1
                    'UPSCommunicationEstablished = 2
                    'UPSServiceStop = 3
                    'UPSCommunicationLost = 4
                    'UPSBatteryMode = 5
                    'UPSPowerSupplyUp = 6

                    .intUPSServiceStart = objINIFile.GetIntVal(UPS_HWD_KEY_UPSSERVICESTART, 1)
                    .intUPSCommEstablished = objINIFile.GetIntVal(UPS_HWD_KEY_UPSCOMMUNICATIONESTABLISHED, 2)
                    .intUPSServiceStop = objINIFile.GetIntVal(UPS_HWD_KEY_UPSSERVICESTOP, 3)
                    .intUPSCommLost = objINIFile.GetIntVal(UPS_HWD_KEY_UPSCOMMUNICATIONLOST, 4)
                    .intUPSBatteryMode = objINIFile.GetIntVal(UPS_HWD_KEY_UPSBATTERYMODE, 5)
                    .intUPSPowerSupplyUp = objINIFile.GetIntVal(UPS_HWD_KEY_UPSPOWERSUPPLYUP, 6)

                End With

                'Log the UPS CFG
                AppLogInfo("== UPS Hardware Setting ==")
                With udtUPSHWDCFG
                    AppLogInfo("Enable UPS=" & .blnEnableUPS)
                    AppLogInfo("UPS Status Path=" & .strMonitoringPath.Trim)
                    AppLogInfo("Monitor Interval=" & .dblCheckInterval)
                    AppLogInfo("Seperator Field=" & .strTextSplitter.Trim)
                    AppLogInfo("UPSServiceStart=" & .intUPSServiceStart)
                    AppLogInfo("UPSCommunicationEstablished=" & .intUPSCommEstablished)
                    AppLogInfo("UPSServiceStop=" & .intUPSServiceStop)
                    AppLogInfo("UPSCommunicationLost=" & .intUPSCommLost)
                    AppLogInfo("UPSBatteryMode=" & .intUPSBatteryMode)
                    AppLogInfo("UPSPowerSupplyUp =" & .intUPSPowerSupplyUp)
                End With
                AppLogInfo("== UPS Hardware Setting End==")

                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("UPS INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadUPSHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function


    Public Function UpdateUPSHWDSetting(ByVal strUPSEnable As String, ByVal strUPSStatusPath As String, ByVal strUPSCFGINIPath As String) As Boolean
        Try

            'Defautl INI File Path - UPSHWDSETTING.INI
            strINIPath = strUPSCFGINIPath.Trim

            If (File.Exists(strINIPath)) Then

                objINIFile = New IniFile(strINIPath, False)

                With objINIFile
                    .CurrentSection = UPS_HWD_SEC
                    .SetVal(UPS_HWD_KEY_UPSENABLE, strUPSEnable.Trim)
                    .SetVal(UPS_HWD_KEY_MONITORINGPATH, strUPSStatusPath.Trim)
                End With

                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("UPS INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in UpdateUPSHWDSetting.ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try

    End Function




#End Region

#Region "UPS Lookup Value"

    Public Function ReadUPSLookupDataSetting(ByVal strUPSCFGINIPath1 As String) As Boolean

        Try

            'Clear UPS Lookup Data Structure
            CLSUPSLOOKUPDATASTR()

            'Defautl INI File Path - UPSLOOKUPDATACFG.INI
            strINIPath = strUPSCFGINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = UPSLOOKUPDATA_SEC
                With udtUPSLOOKUPDATACFG
                    .DeviceGraphicID = objINIFile.GetStrVal(INI_KEY_DEVICE_GRAPHIC_ID, "")
                    .DeviceName = objINIFile.GetStrVal(INI_KEY_DEVICE_NAME, "")

                    'TRANSACTION STATUS
                    .TxnStatusACPowerMode = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_ACPOWER_MODE, "")
                    .TxnStatusBatteryMode = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_BATTERY_MODE, "")
                    '.TxnStatusDeviceNotCfg = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_DEVICE_NOT_CFG, "")
                    '.TxnStatusCancelSideway = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_CANCEL_SIDEWAY, "")

                    'ERROR SEVERITY
                    .ErrSvrtyUPSOk = objINIFile.GetStrVal(INI_KEY_ERR_SEV_OK, "")
                    .ErrSvrtyUPSError = objINIFile.GetStrVal(INI_KEY_ERR_SEV_ERROR, "")
                    .ErrSvrtyUPSWarning = objINIFile.GetStrVal(INI_KEY_ERR_SEV_WARNING, "")
                    .ErrSvrtyUPSFatal = objINIFile.GetStrVal(INI_KEY_ERR_SEV_FATAL, "")

                    'DIAGNOSIS STATUS
                    .MStatus = objINIFile.GetStrVal(INI_KEY_DIAGNOS_MSTATUS, "")
                    .MStatusData = objINIFile.GetStrVal(INI_KEY_DIAGNOS_MSTATUSDATA, "")

                    'DEVICE STATE
                    .DeviceStatePowerSupply = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_POWERSUPPLY, "")
                    .DeviceStateBattery = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_BATTERY, "")

                    'EVENT TYPE
                    .EvtDeviceErr = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_ERROR, "")
                    .EvtDeviceReady = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_READY, "")
                    .EvtDeviceWrap = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_WRAP, "")
                End With
                Return True
            Else
                'Ini File Path Not Found
                AppLogWarn("UPS INI File:" & strINIPath & " Not Found")
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in ReadUPSLookupDataSetting. ErrInfo:" & ex.Message
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
