Imports System
Imports System.IO
'Imports System.Reflection
'Imports System.Reflection.Emit
'Imports System.Windows.Forms

Imports ClsReceiptPrinterHWDInterface

Imports clsAppLogger.clsAppLoggerControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class frmReceiptPrinterControl

#Region "Form Variable"

    Dim strTitle As String = "frmReceiptPrinterControl"
    Dim blnStarDevice As Boolean = False

#End Region

#Region "Printer Object"

    Public WithEvents objReceiptPrinterInterface As ClsReceiptPrinterHWDInterface.clsHardwareLayer

#End Region

    
    Private Sub frmReceipPrinterControl_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

            If blnStarDevice = True Then
                objReceiptPrinterInterface.StopDevice()
            End If

            objReceiptPrinterInterface = Nothing

            AppLogInfo("== End Printer System Mode ==")

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmMaintenanceMode_FormClosed. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub frmReceiptPrinterControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AppLogInfo("== Start Printer System Mode ==")


            'Init the Object - PRINTER
            objReceiptPrinterInterface = New ClsReceiptPrinterHWDInterface.clsHardwareLayer

            'Init Display
            InitDisplay()

          
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".frmReceiptPrinterControl_Load. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#Region "Function"

    Private Sub InitDisplay()
        Try
            txtDeviceProperty.Text = ""

            'Receipt Data 
            strReceiptData = "TRANSACTION RECORD                      :Deposit" & vbCrLf & vbCrLf
            strReceiptData &= "AMOUNT                 DATE                    TIME" & vbCrLf
            strReceiptData &= "RM9,999,900.00       27/03/2013           05:26 PM" & vbCrLf & vbCrLf
            strReceiptData &= "FROM                       TO                         BRANCH" & vbCrLf
            strReceiptData &= "                                  OTHER A/C          KL Main" & vbCrLf & vbCrLf
            strReceiptData &= "OTHER A/C             162227373456" & vbCrLf & vbCrLf
            strReceiptData &= "RM2 = 0                   RM5 = 0                RM10 = 0" & vbCrLf
            strReceiptData &= "RM20= 0                  RM50= 0               RM100= 99999" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
            strReceiptData &= "         RETAIN STATEMENT FOR YOUR RECORD" & vbCrLf
            strReceiptData &= "            THANK YOU FOR BANKING WITH US"
            txtSection1.Text = strReceiptData

            txtFileName1.Text = ""
            txtFileName2.Text = ""
            txtFileName3.Text = ""

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

  

#End Region


#Region "Events Display"

    Private Sub ReceiptPrinterInfo(ByVal strInfo As String)
        Try
            strEvtMsg = strInfo.Trim
            UpdateEvtInfo()
        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
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
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub

    Private Sub EvtEventsMsg(ByVal strEvtMsgDisplay As String)
        Try

            txtDeviceProperty.SelectedText = strEvtMsgDisplay & Environment.NewLine
            txtDeviceProperty.ScrollToCaret()

            'Log Hardware Events
            AppLogInfo(strEvtMsgDisplay)

        Catch ex As Exception
            strErrMsg = "Error in " & strTitle & ".InitDisplay. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            MsgBox(strErrMsg, MsgBoxStyle.Critical, "SysError")
        End Try
    End Sub


#End Region

#Region "Printer - Command"


    Private Sub cmdInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInit.Click
        Try
            If objReceiptPrinterInterface.StartDevice() = True Then
                ReceiptPrinterInfo("StartDevice - Init Receipt Printer Successfully")
            Else
                ReceiptPrinterInfo("StartDevice - Init Receipt Printer Failed")
            End If
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdInit_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try
            If objReceiptPrinterInterface.StopDevice() = True Then
                ReceiptPrinterInfo("StopDevice - Receipt Printer Successfully")
            Else
                ReceiptPrinterInfo("StopDevice - Receipt Printer Failed")
            End If
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdClose_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdUnlockDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnlockDevice.Click
        Try
            If objReceiptPrinterInterface.UnlockDevice() = True Then
                ReceiptPrinterInfo("UnlockDevice - Receipt Printer Successfully")
            Else
                ReceiptPrinterInfo("UnlockDevice - Receipt Printer Failed")
            End If
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdUnlockDevice_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdLockDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLockDevice.Click
        Try
            If objReceiptPrinterInterface.LockDevice() = True Then
                ReceiptPrinterInfo("LockDevice - Receipt Printer Successfully")
            Else
                ReceiptPrinterInfo("LockDevice - Receipt Printer Failed")
            End If
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdLockDevice_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdWakeupDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWakeupDevice.Click
        Try
            If objReceiptPrinterInterface.UnlockDevice() = True Then
                ReceiptPrinterInfo("WakeupDevice - Receipt Printer Successfully")
            Else
                ReceiptPrinterInfo("WakeupDevice - Receipt Printer Failed")
             End If

        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdWakeupDevice_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdWrapDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrapDevice.Click
        Try
            If objReceiptPrinterInterface.WrapDevice() = True Then
                ReceiptPrinterInfo("WrapDevice - Receipt Printer Successfully")
            Else
                ReceiptPrinterInfo("WrapDevice - Receipt Printer Failed")
            End If
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdWrapDevice_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdDiagnosticDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDiagnosticDevice.Click
        Try
            If objReceiptPrinterInterface.DiagnosticDevice() = True Then
                ReceiptPrinterInfo("DiagnosticDevice - Receipt Printer Successfully")
            Else
                ReceiptPrinterInfo("DiagnosticDevice - Receipt Printer Failed")
            End If
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdDiagnosticDevice_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdGetDeviceProperty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetDeviceProperty.Click
        Try
            strEvtTxtMsg = ""

            strEvtMsg = "Txn Status:" & objReceiptPrinterInterface.TxnStatus
            UpdateEvtInfo()

            strEvtMsg = "Error Severity:" & objReceiptPrinterInterface.ErrorSeverity
            UpdateEvtInfo()

            strEvtMsg = "Supply Status:" & objReceiptPrinterInterface.SupplyStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatus:" & objReceiptPrinterInterface.MStatus
            UpdateEvtInfo()

            strEvtMsg = "MStatusData:" & objReceiptPrinterInterface.MStatusData
            UpdateEvtInfo()

        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdGetDeviceProperty_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdClearText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearText.Click
        Try
            txtDeviceProperty.Text = ""
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdClearText_Click:" & ex.Message)
        End Try
    End Sub


    Private Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim fileDialogBox As New OpenFileDialog()
        Dim strReceiptImage As String = String.Empty

        Try
            fileDialogBox.Title = "Select Logo - bmp format"
            fileDialogBox.Filter = "File (*.bmp)|*.bmp|(*.jpg)|*.jpg"

            If (fileDialogBox.ShowDialog = DialogResult.OK) Then
                txtFileName1.Text = fileDialogBox.FileName
                strReceiptImage = txtFileName1.Text
            End If

        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdUpload_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdUpload3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload3.Click
        Dim fileDialogBox As New OpenFileDialog()
        Dim strReceiptImage As String = String.Empty

        Try
            fileDialogBox.Title = "Select Logo - bmp format"
            fileDialogBox.Filter = "File (*.bmp)|*.bmp|(*.jpg)|*.jpg"

            If (fileDialogBox.ShowDialog = DialogResult.OK) Then
                txtFileName3.Text = fileDialogBox.FileName
                strReceiptImage = txtFileName3.Text
            End If

        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdUpload3_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdUpload2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload2.Click
        Dim fileDialogBox As New OpenFileDialog()
        Dim strReceiptImage As String = String.Empty

        Try
            fileDialogBox.Title = "Select Logo - bmp format"
            fileDialogBox.Filter = "File (*.bmp)|*.bmp|(*.jpg)|*.jpg"

            If (fileDialogBox.ShowDialog = DialogResult.OK) Then
                txtFileName2.Text = fileDialogBox.FileName
                strReceiptImage = txtFileName2.Text
            End If

        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdUpload2_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim blnCut As Boolean = False
        Dim blnFLag As Boolean = False


        Try

            If objReceiptPrinterInterface.DiagnosticDevice = True Then

                If txtFileName1.Text.Length > 0 Then
                    If chkCutPaper.Checked = True Then
                        blnFLag = objReceiptPrinterInterface.PrintImage(txtFileName1.Text, True)
                    Else
                        blnFLag = objReceiptPrinterInterface.PrintImage(txtFileName1.Text, False)
                    End If
                End If

                If txtSection1.Text.Length > 0 Then
                    If chkCutPaper2.Checked = True Then
                        blnFLag = objReceiptPrinterInterface.PrintText(txtSection1.Text, True)
                    Else
                        blnFLag = objReceiptPrinterInterface.PrintText(txtSection1.Text, False)
                    End If
                End If

                If txtFileName2.Text.Length > 0 Then
                    If chkCutPaper3.Checked = True Then
                        blnFLag = objReceiptPrinterInterface.PrintImage(txtFileName2.Text, True)
                    Else
                        blnFLag = objReceiptPrinterInterface.PrintImage(txtFileName2.Text, False)
                    End If
                End If

                If txtSection2.Text.Length > 0 Then
                    If chkCutPaper4.Checked = True Then
                        blnFLag = objReceiptPrinterInterface.PrintText(txtSection2.Text, True)
                    Else
                        blnFLag = objReceiptPrinterInterface.PrintText(txtSection2.Text, False)
                    End If
                End If

                If txtFileName3.Text.Length > 0 Then
                    If chkCutPaper5.Checked = True Then
                        blnFLag = objReceiptPrinterInterface.PrintImage(txtFileName3.Text, True)
                    Else
                        blnFLag = objReceiptPrinterInterface.PrintImage(txtFileName3.Text, False)
                    End If
                End If

                If blnFLag = True Then
                    ReceiptPrinterInfo("Print - Receipt Printer Successfully")
                Else
                    ReceiptPrinterInfo("Print - Receipt Printer Failed")
                End If

            Else
                ReceiptPrinterInfo("Print - Receipt Printer Failed")
            End If

        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdPrint_Click:" & ex.Message)
        End Try
    End Sub

    Private Sub cmdClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearAll.Click
        Try
            txtFileName1.Text = ""
            txtFileName2.Text = ""
            txtFileName3.Text = ""
            txtSection1.Text = ""
            txtSection2.Text = ""
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdClearAll_Click:" & ex.Message)
        End Try
    End Sub


    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Try
            'Unload Me
            Me.Close()
        Catch ex As Exception
            ReceiptPrinterInfo("Error in cmdExit_Click:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Printer Events"

    Private Sub objReceiptPrinterInterface_EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objReceiptPrinterInterface.EvtDeviceDataReady
        Try
            ReceiptPrinterInfo("Printer - Device Data Ready. State:" & e.iDeviceState)
        Catch ex As Exception
            ReceiptPrinterInfo("Error in objReceiptPrinterInterface_EvtDeviceDataReady:" & ex.Message)
        End Try
    End Sub

    Private Sub objReceiptPrinterInterface_EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs) Handles objReceiptPrinterInterface.EvtDeviceError
        Dim pStatus As String = String.Empty

        Try
            ReceiptPrinterInfo("Printer - Device Error. Sate:" & e.iDeviceState)

            'Printer Status
            pStatus = e.mDeviceDataValue
            ReceiptPrinterInfo("Printer Status =" & pStatus)

        Catch ex As Exception
            ReceiptPrinterInfo("Error in objReceiptPrinterInterface_EvtDeviceError:" & ex.Message)
        End Try
    End Sub

#End Region



 
  
End Class
