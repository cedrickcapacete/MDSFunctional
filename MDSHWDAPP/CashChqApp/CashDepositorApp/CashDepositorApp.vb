Imports CashDepositorHWD


Public Class CashDepositorApp

    Dim WithEvents objCashDepositor As New clsCashDepositorHWDInterface

#Region "Variable"

    Public strEvtTxtMsg As String = String.Empty
    Public strEvtMsg As String = String.Empty
    Public strErrMsg As String = String.Empty

#End Region


    Private Sub CashDepositorApp_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try
            'Exit Cash Deposit
            AppLogInfo("Exit - Cash Modules")
        Catch ex As Exception
            CashDepositorInfo("Err in CashDepositorApp_FormClosed. ErrInfo:" & ex.Message)
        End Try

    End Sub

    Private Sub CashDepositorApp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AppLogInfo("Cash Modules Loaded")
            InitDisplay()

        Catch ex As Exception
            CashDepositorInfo("Err in CashDepositorApp_Load. ErrInfo:" & ex.Message)
        End Try
    End Sub

#Region "Form Function"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            CashDepositorInfo("Err in InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub CashDepositorInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            CashDepositorInfo("Err in InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Form Event Info"

    Private Sub UpdateEvtInfo()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf UpdateEvtInfo))
            Else
                strEvtMsg = strEvtMsg.Trim
                If strEvtMsg.Length > 0 Then
                    EvtEventsMsg(strEvtMsg)
                End If
            End If

        Catch ex As Exception
            MsgBox("Error in Cash-UpdateEvtInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in Cash-EvtEventsMsg:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region

#Region "Get the Property"


    Private Sub btnGetDevProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDevProp.Click
        Try
            strEvtTxtMsg = ""

            strEvtMsg = "Txn Status:" & objCashDepositor.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objCashDepositor.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objCashDepositor.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objCashDepositor.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objCashDepositor.MStatusData
            UpdateEvtInfo()

        Catch ex As Exception
            CashDepositorInfo("Error in Cash-btnGetDevProp_Click:" & ex.Message)
        End Try
    End Sub

#End Region



#Region "CASH Command"

    Private Sub btnStartDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartDeviceNDC.Click
        Try
            If objCashDepositor.StartDevice Then
                CashDepositorInfo("Cash-StartDevice Success")
            Else
                CashDepositorInfo("Cash-StartDevice Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-StartDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnStopDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopDeviceNDC.Click
        Try
            If objCashDepositor.StopDevice Then
                CashDepositorInfo("Cash-StopDevice Success")
            Else
                CashDepositorInfo("Cash-StopDevice Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-StopDevice: " & ex.Message)
        End Try
    End Sub


    Private Sub btnUnlockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlockDeviceNDC.Click
        Try
            If objCashDepositor.UnlockDevice Then
                CashDepositorInfo("Cash-UnlockDevice Success")
            Else
                CashDepositorInfo("Cash-UnlockDevice Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-UnlockDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnLockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLockDeviceNDC.Click
        Try
            If objCashDepositor.LockDevice Then
                CashDepositorInfo("Cash-LockDevice Success")
            Else
                CashDepositorInfo("Cash-LockDevice Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-LockDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnWrapDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWrapDeviceNDC.Click
        Try
            If objCashDepositor.WrapDevice Then
                CashDepositorInfo("Cash-WrapDevice Success")
            Else
                CashDepositorInfo("Cash-WrapDevice Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-WrapDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDiagDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagDeviceNDC.Click
        Try
            If objCashDepositor.DiagnosticDevice Then
                CashDepositorInfo("Cash-Dignotis Success")
            Else
                CashDepositorInfo("Cash-Dignotis Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-Dignotis: " & ex.Message)
        End Try
    End Sub

    Private Sub btnWakeDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWakeDeviceNDC.Click
        Dim intState As Integer = 0
        Dim strState As String = String.Empty

        Try

            strState = Mid(cboWakeupState.Text, 1, 1).Trim

            If strState = "A" Then
                strState = "10"
            End If

            intState = CInt(strState)

            If objCashDepositor.WakeUpDevice(intState) Then
                CashDepositorInfo("Cash-WakeUpDevice:" & cboWakeupState.Text & " Success")
            Else
                CashDepositorInfo("Cash-WakeUpDevice:" & cboWakeupState.Text & " Failed")
            End If

        Catch ex As Exception
            CashDepositorInfo("Error in Cash-WakeUpDevice: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAmount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmount.Click
        Dim dblAmount As Double
        Try
            dblAmount = objCashDepositor.Amount
            txtAmount.Text = dblAmount.ToString("0.00")
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-Get Amount: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-btnClearText_Click:" & ex.Message)
            'MsgBox("Error in btnClearText_Click:" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Cash Events"


    Private Sub objCashDepositor_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objCashDepositor.EvtDeviceDataReady

        Try

            strEvtMsg = "Cash-Device Data Ready State:" & e.iDeviceState & " DataValue:" & e.mDeviceDataValue & " Trace:" & e.iDeviceTrace
            UpdateEvtInfo()

            Select Case CInt(e.iDeviceState)
                Case 0
                    strEvtMsg = "Current State: Closed"
                Case 1
                    strEvtMsg = "Current State: Open & Prepare"
                Case 2
                    strEvtMsg = "Current State: Insert More"
                Case 3
                    strEvtMsg = "Current State: Rejected Item Detected"
                Case 4
                    strEvtMsg = "Current State: Refunded"
                Case 5
                    strEvtMsg = "Current State: Escrowed"
                Case 6
                    strEvtMsg = "Current State: Processing"
                Case 7
                    strEvtMsg = "Current State: Confirm Transaction"
                Case 8
                    strEvtMsg = "Current State: Trans Complete"

                Case 11
                    strEvtMsg = "Current State: Deposit Item Full"

                Case 9
                    strEvtMsg = "Supervisor - Cash Box Counter"
                    CashBoxItem(e.mDeviceDataValue)


                    'For i = 0 To udtCashBoxNoteList.ListOfBox.Length - 1
                    '    If Not IsNothing(udtCashBoxNoteList.ListOfBox(i).NoteInfo) Then
                    '        For x = 0 To udtCashBoxNoteList.ListOfBox(i).NoteInfo.Length - 1
                    '            If udtCashBoxNoteList.ListOfBox(i).NoteInfo(x).bGoodNote = True Then
                    '                intCountPcs = udtCashBoxNoteList.ListOfBox(i).NoteInfo(x).NoteCount
                    '                intCurrencyVal = udtCashBoxNoteList.ListOfBox(i).NoteInfo(x).NoteValue
                    '                dblTmpDepositAmt = intCurrencyVal * intCountPcs
                    '                strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & Chr(28) & intCountPcs.ToString & Chr(29)
                    '                TotalAmount = TotalAmount + dblTmpDepositAmt
                    '            End If
                    '        Next
                    '    End If
                    'Next


            End Select

            UpdateEvtInfo()

        Catch ex As Exception
            CashDepositorInfo("Error in objCashDepositor_EvtDeviceDataReady:" & ex.Message)
        End Try
    End Sub

    Private Sub objCashDepositor_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objCashDepositor.EvtDeviceError
        Try

            strEvtMsg = "MDS Ping Status =" & objCashDepositor.MDSPingFailed
            UpdateEvtInfo()

            strEvtMsg = "Cash-Device Data Error"
            UpdateEvtInfo()

        Catch ex As Exception
            CashDepositorInfo("Error in objCashDepositor_EvtDeviceError:" & ex.Message)
        End Try
    End Sub

    Private Sub objCashDepositor_EvtDeviceTimeOut(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objCashDepositor.EvtDeviceTimeout
        Try
            strEvtMsg = "Cash-Device Data Timeout"
            UpdateEvtInfo()
        Catch ex As Exception
            CashDepositorInfo("Error in objCashDepositor_EvtDeviceTimeOut:" & ex.Message)
        End Try
    End Sub

    Private Sub btnGetCashCounter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCashCounter.Click
        Try
            objCashDepositor.GetCashCounter()
        Catch ex As Exception
            CashDepositorInfo("Error in btnGetCashCounter_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnResetCashCnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetCashCnt.Click
        Try
            objCashDepositor.ResetCashCounter()
        Catch ex As Exception
            CashDepositorInfo("Error in btnResetCashCnt_Click:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Support Mehtod"

    Private Sub CashBoxItem(ByVal CashItem As String)
        Dim tmpArrCashData() As String = Nothing
        Dim tmpStrCashData As String = ""
        Dim tmpDenoType As String = ""
        Dim tmpDenoCnt As String = ""

        Dim intPos As Integer = 0
        Dim dblTotalAmt As Double = 0.0
        Dim intItemCount As Integer

        Dim i As Integer

        Try

            strEvtMsg = "CashBoxItem Value:" & CashItem
            UpdateEvtInfo()

            tmpArrCashData = Split(CashItem, "|", -1, CompareMethod.Text)

            intItemCount = tmpArrCashData.Count
            strEvtMsg = "Count:" & intItemCount
            UpdateEvtInfo()

            If intItemCount > 0 Then

                For i = 0 To intItemCount - 1

                    tmpStrCashData = tmpArrCashData(i).Trim
                    tmpStrCashData = tmpStrCashData.Trim

                    strEvtMsg = "Item:" & i & " Value:" & tmpStrCashData & " strLen:" & tmpStrCashData.Length
                    UpdateEvtInfo()
                    strEvtMsg = ""

                    If tmpStrCashData.Length > 0 Then
                        intPos = tmpStrCashData.IndexOf(",")
                        tmpDenoType = tmpStrCashData.Substring(0, intPos)
                        tmpDenoCnt = tmpStrCashData.Substring(intPos + 1)
                        strEvtMsg = "Item" & i & ":" & tmpDenoType & "(" & tmpDenoCnt & ")"
                        UpdateEvtInfo()
                        strEvtMsg = ""
                    End If

                    i = i + 1

                Next
            Else
                strEvtMsg = "Empty Cash Box"
                UpdateEvtInfo()
            End If

        Catch ex As Exception
            'CashDepositorInfo("Error in CashBoxItem:" & ex.Message)
        End Try
    End Sub


#End Region


#Region "Light Control Command"

    Private Sub cmdStartMDSCRLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartMDSCRLight.Click
        Dim strChoice As String = "0"
        Try

            strChoice = Mid(cboCRLightChoice.Text, 1, 1).Trim

            If objCashDepositor.StartMDSReaderLight(strChoice) = True Then
                CashDepositorInfo("Cash-Start MDS Reader Light Success LightOption:" & strChoice)
            Else
                CashDepositorInfo("Cash-Start MDS Reader Light Failed LightOption:" & strChoice)
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-StartMDSReaderLight: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdStopMDSCRLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopMDSCRLight.Click
        Try
            If objCashDepositor.StopMDSReaderLight Then
                CashDepositorInfo("Cash-Stop MDS Reader Light Success")
            Else
                CashDepositorInfo("Cash-Stop MDS Reader Light Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-StopMDSReaderLight: " & ex.Message)
        End Try
    End Sub
  

    Private Sub cmdStartMDSLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartMDSLight.Click
        Dim strChoice As String = "0"

        Try

            strChoice = Mid(cboLightChoice.Text, 1, 1).Trim

            If objCashDepositor.StartMDSLight(strChoice) Then
                CashDepositorInfo("Cash-Start MDS Light Success LightOption:" & strChoice)
            Else
                CashDepositorInfo("Cash-Start MDS Light Failed LightOption:" & strChoice)
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-StartMDSLight: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdStopMDSLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopMDSLight.Click
        Try
            If objCashDepositor.StopMDSLight Then
                CashDepositorInfo("Cash-Stop MDS Light Success")
            Else
                CashDepositorInfo("Cash-Stop MDS Light Failed")
            End If
        Catch ex As Exception
            CashDepositorInfo("Error in Cash-StopMDSLight: " & ex.Message)
        End Try
    End Sub

   

#End Region

  
    Private Sub cmdGetMDSStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetMDSStatus.Click

        Try

            strEvtMsg = "MDS Status:" & objCashDepositor.MDSReplyStatus
            UpdateEvtInfo()
            strEvtMsg = ""

            strEvtMsg = "MDS Status Reason:" & objCashDepositor.MDSReplyStatusReason
            UpdateEvtInfo()
            strEvtMsg = ""

            strEvtMsg = "MDS User Status:" & objCashDepositor.MDSUserStatus
            UpdateEvtInfo()
            strEvtMsg = ""


            strEvtMsg = "MDS Status In Details:" & objCashDepositor.MDSReplyStatusDetails
            UpdateEvtInfo()
            strEvtMsg = ""


            'Logical Units 
            strEvtMsg = "MDS Notes In Box Count:" & objCashDepositor.MDSNoteInAllBoxCount
            UpdateEvtInfo()
            strEvtMsg = ""

            strEvtMsg = "MDS Retract Notes Count:" & objCashDepositor.MDSRetractBoxNoteCount
            UpdateEvtInfo()
            strEvtMsg = ""

            strEvtMsg = "MDS Counterfeit Notes Count:" & objCashDepositor.MDSConterfeitBoxNoteCount
            UpdateEvtInfo()
            strEvtMsg = ""



        Catch ex As Exception
            CashDepositorInfo("Err in cmdGetMDSStatus_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdGetNoteInBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetNoteInBox.Click
        Dim strInLogicalBox As String = "0"
        Try

            strInLogicalBox = cboLogicalBox.Text

            strEvtMsg = "Logical Box -" & strInLogicalBox & ":" & objCashDepositor.GetNoteInBox(strInLogicalBox)
            UpdateEvtInfo()
            strEvtMsg = ""

        Catch ex As Exception
            CashDepositorInfo("Err in cmdGetNoteInBox_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdClearPaperPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearPaperPath.Click
        Try

            objCashDepositor.MDSReset()

        Catch ex As Exception
            CashDepositorInfo("Err in cmdClearPaperPath_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            CashDepositorInfo("Err in cmdExit_Click. ErrInfo:" & ex.Message)
        End Try
    End Sub
End Class
