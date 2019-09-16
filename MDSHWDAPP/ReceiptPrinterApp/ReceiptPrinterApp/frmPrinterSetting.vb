Imports ClsReceiptPrinterHWDInterface


Public Class frmPrinterSetting

#Region "Form - Variable"

    Dim strTitle As String = "frmPrinterSetting"

#End Region

#Region "Printer Object"

    Dim objReceiptPrinterInterface As ClsReceiptPrinterHWDInterface.clsHardwareLayer

#End Region

    Private Sub frmPrinterSetting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'Unload
            objReceiptPrinterInterface = Nothing

            AppLogInfo("== End Printer Setting ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmPrinterSetting_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmPrinterSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start Printer Setting ==")

            'Init the Object Printer
            objReceiptPrinterInterface = New ClsReceiptPrinterHWDInterface.clsHardwareLayer

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmPrinterSetting_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "Init Display"

    Private Sub InitDisplay()
        Try

            With objReceiptPrinterInterface
                lblPrinterNm.Text = .ReceiptPrinterName.Trim
            End With

        Catch ex As Exception
            AppLogInfo("Err in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "User Command"


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

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim strPrinterNm As String = String.Empty
        Try

            'Printer Name
            strPrinterNm = lblPrinterNm.Text.Trim

            'Setting Value
            AppLogInfo("Change Printer Setting Value")
            AppLogInfo("Printer Name=" & strPrinterNm)

            'Update Value
            strErrMsg = "Update Printer Setting Successfully"
            AppLogInfo(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Information, "Printer Setting")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnConfirm_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


End Class