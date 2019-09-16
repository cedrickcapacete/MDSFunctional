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
        Me.btnWrapDeviceNDC = New System.Windows.Forms.Button()
        Me.cmdTriggerON = New System.Windows.Forms.Button()
        Me.cmdTriggerOFF = New System.Windows.Forms.Button()
        Me.btnStopDeviceNDC = New System.Windows.Forms.Button()
        Me.btnStartDeviceNDC = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnBeeperSetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnWrapDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.cmdTriggerON)
        Me.GroupBox1.Controls.Add(Me.cmdTriggerOFF)
        Me.GroupBox1.Controls.Add(Me.btnStopDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.btnStartDeviceNDC)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnBeeperSetting)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 517)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: Beeper"
        '
        'btnWrapDeviceNDC
        '
        Me.btnWrapDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWrapDeviceNDC.Location = New System.Drawing.Point(549, 93)
        Me.btnWrapDeviceNDC.Name = "btnWrapDeviceNDC"
        Me.btnWrapDeviceNDC.Size = New System.Drawing.Size(184, 62)
        Me.btnWrapDeviceNDC.TabIndex = 37
        Me.btnWrapDeviceNDC.Text = "WrapDevice"
        Me.btnWrapDeviceNDC.UseVisualStyleBackColor = True
        '
        'cmdTriggerON
        '
        Me.cmdTriggerON.AccessibleDescription = ""
        Me.cmdTriggerON.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerON.Location = New System.Drawing.Point(152, 161)
        Me.cmdTriggerON.Name = "cmdTriggerON"
        Me.cmdTriggerON.Size = New System.Drawing.Size(191, 65)
        Me.cmdTriggerON.TabIndex = 36
        Me.cmdTriggerON.Text = "Trigger ON"
        Me.cmdTriggerON.UseVisualStyleBackColor = True
        '
        'cmdTriggerOFF
        '
        Me.cmdTriggerOFF.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerOFF.Location = New System.Drawing.Point(349, 161)
        Me.cmdTriggerOFF.Name = "cmdTriggerOFF"
        Me.cmdTriggerOFF.Size = New System.Drawing.Size(191, 65)
        Me.cmdTriggerOFF.TabIndex = 35
        Me.cmdTriggerOFF.Text = "Trigger OFF"
        Me.cmdTriggerOFF.UseVisualStyleBackColor = True
        '
        'btnStopDeviceNDC
        '
        Me.btnStopDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStopDeviceNDC.Location = New System.Drawing.Point(349, 93)
        Me.btnStopDeviceNDC.Name = "btnStopDeviceNDC"
        Me.btnStopDeviceNDC.Size = New System.Drawing.Size(191, 62)
        Me.btnStopDeviceNDC.TabIndex = 33
        Me.btnStopDeviceNDC.Text = "StopDevice"
        Me.btnStopDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnStartDeviceNDC
        '
        Me.btnStartDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartDeviceNDC.Location = New System.Drawing.Point(152, 93)
        Me.btnStartDeviceNDC.Name = "btnStartDeviceNDC"
        Me.btnStartDeviceNDC.Size = New System.Drawing.Size(191, 62)
        Me.btnStartDeviceNDC.TabIndex = 32
        Me.btnStartDeviceNDC.Text = "StartDevice"
        Me.btnStartDeviceNDC.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 111)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Beeper Command :"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(549, 35)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 52)
        Me.btnExit.TabIndex = 30
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnBeeperSetting
        '
        Me.btnBeeperSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBeeperSetting.Location = New System.Drawing.Point(149, 35)
        Me.btnBeeperSetting.Name = "btnBeeperSetting"
        Me.btnBeeperSetting.Size = New System.Drawing.Size(194, 52)
        Me.btnBeeperSetting.TabIndex = 29
        Me.btnBeeperSetting.Text = "Beeper Setting"
        Me.btnBeeperSetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(134, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Beeper Configuration :"
        '
        'frmMaintenanceMode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMaintenanceMode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS:Beeper"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnBeeperSetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnStopDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnStartDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerON As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerOFF As System.Windows.Forms.Button
    Friend WithEvents btnWrapDeviceNDC As System.Windows.Forms.Button
End Class
