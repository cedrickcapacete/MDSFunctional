<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChequeDepositorApp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChequeDepositorApp))
        Me.btnChqDiagDeviceNDC = New System.Windows.Forms.Button()
        Me.btnChqWakeDeviceNDC = New System.Windows.Forms.Button()
        Me.btnChqClearText = New System.Windows.Forms.Button()
        Me.btnChqGetDevProp = New System.Windows.Forms.Button()
        Me.btnChqUnlockDeviceNDC = New System.Windows.Forms.Button()
        Me.btnChqLockDeviceNDC = New System.Windows.Forms.Button()
        Me.btnChqWrapDeviceNDC = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.RichTextBox()
        Me.btnChqStopDevice = New System.Windows.Forms.Button()
        Me.btnChqStartDevice = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdClearPaperPath = New System.Windows.Forms.Button()
        Me.cboLightChoice = New System.Windows.Forms.ComboBox()
        Me.cboCRLightChoice = New System.Windows.Forms.ComboBox()
        Me.cmdStopMDSCRLight = New System.Windows.Forms.Button()
        Me.cmdStartMDSCRLight = New System.Windows.Forms.Button()
        Me.cmdStopMDSLight = New System.Windows.Forms.Button()
        Me.cmdStartMDSLight = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboChqWakeupState = New System.Windows.Forms.ComboBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnChqDiagDeviceNDC
        '
        Me.btnChqDiagDeviceNDC.Location = New System.Drawing.Point(21, 140)
        Me.btnChqDiagDeviceNDC.Name = "btnChqDiagDeviceNDC"
        Me.btnChqDiagDeviceNDC.Size = New System.Drawing.Size(195, 35)
        Me.btnChqDiagDeviceNDC.TabIndex = 11
        Me.btnChqDiagDeviceNDC.Text = "DiagnosticDevice"
        Me.btnChqDiagDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnChqWakeDeviceNDC
        '
        Me.btnChqWakeDeviceNDC.Location = New System.Drawing.Point(427, 90)
        Me.btnChqWakeDeviceNDC.Name = "btnChqWakeDeviceNDC"
        Me.btnChqWakeDeviceNDC.Size = New System.Drawing.Size(264, 35)
        Me.btnChqWakeDeviceNDC.TabIndex = 10
        Me.btnChqWakeDeviceNDC.Text = "WakeUpDevice"
        Me.btnChqWakeDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnChqClearText
        '
        Me.btnChqClearText.Location = New System.Drawing.Point(574, 305)
        Me.btnChqClearText.Name = "btnChqClearText"
        Me.btnChqClearText.Size = New System.Drawing.Size(128, 35)
        Me.btnChqClearText.TabIndex = 12
        Me.btnChqClearText.Text = "Clear Text"
        Me.btnChqClearText.UseVisualStyleBackColor = True
        '
        'btnChqGetDevProp
        '
        Me.btnChqGetDevProp.Location = New System.Drawing.Point(234, 140)
        Me.btnChqGetDevProp.Name = "btnChqGetDevProp"
        Me.btnChqGetDevProp.Size = New System.Drawing.Size(199, 35)
        Me.btnChqGetDevProp.TabIndex = 14
        Me.btnChqGetDevProp.Text = "GetDeviceProperty"
        Me.btnChqGetDevProp.UseVisualStyleBackColor = True
        '
        'btnChqUnlockDeviceNDC
        '
        Me.btnChqUnlockDeviceNDC.Location = New System.Drawing.Point(562, 48)
        Me.btnChqUnlockDeviceNDC.Name = "btnChqUnlockDeviceNDC"
        Me.btnChqUnlockDeviceNDC.Size = New System.Drawing.Size(128, 35)
        Me.btnChqUnlockDeviceNDC.TabIndex = 13
        Me.btnChqUnlockDeviceNDC.Text = "UnlockDevice"
        Me.btnChqUnlockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnChqLockDeviceNDC
        '
        Me.btnChqLockDeviceNDC.Location = New System.Drawing.Point(427, 48)
        Me.btnChqLockDeviceNDC.Name = "btnChqLockDeviceNDC"
        Me.btnChqLockDeviceNDC.Size = New System.Drawing.Size(128, 35)
        Me.btnChqLockDeviceNDC.TabIndex = 7
        Me.btnChqLockDeviceNDC.Text = "LockDevice"
        Me.btnChqLockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnChqWrapDeviceNDC
        '
        Me.btnChqWrapDeviceNDC.Location = New System.Drawing.Point(292, 48)
        Me.btnChqWrapDeviceNDC.Name = "btnChqWrapDeviceNDC"
        Me.btnChqWrapDeviceNDC.Size = New System.Drawing.Size(128, 35)
        Me.btnChqWrapDeviceNDC.TabIndex = 6
        Me.btnChqWrapDeviceNDC.Text = "WrapDevice"
        Me.btnChqWrapDeviceNDC.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(14, 381)
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ReadOnly = True
        Me.txtDeviceProperty.Size = New System.Drawing.Size(754, 181)
        Me.txtDeviceProperty.TabIndex = 5
        Me.txtDeviceProperty.Text = ""
        '
        'btnChqStopDevice
        '
        Me.btnChqStopDevice.Location = New System.Drawing.Point(156, 48)
        Me.btnChqStopDevice.Name = "btnChqStopDevice"
        Me.btnChqStopDevice.Size = New System.Drawing.Size(128, 35)
        Me.btnChqStopDevice.TabIndex = 8
        Me.btnChqStopDevice.Text = "StopDevice"
        Me.btnChqStopDevice.UseVisualStyleBackColor = True
        '
        'btnChqStartDevice
        '
        Me.btnChqStartDevice.Location = New System.Drawing.Point(21, 48)
        Me.btnChqStartDevice.Name = "btnChqStartDevice"
        Me.btnChqStartDevice.Size = New System.Drawing.Size(128, 35)
        Me.btnChqStartDevice.TabIndex = 9
        Me.btnChqStartDevice.Text = "StartDevice"
        Me.btnChqStartDevice.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdExit)
        Me.GroupBox1.Controls.Add(Me.cmdClearPaperPath)
        Me.GroupBox1.Controls.Add(Me.cboLightChoice)
        Me.GroupBox1.Controls.Add(Me.cboCRLightChoice)
        Me.GroupBox1.Controls.Add(Me.cmdStopMDSCRLight)
        Me.GroupBox1.Controls.Add(Me.cmdStartMDSCRLight)
        Me.GroupBox1.Controls.Add(Me.cmdStopMDSLight)
        Me.GroupBox1.Controls.Add(Me.cmdStartMDSLight)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cboChqWakeupState)
        Me.GroupBox1.Controls.Add(Me.btnChqStopDevice)
        Me.GroupBox1.Controls.Add(Me.btnChqClearText)
        Me.GroupBox1.Controls.Add(Me.btnChqGetDevProp)
        Me.GroupBox1.Controls.Add(Me.btnChqDiagDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnChqStartDevice)
        Me.GroupBox1.Controls.Add(Me.btnChqWakeDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnChqWrapDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnChqLockDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnChqUnlockDeviceNDC)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(14, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(754, 360)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cheque Depositor"
        '
        'cmdClearPaperPath
        '
        Me.cmdClearPaperPath.Location = New System.Drawing.Point(441, 140)
        Me.cmdClearPaperPath.Name = "cmdClearPaperPath"
        Me.cmdClearPaperPath.Size = New System.Drawing.Size(250, 35)
        Me.cmdClearPaperPath.TabIndex = 32
        Me.cmdClearPaperPath.Text = "Clear Paper Path"
        Me.cmdClearPaperPath.UseVisualStyleBackColor = True
        '
        'cboLightChoice
        '
        Me.cboLightChoice.FormattingEnabled = True
        Me.cboLightChoice.Items.AddRange(New Object() {"0-ON", "1-OFF"})
        Me.cboLightChoice.Location = New System.Drawing.Point(21, 241)
        Me.cboLightChoice.Name = "cboLightChoice"
        Me.cboLightChoice.Size = New System.Drawing.Size(191, 23)
        Me.cboLightChoice.TabIndex = 31
        Me.cboLightChoice.Text = "0-ON"
        '
        'cboCRLightChoice
        '
        Me.cboCRLightChoice.FormattingEnabled = True
        Me.cboCRLightChoice.Items.AddRange(New Object() {"0-ON", "1-OFF", "2-Flashing FAST", "3-Flashing SLOW"})
        Me.cboCRLightChoice.Location = New System.Drawing.Point(21, 189)
        Me.cboCRLightChoice.Name = "cboCRLightChoice"
        Me.cboCRLightChoice.Size = New System.Drawing.Size(194, 23)
        Me.cboCRLightChoice.TabIndex = 30
        Me.cboCRLightChoice.Text = "0-ON"
        '
        'cmdStopMDSCRLight
        '
        Me.cmdStopMDSCRLight.Location = New System.Drawing.Point(355, 180)
        Me.cmdStopMDSCRLight.Name = "cmdStopMDSCRLight"
        Me.cmdStopMDSCRLight.Size = New System.Drawing.Size(141, 48)
        Me.cmdStopMDSCRLight.TabIndex = 29
        Me.cmdStopMDSCRLight.Text = "Stop MDS Reader Light"
        Me.cmdStopMDSCRLight.UseVisualStyleBackColor = True
        '
        'cmdStartMDSCRLight
        '
        Me.cmdStartMDSCRLight.Location = New System.Drawing.Point(219, 181)
        Me.cmdStartMDSCRLight.Name = "cmdStartMDSCRLight"
        Me.cmdStartMDSCRLight.Size = New System.Drawing.Size(128, 47)
        Me.cmdStartMDSCRLight.TabIndex = 28
        Me.cmdStartMDSCRLight.Text = "MDS Reader Light"
        Me.cmdStartMDSCRLight.UseVisualStyleBackColor = True
        '
        'cmdStopMDSLight
        '
        Me.cmdStopMDSLight.Location = New System.Drawing.Point(359, 233)
        Me.cmdStopMDSLight.Name = "cmdStopMDSLight"
        Me.cmdStopMDSLight.Size = New System.Drawing.Size(136, 35)
        Me.cmdStopMDSLight.TabIndex = 27
        Me.cmdStopMDSLight.Text = "Stop MDS Light"
        Me.cmdStopMDSLight.UseVisualStyleBackColor = True
        '
        'cmdStartMDSLight
        '
        Me.cmdStartMDSLight.Location = New System.Drawing.Point(215, 232)
        Me.cmdStartMDSLight.Name = "cmdStartMDSLight"
        Me.cmdStartMDSLight.Size = New System.Drawing.Size(136, 35)
        Me.cmdStartMDSLight.TabIndex = 26
        Me.cmdStartMDSLight.Text = "MDS Light"
        Me.cmdStartMDSLight.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Device State:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Methods"
        '
        'cboChqWakeupState
        '
        Me.cboChqWakeupState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboChqWakeupState.FormattingEnabled = True
        Me.cboChqWakeupState.Items.AddRange(New Object() {"0-No Chq Deposit:Close Feeder", "1-Open Feeder", "2-Insert More", "3-Event:Reject", "4-Refund", "5-Event:Escrowed", "6-Event:Processing", "7-Confirming", "8-Event:Complete", "A-UserTimeout"})
        Me.cboChqWakeupState.Location = New System.Drawing.Point(124, 90)
        Me.cboChqWakeupState.Name = "cboChqWakeupState"
        Me.cboChqWakeupState.Size = New System.Drawing.Size(296, 23)
        Me.cboChqWakeupState.TabIndex = 15
        Me.cboChqWakeupState.Text = "0-No Chq Deposit:Close Feeder"
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(562, 250)
        Me.cmdExit.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(147, 37)
        Me.cmdExit.TabIndex = 35
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'ChequeDepositorApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 572)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtDeviceProperty)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ChequeDepositorApp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS: Cheque Depositor"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnChqDiagDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnChqWakeDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnChqClearText As System.Windows.Forms.Button
    Friend WithEvents btnChqGetDevProp As System.Windows.Forms.Button
    Friend WithEvents btnChqUnlockDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnChqLockDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnChqWrapDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.RichTextBox
    Friend WithEvents btnChqStopDevice As System.Windows.Forms.Button
    Friend WithEvents btnChqStartDevice As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboChqWakeupState As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdStopMDSCRLight As System.Windows.Forms.Button
    Friend WithEvents cmdStartMDSCRLight As System.Windows.Forms.Button
    Friend WithEvents cmdStopMDSLight As System.Windows.Forms.Button
    Friend WithEvents cmdStartMDSLight As System.Windows.Forms.Button
    Friend WithEvents cboLightChoice As System.Windows.Forms.ComboBox
    Friend WithEvents cboCRLightChoice As System.Windows.Forms.ComboBox
    Friend WithEvents cmdClearPaperPath As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button

End Class
