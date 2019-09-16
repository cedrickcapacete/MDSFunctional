Imports CardReaderHWD
Imports clsCardReader.clsCardReaderControl
Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class CardReaderMainApp

#Region "ReadMe-Info"

    'Manuafacture: Sankyo
    'Card Model: Sankyo ICT3Q8_0171

#End Region

#Region "Form - Variable"
    Dim strEvtTxtMsg As String = String.Empty
    Dim strEvtMsg As String = String.Empty
    Dim strErrMsg As String

    Dim blnStarDevice As Boolean = False

#End Region

#Region "CardReader Object"

    Public WithEvents objCardReaderInterface As clsCardReaderHWDInterface

#End Region


    Private Sub CardReaderMainApp_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStarDevice = True Then
                objCardReaderInterface.StopDevice()
            End If

            objCardReaderInterface = Nothing

            AppLogInfo("== End Card Reader System Mode ==")
        Catch ex As Exception
            CardReaderInfo("Error in CardReaderMainApp_FormClosed:" & ex.Message)
        End Try
    End Sub

    Private Sub CardReaderMainApp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start Card Reader System Mode ==")

            'Init the Object - Card Reader
            objCardReaderInterface = New clsCardReaderHWDInterface

            'Init Display
            InitDisplay()

            blnStarDevice = False
            
        Catch ex As Exception
            CardReaderInfo("Error in CardReaderMainApp_Load:" & ex.Message)
        End Try
    End Sub

#Region "InitDisplay"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""
            txttmID.Text = ""
            txtInterval.Text = ""

        Catch ex As Exception
            CardReaderInfo("Err in InitDisplay. ErrInfo:" & ex.Message)
        End Try
    End Sub


#End Region


#Region "Events Display"

    Public Sub CardReaderInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            CardReaderInfo("Err in CardReaderInfo. ErrInfo:" & ex.Message)
        End Try
    End Sub

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
            MsgBox("Error in UpdateEvtInfo:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            MsgBox("Error in EvtEventsMsg:" & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


#End Region

#Region "Card Reader Device - Methods: StartDevice,StopDevice etc"


    Private Sub btnStartDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartDeviceNDC.Click
        Try
            blnStarDevice = True

            If objCardReaderInterface.StartDevice() = True Then
                CardReaderInfo("StartDevice() Success")
            Else
                CardReaderInfo("StartDevice() Failed")
            End If

        Catch ex As Exception
            CardReaderInfo("Error in btnStartDeviceNDC_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnStopDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopDeviceNDC.Click
        Try
            If objCardReaderInterface.StopDevice() = True Then
                CardReaderInfo("StopDevice() Success")
            Else
                CardReaderInfo("StopDevice() Failed")
            End If

        Catch ex As Exception
            CardReaderInfo("Error in btnStopDeviceNDC_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnLockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLockDeviceNDC.Click
        Try
            If objCardReaderInterface.LockDevice() = True Then
                CardReaderInfo("LockDevice() Success")
            Else
                CardReaderInfo("LockDevice() Failed")
            End If

        Catch ex As Exception
            CardReaderInfo("Error in btnLockDeviceNDC_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnUnlockDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlockDeviceNDC.Click
        Try
            If objCardReaderInterface.UnlockDevice() = True Then
                CardReaderInfo("UnlockDevice() Success")
            Else
                CardReaderInfo("UnlockDevice() Failed")
            End If

        Catch ex As Exception
            CardReaderInfo("Error in btnUnlockDeviceNDC_Click:" & ex.Message)
        End Try
    End Sub


    Private Sub btnWakeDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWakeDeviceNDC.Click
        Dim intState As Integer = 0
        Dim strCardDeviceState As String = String.Empty

        Try

            'Card Device State
            '0-No Card
            '1-Card In Reader
            '2-Card Stage
            '3-Captured
            '4-Eject Failed
            '5-Eject
            '6-Blocked

            'Get the Card Device State
            strCardDeviceState = cbState.Text
            strCardDeviceState = Mid(strCardDeviceState, 1, 1)
            strCardDeviceState = strCardDeviceState.Trim


            intState = CInt(strCardDeviceState)

            If objCardReaderInterface.WakeUpDevice(intState) = True Then
                CardReaderInfo("WakeupDevice() :" & cbState.Text.Trim & " Success")
            Else
                CardReaderInfo("WakeupDevice() :" & cbState.Text.Trim & " Failed")
            End If

        Catch ex As Exception
            CardReaderInfo("Error in btnWakeDeviceNDC_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnDiagDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagDeviceNDC.Click
        Try
            If objCardReaderInterface.DiagnosticDevice() = True Then
                CardReaderInfo("DiagDevice() Success")
            Else
                CardReaderInfo("DiagDevice() Failed")
            End If

        Catch ex As Exception
            CardReaderInfo("Error in btnDiagDeviceNDC_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnReadCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadCard.Click
        Dim intCardType As Integer
        Dim intRet As Integer

        Try
            intCardType = CInt(cboCardType.Text.Substring(0, 1))

            intRet = objCardReaderInterface.ReadCard(intCardType)

            If intCardType = intRet Then
                CardReaderInfo("ReadCard() Success")
            Else
                CardReaderInfo("ReadCard() Failed")
            End If
        Catch ex As Exception
            CardReaderInfo("Error in btnReadCard_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnTrack1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrack1.Click
        Dim strTrackData As String
        Try
            strTrackData = objCardReaderInterface.TrackI
            CardReaderInfo("Read TrackI :" & strTrackData)
        Catch ex As Exception
            CardReaderInfo("Error in btnTrack1_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnTrack2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrack2.Click
        Dim strTrackData As String
        Try
            strTrackData = objCardReaderInterface.TrackII
            CardReaderInfo("Read TrackII :" & strTrackData)
        Catch ex As Exception
            CardReaderInfo("Error in btnTrack2_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnTrack3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrack3.Click
        Dim strTrackData As String
        Try
            strTrackData = objCardReaderInterface.TrackIII
            CardReaderInfo("Read TrackIII :" & strTrackData)
        Catch ex As Exception
            CardReaderInfo("Error in btnTrack3_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnWrapDeviceNDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWrapDeviceNDC.Click
        Try
            If objCardReaderInterface.WrapDevice() = True Then
                CardReaderInfo("WrapDevice() Success")
            Else
                CardReaderInfo("WrapDevice() Failed")
            End If
        Catch ex As Exception
            CardReaderInfo("Error in btnWrapDeviceNDC_Click:" & ex.Message)
        End Try
    End Sub


    Private Sub btnClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            CardReaderInfo("Error in btnClearText_Click:" & ex.Message)
        End Try
    End Sub


    Private Sub cmdPMPCCardSerialNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPMPCCardSerialNo.Click
        Try

            strEvtMsg = objCardReaderInterface.CardSerialNum
            CardReaderInfo(strEvtMsg)
            strEvtMsg = ""

        Catch ex As Exception
            CardReaderInfo("Error in cmdPMPCCardSerialNo_Click:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Card Reader - Property"


    Private Sub cmdSetTimeOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetTimeOut.Click
        Dim strSetTimeoutId As String = String.Empty
        Dim lngTimeoutInterval As Long = 0
        Dim strInTimeoutId As String = String.Empty
        Dim strInTimeoutInterval As String = String.Empty

        Try

            'Input
            strInTimeoutId = txttmID.Text.Trim
            strInTimeoutInterval = txtInterval.Text.Trim

            If strInTimeoutId.Trim.Length > 0 And strInTimeoutInterval.Trim.Length > 0 Then

                strSetTimeoutId = Mid(strInTimeoutId, 1, 1)
                lngTimeoutInterval = CLng(strInTimeoutInterval)

                Select Case strSetTimeoutId

                    Case "1" 'CardInsertCheckTimeout
                        objCardReaderInterface.TimeoutInterval(1) = lngTimeoutInterval
                    Case "2" 'CardAtGateTimeout
                        objCardReaderInterface.TimeoutInterval(2) = lngTimeoutInterval
                    Case "3" ' CardCommandTimeout
                        objCardReaderInterface.TimeoutInterval(3) = lngTimeoutInterval
                    Case Else
                        MsgBox("Invalid Input", MsgBoxStyle.Critical, "Input Error")
                End Select

            Else
                MsgBox("Invalid Input", MsgBoxStyle.Critical, "Input Error")
            End If


        Catch ex As Exception
            CardReaderInfo("Error in cmdSetTimeOut_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub btnGetDevProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDevProp.Click
        Try
            strEvtTxtMsg = ""

            strEvtMsg = "Txn Status:" & objCardReaderInterface.TxnStatus
            CardReaderInfo(strEvtMsg)

            strEvtMsg = "Error Severity:" & objCardReaderInterface.ErrorSeverity
            CardReaderInfo(strEvtMsg)

            strEvtMsg = "Supply Status:" & objCardReaderInterface.SupplyStatus
            CardReaderInfo(strEvtMsg)

            strEvtMsg = "MStatus:" & objCardReaderInterface.MStatus
            CardReaderInfo(strEvtMsg)

            strEvtMsg = "MStatusData:" & objCardReaderInterface.MStatusData
            CardReaderInfo(strEvtMsg)


        Catch ex As Exception
            CardReaderInfo("Error in btnGetDevProp_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            CardReaderInfo("Error in cmdExit_Click:" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Card Reader Events - EvtDeviceDataReady,EvtDeviceError,EvtDeviceTimeout"

    Private Sub objCardReaderInterface_EvtDeviceDataReady(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs) Handles objCardReaderInterface.EvtDeviceDataReady
        Try
            strEvtTxtMsg = ""
            CardReaderInfo("EvtDeviceDataReady - Event Value:" & e.iDeviceState & " CardReader Value:" & e.mDeviceDataValue)
        Catch ex As Exception
            CardReaderInfo("Error in objCardReaderInterface_EvtDeviceDataReady:" & ex.Message)
            'MsgBox("Error in objCardReaderInterface_EvtDeviceDataReady:" & ex.Message)
        End Try
    End Sub

    Private Sub objCardReaderInterface_EvtDeviceError(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs) Handles objCardReaderInterface.EvtDeviceError
        Try
            strEvtTxtMsg = ""
            CardReaderInfo("EvtDeviceErr iDeviceState:" & e.iDeviceState & " iDeviceTrace" & e.iDeviceTrace)
        Catch ex As Exception
            CardReaderInfo("Error in objCardReaderInterface_EvtDeviceError:" & ex.Message)
            'MsgBox("Error in objCardReaderInterface_EvtDeviceError:" & ex.Message)
        End Try
    End Sub

    Private Sub objCardReaderInterface_EvtDeviceTimeOut(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs) Handles objCardReaderInterface.EvtDeviceTimeout
        Try
            strEvtTxtMsg = ""
            CardReaderInfo("EvtDeviceTimeout iDeviceState:" & e.iDeviceState & " iDeviceTrace" & e.iDeviceTrace)
        Catch ex As Exception
            CardReaderInfo("Error in objCardReaderInterface_EvtDeviceTimeOut:" & ex.Message)
            'MsgBox("Error in objCardReaderInterface_EvtDeviceTimeOut:" & ex.Message)
        End Try
    End Sub

#End Region



    
  
  
  
End Class
