Public Class clsGolbalVar


#Region "Events Structure"

    Structure DeviceSender
        Dim strDeviceGraphicId As String
        Dim strDeviceName As String
    End Structure

    Structure EventDeviceArgs
        Dim iDeviceState As String
        Dim iDeviceTrace As String
        Dim strEventType As String
        Dim mDeviceDataValue As Object
    End Structure

    Structure device_error
        Dim Status As Integer
        Dim TxnStatus As String
        Dim ErrorSeverity As String
        Dim SupplyStatus As String
        Dim MStatus As String
        Dim MStatusData As String
    End Structure

#End Region


End Class
