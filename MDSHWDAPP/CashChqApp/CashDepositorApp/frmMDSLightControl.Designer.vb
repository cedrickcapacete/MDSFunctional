<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMDSLightControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMDSLightControl))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdCRFlushSlow = New System.Windows.Forms.Button()
        Me.cmdCRFlushFast = New System.Windows.Forms.Button()
        Me.cmdMDSCRLightOFF = New System.Windows.Forms.Button()
        Me.cmdMDSCRLightON = New System.Windows.Forms.Button()
        Me.cmdMDSLightOFF = New System.Windows.Forms.Button()
        Me.cmdMDSLightON = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdCRFlushSlow)
        Me.GroupBox1.Controls.Add(Me.cmdCRFlushFast)
        Me.GroupBox1.Controls.Add(Me.cmdMDSCRLightOFF)
        Me.GroupBox1.Controls.Add(Me.cmdMDSCRLightON)
        Me.GroupBox1.Controls.Add(Me.cmdMDSLightOFF)
        Me.GroupBox1.Controls.Add(Me.cmdMDSLightON)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(745, 538)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: MDS Light"
        '
        'cmdCRFlushSlow
        '
        Me.cmdCRFlushSlow.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCRFlushSlow.Location = New System.Drawing.Point(431, 259)
        Me.cmdCRFlushSlow.Name = "cmdCRFlushSlow"
        Me.cmdCRFlushSlow.Size = New System.Drawing.Size(168, 62)
        Me.cmdCRFlushSlow.TabIndex = 45
        Me.cmdCRFlushSlow.Text = "Flashing Slow"
        Me.cmdCRFlushSlow.UseVisualStyleBackColor = True
        '
        'cmdCRFlushFast
        '
        Me.cmdCRFlushFast.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCRFlushFast.Location = New System.Drawing.Point(231, 259)
        Me.cmdCRFlushFast.Name = "cmdCRFlushFast"
        Me.cmdCRFlushFast.Size = New System.Drawing.Size(168, 62)
        Me.cmdCRFlushFast.TabIndex = 44
        Me.cmdCRFlushFast.Text = "Flashing Fast"
        Me.cmdCRFlushFast.UseVisualStyleBackColor = True
        '
        'cmdMDSCRLightOFF
        '
        Me.cmdMDSCRLightOFF.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMDSCRLightOFF.Location = New System.Drawing.Point(431, 191)
        Me.cmdMDSCRLightOFF.Name = "cmdMDSCRLightOFF"
        Me.cmdMDSCRLightOFF.Size = New System.Drawing.Size(168, 62)
        Me.cmdMDSCRLightOFF.TabIndex = 43
        Me.cmdMDSCRLightOFF.Text = "OFF"
        Me.cmdMDSCRLightOFF.UseVisualStyleBackColor = True
        '
        'cmdMDSCRLightON
        '
        Me.cmdMDSCRLightON.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMDSCRLightON.Location = New System.Drawing.Point(231, 191)
        Me.cmdMDSCRLightON.Name = "cmdMDSCRLightON"
        Me.cmdMDSCRLightON.Size = New System.Drawing.Size(168, 62)
        Me.cmdMDSCRLightON.TabIndex = 42
        Me.cmdMDSCRLightON.Text = "ON"
        Me.cmdMDSCRLightON.UseVisualStyleBackColor = True
        '
        'cmdMDSLightOFF
        '
        Me.cmdMDSLightOFF.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMDSLightOFF.Location = New System.Drawing.Point(431, 113)
        Me.cmdMDSLightOFF.Name = "cmdMDSLightOFF"
        Me.cmdMDSLightOFF.Size = New System.Drawing.Size(168, 59)
        Me.cmdMDSLightOFF.TabIndex = 41
        Me.cmdMDSLightOFF.Text = "OFF"
        Me.cmdMDSLightOFF.UseVisualStyleBackColor = True
        '
        'cmdMDSLightON
        '
        Me.cmdMDSLightON.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMDSLightON.Location = New System.Drawing.Point(231, 113)
        Me.cmdMDSLightON.Name = "cmdMDSLightON"
        Me.cmdMDSLightON.Size = New System.Drawing.Size(168, 62)
        Me.cmdMDSLightON.TabIndex = 40
        Me.cmdMDSLightON.Text = "ON"
        Me.cmdMDSLightON.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(543, 23)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 61)
        Me.btnExit.TabIndex = 34
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 212)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(205, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "MDS Card Reader Light:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(112, 132)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "MDS Light:"
        '
        'frmMDSLightControl
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
        Me.Name = "frmMDSLightControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Devices: MDS Light"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cmdCRFlushSlow As System.Windows.Forms.Button
    Friend WithEvents cmdCRFlushFast As System.Windows.Forms.Button
    Friend WithEvents cmdMDSCRLightOFF As System.Windows.Forms.Button
    Friend WithEvents cmdMDSCRLightON As System.Windows.Forms.Button
    Friend WithEvents cmdMDSLightOFF As System.Windows.Forms.Button
    Friend WithEvents cmdMDSLightON As System.Windows.Forms.Button
End Class
