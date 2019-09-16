Public Class frmMaintenanceMode

#Region "Variable"

    Dim strTitle As String = "frmMaintenanceMode"

    Dim blnStartDevice As Boolean = False

#End Region

#Region "RSI Object"

    Public objRSI As clsRSI.clsRSIHWD

#End Region

    Private Sub frmMaintenanceMode_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStartDevice = True Then
                objRSI.StopDevice()
            End If

            objRSI = Nothing

            AppLogInfo("== End RSI Diagnostic Mode ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMaintenanceMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            
            AppLogInfo("== Start RSI Diagnostic Mode ==")

            'Init the Object - RSI
            objRSI = New clsRSI.clsRSIHWD

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
            'btnRSISetting.Enabled = False
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "User Command"

    Private Sub btnRSISetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRSISetting.Click
        Try
            'RSI Setting
            frmRSISetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnRSISetting_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload
            AppLogInfo("RSI Diagnostic Menu - Exit")
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnStartDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartDeviceNDC.Click
        Try

            If objRSI.StartDevice() = True Then
                blnStartDevice = True
                strLogMsg = "StartDevice - Init RSI Successfully"
                AppLogInfo(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
            Else
                blnStartDevice = False
                strLogMsg = "StartDevice - Init RSI Failed"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnStartDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnStopDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopDeviceNDC.Click
        Try

            If blnStartDevice = True Then

                blnStartDevice = False

                If objRSI.StopDevice() = True Then
                    strLogMsg = "StopDevice - RSI Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "StopDevice - RSI Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If

            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnStopDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnWrapDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWrapDeviceNDC.Click
        Try
            If blnStartDevice = True Then
                If objRSI.WrapDevice = True Then
                    strLogMsg = "WrapDevice - RSI Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "WrapDevice - RSI Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnWrapDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerONRSI1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerONRSI1.Click
        Try

            If blnStartDevice = True Then
                If objRSI.RSILIGHT1Trigger(True) = True Then
                    strLogMsg = "RSI Light1 Trigger ON Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "RSI Light1 Trigger ON Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerONRSI1_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFFRSI1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFFRSI1.Click
        Try

            If blnStartDevice = True Then
                If objRSI.RSILIGHT1Trigger(False) = True Then
                    strLogMsg = "RSI Light1 Trigger OFF Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "RSI Light1 Trigger OFF Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFFRSI1_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerONRSI2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerONRSI2.Click
        Try
            If blnStartDevice = True Then
                If objRSI.RSILIGHT2Trigger(True) = True Then
                    strLogMsg = "RSI Light2 Trigger ON Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "RSI Light2 Trigger ON Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerONRSI2_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFFRSI2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFFRSI2.Click
        Try
            If blnStartDevice = True Then
                If objRSI.RSILIGHT2Trigger(False) = True Then
                    strLogMsg = "RSI Light2 Trigger OFF Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "RSI Light2 Trigger OFF Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFFRSI2_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerONRSI3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerONRSI3.Click
        Try
            If blnStartDevice = True Then
                If objRSI.RSILIGHT3Trigger(True) = True Then
                    strLogMsg = "RSI Light3 Trigger ON Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "RSI Light3 Trigger ON Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerONRSI3_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFFRSI3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFFRSI3.Click
        Try
            If blnStartDevice = True Then
                If objRSI.RSILIGHT3Trigger(False) = True Then
                    strLogMsg = "RSI Light3 Trigger OFF Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "RSI Light3 Trigger OFF Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFFRSI3_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerONRSI4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerONRSI4.Click
        Try
            If blnStartDevice = True Then
                If objRSI.RSILIGHT4Trigger(True) = True Then
                    strLogMsg = "RSI Light4 Trigger ON Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "RSI Light4 Trigger ON Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerONRSI4_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFFRSI4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFFRSI4.Click
        Try
            If blnStartDevice = True Then
                If objRSI.RSILIGHT4Trigger(False) = True Then
                    strLogMsg = "RSI Light4 Trigger OFF Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "RSI Light4 Trigger OFF Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "RSI Comport Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFFRSI4_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class