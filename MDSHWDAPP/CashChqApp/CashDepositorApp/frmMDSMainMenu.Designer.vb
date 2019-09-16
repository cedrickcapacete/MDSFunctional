<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMDSMainMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMDSMainMenu))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdMDSDoor = New System.Windows.Forms.Button()
        Me.cmdCheque = New System.Windows.Forms.Button()
        Me.cmdCash = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdCancel)
        Me.GroupBox1.Controls.Add(Me.cmdMDSDoor)
        Me.GroupBox1.Controls.Add(Me.cmdCheque)
        Me.GroupBox1.Controls.Add(Me.cmdCash)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(758, 536)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Main Modules"
        '
        'cmdCancel
        '
        Me.cmdCancel.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.Color.Red
        Me.cmdCancel.Location = New System.Drawing.Point(162, 426)
        Me.cmdCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(370, 86)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdMDSDoor
        '
        Me.cmdMDSDoor.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMDSDoor.Location = New System.Drawing.Point(162, 256)
        Me.cmdMDSDoor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdMDSDoor.Name = "cmdMDSDoor"
        Me.cmdMDSDoor.Size = New System.Drawing.Size(370, 106)
        Me.cmdMDSDoor.TabIndex = 2
        Me.cmdMDSDoor.Text = "MDS Door Sensor"
        Me.cmdMDSDoor.UseVisualStyleBackColor = True
        '
        'cmdCheque
        '
        Me.cmdCheque.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCheque.Location = New System.Drawing.Point(162, 142)
        Me.cmdCheque.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdCheque.Name = "cmdCheque"
        Me.cmdCheque.Size = New System.Drawing.Size(370, 106)
        Me.cmdCheque.TabIndex = 1
        Me.cmdCheque.Text = "CHEQUE Module"
        Me.cmdCheque.UseVisualStyleBackColor = True
        '
        'cmdCash
        '
        Me.cmdCash.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCash.Location = New System.Drawing.Point(162, 27)
        Me.cmdCash.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdCash.Name = "cmdCash"
        Me.cmdCash.Size = New System.Drawing.Size(370, 107)
        Me.cmdCash.TabIndex = 0
        Me.cmdCash.Text = "CASH Module"
        Me.cmdCash.UseVisualStyleBackColor = True
        '
        'frmMDSMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMDSMainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS Main Menu"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdMDSDoor As System.Windows.Forms.Button
    Friend WithEvents cmdCheque As System.Windows.Forms.Button
    Friend WithEvents cmdCash As System.Windows.Forms.Button
End Class
