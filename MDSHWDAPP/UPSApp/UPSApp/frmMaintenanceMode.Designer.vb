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
        Me.lblUPSStatus = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RBACPower = New System.Windows.Forms.RadioButton()
        Me.RBBatery = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdWrapDevice = New System.Windows.Forms.Button()
        Me.cmdWakeupDevice = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdInit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnUPSSetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblUPSStatus)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.RBACPower)
        Me.GroupBox1.Controls.Add(Me.RBBatery)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmdWrapDevice)
        Me.GroupBox1.Controls.Add(Me.cmdWakeupDevice)
        Me.GroupBox1.Controls.Add(Me.cmdClose)
        Me.GroupBox1.Controls.Add(Me.cmdInit)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnUPSSetting)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 517)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: UPS"
        '
        'lblUPSStatus
        '
        Me.lblUPSStatus.AutoSize = True
        Me.lblUPSStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUPSStatus.Location = New System.Drawing.Point(38, 429)
        Me.lblUPSStatus.Name = "lblUPSStatus"
        Me.lblUPSStatus.Size = New System.Drawing.Size(81, 13)
        Me.lblUPSStatus.TabIndex = 42
        Me.lblUPSStatus.Text = "lblUPSStatus"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(39, 401)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "UPS Status Value:"
        '
        'RBACPower
        '
        Me.RBACPower.AutoSize = True
        Me.RBACPower.ForeColor = System.Drawing.Color.Lime
        Me.RBACPower.Location = New System.Drawing.Point(141, 352)
        Me.RBACPower.Name = "RBACPower"
        Me.RBACPower.Size = New System.Drawing.Size(292, 24)
        Me.RBACPower.TabIndex = 40
        Me.RBACPower.TabStop = True
        Me.RBACPower.Text = "UPS Running on AC Power Mode"
        Me.RBACPower.UseVisualStyleBackColor = True
        '
        'RBBatery
        '
        Me.RBBatery.AutoSize = True
        Me.RBBatery.ForeColor = System.Drawing.Color.Red
        Me.RBBatery.Location = New System.Drawing.Point(141, 306)
        Me.RBBatery.Name = "RBBatery"
        Me.RBBatery.Size = New System.Drawing.Size(272, 24)
        Me.RBBatery.TabIndex = 39
        Me.RBBatery.TabStop = True
        Me.RBBatery.Text = "UPS Running on Battery Mode"
        Me.RBBatery.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(39, 306)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "UPS Mode:"
        '
        'cmdWrapDevice
        '
        Me.cmdWrapDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWrapDevice.Location = New System.Drawing.Point(530, 130)
        Me.cmdWrapDevice.Name = "cmdWrapDevice"
        Me.cmdWrapDevice.Size = New System.Drawing.Size(184, 64)
        Me.cmdWrapDevice.TabIndex = 37
        Me.cmdWrapDevice.Text = "Wrap Device"
        Me.cmdWrapDevice.UseVisualStyleBackColor = True
        '
        'cmdWakeupDevice
        '
        Me.cmdWakeupDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWakeupDevice.Location = New System.Drawing.Point(139, 211)
        Me.cmdWakeupDevice.Name = "cmdWakeupDevice"
        Me.cmdWakeupDevice.Size = New System.Drawing.Size(192, 61)
        Me.cmdWakeupDevice.TabIndex = 36
        Me.cmdWakeupDevice.Text = "WakeupDevice"
        Me.cmdWakeupDevice.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(339, 130)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(184, 64)
        Me.cmdClose.TabIndex = 35
        Me.cmdClose.Text = "StopDevice"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdInit
        '
        Me.cmdInit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInit.Location = New System.Drawing.Point(139, 130)
        Me.cmdInit.Name = "cmdInit"
        Me.cmdInit.Size = New System.Drawing.Size(194, 64)
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
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "UPS Command :"
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
        'btnUPSSetting
        '
        Me.btnUPSSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUPSSetting.Location = New System.Drawing.Point(139, 41)
        Me.btnUPSSetting.Name = "btnUPSSetting"
        Me.btnUPSSetting.Size = New System.Drawing.Size(194, 64)
        Me.btnUPSSetting.TabIndex = 29
        Me.btnUPSSetting.Text = "UPS Setting"
        Me.btnUPSSetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(119, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "UPS Configuration :"
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
        Me.Text = "MDS:UPS"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnUPSSetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents RBACPower As System.Windows.Forms.RadioButton
    Friend WithEvents RBBatery As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdWrapDevice As System.Windows.Forms.Button
    Friend WithEvents cmdWakeupDevice As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdInit As System.Windows.Forms.Button
    Friend WithEvents lblUPSStatus As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
