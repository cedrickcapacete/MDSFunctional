<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRSIMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRSIMain))
        Me.txtDeviceProperty = New System.Windows.Forms.RichTextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnRSISetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdTriggerONRSI4 = New System.Windows.Forms.Button()
        Me.cmdTriggerOFFRSI4 = New System.Windows.Forms.Button()
        Me.cmdTriggerONRSI3 = New System.Windows.Forms.Button()
        Me.cmdTriggerOFFRSI3 = New System.Windows.Forms.Button()
        Me.cmdTriggerONRSI2 = New System.Windows.Forms.Button()
        Me.cmdTriggerOFFRSI2 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdTriggerONRSI1 = New System.Windows.Forms.Button()
        Me.cmdTriggerOFFRSI1 = New System.Windows.Forms.Button()
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
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(12, 354)
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ReadOnly = True
        Me.txtDeviceProperty.Size = New System.Drawing.Size(760, 196)
        Me.txtDeviceProperty.TabIndex = 11
        Me.txtDeviceProperty.Text = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnRSISetting)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerONRSI4)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerOFFRSI4)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerONRSI3)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerOFFRSI3)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerONRSI2)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerOFFRSI2)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerONRSI1)
        Me.GroupBox2.Controls.Add(Me.cmdTriggerOFFRSI1)
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
        Me.GroupBox2.Size = New System.Drawing.Size(760, 336)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "RSI Command"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(548, 19)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 47)
        Me.btnExit.TabIndex = 24
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnRSISetting
        '
        Me.btnRSISetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRSISetting.Location = New System.Drawing.Point(132, 19)
        Me.btnRSISetting.Name = "btnRSISetting"
        Me.btnRSISetting.Size = New System.Drawing.Size(194, 52)
        Me.btnRSISetting.TabIndex = 23
        Me.btnRSISetting.Text = "RSI Setting"
        Me.btnRSISetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "RSI Configuration :"
        '
        'cmdTriggerONRSI4
        '
        Me.cmdTriggerONRSI4.AccessibleDescription = ""
        Me.cmdTriggerONRSI4.BackColor = System.Drawing.Color.Red
        Me.cmdTriggerONRSI4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerONRSI4.Location = New System.Drawing.Point(410, 261)
        Me.cmdTriggerONRSI4.Name = "cmdTriggerONRSI4"
        Me.cmdTriggerONRSI4.Size = New System.Drawing.Size(132, 55)
        Me.cmdTriggerONRSI4.TabIndex = 21
        Me.cmdTriggerONRSI4.Text = "OOS"
        Me.cmdTriggerONRSI4.UseVisualStyleBackColor = False
        '
        'cmdTriggerOFFRSI4
        '
        Me.cmdTriggerOFFRSI4.BackColor = System.Drawing.Color.Lime
        Me.cmdTriggerOFFRSI4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerOFFRSI4.Location = New System.Drawing.Point(548, 261)
        Me.cmdTriggerOFFRSI4.Name = "cmdTriggerOFFRSI4"
        Me.cmdTriggerOFFRSI4.Size = New System.Drawing.Size(141, 55)
        Me.cmdTriggerOFFRSI4.TabIndex = 20
        Me.cmdTriggerOFFRSI4.Text = "GIS"
        Me.cmdTriggerOFFRSI4.UseVisualStyleBackColor = False
        '
        'cmdTriggerONRSI3
        '
        Me.cmdTriggerONRSI3.AccessibleDescription = ""
        Me.cmdTriggerONRSI3.BackColor = System.Drawing.Color.Red
        Me.cmdTriggerONRSI3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerONRSI3.Location = New System.Drawing.Point(410, 201)
        Me.cmdTriggerONRSI3.Name = "cmdTriggerONRSI3"
        Me.cmdTriggerONRSI3.Size = New System.Drawing.Size(132, 54)
        Me.cmdTriggerONRSI3.TabIndex = 19
        Me.cmdTriggerONRSI3.Text = "OOS"
        Me.cmdTriggerONRSI3.UseVisualStyleBackColor = False
        '
        'cmdTriggerOFFRSI3
        '
        Me.cmdTriggerOFFRSI3.BackColor = System.Drawing.Color.Lime
        Me.cmdTriggerOFFRSI3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerOFFRSI3.Location = New System.Drawing.Point(548, 201)
        Me.cmdTriggerOFFRSI3.Name = "cmdTriggerOFFRSI3"
        Me.cmdTriggerOFFRSI3.Size = New System.Drawing.Size(141, 54)
        Me.cmdTriggerOFFRSI3.TabIndex = 18
        Me.cmdTriggerOFFRSI3.Text = "GIS"
        Me.cmdTriggerOFFRSI3.UseVisualStyleBackColor = False
        '
        'cmdTriggerONRSI2
        '
        Me.cmdTriggerONRSI2.AccessibleDescription = ""
        Me.cmdTriggerONRSI2.BackColor = System.Drawing.Color.Red
        Me.cmdTriggerONRSI2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerONRSI2.Location = New System.Drawing.Point(65, 261)
        Me.cmdTriggerONRSI2.Name = "cmdTriggerONRSI2"
        Me.cmdTriggerONRSI2.Size = New System.Drawing.Size(132, 55)
        Me.cmdTriggerONRSI2.TabIndex = 17
        Me.cmdTriggerONRSI2.Text = "OOS"
        Me.cmdTriggerONRSI2.UseVisualStyleBackColor = False
        '
        'cmdTriggerOFFRSI2
        '
        Me.cmdTriggerOFFRSI2.BackColor = System.Drawing.Color.Lime
        Me.cmdTriggerOFFRSI2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerOFFRSI2.Location = New System.Drawing.Point(203, 261)
        Me.cmdTriggerOFFRSI2.Name = "cmdTriggerOFFRSI2"
        Me.cmdTriggerOFFRSI2.Size = New System.Drawing.Size(141, 55)
        Me.cmdTriggerOFFRSI2.TabIndex = 16
        Me.cmdTriggerOFFRSI2.Text = "GIS"
        Me.cmdTriggerOFFRSI2.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(361, 275)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "RSI L4:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(361, 219)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "RSI L3:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 270)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "RSI L2:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 218)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "RSI L1:"
        '
        'cmdTriggerONRSI1
        '
        Me.cmdTriggerONRSI1.AccessibleDescription = ""
        Me.cmdTriggerONRSI1.BackColor = System.Drawing.Color.Red
        Me.cmdTriggerONRSI1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerONRSI1.Location = New System.Drawing.Point(65, 201)
        Me.cmdTriggerONRSI1.Name = "cmdTriggerONRSI1"
        Me.cmdTriggerONRSI1.Size = New System.Drawing.Size(132, 54)
        Me.cmdTriggerONRSI1.TabIndex = 11
        Me.cmdTriggerONRSI1.Text = "OOS"
        Me.cmdTriggerONRSI1.UseVisualStyleBackColor = False
        '
        'cmdTriggerOFFRSI1
        '
        Me.cmdTriggerOFFRSI1.BackColor = System.Drawing.Color.Lime
        Me.cmdTriggerOFFRSI1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTriggerOFFRSI1.Location = New System.Drawing.Point(203, 201)
        Me.cmdTriggerOFFRSI1.Name = "cmdTriggerOFFRSI1"
        Me.cmdTriggerOFFRSI1.Size = New System.Drawing.Size(141, 54)
        Me.cmdTriggerOFFRSI1.TabIndex = 10
        Me.cmdTriggerOFFRSI1.Text = "GIS"
        Me.cmdTriggerOFFRSI1.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Methods"
        '
        'btnDiagDeviceNDC
        '
        Me.btnDiagDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDiagDeviceNDC.Location = New System.Drawing.Point(203, 135)
        Me.btnDiagDeviceNDC.Name = "btnDiagDeviceNDC"
        Me.btnDiagDeviceNDC.Size = New System.Drawing.Size(226, 47)
        Me.btnDiagDeviceNDC.TabIndex = 4
        Me.btnDiagDeviceNDC.Text = "DiagnosticDevice"
        Me.btnDiagDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWakeDeviceNDC
        '
        Me.btnWakeDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWakeDeviceNDC.Location = New System.Drawing.Point(19, 135)
        Me.btnWakeDeviceNDC.Name = "btnWakeDeviceNDC"
        Me.btnWakeDeviceNDC.Size = New System.Drawing.Size(178, 47)
        Me.btnWakeDeviceNDC.TabIndex = 4
        Me.btnWakeDeviceNDC.Text = "WakeUpDevice"
        Me.btnWakeDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnClearText
        '
        Me.btnClearText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearText.Location = New System.Drawing.Point(626, 135)
        Me.btnClearText.Name = "btnClearText"
        Me.btnClearText.Size = New System.Drawing.Size(106, 47)
        Me.btnClearText.TabIndex = 4
        Me.btnClearText.Text = "Clear Text"
        Me.btnClearText.UseVisualStyleBackColor = True
        '
        'btnGetDevProp
        '
        Me.btnGetDevProp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGetDevProp.Location = New System.Drawing.Point(435, 135)
        Me.btnGetDevProp.Name = "btnGetDevProp"
        Me.btnGetDevProp.Size = New System.Drawing.Size(185, 47)
        Me.btnGetDevProp.TabIndex = 4
        Me.btnGetDevProp.Text = "GetDeviceProperty"
        Me.btnGetDevProp.UseVisualStyleBackColor = True
        '
        'btnUnlockDeviceNDC
        '
        Me.btnUnlockDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnlockDeviceNDC.Location = New System.Drawing.Point(605, 77)
        Me.btnUnlockDeviceNDC.Name = "btnUnlockDeviceNDC"
        Me.btnUnlockDeviceNDC.Size = New System.Drawing.Size(127, 52)
        Me.btnUnlockDeviceNDC.TabIndex = 4
        Me.btnUnlockDeviceNDC.Text = "UnlockDevice"
        Me.btnUnlockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnLockDeviceNDC
        '
        Me.btnLockDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLockDeviceNDC.Location = New System.Drawing.Point(471, 77)
        Me.btnLockDeviceNDC.Name = "btnLockDeviceNDC"
        Me.btnLockDeviceNDC.Size = New System.Drawing.Size(125, 52)
        Me.btnLockDeviceNDC.TabIndex = 4
        Me.btnLockDeviceNDC.Text = "LockDevice"
        Me.btnLockDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnWrapDeviceNDC
        '
        Me.btnWrapDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWrapDeviceNDC.Location = New System.Drawing.Point(323, 77)
        Me.btnWrapDeviceNDC.Name = "btnWrapDeviceNDC"
        Me.btnWrapDeviceNDC.Size = New System.Drawing.Size(136, 52)
        Me.btnWrapDeviceNDC.TabIndex = 4
        Me.btnWrapDeviceNDC.Text = "WrapDevice"
        Me.btnWrapDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnStopDeviceNDC
        '
        Me.btnStopDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStopDeviceNDC.Location = New System.Drawing.Point(165, 77)
        Me.btnStopDeviceNDC.Name = "btnStopDeviceNDC"
        Me.btnStopDeviceNDC.Size = New System.Drawing.Size(146, 52)
        Me.btnStopDeviceNDC.TabIndex = 4
        Me.btnStopDeviceNDC.Text = "StopDevice"
        Me.btnStopDeviceNDC.UseVisualStyleBackColor = True
        '
        'btnStartDeviceNDC
        '
        Me.btnStartDeviceNDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartDeviceNDC.Location = New System.Drawing.Point(19, 77)
        Me.btnStartDeviceNDC.Name = "btnStartDeviceNDC"
        Me.btnStartDeviceNDC.Size = New System.Drawing.Size(139, 52)
        Me.btnStartDeviceNDC.TabIndex = 4
        Me.btnStartDeviceNDC.Text = "StartDevice"
        Me.btnStartDeviceNDC.UseVisualStyleBackColor = True
        '
        'frmRSIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.txtDeviceProperty)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmRSIMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS:RSI"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdTriggerONRSI4 As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerOFFRSI4 As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerONRSI3 As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerOFFRSI3 As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerONRSI2 As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerOFFRSI2 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdTriggerONRSI1 As System.Windows.Forms.Button
    Friend WithEvents cmdTriggerOFFRSI1 As System.Windows.Forms.Button
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
    Friend WithEvents btnRSISetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button

End Class
