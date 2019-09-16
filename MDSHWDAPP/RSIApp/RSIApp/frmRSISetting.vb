Public Class frmRSISetting

#Region "Form - Variable"

    Dim strTitle As String = "frmRSISetting"

#End Region

#Region "RSI Object"

    Public objRSI As clsRSI.clsRSIHWD

#End Region


    Private Sub frmRSISetting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

            objRSI = Nothing

            'Unload
            AppLogInfo("== End RSI Setting ==")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmRSISetting_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmRSISetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start RSI Setting ==")

            'Init the Object - RSI
            objRSI = New clsRSI.clsRSIHWD

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmRSISetting_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "Init Display"

    Private Sub InitDisplay()
        Try

            With objRSI
                If .EnableRSI = True Then
                    rbEnable.Checked = True
                Else
                    rbDisable.Checked = True
                End If

                NmNoofRSIPort.Value = .NoofRSIComport
                NmComport1.Value = .RSI1ComportNo
                NmComport2.Value = .RSI2ComportNo

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
        Dim strRSIEnable As String = ""
        Dim strNoofRSI As String = ""
        Dim strComport1 As String = ""
        Dim strComport2 As String = ""


        Try

            If rbEnable.Checked = True Then
                strRSIEnable = "1"
            Else
                strRSIEnable = "0"
            End If

            strNoofRSI = CInt(NmNoofRSIPort.Value).ToString
            strComport1 = CInt(NmComport1.Value).ToString
            strComport2 = CInt(NmComport2.Value).ToString

            'RSI Setting Value
            AppLogInfo("Change RSI Setting Value")
            AppLogInfo("RSI Enable=" & strRSIEnable)
            AppLogInfo("No of RSI Comport=" & strNoofRSI)
            AppLogInfo("RSI Comport1=" & strComport1)
            AppLogInfo("RSI Comport2=" & strComport2)

            'Update RSI Setting
            If objRSI.UpdateRSISetting(strRSIEnable, strNoofRSI, strComport1, strComport2) = True Then
                'Refersh the Value
                If objRSI.ReadRSISetting() = True Then
                    InitDisplay()
                    strErrMsg = "Update RSI Setting Successfully"
                    AppLogInfo(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Information, "RSI Setting")
                Else
                    strErrMsg = "Update RSI Setting Failed"
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "RSI Setting")
                End If
            Else
                strErrMsg = "Update RSI Setting Failed"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "RSI Setting")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnConfirm_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class