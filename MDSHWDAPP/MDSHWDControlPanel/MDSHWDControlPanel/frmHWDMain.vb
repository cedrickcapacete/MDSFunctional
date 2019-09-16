Imports System
Imports System.IO

Public Class frmHWDMain

#Region "Variable"

    Dim strTitle As String = "frmHWDMain"

#End Region

    Private Sub frmHWDMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            AppLogInfo("== End MDS Hardware Control Panel == ")
            'Close Logger
            CloseLog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmHWDMain_FormClosed:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Private Sub frmHWDMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start MDS Hardware Control Panel == ")

            'Version
            AppLogInfo("MDS HWD Control Panel Version=" & udtMDSParam.APPVersionID.Trim)
            lblApplVersion.Text = udtMDSParam.APPVersionID.Trim

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmHWDMain_Load:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

#Region "User Command"


    Private Sub btnCardReader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCardReader.Click
        Dim strAppPath As String = ""
        Try

            AppLogInfo("MDS Card Reader Selected")

            strAppPath = udtMDSParam.CardReaderAppPath.Trim

            If strAppPath.Length > 0 Then
                If File.Exists(strAppPath) = True Then
                    Shell(strAppPath, AppWinStyle.MaximizedFocus)
                Else
                    AppLogWarn("Card Reader Application File Not Found")
                    MsgBox("Card Reader Application File Not Found", MsgBoxStyle.Critical, "App Error")
                End If
            Else
                AppLogWarn("Card Reader Application File Not Found")
                MsgBox("Card Reader Application File Not Found", MsgBoxStyle.Critical, "App Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnCardReader_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub btnMDS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMDS.Click
        Dim strAppPath As String = ""
        Try

            AppLogInfo("MDS Devices Selected")

            strAppPath = udtMDSParam.MDSAppPath.Trim

            If strAppPath.Length > 0 Then
                If File.Exists(strAppPath) = True Then
                    Shell(strAppPath, AppWinStyle.MaximizedFocus)
                Else
                    AppLogWarn("MDS Application File Not Found")
                    MsgBox("MDS Application File Not Found", MsgBoxStyle.Critical, "App Error")
                End If
            Else
                AppLogWarn("MDS Application File Not Found")
                MsgBox("MDS Application File Not Found", MsgBoxStyle.Critical, "App Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnMDS_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub btnKeyPad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeyPad.Click
        Dim strAppPath As String = ""
        Try

            AppLogInfo("KeyPad/EPP Selected")

            strAppPath = udtMDSParam.KeyPadAppPath.Trim

            If strAppPath.Length > 0 Then
                If File.Exists(strAppPath) = True Then
                    Shell(strAppPath, AppWinStyle.MaximizedFocus)
                Else
                    AppLogWarn("Keypad Application File Not Found")
                    MsgBox("Keypad Application File Not Found", MsgBoxStyle.Critical, "App Error")
                End If
            Else
                AppLogWarn("Keypad Application File Not Found")
                MsgBox("Keypad Application File Not Found", MsgBoxStyle.Critical, "App Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnKeyPad_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub btnPrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrinter.Click
        Dim strAppPath As String = ""
        Try

            AppLogInfo("Printer Selected")

            strAppPath = udtMDSParam.PrinterAppPath.Trim

            If strAppPath.Length > 0 Then
                If File.Exists(strAppPath) = True Then
                    Shell(strAppPath, AppWinStyle.MaximizedFocus)
                Else
                    AppLogWarn("Printer Application File Not Found")
                    MsgBox("Printer Application File Not Found", MsgBoxStyle.Critical, "App Error")
                End If
            Else
                AppLogWarn("Printer Application File Not Found")
                MsgBox("Printer Application File Not Found", MsgBoxStyle.Critical, "App Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnPrinter_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub btnBeeper_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBeeper.Click
        Dim strAppPath As String = ""

        Try

            AppLogInfo("Beeper Selected")

            strAppPath = udtMDSParam.BeeperAppPath.Trim

            If strAppPath.Length > 0 Then
                If File.Exists(strAppPath) = True Then
                    Shell(strAppPath, AppWinStyle.MaximizedFocus)
                Else
                    AppLogWarn("Beeper Application File Not Found")
                    MsgBox("Beeper Application File Not Found", MsgBoxStyle.Critical, "App Error")
                End If
            Else
                AppLogWarn("Beeper Application File Not Found")
                MsgBox("Beeper Application File Not Found", MsgBoxStyle.Critical, "App Error")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnBeeper_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub btnUPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUPS.Click
        Dim strAppPath As String = ""
        Try

            AppLogInfo("UPS Selected")

            strAppPath = udtMDSParam.UPSAppPath.Trim

            If strAppPath.Length > 0 Then
                If File.Exists(strAppPath) = True Then
                    Shell(strAppPath, AppWinStyle.MaximizedFocus)
                Else
                    AppLogWarn("UPS Application File Not Found")
                    MsgBox("UPS Application File Not Found", MsgBoxStyle.Critical, "App Error")
                End If
            Else
                AppLogWarn("UPS Application File Not Found")
                MsgBox("UPS Application File Not Found", MsgBoxStyle.Critical, "App Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnUPS_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub btnRSI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRSI.Click
        Dim strAppPath As String = ""
        Try

            AppLogInfo("RSI Selected")

            strAppPath = udtMDSParam.RSIAppPath.Trim

            If strAppPath.Length > 0 Then
                If File.Exists(strAppPath) = True Then
                    Shell(strAppPath, AppWinStyle.MaximizedFocus)
                Else
                    AppLogWarn("RSI Application File Not Found")
                    MsgBox("RSI Application File Not Found", MsgBoxStyle.Critical, "App Error")
                End If
            Else
                AppLogWarn("RSI Application File Not Found")
                MsgBox("RSI Application File Not Found", MsgBoxStyle.Critical, "App Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnRSI_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub btnBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarCode.Click
        Dim strAppPath As String = ""
        Try

            AppLogInfo("Barcode Selected")
            
            strAppPath = udtMDSParam.BarcodeAppPath.Trim

            If strAppPath.Length > 0 Then
                If File.Exists(strAppPath) = True Then
                    Shell(strAppPath, AppWinStyle.MaximizedFocus)
                Else
                    AppLogWarn("Barcode Application File Not Found")
                    MsgBox("Barcode Application File Not Found", MsgBoxStyle.Critical, "App Error")
                End If
            Else
                AppLogWarn("Barcode Application File Not Found")
                MsgBox("Barcode Application File Not Found", MsgBoxStyle.Critical, "App Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnBarCode_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            AppLogInfo("MDS Hardware Control Panel - Exit")
            'Uload Me
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub


#End Region


#Region "System Maintenance and Control"

    Private Sub cmdSysMaint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSysMaint.Click
        Dim strUpdatePatchesPath As String = ""
        Try


            If MsgBox("Are you sure want to perform the update?", vbQuestion + vbYesNo, "Update Patches") = vbYes Then
                AppLogInfo("System Maintenance - Update Patches")

                'RUN The Update Pathes File
                'Browse or Directly run form DVD or CD

                If udtMDSParam.blnReadCDOpt = True Then
                    AppLogInfo("Read from CD option selected")
                    strUpdatePatchesPath = udtMDSParam.strCDDrive.Trim & udtMDSParam.strPatchesNm.Trim
                    strUpdatePatchesPath = strUpdatePatchesPath.Trim

                    If File.Exists(strUpdatePatchesPath) = True Then

                        'Exce the batches File
                        If ExcuteUpdatePatches(strUpdatePatchesPath) = True Then
                            strErrMsg = "Patches File Update Successfully."
                            AppLogInfo(strErrMsg)
                            MsgBox(strErrMsg, MsgBoxStyle.Information, "Update Patches File")
                        Else
                            strErrMsg = "Patches File Update Failed."
                            AppLogWarn(strErrMsg)
                            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Update Patches File Failed")
                        End If

                    Else
                        strErrMsg = "Update Patches File Not Found. FileNm=" & strUpdatePatchesPath
                        AppLogWarn(strErrMsg)
                        MsgBox(strErrMsg, MsgBoxStyle.Critical, "Patches File Not Found")
                    End If

                Else
                    AppLogInfo("Manual Browse the update patches")

                    strUpdatePatchesPath = ManualBrowseUpdatePatches()

                    If File.Exists(strUpdatePatchesPath) = True Then

                        'Exce the batches File
                        If ExcuteUpdatePatches(strUpdatePatchesPath) = True Then
                            strErrMsg = "Patches File Update Successfully."
                            AppLogInfo(strErrMsg)
                            MsgBox(strErrMsg, MsgBoxStyle.Information, "Update Patches File")
                        Else
                            strErrMsg = "Patches File Update Failed."
                            AppLogWarn(strErrMsg)
                            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Update Patches File Failed")
                        End If

                    Else
                        strErrMsg = "Update Patches File Not Found. FileNm=" & strUpdatePatchesPath
                        AppLogWarn(strErrMsg)
                        MsgBox(strErrMsg, MsgBoxStyle.Critical, "Patches File Not Found")
                    End If

                End If
            Else
                AppLogInfo("System Maintenance - Update Patches - Cancel")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdSysMaint_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub cmdRestartPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRestartPC.Click
        Try

            If MsgBox("Are you sure want to Restart PC?", vbQuestion + vbYesNo, "Restart PC") = vbYes Then
                'Restart PC
                AppLogInfo("Restart PC")

                'AppLogInfo("End Process")
                'EndProcessFunc(udtMDSParam.MDSControlPanelAppID, udtMDSParam.MDSControlPanelAppPath)

                'Application Terminated
                'Me.Close()

                'fore to restart PC
                If udtMDSParam.blnRestartPC = True Then
                    clsPCControl.Restart()
                    'AppLogInfo("Restart pc option is enable")
                Else
                    'AppLogInfo("Restart pc option is disable")
                End If

            Else
                AppLogInfo("Restart PC - Cancel")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdRestartPC_Click:" & ex.Message
            'AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

    Private Sub cmd3rdTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd3rdTools.Click
        Try
            AppLogInfo("Vendor Diagnostics Tools Menu")
            frmVendorToolsMenu.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmd3rdTools_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
        End Try
    End Sub

#End Region


#Region "Select Update Patches File"

    Private Function ManualBrowseUpdatePatches() As String
        Dim strTempFileNm As String = ""
        Dim strFileList As String = ""

        Try


            With opnFile
                .Title = "Please select the Update Patches File"
                .InitialDirectory = "E:\"
                .Filter = "Update Patches File (*.bat)|*.bat"
            End With

            If (opnFile.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                If (opnFile.FileNames.Length > 0) Then

                    strTempFileNm = opnFile.FileName

                    'For Each strTmp In opnFile.FileNames
                    '    strFileList += Path.GetFileName(strTmp) & ";"
                    'Next
                    'txtUPSStatusPath.Text = strFileList.Substring(0, strFileList.Length - 1)
                End If
            End If

            Return strTempFileNm
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".ManualBrowseUpdatePatches:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
            Return ""
        End Try
    End Function


#End Region

#Region "Excute the Update Patches File"

    Private Function ExcuteUpdatePatches(ByVal strUpdatePatchesFile As String) As Boolean
        Dim intReplyID As Integer
        Try
            strUpdatePatchesFile = strUpdatePatchesFile.Trim

            'Shell Function 
            intReplyID = Shell(strUpdatePatchesFile, AppWinStyle.NormalFocus, True, -1)

            AppLogInfo("Process Update Patches Reply ID:" & intReplyID)

            Return True

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".ExcuteUpdatePatches:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "Sys Error")
            Return False
        End Try
    End Function

#End Region

  
End Class
