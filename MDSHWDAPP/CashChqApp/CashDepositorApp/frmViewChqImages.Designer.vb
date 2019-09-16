<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewChqImages
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewChqImages))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.picChqImage = New System.Windows.Forms.PictureBox()
        Me.lblTtlChq = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lstChqView = New System.Windows.Forms.ListView()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picChqImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.picChqImage)
        Me.GroupBox1.Controls.Add(Me.lblTtlChq)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.lstChqView)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 523)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "View Cheque Images"
        '
        'picChqImage
        '
        Me.picChqImage.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.picChqImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picChqImage.Location = New System.Drawing.Point(6, 268)
        Me.picChqImage.Name = "picChqImage"
        Me.picChqImage.Size = New System.Drawing.Size(728, 249)
        Me.picChqImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picChqImage.TabIndex = 37
        Me.picChqImage.TabStop = False
        '
        'lblTtlChq
        '
        Me.lblTtlChq.AutoSize = True
        Me.lblTtlChq.Location = New System.Drawing.Point(157, 41)
        Me.lblTtlChq.Name = "lblTtlChq"
        Me.lblTtlChq.Size = New System.Drawing.Size(79, 20)
        Me.lblTtlChq.TabIndex = 36
        Me.lblTtlChq.Text = "lblTtlChq"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 20)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Total Cheques:"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(587, 15)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(147, 46)
        Me.btnExit.TabIndex = 34
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lstChqView
        '
        Me.lstChqView.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lstChqView.Location = New System.Drawing.Point(6, 67)
        Me.lstChqView.Name = "lstChqView"
        Me.lstChqView.Size = New System.Drawing.Size(728, 180)
        Me.lstChqView.TabIndex = 1
        Me.lstChqView.UseCompatibleStateImageBehavior = False
        '
        'frmViewChqImages
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
        Me.Name = "frmViewChqImages"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Chq Images"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picChqImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstChqView As System.Windows.Forms.ListView
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblTtlChq As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picChqImage As System.Windows.Forms.PictureBox
End Class
