Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmMDSMainMenu

    Private Sub frmMDSMainMenu_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
        Catch ex As Exception
            MsgBox("Error in frmMDSMainMenu_FormClosed. ErrorInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub frmMDSMainMenu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
        Catch ex As Exception
            MsgBox("Error in  frmMDSMainMenu_Load. ErrorInfo:" & ex.Message)
        End Try
    End Sub

#Region "User Command"

    Private Sub cmdCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCash.Click
        Try
            AppLogInfo("MDS - Cash Modules")
            'MDS - CASH Modules
            CashDepositorApp.Show()
        Catch ex As Exception
            MsgBox("Error in cmdCash_Click. ErrorInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheque.Click
        Try
            AppLogInfo("MDS - Cheque Modules")
            'MDS - CHEQUE Modules
            ChequeDepositorApp.Show()
        Catch ex As Exception
            MsgBox("Error in cmdCheque_Click. ErrorInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdMDSDoor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDSDoor.Click
        Try
            AppLogInfo("MDS - Door Modules")
            'MDS - Door
            DoorSensorApp.Show()
        Catch ex As Exception
            MsgBox("Error in cmdMDSDoor_Click. ErrorInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Try
            AppLogInfo("MDS - Exit")
            'End Application
            Me.Close()
        Catch ex As Exception
            MsgBox("Error in cmdCancel_Click. ErrorInfo:" & ex.Message)
        End Try
    End Sub

#End Region

End Class