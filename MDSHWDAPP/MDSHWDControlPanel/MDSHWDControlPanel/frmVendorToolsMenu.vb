Imports System
Imports System.IO

Public Class frmVendorToolsMenu


#Region "Variable"

    Dim strTitle As String = "frmVendorToolsMenu"

#End Region

    Private Sub frmVendorToolsMenu_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            AppLogInfo("== End Vendor Diagnostics Tools Menu == ")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmVendorToolsMenu_FormClosed:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Private Sub frmVendorToolsMenu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("== Start Vendor Diagnostics Tools Menu == ")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmVendorToolsMenu_Load:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

#Region "User Command"


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unlod Me
            AppLogInfo("Vendor Diagnostics Tools Menu - Exit")
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Private Sub btnRotoMDSTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotoMDSTools.Click
        Dim strAppPath As String = ""
        Try
            AppLogInfo("Rototype MDSTools Selected")

            strAppPath = udtMDSParam.RotoMDSToolsPath
            strAppPath = strAppPath.Trim

            If File.Exists(strAppPath) = True Then

                'Exce the batches File
                If ExcuteUpdatePatches(strAppPath) = True Then
                    strErrMsg = "Run the Rototype MDSTools Successfully."
                    AppLogInfo(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Information, "Run Application File")
                Else
                    strErrMsg = "Run the Rototype MDSTools Failed."
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "Run Application File Failed")
                End If

            Else
                strErrMsg = "Rototype MDSTools File Not Found. FileNm=" & strAppPath
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "Application File Not Found")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnRotoMDSTools_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

    Private Sub btnRotoWrapper_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotoWrapper.Click
        Dim strAppPath As String = ""
        Try
            AppLogInfo("Rototype Wrapper Selected")

            strAppPath = udtMDSParam.RotoWrapperPath
            strAppPath = strAppPath.Trim

            If File.Exists(strAppPath) = True Then

                'Exce the batches File
                If ExcuteUpdatePatches(strAppPath) = True Then
                    strErrMsg = "Run the Rototype Wrapper Successfully."
                    AppLogInfo(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Information, "Run Application File")
                Else
                    strErrMsg = "Run the Rototype Wrapper Failed."
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "Run Application File Failed")
                End If

            Else
                strErrMsg = "Rototype Wrapper File Not Found. FileNm=" & strAppPath
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "Application File Not Found")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnRotoWrapper_Click:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

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