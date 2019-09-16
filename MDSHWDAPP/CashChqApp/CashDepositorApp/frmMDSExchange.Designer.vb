<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMDSExchange
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMDSExchange))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblMDSResetMsg = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdStartChqExchange = New System.Windows.Forms.Button()
        Me.lblTtlChq = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lstView = New System.Windows.Forms.ListView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdEmptyChq = New System.Windows.Forms.Button()
        Me.cmdEndExchange = New System.Windows.Forms.Button()
        Me.cmdStartExchange = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblMDSResetMsg)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmdStartChqExchange)
        Me.GroupBox1.Controls.Add(Me.lblTtlChq)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lstView)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmdEmptyChq)
        Me.GroupBox1.Controls.Add(Me.cmdEndExchange)
        Me.GroupBox1.Controls.Add(Me.cmdStartExchange)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(745, 538)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: MDS Exchange"
        '
        'lblMDSResetMsg
        '
        Me.lblMDSResetMsg.AutoSize = True
        Me.lblMDSResetMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDSResetMsg.Location = New System.Drawing.Point(37, 510)
        Me.lblMDSResetMsg.Name = "lblMDSResetMsg"
        Me.lblMDSResetMsg.Size = New System.Drawing.Size(118, 15)
        Me.lblMDSResetMsg.TabIndex = 45
        Me.lblMDSResetMsg.Text = "lblMDSResetMsg"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(36, 409)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(165, 20)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "2. Empty Cassettes"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 313)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(237, 20)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Operator- Cheque Exchange"
        '
        'cmdStartChqExchange
        '
        Me.cmdStartChqExchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStartChqExchange.Location = New System.Drawing.Point(24, 337)
        Me.cmdStartChqExchange.Name = "cmdStartChqExchange"
        Me.cmdStartChqExchange.Size = New System.Drawing.Size(184, 61)
        Me.cmdStartChqExchange.TabIndex = 42
        Me.cmdStartChqExchange.Text = "1. Start Exchange"
        Me.cmdStartChqExchange.UseVisualStyleBackColor = True
        '
        'lblTtlChq
        '
        Me.lblTtlChq.AutoSize = True
        Me.lblTtlChq.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTtlChq.Location = New System.Drawing.Point(358, 390)
        Me.lblTtlChq.Name = "lblTtlChq"
        Me.lblTtlChq.Size = New System.Drawing.Size(79, 20)
        Me.lblTtlChq.TabIndex = 41
        Me.lblTtlChq.Text = "lblTtlChq"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(231, 390)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(121, 20)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Total Cheque:"
        '
        'lstView
        '
        Me.lstView.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lstView.Location = New System.Drawing.Point(226, 87)
        Me.lstView.Name = "lstView"
        Me.lstView.Size = New System.Drawing.Size(501, 214)
        Me.lstView.TabIndex = 39
        Me.lstView.UseCompatibleStateImageBehavior = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(36, 181)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(165, 20)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "2. Empty Cassettes"
        '
        'cmdEmptyChq
        '
        Me.cmdEmptyChq.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEmptyChq.Location = New System.Drawing.Point(24, 442)
        Me.cmdEmptyChq.Name = "cmdEmptyChq"
        Me.cmdEmptyChq.Size = New System.Drawing.Size(184, 61)
        Me.cmdEmptyChq.TabIndex = 37
        Me.cmdEmptyChq.Text = "3.End Exchange"
        Me.cmdEmptyChq.UseVisualStyleBackColor = True
        '
        'cmdEndExchange
        '
        Me.cmdEndExchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEndExchange.Location = New System.Drawing.Point(24, 232)
        Me.cmdEndExchange.Name = "cmdEndExchange"
        Me.cmdEndExchange.Size = New System.Drawing.Size(184, 61)
        Me.cmdEndExchange.TabIndex = 36
        Me.cmdEndExchange.Text = "3.End Exchange"
        Me.cmdEndExchange.UseVisualStyleBackColor = True
        '
        'cmdStartExchange
        '
        Me.cmdStartExchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStartExchange.Location = New System.Drawing.Point(24, 98)
        Me.cmdStartExchange.Name = "cmdStartExchange"
        Me.cmdStartExchange.Size = New System.Drawing.Size(184, 61)
        Me.cmdStartExchange.TabIndex = 35
        Me.cmdStartExchange.Text = "1. Start Exchange"
        Me.cmdStartExchange.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(543, 23)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 50)
        Me.btnExit.TabIndex = 34
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(36, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(216, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Operator- Cash Exchange"
        '
        'frmMDSExchange
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
        Me.Name = "frmMDSExchange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Devices: MDS ExChange"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdEmptyChq As System.Windows.Forms.Button
    Friend WithEvents cmdEndExchange As System.Windows.Forms.Button
    Friend WithEvents cmdStartExchange As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstView As System.Windows.Forms.ListView
    Friend WithEvents lblTtlChq As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdStartChqExchange As System.Windows.Forms.Button
    Friend WithEvents lblMDSResetMsg As System.Windows.Forms.Label
End Class
