<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHWDMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHWDMain))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmd3rdTools = New System.Windows.Forms.Button()
        Me.cmdRestartPC = New System.Windows.Forms.Button()
        Me.cmdSysMaint = New System.Windows.Forms.Button()
        Me.lblApplVersion = New System.Windows.Forms.Label()
        Me.btnBarCode = New System.Windows.Forms.Button()
        Me.btnRSI = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnUPS = New System.Windows.Forms.Button()
        Me.btnBeeper = New System.Windows.Forms.Button()
        Me.btnPrinter = New System.Windows.Forms.Button()
        Me.btnKeyPad = New System.Windows.Forms.Button()
        Me.btnMDS = New System.Windows.Forms.Button()
        Me.btnCardReader = New System.Windows.Forms.Button()
        Me.opnFile = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmd3rdTools)
        Me.GroupBox1.Controls.Add(Me.cmdRestartPC)
        Me.GroupBox1.Controls.Add(Me.cmdSysMaint)
        Me.GroupBox1.Controls.Add(Me.lblApplVersion)
        Me.GroupBox1.Controls.Add(Me.btnBarCode)
        Me.GroupBox1.Controls.Add(Me.btnRSI)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnUPS)
        Me.GroupBox1.Controls.Add(Me.btnBeeper)
        Me.GroupBox1.Controls.Add(Me.btnPrinter)
        Me.GroupBox1.Controls.Add(Me.btnKeyPad)
        Me.GroupBox1.Controls.Add(Me.btnMDS)
        Me.GroupBox1.Controls.Add(Me.btnCardReader)
        Me.GroupBox1.Location = New System.Drawing.Point(29, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 538)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS: Hardware Control Panel"
        '
        'cmd3rdTools
        '
        Me.cmd3rdTools.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd3rdTools.Location = New System.Drawing.Point(489, 260)
        Me.cmd3rdTools.Name = "cmd3rdTools"
        Me.cmd3rdTools.Size = New System.Drawing.Size(216, 78)
        Me.cmd3rdTools.TabIndex = 12
        Me.cmd3rdTools.Text = "Vendor - Diagnostics Tools"
        Me.cmd3rdTools.UseVisualStyleBackColor = True
        '
        'cmdRestartPC
        '
        Me.cmdRestartPC.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRestartPC.Location = New System.Drawing.Point(35, 368)
        Me.cmdRestartPC.Name = "cmdRestartPC"
        Me.cmdRestartPC.Size = New System.Drawing.Size(216, 78)
        Me.cmdRestartPC.TabIndex = 11
        Me.cmdRestartPC.Text = "Restart PC"
        Me.cmdRestartPC.UseVisualStyleBackColor = True
        '
        'cmdSysMaint
        '
        Me.cmdSysMaint.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSysMaint.Location = New System.Drawing.Point(257, 368)
        Me.cmdSysMaint.Name = "cmdSysMaint"
        Me.cmdSysMaint.Size = New System.Drawing.Size(216, 78)
        Me.cmdSysMaint.TabIndex = 10
        Me.cmdSysMaint.Text = "Update Patches"
        Me.cmdSysMaint.UseVisualStyleBackColor = True
        Me.cmdSysMaint.Visible = False
        '
        'lblApplVersion
        '
        Me.lblApplVersion.AutoSize = True
        Me.lblApplVersion.Location = New System.Drawing.Point(53, 471)
        Me.lblApplVersion.Name = "lblApplVersion"
        Me.lblApplVersion.Size = New System.Drawing.Size(110, 16)
        Me.lblApplVersion.TabIndex = 9
        Me.lblApplVersion.Text = "lblApplVersion"
        '
        'btnBarCode
        '
        Me.btnBarCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBarCode.Location = New System.Drawing.Point(257, 260)
        Me.btnBarCode.Name = "btnBarCode"
        Me.btnBarCode.Size = New System.Drawing.Size(216, 78)
        Me.btnBarCode.TabIndex = 8
        Me.btnBarCode.Text = "Barcode"
        Me.btnBarCode.UseVisualStyleBackColor = True
        '
        'btnRSI
        '
        Me.btnRSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRSI.Location = New System.Drawing.Point(35, 260)
        Me.btnRSI.Name = "btnRSI"
        Me.btnRSI.Size = New System.Drawing.Size(216, 78)
        Me.btnRSI.TabIndex = 7
        Me.btnRSI.Text = "RSI"
        Me.btnRSI.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(489, 368)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(216, 78)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnUPS
        '
        Me.btnUPS.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUPS.Location = New System.Drawing.Point(489, 150)
        Me.btnUPS.Name = "btnUPS"
        Me.btnUPS.Size = New System.Drawing.Size(216, 78)
        Me.btnUPS.TabIndex = 5
        Me.btnUPS.Text = "UPS"
        Me.btnUPS.UseVisualStyleBackColor = True
        '
        'btnBeeper
        '
        Me.btnBeeper.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBeeper.Location = New System.Drawing.Point(257, 150)
        Me.btnBeeper.Name = "btnBeeper"
        Me.btnBeeper.Size = New System.Drawing.Size(216, 78)
        Me.btnBeeper.TabIndex = 4
        Me.btnBeeper.Text = "Beeper"
        Me.btnBeeper.UseVisualStyleBackColor = True
        '
        'btnPrinter
        '
        Me.btnPrinter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrinter.Location = New System.Drawing.Point(35, 150)
        Me.btnPrinter.Name = "btnPrinter"
        Me.btnPrinter.Size = New System.Drawing.Size(216, 78)
        Me.btnPrinter.TabIndex = 3
        Me.btnPrinter.Text = "Printer"
        Me.btnPrinter.UseVisualStyleBackColor = True
        '
        'btnKeyPad
        '
        Me.btnKeyPad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnKeyPad.Location = New System.Drawing.Point(489, 43)
        Me.btnKeyPad.Name = "btnKeyPad"
        Me.btnKeyPad.Size = New System.Drawing.Size(216, 78)
        Me.btnKeyPad.TabIndex = 2
        Me.btnKeyPad.Text = "Key Pad / EPP"
        Me.btnKeyPad.UseVisualStyleBackColor = True
        '
        'btnMDS
        '
        Me.btnMDS.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMDS.Location = New System.Drawing.Point(257, 43)
        Me.btnMDS.Name = "btnMDS"
        Me.btnMDS.Size = New System.Drawing.Size(216, 78)
        Me.btnMDS.TabIndex = 1
        Me.btnMDS.Text = "MDS"
        Me.btnMDS.UseVisualStyleBackColor = True
        '
        'btnCardReader
        '
        Me.btnCardReader.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCardReader.Location = New System.Drawing.Point(35, 43)
        Me.btnCardReader.Name = "btnCardReader"
        Me.btnCardReader.Size = New System.Drawing.Size(216, 78)
        Me.btnCardReader.TabIndex = 0
        Me.btnCardReader.Text = "Card Reader"
        Me.btnCardReader.UseVisualStyleBackColor = True
        '
        'opnFile
        '
        Me.opnFile.Multiselect = True
        '
        'frmHWDMain
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
        Me.Name = "frmHWDMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS: Hardware Control Panel"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRSI As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnUPS As System.Windows.Forms.Button
    Friend WithEvents btnBeeper As System.Windows.Forms.Button
    Friend WithEvents btnPrinter As System.Windows.Forms.Button
    Friend WithEvents btnKeyPad As System.Windows.Forms.Button
    Friend WithEvents btnMDS As System.Windows.Forms.Button
    Friend WithEvents btnCardReader As System.Windows.Forms.Button
    Friend WithEvents btnBarCode As System.Windows.Forms.Button
    Friend WithEvents lblApplVersion As System.Windows.Forms.Label
    Friend WithEvents cmdRestartPC As System.Windows.Forms.Button
    Friend WithEvents cmdSysMaint As System.Windows.Forms.Button
    Friend WithEvents opnFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmd3rdTools As System.Windows.Forms.Button

End Class
