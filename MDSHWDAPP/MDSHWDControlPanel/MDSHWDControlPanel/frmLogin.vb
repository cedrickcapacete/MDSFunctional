Imports System.IO

Public Class frmLogin


#Region "Local Variable"

    Dim strTitle As String = "frmLogin"

    Dim strLogIniPath As String = ""
    Dim strINAppLayerINIPath1 As String = ""
    Dim strINAppLayerINIPath2 As String = ""
    Dim strINAppLayerINIPath3 As String = ""
    Dim strINAppLayerINIPath4 As String = ""
    Dim strINAppLayerINIPath5 As String = ""
    Dim strINAppLayerINIPath6 As String = ""
    Dim strINAppLayerINIPath7 As String = ""
    Dim strINAppLayerINIPath8 As String = ""
    Dim strINAppLayerINIPath9 As String = ""
    Dim strINAppLayerINIPath10 As String = ""

#End Region

    Private Sub frmLogin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            CloseLog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmLogin_FormClosed:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Init Display
            InitDisplay()

            'Init the Logger
            If InitLoggerControl() = True Then
 
                'Load MDS Hardware App Path
                If LoadMDSHardwareAppPath(strINAppLayerINIPath2) = False Then
                    strErrMsg = "Read MDS Hardware App Failed "
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Hardware App")
                End If

                'Load the App Version
                If LoadAppVersionID(strINAppLayerINIPath3) = True Then
                    lblVersion.Text = udtMDSParam.APPVersionID.Trim
                Else
                    strErrMsg = "Read MDS App Version Failed "
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Application Version")
                End If

                'Load Login or Not
                If udtMDSParam.blnRequiredAppLogin = True Then
                    'Load MDS Application Login Password
                    If LoadAppLoginPassword(strINAppLayerINIPath4) = False Then
                        strErrMsg = "Read MDS App Login Password Failed "
                        AppLogWarn(strErrMsg)
                        MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Application Login Password")
                        Me.Close()
                    End If
                Else
                    AppLogWarn("Login Option is Disable")
                    AppLogInfo("== End Login Menu ==")
                    frmHWDMain.ShowDialog()
                    Me.Close()
                End If

            Else
                strErrMsg = "Init App Logger Failed "
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "Init Logger")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmLogin_Load:" & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub


#Region "Support Method"

    Private Sub InitDisplay()
        Try

            'Password
            txtPassword.MaxLength = 8
            txtPassword.Focus()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay:" & ex.Message
            AppLogWarn(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Private Function InitLoggerControl() As Boolean

        Try
            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(MAINAPPINIPATH) = True Then

                'Read INI File
                'Log Ini File
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strINAppLayerINIPath1 = .strAppLayerINIPath1.Trim
                    strINAppLayerINIPath2 = .strAppLayerINIPath2.Trim
                    strINAppLayerINIPath3 = .strAppLayerINIPath3.Trim
                    strINAppLayerINIPath4 = .strAppLayerINIPath4.Trim
                    'strINAppLayerINIPath5 = .strAppLayerINIPath5.Trim
                    'strINAppLayerINIPath6 = .strAppLayerINIPath6.Trim
                    'strINAppLayerINIPath7 = .strAppLayerINIPath7.Trim
                End With

                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger Ini Path:" & strLogIniPath)
                AppLogInfo("MDS Housekeeping Ini Path:" & strINAppLayerINIPath1)
                AppLogInfo("MDS HWD Ini Path:" & strINAppLayerINIPath2)
                AppLogInfo("MDS APP Version Path:" & strINAppLayerINIPath3)
                AppLogInfo("MDS APP Login Password Path:" & strINAppLayerINIPath4)


                'Layer Class
                strLogIniPath = strLogIniPath.Trim
                If File.Exists(strLogIniPath) = True Then

                    InitLog(strLogIniPath)
                    strLogEvt = "Logger Init Ok"
                    AppLogInfo(strLogEvt)

                    'Log File Housekeeping
                    strINAppLayerINIPath1 = strINAppLayerINIPath1.Trim
                    If File.Exists(strINAppLayerINIPath1) = True Then
                        strAppHSKCfgIniFile = strINAppLayerINIPath1.Trim
                        objAppHSKCfg.InitAppHSKCFGFile(strAppHSKCfgIniFile)
                        AutoHousekeepingLog()
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "User Command"

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        Try
            NumberPressed("1")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn1_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        Try
            NumberPressed("2")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn2_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        Try
            NumberPressed("3")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn3_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
        Try
            NumberPressed("4")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn4_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5.Click
        Try
            NumberPressed("5")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn5_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn6.Click
        Try
            NumberPressed("6")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn6_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn7.Click
        Try
            NumberPressed("7")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn7_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn8.Click
        Try
            NumberPressed("8")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn8_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn9.Click
        Try
            NumberPressed("9")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn9_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btn0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn0.Click
        Try
            NumberPressed("0")
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btn0_Click:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "User Control"


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            ClearKeyPressed()
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btnClear_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click
        Try
            EnterKeyPressed()
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btnEnter_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            'Unload Me
            Me.Close()
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".btnCancel_Click: " & ex.Message)
        End Try
    End Sub

#End Region


#Region "Keypad Support Method"

    Private Sub ClearKeyPressed()
        Try
            If txtPassword.Text.Length > 0 Then
                txtPassword.Text = ""
            End If
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".ClearKeyPressed:" & ex.Message)
        End Try
    End Sub


    Private Sub EnterKeyPressed()
        Dim strPassword As String = String.Empty

        Try

            strPassword = txtPassword.Text.Trim

            If strPassword = String.Empty Or strPassword.Length < 8 Then
                MsgBox("Invalid password length." & vbCrLf & "Please enter a valid password.", MsgBoxStyle.Critical, "Input Error")
                txtPassword.Text = ""
                txtPassword.Focus()
            Else
                'Password Validation
                If PasswordValidation(strPassword) = True Then
                    AppLogInfo("Login Successfully - Valid Password")
                    MsgBox("Login Successfully.", MsgBoxStyle.Information, "Login")
                    txtPassword.Text = ""
                    txtPassword.Focus()
                    frmHWDMain.ShowDialog()
                Else
                    AppLogWarn("Login Failed - Invalid Password")
                    MsgBox("Invalid password." & vbCrLf & "Please enter a valid password.", MsgBoxStyle.Critical, "Login Error")
                    txtPassword.Text = ""
                    txtPassword.Focus()
                End If
            End If

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".EnterKeyPressed:" & ex.Message)
        End Try
    End Sub


    Private Sub NumberPressed(ByVal num As String)
        Try

            If txtPassword.Text.Length < txtPassword.MaxLength Then
                txtPassword.Text = txtPassword.Text & num
            End If

            'If num.Length = 1 Then
            '    If txtPassword.Text.Length < txtPassword.MaxLength Then
            '        txtPassword.Text = txtPassword.Text & num
            '    End If
            'ElseIf num.Length = 2 Then
            '    If txtPassword.Text.Length < txtPassword.MaxLength Then
            '        txtPassword.Text = txtPassword.Text & num.Substring(0, 1)
            '        If txtPassword.Text.Length < txtPassword.MaxLength Then
            '            txtPassword.Text = txtPassword.Text & num.Substring(1, 1)
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".NumberPressed:" & ex.Message)
        End Try
    End Sub


#End Region

#Region "Password Validation"

    Private Function PasswordValidation(ByVal strInUserKeyInPassword As String) As Boolean
        Dim objPwsd As New clsEncryption
        Dim strEncypPwsd As String = ""

        Try

            'Input Password
            strInUserKeyInPassword = strInUserKeyInPassword.Trim

            'Debug
            'MsgBox("Password=" & strInUserKeyInPassword, MsgBoxStyle.Information, "Login Password")

            If strInUserKeyInPassword.Length > 0 Then

                'Encypted Password
                strEncypPwsd = objPwsd.Encrypt(strInUserKeyInPassword)

                'Validation
                If udtMDSParam.APPLoginPassword.Trim.Length > 0 Then
                    If udtMDSParam.APPLoginPassword = strEncypPwsd Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".PasswordValidation:" & ex.Message)
            Return False
        End Try
    End Function


#End Region


End Class