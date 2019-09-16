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
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMainCFG_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
            'Init Log
            InitRSIMainAppLog()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmBeeperMainCFG_Load:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "Support Method"

    Private Sub InitRSIMainAppLog()
      
        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(RSIMAINAPPINIPATH, RSI) = True Then

                'Read INI File
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strAppHSKCfgIniFile = .strAppLayerINIPath1.Trim
                    strAppModeIniFile = .strAppLayerINIPath2.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)

                AppLogInfo("RSI Main App Init")
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
            strErrMsg = "Error in " & strTitle & ".InitRSIMainAppLog:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try

    End Sub

#End Region


#Region "User Command"


    Private Sub btnSystemMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSystemMode.Click
        Try
            AppLogInfo("RSI System Maintenance Mode Selected")
            frmRSIMain.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ". btnSystemMode_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnDiagnostic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagnostic.Click
        Try
            AppLogInfo("RSI Diagnostic Mode Selected - Engineer")
            frmMaintenanceMode.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ". btnDiagnostic_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload Me
            AppLogInfo("RSI Main Menu - Exit")
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ". btnExit_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub



#End Region


End Class