Imports clsKeypad
Imports clsKeypad.clsEPPKeypad

Imports clsHWDGlobalInterface
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class clsHardwareLayer





#Region "HSM Events"

#Region "Events Structure"

    Private udtDeviceSender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender
    Private udtEventDeviceArgs As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs
    Private udtDeviceError As clsHWDGlobalInterface.clsGolbalVar.device_error

#End Region

    'HSM Events  
    Public Event EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)

#End Region


#Region "Property Tmp Variable and Control"

    Private strProDeviceStatus As Integer = 0
    Private strProTxnStatus As String = String.Empty
    Private strProErrSeverity As String = String.Empty
    Private strProDignosticStatus As String = String.Empty
    Private strProSupplyStatus As String = String.Empty

#End Region

#Region "Form Variable"

    Structure HSMHWDSTR
        Dim strData As String
        Dim strPAN As String
        Dim strPadString As String
        Dim intPinBlockType As Integer
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure

    Public udtHSMHWD As HSMHWDSTR

    Public KeyStroke As String

    Public EPPTimeout As Long

#End Region

#Region "Hardware Object - Variable "

    'Keypad Hardware object
    'Public WithEvents KeypadTimer As New Timers.Timer
    Public WithEvents objHSMHWD As New clsKeypad.clsKeypadControl
    Public WithEvents objHSM As New clsEPPKeypad
    Public WithEvents objEPPController As New clsModuleController.clsEPPModuleController
    Public objHSMLookupData As New clsKeypad.clsKeypadControl
    Public WithEvents timerEvent As New Timers.Timer
    Public udtKEYPADHWDSTR As clsKeypadControl.KEYPADHWDSTR
    Public Shared blnReadObj As Boolean = False
    Public Shared strKeypadModel As String = ""
    Public Shared blnEncMode As Boolean = False
    Public Shared blnHideMode As Boolean = False
    Public Shared blnKeypadStatus As Boolean = False
    Public Shared intTimeout As Integer = 0

#End Region


#Region "New Class"

    Public Sub New()
        'Input - Default AppLayer\xxxxx.ini
        If objAppINI.ReadAppLayerINICFGFile(HWDLAYERINIPATH, EPP) = True Then

            'Read INI File
            'Log Ini File
            '1.UPS Setting
            '2.UPS Lookup Data

            With objAppINI.udtClsAppLayerIniCFG
                strLogIniPath = .strLogINIPath.Trim
            End With

            'Layer Class
            InitLog(strLogIniPath)
            strLogEvt = "EPP Hookup Class Init Ok"
            AppLogInfo(strLogEvt)

        End If
    End Sub

#End Region



#Region "Keypad Const Value"

    Private KeypadDeviceGraphicId As String = ""
    Private KeypadDeviceName As String = ""

    Public Sub initKeypadConst()
        Try
            KeypadDeviceGraphicId = objHSMLookupData.HSMLOOKUPDATAInfo.DeviceGraphicID
            KeypadDeviceName = objHSMLookupData.HSMLOOKUPDATAInfo.DeviceName

        Catch ex As Exception
            AppLogErr("Error in initKeypadConst()" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Keypad Property Value"

    'TxnStatus - TXST
    '1 - Successful Print
    '2 - Print Operation Not Successfully Completed
    '3 - Device Not Configure
    '4 - Cancel key pressed during sisdeways receipt print
    Private TXST_PINPAD_SUCCESS As String = ""
    Private TXST_PINPAD_NOT_COMPLETE As String = ""
    Private TXST_PINPAD_DEVICE_NOT_CFG As String = ""
    Private TXST_PINPAD_CANCEL_SIDEWAY As String = ""

    'ErrorSeverity - ERST
    Private ERST_KEYPAD_OK As String = ""
    Private ERST_KEYPAD_ERROR As String = ""
    Private ERST_KEYPAD_WARNING As String = ""
    Private ERST_KEYPAD_FATAL As String = ""

    'Diagnostic Status -DGST
    Private DGST_MSTATUS As String = ""
    Private DGST_MSTATUSDATA As String = ""

    'SupplyStatus - SYST  
    'Private Const SYST_STATUS As String = ""

    'KEY Variable
    Private EPP_KEY_A As String = ""
    Private EPP_KEY_B As String = ""
    Private EPP_MASTER_KEY As String = ""

    Public Sub initKeypadProperty()
        Try
            TXST_PINPAD_SUCCESS = objHSMLookupData.KEYPADLOOKUPDATAInfo.TxnStatusPrtSuccess
            TXST_PINPAD_NOT_COMPLETE = objHSMLookupData.KEYPADLOOKUPDATAInfo.TxnStatusPrtNotComplete
            TXST_PINPAD_DEVICE_NOT_CFG = objHSMLookupData.KEYPADLOOKUPDATAInfo.TxnStatusDeviceNotCfg
            TXST_PINPAD_CANCEL_SIDEWAY = objHSMLookupData.KEYPADLOOKUPDATAInfo.TxnStatusCancelSideway

            ERST_KEYPAD_OK = objHSMLookupData.KEYPADLOOKUPDATAInfo.ErrSvrtyKeypadOk
            ERST_KEYPAD_ERROR = objHSMLookupData.KEYPADLOOKUPDATAInfo.ErrSvrtyKeypadError
            ERST_KEYPAD_WARNING = objHSMLookupData.KEYPADLOOKUPDATAInfo.ErrSvrtyKeypadWarning
            ERST_KEYPAD_FATAL = objHSMLookupData.KEYPADLOOKUPDATAInfo.ErrSvrtyKeypadFatal

            DGST_MSTATUS = objHSMLookupData.KEYPADLOOKUPDATAInfo.MStatus
            DGST_MSTATUSDATA = objHSMLookupData.KEYPADLOOKUPDATAInfo.MStatusData

        Catch ex As Exception
            AppLogErr("Error in initKeypadProperty()" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Register EPP Key"

    Public Sub initEPPKey()
        Try
            EPP_KEY_A = objHSMLookupData.KEYPADLOOKUPDATAInfo.EPPCOMKEY
            EPP_KEY_B = objHSMLookupData.KEYPADLOOKUPDATAInfo.EPPMACKEY
            EPP_MASTER_KEY = objHSMLookupData.KEYPADLOOKUPDATAInfo.EPPMASTERKEY

        Catch ex As Exception
            AppLogErr("Error in initDeviceState()" & ex.Message)
        End Try
    End Sub

#End Region

#Region "EventDeviceArgs value iDeviceState,strEventYype"

#Region "Keypad Const Device State - iDeviceState"

    'Value for iDeviceState
    Private DST_NOPINPADSTATE As String = "" ' No Pinpad 
    Private DST_STARTPINPADSTATE As String = "" ' Start Pinpad

    Public Sub initDeviceState()
        Try
            DST_NOPINPADSTATE = objHSMLookupData.KEYPADLOOKUPDATAInfo.DeviceStateNoPrint
            DST_STARTPINPADSTATE = objHSMLookupData.KEYPADLOOKUPDATAInfo.DeviceStatePrint

        Catch ex As Exception
            AppLogErr("Error in initDeviceState()" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Keypad Const Event Type - strEvenType"

    'Value for strEvenType
    Private EVTTYPE_DEVICEERROR As String = "" ' Keypad Failed
    Private EVTTYPE_DEVICEWRAP As String = "" ' Wrap Keypad
    Private EVTTYPE_DATAARRIVED As String = "" ' Press Keypad
    Private EVTTYPE_TIMEOUT As String = "" ' Press Keypad


    Public Sub initEventType()
        Try
            EVTTYPE_DEVICEERROR = objHSMLookupData.KEYPADLOOKUPDATAInfo.EvtDeviceErr ' Keypad Failed
            EVTTYPE_DEVICEWRAP = objHSMLookupData.KEYPADLOOKUPDATAInfo.EvtDeviceWrap ' Wrap Keypad
            EVTTYPE_DATAARRIVED = objHSMLookupData.KEYPADLOOKUPDATAInfo.EvtDeviceReady ' Press Keypad
            EVTTYPE_TIMEOUT = objHSMLookupData.KEYPADLOOKUPDATAInfo.EvtDeviceTimeout ' Press Keypad
        Catch ex As Exception
            AppLogErr("Error in initEventType()" & ex.Message)
        End Try
    End Sub
#End Region

#End Region





#Region "Property - General"

    Property TxnStatus() As String
        Get
            Return udtHSMHWD.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtHSMHWD.strTxnStatus = value
        End Set
    End Property


    Property ErrorSeverity() As String
        Get
            Return udtHSMHWD.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtHSMHWD.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtHSMHWD.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtHSMHWD.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtHSMHWD.strMStatus
        End Get
        Set(ByVal value As String)
            udtHSMHWD.strMStatus = value
        End Set
    End Property

    Property MStatusData() As String
        Get
            Return udtHSMHWD.strMStatusData
        End Get
        Set(ByVal value As String)
            udtHSMHWD.strMStatusData = value
        End Set
    End Property

    Property udtReplyHSMHWD() As HSMHWDSTR
        Get
            Return udtHSMHWD
        End Get
        Set(ByVal value As HSMHWDSTR)
            udtHSMHWD = value
        End Set
    End Property

    Property udtReplyDeviceError() As clsHWDGlobalInterface.clsGolbalVar.device_error
        Get
            Return udtDeviceError
        End Get
        Set(ByVal value As clsHWDGlobalInterface.clsGolbalVar.device_error)
            udtDeviceError = value
        End Set
    End Property

    Property ReurnKeyStroke() As String
        Get
            Return KeyStroke
        End Get
        Set(ByVal value As String)
            KeyStroke = value
        End Set
    End Property

    Property udtReplyDeviceSender() As clsHWDGlobalInterface.clsGolbalVar.DeviceSender
        Get
            Return udtDeviceSender
        End Get
        Set(ByVal value As clsHWDGlobalInterface.clsGolbalVar.DeviceSender)
            udtDeviceSender = value
        End Set
    End Property

    Property ReturGenIDeviceTrace() As String
        Get
            Return strGeniDeviceTrace
        End Get
        Set(ByVal value As String)
            strGeniDeviceTrace = value
        End Set
    End Property

#End Region

#Region "Property - HSM"

    Property PAN() As String
        Get
            Return Right(udtHSMHWD.strPAN, 12)
        End Get
        Set(ByVal value As String)
            udtHSMHWD.strPAN = value
        End Set
    End Property

    Property PadString() As String
        Get
            Return Right(udtHSMHWD.strPadString, 1)
        End Get
        Set(ByVal value As String)
            udtHSMHWD.strPadString = value
        End Set
    End Property

    Property PinBlockType() As Integer
        Get
            Return udtHSMHWD.intPinBlockType
        End Get
        Set(ByVal value As Integer)
            udtHSMHWD.intPinBlockType = value
        End Set
    End Property

    Property TimeoutInterval(ByVal ConfigId As Long) As Long
        Get
            Select Case ConfigId
                Case 1
                    Return ClearModeTimer
                Case 2
                    Return PINModeTimer
                Case Else
                    Return 0
            End Select
        End Get
        Set(ByVal value As Long)
            Select Case ConfigId
                Case 1
                    ClearModeTimer = 60
                Case 2
                    PINModeTimer = 60
            End Select
        End Set
    End Property

    Property ClearModeTimer() As Long
        Get
            Return EPPTimeout
        End Get
        Set(ByVal value As Long)
            objHSMHWD.UpdateKeypadTimeout(value)
            EPPTimeout = value
        End Set
    End Property

    Property PINModeTimer() As Long
        Get
            Return EPPTimeout
        End Get
        Set(ByVal value As Long)
            objHSMHWD.UpdateKeypadTimeout(value)
            EPPTimeout = value
        End Set
    End Property

#End Region



#Region "StartDevice, StopDevice, WrapDevice"

    Public Function StartDevice() As Boolean
        Dim blnRtrn As Boolean = False
        objEPPController = New clsModuleController.clsEPPModuleController

        Try
            'Init the Hwd Object and the Log File
            If objHSMHWD.InitHSMControl = True Then

                If blnReadObj = False Then

                    'Init the Keypad comport setting
                    If objHSMHWD.ReadHSMHWDCFG = True Then

                        'Init HSM Lookup Data
                        initDeviceState()
                        initEventType()
                        initKeypadConst()
                        initKeypadProperty()
                        initEPPKey()

                        'Init the HSM Communication Port
                        If objEPPController.InitializeEPP("KEYPAD") = True Then
                            blnReadObj = True
                            strKeypadModel = objHSMHWD.KEYPADHWDInfo.strKeypadModel
                            blnEncMode = objHSMHWD.KEYPADHWDInfo.blnEncryptMode
                            blnHideMode = objHSMHWD.KEYPADHWDInfo.blnHideMode
                            blnKeypadStatus = objHSMHWD.KEYPADHWDInfo.blnKeypadStatus
                            intTimeout = objHSMHWD.KEYPADHWDInfo.intKeypadTimeOut
                            blnRtrn = True
                            AppLogInfo("Init HSM Device OK")

                            'Test Epp
                            If objHSMHWD.Send_CmdTestEpp Then
                                blnRtrn = True
                                AppLogInfo("Test EPP OK")
                                blnLockHSM = False
                                udtHSMHWD.strTxnStatus = TXST_PINPAD_SUCCESS
                                timerEvent.Start()
                                timerEvent.Interval = 100
                                AppLogInfo("Init HSD Device OK")
                                AppLogInfo("HSM Hardware Init Successfully")
                                objEPPController.getFormatType = PinBlockType
                                objEPPController.getPAN = PAN
                                objEPPController.getPADString = PadString
                                objHSMHWD.WriteLogStartDeviceHSM(objEPPController.getFormatType, objEPPController.getPAN, objEPPController.getPADString)

                            Else
                                'KeypadDeviceError()
                                blnRtrn = False
                                AppLogErr("Test EPP Failed")
                            End If

                        Else
                            'KeypadDeviceError()
                            blnRtrn = False
                            AppLogErr("Init HSM Device Failed")
                        End If

                    Else
                        blnRtrn = False
                        AppLogErr("Read HSM Hardware Setting Failed")
                    End If
                Else
                    'Init the Keypad comport setting
                    If objHSMHWD.UpdateKEYPADHWDCFG(strKeypadModel, blnEncMode, blnHideMode, blnKeypadStatus, intTimeout) = True Then

                        'Init HSM Lookup Data
                        initDeviceState()
                        initEventType()
                        initKeypadConst()
                        initKeypadProperty()

                        'Init the HSM Communication Port
                        If objEPPController.InitializeEPP("KEYPAD") = True Then

                            blnRtrn = True
                            AppLogInfo("Init HSM Device OK")

                            'Test Epp
                            If objHSMHWD.Send_CmdTestEpp Then
                                blnRtrn = True
                                AppLogInfo("Test EPP OK")
                                blnLockHSM = False
                                udtHSMHWD.strTxnStatus = TXST_PINPAD_SUCCESS
                                timerEvent.Start()
                                timerEvent.Interval = 100
                                AppLogInfo("Init HSD Device OK")
                                AppLogInfo("HSM Hardware Init Successfully")
                                objHSMHWD.WriteLogStartDeviceHSM(PinBlockType, PAN, PadString)
                            Else
                                'KeypadDeviceError()
                                blnRtrn = False
                                AppLogErr("Test EPP Failed")
                            End If

                        Else
                            'KeypadDeviceError()
                            blnRtrn = False
                            AppLogErr("Init HSM Device Failed")
                        End If

                    Else
                        blnRtrn = False
                        AppLogErr("Read HSM Hardware Setting Failed")
                    End If
                End If


            Else
                blnRtrn = False
                AppLogErr("Init HSM Hardware and log object Failed")
            End If

            'Reply Status
            Return blnRtrn
        Catch ex As Exception
            'objAppLogFunction.AppLogErr("Error in StartDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Try
            If objEPPController.CloseEPP Then
                blnLockHSM = False
                objHSMHWD.CloseKEYPADHWDCFG()
                AppLogInfo("HSM Hardware Close Successfully")
                objHSMHWD.WriteLogStopDeviceHSM()
                Return True

            Else
                Return False
            End If


        Catch ex As Exception
            AppLogErr("Error in StopDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean

        Try
            PinBlockType = 1
            PAN = ""
            PadString = ""
            HSMData = ""

            objEPPController.getFormatType = PinBlockType
            objEPPController.getPAN = PAN
            objEPPController.getPADString = PadString

            strProDeviceStatus = 0
            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = ""

            With udtHSMHWD
                .strData = ""
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
                .strMStatusData = ""
            End With

            'Keypad EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = KeypadDeviceGraphicId
                .strDeviceName = KeypadDeviceName
            End With

            'Keypad EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = KeypadDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DST_NOPINPADSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DEVICEWRAP
                .mDeviceDataValue = ""
            End With

            'Log Info

            AppLogInfo("WrapDevice iDeviceTrace ID:" & strGeniDeviceTrace)
            objHSMHWD.WriteLogWrapDeviceHSM(objEPPController.getFormatType, objEPPController.getPAN, objEPPController.getPADString)

            Return True



        Catch ex As Exception
            AppLogErr("Error in WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Try
            blnLockHSM = True
            objHSMHWD.WriteLogLockDeviceHSM()
            Return True

        Catch ex As Exception
            AppLogErr("Error in LockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Try
            blnLockHSM = False
            objHSMHWD.WriteLogUnlockDeviceHSM()
            Return True

        Catch ex As Exception
            AppLogErr("Error in UnLockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal strDeviceState As Integer) As Boolean
        Try
            If blnLockHSM = False Then
                objEPPController.getFormatType = PinBlockType
                objEPPController.getPAN = PAN
                objEPPController.getPADString = PadString
                objHSMHWD.WriteLogWakeDeviceHSM(objEPPController.getFormatType, objEPPController.getPAN, objEPPController.getPADString)
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in WakeUpDevice:" & ex.Message)
            Return False
        End Try
    End Function

    'Public Function DiagnosticDevice() As Boolean
    '    objSagemEPP = New clsKeypad.clsEPPKeypad

    '    Try
    '        'If PrinterIsStopped() = False Then
    '        '    If objCustomPrinter.InitPrinterConnection = True Then
    '        '        If objCustomPrinter.PrinterGetStatus = True Then
    '        '            udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_SUCCESS

    '        '        Else
    '        '            udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_NOT_COMPLETE
    '        '        End If
    '        '    Else
    '        '        udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_DEVICE_NOT_CFG

    '        '    End If

    '        '    ReceiptPrinterGetStatus()

    '        'Else
    '        '    ReceiptPrinterServiceIsStop()
    '        'End If

    '        Return True

    '    Catch ex As Exception
    '        objAppLogFunction.AppLogErr("Error in DiagnosticDevice:" & ex.Message)strEncKeyIndex
    '        Return False
    '    End Try
    'End Function

    Public Function UpdateWorkingKey(ByVal KeyIndex As String, ByVal TheWorkingKey As String, ByVal EncKeyIndex As String, ByVal CheckSum As String) As Boolean
        Dim strIndicator As String = "07"
        Dim strWorkingKeyLen As String = "16"
        Dim strTempKeyIndex As String = ""
        Dim strTempEncKeyIndex As String = ""

        Try
            If KeyIndex = "A" Then
                strTempKeyIndex = EPP_KEY_A

            ElseIf KeyIndex = "B" Then
                strTempKeyIndex = EPP_KEY_B

            ElseIf KeyIndex = "M" Then
                strTempKeyIndex = EPP_MASTER_KEY
            End If

            If EncKeyIndex = "M" Then
                strTempEncKeyIndex = EPP_MASTER_KEY
            End If


            If objHSMHWD.Send_CmdWriteMPA(strTempKeyIndex, strIndicator, strTempEncKeyIndex, strWorkingKeyLen, TheWorkingKey) Then
                If CheckSum = String.Empty Then
                    If objHSMHWD.Send_CmdCheckSum(strTempKeyIndex) Then
                        HSMData = objHSMHWD.GetCheckSumedData
                        HSMDeviceDataReady()
                        Return True

                    Else
                        HSMData = ""
                        HSMDeviceError()
                        Return False
                    End If
                Else
                    If objHSMHWD.Send_CmdValidateCheckSum(strTempKeyIndex, CheckSum) Then
                        HSMData = objHSMHWD.GetCheckSumedData
                        HSMDeviceDataReady()
                        Return True

                    Else
                        HSMData = ""
                        HSMDeviceError()
                        Return False
                    End If
                End If

            Else
                HSMData = ""
                HSMDeviceError()
                Return False
            End If

            'If strChecksum <> String.Empty Then
            '    If objHSMHWD.Send_CmdCheckSum(strKeyIndex, strChecksum) Then
            '        HSMData = objHSMHWD.GetCheckSumedData
            '        HSMDeviceDataReady()
            '        Return True

            '    Else
            '        Return False
            '    End If
            'End If

        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function GenerateMac(ByVal MacString As String) As String
        Dim strMacData As String = ""

        Try
            If blnLockHSM = False Then
                strMacData = objHSMHWD.Send_CmdCalcCBCMAC(MacString, EPP_KEY_B)

                Return strMacData

            Else
                Return strMacData
            End If

        Catch ex As Exception
            Return strMacData
        End Try
    End Function

    Public Function VerifyMac(ByVal MacString As String, ByVal MacData As String) As Boolean
        Dim strMacData As String

        Try
            strMacData = objHSMHWD.Send_CmdCalcCBCMAC(MacString, EPP_KEY_B)

            If strMacData = MacData Then
                HSMData = objHSMHWD.GetMacedData
                HSMDeviceDataReady()
                Return True

            Else
                HSMData = ""
                HSMDeviceError()
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetPinBlock() As String
        Dim strEncPINBlock As String = ""
        Dim strFormatType As String = ""
        Dim strParam As String = ""

        Try
            'strFormatType = objEPPController.getFormatType
            objEPPController.getFormatType = PinBlockType
            objEPPController.getPAN = PAN
            objEPPController.getPADString = PadString
            strFormatType = PinBlockType

            If strFormatType = 1 Then
                strParam = objEPPController.getPAN

            ElseIf strFormatType = 2 Or strFormatType = 3 Or strFormatType = 5 Or strFormatType = 6 Then
                strParam = objEPPController.getPADString

            ElseIf strFormatType = 4 Then
                strParam = ""
            End If

            'MsgBox("PIN Block Type: " & objEPPController.getFormatType)
            'MsgBox("PAN: " & objEPPController.getPAN)
            'MsgBox("Pad String: " & objEPPController.getPADString)

            objHSMHWD.Send_CmdStopEncTextMode()
            strEncPINBlock = objHSMHWD.Send_CmdReadPIN(strFormatType, strParam, EPP_KEY_A)
            objHSMHWD.WriteLogGetPinBlockHSM(objEPPController.getFormatType, objEPPController.getPAN, objEPPController.getPADString)

            Return strEncPINBlock

        Catch ex As Exception
            Return strEncPINBlock
        End Try
    End Function

  
    Public Function HSMDeviceDataReady() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PINPAD_SUCCESS
            strProErrSeverity = ERST_KEYPAD_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = "1"

            'Set the Property Value
            With udtHSMHWD
                .strData = HSMData
                '.strDeviceStatus = strProDeviceStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Device Sender
            With udtDeviceSender
                .strDeviceGraphicId = udtReplyDeviceSender.strDeviceGraphicId
                .strDeviceName = udtReplyDeviceSender.strDeviceName
            End With

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_NOPINPADSTATE
                .strEventType = EVTTYPE_DEVICEWRAP
                .iDeviceTrace = ReturGenIDeviceTrace
                .mDeviceDataValue = udtHSMHWD.strData
            End With

            HSMData = ""

            'Raise the Events
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            AppLogErr("Raise event in Keypad: EvtDeviceDataReady")
            Return True
        Catch ex As Exception
            AppLogErr("Error in KeypadDeviceDataReady:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function HSMDeviceError() As Boolean
        Try

            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PINPAD_DEVICE_NOT_CFG
            strProErrSeverity = ERST_KEYPAD_WARNING
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = ""

            'Set the Property Value
            With udtHSMHWD
                .strData = ""
                '.strDeviceStatus = strProDeviceStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            With udtDeviceError
                '.Status = DeviceStatus
                .TxnStatus = TxnStatus
                .ErrorSeverity = ErrorSeverity
                .SupplyStatus = SupplyStatus
                .MStatus = DGST_MSTATUS
                .MStatus = DGST_MSTATUSDATA
            End With

            'Device Sender
            With udtDeviceSender
                .strDeviceGraphicId = udtReplyDeviceSender.strDeviceGraphicId
                .strDeviceName = udtReplyDeviceSender.strDeviceName
            End With

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_NOPINPADSTATE
                .strEventType = EVTTYPE_DEVICEERROR
                .iDeviceTrace = ReturGenIDeviceTrace
                .mDeviceDataValue = udtReplyDeviceError
            End With

            'Raise the Events
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
            AppLogErr("Raise event in Keypad: EvtDeviceError")
            Return True

        Catch ex As Exception
            AppLogErr("Error in KeypadDeviceError:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

    Private Sub timerEvent_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timerEvent.Elapsed
        Try
            timerEvent.Stop()
            HSMDeviceDataReady()
        Catch ex As Exception
            AppLogErr("Error in timerEvent_Elapsed:" & ex.Message)
        End Try
    End Sub


End Class
