Imports System
Imports System.IO
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Windows.Forms
Imports ClsKeypadHWDInterface

Public Class frmMaintenanceMode

#Region "Variable"

    Dim strTitle As String = "frmMaintenanceMode"
    Dim blnStarDevice As Boolean = False

#End Region

#Region "EPP Object"

    Public WithEvents objKeypadInterface As ClsKeypadHWDInterface.clsHardwareLayer
    'Public WithEvents objEPPController As clsModuleController.clsEPPModuleController

#End Region


    Private Sub frmMaintenanceMode_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStarDevice = True Then
                objKeypadInterface.StopDevice()
            End If

            objKeypadInterface = Nothing

            AppLogInfo("== End Keypad Diagnostic Mode ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMaintenanceMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start Keypad Diagnostic Mode ==")

            'Init the Object - Keypad
            objKeypadInterface = New ClsKeypadHWDInterface.clsHardwareLayer

            blnStarDevice = False

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
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "User Command"


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnKeypadSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeypadSetting.Click
        Try
            'Keypad Setting
            frmKeyPadSetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnKeypadSetting_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInit.Click
        Try
            txtDeviceProperty.Text = ""

            If objKeypadInterface.StartDevice() = True Then
                blnStarDevice = True
                strLogMsg = "StartDevice - Init Keypad Successfully"
                AppLogInfo(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
            Else
                blnStarDevice = False
                strLogMsg = "StartDevice - Init Keypad Failed"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdInit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdWakeupDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWakeupDevice.Click
        Try
            txtDeviceProperty.Text = ""

            If blnStarDevice = True Then

                KEY_STROKE_ENTER = ""
                KEY_ALL = ""

                objKeypadInterface.EncryptMode = False
                objKeypadInterface.HideMode = False
                objKeypadInterface.Supervisor = False
                'objKeypadInterface.MaxLength = txtMaxLen.Text.Trim

                If objKeypadInterface.CmdEnableTextMode() = True Then

                    objKeypadInterface.HideMode = False

                    If objKeypadInterface.WakeUpDevice(0) = True Then
                        strLogMsg = "WakeupDevice - Init Keypad Successfully"
                        AppLogInfo(strLogMsg)
                        MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                    Else
                        strLogMsg = "WakupeDevice - Keypad Failed"
                        AppLogWarn(strLogMsg)
                        MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                    End If

                Else
                    strLogMsg = "WakupeDevice - Keypad Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If

            Else
                strLogMsg = "Keypad is not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdWakeupDevice_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub cmdClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdClearText_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub cmdStopDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopDevice.Click
        Try
            txtDeviceProperty.Text = ""

            If blnStarDevice = True Then
                blnStarDevice = False
                If objKeypadInterface.StopDevice() = True Then
                    strLogMsg = "StopDevice - Init Keypad Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "StopDevice - Init Keypad Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Keypad is not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdStopDevice_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
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
            'AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in EvtEventsMsg. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region

#Region "Keypad Events"


    Private Sub objKeypadInterface_CancelClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.CancelClick
        Try
            AppLogInfo("Keypad Cancel Click Events")

            strEvtMsg = "Keypad:Cancel Click"
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objKeypadInterface_CancelClick:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objKeypadInterface_ClearClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.ClearClick
        Try
            AppLogInfo("Keypad Clear Click Events")

            strEvtMsg = "Keypad:Clear Click"
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objKeypadInterface_ClearClick:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objKeypadInterface_EnterClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.EnterClick
        Try
            AppLogInfo("Keypad Enter Click Events")

            strEvtMsg = "Keypad:Enter Click"
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objKeypadInterface_EnterClick:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objKeypadInterface_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.EvtDeviceDataReady
        Try
            strEvtMsg = "Keypad Evt DataReady Events"
            AppLogInfo(strEvtMsg)
            UpdateEvtInfo()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objKeypadInterface_EvtDeviceDataReady:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objKeypadInterface_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.EvtDeviceError
        Try
            strEvtMsg = "Keypad Evt DeviceError Events"
            AppLogInfo(strEvtMsg)
            UpdateEvtInfo()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objKeypadInterface_EvtDeviceError:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objKeypadInterface_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.EvtDeviceTimeout
        Try
            strEvtMsg = "Keypad Evt Timeout Events"
            AppLogInfo(strEvtMsg)
            UpdateEvtInfo()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objKeypadInterface_EvtDeviceTimeout:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    'Private Sub objKeypadInterface_HexKeyClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.HexKeyClick
    '    Try
    '        AppLogInfo("Keypad HexKey Click Events")
    '    Catch ex As Exception
    '        strErrMsg = "Error in " & strTitle & ".objKeypadInterface_HexKeyClick:" & ex.Message
    '        AppLogErr(strErrMsg)
    '        MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
    '    End Try
    'End Sub

    Private Sub objKeypadInterface_KeypadPress(ByVal KeyStroke As String) Handles objKeypadInterface.KeypadPress
        Try
            AppLogInfo("Keypad KeypadPress Events. KeyValue:" & KeyStroke)

            strEvtMsg = "Keypad - KeyValue:" & KeyStroke
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objKeypadInterface_KeypadPress:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    'Private Sub objKeypadInterface_NumericClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objKeypadInterface.NumericClick
    '    Try
    '        AppLogInfo("Keypad Numeric Click Events")
    '    Catch ex As Exception
    '        strErrMsg = "Error in " & strTitle & ".objKeypadInterface_NumericClick:" & ex.Message
    '        AppLogErr(strErrMsg)
    '        MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
    '    End Try
    'End Sub

    Private Sub objKeypadInterface_Timeout() Handles objKeypadInterface.Timeout
        Try
            strEvtMsg = "Keypad Timeout Events"
            AppLogInfo(strEvtMsg)
            UpdateEvtInfo()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objKeypadInterface_Timeout:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

 
  
End Class