Imports DoorSensorHWD

Public Class DoorSensorApp

    Dim WithEvents objDoorSensor As New clsDoorInterface


#Region "Variable"

    Public strEvtTxtMsg As String = String.Empty
    Public strEvtMsg As String = String.Empty
    Public strErrMsg As String = String.Empty

    Public blnStartDoorSensor As Boolean = False

    Public strDoorStatus As String = String.Empty

#End Region


    Private Sub DoorSensorApp_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStartDoorSensor = True Then
                If objDoorSensor.LockDevice Then
                    AppLogInfo("Door-LockDevice Success")
                Else
                    AppLogWarn("Door-LockDevice Fail")
                End If
            End If

            'Exit Cash Deposit
            AppLogInfo("Exit - Door Modules")
        Catch ex As Exception
            CashDepositorInfo("Err in DoorSensorApp_FormClosed. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub DoorSensorApp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("Cheque Door Loaded")
            InitDisplay()
        Catch ex As Exception
            CashDepositorInfo("Err in DoorSensorApp_Load. ErrInfo:" & ex.Message)
        End Try
    End Sub


#Region "Form Function"

    Private Sub InitDisplay()
        Try
            strDoorStatus = ""
            txtDeviceProperty.Text = ""
            lblDoorStatus.Text = ""
        Catch ex As Exception
            CashDepositorInfo("Err in DoorSensor-InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Form Events"

    Private Sub CashDepositorInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            CashDepositorInfo("Err in DoorSensor-CashDepositorInfo. ErrInfo:" & ex.Message)
        End Try
    End Sub

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
            MsgBox("Error in DoorSensor-UpdateEvtInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            lblDoorStatus.Text = strDoorStatus.Trim

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in DoorSensor-EvtEventsMsg:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


#End Region


#Region "Events"

    Private Sub objDoorSensor_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objDoorSensor.EvtDeviceDataReady
        Try
            strEvtMsg = "Door-EvtDeviceDataReady:" & e.iDeviceState
            UpdateEvtInfo()

            strDoorStatus = "Door Close"
            strEvtMsg = "Door Close"
            UpdateEvtInfo()

            'CashDepositorInfo("Door-DiagDevice DoorStatus:" & objDoorSensor.MDSDoorStatus)
        Catch ex As Exception
            CashDepositorInfo("Err in objDoorSensor_EvtDeviceDataReady. ErrInfo:" & ex.Message)
        End Try
    End Sub


    Private Sub objDoorSensor_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objDoorSensor.EvtDeviceError
        Try
            'strEvtMsg = "Door-EvtDeviceError Detected:" & e.iDeviceState
            'UpdateEvtInfo()

            Select Case e.iDeviceState

                Case 0
                    strDoorStatus = "Door Close"
                    strEvtMsg = "Door Close"
                    UpdateEvtInfo()

                Case 1
                    strDoorStatus = "Door Close"
                    strEvtMsg = "Door Close"
                    UpdateEvtInfo()

                Case 2
                    strDoorStatus = "Door Open - Maintenance Mode"
                    strEvtMsg = "Door Open - Maintenance Mode"
                    UpdateEvtInfo()

                Case 3

                    strDoorStatus = "Door Open - Supervisor Mode"
                    strEvtMsg = "Door Open - Supervisor Mode"
                    UpdateEvtInfo()

                Case Else
                    strDoorStatus = "Door Open - Unknown State"
                    strEvtMsg = "Door Open - Unknown State"
                    UpdateEvtInfo()

            End Select

        Catch ex As Exception
            CashDepositorInfo("Error in objDoorSensor_EvtDeviceError:" & ex.Message)
        End Try
    End Sub

    'Private Sub objDoorSensor_EvtDeviceMDSDoorClose() Handles objDoorSensor.EvtDeviceMDSDoorClose
    '    Try
    '        strDoorStatus = "Door Close"
    '        strEvtMsg = "Door Close"
    '        UpdateEvtInfo()
    '    Catch ex As Exception
    '        CashDepositorInfo("Error in objDoorSensor_EvtDeviceMDSDoorClose:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub objDoorSensor_EvtDeviceMDSDoorOpen() Handles objDoorSensor.EvtDeviceMDSDoorOpen
    '    Try
    '        strDoorStatus = "Door Open"
    '        strEvtMsg = "Door Open"
    '        UpdateEvtInfo()
    '    Catch ex As Exception
    '        CashDepositorInfo("Error in objDoorSensor_EvtDeviceMDSDoorOpen:" & ex.Message)
    '    End Try
    'End Sub

    Private Sub objDoorSensor_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objDoorSensor.EvtDeviceTimeout
        Try
            strEvtMsg = "Door-Device Data Timeout"
            UpdateEvtInfo()
        Catch ex As Exception
            CashDepositorInfo("Error in objDoorSensor_EvtDeviceTimeout:" & ex.Message)
        End Try
    End Sub


#End Region

#Region "Door Command"

    Private Sub btnDoorStartDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoorStartDevice.Click
        Try
            If objDoorSensor.StartDevice Then
                CashDepositorInfo("Door-StartDevice Success")
            Else
                CashDepositorInfo("Door-StartDevice Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnDoorStartDevice_Click: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDoorStopDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoorStopDevice.Click
        Try
            If objDoorSensor.StopDevice Then
                CashDepositorInfo("Door-StopDevice Success")
            Else
                CashDepositorInfo("Door-StopDevice Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnDoorStopDevice_Click: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDoorLockDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoorLockDevice.Click
        Try

            If blnStartDoorSensor = True Then
                If objDoorSensor.LockDevice = True Then
                    CashDepositorInfo("Door-LockDevice Success")
                Else
                    CashDepositorInfo("Door-LockDevice Failed")
                End If
                blnStartDoorSensor = False
            Else
                CashDepositorInfo("Door-LockDevice Failed")
                blnStartDoorSensor = False
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnDoorLockDevice_Click: " & ex.Message)
            blnStartDoorSensor = False
        End Try
    End Sub

    Private Sub btnDoorUnlockDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoorUnlockDevice.Click
        Try
            If objDoorSensor.UnlockDevice Then
                CashDepositorInfo("Door-UnlockDevice Success")
                blnStartDoorSensor = True
            Else
                CashDepositorInfo("Door-UnlockDevice Failed")
                blnStartDoorSensor = False
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnDoorUnlockDevice_Click: " & ex.Message)
            blnStartDoorSensor = False
        End Try
    End Sub


    Private Sub btnDiagDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagDeviceNDC.Click
        Try

            If objDoorSensor.DiagnosticDevice = True Then
                CashDepositorInfo("Door-DiagnosticDevice Success")
            Else
                CashDepositorInfo("Door-DiagnosticDevice Failed")
            End If

        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnDiagDeviceNDC_Click: " & ex.Message)
        End Try
    End Sub


    Private Sub cmdWakeUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWakeUp.Click
        Try
            If objDoorSensor.WakeUpDevice(1) Then
                CashDepositorInfo("Door-Wakeup Success")
                blnStartDoorSensor = True
            Else
                CashDepositorInfo("Door-Wakeup Fail")
                blnStartDoorSensor = False
            End If
        Catch ex As Exception
            CashDepositorInfo("Error Door-cmdWakeUp_Click: " & ex.Message)
            blnStartDoorSensor = False
        End Try
    End Sub

    Private Sub btnGetDevProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDevProp.Click
        Try

            strEvtTxtMsg = ""

            strEvtMsg = "Txn Status:" & objDoorSensor.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objDoorSensor.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objDoorSensor.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objDoorSensor.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objDoorSensor.MStatusdata
            UpdateEvtInfo()


        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnGetDevProp_Click: " & ex.Message)
        End Try
    End Sub

    Private Sub btnWrapDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWrapDeviceNDC.Click
        Try
            If objDoorSensor.WrapDevice Then
                CashDepositorInfo("Door-WrapDevice Success")
            Else
                CashDepositorInfo("Door-WrapDevice Fail")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnWrapDeviceNDC_Click: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnClearText_Click: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            CashDepositorInfo("Error in Door-btnClearText_Click: " & ex.Message)
        End Try
    End Sub
  
#End Region


  
    
 

End Class