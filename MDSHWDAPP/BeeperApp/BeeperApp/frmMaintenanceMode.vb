Public Class frmMaintenanceMode

#Region "Variable"

    Dim strTitle As String = "frmMaintenanceMode"

    Dim blnStartDevice As Boolean = False


#End Region


#Region "Beeper Object"

    Public WithEvents objBeeper As clsBeeperHWD.clsBeeperControl

#End Region

    Private Sub frmMaintenanceMode_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStartDevice = True Then
                objBeeper.StopDevice()
            End If

            objBeeper = Nothing

            AppLogInfo("== End Beeper Diagnostic Mode ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMaintenanceMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("== Start Beeper Diagnostic Mode ==")

            'Init the Object - Beeper
            objBeeper = New clsBeeperHWD.clsBeeperControl

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
            'btnBeeperSetting.Enabled = False
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


#Region "User Command"


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

    Private Sub btnStartDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartDeviceNDC.Click
        Try
           
            If objBeeper.StartDevice() = True Then
                blnStartDevice = True
                strLogMsg = "StartDevice - Init Beeper Successfully"
                AppLogInfo(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                'btnBeeperSetting.Enabled = True
            Else
                blnStartDevice = False
                strLogMsg = "StartDevice - Init Beeper Failed"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                'btnBeeperSetting.Enabled = False
            End If

        Catch ex As Exception
            blnStartDevice = False
            strErrMsg = "Error in " & strTitle & ".btnStartDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnStopDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopDeviceNDC.Click
        Try

            If blnStartDevice = True Then
                blnStartDevice = False
                If objBeeper.StopDevice() = True Then
                    strLogMsg = "StopDevice - Beeper Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "StopDevice - Beeper Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Beeper Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnStopDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerON.Click
        Try
            If blnStartDevice = True Then
                If objBeeper.WakeUpDevice(1) = True Then
                    strLogMsg = "WakeupDevice - Beeper Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "WakeupDevice - Beeper Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Beeper Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerON_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnWrapDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWrapDeviceNDC.Click
        Try

            If blnStartDevice = True Then
                If objBeeper.WrapDevice = True Then
                    strLogMsg = "WrapDevice - Beeper Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "WrapDevice - Beeper Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Beeper Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ". btnWrapDeviceNDC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdTriggerOFF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTriggerOFF.Click
        Try
            If blnStartDevice = True Then
                If objBeeper.LockDevice() = True Then
                    strLogMsg = "LockDevice - Beeper Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "LockDevice - Beeper Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Beeper Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdTriggerOFF_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload Me
            AppLogInfo("Beeper Diagnostic Menu - Exit")
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "Beeper Events"

    Private Sub objBeeper_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objBeeper.EvtDeviceDataReady
        Try
            AppLogInfo("objBeeper_EvtDeviceDataReady")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objBeeper_EvtDeviceDataReady. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objBeeper_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objBeeper.EvtDeviceError
        Try
            AppLogInfo("objBeeper_EvtDeviceError")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objBeeper_EvtDeviceError. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objBeeper_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objBeeper.EvtDeviceTimeout
        Try
            AppLogInfo("objBeeper_EvtDeviceTimeout")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objBeeper_EvtDeviceTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class