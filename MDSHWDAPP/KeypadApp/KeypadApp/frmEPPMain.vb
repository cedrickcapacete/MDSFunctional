Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmEPPMain


    Private Sub frmEPPMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

        Catch ex As Exception
            MsgBox("Error in frmEPPMain_FormClosed. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub frmEPPMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
        Catch ex As Exception
            MsgBox("Error in frmEPPMain_Load. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#Region "Command - cmdKeyPad,cmdEPP,cmdCancel"


    Private Sub cmdKeyPad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKeyPad.Click
        Try
            'Key Pad Control
            frmKeypadControl.Show()
        Catch ex As Exception
            AppLogErr("Error in cmdKeyPad_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdEPP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEPP.Click
        Try
            'EPP Control
            frmHSMControl.Show()
        Catch ex As Exception
            AppLogErr("Error in cmdEPP_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Try
            'Exit
            Me.Close()
        Catch ex As Exception
            AppLogErr("Error in cmdCancel_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region



End Class