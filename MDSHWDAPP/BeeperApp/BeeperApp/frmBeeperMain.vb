Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD


Public Class frmBeeperMain

#Region "Form - Variable"

    Dim strTitle As String = "frmBeeperMain"

    Dim strEvtTxtMsg As String = ""
    Dim strEvtMsg As String = ""
    Dim strErrMsg As String = ""

    Dim blnStartDevice As Boolean = False

#End Region


#Region "Beeper Object"

    Public WithEvents objBeeper As clsBeeperHWD.clsBeeperControl

#End Region


    Private Sub frmBeeperMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try 
            If blnStartDevice = True Then
                objBeeper.StopDevice()
            End If

            objBeeper = Nothing

            AppLogInfo("== End Beeper System Mode ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmBeeperMain_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmBeeperMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("== Start Beeper System Mode ==")

            'Init the Object - Beeper
            objBeeper = New clsBeeperHWD.clsBeeperControl

            blnStartDevice = False

            'Init Display
            InitDisplay()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmBeeperMain_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "InitDisplay"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""
            'btnBeeperSetting.Enabled = False
        Catch ex As Exception
            BeeperInfo("Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Events Display"

    Private Sub BeeperInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            BeeperInfo("Error in " & strTitle & ".BeeperInfo. ErrInfo:" & ex.Message)
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
            MsgBox("Error in " & strTitle & ".UpdateEvtInfo. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in " & strTitle & ".EvtEventsMsg. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


#End Region

#Region "Beeper Method - StartDevice, StopDevice"

    Private Sub btnBeeperSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBeeperSetting.Click
        Try
            'Beeper Setting
            frmBeeperSetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnBeeperSetting_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload Me
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub btnStartDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartDeviceNDC.Click
        Try

            blnStartDevice = True

            If objBeeper.StartDevice() = True Then
                BeeperInfo("StartDevice - Init Beeper Successfully")
                'btnBeeperSetting.Enabled = True
            Else
                BeeperInfo("StartDevice - Init Beeper Failed")
                'btnBeeperSetting.Enabled = False
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnStartDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnStopDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopDeviceNDC.Click
        Try

            If objBeeper.StopDevice() = True Then
                BeeperInfo("StopDevice - Beeper Successfully")
            Else
                BeeperInfo("StopDevice - Beeper Failed")
             End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnStopDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnWrapDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWrapDeviceNDC.Click
        Try

            If objBeeper.WrapDevice = True Then
                BeeperInfo("WrapDevice - Beeper Successfully")
            Else
                BeeperInfo("WrapDevice - Beeper Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnWrapDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnLockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLockDeviceNDC.Click
        Try

            If objBeeper.LockDevice() = True Then
                BeeperInfo("LockDevice - Beeper Successfully")
            Else
                BeeperInfo("LockDevice - Beeper Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnLockDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnUnlockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlockDeviceNDC.Click
        Try

            If objBeeper.UnlockDevice() = True Then
                BeeperInfo("UnlockDevice - Beeper Successfully")
            Else
                BeeperInfo("UnlockDevice - Beeper Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnUnlockDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnWakeDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWakeDeviceNDC.Click
        Try

            If objBeeper.WakeUpDevice(1) = True Then
                BeeperInfo("WakeupDevice - Beeper Successfully")
            Else
                BeeperInfo("WakeupDevice - Beeper Failed")
                'MsgBox("WakeupDevice Beeper Failed", MsgBoxStyle.Critical, "Lock Beeper Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnWakeDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnDiagDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagDeviceNDC.Click
        Try

            If objBeeper.DiagnosticDevice() = True Then
                BeeperInfo("DiagnosticDevice - Beeper Successfully")
            Else
                BeeperInfo("DiagnosticDevice - Beeper Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnDiagDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnGetDevProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDevProp.Click
        Try
            strEvtTxtMsg = ""

            'strEvtMsg = "Device Status:" & objBeeperInterface.DeviceStatus
            'UpdateEvtInfo()

            strEvtMsg = "Txn Status:" & objBeeper.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objBeeper.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objBeeper.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objBeeper.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objBeeper.MStatusData
            UpdateEvtInfo()


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnGetDevProp_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

   
    Private Sub btnClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnClearText_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

   
#End Region

#Region "Beeper Support Command"

    Private Sub cmdTriggerON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerON.Click
        Try
            If objBeeper.Trigger(True) = True Then
                BeeperInfo("TriggerOn - Trigger On Beeper Successfully")
            Else
                BeeperInfo("TriggerOn - Trigger On Beeper Failed")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerON_Click. ErrInfo:"" & ex.Message"
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFF.Click
        Try
            If objBeeper.Trigger(False) = True Then
                BeeperInfo("TriggerOff - Trigger Off Beeper Successfully")
            Else
                BeeperInfo("TriggerOff - Trigger Off Beeper Failed")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFF_Click. ErrInfo:"" & ex.Message"
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub



#End Region

#Region "Beeper Events"


    Private Sub objBeeper_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objBeeper.EvtDeviceDataReady
        Try
            BeeperInfo("objBeeper_EvtDeviceDataReady")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objBeeper_EvtDeviceDataReady. ErrInfo:"" & ex.Message"
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objBeeper_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objBeeper.EvtDeviceError
        Try
            BeeperInfo("objBeeper_EvtDeviceError")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objBeeper_EvtDeviceError. ErrInfo:"" & ex.Message"
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objBeeper_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objBeeper.EvtDeviceTimeout
        Try
            BeeperInfo("objBeeper_EvtDeviceTimeout")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objBeeper_EvtDeviceTimeout. ErrInfo:"" & ex.Message"
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class
