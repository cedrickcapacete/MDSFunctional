Imports CardReaderHWD
Imports clsCardReader.clsCardReaderControl
Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmMaintenanceMode

#Region "Variable"

    Dim strTitle As String = "frmMaintenanceMode"

    Dim blnStarDevice As Boolean = False


#End Region

#Region "CardReader Object"

    Public WithEvents objCardReaderInterface As clsCardReaderHWDInterface

#End Region


    Private Sub frmMaintenanceMode_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStarDevice = True Then
                objCardReaderInterface.StopDevice()
            End If

            objCardReaderInterface = Nothing

            AppLogInfo("== End Card Reader Diagnostic Mode ==")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMaintenanceMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start Card Reader Diagnostic Mode ==")

            'Init the Object - Card Reader
            objCardReaderInterface = New clsCardReaderHWDInterface

            blnStarDevice = False

            'Init Display
            InitDisplay()



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
          
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#End Region

#Region "User Command"

    Private Sub btnCardReaderSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCardReaderSetting.Click
        Try
            'Card Reader Setting
            frmCardReaderSetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnCardReaderSetting_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnReadCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadCard.Click
        Dim intCardType As Integer
        Dim intRet As Integer
        Dim strTrackData1 As String = ""
        Dim strTrackData2 As String = ""
        Dim strTrackData3 As String = ""
    

        Try

            txtDeviceProperty.Text = ""


            If blnStarDevice = True Then

                intCardType = 0 'CInt(cboCardType.Text.Substring(0, 1))

                intRet = objCardReaderInterface.ReadCard(intCardType)

                If intCardType = intRet Then

                    strLogMsg = "Read Card - Magnetic Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")

                    'Display the Card Track Info
                    With objCardReaderInterface
                        strTrackData1 = .TrackI
                        strTrackData2 = .TrackII
                        strTrackData3 = .TrackIII
                    End With

                    'Track Info
                    strTrackData1 = strTrackData1.Trim
                    strTrackData2 = strTrackData2.Trim
                    strTrackData3 = strTrackData3.Trim
                    AppLogInfo("Track1:" & strTrackData1)
                    AppLogInfo("Track2:" & strTrackData2)
                    AppLogInfo("Track3:" & strTrackData3)

                    txtDeviceProperty.Text = "TrackI :" & strTrackData1.Trim & vbCrLf & "TrackII :" & strTrackData2.Trim & vbCrLf & "TrackIII :" & strTrackData3.Trim
                    'txtDeviceProperty.ScrollToCaret()

                Else
                    strLogMsg = "Reader Card Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")

                    txtDeviceProperty.Text = ""

                End If

            Else
                strLogMsg = "Card Reader Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnReadCard_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnReadPMPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadPMPC.Click
        Dim intCardType As Integer
        Dim intRet As Integer
        Dim strTrackData1 As String = ""
        Dim strTrackData2 As String = ""
        Dim strTrackData3 As String = ""
        Dim strCardSerialNo As String = ""

        Try

            txtDeviceProperty.Text = ""


            If blnStarDevice = True Then

                intCardType = 1 'CInt(cboCardType.Text.Substring(0, 1))

                intRet = objCardReaderInterface.ReadCard(intCardType)

                If intCardType = intRet Then

                    strLogMsg = "Read Card - PMPC Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")

                    'Display the Card Track Info
                    With objCardReaderInterface
                        strTrackData1 = .TrackI
                        strTrackData2 = .TrackII
                        strTrackData3 = .TrackIII
                        strCardSerialNo = .CardSerialNum
                    End With

                    'Track Info
                    strTrackData1 = strTrackData1.Trim
                    strTrackData2 = strTrackData2.Trim
                    strTrackData3 = strTrackData3.Trim
                    strCardSerialNo = strCardSerialNo.Trim
                    AppLogInfo("Track1:" & strTrackData1)
                    AppLogInfo("Track2:" & strTrackData2)
                    AppLogInfo("Track3:" & strTrackData3)
                    AppLogInfo("Card Serial No:" & strCardSerialNo)

                    txtDeviceProperty.Text = "TrackI :" & strTrackData1.Trim & Environment.NewLine
                    txtDeviceProperty.Text &= "TrackII :" & strTrackData2.Trim & Environment.NewLine
                    txtDeviceProperty.Text &= "TrackIII :" & strTrackData3.Trim & Environment.NewLine
                    txtDeviceProperty.Text &= "Card Serial No:" & strCardSerialNo

                    'txtDeviceProperty.ScrollToCaret()

                Else
                    strLogMsg = "Reader Card Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                    txtDeviceProperty.Text = ""
                End If
            Else
                strLogMsg = "Card Reader Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnReadPMPC_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub cmdWakeupDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWakeupDevice.Click
        Dim intState As Integer = 0
        Dim strCardDeviceState As String = ""

        Try

            txtDeviceProperty.Text = ""


            'Card Device State
            '0-No Card
            '1-Card In Reader
            '2-Card Stage
            '3-Captured
            '4-Eject Failed
            '5-Eject
            '6-Blocked

            'Get the Card Device State
            'strCardDeviceState = 1  'cbState.Text
            'strCardDeviceState = Mid(strCardDeviceState, 1, 1)
            'strCardDeviceState = strCardDeviceState.Trim


            If blnStarDevice = True Then

                intState = 1 'CInt(strCardDeviceState)

                If objCardReaderInterface.WakeUpDevice(intState) = True Then
                    'strLogMsg = "WakeUpDevice State:" & cbState.Text.Trim & " Successfully"
                    strLogMsg = "WakeUpDevice State:1-Card In Reader Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    'strLogMsg = "WakeupDevice  State:" & cbState.Text.Trim & " Failed"
                    strLogMsg = "WakeUpDevice State:1-Card In Reader Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If

            Else
                strLogMsg = "Card Reader Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdWakeupDevice_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnEjectCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEjectCard.Click
        Dim intState As Integer = 0
        Dim strCardDeviceState As String = ""

        Try

            txtDeviceProperty.Text = ""


            'Card Device State
            '0-No Card
            '1-Card In Reader
            '2-Card Stage
            '3-Captured
            '4-Eject Failed
            '5-Eject
            '6-Blocked

            'Get the Card Device State
            'strCardDeviceState = 1  'cbState.Text
            'strCardDeviceState = Mid(strCardDeviceState, 1, 1)
            'strCardDeviceState = strCardDeviceState.Trim

            If blnStarDevice = True Then

                intState = 5 'CInt(strCardDeviceState)

                If objCardReaderInterface.WakeUpDevice(intState) = True Then
                    'strLogMsg = "WakeUpDevice State:" & cbState.Text.Trim & " Successfully"
                    strLogMsg = "WakeUpDevice State:5-Eject Card Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    'strLogMsg = "WakeupDevice  State:" & cbState.Text.Trim & " Failed"
                    strLogMsg = "WakeUpDevice State:5-Eject Card Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Card Reader Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If



        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnEjectCard_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnCaptureCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCaptureCard.Click
        Dim intState As Integer = 0
        Dim strCardDeviceState As String = ""

        Try

            txtDeviceProperty.Text = ""


            'Card Device State
            '0-No Card
            '1-Card In Reader
            '2-Card Stage
            '3-Captured
            '4-Eject Failed
            '5-Eject
            '6-Blocked

            'Get the Card Device State
            'strCardDeviceState = 1  'cbState.Text
            'strCardDeviceState = Mid(strCardDeviceState, 1, 1)
            'strCardDeviceState = strCardDeviceState.Trim

            If blnStarDevice = True Then

                intState = 3 'CInt(strCardDeviceState)

                If objCardReaderInterface.WakeUpDevice(intState) = True Then
                    'strLogMsg = "WakeUpDevice State:" & cbState.Text.Trim & " Successfully"
                    strLogMsg = "WakeUpDevice State:3-Captured Card Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    'strLogMsg = "WakeupDevice  State:" & cbState.Text.Trim & " Failed"
                    strLogMsg = "WakeUpDevice State:3-Captured Card Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Card Reader Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnCaptureCard_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


    Private Sub cmdWrapDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrapDevice.Click
        Try
            If blnStarDevice = True Then
                If objCardReaderInterface.WrapDevice() = True Then
                    strLogMsg = "WrapeDevice - Card Reader Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "WrapeDevice - Card Reader Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Card Reader Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdWrapDevice_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try

            If blnStarDevice = True Then
                blnStarDevice = False
                If objCardReaderInterface.StopDevice() = True Then
                    strLogMsg = "StopDevice - Card Reader Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "StopDevice - Card Reader Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Card Reader Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdClose_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInit.Click
        Try

            If objCardReaderInterface.StartDevice() = True Then
                blnStarDevice = True
                strLogMsg = "StartDevice - Init Card Reader Successfully"
                AppLogInfo(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
            Else
                blnStarDevice = False
                strLogMsg = "StartDevice - Init Card Reader Failed"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdInit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            'Unload
            AppLogInfo("Card Reader Diagnostic Menu - Exit")
            Me.Close()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnExit_Click. ErrInfo:" & ex.Message
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

#End Region

   
 
End Class