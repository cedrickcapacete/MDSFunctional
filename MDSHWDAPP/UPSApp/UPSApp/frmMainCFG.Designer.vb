<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainCFG
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainCFG))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnSystemMode = New System.Windows.Forms.Button()
        Me.btnDiagnostic = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnSystemMode)
        Me.GroupBox1.Controls.Add(Me.btnDiagnostic)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(736, 520)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: UPS"
        '
        'btnExit
        '
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(133, 356)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(430, 109)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSystemMode
        '
        Me.btnSystemMode.Location = New System.Drawing.Point(133, 216)
        Me.btnSystemMode.Name = "btnSystemMode"
        Me.btnSystemMode.Size = New System.Drawing.Size(430, 109)
        Me.btnSystemMode.TabIndex = 1
        Me.btnSystemMode.Text = "System Mode"
        Me.btnSystemMode.UseVisualStyleBackColor = True
        '
        'btnDiagnostic
        '
        Me.btnDiagnostic.Location = New System.Drawing.Point(133, 75)
        Me.btnDiagnostic.Name = "btnDiagnostic"
        Me.btnDiagnostic.Size = New System.Drawing.Size(430, 109)
        Me.btnDiagnostic.TabIndex = 0
        Me.btnDiagnostic.Text = "Diagnostic"
        Me.btnDiagnostic.UseVisualStyleBackColor = True
        '
        'frmMainCFG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainCFG"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS:UPS Main Control Panel"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSystemMode As System.Windows.Forms.Button
    Friend WithEvents btnDiagnostic As System.Windows.Forms.Button
End Class
