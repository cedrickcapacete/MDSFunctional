Public Class clsAppStructure

    Structure RSIHWDSTR
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure


#Region "RSI Structure"

    Structure RSISETTINGSTR

        Dim blnEnableRSI As Boolean

        Dim strNumofRSI As String

        'Comport 1
        Dim strC1Comport As String
        Dim strC1Baudrate As String
        Dim strC1Parity As String
        Dim strC1Databits As String
        Dim strC1Stopbits As String

        'Comport 2
        Dim strC2Comport As String
        Dim strC2Baudrate As String
        Dim strC2Parity As String
        Dim strC2Databits As String
        Dim strC2Stopbits As String

    End Structure

    Structure RSILOOKUPDATASTR
        Dim DeviceGraphicID As String
        Dim DeviceName As String

        'Device Properties
        Dim TxnStatusRSISoundOK As String
        Dim TxnStatusRSISoundFailed As String

        'Error Severity
        Dim ErrSvrtyPrtOk As String
        Dim ErrSvrtyPrtError As String
        Dim ErrSvrtyPrtFatal As String
        Dim ErrSvrtyPrtWarning As String

        'Diagnos Status
        Dim MStatus As String
        Dim MStatusData As String

        'Supply Status
        Dim RSISuplyStatus As String
        Dim SoundOn As String
        Dim SoundOff As String

        'iDeviceState
        Dim DeviceStateRSIOn As String
        Dim DeviceStateRSIOff As String

        'Event Type
        Dim EvtDeviceErr As String
        Dim EvtDeviceReady As String
        Dim EvtDeviceWrap As String
    End Structure

#End Region

End Class
