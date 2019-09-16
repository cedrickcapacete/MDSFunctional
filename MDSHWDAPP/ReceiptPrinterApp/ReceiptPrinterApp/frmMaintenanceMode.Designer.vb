<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaintenanceMode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaintenanceMode))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblPrinterStatus = New System.Windows.Forms.Label()
        Me.btnPrinterFailed = New System.Windows.Forms.Button()
        Me.btnPaperJam = New System.Windows.Forms.Button()
        Me.btnPaperEnd = New System.Windows.Forms.Button()
        Me.btnPaperLow = New System.Windows.Forms.Button()
        Me.btnPaperOK = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblPrintMsg = New System.Windows.Forms.Label()
        Me.cmdSendImgToPrinter = New System.Windows.Forms.Button()
        Me.cmdSendTextToPrinter = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.txtSection1 = New System.Windows.Forms.TextBox()
        Me.cmdUpload = New System.Windows.Forms.Button()
        Me.txtFileName1 = New System.Windows.Forms.TextBox()
        Me.cmdPrinterStatus = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdInit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnPrinterSetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblPrinterStatus)
        Me.GroupBox1.Controls.Add(Me.btnPrinterFailed)
        Me.GroupBox1.Controls.Add(Me.btnPaperJam)
        Me.GroupBox1.Controls.Add(Me.btnPaperEnd)
        Me.GroupBox1.Controls.Add(Me.btnPaperLow)
        Me.GroupBox1.Controls.Add(Me.btnPaperOK)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.cmdPrinterStatus)
        Me.GroupBox1.Controls.Add(Me.cmdClose)
        Me.GroupBox1.Controls.Add(Me.cmdInit)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnPrinterSetting)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 528)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: Printer"
        '
        'lblPrinterStatus
        '
        Me.lblPrinterStatus.AutoSize = True
        Me.lblPrinterStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrinterStatus.Location = New System.Drawing.Point(102, 160)
        Me.lblPrinterStatus.Name = "lblPrinterStatus"
        Me.lblPrinterStatus.Size = New System.Drawing.Size(37, 13)
        Me.lblPrinterStatus.TabIndex = 49
        Me.lblPrinterStatus.Text = "False"
        '
        'btnPrinterFailed
        '
        Me.btnPrinterFailed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrinterFailed.Location = New System.Drawing.Point(10, 338)
        Me.btnPrinterFailed.Name = "btnPrinterFailed"
        Me.btnPrinterFailed.Size = New System.Drawing.Size(119, 54)
        Me.btnPrinterFailed.TabIndex = 48
        Me.btnPrinterFailed.Text = "Printer Failed"
        Me.btnPrinterFailed.UseVisualStyleBackColor = True
        '
        'btnPaperJam
        '
        Me.btnPaperJam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPaperJam.Location = New System.Drawing.Point(10, 301)
        Me.btnPaperJam.Name = "btnPaperJam"
        Me.btnPaperJam.Size = New System.Drawing.Size(119, 31)
        Me.btnPaperJam.TabIndex = 47
        Me.btnPaperJam.Text = "Paper Jam"
        Me.btnPaperJam.UseVisualStyleBackColor = True
        '
        'btnPaperEnd
        '
        Me.btnPaperEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPaperEnd.Location = New System.Drawing.Point(10, 264)
        Me.btnPaperEnd.Name = "btnPaperEnd"
        Me.btnPaperEnd.Size = New System.Drawing.Size(119, 31)
        Me.btnPaperEnd.TabIndex = 46
        Me.btnPaperEnd.Text = "Paper End"
        Me.btnPaperEnd.UseVisualStyleBackColor = True
        '
        'btnPaperLow
        '
        Me.btnPaperLow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPaperLow.Location = New System.Drawing.Point(10, 227)
        Me.btnPaperLow.Name = "btnPaperLow"
        Me.btnPaperLow.Size = New System.Drawing.Size(119, 31)
        Me.btnPaperLow.TabIndex = 45
        Me.btnPaperLow.Text = "Paper Low"
        Me.btnPaperLow.UseVisualStyleBackColor = True
        '
        'btnPaperOK
        '
        Me.btnPaperOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPaperOK.Location = New System.Drawing.Point(10, 190)
        Me.btnPaperOK.Name = "btnPaperOK"
        Me.btnPaperOK.Size = New System.Drawing.Size(119, 31)
        Me.btnPaperOK.TabIndex = 44
        Me.btnPaperOK.Text = "Paper OK"
        Me.btnPaperOK.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Printer Status:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblPrintMsg)
        Me.GroupBox2.Controls.Add(Me.cmdSendImgToPrinter)
        Me.GroupBox2.Controls.Add(Me.cmdSendTextToPrinter)
        Me.GroupBox2.Controls.Add(Me.cmdPrint)
        Me.GroupBox2.Controls.Add(Me.txtSection1)
        Me.GroupBox2.Controls.Add(Me.cmdUpload)
        Me.GroupBox2.Controls.Add(Me.txtFileName1)
        Me.GroupBox2.Location = New System.Drawing.Point(154, 190)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(563, 304)
        Me.GroupBox2.TabIndex = 42
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Receipt Data"
        '
        'lblPrintMsg
        '
        Me.lblPrintMsg.AutoSize = True
        Me.lblPrintMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrintMsg.Location = New System.Drawing.Point(244, 186)
        Me.lblPrintMsg.Name = "lblPrintMsg"
        Me.lblPrintMsg.Size = New System.Drawing.Size(85, 16)
        Me.lblPrintMsg.TabIndex = 39
        Me.lblPrintMsg.Text = "lblPrintMsg"
        '
        'cmdSendImgToPrinter
        '
        Me.cmdSendImgToPrinter.Location = New System.Drawing.Point(20, 236)
        Me.cmdSendImgToPrinter.Name = "cmdSendImgToPrinter"
        Me.cmdSendImgToPrinter.Size = New System.Drawing.Size(215, 46)
        Me.cmdSendImgToPrinter.TabIndex = 30
        Me.cmdSendImgToPrinter.Text = "Send Image To Printer"
        Me.cmdSendImgToPrinter.UseVisualStyleBackColor = True
        '
        'cmdSendTextToPrinter
        '
        Me.cmdSendTextToPrinter.Location = New System.Drawing.Point(20, 185)
        Me.cmdSendTextToPrinter.Name = "cmdSendTextToPrinter"
        Me.cmdSendTextToPrinter.Size = New System.Drawing.Size(215, 46)
        Me.cmdSendTextToPrinter.TabIndex = 29
        Me.cmdSendTextToPrinter.Text = "Send Text To Printer"
        Me.cmdSendTextToPrinter.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Location = New System.Drawing.Point(241, 212)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(304, 74)
        Me.cmdPrint.TabIndex = 28
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'txtSection1
        '
        Me.txtSection1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSection1.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection1.Location = New System.Drawing.Point(15, 25)
        Me.txtSection1.Multiline = True
        Me.txtSection1.Name = "txtSection1"
        Me.txtSection1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSection1.Size = New System.Drawing.Size(530, 101)
        Me.txtSection1.TabIndex = 22
        '
        'cmdUpload
        '
        Me.cmdUpload.Location = New System.Drawing.Point(392, 132)
        Me.cmdUpload.Name = "cmdUpload"
        Me.cmdUpload.Size = New System.Drawing.Size(153, 38)
        Me.cmdUpload.TabIndex = 21
        Me.cmdUpload.Text = "Browse Images"
        Me.cmdUpload.UseVisualStyleBackColor = True
        '
        'txtFileName1
        '
        Me.txtFileName1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFileName1.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileName1.Location = New System.Drawing.Point(15, 132)
        Me.txtFileName1.Name = "txtFileName1"
        Me.txtFileName1.Size = New System.Drawing.Size(371, 20)
        Me.txtFileName1.TabIndex = 20
        '
        'cmdPrinterStatus
        '
        Me.cmdPrinterStatus.Location = New System.Drawing.Point(533, 110)
        Me.cmdPrinterStatus.Name = "cmdPrinterStatus"
        Me.cmdPrinterStatus.Size = New System.Drawing.Size(184, 63)
        Me.cmdPrinterStatus.TabIndex = 41
        Me.cmdPrinterStatus.Text = "Printer Status"
        Me.cmdPrinterStatus.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(340, 110)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(181, 63)
        Me.cmdClose.TabIndex = 40
        Me.cmdClose.Text = "StopDevice"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdInit
        '
        Me.cmdInit.Location = New System.Drawing.Point(154, 110)
        Me.cmdInit.Name = "cmdInit"
        Me.cmdInit.Size = New System.Drawing.Size(175, 63)
        Me.cmdInit.TabIndex = 39
        Me.cmdInit.Text = "StartDevice"
        Me.cmdInit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(38, 124)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Printer Command :"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(533, 28)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 61)
        Me.btnExit.TabIndex = 33
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnPrinterSetting
        '
        Me.btnPrinterSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrinterSetting.Location = New System.Drawing.Point(154, 25)
        Me.btnPrinterSetting.Name = "btnPrinterSetting"
        Me.btnPrinterSetting.Size = New System.Drawing.Size(194, 64)
        Me.btnPrinterSetting.TabIndex = 32
        Me.btnPrinterSetting.Text = "Printer Setting"
        Me.btnPrinterSetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Printer Configuration :"
        '
        'frmMaintenanceMode
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
        Me.Name = "frmMaintenanceMode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDS:Printer"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnPrinterSetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdPrinterStatus As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdInit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdUpload As System.Windows.Forms.Button
    Friend WithEvents txtFileName1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSection1 As System.Windows.Forms.TextBox
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents cmdSendImgToPrinter As System.Windows.Forms.Button
    Friend WithEvents cmdSendTextToPrinter As System.Windows.Forms.Button
    Friend WithEvents lblPrintMsg As System.Windows.Forms.Label
    Friend WithEvents lblPrinterStatus As System.Windows.Forms.Label
    Friend WithEvents btnPrinterFailed As System.Windows.Forms.Button
    Friend WithEvents btnPaperJam As System.Windows.Forms.Button
    Friend WithEvents btnPaperEnd As System.Windows.Forms.Button
    Friend WithEvents btnPaperLow As System.Windows.Forms.Button
    Friend WithEvents btnPaperOK As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
