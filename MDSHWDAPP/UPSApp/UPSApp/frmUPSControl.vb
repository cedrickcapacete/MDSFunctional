Imports System
Imports ClsUPSHWDInterface
Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmUPSControl

#Region "Variable"

    Dim strTitle As String = "frmUPSControl"
    Dim blnStartDevice As Boolean = False

#End Region

#Region "UPS Object"


    Public WithEvents objUPSInterface As ClsUPSHWDInterface.clsHardwareLayer

#End Region

    Private Sub frmUPSControl_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStartDevice = True Then
                objUPSInterface.StopDevice()
            End If

            objUPSInterface = Nothing

            AppLogInfo("== End UPS System Mode ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmUPSControl_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmUPSControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start UPS System Mode ==")

            'Init the Object - UPS
            objUPSInterface = New ClsUPSHWDInterface.clsHardwareLayer

            blnStartDevice = False

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmUPSControl_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "Function"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""
            'btnUPSSetting.Enabled =False  
        Catch ex As Exception
            UPSInfo("Err in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Events Display"

    Public Sub UPSInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            MsgBox("Err in " & strTitle & ".UPSInfo. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Public Sub UpdateEvtInfo()
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
            MsgBox("Error in " & strTitle & ".UpdateEvtInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in " & strTitle & ".EvtEventsMsg:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


#End Region


#Region "UPS Command"

    Private Sub btnUPSSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUPSSetting.Click
        Try
            'UPS Setting
            frmUPSSetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnUPSSetting_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub cmdInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInit.Click
        Try

            blnStartDevice = True

            If objUPSInterface.StartDevice() = True Then
                UPSInfo("StartDevice - Init UPS Successfully")
                'btnUPSSetting.Enabled =True  
            Else
                UPSInfo("StartDevice - Init UPS Failed")
                'btnUPSSetting.Enabled =False  
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdInit_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try

            If objUPSInterface.StopDevice() = True Then
                UPSInfo("StopDevice - UPS Successfully")
            Else
                UPSInfo("StopDevice - UPS Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdClose_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdUnlockDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnlockDevice.Click
        Try

            If objUPSInterface.UnlockDevice() = True Then
                UPSInfo("UnlockDevice - UPS Successfully")

            Else
                UPSInfo("UnlockDevice - UPS Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdUnlockDevice_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdLockDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLockDevice.Click
        Try

            If objUPSInterface.LockDevice() = True Then
                UPSInfo("LockDevice - UPS Successfully")
            Else
                UPSInfo("LockDevice - UPS Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdLockDevice_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdWakeupDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWakeupDevice.Click
        Try
            If objUPSInterface.WakeUpDevice(0) Then

                UPSInfo("WakeupDevice - UPS Successfully")

            Else
                UPSInfo("WakeupDevice - UPS Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdWakeupDevice_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub cmdWrapDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrapDevice.Click
        Try

            If objUPSInterface.WrapDevice() = True Then
                UPSInfo("WrapDevice - UPS Successfully")

            Else
                UPSInfo("WrapDevice - UPS Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdWrapDevice_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdDiagnosticDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDiagnosticDevice.Click
        Try

            If objUPSInterface.DiagnosticDevice() = True Then
                UPSInfo("DiagnosticDevice - UPS Successfully")
            Else
                UPSInfo("DiagnosticDevice - UPS Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdDiagnosticDevice_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdGetDeviceProperty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetDeviceProperty.Click
        Try
            strEvtTxtMsg = ""

            strEvtMsg = "Txn Status:" & objUPSInterface.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objUPSInterface.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objUPSInterface.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objUPSInterface.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objUPSInterface.MStatusData
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdGetDeviceProperty_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdClearText_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


#Region "UPS Events"


    Private Sub objUPSInterface_EvtCheckUPSBatteryMode(ByVal blnFlag As Boolean) Handles objUPSInterface.EvtCheckUPSBatteryMode
        Try
            UPSInfo("UPS Check UPS Battery Mode Events")
            If blnFlag = True Then
                strEvtMsg = "UPS running on battery mode"
            Else
                strEvtMsg = "UPS running on AC Power mode"
            End If
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objUPSInterface_EvtCheckUPSBatteryMode:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objUPSInterface_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objUPSInterface.EvtDeviceDataReady
        Try
            UPSInfo("UPS Device Data Ready Events")
            AppLogInfo("UPS Status Code = " & objUPSInterface.UPSMonitorStatusCode)
            AppLogInfo("UPS Details = " & objUPSInterface.UPSMonitorStatusDetails)
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objUPSInterface_EvtDeviceDataReady:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objUPSInterface_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objUPSInterface.EvtDeviceError
        Try
            UPSInfo("UPS Device Error Events")
            AppLogInfo("UPS Status Code = " & objUPSInterface.UPSMonitorStatusCode)
            AppLogInfo("UPS Details = " & objUPSInterface.UPSMonitorStatusDetails)
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objUPSInterface_EvtDeviceError:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objUPSInterface_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objUPSInterface.EvtDeviceTimeout
        Try
            UPSInfo("UPS Device Timeout Events")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objUPSInterface_EvtDeviceTimeout:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class
