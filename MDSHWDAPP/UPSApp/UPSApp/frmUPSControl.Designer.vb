<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUPSControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUPSControl))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdClearText = New System.Windows.Forms.Button()
        Me.cmdDiagnosticDevice = New System.Windows.Forms.Button()
        Me.cmdGetDeviceProperty = New System.Windows.Forms.Button()
        Me.cmdWrapDevice = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.TextBox()
        Me.cmdWakeupDevice = New System.Windows.Forms.Button()
        Me.cmdLockDevice = New System.Windows.Forms.Button()
        Me.cmdUnlockDevice = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdInit = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnUPSSetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnUPSSetting)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmdClearText)
        Me.GroupBox1.Controls.Add(Me.cmdDiagnosticDevice)
        Me.GroupBox1.Controls.Add(Me.cmdGetDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.cmdWrapDevice)
        Me.GroupBox1.Controls.Add(Me.txtDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.cmdWakeupDevice)
        Me.GroupBox1.Controls.Add(Me.cmdLockDevice)
        Me.GroupBox1.Controls.Add(Me.cmdUnlockDevice)
        Me.GroupBox1.Controls.Add(Me.cmdClose)
        Me.GroupBox1.Controls.Add(Me.cmdInit)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(760, 538)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "UPS Command"
        '
        'cmdClearText
        '
        Me.cmdClearText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearText.Location = New System.Drawing.Point(575, 184)
        Me.cmdClearText.Name = "cmdClearText"
        Me.cmdClearText.Size = New System.Drawing.Size(143, 49)
        Me.cmdClearText.TabIndex = 11
        Me.cmdClearText.Text = "Clear Text"
        Me.cmdClearText.UseVisualStyleBackColor = True
        '
        'cmdDiagnosticDevice
        '
        Me.cmdDiagnosticDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDiagnosticDevice.Location = New System.Drawing.Point(184, 182)
        Me.cmdDiagnosticDevice.Name = "cmdDiagnosticDevice"
        Me.cmdDiagnosticDevice.Size = New System.Drawing.Size(199, 51)
        Me.cmdDiagnosticDevice.TabIndex = 10
        Me.cmdDiagnosticDevice.Text = "DiagnosticDevice"
        Me.cmdDiagnosticDevice.UseVisualStyleBackColor = True
        '
        'cmdGetDeviceProperty
        '
        Me.cmdGetDeviceProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetDeviceProperty.Location = New System.Drawing.Point(389, 184)
        Me.cmdGetDeviceProperty.Name = "cmdGetDeviceProperty"
        Me.cmdGetDeviceProperty.Size = New System.Drawing.Size(180, 49)
        Me.cmdGetDeviceProperty.TabIndex = 8
        Me.cmdGetDeviceProperty.Text = "Get Device Property"
        Me.cmdGetDeviceProperty.UseVisualStyleBackColor = True
        '
        'cmdWrapDevice
        '
        Me.cmdWrapDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWrapDevice.Location = New System.Drawing.Point(291, 115)
        Me.cmdWrapDevice.Name = "cmdWrapDevice"
        Me.cmdWrapDevice.Size = New System.Drawing.Size(134, 54)
        Me.cmdWrapDevice.TabIndex = 7
        Me.cmdWrapDevice.Text = "Wrap Device"
        Me.cmdWrapDevice.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(22, 251)
        Me.txtDeviceProperty.Multiline = True
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDeviceProperty.Size = New System.Drawing.Size(721, 272)
        Me.txtDeviceProperty.TabIndex = 6
        '
        'cmdWakeupDevice
        '
        Me.cmdWakeupDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWakeupDevice.Location = New System.Drawing.Point(24, 182)
        Me.cmdWakeupDevice.Name = "cmdWakeupDevice"
        Me.cmdWakeupDevice.Size = New System.Drawing.Size(154, 51)
        Me.cmdWakeupDevice.TabIndex = 5
        Me.cmdWakeupDevice.Text = "WakeupDevice"
        Me.cmdWakeupDevice.UseVisualStyleBackColor = True
        '
        'cmdLockDevice
        '
        Me.cmdLockDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLockDevice.Location = New System.Drawing.Point(431, 115)
        Me.cmdLockDevice.Name = "cmdLockDevice"
        Me.cmdLockDevice.Size = New System.Drawing.Size(138, 54)
        Me.cmdLockDevice.TabIndex = 3
        Me.cmdLockDevice.Text = "LockDevice"
        Me.cmdLockDevice.UseVisualStyleBackColor = True
        '
        'cmdUnlockDevice
        '
        Me.cmdUnlockDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUnlockDevice.Location = New System.Drawing.Point(575, 115)
        Me.cmdUnlockDevice.Name = "cmdUnlockDevice"
        Me.cmdUnlockDevice.Size = New System.Drawing.Size(143, 54)
        Me.cmdUnlockDevice.TabIndex = 2
        Me.cmdUnlockDevice.Text = "UnlockDevice"
        Me.cmdUnlockDevice.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(155, 115)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(130, 54)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "StopDevice"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdInit
        '
        Me.cmdInit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInit.Location = New System.Drawing.Point(22, 115)
        Me.cmdInit.Name = "cmdInit"
        Me.cmdInit.Size = New System.Drawing.Size(127, 54)
        Me.cmdInit.TabIndex = 0
        Me.cmdInit.Text = "StartDevice"
        Me.cmdInit.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Methods"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(534, 38)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 47)
        Me.btnExit.TabIndex = 27
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnUPSSetting
        '
        Me.btnUPSSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUPSSetting.Location = New System.Drawing.Point(143, 35)
        Me.btnUPSSetting.Name = "btnUPSSetting"
        Me.btnUPSSetting.Size = New System.Drawing.Size(194, 52)
        Me.btnUPSSetting.TabIndex = 26
        Me.btnUPSSetting.Text = "UPS Setting"
        Me.btnUPSSetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(119, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "UPS Configuration :"
        '
        'frmUPSControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUPSControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS:UPS"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdLockDevice As System.Windows.Forms.Button
    Friend WithEvents cmdUnlockDevice As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdInit As System.Windows.Forms.Button
    Friend WithEvents cmdWakeupDevice As System.Windows.Forms.Button
    Friend WithEvents cmdDiagnosticDevice As System.Windows.Forms.Button
    Friend WithEvents cmdGetDeviceProperty As System.Windows.Forms.Button
    Friend WithEvents cmdWrapDevice As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.TextBox
    Friend WithEvents cmdClearText As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnUPSSetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
