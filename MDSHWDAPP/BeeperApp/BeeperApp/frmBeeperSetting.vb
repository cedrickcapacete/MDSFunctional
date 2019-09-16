Public Class frmBeeperSetting

#Region "Form - Variable"

    Dim strTitle As String = "frmBeeperSetting"

#End Region

#Region "Beeper Object"

    Public objBeeper As clsBeeperHWD.clsBeeperControl

#End Region



    Private Sub frmBeeperSetting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

            objBeeper = Nothing

            'Unload
            AppLogInfo("== End Beeper Setting ==")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmBeeperSetting_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmBeeperSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start Beeper Setting ==")

            'Init the Object - Beeper
            objBeeper = New clsBeeperHWD.clsBeeperControl


            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmBeeperSetting_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "Init Display"

    Private Sub InitDisplay()
        Try

            With objBeeper
                If .EnableBeeper = True Then
                    rbEnable.Checked = True
                Else
                    rbDisable.Checked = True
                End If

                NmComport1.Value = .BeeperComportNo

            End With

        Catch ex As Exception
            AppLogInfo("Err in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub


#End Region

#Region "User Command"

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim strBeeperEnable As String = ""
        Dim strComportNo As String = ""

        Try


            If rbEnable.Checked = True Then
                strBeeperEnable = "1"
            Else
                strBeeperEnable = "0"
            End If

            strComportNo = CInt(NmComport1.Value).ToString

            'Beeper Setting Value
            AppLogInfo("Change Beeper Setting Value")
            AppLogInfo("Beeper Enable=" & strBeeperEnable)
            AppLogInfo("Beeper Comport=" & strComportNo)

            'Update Beeper Setting
            If objBeeper.UpdateBeeperSetting(strBeeperEnable, strComportNo) = True Then
                'Refersh the Value
                If objBeeper.ReadBeeperSetting() = True Then
                    InitDisplay()
                    strErrMsg = "Update Beeper Setting Successfully"
                    AppLogInfo(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Information, "Beeper Setting")
                Else
                    strErrMsg = "Update Beeper Setting Failed"
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "Beeper Setting")
                End If
            Else
                strErrMsg = "Update Beeper Setting Failed"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "Beeper Setting")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnConfirm_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload Me
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


End Class