<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBeeperMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBeeperMain))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnBeeperSetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdTriggerON = New System.Windows.Forms.Button()
        Me.cmdTriggerOFF = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnDiagDeviceNDC = New System.Windows.Forms.Button()
        Me.btnWakeDeviceNDC = New System.Windows.Forms.Button()
        Me.btnClearText = New System.Windows.Forms.Button()
        Me.btnGetDevProp = New System.Windows.Forms.Button()
        Me.btnUnlockDeviceNDC = New System.Windows.Forms.Button()
        Me.btnLockDeviceNDC = New System.Windows.Forms.Button()
        Me.btnWrapDeviceNDC = New System.Windows.Forms.Button()
        Me.btnStopDeviceNDC = New System.Windows.Forms.Button()
        Me.btnStartDeviceNDC = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.RichTextBox()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnBeeperSetting)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerON)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerOFF)
        Me.GroupBox2.Controls.Add(Me.Label2)
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
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(760, 273)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Beeper Command"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(554, 19)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 47)
        Me.btnExit.TabIndex = 27
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnBeeperSetting
        '
        Me.btnBeeperSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBeeperSetting.Location = New System.Drawing.Point(154, 19)
        Me.btnBeeperSetting.Name = "btnBeeperSetting"
        Me.btnBeeperSetting.Size = New System.Drawing.Size(194, 52)
        Me.btnBeeperSetting.TabIndex = 26
        Me.btnBeeperSetting.Text = "Beeper Setting"
        Me.btnBeeperSetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(134, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Beeper Configuration :"
        '
        'cmdTriggerON
        '
        Me.cmdTriggerON.AccessibleDescription = ""
        Me.cmdTriggerON.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerON.Location = New System.Drawing.Point(19, 205)
        Me.cmdTriggerON.Name = "cmdTriggerON"
        Me.cmdTriggerON.Size = New System.Drawing.Size(144, 54)
        Me.cmdTriggerON.TabIndex = 11
        Me.cmdTriggerON.Text = "Trigger ON"
        Me.cmdTriggerON.UseVisualStyleBackColor = True
        '
        'cmdTriggerOFF
        '
        Me.cmdTriggerOFF.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerOFF.Location = New System.Drawing.Point(169, 205)
        Me.cmdTriggerOFF.Name = "cmdTriggerOFF"
        Me.cmdTriggerOFF.Size = New System.Drawing.Size(130, 54)
        Me.cmdTriggerOFF.TabIndex = 10
        Me.cmdTriggerOFF.Text = "Trigger OFF"
        Me.cmdTriggerOFF.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Methods"
        '
        'btnDiagDeviceNDC
        '
        Me.btnDiagDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDiagDeviceNDC.Location = New System.Drawing.Point(186, 145)
        Me.btnDiagDeviceNDC.Name = "btnDiagDeviceNDC"
        Me.btnDiagDeviceNDC.Size = New System.Drawing.Size(191, 54)
        Me.btnDiagDeviceNDC.TabIndex = 4
        Me.btnDiagDeviceNDC.Text = "DiagnosticDevice"
        Me.btnDiagDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWakeDeviceNDC
        '
        Me.btnWakeDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWakeDeviceNDC.Location = New System.Drawing.Point(19, 144)
        Me.btnWakeDeviceNDC.Name = "btnWakeDeviceNDC"
        Me.btnWakeDeviceNDC.Size = New System.Drawing.Size(161, 54)
        Me.btnWakeDeviceNDC.TabIndex = 4
        Me.btnWakeDeviceNDC.Text = "WakeUpDevice"
        Me.btnWakeDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnClearText
        '
        Me.btnClearText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearText.Location = New System.Drawing.Point(599, 149)
        Me.btnClearText.Name = "btnClearText"
        Me.btnClearText.Size = New System.Drawing.Size(139, 47)
        Me.btnClearText.TabIndex = 4
        Me.btnClearText.Text = "Clear Text"
        Me.btnClearText.UseVisualStyleBackColor = True
        '
        'btnGetDevProp
        '
        Me.btnGetDevProp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGetDevProp.Location = New System.Drawing.Point(383, 145)
        Me.btnGetDevProp.Name = "btnGetDevProp"
        Me.btnGetDevProp.Size = New System.Drawing.Size(210, 54)
        Me.btnGetDevProp.TabIndex = 4
        Me.btnGetDevProp.Text = "GetDeviceProperty"
        Me.btnGetDevProp.UseVisualStyleBackColor = True
        '
        'btnUnlockDeviceNDC
        '
        Me.btnUnlockDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnlockDeviceNDC.Location = New System.Drawing.Point(599, 77)
        Me.btnUnlockDeviceNDC.Name = "btnUnlockDeviceNDC"
        Me.btnUnlockDeviceNDC.Size = New System.Drawing.Size(139, 62)
        Me.btnUnlockDeviceNDC.TabIndex = 4
        Me.btnUnlockDeviceNDC.Text = "UnlockDevice"
        Me.btnUnlockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnLockDeviceNDC
        '
        Me.btnLockDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLockDeviceNDC.Location = New System.Drawing.Point(456, 77)
        Me.btnLockDeviceNDC.Name = "btnLockDeviceNDC"
        Me.btnLockDeviceNDC.Size = New System.Drawing.Size(137, 62)
        Me.btnLockDeviceNDC.TabIndex = 4
        Me.btnLockDeviceNDC.Text = "LockDevice"
        Me.btnLockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWrapDeviceNDC
        '
        Me.btnWrapDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWrapDeviceNDC.Location = New System.Drawing.Point(305, 77)
        Me.btnWrapDeviceNDC.Name = "btnWrapDeviceNDC"
        Me.btnWrapDeviceNDC.Size = New System.Drawing.Size(145, 62)
        Me.btnWrapDeviceNDC.TabIndex = 4
        Me.btnWrapDeviceNDC.Text = "WrapDevice"
        Me.btnWrapDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnStopDeviceNDC
        '
        Me.btnStopDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStopDeviceNDC.Location = New System.Drawing.Point(154, 77)
        Me.btnStopDeviceNDC.Name = "btnStopDeviceNDC"
        Me.btnStopDeviceNDC.Size = New System.Drawing.Size(145, 62)
        Me.btnStopDeviceNDC.TabIndex = 4
        Me.btnStopDeviceNDC.Text = "StopDevice"
        Me.btnStopDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnStartDeviceNDC
        '
        Me.btnStartDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartDeviceNDC.Location = New System.Drawing.Point(19, 77)
        Me.btnStartDeviceNDC.Name = "btnStartDeviceNDC"
        Me.btnStartDeviceNDC.Size = New System.Drawing.Size(128, 62)
        Me.btnStartDeviceNDC.TabIndex = 4
        Me.btnStartDeviceNDC.Text = "StartDevice"
        Me.btnStartDeviceNDC.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(13, 291)
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ReadOnly = True
        Me.txtDeviceProperty.Size = New System.Drawing.Size(759, 269)
        Me.txtDeviceProperty.TabIndex = 9
        Me.txtDeviceProperty.Text = ""
        '
        'frmBeeperMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 572)
        Me.Controls.Add(Me.txtDeviceProperty)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBeeperMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS:Beeper"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdTriggerON As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerOFF As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDiagDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnWakeDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnClearText As System.Windows.Forms.Button
    Friend WithEvents btnGetDevProp As System.Windows.Forms.Button
    Friend WithEvents btnUnlockDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnLockDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnWrapDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnStopDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnStartDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.RichTextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnBeeperSetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
