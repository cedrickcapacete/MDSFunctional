Public Class clsAppStructure


    Structure UPSHWDSTR
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure


    Structure UPSSETTINGSTR
        Dim blnEnableUPS As Boolean
        Dim strMonitoringPath As String
        Dim dblCheckInterval As Double
        Dim strTextSplitter As String
        Dim intUPSCommLost As Integer
        Dim intUPSCommEstablished As Integer
        Dim intUPSServiceStart As Integer
        Dim intUPSServiceStop As Integer
        Dim intUPSBatteryMode As Integer
        Dim intUPSPowerSupplyUp As Integer
    End Structure

    Structure UPSLOOKUPDATASTR
        Dim DeviceGraphicID As String
        Dim DeviceName As String

        'Device Properties
        Dim TxnStatusACPowerMode As String
        Dim TxnStatusBatteryMode As String
        'Dim TxnStatusDeviceNotCfg As String
        'Dim TxnStatusCancelSideway As String

        'Error Severity
        Dim ErrSvrtyUPSOk As String
        Dim ErrSvrtyUPSError As String
        Dim ErrSvrtyUPSFatal As String
        Dim ErrSvrtyUPSWarning As String

        'Supply Status
        'Power mode
        Dim PowerUP As String
        Dim PowerOnBattery As String
        Dim PowerDown As String

        'Diagnos Status
        Dim MStatus As String
        Dim MStatusData As String

        'iDeviceState
        Dim DeviceStatePowerSupply As String
        Dim DeviceStateBattery As String

        'Event Type
        Dim EvtDeviceErr As String
        Dim EvtDeviceReady As String
        Dim EvtDeviceWrap As String
    End Structure




End Class
