Public Class frmKeyPadSetting

#Region "Form - Variable"

    Dim strTitle As String = "frmKeyPadSetting"


#End Region

#Region "Keypad Variable"

    Public objKeypadInterface As ClsKeypadHWDInterface.clsHardwareLayer

#End Region

    Private Sub frmKeyPadSetting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'Unload
            objKeypadInterface = Nothing
            AppLogInfo("== End Keypad Setting ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmKeyPadSetting_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmKeyPadSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("== Start Keypad Setting ==")

            objKeypadInterface = New ClsKeypadHWDInterface.clsHardwareLayer

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmKeyPadSetting_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "Init Display"

    Private Sub InitDisplay()
        Dim strComport As String = ""
        Dim strTmpComport As String = ""

        Try

            With objKeypadInterface

                'EPP Model
                lblKeypadModel.Text = .KeypadModel

                'Comport No
                strTmpComport = .KeypadComport.Trim
                strComport = Mid(strTmpComport, 4, strTmpComport.Length - 3)
                strComport = strComport.Trim
                NmComport.Value = strComport

            End With

        Catch ex As Exception
            AppLogErr("Err in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub


#End Region

#Region "User Command"


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


    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim strKeypadNm As String = ""
        Dim strKeypadComport As String = ""

        Try

            strKeypadNm = lblKeypadModel.Text.Trim
            strKeypadComport = CInt(NmComport.Value).ToString

            If strKeypadNm.Length > 0 Then

                'Keypad Setting Value
                AppLogInfo("Change Keypad Setting Value")
                AppLogInfo("Keypad Name=" & strKeypadNm)
                AppLogInfo("Keypad Comport=" & strKeypadComport)

                'Update Keypad Setting
                If objKeypadInterface.UpdateKeyPadSetting(strKeypadNm, strKeypadComport) = True Then

                    strErrMsg = "Update Keypad Setting Successfully"
                    AppLogInfo(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Information, "Keypad Setting")


                    'Refersh the Value
                    'If objCardReaderInterface.ReadCardReaderSetting() = True Then
                    '    InitDisplay()
                    '    strErrMsg = "Update Card Reader Setting Successfully"
                    '    AppLogInfo(strErrMsg)
                    '    MsgBox(strErrMsg, MsgBoxStyle.Information, "Card Reader Setting")
                    'Else
                    '    strErrMsg = "Update Card Reader Setting Failed"
                    '    AppLogWarn(strErrMsg)
                    '    MsgBox(strErrMsg, MsgBoxStyle.Critical, "Card Reader Setting")
                    'End If
                Else
                    strErrMsg = "Update Keypad Setting Failed"
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "Keypad Setting")
                End If

            Else
                strErrMsg = "Empty Keypad Name"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnConfirm_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class