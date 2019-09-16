<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DoorSensorApp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DoorSensorApp))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblDoorStatus = New System.Windows.Forms.Label()
        Me.cmdWakeUp = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnGetDevProp = New System.Windows.Forms.Button()
        Me.btnClearText = New System.Windows.Forms.Button()
        Me.btnDiagDeviceNDC = New System.Windows.Forms.Button()
        Me.btnWrapDeviceNDC = New System.Windows.Forms.Button()
        Me.btnDoorUnlockDevice = New System.Windows.Forms.Button()
        Me.btnDoorLockDevice = New System.Windows.Forms.Button()
        Me.btnDoorStopDevice = New System.Windows.Forms.Button()
        Me.btnDoorStartDevice = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.RichTextBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdExit)
        Me.GroupBox2.Controls.Add(Me.lblDoorStatus)
        Me.GroupBox2.Controls.Add(Me.cmdWakeUp)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnGetDevProp)
        Me.GroupBox2.Controls.Add(Me.btnClearText)
        Me.GroupBox2.Controls.Add(Me.btnDiagDeviceNDC)
        Me.GroupBox2.Controls.Add(Me.btnWrapDeviceNDC)
        Me.GroupBox2.Controls.Add(Me.btnDoorUnlockDevice)
        Me.GroupBox2.Controls.Add(Me.btnDoorLockDevice)
        Me.GroupBox2.Controls.Add(Me.btnDoorStopDevice)
        Me.GroupBox2.Controls.Add(Me.btnDoorStartDevice)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(16, 3)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox2.Size = New System.Drawing.Size(754, 171)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Door"
        '
        'lblDoorStatus
        '
        Me.lblDoorStatus.AutoSize = True
        Me.lblDoorStatus.Location = New System.Drawing.Point(197, 133)
        Me.lblDoorStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDoorStatus.Name = "lblDoorStatus"
        Me.lblDoorStatus.Size = New System.Drawing.Size(83, 13)
        Me.lblDoorStatus.TabIndex = 21
        Me.lblDoorStatus.Text = "lblDoorStatus"
        '
        'cmdWakeUp
        '
        Me.cmdWakeUp.Location = New System.Drawing.Point(24, 122)
        Me.cmdWakeUp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdWakeUp.Name = "cmdWakeUp"
        Me.cmdWakeUp.Size = New System.Drawing.Size(147, 35)
        Me.cmdWakeUp.TabIndex = 20
        Me.cmdWakeUp.Text = "WakeupDevice"
        Me.cmdWakeUp.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 21)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Methods"
        '
        'btnGetDevProp
        '
        Me.btnGetDevProp.Location = New System.Drawing.Point(333, 81)
        Me.btnGetDevProp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnGetDevProp.Name = "btnGetDevProp"
        Me.btnGetDevProp.Size = New System.Drawing.Size(201, 35)
        Me.btnGetDevProp.TabIndex = 18
        Me.btnGetDevProp.Text = "GetDeviceProperty"
        Me.btnGetDevProp.UseVisualStyleBackColor = True
        '
        'btnClearText
        '
        Me.btnClearText.Location = New System.Drawing.Point(553, 81)
        Me.btnClearText.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnClearText.Name = "btnClearText"
        Me.btnClearText.Size = New System.Drawing.Size(147, 35)
        Me.btnClearText.TabIndex = 16
        Me.btnClearText.Text = "Clear Text"
        Me.btnClearText.UseVisualStyleBackColor = True
        '
        'btnDiagDeviceNDC
        '
        Me.btnDiagDeviceNDC.Location = New System.Drawing.Point(488, 39)
        Me.btnDiagDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDiagDeviceNDC.Name = "btnDiagDeviceNDC"
        Me.btnDiagDeviceNDC.Size = New System.Drawing.Size(212, 35)
        Me.btnDiagDeviceNDC.TabIndex = 15
        Me.btnDiagDeviceNDC.Text = "DiagnosticDevice"
        Me.btnDiagDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWrapDeviceNDC
        '
        Me.btnWrapDeviceNDC.Location = New System.Drawing.Point(333, 39)
        Me.btnWrapDeviceNDC.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnWrapDeviceNDC.Name = "btnWrapDeviceNDC"
        Me.btnWrapDeviceNDC.Size = New System.Drawing.Size(147, 35)
        Me.btnWrapDeviceNDC.TabIndex = 7
        Me.btnWrapDeviceNDC.Text = "WrapDevice"
        Me.btnWrapDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnDoorUnlockDevice
        '
        Me.btnDoorUnlockDevice.Location = New System.Drawing.Point(179, 81)
        Me.btnDoorUnlockDevice.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDoorUnlockDevice.Name = "btnDoorUnlockDevice"
        Me.btnDoorUnlockDevice.Size = New System.Drawing.Size(147, 35)
        Me.btnDoorUnlockDevice.TabIndex = 0
        Me.btnDoorUnlockDevice.Text = "UnlockDevice"
        Me.btnDoorUnlockDevice.UseVisualStyleBackColor = True
        '
        'btnDoorLockDevice
        '
        Me.btnDoorLockDevice.Location = New System.Drawing.Point(24, 81)
        Me.btnDoorLockDevice.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDoorLockDevice.Name = "btnDoorLockDevice"
        Me.btnDoorLockDevice.Size = New System.Drawing.Size(147, 35)
        Me.btnDoorLockDevice.TabIndex = 0
        Me.btnDoorLockDevice.Text = "LockDevice"
        Me.btnDoorLockDevice.UseVisualStyleBackColor = True
        '
        'btnDoorStopDevice
        '
        Me.btnDoorStopDevice.Location = New System.Drawing.Point(179, 39)
        Me.btnDoorStopDevice.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDoorStopDevice.Name = "btnDoorStopDevice"
        Me.btnDoorStopDevice.Size = New System.Drawing.Size(147, 35)
        Me.btnDoorStopDevice.TabIndex = 0
        Me.btnDoorStopDevice.Text = "StopDevice"
        Me.btnDoorStopDevice.UseVisualStyleBackColor = True
        '
        'btnDoorStartDevice
        '
        Me.btnDoorStartDevice.Location = New System.Drawing.Point(24, 39)
        Me.btnDoorStartDevice.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDoorStartDevice.Name = "btnDoorStartDevice"
        Me.btnDoorStartDevice.Size = New System.Drawing.Size(147, 35)
        Me.btnDoorStartDevice.TabIndex = 0
        Me.btnDoorStartDevice.Text = "StartDevice"
        Me.btnDoorStartDevice.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(16, 181)
        Me.txtDeviceProperty.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ReadOnly = True
        Me.txtDeviceProperty.Size = New System.Drawing.Size(754, 379)
        Me.txtDeviceProperty.TabIndex = 18
        Me.txtDeviceProperty.Text = ""
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(553, 123)
        Me.cmdExit.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(147, 37)
        Me.cmdExit.TabIndex = 36
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'DoorSensorApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 572)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtDeviceProperty)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DoorSensorApp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS: Door Sensor"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDoorUnlockDevice As System.Windows.Forms.Button
    Friend WithEvents btnDoorLockDevice As System.Windows.Forms.Button
    Friend WithEvents btnDoorStopDevice As System.Windows.Forms.Button
    Friend WithEvents btnDoorStartDevice As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.RichTextBox
    Friend WithEvents btnWrapDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents btnGetDevProp As System.Windows.Forms.Button
    Friend WithEvents btnClearText As System.Windows.Forms.Button
    Friend WithEvents btnDiagDeviceNDC As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdWakeUp As System.Windows.Forms.Button
    Friend WithEvents lblDoorStatus As System.Windows.Forms.Label
    Friend WithEvents cmdExit As System.Windows.Forms.Button
End Class
