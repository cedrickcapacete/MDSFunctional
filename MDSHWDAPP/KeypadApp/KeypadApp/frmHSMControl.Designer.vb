<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHSMControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHSMControl))
        Me.cmdClearText = New System.Windows.Forms.Button()
        Me.cmdDiagnosticDevice = New System.Windows.Forms.Button()
        Me.cmdGetDeviceProperty = New System.Windows.Forms.Button()
        Me.txtDeviceProperty = New System.Windows.Forms.TextBox()
        Me.cmdWrapDevice = New System.Windows.Forms.Button()
        Me.cmdLockDevice = New System.Windows.Forms.Button()
        Me.cmdUnlockDevice = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rbnGenMac = New System.Windows.Forms.RadioButton()
        Me.btnGenerateMAC = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDataToMac = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnVerifyMac = New System.Windows.Forms.Button()
        Me.txtMacedData = New System.Windows.Forms.TextBox()
        Me.rbnVerifyMac = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rbnNoParam = New System.Windows.Forms.RadioButton()
        Me.rbnPAD = New System.Windows.Forms.RadioButton()
        Me.rbnPAN = New System.Windows.Forms.RadioButton()
        Me.ddlFormatType = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPAN = New System.Windows.Forms.TextBox()
        Me.txtPinBlock = New System.Windows.Forms.TextBox()
        Me.txtPAD = New System.Windows.Forms.TextBox()
        Me.cmdGetPinBlock = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtCheckSum = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmdUpdateWorkingKey = New System.Windows.Forms.Button()
        Me.txtKeyIndex = New System.Windows.Forms.TextBox()
        Me.txtWorkingKey = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtEncKeyIndex = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdWakeUpDevice = New System.Windows.Forms.Button()
        Me.cmdInit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdClearText
        '
        Me.cmdClearText.Location = New System.Drawing.Point(583, 402)
        Me.cmdClearText.Name = "cmdClearText"
        Me.cmdClearText.Size = New System.Drawing.Size(122, 23)
        Me.cmdClearText.TabIndex = 11
        Me.cmdClearText.Text = "Clear Text"
        Me.cmdClearText.UseVisualStyleBackColor = True
        '
        'cmdDiagnosticDevice
        '
        Me.cmdDiagnosticDevice.Location = New System.Drawing.Point(559, 25)
        Me.cmdDiagnosticDevice.Name = "cmdDiagnosticDevice"
        Me.cmdDiagnosticDevice.Size = New System.Drawing.Size(146, 39)
        Me.cmdDiagnosticDevice.TabIndex = 10
        Me.cmdDiagnosticDevice.Text = "DiagnosticDevice"
        Me.cmdDiagnosticDevice.UseVisualStyleBackColor = True
        '
        'cmdGetDeviceProperty
        '
        Me.cmdGetDeviceProperty.Location = New System.Drawing.Point(24, 402)
        Me.cmdGetDeviceProperty.Name = "cmdGetDeviceProperty"
        Me.cmdGetDeviceProperty.Size = New System.Drawing.Size(165, 26)
        Me.cmdGetDeviceProperty.TabIndex = 8
        Me.cmdGetDeviceProperty.Text = "Get Device Property"
        Me.cmdGetDeviceProperty.UseVisualStyleBackColor = True
        '
        'txtDeviceProperty
        '
        Me.txtDeviceProperty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeviceProperty.Location = New System.Drawing.Point(24, 431)
        Me.txtDeviceProperty.Multiline = True
        Me.txtDeviceProperty.Name = "txtDeviceProperty"
        Me.txtDeviceProperty.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDeviceProperty.Size = New System.Drawing.Size(681, 113)
        Me.txtDeviceProperty.TabIndex = 6
        '
        'cmdWrapDevice
        '
        Me.cmdWrapDevice.Location = New System.Drawing.Point(280, 25)
        Me.cmdWrapDevice.Name = "cmdWrapDevice"
        Me.cmdWrapDevice.Size = New System.Drawing.Size(125, 39)
        Me.cmdWrapDevice.TabIndex = 7
        Me.cmdWrapDevice.Text = "Wrap Device"
        Me.cmdWrapDevice.UseVisualStyleBackColor = True
        '
        'cmdLockDevice
        '
        Me.cmdLockDevice.Location = New System.Drawing.Point(152, 72)
        Me.cmdLockDevice.Name = "cmdLockDevice"
        Me.cmdLockDevice.Size = New System.Drawing.Size(116, 39)
        Me.cmdLockDevice.TabIndex = 3
        Me.cmdLockDevice.Text = "LockDevice"
        Me.cmdLockDevice.UseVisualStyleBackColor = True
        '
        'cmdUnlockDevice
        '
        Me.cmdUnlockDevice.Location = New System.Drawing.Point(24, 71)
        Me.cmdUnlockDevice.Name = "cmdUnlockDevice"
        Me.cmdUnlockDevice.Size = New System.Drawing.Size(116, 39)
        Me.cmdUnlockDevice.TabIndex = 2
        Me.cmdUnlockDevice.Text = "UnlockDevice"
        Me.cmdUnlockDevice.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(151, 25)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(116, 39)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "StopDevice"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.cmdWakeUpDevice)
        Me.GroupBox1.Controls.Add(Me.cmdClearText)
        Me.GroupBox1.Controls.Add(Me.cmdDiagnosticDevice)
        Me.GroupBox1.Controls.Add(Me.cmdGetDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.txtDeviceProperty)
        Me.GroupBox1.Controls.Add(Me.cmdWrapDevice)
        Me.GroupBox1.Controls.Add(Me.cmdLockDevice)
        Me.GroupBox1.Controls.Add(Me.cmdUnlockDevice)
        Me.GroupBox1.Controls.Add(Me.cmdClose)
        Me.GroupBox1.Controls.Add(Me.cmdInit)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(32, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(729, 550)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "HSM Command"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbnGenMac)
        Me.GroupBox3.Controls.Add(Me.btnGenerateMAC)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.txtDataToMac)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.btnVerifyMac)
        Me.GroupBox3.Controls.Add(Me.txtMacedData)
        Me.GroupBox3.Controls.Add(Me.rbnVerifyMac)
        Me.GroupBox3.Location = New System.Drawing.Point(24, 210)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(681, 85)
        Me.GroupBox3.TabIndex = 41
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Generate and Verify MAC"
        '
        'rbnGenMac
        '
        Me.rbnGenMac.AutoSize = True
        Me.rbnGenMac.Location = New System.Drawing.Point(7, 21)
        Me.rbnGenMac.Name = "rbnGenMac"
        Me.rbnGenMac.Size = New System.Drawing.Size(97, 23)
        Me.rbnGenMac.TabIndex = 24
        Me.rbnGenMac.TabStop = True
        Me.rbnGenMac.Text = "Gen MAC"
        Me.rbnGenMac.UseVisualStyleBackColor = True
        '
        'btnGenerateMAC
        '
        Me.btnGenerateMAC.Location = New System.Drawing.Point(453, 16)
        Me.btnGenerateMAC.Name = "btnGenerateMAC"
        Me.btnGenerateMAC.Size = New System.Drawing.Size(100, 59)
        Me.btnGenerateMAC.TabIndex = 19
        Me.btnGenerateMAC.Text = "Gen MAC"
        Me.btnGenerateMAC.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(282, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 19)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "MACed Data"
        '
        'txtDataToMac
        '
        Me.txtDataToMac.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDataToMac.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDataToMac.Location = New System.Drawing.Point(137, 39)
        Me.txtDataToMac.Name = "txtDataToMac"
        Me.txtDataToMac.Size = New System.Drawing.Size(134, 32)
        Me.txtDataToMac.TabIndex = 20
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(133, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 19)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "MAC String"
        '
        'btnVerifyMac
        '
        Me.btnVerifyMac.Location = New System.Drawing.Point(562, 16)
        Me.btnVerifyMac.Name = "btnVerifyMac"
        Me.btnVerifyMac.Size = New System.Drawing.Size(108, 59)
        Me.btnVerifyMac.TabIndex = 21
        Me.btnVerifyMac.Text = "Verify MAC"
        Me.btnVerifyMac.UseVisualStyleBackColor = True
        '
        'txtMacedData
        '
        Me.txtMacedData.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMacedData.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMacedData.Location = New System.Drawing.Point(288, 39)
        Me.txtMacedData.Name = "txtMacedData"
        Me.txtMacedData.Size = New System.Drawing.Size(144, 32)
        Me.txtMacedData.TabIndex = 23
        '
        'rbnVerifyMac
        '
        Me.rbnVerifyMac.AutoSize = True
        Me.rbnVerifyMac.Location = New System.Drawing.Point(6, 52)
        Me.rbnVerifyMac.Name = "rbnVerifyMac"
        Me.rbnVerifyMac.Size = New System.Drawing.Size(110, 23)
        Me.rbnVerifyMac.TabIndex = 25
        Me.rbnVerifyMac.TabStop = True
        Me.rbnVerifyMac.Text = "Verify MAC"
        Me.rbnVerifyMac.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.rbnNoParam)
        Me.GroupBox4.Controls.Add(Me.rbnPAD)
        Me.GroupBox4.Controls.Add(Me.rbnPAN)
        Me.GroupBox4.Controls.Add(Me.ddlFormatType)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.txtPAN)
        Me.GroupBox4.Controls.Add(Me.txtPinBlock)
        Me.GroupBox4.Controls.Add(Me.txtPAD)
        Me.GroupBox4.Controls.Add(Me.cmdGetPinBlock)
        Me.GroupBox4.Location = New System.Drawing.Point(24, 297)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(681, 100)
        Me.GroupBox4.TabIndex = 41
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Generate PIN Block"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(542, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 19)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Encrypted"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(541, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 19)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "PIN Block"
        '
        'rbnNoParam
        '
        Me.rbnNoParam.AutoSize = True
        Me.rbnNoParam.Location = New System.Drawing.Point(186, 74)
        Me.rbnNoParam.Name = "rbnNoParam"
        Me.rbnNoParam.Size = New System.Drawing.Size(94, 23)
        Me.rbnNoParam.TabIndex = 44
        Me.rbnNoParam.TabStop = True
        Me.rbnNoParam.Text = "No Param"
        Me.rbnNoParam.UseVisualStyleBackColor = True
        '
        'rbnPAD
        '
        Me.rbnPAD.AutoSize = True
        Me.rbnPAD.Location = New System.Drawing.Point(185, 47)
        Me.rbnPAD.Name = "rbnPAD"
        Me.rbnPAD.Size = New System.Drawing.Size(100, 23)
        Me.rbnPAD.TabIndex = 43
        Me.rbnPAD.TabStop = True
        Me.rbnPAD.Text = "PAD Value"
        Me.rbnPAD.UseVisualStyleBackColor = True
        '
        'rbnPAN
        '
        Me.rbnPAN.AutoSize = True
        Me.rbnPAN.Location = New System.Drawing.Point(185, 16)
        Me.rbnPAN.Name = "rbnPAN"
        Me.rbnPAN.Size = New System.Drawing.Size(58, 23)
        Me.rbnPAN.TabIndex = 34
        Me.rbnPAN.TabStop = True
        Me.rbnPAN.Text = "PAN"
        Me.rbnPAN.UseVisualStyleBackColor = True
        '
        'ddlFormatType
        '
        Me.ddlFormatType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ddlFormatType.FormattingEnabled = True
        Me.ddlFormatType.Items.AddRange(New Object() {"1 - ISO Format 0", "2 - VISA Format 2", "3 - VISA Format 3", "4 - ISO Format 1", "5 - Diebold Standard", "6 - Diebold Enhanced"})
        Me.ddlFormatType.Location = New System.Drawing.Point(19, 54)
        Me.ddlFormatType.Name = "ddlFormatType"
        Me.ddlFormatType.Size = New System.Drawing.Size(146, 27)
        Me.ddlFormatType.TabIndex = 42
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 19)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Format Type"
        '
        'txtPAN
        '
        Me.txtPAN.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPAN.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPAN.Location = New System.Drawing.Point(301, 12)
        Me.txtPAN.Name = "txtPAN"
        Me.txtPAN.Size = New System.Drawing.Size(125, 32)
        Me.txtPAN.TabIndex = 17
        '
        'txtPinBlock
        '
        Me.txtPinBlock.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPinBlock.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinBlock.Location = New System.Drawing.Point(544, 60)
        Me.txtPinBlock.Name = "txtPinBlock"
        Me.txtPinBlock.Size = New System.Drawing.Size(131, 32)
        Me.txtPinBlock.TabIndex = 31
        '
        'txtPAD
        '
        Me.txtPAD.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPAD.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPAD.Location = New System.Drawing.Point(301, 45)
        Me.txtPAD.Name = "txtPAD"
        Me.txtPAD.Size = New System.Drawing.Size(125, 32)
        Me.txtPAD.TabIndex = 27
        '
        'cmdGetPinBlock
        '
        Me.cmdGetPinBlock.Location = New System.Drawing.Point(435, 15)
        Me.cmdGetPinBlock.Name = "cmdGetPinBlock"
        Me.cmdGetPinBlock.Size = New System.Drawing.Size(99, 79)
        Me.cmdGetPinBlock.TabIndex = 30
        Me.cmdGetPinBlock.Text = "PIN Block"
        Me.cmdGetPinBlock.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtCheckSum)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.cmdUpdateWorkingKey)
        Me.GroupBox2.Controls.Add(Me.txtKeyIndex)
        Me.GroupBox2.Controls.Add(Me.txtWorkingKey)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtEncKeyIndex)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(24, 114)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(681, 94)
        Me.GroupBox2.TabIndex = 40
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Register A Key B Key"
        '
        'txtCheckSum
        '
        Me.txtCheckSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCheckSum.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCheckSum.Location = New System.Drawing.Point(583, 51)
        Me.txtCheckSum.Name = "txtCheckSum"
        Me.txtCheckSum.Size = New System.Drawing.Size(83, 32)
        Me.txtCheckSum.TabIndex = 47
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(579, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 19)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Check Sum"
        '
        'cmdUpdateWorkingKey
        '
        Me.cmdUpdateWorkingKey.Location = New System.Drawing.Point(19, 25)
        Me.cmdUpdateWorkingKey.Name = "cmdUpdateWorkingKey"
        Me.cmdUpdateWorkingKey.Size = New System.Drawing.Size(165, 60)
        Me.cmdUpdateWorkingKey.TabIndex = 39
        Me.cmdUpdateWorkingKey.Text = "Update Working Key"
        Me.cmdUpdateWorkingKey.UseVisualStyleBackColor = True
        '
        'txtKeyIndex
        '
        Me.txtKeyIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKeyIndex.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKeyIndex.Location = New System.Drawing.Point(207, 53)
        Me.txtKeyIndex.Name = "txtKeyIndex"
        Me.txtKeyIndex.Size = New System.Drawing.Size(75, 32)
        Me.txtKeyIndex.TabIndex = 40
        '
        'txtWorkingKey
        '
        Me.txtWorkingKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWorkingKey.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkingKey.Location = New System.Drawing.Point(301, 53)
        Me.txtWorkingKey.Name = "txtWorkingKey"
        Me.txtWorkingKey.Size = New System.Drawing.Size(153, 32)
        Me.txtWorkingKey.TabIndex = 41
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(465, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 19)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Master Key"
        '
        'txtEncKeyIndex
        '
        Me.txtEncKeyIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEncKeyIndex.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEncKeyIndex.Location = New System.Drawing.Point(469, 53)
        Me.txtEncKeyIndex.Name = "txtEncKeyIndex"
        Me.txtEncKeyIndex.Size = New System.Drawing.Size(83, 32)
        Me.txtEncKeyIndex.TabIndex = 42
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(297, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 19)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Working Key"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(203, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 19)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Key Index"
        '
        'cmdWakeUpDevice
        '
        Me.cmdWakeUpDevice.Location = New System.Drawing.Point(416, 25)
        Me.cmdWakeUpDevice.Name = "cmdWakeUpDevice"
        Me.cmdWakeUpDevice.Size = New System.Drawing.Size(133, 39)
        Me.cmdWakeUpDevice.TabIndex = 29
        Me.cmdWakeUpDevice.Text = "WakeUpDevice"
        Me.cmdWakeUpDevice.UseVisualStyleBackColor = True
        '
        'cmdInit
        '
        Me.cmdInit.Location = New System.Drawing.Point(24, 25)
        Me.cmdInit.Name = "cmdInit"
        Me.cmdInit.Size = New System.Drawing.Size(116, 39)
        Me.cmdInit.TabIndex = 0
        Me.cmdInit.Text = "StartDevice"
        Me.cmdInit.UseVisualStyleBackColor = True
        '
        'frmHSMControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 572)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmHSMControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HSM Control Panel"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdClearText As System.Windows.Forms.Button
    Friend WithEvents cmdDiagnosticDevice As System.Windows.Forms.Button
    Friend WithEvents cmdGetDeviceProperty As System.Windows.Forms.Button
    Friend WithEvents txtDeviceProperty As System.Windows.Forms.TextBox
    Friend WithEvents cmdWrapDevice As System.Windows.Forms.Button
    Friend WithEvents cmdLockDevice As System.Windows.Forms.Button
    Friend WithEvents cmdUnlockDevice As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdInit As System.Windows.Forms.Button
    Friend WithEvents txtPAN As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerateMAC As System.Windows.Forms.Button
    Friend WithEvents txtDataToMac As System.Windows.Forms.TextBox
    Friend WithEvents txtMacedData As System.Windows.Forms.TextBox
    Friend WithEvents btnVerifyMac As System.Windows.Forms.Button
    Friend WithEvents rbnVerifyMac As System.Windows.Forms.RadioButton
    Friend WithEvents rbnGenMac As System.Windows.Forms.RadioButton
    Friend WithEvents cmdWakeUpDevice As System.Windows.Forms.Button
    Friend WithEvents txtPAD As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPinBlock As System.Windows.Forms.TextBox
    Friend WithEvents cmdGetPinBlock As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdUpdateWorkingKey As System.Windows.Forms.Button
    Friend WithEvents txtKeyIndex As System.Windows.Forms.TextBox
    Friend WithEvents txtWorkingKey As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtEncKeyIndex As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rbnNoParam As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPAD As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPAN As System.Windows.Forms.RadioButton
    Friend WithEvents ddlFormatType As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCheckSum As System.Windows.Forms.TextBox
End Class
