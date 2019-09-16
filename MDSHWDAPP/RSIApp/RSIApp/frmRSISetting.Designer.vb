<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRSISetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRSISetting))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.NmComport2 = New System.Windows.Forms.NumericUpDown()
        Me.NmComport1 = New System.Windows.Forms.NumericUpDown()
        Me.NmNoofRSIPort = New System.Windows.Forms.NumericUpDown()
        Me.rbDisable = New System.Windows.Forms.RadioButton()
        Me.rbEnable = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NmComport2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NmComport1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NmNoofRSIPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.NmComport2)
        Me.GroupBox1.Controls.Add(Me.NmComport1)
        Me.GroupBox1.Controls.Add(Me.NmNoofRSIPort)
        Me.GroupBox1.Controls.Add(Me.rbDisable)
        Me.GroupBox1.Controls.Add(Me.rbEnable)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnConfirm)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(760, 538)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Maintenance: RSI Setting"
        '
        'NmComport2
        '
        Me.NmComport2.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NmComport2.Location = New System.Drawing.Point(200, 274)
        Me.NmComport2.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NmComport2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NmComport2.Name = "NmComport2"
        Me.NmComport2.Size = New System.Drawing.Size(346, 62)
        Me.NmComport2.TabIndex = 35
        Me.NmComport2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NmComport1
        '
        Me.NmComport1.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NmComport1.Location = New System.Drawing.Point(200, 175)
        Me.NmComport1.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NmComport1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NmComport1.Name = "NmComport1"
        Me.NmComport1.Size = New System.Drawing.Size(346, 62)
        Me.NmComport1.TabIndex = 34
        Me.NmComport1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NmNoofRSIPort
        '
        Me.NmNoofRSIPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NmNoofRSIPort.Location = New System.Drawing.Point(200, 82)
        Me.NmNoofRSIPort.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NmNoofRSIPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NmNoofRSIPort.Name = "NmNoofRSIPort"
        Me.NmNoofRSIPort.Size = New System.Drawing.Size(346, 62)
        Me.NmNoofRSIPort.TabIndex = 33
        Me.NmNoofRSIPort.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'rbDisable
        '
        Me.rbDisable.AutoSize = True
        Me.rbDisable.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDisable.Location = New System.Drawing.Point(351, 24)
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
        Me.rbEnable.Location = New System.Drawing.Point(184, 24)
        Me.rbEnable.Name = "rbEnable"
        Me.rbEnable.Size = New System.Drawing.Size(129, 37)
        Me.rbEnable.TabIndex = 31
        Me.rbEnable.TabStop = True
        Me.rbEnable.Text = "Enable"
        Me.rbEnable.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(38, 301)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(156, 24)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "2. RSI Comport:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(35, 192)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(156, 24)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "1. RSI Comport:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(189, 24)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "No of RSI Comport:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(60, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 24)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "RSI Device:"
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirm.Location = New System.Drawing.Point(288, 382)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(184, 61)
        Me.btnConfirm.TabIndex = 26
        Me.btnConfirm.Text = "Confirm"
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(478, 382)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 61)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmRSISetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmRSISetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Maintenance: RSI Setting"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NmComport2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NmComport1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NmNoofRSIPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents NmComport2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NmComport1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NmNoofRSIPort As System.Windows.Forms.NumericUpDown
    Friend WithEvents rbDisable As System.Windows.Forms.RadioButton
    Friend WithEvents rbEnable As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
