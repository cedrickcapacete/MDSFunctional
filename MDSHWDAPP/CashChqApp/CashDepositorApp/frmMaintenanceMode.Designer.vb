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
        Me.cmdMDSExchange = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmdCollectionBoxCounter = New System.Windows.Forms.Button()
        Me.btnViewChqImages = New System.Windows.Forms.Button()
        Me.gbSummary = New System.Windows.Forms.GroupBox()
        Me.lblTtlChq = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstView = New System.Windows.Forms.ListView()
        Me.lblMDSEvent = New System.Windows.Forms.Label()
        Me.cmdStartDevice = New System.Windows.Forms.Button()
        Me.cmdMDSStatus = New System.Windows.Forms.Button()
        Me.cmdDeposit = New System.Windows.Forms.Button()
        Me.cmdCleanPath = New System.Windows.Forms.Button()
        Me.cmdStopDevice = New System.Windows.Forms.Button()
        Me.cmdUserTimeout = New System.Windows.Forms.Button()
        Me.cmdReject = New System.Windows.Forms.Button()
        Me.btnClearText = New System.Windows.Forms.Button()
        Me.cmdOpenFeeder = New System.Windows.Forms.Button()
        Me.cmdInsertMore = New System.Windows.Forms.Button()
        Me.cmdCloseFeeder = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblUserStatus = New System.Windows.Forms.Label()
        Me.lblMDSStatus = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdMDSLight = New System.Windows.Forms.Button()
        Me.lblDoorStatus = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rbCHQ = New System.Windows.Forms.RadioButton()
        Me.rbCASH = New System.Windows.Forms.RadioButton()
        Me.txtDeviceProperty = New System.Windows.Forms.RichTextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnMDSSetting = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.rbCASHNCHQ = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.gbSummary.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbCASHNCHQ)
        Me.GroupBox1.Controls.Add(Me.cmdMDSExchange)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.cmdMDSLight)
        Me.GroupBox1.Controls.Add(Me.lblDoorStatus)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.rbCHQ)
        Me.GroupBox1.Controls.Add(Me.rbCASH)
        Me.GroupBox1.Controls.Add(Me.txtDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnMDSSetting)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(748, 528)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MDS Devices: MDS"
        '
        'cmdMDSExchange
        '
        Me.cmdMDSExchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMDSExchange.Location = New System.Drawing.Point(430, 22)
        Me.cmdMDSExchange.Name = "cmdMDSExchange"
        Me.cmdMDSExchange.Size = New System.Drawing.Size(141, 46)
        Me.cmdMDSExchange.TabIndex = 65
        Me.cmdMDSExchange.Text = "MDS Exchange"
        Me.cmdMDSExchange.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmdCollectionBoxCounter)
        Me.GroupBox3.Controls.Add(Me.btnViewChqImages)
        Me.GroupBox3.Controls.Add(Me.gbSummary)
        Me.GroupBox3.Controls.Add(Me.lblMDSEvent)
        Me.GroupBox3.Controls.Add(Me.cmdStartDevice)
        Me.GroupBox3.Controls.Add(Me.cmdMDSStatus)
        Me.GroupBox3.Controls.Add(Me.cmdDeposit)
        Me.GroupBox3.Controls.Add(Me.cmdCleanPath)
        Me.GroupBox3.Controls.Add(Me.cmdStopDevice)
        Me.GroupBox3.Controls.Add(Me.cmdUserTimeout)
        Me.GroupBox3.Controls.Add(Me.cmdReject)
        Me.GroupBox3.Controls.Add(Me.btnClearText)
        Me.GroupBox3.Controls.Add(Me.cmdOpenFeeder)
        Me.GroupBox3.Controls.Add(Me.cmdInsertMore)
        Me.GroupBox3.Controls.Add(Me.cmdCloseFeeder)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(6, 181)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(712, 264)
        Me.GroupBox3.TabIndex = 64
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "User Interaction"
        '
        'cmdCollectionBoxCounter
        '
        Me.cmdCollectionBoxCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCollectionBoxCounter.Location = New System.Drawing.Point(14, 214)
        Me.cmdCollectionBoxCounter.Name = "cmdCollectionBoxCounter"
        Me.cmdCollectionBoxCounter.Size = New System.Drawing.Size(257, 44)
        Me.cmdCollectionBoxCounter.TabIndex = 64
        Me.cmdCollectionBoxCounter.Text = "Get Collection Box Counter"
        Me.cmdCollectionBoxCounter.UseVisualStyleBackColor = True
        '
        'btnViewChqImages
        '
        Me.btnViewChqImages.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewChqImages.Location = New System.Drawing.Point(565, 157)
        Me.btnViewChqImages.Name = "btnViewChqImages"
        Me.btnViewChqImages.Size = New System.Drawing.Size(139, 52)
        Me.btnViewChqImages.TabIndex = 63
        Me.btnViewChqImages.Text = "View CHQ Images"
        Me.btnViewChqImages.UseVisualStyleBackColor = True
        '
        'gbSummary
        '
        Me.gbSummary.Controls.Add(Me.lblTtlChq)
        Me.gbSummary.Controls.Add(Me.Label1)
        Me.gbSummary.Controls.Add(Me.lstView)
        Me.gbSummary.Location = New System.Drawing.Point(277, 57)
        Me.gbSummary.Name = "gbSummary"
        Me.gbSummary.Size = New System.Drawing.Size(282, 197)
        Me.gbSummary.TabIndex = 62
        Me.gbSummary.TabStop = False
        Me.gbSummary.Text = "Summary"
        '
        'lblTtlChq
        '
        Me.lblTtlChq.AutoSize = True
        Me.lblTtlChq.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTtlChq.Location = New System.Drawing.Point(113, 21)
        Me.lblTtlChq.Name = "lblTtlChq"
        Me.lblTtlChq.Size = New System.Drawing.Size(93, 24)
        Me.lblTtlChq.TabIndex = 64
        Me.lblTtlChq.Text = "lblTtlChq"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 24)
        Me.Label1.TabIndex = 63
        Me.Label1.Text = "Total CHQ:"
        '
        'lstView
        '
        Me.lstView.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lstView.Location = New System.Drawing.Point(6, 53)
        Me.lstView.Name = "lstView"
        Me.lstView.Size = New System.Drawing.Size(270, 138)
        Me.lstView.TabIndex = 0
        Me.lstView.UseCompatibleStateImageBehavior = False
        '
        'lblMDSEvent
        '
        Me.lblMDSEvent.AutoSize = True
        Me.lblMDSEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDSEvent.Location = New System.Drawing.Point(11, 27)
        Me.lblMDSEvent.Name = "lblMDSEvent"
        Me.lblMDSEvent.Size = New System.Drawing.Size(129, 24)
        Me.lblMDSEvent.TabIndex = 39
        Me.lblMDSEvent.Text = "lblMDSEvent"
        '
        'cmdStartDevice
        '
        Me.cmdStartDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStartDevice.Location = New System.Drawing.Point(565, 14)
        Me.cmdStartDevice.Name = "cmdStartDevice"
        Me.cmdStartDevice.Size = New System.Drawing.Size(141, 37)
        Me.cmdStartDevice.TabIndex = 39
        Me.cmdStartDevice.Text = "Initialise"
        Me.cmdStartDevice.UseVisualStyleBackColor = True
        '
        'cmdMDSStatus
        '
        Me.cmdMDSStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMDSStatus.Location = New System.Drawing.Point(565, 107)
        Me.cmdMDSStatus.Name = "cmdMDSStatus"
        Me.cmdMDSStatus.Size = New System.Drawing.Size(139, 44)
        Me.cmdMDSStatus.TabIndex = 53
        Me.cmdMDSStatus.Text = "MDS Status"
        Me.cmdMDSStatus.UseVisualStyleBackColor = True
        '
        'cmdDeposit
        '
        Me.cmdDeposit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeposit.Location = New System.Drawing.Point(147, 159)
        Me.cmdDeposit.Name = "cmdDeposit"
        Me.cmdDeposit.Size = New System.Drawing.Size(125, 47)
        Me.cmdDeposit.TabIndex = 59
        Me.cmdDeposit.Text = "Deposit"
        Me.cmdDeposit.UseVisualStyleBackColor = True
        '
        'cmdCleanPath
        '
        Me.cmdCleanPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCleanPath.Location = New System.Drawing.Point(565, 55)
        Me.cmdCleanPath.Name = "cmdCleanPath"
        Me.cmdCleanPath.Size = New System.Drawing.Size(139, 46)
        Me.cmdCleanPath.TabIndex = 61
        Me.cmdCleanPath.Text = "Clean Path"
        Me.cmdCleanPath.UseVisualStyleBackColor = True
        '
        'cmdStopDevice
        '
        Me.cmdStopDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStopDevice.Location = New System.Drawing.Point(442, 16)
        Me.cmdStopDevice.Name = "cmdStopDevice"
        Me.cmdStopDevice.Size = New System.Drawing.Size(117, 32)
        Me.cmdStopDevice.TabIndex = 52
        Me.cmdStopDevice.Text = "StopDevice"
        Me.cmdStopDevice.UseVisualStyleBackColor = True
        Me.cmdStopDevice.Visible = False
        '
        'cmdUserTimeout
        '
        Me.cmdUserTimeout.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUserTimeout.Location = New System.Drawing.Point(146, 107)
        Me.cmdUserTimeout.Name = "cmdUserTimeout"
        Me.cmdUserTimeout.Size = New System.Drawing.Size(126, 45)
        Me.cmdUserTimeout.TabIndex = 60
        Me.cmdUserTimeout.Text = "User Timeout"
        Me.cmdUserTimeout.UseVisualStyleBackColor = True
        '
        'cmdReject
        '
        Me.cmdReject.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReject.Location = New System.Drawing.Point(15, 162)
        Me.cmdReject.Name = "cmdReject"
        Me.cmdReject.Size = New System.Drawing.Size(125, 47)
        Me.cmdReject.TabIndex = 58
        Me.cmdReject.Text = "Reject"
        Me.cmdReject.UseVisualStyleBackColor = True
        '
        'btnClearText
        '
        Me.btnClearText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearText.Location = New System.Drawing.Point(565, 212)
        Me.btnClearText.Name = "btnClearText"
        Me.btnClearText.Size = New System.Drawing.Size(141, 36)
        Me.btnClearText.TabIndex = 48
        Me.btnClearText.Text = "Clear Text"
        Me.btnClearText.UseVisualStyleBackColor = True
        '
        'cmdOpenFeeder
        '
        Me.cmdOpenFeeder.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOpenFeeder.Location = New System.Drawing.Point(14, 57)
        Me.cmdOpenFeeder.Name = "cmdOpenFeeder"
        Me.cmdOpenFeeder.Size = New System.Drawing.Size(126, 45)
        Me.cmdOpenFeeder.TabIndex = 56
        Me.cmdOpenFeeder.Text = "Open Feeder"
        Me.cmdOpenFeeder.UseVisualStyleBackColor = True
        '
        'cmdInsertMore
        '
        Me.cmdInsertMore.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInsertMore.Location = New System.Drawing.Point(15, 110)
        Me.cmdInsertMore.Name = "cmdInsertMore"
        Me.cmdInsertMore.Size = New System.Drawing.Size(125, 44)
        Me.cmdInsertMore.TabIndex = 57
        Me.cmdInsertMore.Text = "Insert More"
        Me.cmdInsertMore.UseVisualStyleBackColor = True
        '
        'cmdCloseFeeder
        '
        Me.cmdCloseFeeder.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCloseFeeder.Location = New System.Drawing.Point(146, 56)
        Me.cmdCloseFeeder.Name = "cmdCloseFeeder"
        Me.cmdCloseFeeder.Size = New System.Drawing.Size(125, 45)
        Me.cmdCloseFeeder.TabIndex = 55
        Me.cmdCloseFeeder.Text = "Close Feeder"
        Me.cmdCloseFeeder.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblUserStatus)
        Me.GroupBox2.Controls.Add(Me.lblMDSStatus)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(6, 97)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(712, 78)
        Me.GroupBox2.TabIndex = 63
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Status && Actions"
        '
        'lblUserStatus
        '
        Me.lblUserStatus.AutoSize = True
        Me.lblUserStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserStatus.Location = New System.Drawing.Point(99, 57)
        Me.lblUserStatus.Name = "lblUserStatus"
        Me.lblUserStatus.Size = New System.Drawing.Size(82, 13)
        Me.lblUserStatus.TabIndex = 42
        Me.lblUserStatus.Text = "lblUserStatus"
        '
        'lblMDSStatus
        '
        Me.lblMDSStatus.AutoSize = True
        Me.lblMDSStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDSStatus.Location = New System.Drawing.Point(99, 27)
        Me.lblMDSStatus.Name = "lblMDSStatus"
        Me.lblMDSStatus.Size = New System.Drawing.Size(83, 13)
        Me.lblMDSStatus.TabIndex = 41
        Me.lblMDSStatus.Text = "lblMDSStatus"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "User Status :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "MDS Status :"
        '
        'cmdMDSLight
        '
        Me.cmdMDSLight.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMDSLight.Location = New System.Drawing.Point(283, 21)
        Me.cmdMDSLight.Name = "cmdMDSLight"
        Me.cmdMDSLight.Size = New System.Drawing.Size(141, 46)
        Me.cmdMDSLight.TabIndex = 54
        Me.cmdMDSLight.Text = "MDS Light"
        Me.cmdMDSLight.UseVisualStyleBackColor = True
        '
        'lblDoorStatus
        '
        Me.lblDoorStatus.AutoSize = True
        Me.lblDoorStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoorStatus.Location = New System.Drawing.Point(572, 78)
        Me.lblDoorStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDoorStatus.Name = "lblDoorStatus"
        Me.lblDoorStatus.Size = New System.Drawing.Size(102, 16)
        Me.lblDoorStatus.TabIndex = 62
        Me.lblDoorStatus.Text = "lblDoorStatus"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(57, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "MDS Mode :"
        '
        'rbCHQ
        '
        Me.rbCHQ.AutoSize = True
        Me.rbCHQ.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCHQ.Location = New System.Drawing.Point(234, 71)
        Me.rbCHQ.Name = "rbCHQ"
        Me.rbCHQ.Size = New System.Drawing.Size(115, 28)
        Me.rbCHQ.TabIndex = 50
        Me.rbCHQ.TabStop = True
        Me.rbCHQ.Text = "CHEQUE"
        Me.rbCHQ.UseVisualStyleBackColor = True
        '
        'rbCASH
        '
        Me.rbCASH.AutoSize = True
        Me.rbCASH.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCASH.Location = New System.Drawing.Point(144, 71)
        Me.rbCASH.Name = "rbCASH"
        Me.rbCASH.Size = New System.Drawing.Size(84, 28)
        Me.rbCASH.TabIndex = 49
        Me.rbCASH.TabStop = True
        Me.rbCASH.Text = "CASH"
        Me.rbCASH.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(6, 451)
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ReadOnly = True
        Me.txtDeviceProperty.Size = New System.Drawing.Size(709, 71)
        Me.txtDeviceProperty.TabIndex = 47
        Me.txtDeviceProperty.Text = ""
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Location = New System.Drawing.Point(575, 24)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(147, 46)
        Me.btnExit.TabIndex = 33
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnMDSSetting
        '
        Me.btnMDSSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMDSSetting.Location = New System.Drawing.Point(144, 19)
        Me.btnMDSSetting.Name = "btnMDSSetting"
        Me.btnMDSSetting.Size = New System.Drawing.Size(133, 46)
        Me.btnMDSSetting.TabIndex = 32
        Me.btnMDSSetting.Text = "MDS Setting"
        Me.btnMDSSetting.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "MDS Configuration :"
        '
        'rbCASHNCHQ
        '
        Me.rbCASHNCHQ.AutoSize = True
        Me.rbCASHNCHQ.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCASHNCHQ.Location = New System.Drawing.Point(355, 71)
        Me.rbCASHNCHQ.Name = "rbCASHNCHQ"
        Me.rbCASHNCHQ.Size = New System.Drawing.Size(155, 28)
        Me.rbCASHNCHQ.TabIndex = 66
        Me.rbCASHNCHQ.TabStop = True
        Me.rbCASHNCHQ.Text = "CASH && CHQ"
        Me.rbCASHNCHQ.UseVisualStyleBackColor = True
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
        Me.Text = "MDS:MDS"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.gbSummary.ResumeLayout(False)
        Me.gbSummary.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearText As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdStartDevice As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnMDSSetting As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rbCHQ As System.Windows.Forms.RadioButton
    Friend WithEvents rbCASH As System.Windows.Forms.RadioButton
    Friend WithEvents cmdStopDevice As System.Windows.Forms.Button
    Friend WithEvents cmdUserTimeout As System.Windows.Forms.Button
    Friend WithEvents cmdDeposit As System.Windows.Forms.Button
    Friend WithEvents cmdReject As System.Windows.Forms.Button
    Friend WithEvents cmdInsertMore As System.Windows.Forms.Button
    Friend WithEvents cmdOpenFeeder As System.Windows.Forms.Button
    Friend WithEvents cmdCloseFeeder As System.Windows.Forms.Button
    Friend WithEvents cmdMDSLight As System.Windows.Forms.Button
    Friend WithEvents cmdMDSStatus As System.Windows.Forms.Button
    Friend WithEvents cmdCleanPath As System.Windows.Forms.Button
    Friend WithEvents lblDoorStatus As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblMDSEvent As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblUserStatus As System.Windows.Forms.Label
    Friend WithEvents lblMDSStatus As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdMDSExchange As System.Windows.Forms.Button
    Friend WithEvents gbSummary As System.Windows.Forms.GroupBox
    Friend WithEvents lstView As System.Windows.Forms.ListView
    Friend WithEvents lblTtlChq As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnViewChqImages As System.Windows.Forms.Button
    Friend WithEvents cmdCollectionBoxCounter As System.Windows.Forms.Button
    Friend WithEvents rbCASHNCHQ As System.Windows.Forms.RadioButton
End Class
