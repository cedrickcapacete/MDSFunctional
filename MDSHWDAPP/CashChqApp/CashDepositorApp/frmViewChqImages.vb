Imports System.IO

Public Class frmViewChqImages

#Region "Variable"

    Dim strTitle As String = "frmViewChqImages"

#End Region


    Private Sub frmViewChqImages_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            AppLogInfo("== End View Cheque Images ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmViewChqImages_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmViewChqImages_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("== Start View Cheque Images ==")

            'Init Display
            InitDisplay()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmViewChqImages_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "Support Method"

    Private Sub InitDisplay()
        Try
            picChqImage.Image = Nothing
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay.ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "User Command"


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


#Region "Count Insert Cheque Item"

    Public Sub DepositChequeSummaryInfo(ByVal strChqData As String)
        Dim tmpArrChqData() As String = Nothing
        Dim intTtlCheque As Integer = 0
        Dim intPos As Integer = 0
        Dim tmpStrChqData As String = ""
        Dim intItem As Integer

        Dim tmpMICR As String = ""
        Dim tmpImgFileNm As String = ""

        Dim objItem As ListViewItem

        Try

            intItem = 1
            lstChqView.Items.Clear()

            AppLogInfo("Deposit Cheque SummaryInfo:" & strChqData)
            'MsgBox("Complete Deposit Cheque Info:" & strChqData)

            strChqData = strChqData.Trim

            If strChqData.Length > 0 Then

                tmpArrChqData = Split(strChqData, "|", -1, CompareMethod.Text)

                For i = 0 To tmpArrChqData.Length - 1

                    If tmpArrChqData(i).Trim <> String.Empty Then

                        tmpStrChqData = tmpArrChqData(i)

                        intPos = tmpStrChqData.IndexOf(",")

                        'MICR and Image File Name
                        tmpMICR = tmpStrChqData.Substring(0, intPos)
                        tmpImgFileNm = tmpStrChqData.Substring(intPos + 1)

                        tmpMICR = tmpMICR.Trim
                        tmpImgFileNm = tmpImgFileNm.Trim

                        AppLogInfo("Chq Item:" & intItem & "-MICR:" & tmpMICR & " FileNm:" & tmpImgFileNm)

                        'Add to List Viewer
                        objItem = Me.lstChqView.Items.Add(intItem)
                        With objItem
                            .SubItems.Add(tmpMICR)
                            .SubItems.Add(tmpImgFileNm)
                        End With

                        intTtlCheque = intTtlCheque + 1
                        intItem = intItem + 1

                    End If
                Next

                lblTtlChq.Text = intTtlCheque
            Else
                AppLogInfo("Deposit Cheque Info Empty")
                lblTtlChq.Text = "0"
            End If

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".DepositChequeSummaryInfo:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Chq Images Selection"

    Private Sub lstChqView_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lstChqView.ItemSelectionChanged
        Dim strChqImagePath As String = ""
        Dim strEvtMsg As String = ""

        Try

            picChqImage.Image = Nothing

            strChqImagePath = e.Item.SubItems(2).Text 'lstChqView.SelectedItems.Item(lstChqView.FocusedItem.Index).SubItems(2).Text
            strChqImagePath = strChqImagePath.Trim

            If strChqImagePath.Length > 0 Then
                If File.Exists(strChqImagePath) = True Then
                    picChqImage.Image = Image.FromFile(strChqImagePath)
                    strEvtMsg = "Cheque Image Path Found. Path=" & strChqImagePath
                    AppLogWarn(strEvtMsg)
                Else
                    picChqImage.Image = Nothing
                    strEvtMsg = "Cheque Image Path Not Found. Path=" & strChqImagePath
                    AppLogWarn(strEvtMsg)
                    MsgBox(strEvtMsg, MsgBoxStyle.Critical, "View Cheque Images")
                End If
            Else
                AppLogWarn("Empty Cheque Image Path")
                MsgBox("Empty Cheque Image Path", MsgBoxStyle.Critical, "Invalid Input")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".lstChqView_ItemSelectionChanged. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try


    End Sub


#End Region


End Class