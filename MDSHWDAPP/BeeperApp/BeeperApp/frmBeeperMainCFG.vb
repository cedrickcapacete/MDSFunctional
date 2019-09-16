Imports System.IO
Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmBeeperMainCFG

#Region "Variable"

    Dim strTitle As String = "frmBeeperMainCFG"

    Dim strLogIniPath As String = ""
    Dim strAppModeIniFile As String = ""
    Dim strAppHSKCfgIniFile As String = ""
    Dim strTmpAppMode As String = ""

#End Region


    Private Sub frmBeeperMainCFG_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            CloseLog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmBeeperMainCFG_FormClosed. ErrInfo:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmBeeperMainCFG_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
            'Init Log
            InitBeeperMainAppLog()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmBeeperMainCFG_Load. ErrInfo:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "Support Method"

    Private Sub InitBeeperMainAppLog()

        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(BEEPERMAINAPPINIPATH, BEEPER) = True Then

                'Read INI File
                'Log Ini File
                '1.Cardreader Hardware
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strAppHSKCfgIniFile = .strAppLayerINIPath1.Trim
                    strAppModeIniFile = .strAppLayerINIPath2.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)

                'LOG ININ PATH
                AppLogInfo("Beeper Main App Init Ok")
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                AppLogInfo("App HSK INI Path:" & strAppHSKCfgIniFile.Trim)
                AppLogInfo("App Mode Path:" & strAppModeIniFile.Trim)


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

            Else
                strErrMsg = "Error in " & strTitle & ".InitMainAppLog - Init Layer Ini Failed"
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitBeeperMainAppLog. ErrInfo:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try

    End Sub



#End Region



#Region "User Command"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload Me
            AppLogInfo("Beeper Main App - Exit")
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub btnSystemMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSystemMode.Click
        Try
            AppLogInfo("Beeper System Maintenance Mode Selected")
            frmBeeperMain.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnSystemMode_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub btnDiagnostic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagnostic.Click
        Try
            AppLogInfo("Beeper Diagnostic Mode Selected - Engineer")
            frmMaintenanceMode.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnDiagnostic_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

End Class