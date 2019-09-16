<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUPSSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUPSSetting))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSelectPath = New System.Windows.Forms.Button()
        Me.txtUPSStatusPath = New System.Windows.Forms.TextBox()
        Me.rbDisable = New System.Windows.Forms.RadioButton()
        Me.rbEnable = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.opnFile = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSelectPath)
        Me.GroupBox1.Controls.Add(Me.txtUPSStatusPath)
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
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Maintenance: UPS Setting"
        '
        'btnSelectPath
        '
        Me.btnSelectPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectPath.Location = New System.Drawing.Point(460, 267)
        Me.btnSelectPath.Name = "btnSelectPath"
        Me.btnSelectPath.Size = New System.Drawing.Size(276, 61)
        Me.btnSelectPath.TabIndex = 34
        Me.btnSelectPath.Text = "Select UPS Status File"
        Me.btnSelectPath.UseVisualStyleBackColor = True
        '
        'txtUPSStatusPath
        '
        Me.txtUPSStatusPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUPSStatusPath.Location = New System.Drawing.Point(14, 127)
        Me.txtUPSStatusPath.Multiline = True
        Me.txtUPSStatusPath.Name = "txtUPSStatusPath"
        Me.txtUPSStatusPath.ReadOnly = True
        Me.txtUPSStatusPath.Size = New System.Drawing.Size(722, 134)
        Me.txtUPSStatusPath.TabIndex = 33
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(165, 24)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "UPS Status Path:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(60, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 24)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "UPS Device:"
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirm.Location = New System.Drawing.Point(338, 372)
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
        Me.btnExit.Location = New System.Drawing.Point(552, 372)
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
        'frmUPSSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUPSSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Maintenance: UPS Setting"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbDisable As System.Windows.Forms.RadioButton
    Friend WithEvents rbEnable As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSelectPath As System.Windows.Forms.Button
    Friend WithEvents txtUPSStatusPath As System.Windows.Forms.TextBox
    Friend WithEvents opnFile As System.Windows.Forms.OpenFileDialog
End Class
