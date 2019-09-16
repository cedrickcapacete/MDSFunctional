Imports System.IO
Imports CardReaderHWD
Imports clsCardReader.clsCardReaderControl

Public Class frmCardReaderSetting

#Region "Form - Variable"

    Dim strTitle As String = "frmCardReaderSetting"

  
#End Region

#Region "CardReader Object"

    Public objCardReaderInterface As clsCardReaderHWDInterface

#End Region


    Private Sub frmCardReaderSetting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

            objCardReaderInterface = Nothing

            AppLogInfo("== End Card Reader Setting ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmCardReaderSetting_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmCardReaderSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start Card Reader Setting ==")

            'Init the Object - Card Reader
            objCardReaderInterface = New clsCardReaderHWDInterface

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmCardReaderSetting_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "Init Display"

    Private Sub InitDisplay()
        Try

            With objCardReaderInterface

                If .EnableCardReaderOpt = True Then
                    rbEnable.Checked = True
                Else
                    rbDisable.Checked = True
                End If

                NmComport.Value = .CardReaderComportNo

                nmCMDTimeout.Value = .CardReaderCommandTimeout
                nmCheckingTimeout.Value = .CardReaderCheckTimeout
                nmEjectTimeout.Value = .CardReaderEjectTimeout

                txtPSAMIDPath.Text = .CardReaderPSAMIDPath.Trim

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

    Private Sub btnSelectPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectPath.Click
        Dim strTmp As String = ""
        Dim strFileList As String = ""
        Dim strErrMsg As String = ""

        Try

            With opnFile
                .Title = "Please select the PSAMID File"
                .InitialDirectory = "E:\"
                .Filter = " PSAMID File All files (*.*)|*.*"
            End With

            If (opnFile.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                If (opnFile.FileNames.Length > 0) Then

                    txtPSAMIDPath.Text = opnFile.FileName

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
        Dim strCREnable As String = ""
        Dim strCRComport As String = ""
        Dim strCRCMDTM As String = ""
        Dim strCRCHKTM As String = ""
        Dim strCREJTTM As String = ""
        Dim strCRPSAMPATH As String = ""


        Try

            If rbEnable.Checked = True Then
                strCREnable = "1"
            Else
                strCREnable = "0"
            End If

            strCRComport = CInt(NmComport.Value).ToString

            strCRCMDTM = CInt(nmCMDTimeout.Value).ToString
            strCRCHKTM = CInt(nmCheckingTimeout.Value).ToString
            strCREJTTM = CInt(nmEjectTimeout.Value).ToString

            strCRPSAMPATH = txtPSAMIDPath.Text.Trim


            If strCRPSAMPATH.Length > 0 Then

            
                'Card Reader Setting Value
                AppLogInfo("Change Card Reader Setting Value")
                AppLogInfo("Card Reader Enable=" & strCREnable)
                AppLogInfo("Card Reader Comport=" & strCRComport)
                AppLogInfo("Command Timeout=" & strCRCMDTM)
                AppLogInfo("Checking Timeout=" & strCRCHKTM)
                AppLogInfo("Eject Timeout=" & strCREJTTM)
                AppLogInfo("PSAMID Path=" & strCRPSAMPATH)

                'Update Card Reader Setting
                If objCardReaderInterface.UpdateCardReaderSetting(strCREnable, strCRComport, strCRCMDTM, strCRCHKTM, strCREJTTM, strCRPSAMPATH) = True Then
                    'Refersh the Value
                    If objCardReaderInterface.ReadCardReaderSetting() = True Then
                        InitDisplay()
                        strErrMsg = "Update Card Reader Setting Successfully"
                        AppLogInfo(strErrMsg)
                        MsgBox(strErrMsg, MsgBoxStyle.Information, "Card Reader Setting")
                    Else
                        strErrMsg = "Update Card Reader Setting Failed"
                        AppLogWarn(strErrMsg)
                        MsgBox(strErrMsg, MsgBoxStyle.Critical, "Card Reader Setting")
                    End If
                Else
                    strErrMsg = "Update Card Reader Setting Failed"
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "Card Reader Setting")
                End If

            Else
                strErrMsg = "Empty PSAMID File Path Selected"
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "Error - PSAMID File")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnConfirm_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class