Imports System.IO
Imports ClsReceiptPrinterHWDInterface
Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmMaintenanceMode

#Region "Variable"

    Dim strTitle As String = "frmMaintenanceMode"

    Dim blnStarDevice As Boolean = False

#End Region


#Region "Printer Object"

    Public WithEvents objReceiptPrinterInterface As ClsReceiptPrinterHWDInterface.clsHardwareLayer

#End Region

    Private Sub frmMaintenanceMode_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnStarDevice = True Then
                objReceiptPrinterInterface.StopDevice()
            End If

            objReceiptPrinterInterface = Nothing

            AppLogInfo("== End Printer Diagnostic Mode ==")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmMaintenanceMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start Printer Diagnostic Mode ==")

            'Init the Object - PRINTER
            objReceiptPrinterInterface = New ClsReceiptPrinterHWDInterface.clsHardwareLayer

            'Init Display
            InitDisplay()

            blnStarDevice = False

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "InitDisplay"

    Private Sub InitDisplay()
        Try
            strReceiptData = "TRANSACTION RECORD                      :Deposit" & vbCrLf & vbCrLf
            strReceiptData &= "AMOUNT                 DATE                    TIME" & vbCrLf
            strReceiptData &= "RM9,999,900.00       27/03/2013           05:26 PM" & vbCrLf & vbCrLf
            strReceiptData &= "FROM                       TO                         BRANCH" & vbCrLf
            strReceiptData &= "                                  OTHER A/C          KL Main" & vbCrLf & vbCrLf
            strReceiptData &= "OTHER A/C             162227373456" & vbCrLf & vbCrLf
            strReceiptData &= "RM2 = 0                   RM5 = 0                RM10 = 0" & vbCrLf
            strReceiptData &= "RM20= 0                  RM50= 0               RM100= 99999" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
            strReceiptData &= "         RETAIN STATEMENT FOR YOUR RECORD" & vbCrLf
            strReceiptData &= "            THANK YOU FOR BANKING WITH US"
            txtSection1.Text = strReceiptData

            txtFileName1.Text = ""

            lblPrintMsg.Text = ""


            lblPrinterStatus.Text = "False"
            btnPaperOK.BackColor = Color.GreenYellow
            btnPaperLow.BackColor = Color.GreenYellow
            btnPaperEnd.BackColor = Color.GreenYellow
            btnPaperJam.BackColor = Color.GreenYellow
            btnPrinterFailed.BackColor = Color.GreenYellow


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

    Private Sub btnPrinterSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrinterSetting.Click
        Try
            'Printer Setting
            frmPrinterSetting.ShowDialog()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".btnPrinterSetting_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInit.Click
        Try


            lblPrintMsg.Text = ""

            If objReceiptPrinterInterface.StartDevice() = True Then
                blnStarDevice = True
                strLogMsg = "StartDevice - Init Printer Successfully"
                AppLogInfo(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
            Else
                blnStarDevice = False
                strLogMsg = "StartDevice - Init Printer Failed"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdInit_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim fileDialogBox As New OpenFileDialog()
        Dim strReceiptImage As String = String.Empty

        Try

            lblPrintMsg.Text = ""

            fileDialogBox.Title = "Select Logo - bmp format"
            fileDialogBox.Filter = "File (*.bmp)|*.bmp|(*.jpg)|*.jpg"

            If (fileDialogBox.ShowDialog = DialogResult.OK) Then
                txtFileName1.Text = fileDialogBox.FileName
                strReceiptImage = txtFileName1.Text
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdUpload_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdPrinterStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrinterStatus.Click
        Try
            lblPrintMsg.Text = ""

            If blnStarDevice = True Then
                If objReceiptPrinterInterface.DiagnosticDevice() = True Then
                    strLogMsg = "DiagnosticDevice - Printer Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                    'Printer All Status Reply Ok
                    PrinterStatusOk()
                Else
                    strLogMsg = "DiagnosticDevice - Printer Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Printer Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdPrinterStatus_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try

            lblPrintMsg.Text = ""

            If blnStarDevice = True Then
                blnStarDevice = False
                If objReceiptPrinterInterface.StopDevice() = True Then
                    strLogMsg = "StopDevice - Printer Successfully"
                    AppLogInfo(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                Else
                    strLogMsg = "StopDevice - Printer Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Printer Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdClose_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim blnCut As Boolean = False
        Dim blnFLag As Boolean = False

        Try

            lblPrintMsg.Text = "Printing ... Please Wait"

            If blnStarDevice = True Then
                'Check Printer Status
                If objReceiptPrinterInterface.DiagnosticDevice = True Then

                    'End Point Cut the Paper
                    blnFLag = objReceiptPrinterInterface.PrintText("==== END Print Test Page ====", True)

                    'Print Flag by events
                    If blnFLag = True Then
                        strLogMsg = "Print - Printer Successfully"
                        AppLogInfo(strLogMsg)
                        'MsgBox(strLogMsg, MsgBoxStyle.Information, "System Info")
                    Else
                        lblPrintMsg.Text = ""
                        strLogMsg = "Print - Printer Failed"
                        AppLogWarn(strLogMsg)
                        MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                    End If
                Else
                    lblPrintMsg.Text = ""
                    strLogMsg = "DiagnosticDevice - Printer Failed"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If

            Else
                lblPrintMsg.Text = ""
                strLogMsg = "Printer Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdPrint_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdSendTextToPrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSendTextToPrinter.Click
        Try

            lblPrintMsg.Text = ""

            If blnStarDevice = True Then
                'Print Text
                If txtSection1.Text.Length > 0 Then
                    'Print Command
                    objReceiptPrinterInterface.PrintText("==== Print Test Text ====", False)
                    objReceiptPrinterInterface.PrintText("", False)
                    objReceiptPrinterInterface.PrintText(txtSection1.Text, False)
                    objReceiptPrinterInterface.PrintText("", False)
                Else
                    strLogMsg = "Input Text is Empty"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Printer Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdSendTextToPrinter_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub cmdSendImgToPrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSendImgToPrinter.Click
        Try

            lblPrintMsg.Text = ""

            If blnStarDevice = True Then
                'Print Images
                If txtFileName1.Text.Length > 0 Then
                    'Print Command
                    objReceiptPrinterInterface.PrintText("==== Print Test Images ====", False)
                    objReceiptPrinterInterface.PrintText("", False)
                    objReceiptPrinterInterface.PrintImage(txtFileName1.Text, False)
                    objReceiptPrinterInterface.PrintText("", False)
                Else
                    strLogMsg = "Image path is Empty"
                    AppLogWarn(strLogMsg)
                    MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
                End If
            Else
                strLogMsg = "Printer Is Not Initialise"
                AppLogWarn(strLogMsg)
                MsgBox(strLogMsg, MsgBoxStyle.Exclamation, "System Error")
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".cmdSendImgToPrinter_Click. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#End Region


#Region "Printer Events"

    Private Sub objReceiptPrinterInterface_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objReceiptPrinterInterface.EvtDeviceDataReady
        Try
            AppLogInfo("objReceiptPrinterInterface_EvtDeviceDataReady")
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objReceiptPrinterInterface_EvtDeviceDataReady. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objReceiptPrinterInterface_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objReceiptPrinterInterface.EvtDeviceError
        Dim pStatus As String = ""
        Dim ArrStatusInfo() As String = Nothing

        Dim strPrinterStatus As String = ""

        Try

            AppLogWarn("objReceiptPrinterInterface_EvtDeviceError")
            AppLogWarn("Printer - Device Error. Sate:" & e.iDeviceState)

            'Printer Status
            pStatus = e.mDeviceDataValue

            'Get Printer Status in details
            ArrStatusInfo = Split(pStatus, "|", -1, CompareMethod.Text)
            AppLogWarn("Printer Reply Value=" & pStatus)
            AppLogWarn("Printer Status=" & ArrStatusInfo(0))
            AppLogWarn("Printer Failed=" & ArrStatusInfo(1))
            AppLogWarn("Paper OK=" & ArrStatusInfo(2))
            AppLogWarn("Paper Low=" & ArrStatusInfo(3))
            AppLogWarn("Paper End=" & ArrStatusInfo(4))
            AppLogWarn("Paper Jam=" & ArrStatusInfo(5))

            strPrinterStatus = "Printer Status Info:" & vbCrLf & "Printer Status=" & ArrStatusInfo(0) & vbCrLf
            strPrinterStatus &= "Printer Failed=" & ArrStatusInfo(1) & vbCrLf & "Paper OK=" & ArrStatusInfo(2) & vbCrLf
            strPrinterStatus &= "Paper Low=" & ArrStatusInfo(3) & vbCrLf & "Paper End=" & ArrStatusInfo(4) & vbCrLf
            strPrinterStatus &= "Paper Jam=" & ArrStatusInfo(5)

            MsgBox(strPrinterStatus, MsgBoxStyle.Critical, "Printer Status")

            'Display
            lblPrinterStatus.Text = ArrStatusInfo(0)

            If ArrStatusInfo(1) = "False" Then
                btnPrinterFailed.BackColor = Color.GreenYellow
            Else
                btnPrinterFailed.BackColor = Color.Red
            End If

            'Paper Ok
            If ArrStatusInfo(2) = "False" Then
                btnPaperOK.BackColor = Color.GreenYellow
            Else
                btnPaperOK.BackColor = Color.Red
            End If

            'Paper Low
            If ArrStatusInfo(3) = "False" Then
                btnPaperLow.BackColor = Color.GreenYellow
            Else
                btnPaperLow.BackColor = Color.Red
            End If

            'Paper End
            If ArrStatusInfo(4) = "False" Then
                btnPaperEnd.BackColor = Color.GreenYellow
            Else
                btnPaperEnd.BackColor = Color.Red
            End If

            'Paper Jam
            If ArrStatusInfo(5) = "False" Then
                btnPaperJam.BackColor = Color.GreenYellow
            Else
                btnPaperJam.BackColor = Color.Red
            End If

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objReceiptPrinterInterface_EvtDeviceError. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub objReceiptPrinterInterface_EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objReceiptPrinterInterface.EvtDeviceTimeout
        Try

            AppLogWarn("objReceiptPrinterInterface_EvtDeviceTimeout")


        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".objReceiptPrinterInterface_EvtDeviceTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#End Region

#Region "Support Method"

    Private Sub PrinterStatusOk()
        Try

            btnPrinterFailed.BackColor = Color.GreenYellow
            btnPaperOK.BackColor = Color.GreenYellow
            btnPaperLow.BackColor = Color.GreenYellow
            btnPaperEnd.BackColor = Color.GreenYellow
            btnPaperJam.BackColor = Color.GreenYellow

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".PrinterStatusOk. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

#End Region

   
End Class