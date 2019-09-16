<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CardReaderMainApp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CardReaderMainApp))
        Me.txtDeviceProperty = New System.Windows.Forms.RichTextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtInterval = New System.Windows.Forms.TextBox()
        Me.txttmID = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdSetTimeOut = New System.Windows.Forms.Button()
        Me.cmdPMPCCardSerialNo = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCardType = New System.Windows.Forms.ComboBox()
        Me.cbState = New System.Windows.Forms.ComboBox()
        Me.btnTrack1 = New System.Windows.Forms.Button()
        Me.btnTrack3 = New System.Windows.Forms.Button()
        Me.btnTrack2 = New System.Windows.Forms.Button()
        Me.btnReadCard = New System.Windows.Forms.Button()
        Me.btnDiagDeviceNDC = New System.Windows.Forms.Button()
        Me.btnWakeDeviceNDC = New System.Windows.Forms.Button()
        Me.btnClearText = New System.Windows.Forms.Button()
        Me.btnGetDevProp = New System.Windows.Forms.Button()
        Me.btnUnlockDeviceNDC = New System.Windows.Forms.Button()
        Me.btnLockDeviceNDC = New System.Windows.Forms.Button()
        Me.btnWrapDeviceNDC = New System.Windows.Forms.Button()
        Me.btnStopDeviceNDC = New System.Windows.Forms.Button()
        Me.btnStartDeviceNDC = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(14, 276)
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ReadOnly = True
        Me.txtDeviceProperty.Size = New System.Drawing.Size(758, 210)
        Me.txtDeviceProperty.TabIndex = 0
        Me.txtDeviceProperty.Text = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdExit)
        Me.GroupBox2.Controls.Add(Me.txtInterval)
        Me.GroupBox2.Controls.Add(Me.txttmID)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cmdSetTimeOut)
        Me.GroupBox2.Controls.Add(Me.cmdPMPCCardSerialNo)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cboCardType)
        Me.GroupBox2.Controls.Add(Me.cbState)
        Me.GroupBox2.Controls.Add(Me.btnTrack1)
        Me.GroupBox2.Controls.Add(Me.btnTrack3)
        Me.GroupBox2.Controls.Add(Me.btnTrack2)
        Me.GroupBox2.Controls.Add(Me.btnReadCard)
        Me.GroupBox2.Controls.Add(Me.btnDiagDeviceNDC)
        Me.GroupBox2.Controls.Add(Me.btnWakeDeviceNDC)
        Me.GroupBox2.Controls.Add(Me.btnClearText)
        Me.GroupBox2.Controls.Add(Me.btnGetDevProp)
        Me.GroupBox2.Controls.Add(Me.btnUnlockDeviceNDC)
        Me.GroupBox2.Controls.Add(Me.btnLockDeviceNDC)
        Me.GroupBox2.Controls.Add(Me.btnWrapDeviceNDC)
        Me.GroupBox2.Controls.Add(Me.btnStopDeviceNDC)
        Me.GroupBox2.Controls.Add(Me.btnStartDeviceNDC)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(17, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(755, 258)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Card Reader Command"
        '
        'txtInterval
        '
        Me.txtInterval.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInterval.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInterval.Location = New System.Drawing.Point(217, 189)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.ReadOnly = True
        Me.txtInterval.Size = New System.Drawing.Size(137, 20)
        Me.txtInterval.TabIndex = 15
        '
        'txttmID
        '
        Me.txttmID.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txttmID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txttmID.Location = New System.Drawing.Point(214, 162)
        Me.txttmID.Name = "txttmID"
        Me.txttmID.ReadOnly = True
        Me.txttmID.Size = New System.Drawing.Size(137, 20)
        Me.txttmID.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(157, 189)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Interval:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(183, 169)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "ID:"
        '
        'cmdSetTimeOut
        '
        Me.cmdSetTimeOut.Location = New System.Drawing.Point(19, 171)
        Me.cmdSetTimeOut.Name = "cmdSetTimeOut"
        Me.cmdSetTimeOut.Size = New System.Drawing.Size(132, 30)
        Me.cmdSetTimeOut.TabIndex = 11
        Me.cmdSetTimeOut.Text = "Timeout Interval"
        Me.cmdSetTimeOut.UseVisualStyleBackColor = True
        '
        'cmdPMPCCardSerialNo
        '
        Me.cmdPMPCCardSerialNo.Location = New System.Drawing.Point(184, 213)
        Me.cmdPMPCCardSerialNo.Name = "cmdPMPCCardSerialNo"
        Me.cmdPMPCCardSerialNo.Size = New System.Drawing.Size(189, 30)
        Me.cmdPMPCCardSerialNo.TabIndex = 10
        Me.cmdPMPCCardSerialNo.Text = "Card Serial Number - PMPC"
        Me.cmdPMPCCardSerialNo.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Device State:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Methods"
        '
        'cboCardType
        '
        Me.cboCardType.FormattingEnabled = True
        Me.cboCardType.Items.AddRange(New Object() {"0 - Magnetic", "1 - PMPC", "2 - EMV", "-1"})
        Me.cboCardType.Location = New System.Drawing.Point(148, 124)
        Me.cboCardType.Name = "cboCardType"
        Me.cboCardType.Size = New System.Drawing.Size(128, 21)
        Me.cboCardType.TabIndex = 7
        Me.cboCardType.Text = "0 - Magnetic"
        '
        'cbState
        '
        Me.cbState.FormattingEnabled = True
        Me.cbState.Items.AddRange(New Object() {"0-No Card", "1-Card In Reader", "2-Card Stage", "3-Captured", "4-Eject Failed", "5-Eject", "6-Blocked"})
        Me.cbState.Location = New System.Drawing.Point(127, 80)
        Me.cbState.Name = "cbState"
        Me.cbState.Size = New System.Drawing.Size(154, 21)
        Me.cbState.TabIndex = 7
        Me.cbState.Text = "0-No Card"
        '
        'btnTrack1
        '
        Me.btnTrack1.Location = New System.Drawing.Point(297, 124)
        Me.btnTrack1.Name = "btnTrack1"
        Me.btnTrack1.Size = New System.Drawing.Size(128, 30)
        Me.btnTrack1.TabIndex = 4
        Me.btnTrack1.Text = "TrackI()"
        Me.btnTrack1.UseVisualStyleBackColor = True
        '
        'btnTrack3
        '
        Me.btnTrack3.Location = New System.Drawing.Point(568, 124)
        Me.btnTrack3.Name = "btnTrack3"
        Me.btnTrack3.Size = New System.Drawing.Size(128, 30)
        Me.btnTrack3.TabIndex = 4
        Me.btnTrack3.Text = "TrackIII()"
        Me.btnTrack3.UseVisualStyleBackColor = True
        '
        'btnTrack2
        '
        Me.btnTrack2.Location = New System.Drawing.Point(433, 124)
        Me.btnTrack2.Name = "btnTrack2"
        Me.btnTrack2.Size = New System.Drawing.Size(128, 30)
        Me.btnTrack2.TabIndex = 4
        Me.btnTrack2.Text = "TrackII()"
        Me.btnTrack2.UseVisualStyleBackColor = True
        '
        'btnReadCard
        '
        Me.btnReadCard.Location = New System.Drawing.Point(19, 121)
        Me.btnReadCard.Name = "btnReadCard"
        Me.btnReadCard.Size = New System.Drawing.Size(128, 30)
        Me.btnReadCard.TabIndex = 4
        Me.btnReadCard.Text = "ReadCard()"
        Me.btnReadCard.UseVisualStyleBackColor = True
        '
        'btnDiagDeviceNDC
        '
        Me.btnDiagDeviceNDC.Location = New System.Drawing.Point(439, 74)
        Me.btnDiagDeviceNDC.Name = "btnDiagDeviceNDC"
        Me.btnDiagDeviceNDC.Size = New System.Drawing.Size(264, 30)
        Me.btnDiagDeviceNDC.TabIndex = 4
        Me.btnDiagDeviceNDC.Text = "DiagnosticDevice()"
        Me.btnDiagDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWakeDeviceNDC
        '
        Me.btnWakeDeviceNDC.Location = New System.Drawing.Point(289, 74)
        Me.btnWakeDeviceNDC.Name = "btnWakeDeviceNDC"
        Me.btnWakeDeviceNDC.Size = New System.Drawing.Size(136, 30)
        Me.btnWakeDeviceNDC.TabIndex = 4
        Me.btnWakeDeviceNDC.Text = "WakeUpDevice()"
        Me.btnWakeDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnClearText
        '
        Me.btnClearText.Location = New System.Drawing.Point(596, 212)
        Me.btnClearText.Name = "btnClearText"
        Me.btnClearText.Size = New System.Drawing.Size(106, 30)
        Me.btnClearText.TabIndex = 4
        Me.btnClearText.Text = "Clear Text"
        Me.btnClearText.UseVisualStyleBackColor = True
        '
        'btnGetDevProp
        '
        Me.btnGetDevProp.Location = New System.Drawing.Point(19, 212)
        Me.btnGetDevProp.Name = "btnGetDevProp"
        Me.btnGetDevProp.Size = New System.Drawing.Size(159, 30)
        Me.btnGetDevProp.TabIndex = 4
        Me.btnGetDevProp.Text = "GetDeviceProperty"
        Me.btnGetDevProp.UseVisualStyleBackColor = True
        '
        'btnUnlockDeviceNDC
        '
        Me.btnUnlockDeviceNDC.Location = New System.Drawing.Point(574, 41)
        Me.btnUnlockDeviceNDC.Name = "btnUnlockDeviceNDC"
        Me.btnUnlockDeviceNDC.Size = New System.Drawing.Size(128, 30)
        Me.btnUnlockDeviceNDC.TabIndex = 4
        Me.btnUnlockDeviceNDC.Text = "UnlockDevice()"
        Me.btnUnlockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnLockDeviceNDC
        '
        Me.btnLockDeviceNDC.Location = New System.Drawing.Point(439, 41)
        Me.btnLockDeviceNDC.Name = "btnLockDeviceNDC"
        Me.btnLockDeviceNDC.Size = New System.Drawing.Size(128, 30)
        Me.btnLockDeviceNDC.TabIndex = 4
        Me.btnLockDeviceNDC.Text = "LockDevice()"
        Me.btnLockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWrapDeviceNDC
        '
        Me.btnWrapDeviceNDC.Location = New System.Drawing.Point(289, 41)
        Me.btnWrapDeviceNDC.Name = "btnWrapDeviceNDC"
        Me.btnWrapDeviceNDC.Size = New System.Drawing.Size(136, 30)
        Me.btnWrapDeviceNDC.TabIndex = 4
        Me.btnWrapDeviceNDC.Text = "WrapDevice()"
        Me.btnWrapDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnStopDeviceNDC
        '
        Me.btnStopDeviceNDC.Location = New System.Drawing.Point(154, 41)
        Me.btnStopDeviceNDC.Name = "btnStopDeviceNDC"
        Me.btnStopDeviceNDC.Size = New System.Drawing.Size(128, 30)
        Me.btnStopDeviceNDC.TabIndex = 4
        Me.btnStopDeviceNDC.Text = "StopDevice()"
        Me.btnStopDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnStartDeviceNDC
        '
        Me.btnStartDeviceNDC.Location = New System.Drawing.Point(19, 41)
        Me.btnStartDeviceNDC.Name = "btnStartDeviceNDC"
        Me.btnStartDeviceNDC.Size = New System.Drawing.Size(128, 30)
        Me.btnStartDeviceNDC.TabIndex = 4
        Me.btnStartDeviceNDC.Text = "StartDevice()"
        Me.btnStartDeviceNDC.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(484, 213)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(106, 30)
        Me.cmdExit.TabIndex = 16
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'CardReaderMainApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtDeviceProperty)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CardReaderMainApp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS:Card Reader"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDiagDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnWakeDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnUnlockDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnLockDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnWrapDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnStopDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnStartDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnClearText As System.Windows.Forms.Button
    Friend WithEvents btnGetDevProp As System.Windows.Forms.Button
    Friend WithEvents btnReadCard As System.Windows.Forms.Button
    Friend WithEvents btnTrack3 As System.Windows.Forms.Button
    Friend WithEvents btnTrack2 As System.Windows.Forms.Button
    Friend WithEvents btnTrack1 As System.Windows.Forms.Button
    Friend WithEvents cbState As System.Windows.Forms.ComboBox
    Friend WithEvents cboCardType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdPMPCCardSerialNo As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdSetTimeOut As System.Windows.Forms.Button
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    Friend WithEvents txttmID As System.Windows.Forms.TextBox
    Friend WithEvents cmdExit As System.Windows.Forms.Button

End Class
