Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmRSIMain

#Region "Form - Variable"

    Dim strTitle As String = "frmRSIMain"

    Dim strEvtTxtMsg As String = ""
    Dim strEvtMsg As String = ""
    Dim strErrMsg As String = ""

    Dim blnStartDevice As Boolean = False

#End Region

#Region "RSI Object"

    Public objRSI As clsRSI.clsRSIHWD

#End Region


    Private Sub frmRSIMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStartDevice = True Then
                objRSI.StopDevice()
            End If
            objRSI = Nothing
            AppLogInfo("== End RSI System Mode ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmRSIMain_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmRSIMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("== Start RSI System Mode ==")

            'Init the Object - RSI
            objRSI = New clsRSI.clsRSIHWD

            blnStartDevice = False

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmRSIMain_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "InitDisplay"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""
            'btnRSISetting.Enabled = False
        Catch ex As Exception
            AppLogInfo("Err in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region



#Region "Events Display"

    Private Sub RSIInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            RSIInfo("Err in " & strTitle & ".RSIInfo. ErrInfo:" & ex.Message)
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


#Region "User Command"

    Private Sub btnRSISetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRSISetting.Click
        Try
            'RSI Setting
            frmRSISetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnRSISetting_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload Me
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnStartDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartDeviceNDC.Click
        Try

            blnStartDevice = True

            If objRSI.StartDevice() = True Then
                RSIInfo("StartDevice - Init RSI Successfully")
                'btnRSISetting.Enabled = True
            Else
                RSIInfo("StartDevice - Init RSI Failed")
                'btnRSISetting.Enabled = False
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnStartDeviceNDC_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnStopDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopDeviceNDC.Click
        Try
            If objRSI.StopDevice() = True Then
                RSIInfo("StopDevice - RSI Successfully")
            Else
                RSIInfo("StopDevice - RSI Failed")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnStopDeviceNDC_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnWrapDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWrapDeviceNDC.Click
        Try
            If objRSI.WrapDevice = True Then
                RSIInfo("WrapDevice - RSI Successfully")
            Else
                RSIInfo("WrapDevice - RSI Failed")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnWrapDeviceNDC_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnLockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLockDeviceNDC.Click
        Try
            If objRSI.LockDevice() = True Then
                RSIInfo("LockDevice - RSI Successfully")
            Else
                RSIInfo("LockDevice - RSI Failed")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnLockDeviceNDC_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnUnlockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlockDeviceNDC.Click
        Try
            If objRSI.UnlockDevice() = True Then
                RSIInfo("UnlockDevice - RSI Successfully")
            Else
                RSIInfo("UnlockDevice - RSI Failed")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnUnlockDeviceNDC_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnWakeDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWakeDeviceNDC.Click
        Try

            If objRSI.WakeUpDevice(1) = True Then
                RSIInfo("WakeupDevice - RSI Successfully")
            Else
                RSIInfo("WakeupDevice - RSI Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnWakeDeviceNDC_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnDiagDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagDeviceNDC.Click
        Try
            If objRSI.DiagnosticDevice() = True Then
                RSIInfo("DiagnosticDevice - RSI Successfully")
            Else
                RSIInfo("DiagnosticDevice - RSI Failed")
                'MsgBox("RSI Diagnostic Failed", MsgBoxStyle.Critical, "RSI Diagnostic Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnDiagDeviceNDC_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnGetDevProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDevProp.Click
        Try
          
            strEvtTxtMsg = ""

            'strEvtMsg = "Device Status:" & objRSIInterface.DeviceStatus
            'UpdateEvtInfo()

            strEvtMsg = "Txn Status:" & objRSI.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objRSI.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objRSI.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objRSI.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objRSI.MStatusData
            UpdateEvtInfo()


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnGetDevProp_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnClearText_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


#Region "RSI Light Control"


    Private Sub cmdTriggerONRSI1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerONRSI1.Click
        Try

            If objRSI.RSILIGHT1Trigger(True) = True Then
                RSIInfo("RSI Light1 Trigger ON Successfully")
            Else
                RSIInfo("RSI Light1 Trigger ON Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerONRSI1_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFFRSI1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFFRSI1.Click
        Try

            If objRSI.RSILIGHT1Trigger(False) = True Then
                RSIInfo("RSI Light1 Trigger OFF Successfully")
            Else
                RSIInfo("RSI Light1 Trigger OFF Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFFRSI1_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerONRSI2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerONRSI2.Click
        Try

            If objRSI.RSILIGHT2Trigger(True) = True Then
                RSIInfo("RSI Light2 Trigger ON Successfully")
            Else
                RSIInfo("RSI Light2 Trigger ON Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerONRSI2_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFFRSI2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFFRSI2.Click
        Try

            If objRSI.RSILIGHT2Trigger(False) = True Then
                RSIInfo("RSI Light2 Trigger OFF Successfully")
            Else
                RSIInfo("RSI Light2 Trigger OFF Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFFRSI2_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerONRSI3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerONRSI3.Click
        Try

            If objRSI.RSILIGHT3Trigger(True) = True Then
                RSIInfo("RSI Light3 Trigger ON Successfully")
            Else
                RSIInfo("RSI Light3 Trigger ON Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerONRSI3_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFFRSI3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFFRSI3.Click
        Try

            If objRSI.RSILIGHT3Trigger(False) = True Then
                RSIInfo("RSI Light3 Trigger OFF Successfully")
            Else
                RSIInfo("RSI Light3 Trigger OFF Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFFRSI3_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerONRSI4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerONRSI4.Click
        Try

            If objRSI.RSILIGHT4Trigger(True) = True Then
                RSIInfo("RSI Light4 Trigger ON Successfully")
            Else
                RSIInfo("RSI Light4 Trigger ON Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerONRSI4_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFFRSI4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFFRSI4.Click
        Try

            If objRSI.RSILIGHT4Trigger(False) = True Then
                RSIInfo("RSI Light4 Trigger OFF Successfully")
            Else
                RSIInfo("RSI Light4 Trigger OFF Failed")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFFRSI4_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class
