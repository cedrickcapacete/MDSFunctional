Imports System
Imports System.IO
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Windows.Forms
Imports ClsHSMHWDInterface

Public Class frmHSMControl

#Region "EPP Object"

    Public WithEvents objHSMInterface As ClsHSMHWDInterface.clsHardwareLayer

#End Region

    Private Sub frmHSMControl_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            AppLogInfo("Exit EPP Pin Pad control")
        Catch ex As Exception
            MsgBox("Error in frmHSMControl_FormClosed. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub frmHSMControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("Load EPP Pin Pad control")

            objHSMInterface = New ClsHSMHWDInterface.clsHardwareLayer

            'Init Display
            InitDisplay()


        Catch ex As Exception
            MsgBox("Error in frmHSMControl_Load. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub





#Region "Function"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""

            'EPP Setting
            'EPP Format Type
            ddlFormatType.SelectedIndex() = 0
            txtPAN.Text = "4283321014922343"
            txtPAD.Text = "FF"

        Catch ex As Exception
            MsgBox("Error in InitDisplay. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region


#Region "Events Display"

    Private Sub HSMInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            MsgBox("Error in HSMInfo. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
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
            MsgBox("Error in EPP-UpdateEvtInfo. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in EPP-EvtEventsMsg. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region

#Region "Command"


    Private Sub cmdInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInit.Click
        Try

            If objHSMInterface.StartDevice() = True Then
                HSMInfo("StartDevice - Init HSM Successfully")
            Else
                HSMInfo("StartDevice - Init HSM Failed")
            End If

        Catch ex As Exception
            AppLogErr("Error in cmdInitClick. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try
            If objHSMInterface.StopDevice() = True Then
                HSMInfo("StopDevice - Close HSM Successfully")
            Else
                HSMInfo("StopDevice - Close HSM Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdInitClick. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdWrapDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrapDevice.Click
        Try
            If objHSMInterface.WrapDevice() = True Then
                HSMInfo("WrapDevice - Wrap HSM Successful")
            Else
                HSMInfo("WrapDevice - Wrap HSM Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdWrapDevice. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdLockDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLockDevice.Click
        Try
            If objHSMInterface.LockDevice() = True Then
                HSMInfo("LockDevice - Lock HSM Successful")
            Else
                HSMInfo("LockDevice - Lock HSM Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdLockDevice. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdUnlockDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnlockDevice.Click
        Try
            If objHSMInterface.UnlockDevice() = True Then
                HSMInfo("UnlockDevice - Unlock HSM Successful")
            Else
                HSMInfo("UnlockDevice - Unlock HSM Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdUnlockDevice. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdUpdateMasterKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If objHSMInterface.UnlockDevice() = True Then
                HSMInfo("UnlockDevice - Unlock HSM Successful")
            Else
                HSMInfo("UnlockDevice - Unlock HSM Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdUpdateMasterKey. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub btnGenerateMAC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateMAC.Click
        Dim strMACData As String = ""

        Try

            If rbnGenMac.Checked = True And txtDataToMac.Text <> String.Empty Then

                strMACData = objHSMInterface.GenerateMac(txtDataToMac.Text)

                If strMACData = String.Empty Then
                    HSMInfo("GenerateMAC - GenerateMAC Failed")
                Else
                    txtMacedData.Text = strMACData
                    HSMInfo("GenerateMAC - GenerateMAC Successful")
                End If
            Else
                HSMInfo("GenerateMAC - GenerateMAC Failed")
            End If


        Catch ex As Exception
            AppLogErr("Error in btnGenerateMAC_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub btnVerifyMac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerifyMac.Click
        Try
            If rbnVerifyMac.Checked = True And txtDataToMac.Text <> String.Empty Then
                If objHSMInterface.VerifyMac(txtDataToMac.Text, txtMacedData.Text) = True Then
                    HSMInfo("VerifyMAC - Verify MAC Successful")
                Else
                    HSMInfo("VerifyMAC - Verify MAC Failed")
                End If

            Else
                HSMInfo("VerifyMAC - Verify MAC Faield")
            End If

        Catch ex As Exception
            AppLogErr("Error in btnVerifyMac_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdGetPinBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetPinBlock.Click
        Dim strEncPINBlock As String = ""

        Try
            If PIN_FORMAT_TYPE = 1 Then
                If rbnPAN.Checked = True And txtPAN.Text.Length >= 12 Then
                    strEncPINBlock = objHSMInterface.GetPinBlock()
                    txtPinBlock.Text = strEncPINBlock
                    HSMInfo("GetPinBlock() - Generate PIN Block Successful")
                Else
                    HSMInfo("GetPinBlock() - Generate PIN Block Failed")
                End If

            ElseIf PIN_FORMAT_TYPE = 2 Or PIN_FORMAT_TYPE = 3 Or PIN_FORMAT_TYPE = 5 Or PIN_FORMAT_TYPE = 6 Then
                If rbnPAD.Checked = True And (txtPAD.TextLength > 0) Then
                    strEncPINBlock = objHSMInterface.GetPinBlock()
                    txtPinBlock.Text = strEncPINBlock
                    HSMInfo("GetPinBlock() - Generate PIN Block Successful")
                Else
                    HSMInfo("GetPinBlock() - Generate PIN Block Failed")
                End If
            ElseIf PIN_FORMAT_TYPE = 4 Then
                If rbnNoParam.Checked = True Then
                    strEncPINBlock = objHSMInterface.GetPinBlock()
                    txtPinBlock.Text = strEncPINBlock
                    HSMInfo("GetPinBlock() - Generate PIN Block Successful")
                Else
                    HSMInfo("GetPinBlock() - Generate PIN Block Failed")
                End If
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdGetPinBlock_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub ddlFormatType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFormatType.SelectedIndexChanged
        Try
            If ddlFormatType.SelectedIndex() = 0 Then
                PIN_FORMAT_TYPE = 1

            ElseIf ddlFormatType.SelectedIndex() = 1 Then
                PIN_FORMAT_TYPE = 2

            ElseIf ddlFormatType.SelectedIndex() = 2 Then
                PIN_FORMAT_TYPE = 3

            ElseIf ddlFormatType.SelectedIndex() = 3 Then
                PIN_FORMAT_TYPE = 4

            ElseIf ddlFormatType.SelectedIndex() = 4 Then
                PIN_FORMAT_TYPE = 5

            ElseIf ddlFormatType.SelectedIndex() = 5 Then
                PIN_FORMAT_TYPE = 6
            End If
        Catch ex As Exception
            AppLogErr("Error in ddlFormatType_SelectedIndexChanged. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdUpdateWorkingKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateWorkingKey.Click
        Try
            If txtKeyIndex.Text.Length > 0 And txtEncKeyIndex.Text.Length > 0 And txtWorkingKey.Text.Length > 0 Then
                If objHSMInterface.UpdateWorkingKey(txtKeyIndex.Text, txtWorkingKey.Text, txtEncKeyIndex.Text, txtCheckSum.Text) Then
                    HSMInfo("UpdateWorkingKey() - Register Working Key Successful")
                Else
                    HSMInfo("UpdateWorkingKey() - Register Working Key Failed")
                End If
            Else
                HSMInfo("UpdateWorkingKey() - Register Working Key Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdUpdateWorkingKey_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdWakeUpDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWakeUpDevice.Click
        Try
            objHSMInterface.PinBlockType = PIN_FORMAT_TYPE
            objHSMInterface.PAN = txtPAN.Text
            objHSMInterface.PadString = txtPAD.Text

            If objHSMInterface.WakeUpDevice(0) = True Then
                HSMInfo("WakeUpDevice() - Wakeup HSM Successful")
            Else
                HSMInfo("WakeUpDevice() - Wakeup HSM Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdWakeUpDevice_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdDiagnosticDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDiagnosticDevice.Click
        Try

        Catch ex As Exception
            AppLogErr("Error in cmdDiagnosticDevice_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            AppLogErr("Error in cmdClearText_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdGetDeviceProperty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetDeviceProperty.Click
        Try
            strEvtTxtMsg = ""

            strEvtMsg = "Txn Status:" & objHSMInterface.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objHSMInterface.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objHSMInterface.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objHSMInterface.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objHSMInterface.MStatusData
            UpdateEvtInfo()

        Catch ex As Exception
            AppLogErr("Error in cmdGetDeviceProperty_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Support Command"



#End Region


#Region "Event"

    Private Sub objHSMInterface_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objHSMInterface.EvtDeviceDataReady
        Try
            AppLogInfo("Raise Event in EvtDeviceDataReady")
        Catch ex As Exception
            AppLogErr("Error in objHSMInterface_EvtDeviceDataReady. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objHSMInterface_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objHSMInterface.EvtDeviceError
        Try
            AppLogInfo("Raise Event in EvtDeviceError")
        Catch ex As Exception
            AppLogErr("Error in objHSMInterface_EvtDeviceError. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objHSMInterface_EvtDeviceTimeOut(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objHSMInterface.EvtDeviceTimeout
        Try
            AppLogInfo("Raise Event in EvtDeviceTimeOut")
        Catch ex As Exception
            AppLogErr("Error in objHSMInterface_EvtDeviceTimeOut. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region



    'Private Sub cmdGetPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetPAN.Click
    '    Try
    '        If objHSMInterface.UnlockDevice() = True Then
    '            HSMInfo("UnlockDevice - Unlock HSM Successful")

    '        Else
    '            HSMInfo("UnlockDevice - Unlock HSM Failed")
    '            MsgBox("Unlock HSM Failed", MsgBoxStyle.Critical, "Unlock HSM Failed")
    '        End If
    '    Catch ex As Exception
    '        objAppLogFunction.AppLogErr("Error in cmdGetPAN_Click:" & ex.Message)
    '        MsgBox("Error in cmdGetPAN_Click:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub cmdUpdateWorkingKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try
    '        If objHSMInterface.UpdateWorkingKey(txtKeyIndex.Text, txtWorkingKey.Text, txtEncKeyIndex.Text, "") = True Then
    '            HSMInfo("UnlockDevice - Unlock HSM Successful")
    '        Else
    '            HSMInfo("UnlockDevice - Unlock HSM Failed")
    '        End If
    '    Catch ex As Exception
    '        objAppLogFunction.AppLogErr("Error in cmdUpdateWorkingKey_Click:" & ex.Message)
    '        MsgBox("Error in cmdUpdateWorkingKey_Click:" & ex.Message)
    '    End Try
    'End Sub



End Class