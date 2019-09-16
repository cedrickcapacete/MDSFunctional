<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaintenanceMode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaintenanceMode))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdStopDevice = New System.Windows.Forms.Button()
        Me.cmdClearText = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.TextBox()
        Me.cmdWakeupDevice = New System.Windows.Forms.Button()
        Me.cmdInit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnKeypadSetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdStopDevice)
        Me.GroupBox1.Controls.Add(Me.cmdClearText)
        Me.GroupBox1.Controls.Add(Me.txtDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.cmdWakeupDevice)
        Me.GroupBox1.Controls.Add(Me.cmdInit)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnKeypadSetting)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 517)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: Keypad"
        '
        'cmdStopDevice
        '
        Me.cmdStopDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStopDevice.Location = New System.Drawing.Point(360, 130)
        Me.cmdStopDevice.Name = "cmdStopDevice"
        Me.cmdStopDevice.Size = New System.Drawing.Size(194, 59)
        Me.cmdStopDevice.TabIndex = 38
        Me.cmdStopDevice.Text = "StopDevice"
        Me.cmdStopDevice.UseVisualStyleBackColor = True
        '
        'cmdClearText
        '
        Me.cmdClearText.Location = New System.Drawing.Point(360, 195)
        Me.cmdClearText.Name = "cmdClearText"
        Me.cmdClearText.Size = New System.Drawing.Size(194, 59)
        Me.cmdClearText.TabIndex = 37
        Me.cmdClearText.Text = "Clear Text"
        Me.cmdClearText.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(160, 265)
        Me.txtDeviceProperty.Multiline = True
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDeviceProperty.Size = New System.Drawing.Size(569, 234)
        Me.txtDeviceProperty.TabIndex = 36
        '
        'cmdWakeupDevice
        '
        Me.cmdWakeupDevice.Location = New System.Drawing.Point(160, 195)
        Me.cmdWakeupDevice.Name = "cmdWakeupDevice"
        Me.cmdWakeupDevice.Size = New System.Drawing.Size(194, 59)
        Me.cmdWakeupDevice.TabIndex = 35
        Me.cmdWakeupDevice.Text = "WakeupDevice"
        Me.cmdWakeupDevice.UseVisualStyleBackColor = True
        '
        'cmdInit
        '
        Me.cmdInit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInit.Location = New System.Drawing.Point(160, 130)
        Me.cmdInit.Name = "cmdInit"
        Me.cmdInit.Size = New System.Drawing.Size(194, 59)
        Me.cmdInit.TabIndex = 34
        Me.cmdInit.Text = "StartDevice"
        Me.cmdInit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 130)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Keypad Command :"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(530, 44)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 61)
        Me.btnExit.TabIndex = 30
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnKeypadSetting
        '
        Me.btnKeypadSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnKeypadSetting.Location = New System.Drawing.Point(160, 41)
        Me.btnKeypadSetting.Name = "btnKeypadSetting"
        Me.btnKeypadSetting.Size = New System.Drawing.Size(194, 64)
        Me.btnKeypadSetting.TabIndex = 29
        Me.btnKeypadSetting.Text = "Keypad Setting"
        Me.btnKeypadSetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Keypad Configuration :"
        '
        'frmMaintenanceMode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMaintenanceMode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS:Keypad"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdInit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnKeypadSetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdWakeupDevice As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.TextBox
    Friend WithEvents cmdClearText As System.Windows.Forms.Button
    Friend WithEvents cmdStopDevice As System.Windows.Forms.Button
End Class
