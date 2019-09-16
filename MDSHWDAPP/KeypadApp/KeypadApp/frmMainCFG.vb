Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmMainCFG


#Region "Variable"

    Dim strTitle As String = "frmMainCFG"

    Dim strLogIniPath As String = ""
    Dim strAppModeIniFile As String = ""
    Dim strAppHSKCfgIniFile As String = ""
    Dim strTmpAppMode As String = ""

#End Region

    Private Sub frmMainCFG_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            CloseLog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMainCFG_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMainCFG_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Init Log
            InitMainAppLog()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmBeeperMainCFG_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "Support Method"

    Private Sub InitMainAppLog()

        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(MAINAPPINIPATH, EPP) = True Then

                'Read INI File
                'Log Ini File
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strAppHSKCfgIniFile = .strAppLayerINIPath1.Trim
                    strAppModeIniFile = .strAppLayerINIPath2.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "EPP Main App Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                AppLogInfo("App HSK INI Path:" & strAppHSKCfgIniFile.Trim)
                AppLogInfo("App Mode Path:" & strAppModeIniFile.Trim)

            End If

            'Log File Housekeeping
            objAppHSKCfg.InitAppHSKCFGFile(strAppHSKCfgIniFile)
            AutoHousekeepingLog()

            'App Mode
            strTmpAppMode = ReadAPPMode(strAppModeIniFile)
            strTmpAppMode = strTmpAppMode.Trim

            If strTmpAppMode.Length > 0 Then
                If strTmpAppMode <> "1" Then
                    btnSystemMode.Enabled = True
                Else
                    btnSystemMode.Enabled = False
                End If
            Else
                btnSystemMode.Enabled = False
            End If

        Catch ex As Exception
            MsgBox("Error in InitMainAppLog. ErrInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub


#End Region


#Region "User Command"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload Me
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ". btnExit_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnSystemMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSystemMode.Click
        Try
            AppLogInfo("Keypad System Maintenance Mode Selected")
            frmEPPMain.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ". btnSystemMode_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnDiagnostic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagnostic.Click
        Try
            AppLogInfo("Keypad Diagnostic Mode Selected - Engineer")
            frmMaintenanceMode.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ". btnDiagnostic_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class