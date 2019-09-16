<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKeypadControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKeypadControl))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnMasterKey = New System.Windows.Forms.Button()
        Me.btnWriteKeyB = New System.Windows.Forms.Button()
        Me.btnWriteKeyA = New System.Windows.Forms.Button()
        Me.rbnSupervisor = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMaxLen = New System.Windows.Forms.TextBox()
        Me.rbnEncryptText = New System.Windows.Forms.RadioButton()
        Me.rbnClearText = New System.Windows.Forms.RadioButton()
        Me.txtKeyedValue = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdWakeupDevice = New System.Windows.Forms.Button()
        Me.cmdTxt = New System.Windows.Forms.Button()
        Me.cmdClearText = New System.Windows.Forms.Button()
        Me.cmdDiagnosticDevice = New System.Windows.Forms.Button()
        Me.cmdGetDeviceProperty = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.TextBox()
        Me.cmdWrapDevice = New System.Windows.Forms.Button()
        Me.cmdLockDevice = New System.Windows.Forms.Button()
        Me.cmdUnlockDevice = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdInit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.cmdTxt)
        Me.GroupBox1.Controls.Add(Me.cmdClearText)
        Me.GroupBox1.Controls.Add(Me.cmdDiagnosticDevice)
        Me.GroupBox1.Controls.Add(Me.cmdGetDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.txtDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.cmdWrapDevice)
        Me.GroupBox1.Controls.Add(Me.cmdLockDevice)
        Me.GroupBox1.Controls.Add(Me.cmdUnlockDevice)
        Me.GroupBox1.Controls.Add(Me.cmdClose)
        Me.GroupBox1.Controls.Add(Me.cmdInit)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(32, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(729, 550)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pin Pad Command"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.GroupBox2.Controls.Add(Me.btnMasterKey)
        Me.GroupBox2.Controls.Add(Me.btnWriteKeyB)
        Me.GroupBox2.Controls.Add(Me.btnWriteKeyA)
        Me.GroupBox2.Controls.Add(Me.rbnSupervisor)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtMaxLen)
        Me.GroupBox2.Controls.Add(Me.rbnEncryptText)
        Me.GroupBox2.Controls.Add(Me.rbnClearText)
        Me.GroupBox2.Controls.Add(Me.txtKeyedValue)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cmdWakeupDevice)
        Me.GroupBox2.Location = New System.Drawing.Point(24, 137)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(681, 179)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Get Key Pressed From Pinpad"
        '
        'btnMasterKey
        '
        Me.btnMasterKey.Location = New System.Drawing.Point(541, 130)
        Me.btnMasterKey.Name = "btnMasterKey"
        Me.btnMasterKey.Size = New System.Drawing.Size(113, 36)
        Me.btnMasterKey.TabIndex = 25
        Me.btnMasterKey.Text = "Master Key"
        Me.btnMasterKey.UseVisualStyleBackColor = True
        '
        'btnWriteKeyB
        '
        Me.btnWriteKeyB.Location = New System.Drawing.Point(450, 130)
        Me.btnWriteKeyB.Name = "btnWriteKeyB"
        Me.btnWriteKeyB.Size = New System.Drawing.Size(75, 36)
        Me.btnWriteKeyB.TabIndex = 24
        Me.btnWriteKeyB.Text = "B Key"
        Me.btnWriteKeyB.UseVisualStyleBackColor = True
        '
        'btnWriteKeyA
        '
        Me.btnWriteKeyA.Location = New System.Drawing.Point(359, 130)
        Me.btnWriteKeyA.Name = "btnWriteKeyA"
        Me.btnWriteKeyA.Size = New System.Drawing.Size(75, 36)
        Me.btnWriteKeyA.TabIndex = 23
        Me.btnWriteKeyA.Text = "A Key"
        Me.btnWriteKeyA.UseVisualStyleBackColor = True
        '
        'rbnSupervisor
        '
        Me.rbnSupervisor.AutoSize = True
        Me.rbnSupervisor.Location = New System.Drawing.Point(16, 85)
        Me.rbnSupervisor.Name = "rbnSupervisor"
        Me.rbnSupervisor.Size = New System.Drawing.Size(99, 23)
        Me.rbnSupervisor.TabIndex = 22
        Me.rbnSupervisor.TabStop = True
        Me.rbnSupervisor.Text = "Supervisor"
        Me.rbnSupervisor.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(150, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 19)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Max Length"
        '
        'txtMaxLen
        '
        Me.txtMaxLen.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMaxLen.Location = New System.Drawing.Point(248, 54)
        Me.txtMaxLen.Name = "txtMaxLen"
        Me.txtMaxLen.Size = New System.Drawing.Size(31, 26)
        Me.txtMaxLen.TabIndex = 20
        '
        'rbnEncryptText
        '
        Me.rbnEncryptText.AutoSize = True
        Me.rbnEncryptText.Location = New System.Drawing.Point(16, 55)
        Me.rbnEncryptText.Name = "rbnEncryptText"
        Me.rbnEncryptText.Size = New System.Drawing.Size(113, 23)
        Me.rbnEncryptText.TabIndex = 19
        Me.rbnEncryptText.TabStop = True
        Me.rbnEncryptText.Text = "Encrypt Text"
        Me.rbnEncryptText.UseVisualStyleBackColor = True
        '
        'rbnClearText
        '
        Me.rbnClearText.AutoSize = True
        Me.rbnClearText.Location = New System.Drawing.Point(16, 25)
        Me.rbnClearText.Name = "rbnClearText"
        Me.rbnClearText.Size = New System.Drawing.Size(98, 23)
        Me.rbnClearText.TabIndex = 18
        Me.rbnClearText.TabStop = True
        Me.rbnClearText.Text = "Clear Text"
        Me.rbnClearText.UseVisualStyleBackColor = True
        '
        'txtKeyedValue
        '
        Me.txtKeyedValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKeyedValue.Font = New System.Drawing.Font("Times New Roman", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKeyedValue.Location = New System.Drawing.Point(359, 61)
        Me.txtKeyedValue.Name = "txtKeyedValue"
        Me.txtKeyedValue.Size = New System.Drawing.Size(295, 53)
        Me.txtKeyedValue.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(354, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(181, 26)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "KEY IN VALUE"
        '
        'cmdWakeupDevice
        '
        Me.cmdWakeupDevice.Location = New System.Drawing.Point(16, 134)
        Me.cmdWakeupDevice.Name = "cmdWakeupDevice"
        Me.cmdWakeupDevice.Size = New System.Drawing.Size(125, 39)
        Me.cmdWakeupDevice.TabIndex = 5
        Me.cmdWakeupDevice.Text = "WakeupDevice"
        Me.cmdWakeupDevice.UseVisualStyleBackColor = True
        '
        'cmdTxt
        '
        Me.cmdTxt.Location = New System.Drawing.Point(289, 79)
        Me.cmdTxt.Name = "cmdTxt"
        Me.cmdTxt.Size = New System.Drawing.Size(125, 39)
        Me.cmdTxt.TabIndex = 12
        Me.cmdTxt.Text = "Text"
        Me.cmdTxt.UseVisualStyleBackColor = True
        '
        'cmdClearText
        '
        Me.cmdClearText.Location = New System.Drawing.Point(583, 335)
        Me.cmdClearText.Name = "cmdClearText"
        Me.cmdClearText.Size = New System.Drawing.Size(122, 23)
        Me.cmdClearText.TabIndex = 11
        Me.cmdClearText.Text = "Clear Text"
        Me.cmdClearText.UseVisualStyleBackColor = True
        '
        'cmdDiagnosticDevice
        '
        Me.cmdDiagnosticDevice.Location = New System.Drawing.Point(433, 79)
        Me.cmdDiagnosticDevice.Name = "cmdDiagnosticDevice"
        Me.cmdDiagnosticDevice.Size = New System.Drawing.Size(272, 39)
        Me.cmdDiagnosticDevice.TabIndex = 10
        Me.cmdDiagnosticDevice.Text = "DiagnosticDevice"
        Me.cmdDiagnosticDevice.UseVisualStyleBackColor = True
        '
        'cmdGetDeviceProperty
        '
        Me.cmdGetDeviceProperty.Location = New System.Drawing.Point(24, 332)
        Me.cmdGetDeviceProperty.Name = "cmdGetDeviceProperty"
        Me.cmdGetDeviceProperty.Size = New System.Drawing.Size(165, 26)
        Me.cmdGetDeviceProperty.TabIndex = 8
        Me.cmdGetDeviceProperty.Text = "Get Device Property"
        Me.cmdGetDeviceProperty.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(24, 362)
        Me.txtDeviceProperty.Multiline = True
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDeviceProperty.Size = New System.Drawing.Size(681, 173)
        Me.txtDeviceProperty.TabIndex = 6
        '
        'cmdWrapDevice
        '
        Me.cmdWrapDevice.Location = New System.Drawing.Point(289, 25)
        Me.cmdWrapDevice.Name = "cmdWrapDevice"
        Me.cmdWrapDevice.Size = New System.Drawing.Size(125, 39)
        Me.cmdWrapDevice.TabIndex = 7
        Me.cmdWrapDevice.Text = "Wrap Device"
        Me.cmdWrapDevice.UseVisualStyleBackColor = True
        '
        'cmdLockDevice
        '
        Me.cmdLockDevice.Location = New System.Drawing.Point(157, 79)
        Me.cmdLockDevice.Name = "cmdLockDevice"
        Me.cmdLockDevice.Size = New System.Drawing.Size(116, 39)
        Me.cmdLockDevice.TabIndex = 3
        Me.cmdLockDevice.Text = "LockDevice"
        Me.cmdLockDevice.UseVisualStyleBackColor = True
        '
        'cmdUnlockDevice
        '
        Me.cmdUnlockDevice.Location = New System.Drawing.Point(24, 79)
        Me.cmdUnlockDevice.Name = "cmdUnlockDevice"
        Me.cmdUnlockDevice.Size = New System.Drawing.Size(116, 39)
        Me.cmdUnlockDevice.TabIndex = 2
        Me.cmdUnlockDevice.Text = "UnlockDevice"
        Me.cmdUnlockDevice.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(157, 25)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(116, 39)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "StopDevice"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdInit
        '
        Me.cmdInit.Location = New System.Drawing.Point(24, 25)
        Me.cmdInit.Name = "cmdInit"
        Me.cmdInit.Size = New System.Drawing.Size(116, 39)
        Me.cmdInit.TabIndex = 0
        Me.cmdInit.Text = "StartDevice"
        Me.cmdInit.UseVisualStyleBackColor = True
        '
        'frmKeypadControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 572)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmKeypadControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Key Pad Control Panel"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdClearText As System.Windows.Forms.Button
    Friend WithEvents cmdDiagnosticDevice As System.Windows.Forms.Button
    Friend WithEvents cmdGetDeviceProperty As System.Windows.Forms.Button
    Friend WithEvents cmdWrapDevice As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.TextBox
    Friend WithEvents cmdWakeupDevice As System.Windows.Forms.Button
    Friend WithEvents cmdLockDevice As System.Windows.Forms.Button
    Friend WithEvents cmdUnlockDevice As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdInit As System.Windows.Forms.Button
    Friend WithEvents cmdTxt As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtKeyedValue As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbnEncryptText As System.Windows.Forms.RadioButton
    Friend WithEvents rbnClearText As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMaxLen As System.Windows.Forms.TextBox
    Friend WithEvents rbnSupervisor As System.Windows.Forms.RadioButton
    Friend WithEvents btnMasterKey As System.Windows.Forms.Button
    Friend WithEvents btnWriteKeyB As System.Windows.Forms.Button
    Friend WithEvents btnWriteKeyA As System.Windows.Forms.Button

End Class
