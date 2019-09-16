Public Class frmMDSLightControl

#Region "Variable"

    Dim strTitle As String = "frmMDSLightControl"
    Dim blnStarDevice As Boolean = False

#End Region


    Private Sub frmMDSLightControl_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            AppLogInfo("== End MDS Light Control Menu ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMDSLightControl_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMDSLightControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("== Start MDS Light Control Menu ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMDSLightControl_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

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

#End Region

#Region "MDS Light"


    Private Sub cmdMDSLightON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDSLightON.Click
        Try
            If frmMaintenanceMode.objCashDepositor.StartMDSLight("0") Then
                strErrMsg = "ON MDS Top Kiosk Light Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Top Kiosk Light")
            Else
                strErrMsg = "ON MDS Top Kiosk Light Failed"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Top Kiosk Light")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdMDSLightON_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdMDSLightOFF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDSLightOFF.Click
        Try
            If frmMaintenanceMode.objCashDepositor.StartMDSLight("1") Then
                strErrMsg = "OFF MDS Top Kiosk Light Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Top Kiosk Light")
            Else
                strErrMsg = "OFF MDS Top Kiosk Light Failed"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Top Kiosk Light")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdMDSLightOFF_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


#Region "MDS Card Reader Light"


    Private Sub cmdMDSCRLightON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDSCRLightON.Click
        Try
            If frmMaintenanceMode.objCashDepositor.StartMDSReaderLight("0") Then
                strErrMsg = "ON MDS Card Reader Light Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Card Reader Light")
            Else
                strErrMsg = "ON MDS Card Reader Light Failed"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Card Reader Light")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdMDSCRLightON_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdMDSCRLightOFF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDSCRLightOFF.Click
        Try
            If frmMaintenanceMode.objCashDepositor.StartMDSReaderLight("1") Then
                strErrMsg = "OFF MDS Card Reader Light Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Card Reader Light")
            Else
                strErrMsg = "OFF MDS Card Reader Light Failed"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Card Reader Light")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdMDSCRLightOFF_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdCRFlushFast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCRFlushFast.Click
        Try
            If frmMaintenanceMode.objCashDepositor.StartMDSReaderLight("2") Then
                strErrMsg = "MDS Card Reader Light Flashing Fast Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Card Reader Light")
            Else
                strErrMsg = "MDS Card Reader Light Flashing Fast Failed"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Card Reader Light")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdCRFlushFast_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdCRFlushSlow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCRFlushSlow.Click
        Try
            If frmMaintenanceMode.objCashDepositor.StartMDSReaderLight("3") Then
                strErrMsg = "MDS Card Reader Light Flashing Slow Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Card Reader Light")
            Else
                strErrMsg = "MDS Card Reader Light Flashing Slow Failed"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Card Reader Light")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdCRFlushSlow_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region



End Class