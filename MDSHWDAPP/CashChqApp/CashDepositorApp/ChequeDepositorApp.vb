Imports CashDepositorHWD
Imports DoorSensorHWD
Imports ChequeDepositHWD

Public Class ChequeDepositorApp

    Dim WithEvents objChqDepositor As New clsChequeDepositorHWDInterface

#Region "Variable"

    Public strEvtTxtMsg As String = String.Empty
    Public strEvtMsg As String = String.Empty
    Public strErrMsg As String

#End Region


    Private Sub ChequeDepositorApp_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'Exit Cash Deposit
            AppLogInfo("Exit - Cheque Modules")
        Catch ex As Exception
            CashDepositorInfo("Err in ChequeDepositorApp_FormClosed. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub ChequeDepositorApp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("Cheque Modules Loaded")
            InitDisplay()

        Catch ex As Exception
            CashDepositorInfo("Err in ChequeDepositorApp_Load. ErrInfo:" & ex.Message)
        End Try
    End Sub

#Region "Form Function"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            CashDepositorInfo("Err in InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub CashDepositorInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            CashDepositorInfo("Err in InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Form Event Info"

    Private Sub UpdateEvtInfo()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf UpdateEvtInfo))
            Else
                strEvtMsg = strEvtMsg.Trim
                If strEvtMsg.Length > 0 Then
                    EvtEventsMsg(strEvtMsg)
                End If
            End If

        Catch ex As Exception
            MsgBox("Error in Cheque-UpdateEvtInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText &= strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in Cheque-EvtEventsMsg:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


#End Region

#Region "Get Property"

    Private Sub btnGetDevProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqGetDevProp.Click
        Try
            strEvtTxtMsg = ""

            strEvtMsg = "Txn Status:" & objChqDepositor.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objChqDepositor.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objChqDepositor.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objChqDepositor.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objChqDepositor.MStatusData
            UpdateEvtInfo()

        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-btnGetDevProp_Click:" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Cheque Command"



    Private Sub btnStartDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqStartDevice.Click
        Try
            If objChqDepositor.StartDevice Then
                CashDepositorInfo("Cheque-StartDevice Success")
            Else
                CashDepositorInfo("Cheque-StartDevice Fail")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-StartDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnStopDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqStopDevice.Click
        Try
            If objChqDepositor.StopDevice Then
                CashDepositorInfo("Cheque-StopDevice Success")
            Else
                CashDepositorInfo("Cheque-StopDevice Fail")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-StopDevice: " & ex.Message)
        End Try
    End Sub


    Private Sub btnUnlockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqUnlockDeviceNDC.Click
        Try
            If objChqDepositor.UnlockDevice Then
                CashDepositorInfo("Cheque-UnlockDevice Success")
            Else
                CashDepositorInfo("Cheque-UnlockDevice Fail")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-UnlockDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnLockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqLockDeviceNDC.Click
        Try
            If objChqDepositor.LockDevice Then
                CashDepositorInfo("Cheque-LockDevice Success")
            Else
                CashDepositorInfo("Cheque-LockDevice Fail")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-LockDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnWrapDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqWrapDeviceNDC.Click
        Try
            If objChqDepositor.WrapDevice Then
                CashDepositorInfo("Cheque-WrapDevice Success")
            Else
                CashDepositorInfo("Cheque-WrapDevice Fail")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-WrapDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDiagDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqDiagDeviceNDC.Click
        Try
            If objChqDepositor.DiagnosticDevice Then
                CashDepositorInfo("Cheque-Dignotis Success")
            Else
                CashDepositorInfo("Cheque-Dignotis Fail")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-Dignotis: " & ex.Message)
        End Try
    End Sub

    Private Sub btnWakeDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqWakeDeviceNDC.Click
        Dim intState As Integer = 0
        Dim strState As String = String.Empty

        Try

            strState = Mid(cboChqWakeupState.Text, 1, 1).Trim

            If strState = "A" Then
                strState = "10"
            End If

            intState = CInt(strState)

            If objChqDepositor.WakeUpDevice(intState) Then
                CashDepositorInfo("Cheque-WakeUpDevice:" & cboChqWakeupState.Text & " Success")
            Else
                CashDepositorInfo("Cheque-WakeUpDevice:" & cboChqWakeupState.Text & " Fail")
            End If

        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-WakeUpDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChqClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-btnClearText_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-btnClearText_Click:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Events"


    Private Sub objChqDepositor_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objChqDepositor.EvtDeviceDataReady

        Try

            strEvtMsg = "Cheque-Device Data Ready State:" & e.iDeviceState & " DataValue:" & e.mDeviceDataValue & " Trace:" & e.iDeviceTrace
            UpdateEvtInfo()

            Select Case CInt(e.iDeviceState)
                Case 0
                    strEvtMsg = "Current State: Closed"
                Case 1
                    strEvtMsg = "Current State: Open & Prepare"
                Case 2
                    strEvtMsg = "Current State: Insert More"
                Case 3
                    strEvtMsg = "Current State: Rejected Item Detected"
                Case 4
                    strEvtMsg = "Current State: Refunded"
                Case 5
                    strEvtMsg = "Current State: Escrowed"
                Case 6
                    strEvtMsg = "Current State: Processing"
                Case 7
                    strEvtMsg = "Current State: Confirm Transaction"
                Case 8
                    strEvtMsg = "Current State: Trans Complete"
                Case 11
                    strEvtMsg = "Current State: Deposit Item Full"


            End Select

            UpdateEvtInfo()

        Catch ex As Exception
            CashDepositorInfo("Error in objChqDepositor_EvtDeviceDataReady:" & ex.Message)
        End Try
    End Sub

    Private Sub objChqDepositor_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objChqDepositor.EvtDeviceError
        Try

            strEvtMsg = "MDS Ping Status =" & objChqDepositor.MDSPingFailed
            UpdateEvtInfo()

            strEvtMsg = "Cheque-Device Data Error"
            UpdateEvtInfo()
        Catch ex As Exception
            CashDepositorInfo("Error in objChqDepositor_EvtDeviceError:" & ex.Message)
        End Try
    End Sub

    Private Sub objChqDepositor_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objChqDepositor.EvtDeviceTimeout
        Try
            strEvtMsg = "Cheque-Device Data Timeout"
            UpdateEvtInfo()
        Catch ex As Exception
            CashDepositorInfo("Error in objChqDepositor_EvtDeviceTimeout:" & ex.Message)
        End Try
    End Sub


#End Region

#Region "Light Command"

    Private Sub cmdStartMDSCRLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartMDSCRLight.Click
        Dim strChoice As String = "0"

        Try

            strChoice = Mid(cboCRLightChoice.Text, 1, 1).Trim

            If objChqDepositor.StartMDSReaderLight(strChoice) = True Then
                CashDepositorInfo("Cheque-Start MDS Reader Light Success LightOption:" & strChoice)
            Else
                CashDepositorInfo("Cheque-Start MDS Reader Light Failed LightOption:" & strChoice)
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-StartMDSReaderLight: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdStopMDSCRLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopMDSCRLight.Click
        Try
            If objChqDepositor.StopMDSReaderLight Then
                CashDepositorInfo("Cheque-Stop MDS Reader Light Success")
            Else
                CashDepositorInfo("Cheque-Stop MDS Reader Light Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-StopMDSReaderLight: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdStartMDSLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartMDSLight.Click
        Dim strChoice As String = "0"

        Try

            strChoice = Mid(cboLightChoice.Text, 1, 1).Trim

            If objChqDepositor.StartMDSLight(strChoice) Then
                CashDepositorInfo("Cheque-Start MDS Light Success LightOption:" & strChoice)
            Else
                CashDepositorInfo("Cheque-Start MDS Light Failed LightOption:" & strChoice)
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-StartMDSLight: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdStopMDSLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopMDSLight.Click
        Try
            If objChqDepositor.StopMDSLight Then
                CashDepositorInfo("Cheque-Stop MDS Light Success")
            Else
                CashDepositorInfo("Cheque-Stop MDS Light Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cheque-StopMDSLight: " & ex.Message)
        End Try
    End Sub

    
#End Region

 
End Class
