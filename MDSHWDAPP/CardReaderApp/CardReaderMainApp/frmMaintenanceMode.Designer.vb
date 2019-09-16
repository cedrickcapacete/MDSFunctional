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
        Me.btnReadPMPC = New System.Windows.Forms.Button()
        Me.btnCaptureCard = New System.Windows.Forms.Button()
        Me.btnEjectCard = New System.Windows.Forms.Button()
        Me.btnClearText = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.RichTextBox()
        Me.btnReadCard = New System.Windows.Forms.Button()
        Me.cmdWrapDevice = New System.Windows.Forms.Button()
        Me.cmdWakeupDevice = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdInit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnCardReaderSetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnReadPMPC)
        Me.GroupBox1.Controls.Add(Me.btnCaptureCard)
        Me.GroupBox1.Controls.Add(Me.btnEjectCard)
        Me.GroupBox1.Controls.Add(Me.btnClearText)
        Me.GroupBox1.Controls.Add(Me.txtDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.btnReadCard)
        Me.GroupBox1.Controls.Add(Me.cmdWrapDevice)
        Me.GroupBox1.Controls.Add(Me.cmdWakeupDevice)
        Me.GroupBox1.Controls.Add(Me.cmdClose)
        Me.GroupBox1.Controls.Add(Me.cmdInit)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnCardReaderSetting)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 528)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: Card Reader"
        '
        'btnReadPMPC
        '
        Me.btnReadPMPC.Location = New System.Drawing.Point(346, 261)
        Me.btnReadPMPC.Name = "btnReadPMPC"
        Me.btnReadPMPC.Size = New System.Drawing.Size(179, 61)
        Me.btnReadPMPC.TabIndex = 51
        Me.btnReadPMPC.Text = "Read - PMPC"
        Me.btnReadPMPC.UseVisualStyleBackColor = True
        '
        'btnCaptureCard
        '
        Me.btnCaptureCard.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCaptureCard.Location = New System.Drawing.Point(531, 185)
        Me.btnCaptureCard.Name = "btnCaptureCard"
        Me.btnCaptureCard.Size = New System.Drawing.Size(179, 61)
        Me.btnCaptureCard.TabIndex = 50
        Me.btnCaptureCard.Text = "Captured Card"
        Me.btnCaptureCard.UseVisualStyleBackColor = True
        '
        'btnEjectCard
        '
        Me.btnEjectCard.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEjectCard.Location = New System.Drawing.Point(341, 185)
        Me.btnEjectCard.Name = "btnEjectCard"
        Me.btnEjectCard.Size = New System.Drawing.Size(179, 61)
        Me.btnEjectCard.TabIndex = 49
        Me.btnEjectCard.Text = "Eject Card"
        Me.btnEjectCard.UseVisualStyleBackColor = True
        '
        'btnClearText
        '
        Me.btnClearText.Location = New System.Drawing.Point(533, 261)
        Me.btnClearText.Name = "btnClearText"
        Me.btnClearText.Size = New System.Drawing.Size(177, 61)
        Me.btnClearText.TabIndex = 48
        Me.btnClearText.Text = "Clear Text"
        Me.btnClearText.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(20, 328)
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ReadOnly = True
        Me.txtDeviceProperty.Size = New System.Drawing.Size(695, 175)
        Me.txtDeviceProperty.TabIndex = 47
        Me.txtDeviceProperty.Text = ""
        '
        'btnReadCard
        '
        Me.btnReadCard.Location = New System.Drawing.Point(161, 261)
        Me.btnReadCard.Name = "btnReadCard"
        Me.btnReadCard.Size = New System.Drawing.Size(179, 61)
        Me.btnReadCard.TabIndex = 43
        Me.btnReadCard.Text = "Read - Magnetic"
        Me.btnReadCard.UseVisualStyleBackColor = True
        '
        'cmdWrapDevice
        '
        Me.cmdWrapDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWrapDevice.Location = New System.Drawing.Point(531, 106)
        Me.cmdWrapDevice.Name = "cmdWrapDevice"
        Me.cmdWrapDevice.Size = New System.Drawing.Size(184, 64)
        Me.cmdWrapDevice.TabIndex = 42
        Me.cmdWrapDevice.Text = "Wrap Device"
        Me.cmdWrapDevice.UseVisualStyleBackColor = True
        '
        'cmdWakeupDevice
        '
        Me.cmdWakeupDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWakeupDevice.Location = New System.Drawing.Point(159, 185)
        Me.cmdWakeupDevice.Name = "cmdWakeupDevice"
        Me.cmdWakeupDevice.Size = New System.Drawing.Size(179, 61)
        Me.cmdWakeupDevice.TabIndex = 41
        Me.cmdWakeupDevice.Text = "Open Gate"
        Me.cmdWakeupDevice.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(341, 106)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(184, 64)
        Me.cmdClose.TabIndex = 40
        Me.cmdClose.Text = "StopDevice"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdInit
        '
        Me.cmdInit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInit.Location = New System.Drawing.Point(159, 106)
        Me.cmdInit.Name = "cmdInit"
        Me.cmdInit.Size = New System.Drawing.Size(176, 64)
        Me.cmdInit.TabIndex = 39
        Me.cmdInit.Text = "StartDevice"
        Me.cmdInit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 133)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Card Reader Command :"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(533, 28)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 61)
        Me.btnExit.TabIndex = 33
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnCardReaderSetting
        '
        Me.btnCardReaderSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardReaderSetting.Location = New System.Drawing.Point(188, 25)
        Me.btnCardReaderSetting.Name = "btnCardReaderSetting"
        Me.btnCardReaderSetting.Size = New System.Drawing.Size(194, 64)
        Me.btnCardReaderSetting.TabIndex = 32
        Me.btnCardReaderSetting.Text = "Card Reader Setting"
        Me.btnCardReaderSetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(165, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Card Reader Configuration :"
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
        Me.Text = "MDS:Card Reader"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnCardReaderSetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdWrapDevice As System.Windows.Forms.Button
    Friend WithEvents cmdWakeupDevice As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdInit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnReadCard As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.RichTextBox
    Friend WithEvents btnClearText As System.Windows.Forms.Button
    Friend WithEvents btnReadPMPC As System.Windows.Forms.Button
    Friend WithEvents btnCaptureCard As System.Windows.Forms.Button
    Friend WithEvents btnEjectCard As System.Windows.Forms.Button
End Class
