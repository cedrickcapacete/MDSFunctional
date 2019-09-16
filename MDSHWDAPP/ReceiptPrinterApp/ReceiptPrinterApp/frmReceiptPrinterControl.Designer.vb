<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReceiptPrinterControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReceiptPrinterControl))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdClearAll = New System.Windows.Forms.Button()
        Me.chkCutPaper5 = New System.Windows.Forms.CheckBox()
        Me.chkCutPaper4 = New System.Windows.Forms.CheckBox()
        Me.chkCutPaper3 = New System.Windows.Forms.CheckBox()
        Me.chkCutPaper2 = New System.Windows.Forms.CheckBox()
        Me.cmdUpload3 = New System.Windows.Forms.Button()
        Me.txtFileName3 = New System.Windows.Forms.TextBox()
        Me.txtSection2 = New System.Windows.Forms.TextBox()
        Me.cmdUpload2 = New System.Windows.Forms.Button()
        Me.txtFileName2 = New System.Windows.Forms.TextBox()
        Me.txtSection1 = New System.Windows.Forms.TextBox()
        Me.cmdUpload = New System.Windows.Forms.Button()
        Me.txtFileName1 = New System.Windows.Forms.TextBox()
        Me.chkCutPaper = New System.Windows.Forms.CheckBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
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
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdExit)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
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
        Me.GroupBox1.Location = New System.Drawing.Point(33, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(729, 550)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Receipt Printer Command"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdClearAll)
        Me.GroupBox2.Controls.Add(Me.chkCutPaper5)
        Me.GroupBox2.Controls.Add(Me.chkCutPaper4)
        Me.GroupBox2.Controls.Add(Me.chkCutPaper3)
        Me.GroupBox2.Controls.Add(Me.chkCutPaper2)
        Me.GroupBox2.Controls.Add(Me.cmdUpload3)
        Me.GroupBox2.Controls.Add(Me.txtFileName3)
        Me.GroupBox2.Controls.Add(Me.txtSection2)
        Me.GroupBox2.Controls.Add(Me.cmdUpload2)
        Me.GroupBox2.Controls.Add(Me.txtFileName2)
        Me.GroupBox2.Controls.Add(Me.txtSection1)
        Me.GroupBox2.Controls.Add(Me.cmdUpload)
        Me.GroupBox2.Controls.Add(Me.txtFileName1)
        Me.GroupBox2.Controls.Add(Me.chkCutPaper)
        Me.GroupBox2.Controls.Add(Me.cmdPrint)
        Me.GroupBox2.Location = New System.Drawing.Point(28, 129)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(701, 252)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Receipt Data"
        '
        'cmdClearAll
        '
        Me.cmdClearAll.Location = New System.Drawing.Point(557, 121)
        Me.cmdClearAll.Name = "cmdClearAll"
        Me.cmdClearAll.Size = New System.Drawing.Size(124, 64)
        Me.cmdClearAll.TabIndex = 30
        Me.cmdClearAll.Text = "Clear Receipt Data"
        Me.cmdClearAll.UseVisualStyleBackColor = True
        '
        'chkCutPaper5
        '
        Me.chkCutPaper5.AutoSize = True
        Me.chkCutPaper5.Location = New System.Drawing.Point(455, 210)
        Me.chkCutPaper5.Name = "chkCutPaper5"
        Me.chkCutPaper5.Size = New System.Drawing.Size(95, 23)
        Me.chkCutPaper5.TabIndex = 29
        Me.chkCutPaper5.Text = "Cut Paper"
        Me.chkCutPaper5.UseVisualStyleBackColor = True
        '
        'chkCutPaper4
        '
        Me.chkCutPaper4.AutoSize = True
        Me.chkCutPaper4.Location = New System.Drawing.Point(455, 157)
        Me.chkCutPaper4.Name = "chkCutPaper4"
        Me.chkCutPaper4.Size = New System.Drawing.Size(95, 23)
        Me.chkCutPaper4.TabIndex = 28
        Me.chkCutPaper4.Text = "Cut Paper"
        Me.chkCutPaper4.UseVisualStyleBackColor = True
        '
        'chkCutPaper3
        '
        Me.chkCutPaper3.AutoSize = True
        Me.chkCutPaper3.Location = New System.Drawing.Point(455, 119)
        Me.chkCutPaper3.Name = "chkCutPaper3"
        Me.chkCutPaper3.Size = New System.Drawing.Size(95, 23)
        Me.chkCutPaper3.TabIndex = 27
        Me.chkCutPaper3.Text = "Cut Paper"
        Me.chkCutPaper3.UseVisualStyleBackColor = True
        '
        'chkCutPaper2
        '
        Me.chkCutPaper2.AutoSize = True
        Me.chkCutPaper2.Location = New System.Drawing.Point(455, 62)
        Me.chkCutPaper2.Name = "chkCutPaper2"
        Me.chkCutPaper2.Size = New System.Drawing.Size(95, 23)
        Me.chkCutPaper2.TabIndex = 26
        Me.chkCutPaper2.Text = "Cut Paper"
        Me.chkCutPaper2.UseVisualStyleBackColor = True
        '
        'cmdUpload3
        '
        Me.cmdUpload3.Location = New System.Drawing.Point(370, 213)
        Me.cmdUpload3.Name = "cmdUpload3"
        Me.cmdUpload3.Size = New System.Drawing.Size(75, 26)
        Me.cmdUpload3.TabIndex = 25
        Me.cmdUpload3.Text = "Browse"
        Me.cmdUpload3.UseVisualStyleBackColor = True
        '
        'txtFileName3
        '
        Me.txtFileName3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFileName3.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileName3.Location = New System.Drawing.Point(12, 213)
        Me.txtFileName3.Name = "txtFileName3"
        Me.txtFileName3.Size = New System.Drawing.Size(352, 20)
        Me.txtFileName3.TabIndex = 24
        '
        'txtSection2
        '
        Me.txtSection2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSection2.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection2.Location = New System.Drawing.Point(12, 157)
        Me.txtSection2.Multiline = True
        Me.txtSection2.Name = "txtSection2"
        Me.txtSection2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSection2.Size = New System.Drawing.Size(433, 44)
        Me.txtSection2.TabIndex = 23
        '
        'cmdUpload2
        '
        Me.cmdUpload2.Location = New System.Drawing.Point(370, 121)
        Me.cmdUpload2.Name = "cmdUpload2"
        Me.cmdUpload2.Size = New System.Drawing.Size(75, 26)
        Me.cmdUpload2.TabIndex = 22
        Me.cmdUpload2.Text = "Browse"
        Me.cmdUpload2.UseVisualStyleBackColor = True
        '
        'txtFileName2
        '
        Me.txtFileName2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFileName2.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileName2.Location = New System.Drawing.Point(12, 121)
        Me.txtFileName2.Name = "txtFileName2"
        Me.txtFileName2.Size = New System.Drawing.Size(352, 20)
        Me.txtFileName2.TabIndex = 21
        '
        'txtSection1
        '
        Me.txtSection1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSection1.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection1.Location = New System.Drawing.Point(12, 62)
        Me.txtSection1.Multiline = True
        Me.txtSection1.Name = "txtSection1"
        Me.txtSection1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSection1.Size = New System.Drawing.Size(433, 44)
        Me.txtSection1.TabIndex = 20
        '
        'cmdUpload
        '
        Me.cmdUpload.Location = New System.Drawing.Point(370, 25)
        Me.cmdUpload.Name = "cmdUpload"
        Me.cmdUpload.Size = New System.Drawing.Size(75, 26)
        Me.cmdUpload.TabIndex = 19
        Me.cmdUpload.Text = "Browse"
        Me.cmdUpload.UseVisualStyleBackColor = True
        '
        'txtFileName1
        '
        Me.txtFileName1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFileName1.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileName1.Location = New System.Drawing.Point(12, 25)
        Me.txtFileName1.Name = "txtFileName1"
        Me.txtFileName1.Size = New System.Drawing.Size(352, 20)
        Me.txtFileName1.TabIndex = 18
        '
        'chkCutPaper
        '
        Me.chkCutPaper.AutoSize = True
        Me.chkCutPaper.Location = New System.Drawing.Point(455, 28)
        Me.chkCutPaper.Name = "chkCutPaper"
        Me.chkCutPaper.Size = New System.Drawing.Size(95, 23)
        Me.chkCutPaper.TabIndex = 17
        Me.chkCutPaper.Text = "Cut Paper"
        Me.chkCutPaper.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Location = New System.Drawing.Point(557, 22)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(124, 63)
        Me.cmdPrint.TabIndex = 13
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdClearText
        '
        Me.cmdClearText.Location = New System.Drawing.Point(535, 387)
        Me.cmdClearText.Name = "cmdClearText"
        Me.cmdClearText.Size = New System.Drawing.Size(174, 47)
        Me.cmdClearText.TabIndex = 11
        Me.cmdClearText.Text = "Clear Text"
        Me.cmdClearText.UseVisualStyleBackColor = True
        '
        'cmdDiagnosticDevice
        '
        Me.cmdDiagnosticDevice.Location = New System.Drawing.Point(446, 25)
        Me.cmdDiagnosticDevice.Name = "cmdDiagnosticDevice"
        Me.cmdDiagnosticDevice.Size = New System.Drawing.Size(162, 48)
        Me.cmdDiagnosticDevice.TabIndex = 10
        Me.cmdDiagnosticDevice.Text = "DiagnosticDevice"
        Me.cmdDiagnosticDevice.UseVisualStyleBackColor = True
        '
        'cmdGetDeviceProperty
        '
        Me.cmdGetDeviceProperty.Location = New System.Drawing.Point(23, 386)
        Me.cmdGetDeviceProperty.Name = "cmdGetDeviceProperty"
        Me.cmdGetDeviceProperty.Size = New System.Drawing.Size(207, 50)
        Me.cmdGetDeviceProperty.TabIndex = 8
        Me.cmdGetDeviceProperty.Text = "Get Device Property"
        Me.cmdGetDeviceProperty.UseVisualStyleBackColor = True
        '
        'cmdWrapDevice
        '
        Me.cmdWrapDevice.Location = New System.Drawing.Point(302, 25)
        Me.cmdWrapDevice.Name = "cmdWrapDevice"
        Me.cmdWrapDevice.Size = New System.Drawing.Size(125, 48)
        Me.cmdWrapDevice.TabIndex = 7
        Me.cmdWrapDevice.Text = "Wrap Device"
        Me.cmdWrapDevice.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(24, 440)
        Me.txtDeviceProperty.Multiline = True
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDeviceProperty.Size = New System.Drawing.Size(685, 104)
        Me.txtDeviceProperty.TabIndex = 6
        '
        'cmdWakeupDevice
        '
        Me.cmdWakeupDevice.Location = New System.Drawing.Point(302, 79)
        Me.cmdWakeupDevice.Name = "cmdWakeupDevice"
        Me.cmdWakeupDevice.Size = New System.Drawing.Size(125, 44)
        Me.cmdWakeupDevice.TabIndex = 5
        Me.cmdWakeupDevice.Text = "WakeupDevice"
        Me.cmdWakeupDevice.UseVisualStyleBackColor = True
        '
        'cmdLockDevice
        '
        Me.cmdLockDevice.Location = New System.Drawing.Point(157, 79)
        Me.cmdLockDevice.Name = "cmdLockDevice"
        Me.cmdLockDevice.Size = New System.Drawing.Size(126, 44)
        Me.cmdLockDevice.TabIndex = 3
        Me.cmdLockDevice.Text = "LockDevice"
        Me.cmdLockDevice.UseVisualStyleBackColor = True
        '
        'cmdUnlockDevice
        '
        Me.cmdUnlockDevice.Location = New System.Drawing.Point(24, 79)
        Me.cmdUnlockDevice.Name = "cmdUnlockDevice"
        Me.cmdUnlockDevice.Size = New System.Drawing.Size(127, 44)
        Me.cmdUnlockDevice.TabIndex = 2
        Me.cmdUnlockDevice.Text = "UnlockDevice"
        Me.cmdUnlockDevice.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(157, 25)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(126, 48)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "StopDevice"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdInit
        '
        Me.cmdInit.Location = New System.Drawing.Point(24, 25)
        Me.cmdInit.Name = "cmdInit"
        Me.cmdInit.Size = New System.Drawing.Size(127, 48)
        Me.cmdInit.TabIndex = 0
        Me.cmdInit.Text = "StartDevice"
        Me.cmdInit.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(446, 79)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(162, 48)
        Me.cmdExit.TabIndex = 13
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'frmReceiptPrinterControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReceiptPrinterControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Receipt Printer Control Panel"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkCutPaper As System.Windows.Forms.CheckBox
    Friend WithEvents cmdUpload As System.Windows.Forms.Button
    Friend WithEvents txtFileName1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSection1 As System.Windows.Forms.TextBox
    Friend WithEvents cmdUpload3 As System.Windows.Forms.Button
    Friend WithEvents txtFileName3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSection2 As System.Windows.Forms.TextBox
    Friend WithEvents cmdUpload2 As System.Windows.Forms.Button
    Friend WithEvents txtFileName2 As System.Windows.Forms.TextBox
    Friend WithEvents chkCutPaper5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCutPaper4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCutPaper3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCutPaper2 As System.Windows.Forms.CheckBox
    Friend WithEvents cmdClearAll As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button

End Class
