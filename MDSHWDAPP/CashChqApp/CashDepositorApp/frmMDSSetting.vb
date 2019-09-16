Imports CashDepositorHWD

Public Class frmMDSSetting

#Region "Form - Variable"

    Dim strTitle As String = "frmMDSSetting"

#End Region

#Region "MDS Object"

    Public objCashDepositor As clsCashDepositorHWDInterface

#End Region


    Private Sub frmMDSSetting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            objCashDepositor = Nothing
            AppLogInfo("== End MDS Setting ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMDSSetting_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMDSSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start MDS Setting ==")

            'Init the Object - MDS
            objCashDepositor = New clsCashDepositorHWDInterface

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMDSSetting_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "Init Display"

    Private Sub InitDisplay()
        Try

            With objCashDepositor

                'MDS Opt
                If .EnableMDSOpt = True Then
                    rbEnable.Checked = True
                Else
                    rbDisable.Checked = True
                End If

                'Comport
                NmComport.Value = .MDSComportNo

                'ForeAcceptCHQOpt
                If .ForeAcceptChqOpt = True Then
                    cbChqForeOpt.SelectedIndex = 1
                Else
                    cbChqForeOpt.SelectedIndex = 0
                End If

                'Timeout
                nmInsertItemTM.Value = .MDSInsertItemTimeout
                nmCleanFeederTM.Value = .MDSCleanFeederTimeout
                nmRepositionItemTM.Value = .MDSRepositionDocTimeout
                nmTakeReturnTM.Value = .MDSTakeReturnItemTimeout

                'PATH
                txtXFSLogPath.Text = .MDSXFSLogPath.Trim
                txtChqImagePath.Text = .MDSChqImagePath.Trim
                txtChqTemplatePath.Text = .MDSChqTemplatePath.Trim


            End With

        Catch ex As Exception
            AppLogErr("Err in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message)
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
        Dim strMDSEnable As String = ""
        Dim strForeChqOpt As String = ""
        Dim strMDSComport As String = ""

        Dim strMDSInsertTM As String = ""
        Dim strMDSRepositionTM As String = ""
        Dim strMDSCleanFeederTM As String = ""
        Dim strMDSTakeReturnTM As String = ""

        Dim strMDSTraceLogPath As String = ""
        Dim strMDSChqImgPath As String = ""
        Dim strMDSChqTemplatePath As String = ""


        Try

            'Enable Option
            If rbEnable.Checked = True Then
                strMDSEnable = "1"
            Else
                strMDSEnable = "0"
            End If

            If cbChqForeOpt.SelectedText = "TRUE" Then
                strForeChqOpt = "1"
            Else
                strForeChqOpt = "0"
            End If

            'Comport
            strMDSComport = CInt(NmComport.Value).ToString

            'Timeout
            strMDSInsertTM = CInt(nmInsertItemTM.Value).ToString
            strMDSRepositionTM = CInt(nmRepositionItemTM.Value).ToString
            strMDSCleanFeederTM = CInt(nmCleanFeederTM.Value).ToString
            strMDSTakeReturnTM = CInt(nmTakeReturnTM.Value).ToString

            'MDS Path
            strMDSTraceLogPath = txtXFSLogPath.Text.Trim
            strMDSChqImgPath = txtChqImagePath.Text.Trim
            strMDSChqTemplatePath = txtChqTemplatePath.Text.Trim

            If strMDSTraceLogPath.Length > 0 And strMDSChqImgPath.Length > 0 Then

                'MDS Setting Value
                AppLogInfo("Change MDSSetting Value")
                AppLogInfo("MDS Enable=" & strMDSEnable)
                AppLogInfo("Comport=" & strMDSComport)
                AppLogInfo("ForeChqDepositOpt=" & strForeChqOpt)

                AppLogInfo("Insert Timeout=" & strMDSInsertTM)
                AppLogInfo("Reposition Timeout=" & strMDSRepositionTM)
                AppLogInfo("Clean Feeder Timeout=" & strMDSCleanFeederTM)
                AppLogInfo("Take Return Timeout=" & strMDSTakeReturnTM)

                AppLogInfo("MDS Trace Log Path=" & strMDSTraceLogPath)
                AppLogInfo("MDS ChqImg Path=" & strMDSChqImgPath)
                AppLogInfo("MDS ChqTemplate Path=" & strMDSChqTemplatePath)

                'Update Card Reader Setting
                If objCashDepositor.UpdateMDSSetting(strMDSEnable, strForeChqOpt, strMDSComport, strMDSInsertTM, strMDSRepositionTM, strMDSCleanFeederTM, strMDSTakeReturnTM, strMDSTraceLogPath, strMDSChqImgPath, strMDSChqTemplatePath) = True Then
                    strErrMsg = "Update MDS Setting Successfully"
                    AppLogInfo(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Setting")
                Else
                    strErrMsg = "Update MDS Setting Failed"
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Setting")
                End If
            Else
                strErrMsg = "Empty File Path Selected"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "Error - File")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnConfirm_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub btnSelectPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectPath.Click
        Try

            'Select the Folder Control
            With FolderDlg
                .SelectedPath = "C:\"
                .ShowNewFolderButton = True
                .Description = "Use the tree below to select a folder:"
            End With

            If (FolderDlg.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                If (FolderDlg.SelectedPath.Length > 0) Then

                    'Trace File
                    If rbTraceFilePath.Checked = True Then
                        txtXFSLogPath.Text = FolderDlg.SelectedPath
                    End If

                    'Chq Images
                    If rbChqImgPath.Checked = True Then
                        txtChqImagePath.Text = FolderDlg.SelectedPath
                    End If

                    'Chq Template
                    If rbChqTemplatePath.Checked = True Then
                        txtChqTemplatePath.Text = FolderDlg.SelectedPath
                    End If

                End If
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnSelectPath_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


End Class