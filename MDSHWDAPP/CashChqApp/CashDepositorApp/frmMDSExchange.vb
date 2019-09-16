

Public Class frmMDSExchange

#Region "Variable"

    Dim strTitle As String = "frmMDSExchange"
    Dim blnStarDevice As Boolean = False

    Dim WithEvents objCashTemp As CashDepositorHWD.clsCashDepositorHWDInterface

    Public intMDSExchangeMode As Integer

#End Region


    Private Sub frmMDSExchange_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            objCashTemp = Nothing
            AppLogInfo("== End MDS Exchange Menu ==")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMDSExchange_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMDSExchange_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("== Start MDS Exchange Menu ==")

            'Init Display
            InitDisplay()

            objCashTemp = frmMaintenanceMode.objCashDepositor

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMDSExchange_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "Support Method"

    Private Sub InitDisplay()
        Try
            intMDSExchangeMode = 0

            lblTtlChq.Text = "0"

            lblMDSResetMsg.Text = ""

            'List Viewer
            lstView.Clear()

            With lstView.Columns
                .Add("Physical Box", 150, HorizontalAlignment.Left)
                .Add("Value", 120, HorizontalAlignment.Left)
                .Add("Currency", 110, HorizontalAlignment.Left)
                .Add("Number", 150, HorizontalAlignment.Left)
            End With

            'lvwIcon is replaced by LargeIcon, lvwSmallIcon by SmallIcon, lvwList by List, and lvwReport by Details. 
            lstView.View = View.Details
            lstView.FullRowSelect = True
            lstView.GridLines = True
            lstView.MultiSelect = True



        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "User Command"


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdEmptyChq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEmptyChq.Click
        Dim strMsg As String = ""
        Dim vAns As MsgBoxResult
        Dim strMDSStatus As String = ""

        Try
            'Empty Chq
            'intMDSExchangeMode = 2
            intMDSExchangeMode = 4
            lblMDSResetMsg.Text = ""

            'End Exchange
            strMsg = "Are You Confirm Want To Clear Local Cheque Total ?" & vbCrLf & " ==== IMPORTANT NOTE ==== " & vbCrLf & "1.Empty the cheque in cheque box." & vbCrLf & " ** Now MDS is ready for Cheque-Units Exchange ** "

            vAns = MsgBox(strMsg, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation, "Clear Local Cheque Total")

            If (vAns = MsgBoxResult.Ok) Then
                lblMDSResetMsg.Text = "MDS Reset Cheque Box Counter in progress .... Please Wait"
                'frmMaintenanceMode.objCashDepositor.SetMDSTOChequeMode()
                frmMaintenanceMode.objCashDepositor.ResetChequeCounter()
            Else
                AppLogInfo("End Cheque Box Exchange - Cancel")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdEmptyChq_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdEndExchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEndExchange.Click
        Dim strMsg As String = ""
        Dim vAns As MsgBoxResult

        Try


            intMDSExchangeMode = 3

            lblMDSResetMsg.Text = ""

            'End Exchange
            strMsg = "Are You Confirm Want To Clear Local Cash Total ?" & vbCrLf & " ==== IMPORTANT NOTE ==== " & vbCrLf & "1.Open the Safebox, and Empty all the Cassette." & vbCrLf & " ** Now MDS is ready for Cash-Units Exchange ** "

            vAns = MsgBox(strMsg, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation, "Clear Local Cash Total")

            If (vAns = MsgBoxResult.Ok) Then
                lblMDSResetMsg.Text = "MDS Reset Cash Box Counter in progress .... Please Wait"
                frmMaintenanceMode.objCashDepositor.ResetCashCounter()
            Else
                AppLogInfo("End Cash Box Exchange - Cancel")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdEndExchange_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdStartExchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartExchange.Click
        Try

            lblMDSResetMsg.Text = ""
            lstView.Items.Clear()
            lblTtlChq.Text = "0"

            intMDSExchangeMode = 1
            lblMDSResetMsg.Text = "MDS set to Cash Mode in progress .... Please Wait"
            frmMaintenanceMode.objCashDepositor.SetMDSTOCashMode()

            ''Start Exchange - CASH
            'If frmMaintenanceMode.objCashDepositor.GetCashCounter = True Then
            '    strErrMsg = "Get Cash Box Counter Successfully"
            '    AppLogInfo(strErrMsg)
            '    MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Cash Box Counter")
            'Else
            '    strErrMsg = "Get Cash Box Counter Failed"
            '    AppLogWarn(strErrMsg)
            '    MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Cash Box Counter")
            'End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdStartExchange_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdStartChqExchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartChqExchange.Click
        Try

            intMDSExchangeMode = 0
            lblMDSResetMsg.Text = ""

            'intMDSExchangeMode = 2
            'lblMDSResetMsg.Text = "MDS set to Cheque Mode in progress .... Please Wait"
            'frmMaintenanceMode.objCashDepositor.SetMDSTOChequeMode()

            'Start Exchange - Cheques
            lblTtlChq.Text = frmMaintenanceMode.objChqDepositor.MDSChequeInBoxCounter
            AppLogInfo("Total Cheque in Box=" & lblTtlChq.Text)
            strErrMsg = "Get Cheque Box Counter Successfully"
            AppLogInfo(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Cheque Box Counter")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdStartChqExchange_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#End Region



#Region "Count Insert Cash Item"

    Public Sub CashBoxSummary(ByVal strCashData As String)
        Dim tmpStrCashData As String = ""
        Dim tmpArrCashData() As String = Nothing

        Dim tmpDenoType As String = ""
        Dim tmpDenoCnt As String = ""
        Dim tmpLogicalBox As String = ""
        Dim tmpCurrencyType As String = ""

        Dim intPos As Integer = 0
        Dim dblTotalAmt As Double = 0.0

        Dim tmpArryInfo() As String = Nothing

        Dim objItem As ListViewItem

        Try

            'Clear Item
            lstView.Items.Clear()

            AppLogInfo("CountInsertedCash CashData:" & strCashData)

            strCashData = strCashData.Trim

            If strCashData.Length > 0 Then

                tmpArrCashData = Split(strCashData, "|", -1, CompareMethod.Text)

                For i = 0 To tmpArrCashData.Length - 1

                    If tmpArrCashData(i).Trim <> String.Empty Then

                        tmpStrCashData = tmpArrCashData(i)

                        'intPos = tmpStrCashData.IndexOf(",")

                        tmpArryInfo = Split(tmpStrCashData, ",", -1, CompareMethod.Text)

                        tmpDenoType = tmpArryInfo(0) 'tmpStrCashData.Substring(0, intPos)
                        tmpDenoCnt = tmpArryInfo(1) 'tmpStrCashData.Substring(intPos + 1)
                        tmpCurrencyType = tmpArryInfo(2)
                        tmpLogicalBox = tmpArryInfo(3)

                        'Add to List Viewer
                        objItem = lstView.Items.Add(tmpDenoType)
                        With objItem
                            .SubItems.Add(tmpDenoCnt)
                            .SubItems.Add(tmpCurrencyType)
                            .SubItems.Add("Note Box # " & tmpLogicalBox)
                        End With

                    End If
                Next

            Else
                AppLogInfo("Insert Cash Value Empty")
            End If

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".CountInsertedCash:" & ex.Message)
        End Try
    End Sub


#End Region

    Private Sub objCashTemp_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objCashTemp.EvtDeviceDataReady
        Dim strMsg As String = ""
        Dim vAns As MsgBoxResult

        Try

            lblMDSResetMsg.Text = ""

            If intMDSExchangeMode = 1 Then

                intMDSExchangeMode = 5

                'Start Exchange - CASH
                If frmMaintenanceMode.objCashDepositor.GetCashCounter = True Then
                    strErrMsg = "Get Cash Box Counter Successfully"
                    AppLogInfo(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Cash Box Counter")
                Else
                    strErrMsg = "Get Cash Box Counter Failed"
                    AppLogWarn(strErrMsg)
                    MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Cash Box Counter")
                End If

                'strMsg = "Are You Confirm Want To Clear Local Cash Total ?"

                'vAns = MsgBox(strMsg, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation, "Clear Local Cash Total")

                'If (vAns = MsgBoxResult.Ok) Then
                '    intMDSExchangeMode = 3
                '    frmMaintenanceMode.objCashDepositor.ResetCashCounter()
                'Else
                '    AppLogInfo("End Cash Box Exchange - Cancel")
                'End If

            ElseIf intMDSExchangeMode = 5 Then
                intMDSExchangeMode = 0
                'Get the Cash Box Counter
                CashBoxSummary(e.mDeviceDataValue)
            ElseIf intMDSExchangeMode = 3 Then
                strErrMsg = "Reset Cash Box Counter Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Reset Cash Box Counter")
                lblMDSResetMsg.Text = ""
            ElseIf intMDSExchangeMode = 2 Then
                'End Exchange
                strMsg = "Are You Confirm Want To Clear Local Cheque Total ?"
                vAns = MsgBox(strMsg, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation, "Clear Local Cheque Total")
                If (vAns = MsgBoxResult.Ok) Then
                    'Reset Cheque Conter
                    intMDSExchangeMode = 4
                    frmMaintenanceMode.objCashDepositor.ResetChequeCounter()
                Else
                    AppLogInfo("End Cheque Box Exchange - Cancel")
                End If
                'intMDSExchangeMode = 0
                ''Start Exchange - Cheques
                'lblTtlChq.Text = frmMaintenanceMode.objChqDepositor.MDSChequeInBoxCounter
                'AppLogInfo("Total Cheque in Box=" & lblTtlChq.Text)
                'strErrMsg = "Get Cheque Box Counter Successfully"
                'AppLogInfo(strErrMsg)
                'MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Cheque Box Counter")
            ElseIf intMDSExchangeMode = 4 Then
                strErrMsg = "Reset Cheque Box Counter Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Reset Cheque Box Counter")
                'Start Exchange - Cheques
                lblTtlChq.Text = frmMaintenanceMode.objChqDepositor.MDSChequeInBoxCounter
                AppLogInfo("Total Cheque in Box After Reset=" & lblTtlChq.Text)
                lblMDSResetMsg.Text = ""
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objCashTemp_EvtDeviceDataReady. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objCashTemp_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objCashTemp.EvtDeviceError
        Dim strMDSStatus As String = ""

        Try

            strMDSStatus = frmMaintenanceMode.objCashDepositor.MDSReplyStatusReason

            If intMDSExchangeMode = 1 Or intMDSExchangeMode = 3 Then
                strErrMsg = "Reset Cash Box Counter Failed. MDSStatus:" & strMDSStatus
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Reset Cash Box Counter")
                lblMDSResetMsg.Text = "Reset Failed.MDSStatus:" & strMDSStatus
            ElseIf intMDSExchangeMode = 2 Or intMDSExchangeMode = 4 Then
                strErrMsg = "Reset Cheque Box Counter Failed. MDSStatus:" & strMDSStatus
                AppLogWarn(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Reset Cheque Box Counter")
                lblMDSResetMsg.Text = "Reset Failed.MDSStatus:" & strMDSStatus
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objCashTemp_EvtDeviceError. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

End Class