<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CashDepositorApp
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CashDepositorApp))
        Me.btnDiagDeviceNDC = New System.Windows.Forms.Button()
        Me.btnWakeDeviceNDC = New System.Windows.Forms.Button()
        Me.btnClearText = New System.Windows.Forms.Button()
        Me.btnGetDevProp = New System.Windows.Forms.Button()
        Me.btnUnlockDeviceNDC = New System.Windows.Forms.Button()
        Me.btnLockDeviceNDC = New System.Windows.Forms.Button()
        Me.btnWrapDeviceNDC = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.RichTextBox()
        Me.btnStopDeviceNDC = New System.Windows.Forms.Button()
        Me.btnStartDeviceNDC = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboLogicalBox = New System.Windows.Forms.ComboBox()
        Me.cmdGetNoteInBox = New System.Windows.Forms.Button()
        Me.cmdGetMDSStatus = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdClearPaperPath = New System.Windows.Forms.Button()
        Me.cboLightChoice = New System.Windows.Forms.ComboBox()
        Me.cboCRLightChoice = New System.Windows.Forms.ComboBox()
        Me.cmdStopMDSCRLight = New System.Windows.Forms.Button()
        Me.cmdStartMDSCRLight = New System.Windows.Forms.Button()
        Me.cmdStopMDSLight = New System.Windows.Forms.Button()
        Me.cmdStartMDSLight = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnResetCashCnt = New System.Windows.Forms.Button()
        Me.btnGetCashCounter = New System.Windows.Forms.Button()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.cboWakeupState = New System.Windows.Forms.ComboBox()
        Me.btnAmount = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDiagDeviceNDC
        '
        Me.btnDiagDeviceNDC.Location = New System.Drawing.Point(217, 123)
        Me.btnDiagDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnDiagDeviceNDC.Name = "btnDiagDeviceNDC"
        Me.btnDiagDeviceNDC.Size = New System.Drawing.Size(207, 37)
        Me.btnDiagDeviceNDC.TabIndex = 11
        Me.btnDiagDeviceNDC.Text = "DiagnosticDevice"
        Me.btnDiagDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWakeDeviceNDC
        '
        Me.btnWakeDeviceNDC.Location = New System.Drawing.Point(370, 82)
        Me.btnWakeDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnWakeDeviceNDC.Name = "btnWakeDeviceNDC"
        Me.btnWakeDeviceNDC.Size = New System.Drawing.Size(232, 37)
        Me.btnWakeDeviceNDC.TabIndex = 10
        Me.btnWakeDeviceNDC.Text = "WakeUpDevice()"
        Me.btnWakeDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnClearText
        '
        Me.btnClearText.Location = New System.Drawing.Point(572, 325)
        Me.btnClearText.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnClearText.Name = "btnClearText"
        Me.btnClearText.Size = New System.Drawing.Size(147, 37)
        Me.btnClearText.TabIndex = 12
        Me.btnClearText.Text = "Clear Text"
        Me.btnClearText.UseVisualStyleBackColor = True
        '
        'btnGetDevProp
        '
        Me.btnGetDevProp.Location = New System.Drawing.Point(432, 122)
        Me.btnGetDevProp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGetDevProp.Name = "btnGetDevProp"
        Me.btnGetDevProp.Size = New System.Drawing.Size(170, 37)
        Me.btnGetDevProp.TabIndex = 14
        Me.btnGetDevProp.Text = "GetDeviceProperty"
        Me.btnGetDevProp.UseVisualStyleBackColor = True
        '
        'btnUnlockDeviceNDC
        '
        Me.btnUnlockDeviceNDC.Location = New System.Drawing.Point(485, 42)
        Me.btnUnlockDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnUnlockDeviceNDC.Name = "btnUnlockDeviceNDC"
        Me.btnUnlockDeviceNDC.Size = New System.Drawing.Size(118, 37)
        Me.btnUnlockDeviceNDC.TabIndex = 13
        Me.btnUnlockDeviceNDC.Text = "UnlockDevice"
        Me.btnUnlockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnLockDeviceNDC
        '
        Me.btnLockDeviceNDC.Location = New System.Drawing.Point(370, 43)
        Me.btnLockDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnLockDeviceNDC.Name = "btnLockDeviceNDC"
        Me.btnLockDeviceNDC.Size = New System.Drawing.Size(107, 37)
        Me.btnLockDeviceNDC.TabIndex = 7
        Me.btnLockDeviceNDC.Text = "LockDevice"
        Me.btnLockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWrapDeviceNDC
        '
        Me.btnWrapDeviceNDC.Location = New System.Drawing.Point(253, 43)
        Me.btnWrapDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnWrapDeviceNDC.Name = "btnWrapDeviceNDC"
        Me.btnWrapDeviceNDC.Size = New System.Drawing.Size(109, 37)
        Me.btnWrapDeviceNDC.TabIndex = 6
        Me.btnWrapDeviceNDC.Text = "WrapDevice"
        Me.btnWrapDeviceNDC.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(13, 392)
        Me.txtDeviceProperty.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ReadOnly = True
        Me.txtDeviceProperty.Size = New System.Drawing.Size(768, 167)
        Me.txtDeviceProperty.TabIndex = 5
        Me.txtDeviceProperty.Text = ""
        '
        'btnStopDeviceNDC
        '
        Me.btnStopDeviceNDC.Location = New System.Drawing.Point(134, 42)
        Me.btnStopDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnStopDeviceNDC.Name = "btnStopDeviceNDC"
        Me.btnStopDeviceNDC.Size = New System.Drawing.Size(112, 37)
        Me.btnStopDeviceNDC.TabIndex = 8
        Me.btnStopDeviceNDC.Text = "StopDevice"
        Me.btnStopDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnStartDeviceNDC
        '
        Me.btnStartDeviceNDC.Location = New System.Drawing.Point(14, 43)
        Me.btnStartDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnStartDeviceNDC.Name = "btnStartDeviceNDC"
        Me.btnStartDeviceNDC.Size = New System.Drawing.Size(112, 37)
        Me.btnStartDeviceNDC.TabIndex = 9
        Me.btnStartDeviceNDC.Text = "StartDevice"
        Me.btnStartDeviceNDC.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdExit)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboLogicalBox)
        Me.GroupBox1.Controls.Add(Me.cmdGetNoteInBox)
        Me.GroupBox1.Controls.Add(Me.cmdGetMDSStatus)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmdClearPaperPath)
        Me.GroupBox1.Controls.Add(Me.cboLightChoice)
        Me.GroupBox1.Controls.Add(Me.cboCRLightChoice)
        Me.GroupBox1.Controls.Add(Me.cmdStopMDSCRLight)
        Me.GroupBox1.Controls.Add(Me.cmdStartMDSCRLight)
        Me.GroupBox1.Controls.Add(Me.cmdStopMDSLight)
        Me.GroupBox1.Controls.Add(Me.cmdStartMDSLight)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnResetCashCnt)
        Me.GroupBox1.Controls.Add(Me.btnGetCashCounter)
        Me.GroupBox1.Controls.Add(Me.txtAmount)
        Me.GroupBox1.Controls.Add(Me.cboWakeupState)
        Me.GroupBox1.Controls.Add(Me.btnStopDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnClearText)
        Me.GroupBox1.Controls.Add(Me.btnGetDevProp)
        Me.GroupBox1.Controls.Add(Me.btnDiagDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnStartDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnAmount)
        Me.GroupBox1.Controls.Add(Me.btnWakeDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnWrapDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnLockDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnUnlockDeviceNDC)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(16, 14)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(765, 370)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cash Depositor"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(169, 222)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Logical Box:"
        '
        'cboLogicalBox
        '
        Me.cboLogicalBox.FormattingEnabled = True
        Me.cboLogicalBox.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8"})
        Me.cboLogicalBox.Location = New System.Drawing.Point(280, 215)
        Me.cboLogicalBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboLogicalBox.Name = "cboLogicalBox"
        Me.cboLogicalBox.Size = New System.Drawing.Size(82, 23)
        Me.cboLogicalBox.TabIndex = 32
        Me.cboLogicalBox.Text = "0"
        '
        'cmdGetNoteInBox
        '
        Me.cmdGetNoteInBox.Location = New System.Drawing.Point(372, 210)
        Me.cmdGetNoteInBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdGetNoteInBox.Name = "cmdGetNoteInBox"
        Me.cmdGetNoteInBox.Size = New System.Drawing.Size(192, 38)
        Me.cmdGetNoteInBox.TabIndex = 31
        Me.cmdGetNoteInBox.Text = "Get NoteInBox"
        Me.cmdGetNoteInBox.UseVisualStyleBackColor = True
        '
        'cmdGetMDSStatus
        '
        Me.cmdGetMDSStatus.Location = New System.Drawing.Point(572, 212)
        Me.cmdGetMDSStatus.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdGetMDSStatus.Name = "cmdGetMDSStatus"
        Me.cmdGetMDSStatus.Size = New System.Drawing.Size(135, 38)
        Me.cmdGetMDSStatus.TabIndex = 30
        Me.cmdGetMDSStatus.Text = "Get MDS Status"
        Me.cmdGetMDSStatus.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(25, 234)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Light Status:"
        '
        'cmdClearPaperPath
        '
        Me.cmdClearPaperPath.Location = New System.Drawing.Point(572, 167)
        Me.cmdClearPaperPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdClearPaperPath.Name = "cmdClearPaperPath"
        Me.cmdClearPaperPath.Size = New System.Drawing.Size(135, 37)
        Me.cmdClearPaperPath.TabIndex = 28
        Me.cmdClearPaperPath.Text = "Clear Paper Path"
        Me.cmdClearPaperPath.UseVisualStyleBackColor = True
        '
        'cboLightChoice
        '
        Me.cboLightChoice.FormattingEnabled = True
        Me.cboLightChoice.Items.AddRange(New Object() {"0-ON", "1-OFF"})
        Me.cboLightChoice.Location = New System.Drawing.Point(18, 324)
        Me.cboLightChoice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboLightChoice.Name = "cboLightChoice"
        Me.cboLightChoice.Size = New System.Drawing.Size(214, 23)
        Me.cboLightChoice.TabIndex = 27
        Me.cboLightChoice.Text = "0-ON"
        '
        'cboCRLightChoice
        '
        Me.cboCRLightChoice.FormattingEnabled = True
        Me.cboCRLightChoice.Items.AddRange(New Object() {"0-ON", "1-OFF", "2-Flashing FAST", "3-Flashing SLOW"})
        Me.cboCRLightChoice.Location = New System.Drawing.Point(18, 274)
        Me.cboCRLightChoice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboCRLightChoice.Name = "cboCRLightChoice"
        Me.cboCRLightChoice.Size = New System.Drawing.Size(214, 23)
        Me.cboCRLightChoice.TabIndex = 26
        Me.cboCRLightChoice.Text = "0-ON"
        '
        'cmdStopMDSCRLight
        '
        Me.cmdStopMDSCRLight.Location = New System.Drawing.Point(408, 258)
        Me.cmdStopMDSCRLight.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdStopMDSCRLight.Name = "cmdStopMDSCRLight"
        Me.cmdStopMDSCRLight.Size = New System.Drawing.Size(156, 50)
        Me.cmdStopMDSCRLight.TabIndex = 25
        Me.cmdStopMDSCRLight.Text = "Stop MDS Reader Light"
        Me.cmdStopMDSCRLight.UseVisualStyleBackColor = True
        '
        'cmdStartMDSCRLight
        '
        Me.cmdStartMDSCRLight.Location = New System.Drawing.Point(240, 254)
        Me.cmdStartMDSCRLight.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdStartMDSCRLight.Name = "cmdStartMDSCRLight"
        Me.cmdStartMDSCRLight.Size = New System.Drawing.Size(160, 54)
        Me.cmdStartMDSCRLight.TabIndex = 24
        Me.cmdStartMDSCRLight.Text = "MDS Reader Light"
        Me.cmdStartMDSCRLight.UseVisualStyleBackColor = True
        '
        'cmdStopMDSLight
        '
        Me.cmdStopMDSLight.Location = New System.Drawing.Point(408, 314)
        Me.cmdStopMDSLight.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdStopMDSLight.Name = "cmdStopMDSLight"
        Me.cmdStopMDSLight.Size = New System.Drawing.Size(156, 50)
        Me.cmdStopMDSLight.TabIndex = 23
        Me.cmdStopMDSLight.Text = "Stop MDS Light"
        Me.cmdStopMDSLight.UseVisualStyleBackColor = True
        '
        'cmdStartMDSLight
        '
        Me.cmdStartMDSLight.Location = New System.Drawing.Point(240, 313)
        Me.cmdStartMDSLight.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdStartMDSLight.Name = "cmdStartMDSLight"
        Me.cmdStartMDSLight.Size = New System.Drawing.Size(160, 48)
        Me.cmdStartMDSLight.TabIndex = 22
        Me.cmdStartMDSLight.Text = "MDS Light"
        Me.cmdStartMDSLight.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 92)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Device State:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 23)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Methods"
        '
        'btnResetCashCnt
        '
        Me.btnResetCashCnt.Location = New System.Drawing.Point(18, 167)
        Me.btnResetCashCnt.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnResetCashCnt.Name = "btnResetCashCnt"
        Me.btnResetCashCnt.Size = New System.Drawing.Size(192, 37)
        Me.btnResetCashCnt.TabIndex = 17
        Me.btnResetCashCnt.Text = "Reset Cash Counter"
        Me.btnResetCashCnt.UseVisualStyleBackColor = True
        '
        'btnGetCashCounter
        '
        Me.btnGetCashCounter.Location = New System.Drawing.Point(18, 123)
        Me.btnGetCashCounter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGetCashCounter.Name = "btnGetCashCounter"
        Me.btnGetCashCounter.Size = New System.Drawing.Size(192, 37)
        Me.btnGetCashCounter.TabIndex = 17
        Me.btnGetCashCounter.Text = "Get Cash Counter"
        Me.btnGetCashCounter.UseVisualStyleBackColor = True
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(372, 167)
        Me.txtAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(191, 21)
        Me.txtAmount.TabIndex = 16
        '
        'cboWakeupState
        '
        Me.cboWakeupState.FormattingEnabled = True
        Me.cboWakeupState.Items.AddRange(New Object() {"0-No Cash Deposit:Close Feeder", "1-Open Feeder", "2-Insert More", "3-Event:Reject", "4-Refund", "5-Event:Escrowed", "6-Event:Processing", "7-Confirming", "8-Event:Complete", "A-UserTimeOut"})
        Me.cboWakeupState.Location = New System.Drawing.Point(96, 88)
        Me.cboWakeupState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboWakeupState.Name = "cboWakeupState"
        Me.cboWakeupState.Size = New System.Drawing.Size(241, 23)
        Me.cboWakeupState.TabIndex = 15
        Me.cboWakeupState.Text = "0-No Cash Deposit:Close Feeder"
        '
        'btnAmount
        '
        Me.btnAmount.Location = New System.Drawing.Point(217, 167)
        Me.btnAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAmount.Name = "btnAmount"
        Me.btnAmount.Size = New System.Drawing.Size(147, 37)
        Me.btnAmount.TabIndex = 10
        Me.btnAmount.Text = "Amount:"
        Me.btnAmount.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(572, 263)
        Me.cmdExit.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(147, 37)
        Me.cmdExit.TabIndex = 34
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'CashDepositorApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 572)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtDeviceProperty)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CashDepositorApp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS: Cash Depositor"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDiagDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnWakeDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnClearText As System.Windows.Forms.Button
    Friend WithEvents btnGetDevProp As System.Windows.Forms.Button
    Friend WithEvents btnUnlockDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnLockDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnWrapDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.RichTextBox
    Friend WithEvents btnStopDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnStartDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboWakeupState As System.Windows.Forms.ComboBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents btnAmount As System.Windows.Forms.Button
    Friend WithEvents btnResetCashCnt As System.Windows.Forms.Button
    Friend WithEvents btnGetCashCounter As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdStopMDSCRLight As System.Windows.Forms.Button
    Friend WithEvents cmdStartMDSCRLight As System.Windows.Forms.Button
    Friend WithEvents cmdStopMDSLight As System.Windows.Forms.Button
    Friend WithEvents cmdStartMDSLight As System.Windows.Forms.Button
    Friend WithEvents cboLightChoice As System.Windows.Forms.ComboBox
    Friend WithEvents cboCRLightChoice As System.Windows.Forms.ComboBox
    Friend WithEvents cmdClearPaperPath As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdGetMDSStatus As System.Windows.Forms.Button
    Friend WithEvents cmdGetNoteInBox As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboLogicalBox As System.Windows.Forms.ComboBox
    Friend WithEvents cmdExit As System.Windows.Forms.Button

End Class
