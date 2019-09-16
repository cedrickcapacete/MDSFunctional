Public Class frmMaintenanceMode

#Region "Variable"

    Dim strTitle As String = "frmMaintenanceMode"

    Dim blnStartDevice As Boolean = False

#End Region

#Region "UPS Object"

    Public WithEvents objUPSInterface As ClsUPSHWDInterface.clsHardwareLayer

#End Region

    Private Sub frmMaintenanceMode_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStartDevice = True Then
                objUPSInterface.StopDevice()
            End If

            objUPSInterface = Nothing

            AppLogInfo("== End UPS Diagnostic Mode ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMaintenanceMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start UPS Diagnostic Mode ==")

            'Init the Object - UPS
            objUPSInterface = New ClsUPSHWDInterface.clsHardwareLayer

            blnStartDevice = False

            'Init Display
            InitDisplay()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "InitDisplay"

    Private Sub InitDisplay()
        Try
            RBACPower.Checked = True
            lblUPSStatus.Text = "-"
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "User Command"


    Private Sub btnUPSSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUPSSetting.Click
        Try
            'UPS Setting
            frmUPSSetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnUPSSetting_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload
            AppLogInfo("UPS Diagnostic Menu - Exit")
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInit.Click
        Try
            If objUPSInterface.StartDevice() = True Then
                blnStartDevice = True
                strLogMsg = "StartDevice - Init UPS Successfully"
                AppLogInfo(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
            Else
                blnStartDevice = False
                strLogMsg = "StartDevice - Init UPS Failed"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdInit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try

            If blnStartDevice = True Then
                blnStartDevice = False
                If objUPSInterface.StopDevice() = True Then
                    strLogMsg = "StopDevice - UPS Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "StopDevice - UPS Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "UPS is not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdClose_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdWrapDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrapDevice.Click
        Try
            If blnStartDevice = True Then
                If objUPSInterface.WrapDevice() = True Then
                    strLogMsg = "WrapDevice - UPS Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "WrapDevice - UPS Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "UPS is not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdWrapDevice_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdWakeupDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWakeupDevice.Click
        Try
            If blnStartDevice = True Then
                If objUPSInterface.WakeUpDevice(0) Then
                    strLogMsg = "WakeupDevice - UPS Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "WakeupDevice - UPS Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "UPS is not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdWakeupDevice_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "UPS Events"


    Private Sub objUPSInterface_EvtCheckUPSBatteryMode(ByVal blnFlag As Boolean) Handles objUPSInterface.EvtCheckUPSBatteryMode
        Try
            'Diagnotisc Control
            AppLogInfo("UPS Check UPS Battery Mode Events. Flag:" & blnFlag)
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objUPSInterface_EvtCheckUPSBatteryMode:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objUPSInterface_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objUPSInterface.EvtDeviceDataReady
        Try
            AppLogInfo("UPS Device Data Ready Events")
            AppLogInfo("UPS Status Code = " & objUPSInterface.UPSMonitorStatusCode)
            AppLogInfo("UPS Details = " & objUPSInterface.UPSMonitorStatusDetails)

            lblUPSStatus.Text = objUPSInterface.UPSMonitorStatusDetails
            RBACPower.Checked = True

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objUPSInterface_EvtDeviceDataReady:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objUPSInterface_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objUPSInterface.EvtDeviceError
        Try
          
            AppLogInfo("UPS Device Error Events")
            AppLogInfo("UPS Status Code = " & objUPSInterface.UPSMonitorStatusCode)
            AppLogInfo("UPS Details = " & objUPSInterface.UPSMonitorStatusDetails)

            lblUPSStatus.Text = objUPSInterface.UPSMonitorStatusDetails
            RBBatery.Checked = True
            
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objUPSInterface_EvtDeviceError:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objUPSInterface_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objUPSInterface.EvtDeviceTimeout
        Try
            AppLogInfo("UPS Device Timeout Events")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objUPSInterface_EvtDeviceTimeout:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


End Class