<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCardReaderSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCardReaderSetting))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nmCheckingTimeout = New System.Windows.Forms.NumericUpDown()
        Me.nmEjectTimeout = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nmCMDTimeout = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NmComport = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSelectPath = New System.Windows.Forms.Button()
        Me.txtPSAMIDPath = New System.Windows.Forms.TextBox()
        Me.rbDisable = New System.Windows.Forms.RadioButton()
        Me.rbEnable = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.opnFile = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmCheckingTimeout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmEjectTimeout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmCMDTimeout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NmComport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.nmCheckingTimeout)
        Me.GroupBox1.Controls.Add(Me.nmEjectTimeout)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.nmCMDTimeout)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.NmComport)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnSelectPath)
        Me.GroupBox1.Controls.Add(Me.txtPSAMIDPath)
        Me.GroupBox1.Controls.Add(Me.rbDisable)
        Me.GroupBox1.Controls.Add(Me.rbEnable)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnConfirm)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(760, 538)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Maintenance: Card Reader Setting"
        '
        'nmCheckingTimeout
        '
        Me.nmCheckingTimeout.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmCheckingTimeout.Location = New System.Drawing.Point(603, 162)
        Me.nmCheckingTimeout.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.nmCheckingTimeout.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmCheckingTimeout.Name = "nmCheckingTimeout"
        Me.nmCheckingTimeout.Size = New System.Drawing.Size(118, 49)
        Me.nmCheckingTimeout.TabIndex = 43
        Me.nmCheckingTimeout.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmEjectTimeout
        '
        Me.nmEjectTimeout.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmEjectTimeout.Location = New System.Drawing.Point(332, 162)
        Me.nmEjectTimeout.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.nmEjectTimeout.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmEjectTimeout.Name = "nmEjectTimeout"
        Me.nmEjectTimeout.Size = New System.Drawing.Size(118, 49)
        Me.nmEjectTimeout.TabIndex = 42
        Me.nmEjectTimeout.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(477, 172)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 24)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "CMD Reply:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(263, 172)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 24)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Eject:"
        '
        'nmCMDTimeout
        '
        Me.nmCMDTimeout.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmCMDTimeout.Location = New System.Drawing.Point(138, 162)
        Me.nmCMDTimeout.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.nmCMDTimeout.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmCMDTimeout.Name = "nmCMDTimeout"
        Me.nmCMDTimeout.Size = New System.Drawing.Size(107, 49)
        Me.nmCMDTimeout.TabIndex = 39
        Me.nmCMDTimeout.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 172)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 24)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Command:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(40, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 24)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Timeout (sec)"
        '
        'NmComport
        '
        Me.NmComport.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NmComport.Location = New System.Drawing.Point(231, 67)
        Me.NmComport.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NmComport.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NmComport.Name = "NmComport"
        Me.NmComport.Size = New System.Drawing.Size(346, 49)
        Me.NmComport.TabIndex = 36
        Me.NmComport.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(130, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 24)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Comport:"
        '
        'btnSelectPath
        '
        Me.btnSelectPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectPath.Location = New System.Drawing.Point(460, 326)
        Me.btnSelectPath.Name = "btnSelectPath"
        Me.btnSelectPath.Size = New System.Drawing.Size(276, 61)
        Me.btnSelectPath.TabIndex = 34
        Me.btnSelectPath.Text = "Select PSAM Key File"
        Me.btnSelectPath.UseVisualStyleBackColor = True
        '
        'txtPSAMIDPath
        '
        Me.txtPSAMIDPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPSAMIDPath.Location = New System.Drawing.Point(14, 249)
        Me.txtPSAMIDPath.Multiline = True
        Me.txtPSAMIDPath.Name = "txtPSAMIDPath"
        Me.txtPSAMIDPath.ReadOnly = True
        Me.txtPSAMIDPath.Size = New System.Drawing.Size(722, 71)
        Me.txtPSAMIDPath.TabIndex = 33
        '
        'rbDisable
        '
        Me.rbDisable.AutoSize = True
        Me.rbDisable.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDisable.Location = New System.Drawing.Point(366, 24)
        Me.rbDisable.Name = "rbDisable"
        Me.rbDisable.Size = New System.Drawing.Size(138, 37)
        Me.rbDisable.TabIndex = 32
        Me.rbDisable.TabStop = True
        Me.rbDisable.Text = "Disable"
        Me.rbDisable.UseVisualStyleBackColor = True
        '
        'rbEnable
        '
        Me.rbEnable.AutoSize = True
        Me.rbEnable.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbEnable.Location = New System.Drawing.Point(231, 24)
        Me.rbEnable.Name = "rbEnable"
        Me.rbEnable.Size = New System.Drawing.Size(129, 37)
        Me.rbEnable.TabIndex = 31
        Me.rbEnable.TabStop = True
        Me.rbEnable.Text = "Enable"
        Me.rbEnable.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 222)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(161, 24)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "PSAM Key Path:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(204, 24)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Card Reader Device:"
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirm.Location = New System.Drawing.Point(351, 408)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(184, 72)
        Me.btnConfirm.TabIndex = 26
        Me.btnConfirm.Text = "Confirm"
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(552, 408)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 72)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'opnFile
        '
        Me.opnFile.Multiselect = True
        '
        'frmCardReaderSetting
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
        Me.Name = "frmCardReaderSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Maintenance: Card Reader Setting"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmCheckingTimeout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmEjectTimeout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmCMDTimeout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NmComport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents nmCheckingTimeout As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmEjectTimeout As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nmCMDTimeout As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NmComport As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSelectPath As System.Windows.Forms.Button
    Friend WithEvents txtPSAMIDPath As System.Windows.Forms.TextBox
    Friend WithEvents rbDisable As System.Windows.Forms.RadioButton
    Friend WithEvents rbEnable As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents opnFile As System.Windows.Forms.OpenFileDialog
End Class
