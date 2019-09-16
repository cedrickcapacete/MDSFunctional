Imports System
Imports System.IO
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Windows.Forms
Imports ClsKeypadHWDInterface

Public Class frmKeypadControl

#Region "EPP Object"

    Public WithEvents objKeypadInterface As ClsKeypadHWDInterface.clsHardwareLayer
    Public WithEvents objEPPController As clsModuleController.clsEPPModuleController

#End Region

    Private Sub frmKeypadControl_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            AppLogInfo("Exit Keypad control")
        Catch ex As Exception
            MsgBox("Error in frmKeypadControl_FormClosed. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub frmKeypadControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("Load Keypad control")

            objKeypadInterface = New ClsKeypadHWDInterface.clsHardwareLayer

            'Init Display
            InitDisplay()

        Catch ex As Exception
            MsgBox("Error in frmKeypadControl_Load. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub



#Region "Function"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""
            'Input text - len setting
            txtMaxLen.Text = DefaultInputTextLen
        Catch ex As Exception
            MsgBox("Error in InitDisplay. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region

#Region "Events Display"

    Private Sub KeypadInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            MsgBox("Error in KeypadInfo. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
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
            MsgBox("Error in UpdateEvtInfo. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in EvtEventsMsg. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region


  

#Region "Command - cmdInit, cmdClose etc"

    Private Sub cmdInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInit.Click
        Try

            If objKeypadInterface.StartDevice() = True Then
                KeypadInfo("StartDevice - Init Keypad Successfully")
            Else
                KeypadInfo("StartDevice - Init Keypad Failed")
            End If

        Catch ex As Exception
            AppLogErr("Error in cmdInitClick. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try
            If objKeypadInterface.StopDevice() = True Then
                KeypadInfo("StopDevice - Close Keypad Successfully")
            Else
                KeypadInfo("StopDevice - Close Keypad Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdClose_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub


    Private Sub cmdWrapDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrapDevice.Click
        Try
            If objKeypadInterface.WrapDevice() = True Then
                KeypadInfo("WrapDevice - Wrap Keypad Successful")
            Else
                KeypadInfo("WrapDevice - Wrap Keypad Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdWrapDevice. ErrInfo:" & ex.Message)

        End Try
    End Sub

    Private Sub cmdWakeupDevice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdWakeupDevice.Click
        Try
            KEY_STROKE_ENTER = ""
            KEY_ALL = ""

            If rbnClearText.Checked = True Then

                KeypadInfo("WakeupDevice - Clear Text")

                objKeypadInterface.EncryptMode = False
                objKeypadInterface.HideMode = False
                objKeypadInterface.Supervisor = False
                'objKeypadInterface.MaxLength = txtMaxLen.Text.Trim

                If objKeypadInterface.CmdEnableTextMode() = True Then
                    objKeypadInterface.HideMode = False

                    If objKeypadInterface.WakeUpDevice(0) = True Then
                        KeypadInfo("WakeupDevice - WakeupDevice Keypad Successful")
                    Else
                        KeypadInfo("WakeupDeviceDevice - WakeupDevice Keypad Failed")
                    End If

                Else
                    KeypadInfo("WakupeDevice - Wakupe Keypad Failed")
                End If

            ElseIf rbnEncryptText.Checked = True Then
                objKeypadInterface.EncryptMode = True
                objKeypadInterface.HideMode = True
                objKeypadInterface.Supervisor = False
                objKeypadInterface.MaxLength = txtMaxLen.Text.Trim

                If objKeypadInterface.CmdEnableEncryptMode() = True Then
                    If objKeypadInterface.WakeUpDevice(0) = True Then
                        KeypadInfo("WakeupDevice - WakeupDevice Keypad Successful")
                    Else
                        KeypadInfo("WakupeDeviceDevice - WakeupDevice Keypad Failed")
                    End If

                Else
                    KeypadInfo("WakupeDevice - Wakupe Keypad Failed")
                End If

            ElseIf rbnSupervisor.Checked = True Then
                objKeypadInterface.EncryptMode = False
                objKeypadInterface.HideMode = False
                objKeypadInterface.Supervisor = True

                If objKeypadInterface.WakeUpDevice(0) Then
                    KeypadInfo("WakeupDevice - WakeupDevice Keypad Successful")
                Else
                    KeypadInfo("WakupeDeviceDevice - WakeupDevice Keypad Failed")
                End If
            End If

        Catch ex As Exception
            AppLogErr("Error in cmdWakeupDevice. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdLockDevice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLockDevice.Click
        Try
            If objKeypadInterface.LockDevice() Then
                txtKeyedValue.Text = ""
                KeypadInfo("LockDevice - Lock Keypad Successfully")
            Else
                KeypadInfo("LockDevice - Lock Keypad Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdLockDevice. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdUnlockDevice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnlockDevice.Click
        Try
            If objKeypadInterface.UnlockDevice() Then
                KeypadInfo("UnlockDevice - Unlock Keypad Successfully")
            Else
                KeypadInfo("UnlockDevice - Unlock Keypad Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdUnlockDevice. ErrInfo:" & ex.Message)
        End Try
    End Sub


    Private Sub cmdDiagnosticDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDiagnosticDevice.Click
        Try

        Catch ex As Exception
            AppLogErr("Error in cmdDiagnosticDevice. ErrInfo:" & ex.Message)
        End Try
    End Sub


    Private Sub cmdClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            AppLogErr("Error in cmdClearText. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdGetDeviceProperty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetDeviceProperty.Click
        Try
            strEvtTxtMsg = ""

            strEvtMsg = "Txn Status:" & objKeypadInterface.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objKeypadInterface.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objKeypadInterface.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objKeypadInterface.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objKeypadInterface.MStatusData
            UpdateEvtInfo()

        Catch ex As Exception
            AppLogErr("Error in cmdGetDeviceProperty. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Support Command "


    Private Sub cmdStartPress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            txtKeyedValue.Text = ""
            KEY_ALL = ""
            If objKeypadInterface.CmdEnableTextMode() Then
                If objKeypadInterface.CmdCallKeyPress() Then
                    KeypadInfo("Start Key-in Value - Keypad Enable OK")
                End If
            Else
                KeypadInfo("Start Key-in Value - Keypad Enable Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdStartPress. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdStopPress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If objKeypadInterface.CmdDisableTextMode Then
                KeypadInfo("Stop Key-in Value - Keypad Disable OK")
            Else
                KeypadInfo("Stop Key-in Value - Keypad Disable Failed")
            End If
        Catch ex As Exception
            AppLogErr("Error in cmdStopPress. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdTxt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTxt.Click
        Try
            strEvtMsg = "Key Entered: " & objKeypadInterface.Text()
            UpdateEvtInfo()
        Catch ex As Exception
            AppLogErr("Error in cmdTxt_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub


#End Region


#Region "Events"

    Private Sub objKeypadInterface_CancelClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.CancelClick
        Try
            strEvtMsg = "objKeypadInterface_Cancel"
            UpdateEvtInfo()
            AppLogInfo("Raise Event in CancelClick")
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_CancelClick. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypadInterface_ClearClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.ClearClick
        Try
            strEvtMsg = "objKeypadInterface_ClearClick"
            UpdateEvtInfo()
            AppLogInfo("Raise Event in ClearClick")
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_ClearClick. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypadInterface_EnterClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.EnterClick
        Try
            strEvtMsg = "objKeypadInterface_EnterClick"
            UpdateEvtInfo()
            AppLogInfo("Raise Event in EnterClick")
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_EnterClick. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypadInterface_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.EvtDeviceDataReady
        Try
            strEvtMsg = "objKeypadInterface_EvtDeviceDataReady"
            UpdateEvtInfo()
            AppLogInfo("Raise Event in EvtDeviceDataReady")
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_EvtDeviceDataReady. ErrInfo:" & ex.Message)
        End Try
    End Sub


    'Private Sub objKeypadInterface_NumericClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.NumericClick
    '    Try
    '        strEvtMsg = "User press NUMERIC button"
    '        UpdateEvtInfo()
    '        AppLogInfo("Raise Event in NumericClick")
    '    Catch ex As Exception
    '        AppLogErr("Error in objKeypadInterface_NumericClick. ErrInfo:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub objKeypadInterface_Timeout() Handles objKeypadInterface.Timeout
    '    Try
    '        strEvtMsg = "TIMEOUT"
    '        UpdateEvtInfo()
    '        AppLogInfo("Raise Event in Timeout")
    '    Catch ex As Exception
    '        AppLogErr("Error in objKeypadInterface_Timeout. ErrInfo:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnWriteKeyA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWriteKeyA.Click
    '    Try
    '        If objKeypadInterface.WriteKey("A") Then
    '            'txtKeyedValue.Text = ""
    '            KeypadInfo("Start WriteKey - Write Key A Successfully")
    '        Else
    '            KeypadInfo("Start WriteKey - Write Key A Failed")
    '        End If
    '    Catch ex As Exception
    '        AppLogErr("Error in btnWriteKeyA_Click. ErrInfo:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnWriteKeyB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWriteKeyB.Click
    '    Try
    '        If objKeypadInterface.WriteKey("B") Then
    '            'txtKeyedValue.Text = ""
    '            KeypadInfo("Start WriteKey - Write Key B Successfully")
    '        Else
    '            KeypadInfo("Start WriteKey - Write Key B Failed")
    '        End If
    '    Catch ex As Exception
    '        AppLogErr("Error in btnWriteKeyB_Click. ErrInfo:" & ex.Message)

    '    End Try
    'End Sub

    'Private Sub btnMasterKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMasterKey.Click
    '    Try
    '        If objKeypadInterface.WriteKey("M") Then
    '            'txtKeyedValue.Text = ""
    '            KeypadInfo("WriteKey - Register Master Key Successfully")
    '        Else
    '            KeypadInfo("WriteKey - Register Master Key Failed")
    '        End If
    '    Catch ex As Exception
    '        AppLogErr("Error in btnMasterKey_Click. ErrInfo:" & ex.Message)
    '    End Try
    'End Sub


#End Region



    Private Sub objKeypadInterface_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.EvtDeviceError
        Try
            strEvtMsg = "objKeypadInterface_EvtDeviceError"
            UpdateEvtInfo()
            'AppLogInfo("Raise Event in HexKeyClick")
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_EvtDeviceError. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypadInterface_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.EvtDeviceTimeout
        Try
            strEvtMsg = "objKeypadInterface_EvtDeviceTimeout"
            UpdateEvtInfo()
            'AppLogInfo("Raise Event in HexKeyClick")
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_EvtDeviceTimeout. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypadInterface_HexKeyClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.HexKeyClick
        Try
            strEvtMsg = "objKeypadInterface_HexKeyClick - User press NUMERIC button" & e.mDeviceDataValue
            UpdateEvtInfo()
            AppLogInfo("Raise Event in HexKeyClick - User press NUMERIC button")
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_HexKeyClick. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypadInterface_KeypadPress(ByVal KeyStroke As String) Handles objKeypadInterface.KeypadPress
        Try
            strEvtMsg = "objKeypadInterface_KeypadPress - User press NUMERIC button" & KeyStroke
            UpdateEvtInfo()
            AppLogInfo("Raise Event in KeypadPress- User press NUMERIC button" & KeyStroke)
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_KeypadPress. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypadInterface_NumericClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.NumericClick
        Try
            AppLogInfo("Raise Event in NumericClick. " & e.mDeviceDataValue)

            strEvtMsg = "objKeypadInterface_NumericClick - User press NUMERIC button" & e.mDeviceDataValue
            UpdateEvtInfo()
            AppLogInfo("Raise Event in NumericClick - User press NUMERIC button" & e.mDeviceDataValue)
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_NumericClick. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypadInterface_Timeout() Handles objKeypadInterface.Timeout
        Try
            strEvtMsg = "TIMEOUT"
            UpdateEvtInfo()
            AppLogInfo("Raise Event in Timeout")
        Catch ex As Exception
            AppLogErr("Error in objKeypadInterface_Timeout. ErrInfo:" & ex.Message)
        End Try
    End Sub
End Class
