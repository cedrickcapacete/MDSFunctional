Imports System.IO


Public Class frmUPSSetting


#Region "Form - Variable"

    Dim strTitle As String = "frmUPSSetting"

    Public objUPSInterface As ClsUPSHWDInterface.clsHardwareLayer

#End Region

    Private Sub frmUPSSetting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'Unload
            objUPSInterface = Nothing
            AppLogInfo("== End UPS Setting ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmUPSSetting_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmUPSSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start UPS Setting ==")

            objUPSInterface = New ClsUPSHWDInterface.clsHardwareLayer

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmUPSSetting_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "Init Display"

    Private Sub InitDisplay()
        Try

            With objUPSInterface
                If .EnableUPS = True Then
                    rbEnable.Checked = True
                Else
                    rbDisable.Checked = True
                End If

                txtUPSStatusPath.Text = .UPSStatusPath.Trim

            End With

        Catch ex As Exception
            AppLogInfo("Err in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub


#End Region

#Region "User Command"

    Private Sub btnSelectPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectPath.Click
        Dim strTmp As String = ""
        Dim strFileList As String = ""
        Dim strErrMsg As String = ""

        Try

            With opnFile
                .Title = "Please select the UPS Status File"
                .InitialDirectory = "E:\"
                .Filter = "UPS Status File All files (*.*)|*.*"
            End With

            If (opnFile.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                If (opnFile.FileNames.Length > 0) Then

                    txtUPSStatusPath.Text = opnFile.FileName

                    'For Each strTmp In opnFile.FileNames
                    '    strFileList += Path.GetFileName(strTmp) & ";"
                    'Next
                    'txtUPSStatusPath.Text = strFileList.Substring(0, strFileList.Length - 1)
                End If
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnSelectPath_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim strUPSEnable As String = ""
        Dim strUPSStatusPath As String = ""

        Try

            strUPSStatusPath = txtUPSStatusPath.Text.Trim

            If strUPSStatusPath.Length > 0 Then

                If rbEnable.Checked = True Then
                    strUPSEnable = "1"
                Else
                    strUPSEnable = "0"
                End If

                'UPS Setting Value
                AppLogInfo("Change UPS Setting Value")
                AppLogInfo("UPS Enable=" & strUPSEnable)
                AppLogInfo("UPS Status Monitoring Path=" & strUPSStatusPath)

                'Update the UPS Setting
                If objUPSInterface.UpdateUPSSetting(strUPSEnable, strUPSStatusPath) = True Then
                    'Refersh the Value
                    If objUPSInterface.ReadUPSSetting() = True Then
                        InitDisplay()
                        strErrMsg = "Update UPS Setting Successfully"
                        AppLogInfo(strErrMsg)
                        MsgBox(strErrMsg, MsgBoxStyle.Information, "UPS Setting")
                    Else
                        strErrMsg = "Update UPS Setting Failed"
                        AppLogWarn(strErrMsg)
                        MsgBox(strErrMsg, MsgBoxStyle.Critical, "UPS Setting")
                    End If
                Else
                    strErrMsg = "Update UPS Setting Failed"
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "UPS Setting")
                End If
            Else
                strErrMsg = "Empty UPS Status Path"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "Input Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnConfirm_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class