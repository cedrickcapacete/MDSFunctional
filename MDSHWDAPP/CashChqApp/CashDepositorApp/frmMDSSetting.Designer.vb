<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMDSSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMDSSetting))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbChqForeOpt = New System.Windows.Forms.ComboBox()
        Me.rbChqTemplatePath = New System.Windows.Forms.RadioButton()
        Me.rbChqImgPath = New System.Windows.Forms.RadioButton()
        Me.rbTraceFilePath = New System.Windows.Forms.RadioButton()
        Me.txtChqTemplatePath = New System.Windows.Forms.TextBox()
        Me.txtChqImagePath = New System.Windows.Forms.TextBox()
        Me.nmTakeReturnTM = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.nmRepositionItemTM = New System.Windows.Forms.NumericUpDown()
        Me.nmCleanFeederTM = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nmInsertItemTM = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NmComport = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSelectPath = New System.Windows.Forms.Button()
        Me.txtXFSLogPath = New System.Windows.Forms.TextBox()
        Me.rbDisable = New System.Windows.Forms.RadioButton()
        Me.rbEnable = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.FolderDlg = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmTakeReturnTM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmRepositionItemTM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmCleanFeederTM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmInsertItemTM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NmComport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbChqForeOpt)
        Me.GroupBox1.Controls.Add(Me.rbChqTemplatePath)
        Me.GroupBox1.Controls.Add(Me.rbChqImgPath)
        Me.GroupBox1.Controls.Add(Me.rbTraceFilePath)
        Me.GroupBox1.Controls.Add(Me.txtChqTemplatePath)
        Me.GroupBox1.Controls.Add(Me.txtChqImagePath)
        Me.GroupBox1.Controls.Add(Me.nmTakeReturnTM)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.nmRepositionItemTM)
        Me.GroupBox1.Controls.Add(Me.nmCleanFeederTM)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.nmInsertItemTM)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.NmComport)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnSelectPath)
        Me.GroupBox1.Controls.Add(Me.txtXFSLogPath)
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
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Maintenance: MDS Setting"
        '
        'cbChqForeOpt
        '
        Me.cbChqForeOpt.FormattingEnabled = True
        Me.cbChqForeOpt.Items.AddRange(New Object() {"FALSE", "TRUE"})
        Me.cbChqForeOpt.Location = New System.Drawing.Point(169, 124)
        Me.cbChqForeOpt.Name = "cbChqForeOpt"
        Me.cbChqForeOpt.Size = New System.Drawing.Size(166, 32)
        Me.cbChqForeOpt.TabIndex = 67
        '
        'rbChqTemplatePath
        '
        Me.rbChqTemplatePath.AutoSize = True
        Me.rbChqTemplatePath.Location = New System.Drawing.Point(6, 350)
        Me.rbChqTemplatePath.Name = "rbChqTemplatePath"
        Me.rbChqTemplatePath.Size = New System.Drawing.Size(212, 28)
        Me.rbChqTemplatePath.TabIndex = 66
        Me.rbChqTemplatePath.TabStop = True
        Me.rbChqTemplatePath.Text = "Chq Template Path:"
        Me.rbChqTemplatePath.UseVisualStyleBackColor = True
        '
        'rbChqImgPath
        '
        Me.rbChqImgPath.AutoSize = True
        Me.rbChqImgPath.Location = New System.Drawing.Point(26, 298)
        Me.rbChqImgPath.Name = "rbChqImgPath"
        Me.rbChqImgPath.Size = New System.Drawing.Size(182, 28)
        Me.rbChqImgPath.TabIndex = 65
        Me.rbChqImgPath.TabStop = True
        Me.rbChqImgPath.Text = "Chq Image Path:"
        Me.rbChqImgPath.UseVisualStyleBackColor = True
        '
        'rbTraceFilePath
        '
        Me.rbTraceFilePath.AutoSize = True
        Me.rbTraceFilePath.Location = New System.Drawing.Point(45, 251)
        Me.rbTraceFilePath.Name = "rbTraceFilePath"
        Me.rbTraceFilePath.Size = New System.Drawing.Size(163, 28)
        Me.rbTraceFilePath.TabIndex = 64
        Me.rbTraceFilePath.TabStop = True
        Me.rbTraceFilePath.Text = "XFS Log Path:"
        Me.rbTraceFilePath.UseVisualStyleBackColor = True
        '
        'txtChqTemplatePath
        '
        Me.txtChqTemplatePath.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtChqTemplatePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqTemplatePath.Location = New System.Drawing.Point(218, 345)
        Me.txtChqTemplatePath.Multiline = True
        Me.txtChqTemplatePath.Name = "txtChqTemplatePath"
        Me.txtChqTemplatePath.ReadOnly = True
        Me.txtChqTemplatePath.Size = New System.Drawing.Size(518, 41)
        Me.txtChqTemplatePath.TabIndex = 62
        '
        'txtChqImagePath
        '
        Me.txtChqImagePath.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtChqImagePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqImagePath.Location = New System.Drawing.Point(218, 298)
        Me.txtChqImagePath.Multiline = True
        Me.txtChqImagePath.Name = "txtChqImagePath"
        Me.txtChqImagePath.ReadOnly = True
        Me.txtChqImagePath.Size = New System.Drawing.Size(518, 41)
        Me.txtChqImagePath.TabIndex = 61
        '
        'nmTakeReturnTM
        '
        Me.nmTakeReturnTM.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmTakeReturnTM.Location = New System.Drawing.Point(559, 203)
        Me.nmTakeReturnTM.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmTakeReturnTM.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmTakeReturnTM.Name = "nmTakeReturnTM"
        Me.nmTakeReturnTM.Size = New System.Drawing.Size(118, 44)
        Me.nmTakeReturnTM.TabIndex = 60
        Me.nmTakeReturnTM.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(379, 215)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(175, 24)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Take Return Item:"
        '
        'nmRepositionItemTM
        '
        Me.nmRepositionItemTM.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmRepositionItemTM.Location = New System.Drawing.Point(559, 150)
        Me.nmRepositionItemTM.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmRepositionItemTM.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmRepositionItemTM.Name = "nmRepositionItemTM"
        Me.nmRepositionItemTM.Size = New System.Drawing.Size(118, 44)
        Me.nmRepositionItemTM.TabIndex = 58
        Me.nmRepositionItemTM.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmCleanFeederTM
        '
        Me.nmCleanFeederTM.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmCleanFeederTM.Location = New System.Drawing.Point(559, 96)
        Me.nmCleanFeederTM.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmCleanFeederTM.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmCleanFeederTM.Name = "nmCleanFeederTM"
        Me.nmCleanFeederTM.Size = New System.Drawing.Size(118, 44)
        Me.nmCleanFeederTM.TabIndex = 57
        Me.nmCleanFeederTM.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(396, 162)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(160, 24)
        Me.Label7.TabIndex = 56
        Me.Label7.Text = "Reposition Item:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(410, 105)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 24)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Clean Feeder:"
        '
        'nmInsertItemTM
        '
        Me.nmInsertItemTM.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmInsertItemTM.Location = New System.Drawing.Point(559, 45)
        Me.nmInsertItemTM.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmInsertItemTM.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmInsertItemTM.Name = "nmInsertItemTM"
        Me.nmInsertItemTM.Size = New System.Drawing.Size(118, 44)
        Me.nmInsertItemTM.TabIndex = 54
        Me.nmInsertItemTM.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(442, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 24)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "Insert Item:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(442, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 24)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "Timeout (sec)"
        '
        'NmComport
        '
        Me.NmComport.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NmComport.Location = New System.Drawing.Point(169, 62)
        Me.NmComport.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NmComport.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NmComport.Name = "NmComport"
        Me.NmComport.Size = New System.Drawing.Size(196, 44)
        Me.NmComport.TabIndex = 51
        Me.NmComport.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(63, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 24)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Comport:"
        '
        'btnSelectPath
        '
        Me.btnSelectPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectPath.Location = New System.Drawing.Point(36, 415)
        Me.btnSelectPath.Name = "btnSelectPath"
        Me.btnSelectPath.Size = New System.Drawing.Size(209, 72)
        Me.btnSelectPath.TabIndex = 49
        Me.btnSelectPath.Text = "Select Folder"
        Me.btnSelectPath.UseVisualStyleBackColor = True
        '
        'txtXFSLogPath
        '
        Me.txtXFSLogPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtXFSLogPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXFSLogPath.Location = New System.Drawing.Point(218, 251)
        Me.txtXFSLogPath.Multiline = True
        Me.txtXFSLogPath.Name = "txtXFSLogPath"
        Me.txtXFSLogPath.ReadOnly = True
        Me.txtXFSLogPath.Size = New System.Drawing.Size(518, 41)
        Me.txtXFSLogPath.TabIndex = 48
        '
        'rbDisable
        '
        Me.rbDisable.AutoSize = True
        Me.rbDisable.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDisable.Location = New System.Drawing.Point(287, 18)
        Me.rbDisable.Name = "rbDisable"
        Me.rbDisable.Size = New System.Drawing.Size(138, 37)
        Me.rbDisable.TabIndex = 47
        Me.rbDisable.TabStop = True
        Me.rbDisable.Text = "Disable"
        Me.rbDisable.UseVisualStyleBackColor = True
        '
        'rbEnable
        '
        Me.rbEnable.AutoSize = True
        Me.rbEnable.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbEnable.Location = New System.Drawing.Point(152, 18)
        Me.rbEnable.Name = "rbEnable"
        Me.rbEnable.Size = New System.Drawing.Size(129, 37)
        Me.rbEnable.TabIndex = 46
        Me.rbEnable.TabStop = True
        Me.rbEnable.Text = "Enable"
        Me.rbEnable.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 24)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Chq-DropBox:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 24)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "MDS Device:"
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirm.Location = New System.Drawing.Point(363, 415)
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
        Me.btnExit.Location = New System.Drawing.Point(553, 415)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(184, 72)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmMDSSetting
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
        Me.Name = "frmMDSSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Maintenance:MDS Setting"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmTakeReturnTM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmRepositionItemTM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmCleanFeederTM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmInsertItemTM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NmComport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cbChqForeOpt As System.Windows.Forms.ComboBox
    Friend WithEvents rbChqTemplatePath As System.Windows.Forms.RadioButton
    Friend WithEvents rbChqImgPath As System.Windows.Forms.RadioButton
    Friend WithEvents rbTraceFilePath As System.Windows.Forms.RadioButton
    Friend WithEvents txtChqTemplatePath As System.Windows.Forms.TextBox
    Friend WithEvents txtChqImagePath As System.Windows.Forms.TextBox
    Friend WithEvents nmTakeReturnTM As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents nmRepositionItemTM As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmCleanFeederTM As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nmInsertItemTM As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NmComport As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSelectPath As System.Windows.Forms.Button
    Friend WithEvents txtXFSLogPath As System.Windows.Forms.TextBox
    Friend WithEvents rbDisable As System.Windows.Forms.RadioButton
    Friend WithEvents rbEnable As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FolderDlg As System.Windows.Forms.FolderBrowserDialog
End Class
