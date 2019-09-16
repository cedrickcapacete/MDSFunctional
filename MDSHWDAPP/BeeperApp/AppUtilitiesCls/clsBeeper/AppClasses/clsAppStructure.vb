Public Class clsAppStructure


#Region "Beeper Structure"

    Structure BeeperHWDSTR
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure

    Structure BEEPERSETTINGSTR
        Dim blnEnableBeeper As Boolean
        Dim strComport As String
        Dim strBaudrate As String
        Dim strParity As String
        Dim strDatabits As String
        Dim strStopbits As String
    End Structure

    Structure BEEPERLOOKUPDATASTR
        Dim DeviceGraphicID As String
        Dim DeviceName As String

        'Device Properties
        Dim TxnStatusBeeperSoundOK As String
        Dim TxnStatusBeeperSoundFailed As String

        'Error Severity
        Dim ErrSvrtyPrtOk As String
        Dim ErrSvrtyPrtError As String
        Dim ErrSvrtyPrtFatal As String
        Dim ErrSvrtyPrtWarning As String

        'Diagnos Status
        Dim MStatus As String
        Dim MStatusData As String

        'Supply Status
        Dim BeeperSuplyStatus As String
        Dim SoundOn As String
        Dim SoundOff As String

        'iDeviceState
        Dim DeviceStateBeeperOn As String
        Dim DeviceStateBeeperOff As String

        'Event Type
        Dim EvtDeviceErr As String
        Dim EvtDeviceReady As String
        Dim EvtDeviceWrap As String
    End Structure

#End Region


End Class
