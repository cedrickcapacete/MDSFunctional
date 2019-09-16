Imports CashDepositorHWD
Imports ChequeDepositHWD
Imports DoorSensorHWD

Public Class frmMaintenanceMode

#Region "Variable"

    Dim strTitle As String = "frmMaintenanceMode"

    Dim blnStarDevice As Boolean = False

    Public strEvtTxtMsg As String = ""
    Public strEvtMsg As String = ""
    Public strErrMsg As String = ""

  
#End Region


#Region "Door Control"

    Public blnStartDoorSensor As Boolean = False
    Public strDoorStatus As String = ""
    Public strMDSStatus As String = ""
    Public strUserStatus As String = ""
    Public strMDSEvents As String = ""

#End Region



#Region "MDS Object"

    Public WithEvents objCashDepositor As clsCashDepositorHWDInterface
    Public WithEvents objChqDepositor As clsChequeDepositorHWDInterface
    Public WithEvents objDoorSensor As clsDoorInterface

#End Region



    Private Sub frmMaintenanceMode_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStarDevice = True Then
                objCashDepositor.StopDevice()
                objChqDepositor.StopDevice()
                objDoorSensor.StopDevice()
            End If

            objCashDepositor = Nothing
            objChqDepositor = Nothing
            objDoorSensor = Nothing

            AppLogInfo("== End MDS Diagnostic Mode ==")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMaintenanceMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start MDS Diagnostic Mode ==")

            'Init the Object - MDS
            objCashDepositor = New clsCashDepositorHWDInterface
            objChqDepositor = New clsChequeDepositorHWDInterface
            objDoorSensor = New clsDoorInterface

            blnStarDevice = False

            'Init Display
            InitDisplay()

            InitViewChqImageInfo()

            frmMDSExchange.intMDSExchangeMode = 0

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#Region "InitDisplay"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""

            lblDoorStatus.Text = ""
            lblMDSEvent.Text = "Out of Services"
            lblMDSStatus.Text = "-"
            lblUserStatus.Text = "-"

            lblTtlChq.Text = "0"

            rbCASH.Checked = True


            'List Viewer
            lstView.Clear()

            With lstView.Columns
                .Add("Value", 120, HorizontalAlignment.Left)
                .Add("Ttl Item", 120, HorizontalAlignment.Left)
                .Add("Currency", 80, HorizontalAlignment.Left)
                '.Add("Physical Box", 100, HorizontalAlignment.Left)
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

#Region "Support Function"

    Private Sub InitViewChqImageInfo()
        Try

            frmViewChqImages.lblTtlChq.Text = "0"

            'List Viewer
            frmViewChqImages.lstChqView.Clear()

            With frmViewChqImages.lstChqView.Columns
                .Add("No.", 100, HorizontalAlignment.Left)
                .Add("MICR", 350, HorizontalAlignment.Left)
                .Add("Image Path", 600, HorizontalAlignment.Left)
            End With

            'lvwIcon is replaced by LargeIcon, lvwSmallIcon by SmallIcon, lvwList by List, and lvwReport by Details. 
            frmViewChqImages.lstChqView.View = View.Details
            frmViewChqImages.lstChqView.FullRowSelect = True
            frmViewChqImages.lstChqView.GridLines = True
            frmViewChqImages.lstChqView.MultiSelect = True

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitViewChqImageInfo. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


#Region "Form Events"

    Private Sub DisplayInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".DisplayInfo. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
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
            strErrMsg = "Error in " & strTitle & ".UpdateEvtInfo. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            lblDoorStatus.Text = strDoorStatus.Trim
            lblMDSStatus.Text = strMDSStatus.Trim
            lblUserStatus.Text = strUserStatus.Trim

            lblMDSEvent.Text = strMDSEvents.Trim

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".EvtEventsMsg. ErrInfo:" & ex.Message
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

    Private Sub btnMDSSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMDSSetting.Click
        Try
            'MDS Setting
            frmMDSSetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnMDSSetting_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdStartDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartDevice.Click
        Try

            blnStarDevice = True

            If objCashDepositor.StartDevice Then
                DisplayInfo("CASH-StartDevice Successfully")
            Else
                DisplayInfo("CASH-StartDevice Failed")
            End If

            If objChqDepositor.StartDevice Then
                DisplayInfo("CHQ-StartDevice Successfully")
            Else
                DisplayInfo("CHQ-StartDevice Failed")
            End If

            If objDoorSensor.StartDevice Then
                DisplayInfo("DOOR-StartDevice Successfully")
            Else
                DisplayInfo("DOOR-StartDevice Failed")
            End If

            MsgBox("MDS Initialise. Please Wait ....", MsgBoxStyle.Information, "MDS Initialise")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdStartDevice_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdStopDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopDevice.Click
        Try

            If blnStarDevice = True Then

                blnStarDevice = False

                'If objCashDepositor.StopDevice Then
                '    DisplayInfo("MDS-StopDevice Success")
                'Else
                '    DisplayInfo("MDS-StopDevice Failed")
                'End If

                'If objChqDepositor.StopDevice Then
                '    DisplayInfo("CHQ-StopDevice Success")
                'Else
                '    DisplayInfo("CHQ-StopDevice Failed")
                'End If

                'If objDoorSensor.StopDevice Then
                '    DisplayInfo("DOOR-StopDevice Success")
                'Else
                '    DisplayInfo("DOOR-StopDevice Failed")
                'End If

            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdStopDevice_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdMDSStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDSStatus.Click
        Try

            If blnStarDevice = True Then

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


            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdMDSStatus_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdMDSLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDSLight.Click
        Try
            'MDS Light Control
            If blnStarDevice = True Then
                frmMDSLightControl.ShowDialog()
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdMDSLight_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdMDSExchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDSExchange.Click
        Try
            'MDS Exchange
            If blnStarDevice = True Then
                frmMDSExchange.ShowDialog()
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdMDSExchange_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdCloseFeeder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCloseFeeder.Click
        Try

            '0-No Cash Deposit:Close Feeder
            '1-Open Feeder
            '2-Insert More
            '3-Event:Reject
            '4-Refund
            '5-Event:Escrowed
            '6-Event:Processing
            '7-Confirming
            '8-Event:Complete
            '10:A(-UserTimeOut)

            If blnStarDevice = True Then
                If rbCASH.Checked = True Then
                    If objCashDepositor.WakeUpDevice(0) Then
                        DisplayInfo("CASH-WakeUpDevice:0-Close Feeder Successfully")
                    Else
                        DisplayInfo("CASH-WakeUpDevice:0-Close Feeder Failed")
                    End If
                ElseIf rbCASHNCHQ.Checked = True Then
                    If objCashDepositor.WakeUpDevice(0) Then
                        DisplayInfo("CASHCHQ-WakeUpDevice:0-Close Feeder Successfully")
                    Else
                        DisplayInfo("CASHCHQ-WakeUpDevice:0-Close Feeder Failed")
                    End If
                ElseIf rbCHQ.Checked = True Then
                    If objChqDepositor.WakeUpDevice(0) Then
                        DisplayInfo("CHQ-WakeUpDevice:0-Close Feeder Successfully")
                    Else
                        DisplayInfo("CHQ-WakeUpDevice:0-Close Feeder Failed")
                    End If
                End If
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdCloseFeeder_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdOpenFeeder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenFeeder.Click
        Try

            'Clear Item
            lblTtlChq.Text = "0"
            'Clear Item
            lstView.Items.Clear()

            'View Cheque Images
            frmViewChqImages.lblTtlChq.Text = "0"
            frmViewChqImages.lstChqView.Items.Clear()

            If blnStarDevice = True Then
                If rbCASH.Checked = True Then
                    AppLogInfo("MDS Cash Only Mode Selected")
                    objCashDepositor.SetMixModeOn = False
                    If objCashDepositor.WakeUpDevice(1) Then
                        DisplayInfo("CASH-WakeUpDevice:1-Open Feeder Successfully")
                    Else
                        DisplayInfo("CASH-WakeUpDevice:1-Open Feeder Failed")
                    End If
                ElseIf rbCASHNCHQ.Checked = True Then
                    AppLogInfo("MDS Mix Mode Selected")
                    objCashDepositor.SetMixModeOn = True
                    If objCashDepositor.WakeUpDevice(1) Then
                        DisplayInfo("CASHCHQ-WakeUpDevice:1-Open Feeder Successfully")
                    Else
                        DisplayInfo("CASHCHQ-WakeUpDevice:1-Open Feeder Failed")
                    End If
                ElseIf rbCHQ.Checked = True Then
                    AppLogInfo("MDS Cheque Only Mode Selected")
                    objCashDepositor.SetMixModeOn = False
                    If objChqDepositor.WakeUpDevice(1) Then
                        DisplayInfo("CHQ-WakeUpDevice:1-Open Feeder Successfully")
                    Else
                        DisplayInfo("CHQ-WakeUpDevice:1-Open Feeder Failed")
                    End If
                End If
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdOpenFeeder_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdInsertMore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInsertMore.Click
        Try
            If blnStarDevice = True Then
                If rbCASH.Checked = True Then
                    If objCashDepositor.WakeUpDevice(2) Then
                        DisplayInfo("CASH-WakeUpDevice:2-Insert More Successfully")
                    Else
                        DisplayInfo("CASH-WakeUpDevice:2-Insert More Failed")
                    End If
                ElseIf rbCASHNCHQ.Checked = True Then
                    If objCashDepositor.WakeUpDevice(2) Then
                        DisplayInfo("CASHCHQ-WakeUpDevice:2-Insert More Successfully")
                    Else
                        DisplayInfo("CASHCHQ-WakeUpDevice:2-Insert More Failed")
                    End If
                ElseIf rbCHQ.Checked = True Then
                    If objChqDepositor.WakeUpDevice(2) Then
                        DisplayInfo("CHQ-WakeUpDevice:2-Insert More Successfully")
                    Else
                        DisplayInfo("CHQ-WakeUpDevice:2-Insert More Failed")
                    End If
                End If
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdInsertMore_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReject.Click
        Try
            If blnStarDevice = True Then
                If rbCASH.Checked = True Then
                    If objCashDepositor.WakeUpDevice(4) Then
                        DisplayInfo("CASH-WakeUpDevice:4-Refund Successfully")
                    Else
                        DisplayInfo("CASH-WakeUpDevice:4-Refund Failed")
                    End If
                ElseIf rbCASHNCHQ.Checked = True Then
                    If objCashDepositor.WakeUpDevice(4) Then
                        DisplayInfo("CASHCHQ-WakeUpDevice:4-Refund Successfully")
                    Else
                        DisplayInfo("CASHCHQ-WakeUpDevice:4-Refund Failed")
                    End If
                ElseIf rbCHQ.Checked = True Then
                    If objChqDepositor.WakeUpDevice(4) Then
                        DisplayInfo("CHQ-WakeUpDevice:4-Refund Successfully")
                    Else
                        DisplayInfo("CHQ-WakeUpDevice:4-Refund Failed")
                    End If
                End If
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdReject_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeposit.Click
        Try
            If blnStarDevice = True Then

                'Msgbox
                If MsgBox("Are you sure want to perform the deposit?" & vbCrLf & "Note:Please make sure you already perform the settlement.", vbQuestion + vbYesNo, "Deposit") = vbYes Then
                    If rbCASH.Checked = True Then
                        If objCashDepositor.WakeUpDevice(7) Then
                            DisplayInfo("CASH-WakeUpDevice:7-Deposit Successfully")
                        Else
                            DisplayInfo("CASH-WakeUpDevice:7-Deposit Failed")
                        End If
                    ElseIf rbCASHNCHQ.Checked = True Then
                        If objCashDepositor.WakeUpDevice(7) Then
                            DisplayInfo("CASHCHQ-WakeUpDevice:7-Deposit Successfully")
                        Else
                            DisplayInfo("CASHCHQ-WakeUpDevice:7-Deposit Failed")
                        End If
                    ElseIf rbCHQ.Checked = True Then
                        If objChqDepositor.WakeUpDevice(7) Then
                            DisplayInfo("CHQ-WakeUpDevice:7-Deposit Successfully")
                        Else
                            DisplayInfo("CHQ-WakeUpDevice:7-Deposit Failed")
                        End If
                    End If
                Else
                    AppLogInfo("Deposit Cancel")
                End If
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdDeposit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdUserTimeout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUserTimeout.Click
        Try
            If blnStarDevice = True Then

                If rbCASH.Checked = True Then
                    If objCashDepositor.WakeUpDevice(10) Then
                        DisplayInfo("CASH-WakeUpDevice:10-UserTimeout Successfully")
                    Else
                        DisplayInfo("CASH-WakeUpDevice:10-UserTimeout Failed")
                    End If
                ElseIf rbCASHNCHQ.Checked = True Then
                    If objCashDepositor.WakeUpDevice(10) Then
                        DisplayInfo("CASHCHQ-WakeUpDevice:10-UserTimeout Successfully")
                    Else
                        DisplayInfo("CASHCHQ-WakeUpDevice:10-UserTimeout Failed")
                    End If
                ElseIf rbCHQ.Checked = True Then
                    If objChqDepositor.WakeUpDevice(10) Then
                        DisplayInfo("CHQ-WakeUpDevice:10-UserTimeout Successfully")
                    Else
                        DisplayInfo("CHQ-WakeUpDevice:10-UserTimeout Failed")
                    End If
                End If
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdUserTimeout_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdCleanPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCleanPath.Click
        Try
            If blnStarDevice = True Then

                MsgBox("MDS Clean Path. Please Wait ....", MsgBoxStyle.Information, "MDS Clean Path")

                objCashDepositor.MDSReset()
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdCleanPath_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnClearText_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnViewChqImages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewChqImages.Click
        Try
            If blnStarDevice = True Then
                'View Chq Images
                frmViewChqImages.ShowDialog()
            Else
                strLogMsg = "MDS Modules Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnViewChqImages_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#End Region







#Region "Door Events"

    Private Sub objDoorSensor_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objDoorSensor.EvtDeviceError
        Try

            Select Case e.iDeviceState

                Case 0
                    strDoorStatus = "Door Close"
                    strEvtMsg = "Door Close"

                Case 1
                    strDoorStatus = "Door Close"
                    strEvtMsg = "Door Close"

                Case 2
                    strDoorStatus = "Door Open - Maintenance Mode"
                    strEvtMsg = "Door Open - Maintenance Mode"

                Case 3

                    strDoorStatus = "Door Open - Supervisor Mode"
                    strEvtMsg = "Door Open - Supervisor Mode"

                Case Else
                    strDoorStatus = "Door Open - Unknown State"
                    strEvtMsg = "Door Open - Unknown State"

            End Select

            UpdateEvtInfo()
            strEvtMsg = ""

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objDoorSensor_EvtDeviceError. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "Cash Events"

    Private Sub objCashDepositor_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objCashDepositor.EvtDeviceDataReady
        Try
            AppLogInfo("objCashDepositor_EvtDeviceDataReady")
            'MDS Status
            MDSStatusDisplayInfo()

            strEvtMsg = "CASH-Device Data Ready State:" & e.iDeviceState & " DataValue:" & e.mDeviceDataValue & " Trace:" & e.iDeviceTrace
            UpdateEvtInfo()

            Select Case CInt(e.iDeviceState)
                Case 0
                    strEvtMsg = "CASH-State0: Closed"
                    strMDSEvents = "Ready to Start Transaction"

                Case 1
                    strEvtMsg = "CASH-State1: Open & Prepare"
                    strMDSEvents = "Insert Items"

                Case 2
                    strEvtMsg = "CASH-State2: Insert More"

                Case 3
                    strEvtMsg = "CASH-State3: Rejected Item Detected"
                    strMDSEvents = "Please Take Rejected Item"

                Case 4
                    strEvtMsg = "CASH-State4: Refunded"

                Case 5
                    strEvtMsg = "CASH-State5: Escrowed"
                    strMDSEvents = "Do you want to insert more items?"

                    If rbCASH.Checked = True Then
                        'Deposit Note Summary - Cash Only
                        CountInsertedCash(e.mDeviceDataValue)
                    ElseIf rbCASHNCHQ.Checked = True Then
                        'MIX Mode
                        CountInsertedCashChq(e.mDeviceDataValue)
                    End If

                Case 6
                    strEvtMsg = "CASH-State6: Processing"
                    strMDSEvents = "Processing"

                Case 7
                    strEvtMsg = "CASH-State7: Confirm Transaction"
                    strMDSEvents = "Do you want to complete deposit?"

                Case 8
                    strEvtMsg = "CASH-State8: Trans Complete"
                    strMDSEvents = "Ready to Start Transaction"

                    'Clear Item
                    lblTtlChq.Text = "0"
                    'Clear Item
                    lstView.Items.Clear()

                Case 11
                    strEvtMsg = "CASH-State11: Deposit Item Full"
                    strMDSEvents = "Do you want to complete deposit?"

                Case 9
                    strEvtMsg = "CASH-State11: Supervisor - Cash Box Counter"
                    'frmMDSExchange.CashBoxSummary(e.mDeviceDataValue)

            End Select

            UpdateEvtInfo()


            'ExChangeDataReady()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objCashDepositor_EvtDeviceDataReady. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objCashDepositor_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objCashDepositor.EvtDeviceError
        Try
            AppLogWarn("objCashDepositor_EvtDeviceError")
            'MDS Status
            MDSStatusDisplayInfo()

            strEvtMsg = "CASH-Device Data Error State:" & e.iDeviceState & " DataValue:" & e.mDeviceDataValue & " Trace:" & e.iDeviceTrace
            UpdateEvtInfo()


            ''For the EXCHANGE PROCESS and Control
            'If frmMDSExchange.intMDSExchangeMode = 1 Or frmMDSExchange.intMDSExchangeMode = 3 Then
            '    strMDSStatus = objCashDepositor.MDSReplyStatusReason
            '    strErrMsg = "Reset Cash Box Counter Failed. MDSStatus:" & strMDSStatus
            '    AppLogWarn(strErrMsg)
            '    MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Reset Cash Box Counter")
            '    frmMDSExchange.lblMDSResetMsg.Text = "Reset Failed.MDSStatus:" & strMDSStatus
            'ElseIf frmMDSExchange.intMDSExchangeMode = 2 Or frmMDSExchange.intMDSExchangeMode = 4 Then
            '    strMDSStatus = objCashDepositor.MDSReplyStatusReason
            '    strErrMsg = "Reset Cheque Box Counter Failed. MDSStatus:" & strMDSStatus
            '    AppLogWarn(strErrMsg)
            '    MsgBox(strErrMsg, MsgBoxStyle.Critical, "MDS Reset Cheque Box Counter")
            '    frmMDSExchange.lblMDSResetMsg.Text = "Reset Failed.MDSStatus:" & strMDSStatus
            'End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objCashDepositor_EvtDeviceError. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objCashDepositor_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objCashDepositor.EvtDeviceTimeout
        Try
            AppLogWarn("objCashDepositor_EvtDeviceTimeout")
            'MDS Status
            MDSStatusDisplayInfo()

            strEvtMsg = "CASH-Device Timeout State:" & e.iDeviceState & " DataValue:" & e.mDeviceDataValue & " Trace:" & e.iDeviceTrace
            strMDSEvents = "Clean feeder from all items"
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objCashDepositor_EvtDeviceTimeoutr. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "Cheque Events"

    Private Sub objChqDepositor_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objChqDepositor.EvtDeviceDataReady
        Try
            AppLogInfo("objChqDepositor_EvtDeviceDataReady")
            'MDS Status
            MDSStatusDisplayInfo()

            strEvtMsg = "Cheque-Device Data Ready State:" & e.iDeviceState & " DataValue:" & e.mDeviceDataValue & " Trace:" & e.iDeviceTrace
            UpdateEvtInfo()

            Select Case CInt(e.iDeviceState)
                Case 0
                    strEvtMsg = "CHQ-State0: Closed"
                    strMDSEvents = "Ready to Start Transaction"

                Case 1
                    strEvtMsg = "CHQ-State1: Open & Prepare"
                    strMDSEvents = "Insert Items"

                Case 2
                    strEvtMsg = "CHQ-State2: Insert More"

                Case 3
                    strEvtMsg = "CHQ-State3: Rejected Item Detected"
                    strMDSEvents = "Please Take Rejected Item"

                Case 4
                    strEvtMsg = "CHQ-State4: Refunded"

                Case 5
                    strEvtMsg = "CHQ-State5: Escrowed"
                    strMDSEvents = "Do you want to insert more items?"

                    'Count the Cheque - in Escrow
                    CountInsertedCheque(e.mDeviceDataValue)


                Case 6
                    strEvtMsg = "CHQ-State6: Processing"
                    strMDSEvents = "Processing"

                Case 7
                    strEvtMsg = "CHQ-State7: Confirm Transaction"
                    strMDSEvents = "Do you want to complete deposit?"

                Case 8
                    strEvtMsg = "CHQ-State8: Trans Complete"
                    strMDSEvents = "Ready to Start Transaction"

                    frmViewChqImages.DepositChequeSummaryInfo(e.mDeviceDataValue)

                    'Clear Item
                    lblTtlChq.Text = "0"
                    'Clear Item
                    lstView.Items.Clear()

                Case 11
                    strEvtMsg = "CHQ-State11: Deposit Item Full"
                    strMDSEvents = "Do you want to complete deposit?"

            End Select

            UpdateEvtInfo()
           
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objChqDepositor_EvtDeviceDataReady. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objChqDepositor_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objChqDepositor.EvtDeviceError
        Try
            AppLogWarn("objChqDepositor_EvtDeviceError")
            'MDS Status
            MDSStatusDisplayInfo()

            strEvtMsg = "Cheque-Device Data Error State:" & e.iDeviceState & " DataValue:" & e.mDeviceDataValue & " Trace:" & e.iDeviceTrace
            UpdateEvtInfo()


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objChqDepositor_EvtDeviceError. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objChqDepositor_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objChqDepositor.EvtDeviceTimeout
        Try
            AppLogWarn("objChqDepositor_EvtDeviceTimeout")
            'MDS Status
            MDSStatusDisplayInfo()

            strEvtMsg = "Cheque-Device Timeout State:" & e.iDeviceState & " DataValue:" & e.mDeviceDataValue & " Trace:" & e.iDeviceTrace
            strMDSEvents = "Clean feeder from all items"
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objChqDepositor_EvtDeviceTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "Support App Sub Func"

    Public Sub MDSStatusDisplayInfo()
        Try

            strMDSStatus = objCashDepositor.MDSReplyStatusReason
            UpdateEvtInfo()
            'strEvtMsg = ""

            strUserStatus = objCashDepositor.MDSUserStatus
            UpdateEvtInfo()
            'strEvtMsg = ""

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".MDSStatusDisplayInfo. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


#Region "Count Insert Cash Item"

    Public Sub CountInsertedCash(ByVal strCashData As String)
        Dim tmpStrCashData As String = String.Empty
        Dim tmpArrCashData() As String = Nothing

        Dim tmpArrCashDataInfo() As String = Nothing

        Dim tmpDenoType As String = String.Empty
        Dim tmpDenoCnt As String = String.Empty
        Dim tmpDenoCurrencyType As String = String.Empty


        Dim intPos As Integer = 0
        Dim dblTotalAmt As Double = 0.0

        Dim objItem As ListViewItem

        Try
            'udtTransCashInfo = New InsertedCashCount

            ''Init Trans Cash Info
            'With udtTransCashInfo
            '    .intRM1 = 0
            '    .intRM5 = 0
            '    .intRM10 = 0
            '    .intRM20 = 0
            '    .intRM50 = 0
            '    .intRM100 = 0
            '    .dblTotalDeposit = 0
            'End With

            'Clear Item
            lstView.Items.Clear()


            AppLogInfo("CountInsertedCash CashData:" & strCashData)

            strCashData = strCashData.Trim

            If strCashData.Length > 0 Then

                tmpArrCashData = Split(strCashData, "|", -1, CompareMethod.Text)

                For i = 0 To tmpArrCashData.Length - 1
                    If tmpArrCashData(i).Trim <> String.Empty Then
                        tmpStrCashData = tmpArrCashData(i)

                        tmpArrCashDataInfo = Split(tmpStrCashData, ",", -1, CompareMethod.Text)

                        'intPos = tmpStrCashData.IndexOf(",")
                        'tmpDenoType = tmpStrCashData.Substring(0, intPos)
                        'tmpDenoCnt = tmpStrCashData.Substring(intPos + 1)

                        'Multiple Currency
                        tmpDenoType = tmpArrCashDataInfo(0)
                        tmpDenoCnt = tmpArrCashDataInfo(1)
                        tmpDenoCurrencyType = tmpArrCashDataInfo(2)

                        'Add to List Viewer
                        objItem = lstView.Items.Add(tmpDenoType)
                        With objItem
                            .SubItems.Add(tmpDenoCnt)
                            .SubItems.Add(tmpDenoCurrencyType)
                        End With

                        'Select Case CInt(tmpDenoType)
                        '    Case 1
                        '        'udtTransCashInfo.intRM1 = CInt(tmpDenoCnt)
                        '        'dblTotalAmt = dblTotalAmt + (udtTransCashInfo.intRM1 * 1)
                        '    Case 5
                        '        'udtTransCashInfo.intRM5 = CInt(tmpDenoCnt)
                        '        'dblTotalAmt = dblTotalAmt + (udtTransCashInfo.intRM5 * 5)
                        '    Case 10
                        '        'udtTransCashInfo.intRM10 = CInt(tmpDenoCnt)
                        '        'dblTotalAmt = dblTotalAmt + (udtTransCashInfo.intRM10 * 10)
                        '    Case 20
                        '        'udtTransCashInfo.intRM20 = CInt(tmpDenoCnt)
                        '        'dblTotalAmt = dblTotalAmt + (udtTransCashInfo.intRM20 * 20)
                        '    Case 50
                        '        'udtTransCashInfo.intRM50 = CInt(tmpDenoCnt)
                        '        'dblTotalAmt = dblTotalAmt + (udtTransCashInfo.intRM50 * 50)
                        '    Case 100
                        '        'udtTransCashInfo.intRM100 = CInt(tmpDenoCnt)
                        '        'dblTotalAmt = dblTotalAmt + (udtTransCashInfo.intRM100 * 100)
                        'End Select
                    End If
                Next

                'udtTransCashInfo.dblTotalDeposit = dblTotalAmt

            Else
                AppLogInfo("Insert Cash Value Empty")
            End If

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".CountInsertedCash:" & ex.Message)
        End Try
    End Sub


#End Region

#Region "Count Insert Cheque Item"

    Public Sub CountInsertedCheque(ByVal strChqData As String)
        Dim tmpStrCashData As String = String.Empty
        Dim tmpArrCashData() As String = Nothing

        Dim tmpDenoType As String = String.Empty
        Dim tmpDenoCnt As String = String.Empty

        Dim intBadChequeCount As Integer = 0
        Dim intGoodChequeCount As Integer = 0
        Dim intTtlCheque As Integer = 0
        Dim intPos As Integer = 0

        Try

            AppLogInfo("CountInsertedCheque IN ESCROW ChqData:" & strChqData)

            tmpArrCashData = Split(strChqData, "|", -1, CompareMethod.Text)

            For i = 0 To tmpArrCashData.Length - 2

                AppLogInfo("CountInsertedCheque IN ESCROW tmpArrCashData details - Item:" & i & ":" & tmpArrCashData(i).Trim)

                If tmpArrCashData(i).Trim <> String.Empty Then
                    intGoodChequeCount = intGoodChequeCount + 1
                Else
                    intBadChequeCount = intBadChequeCount + 1
                End If

            Next

            intTtlCheque = intGoodChequeCount + intBadChequeCount

            AppLogInfo("Good Cheque Cnt=" & intGoodChequeCount)
            AppLogInfo("Bad Cheque Cnt=" & intBadChequeCount)
            AppLogInfo("Ttl Cheque Cnt=" & intTtlCheque)

            'Total Cheques
            lblTtlChq.Text = intTtlCheque

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".CountInsertedCheque:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Count Insert Cash and Chq Item"

    Public Sub CountInsertedCashChq(ByVal strCashData As String)
        Dim tmpArrCashChqData() As String = Nothing

        Dim tmpStrCashData As String = ""
        Dim tmpStrChqData As String = ""

        Try

            'Clear Item
            lstView.Items.Clear()

            AppLogInfo("CountInsertedCashChq CashChqData:" & strCashData)

            strCashData = strCashData.Trim

            If strCashData.Length > 0 Then

                tmpArrCashChqData = Split(strCashData, ";", -1, CompareMethod.Text)

                tmpStrCashData = tmpArrCashChqData(0)
                tmpStrChqData = tmpArrCashChqData(1)

                AppLogInfo("CashChq-CashValue=" & tmpStrCashData)
                AppLogInfo("CashChq-ChqValue=" & tmpStrChqData)

                CountInsertedCash(tmpStrCashData)
                CountInsertedCheque(tmpStrChqData)

            Else
                AppLogInfo("Insert CashChq Value Empty")
            End If

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".CountInsertedCashChq:" & ex.Message)
        End Try
    End Sub


#End Region



#Region "MDS Exchange Process"


    Private Sub ExChangeDataReady()
        Dim strMsg As String = ""
        Dim vAns As MsgBoxResult

        Try

            frmMDSExchange.lblMDSResetMsg.Text = ""

            If frmMDSExchange.intMDSExchangeMode = 1 Then

                strMsg = "Are You Confirm Want To Clear Local Cash Total ?"

                vAns = MsgBox(strMsg, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation, "Clear Local Cash Total")

                If (vAns = MsgBoxResult.Ok) Then
                    frmMDSExchange.intMDSExchangeMode = 3
                    objCashDepositor.ResetCashCounter()
                Else
                    AppLogInfo("End Cash Box Exchange - Cancel")
                End If

            ElseIf frmMDSExchange.intMDSExchangeMode = 3 Then
                strErrMsg = "Reset Cash Box Counter Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Reset Cash Box Counter")
                frmMDSExchange.lblMDSResetMsg.Text = ""
            ElseIf frmMDSExchange.intMDSExchangeMode = 2 Then
                'End Exchange
                strMsg = "Are You Confirm Want To Clear Local Cheque Total ?"
                vAns = MsgBox(strMsg, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation, "Clear Local Cheque Total")
                If (vAns = MsgBoxResult.Ok) Then
                    'Reset Cheque Conter
                    frmMDSExchange.intMDSExchangeMode = 4
                    objCashDepositor.ResetChequeCounter()
                Else
                    AppLogInfo("End Cheque Box Exchange - Cancel")
                End If
            ElseIf frmMDSExchange.intMDSExchangeMode = 4 Then
                strErrMsg = "Reset Cheque Box Counter Successfully"
                AppLogInfo(strErrMsg)
                MsgBox(strErrMsg, MsgBoxStyle.Information, "MDS Reset Cheque Box Counter")
                'Start Exchange - Cheques
                lblTtlChq.Text = objChqDepositor.MDSChequeInBoxCounter
                AppLogInfo("Total Cheque in Box After Reset=" & lblTtlChq.Text)
                frmMDSExchange.lblMDSResetMsg.Text = ""
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".ExChangeDataReady. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

#Region "Get Cash and Cheque Box Counter"


 
    Private Sub cmdCollectionBoxCounter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCollectionBoxCounter.Click
        Dim strReplyCashBoxInfo As String = ""
        Dim intCrrChqTtl As Integer

        Try

            'Get the Cash Box Item Counter
            strReplyCashBoxInfo = objCashDepositor.MDSNoteInAllBoxCount
            strReplyCashBoxInfo = strReplyCashBoxInfo.Trim
            strEvtMsg = "CashBoxItemCounter-ByMachineValue CounterfitBox|RetractBox|CashBox1|CashBox2|CashBox3|CashBox4|CashBox5=" & strReplyCashBoxInfo
            AppLogInfo(strEvtMsg)
            UpdateEvtInfo()

            'Get the Cheque Box Item Counter
            intCrrChqTtl = objChqDepositor.MDSChequeInBoxCounter
            strEvtMsg = "ChqBoxItemCounter-ByMachineValue CurrChqBoxCounter=" & intCrrChqTtl
            AppLogInfo(strEvtMsg)
            UpdateEvtInfo()

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdCollectionBoxCounter_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region


End Class