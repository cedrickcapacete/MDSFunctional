Imports System.Threading

Imports clsKeypad
Imports clsKeypad.clsEPPKeypad
Imports clsHWDGlobalInterface
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class clsHardwareLayer

#Region "Form Variable"

    Structure KeypadHWDSTR
        Dim strRawData As String
        Dim strEncData As String
        Dim strHideData As String
        Dim blnHideMode As Boolean
        Dim blnEncMode As Boolean
        Dim intMaxLen As Integer
        Dim blnSupervisor As Boolean
        Dim intTimeout As Integer
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure

    Public udtKeypadHWD As KeypadHWDSTR

    Public KeyStroke As String = ""

    Public EPPTimeout As Long

#End Region

#Region "Property Tmp Variable and Control"

    Private strProDeviceStatus As Integer = 0
    Private strProTxnStatus As String = String.Empty
    Private strProErrSeverity As String = String.Empty
    Private strProDignosticStatus As String = String.Empty
    Private strProSupplyStatus As String = String.Empty

#End Region

#Region "Hardware Object - Variable "

    'Keypad Hardware object
    'Public WithEvents KeypadTimer As New Timers.Timer
    Public WithEvents objKeypadHWD As New clsKeypad.clsKeypadControl
    Public WithEvents objKeypad As New clsEPPKeypad
    Public WithEvents objEPPController As New clsModuleController.clsEPPModuleController


    Public objKeypadLookupData As New clsKeypad.clsKeypadControl

    'Public WithEvents timer As New Timers.Timer
    'Public WithEvents timerEvent As New Timers.Timer
    'Public WithEvents timerSupervisorKey As New Timers.Timer
    'Public WithEvents timer2 As New Timers.Timer
    'Private WithEvents timerKeyPress As New Timers.Timer


    Public Shared blnReadObj As Boolean = False
    Public Shared strKeypadModel As String = ""
    Public Shared blnEncMode As Boolean = False
    Public Shared blnHideMode As Boolean = False
    Public Shared blnKeypadStatus As Boolean = False
    Public Shared intTimeout As Integer = 0
    Public Shared blnTimerStop As Boolean = False


#End Region



#Region "Keypad Events"

    Private udtDeviceSender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender
    Private udtEventDeviceArgs As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs
    Private udtDeviceError As clsHWDGlobalInterface.clsGolbalVar.device_error

    'Printer Receipt Events  
    Public Event EvtDeviceDataReady(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event EnterClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event CancelClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event ClearClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event NumericClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event HexKeyClick(ByVal Sender As clsHWDGlobalInterface.clsGolbalVar.DeviceSender, ByVal e As clsHWDGlobalInterface.clsGolbalVar.EventDeviceArgs)
    Public Event KeypadPress(ByVal KeyStroke As String)
    Public Event Timeout()

#End Region


#Region "New Class"

    Public Sub New()

        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppINI.ReadAppLayerINICFGFile(HWDLAYERINIPATH, EPP) = True Then

                objEPPController = New clsModuleController.clsEPPModuleController

                'Log Ini File
                'Keypad Lookup Data
                With objAppINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                AppLogInfo("EPP Hookup Class Init Ok")
                AppLogInfo("EPP Hookup Log Path:" & strLogIniPath.Trim)

                'Init the Hwd Object and the Log File
                If objKeypadHWD.InitKEYPADControl = True Then
                    AppLogInfo("Read Keypad Hardware Setting Ok")
                    'Init the Keypad comport setting
                    If objKeypadHWD.ReadKEYPADHWDCFG = True Then
                        AppLogInfo("Init Keypad Hardware Ok")
                    Else
                        AppLogWarn("Init Keypad Hardware Failed")
                        KeypadDeviceError()
                    End If
                Else
                    AppLogWarn("Read Keypad Hardware Setting Failed")
                    KeypadDeviceError()
                End If
            Else
                'Init Error
                KeypadDeviceError()
            End If

        Catch ex As Exception
            'Init Error - System Error
            KeypadDeviceError()
        End Try
    End Sub

#End Region





#Region "Keypad Const Value"

    Private KeypadDeviceGraphicId As String = ""
    Private KeypadDeviceName As String = ""

    Public Sub initKeypadConst()
        Try
            KeypadDeviceGraphicId = objKeypadLookupData.KEYPADLOOKUPDATAInfo.DeviceGraphicID
            KeypadDeviceName = objKeypadLookupData.KEYPADLOOKUPDATAInfo.DeviceName

        Catch ex As Exception
            AppLogErr("Error in initKeypadConst" & ex.Message)
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

    Private EPP_MASTER_KEY As String = ""


    Public Sub initKeypadProperty()
        Try
            TXST_PINPAD_SUCCESS = objKeypadLookupData.KEYPADLOOKUPDATAInfo.TxnStatusPrtSuccess
            TXST_PINPAD_NOT_COMPLETE = objKeypadLookupData.KEYPADLOOKUPDATAInfo.TxnStatusPrtNotComplete
            TXST_PINPAD_DEVICE_NOT_CFG = objKeypadLookupData.KEYPADLOOKUPDATAInfo.TxnStatusDeviceNotCfg
            TXST_PINPAD_CANCEL_SIDEWAY = objKeypadLookupData.KEYPADLOOKUPDATAInfo.TxnStatusCancelSideway

            ERST_KEYPAD_OK = objKeypadLookupData.KEYPADLOOKUPDATAInfo.ErrSvrtyKeypadOk
            ERST_KEYPAD_ERROR = objKeypadLookupData.KEYPADLOOKUPDATAInfo.ErrSvrtyKeypadError
            ERST_KEYPAD_WARNING = objKeypadLookupData.KEYPADLOOKUPDATAInfo.ErrSvrtyKeypadWarning
            ERST_KEYPAD_FATAL = objKeypadLookupData.KEYPADLOOKUPDATAInfo.ErrSvrtyKeypadFatal

            DGST_MSTATUS = objKeypadLookupData.KEYPADLOOKUPDATAInfo.MStatus
            DGST_MSTATUSDATA = objKeypadLookupData.KEYPADLOOKUPDATAInfo.MStatusData

        Catch ex As Exception
            AppLogErr("Error in initKeypadProperty()" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Register EPP Key"

    Public Sub initEPPKey()
        Try
            EPP_MASTER_KEY = objKeypadLookupData.KEYPADLOOKUPDATAInfo.EPPMASTERKEY

        Catch ex As Exception
            AppLogErr("Error in initEPPKey()" & ex.Message)
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
            DST_NOPINPADSTATE = objKeypadLookupData.KEYPADLOOKUPDATAInfo.DeviceStateNoPrint
            DST_STARTPINPADSTATE = objKeypadLookupData.KEYPADLOOKUPDATAInfo.DeviceStatePrint

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
            EVTTYPE_DEVICEERROR = objKeypadLookupData.KEYPADLOOKUPDATAInfo.EvtDeviceErr ' Keypad Failed
            EVTTYPE_DEVICEWRAP = objKeypadLookupData.KEYPADLOOKUPDATAInfo.EvtDeviceWrap ' Wrap Keypad
            EVTTYPE_DATAARRIVED = objKeypadLookupData.KEYPADLOOKUPDATAInfo.EvtDeviceReady ' Press Keypad
            EVTTYPE_TIMEOUT = objKeypadLookupData.KEYPADLOOKUPDATAInfo.EvtDeviceTimeout ' Press Keypad
        Catch ex As Exception
            AppLogErr("Error in initEventType()" & ex.Message)
        End Try
    End Sub
#End Region

#End Region


#Region "Property"

    ReadOnly Property KeypadModel() As String
        Get
            Return objKeypadHWD.KeypadModelInfo
        End Get
    End Property

    ReadOnly Property KeypadComport() As String
        Get
            Return objKeypadHWD.KeypadComportInfo
        End Get
    End Property

#End Region


#Region "Support Method"

    Public Function UpdateKeyPadSetting(ByVal strKeypadNm As String, ByVal strKeypadComport As String) As Boolean
        Try

            If objKeypadHWD.UpdateKEYPADHWDSetting(strKeypadNm, strKeypadComport) = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in UpdateKeyPadSetting:" & ex.Message)
            Return False
        End Try
    End Function



#End Region






#Region "StartDevice, StopDevice, WrapDevice"

    Public Function StartDevice() As Boolean
        Dim blnRtrn As Boolean = False
       
        Try

          
            'Init Keypad Lookup Data
            initDeviceState()
            initEventType()
            initKeypadConst()
            initKeypadProperty()
            initEPPKey()

            TimeoutInterval(1) = 0

            'Init the Keypad Communication Port
            If objEPPController.InitializeEPP("KEYPAD") = True Then
                blnReadObj = True
                strKeypadModel = objKeypadHWD.KEYPADHWDInfo.strKeypadModel
                blnEncMode = objKeypadHWD.KEYPADHWDInfo.blnEncryptMode
                blnHideMode = objKeypadHWD.KEYPADHWDInfo.blnHideMode
                blnKeypadStatus = objKeypadHWD.KEYPADHWDInfo.blnKeypadStatus
                intTimeout = objKeypadHWD.KEYPADHWDInfo.intKeypadTimeOut
                blnRtrn = True

                AppLogInfo("Init Keypad Device OK")

                'Test Epp
                If objKeypadHWD.Send_CmdTestEpp Then
                    blnRtrn = True
                    AppLogInfo("Test EPP OK")
                    blnLockKeypad = False

                    If WrapDevice() = True Then
                        blnRtrn = True
                        udtKeypadHWD.strTxnStatus = TXST_PINPAD_SUCCESS
                        'timerEvent.Start()
                        'timerEvent.Interval = 1000
                        'KeypadDeviceDataReady()
                        AppLogInfo("Init Keypad Device OK")
                        AppLogInfo("Keypad Hardware Init Successfully")
                    Else
                        AppLogErr("WrapDevice()- Wrap Keypad Failed")
                        KeypadDeviceError()
                        blnRtrn = False
                    End If
                Else
                    AppLogErr("Test EPP Failed")
                    KeypadDeviceError()
                    blnRtrn = False
                End If
            Else
                AppLogErr("Init Keypad Device Failed")
                KeypadDeviceError()
                blnRtrn = False
            End If

            'Reply Status
            Return blnRtrn
        Catch ex As Exception
            AppLogErr("Error in StartDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Dim blnReply As Boolean = False

        Try
            If objEPPController.CloseEPP Then
                AppLogInfo("Keypad Hardware Close Successfully")
                blnReply = True
            Else
                AppLogInfo("Keypad Hardware Close Failed")
                blnReply = False
            End If

            'Close Logger
            CloseLog()

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in StopDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean

        Try
            'EncryptMode = False
            'HideMode = False
            'MaxLength = 0

            RawData = ""
            EncData = ""
            HideData = ""

            strProDeviceStatus = 0
            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = ""

            With udtKeypadHWD
                .strRawData = ""
                .strEncData = ""
                .strHideData = ""
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

            Return True

        Catch ex As Exception
            AppLogErr("Error in WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function GenDateTimeStamp() As String
        Dim strDateTimeStamp As String = String.Empty
        Dim strDay As String = String.Empty
        Dim strMonth As String = String.Empty
        Dim strYear As String = String.Empty

        Dim strHH As String = String.Empty
        Dim strMM As String = String.Empty
        Dim strSS As String = String.Empty

        Dim strTmpdate As String = String.Empty
        Dim strTmptime As String = String.Empty

        Try

            strDay = DateTime.Now.Day.ToString("00")
            strMonth = DateTime.Now.Month.ToString("00")
            strYear = DateTime.Now.Year.ToString("0000")

            strHH = DateTime.Now.Hour.ToString("00")
            strMM = DateTime.Now.Minute.ToString("00")
            strSS = DateTime.Now.Second.ToString("00")

            strTmpdate = strDay & strMonth & strYear
            strTmptime = strHH & strMM & strSS

            strDateTimeStamp = strTmpdate.Trim & strTmptime.Trim

            Return strDateTimeStamp
        Catch ex As Exception
            AppLogErr("Error in GenDateTimeStamp:" & ex.Message)
            Return "9999"
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Try
            blnLockKeypad = True
            Text = ""
            'timerKeyPress.Stop()
            objKeypadHWD.Send_CmdStopClearTextMode()
            Return True

        Catch ex As Exception
            AppLogErr("Error in LockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Try
            blnLockKeypad = False
            Return True

        Catch ex As Exception
            AppLogErr("Error in UnLockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal strDeviceState As Integer) As Boolean
        Dim blnReply As Boolean = False
        Try
            blnTimerStop = False
            blnLockKeypad = False

            If HideMode = True And EncryptMode = True And MaxLength > 0 Then
                If objKeypadHWD.Send_CmdStartEncTextMode Then
                    'timerKeyPress.Start()
                    'timerKeyPress.Interval = 60000
                    Text = ""
                    objKeypadHWD.Send_CmdCallKeyPress()
                    blnReply = True
                End If

            ElseIf Supervisor = True Then
                If objKeypadHWD.Send_CmdClearAllRegister() Then
                    If objKeypadHWD.Send_CmdDisablePassword Then
                        Text = ""
                        blnReply = True
                    End If
                End If

            Else
                If objKeypadHWD.Send_CmdStartClearTextMode Then
                    'timerKeyPress.Start()
                    'timerKeyPress.Interval = 60000
                    Text = ""
                    'MaxLength = 6
                    objKeypadHWD.Send_CmdCallKeyPress()
                    blnReply = True
                End If

            End If

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in WakeUpDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        objSagemEPP = New clsKeypad.clsEPPKeypad

        Try

            Return True

        Catch ex As Exception
            AppLogErr("Error in DiagnosticDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WriteKey(ByVal strKey As String) As Boolean

        Dim blnReply As Boolean = False

        Try
            ControlKey = strKey

            If ControlKey = "A" Or ControlKey = "B" Then
                If objKeypadHWD.Send_CmdStartSupervisorMode Then
                    objKeypadHWD.Send_CmdCallHexKeyPress()
                    blnReply = True
                End If

            Else
                If objKeypadHWD.Send_CmdSetMasterKey(EPP_MASTER_KEY) Then
                    If objSagemEPP.CMD_CHECK_SUM(EPP_MASTER_KEY, String.Empty) Then
                        RawData = String.Empty
                        RawData &= objSagemEPP.GetCheckSumedData
                        KeypadDeviceDataReady()
                        blnReply = True
                    End If

                Else
                    blnReply = False
                End If

            End If

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in WriteKey(" & strKey & ") : " & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Property - General"

    Property TxnStatus() As String
        Get
            Return udtKeypadHWD.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtKeypadHWD.strTxnStatus = value
        End Set
    End Property


    Property ErrorSeverity() As String
        Get
            Return udtKeypadHWD.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtKeypadHWD.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtKeypadHWD.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtKeypadHWD.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtKeypadHWD.strMStatus
        End Get
        Set(ByVal value As String)
            udtKeypadHWD.strMStatus = value
        End Set
    End Property

    Property MStatusData() As String
        Get
            Return udtKeypadHWD.strMStatusData
        End Get
        Set(ByVal value As String)
            udtKeypadHWD.strMStatusData = value
        End Set
    End Property

    Property udtReplyKeypadHWD() As KeypadHWDSTR
        Get
            Return udtKeypadHWD
        End Get
        Set(ByVal value As KeypadHWDSTR)
            udtKeypadHWD = value
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

    Property Supervisor() As Boolean
        Get
            Return udtKeypadHWD.blnSupervisor
        End Get
        Set(ByVal value As Boolean)
            udtKeypadHWD.blnSupervisor = value
        End Set
    End Property

    Property MaxLength() As Integer
        Get
            Return udtKeypadHWD.intMaxLen
        End Get
        Set(ByVal value As Integer)
            udtKeypadHWD.intMaxLen = value
        End Set
    End Property

    Property HideMode() As Boolean
        Get
            Return udtKeypadHWD.blnHideMode
        End Get
        Set(ByVal value As Boolean)
            udtKeypadHWD.blnHideMode = value
        End Set
    End Property

    Property EncryptMode() As Boolean
        Get
            Return udtKeypadHWD.blnEncMode
        End Get
        Set(ByVal value As Boolean)
            udtKeypadHWD.blnEncMode = value
        End Set
    End Property

    Property TimeoutInterval(ByVal ConfigId As Long) As Long
        Get
            Select Case ConfigId
                Case 1
                    Return ClearModeTimer
                    'MsgBox(ClearModeTimer)
                Case 2
                    Return PINModeTimer
                    'MsgBox(PINModeTimer)
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
            objKeypadHWD.UpdateKeypadTimeout(value)
            EPPTimeout = value
        End Set
    End Property

    Property PINModeTimer() As Long
        Get
            Return EPPTimeout
        End Get
        Set(ByVal value As Long)
            objKeypadHWD.UpdateKeypadTimeout(value)
            EPPTimeout = value
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


#Region "Property - Keypad"

    'Property EncryptMode() As Boolean
    '    Get
    '        KeypadDeviceDataReady()
    '        Return True
    '    End Get
    '    Set(ByVal value As Boolean)
    '        udtKeypadHWD.blnEncMode = True
    '        udtKeypadHWD.blnHideMode = True
    '        RawData = value
    '    End Set
    'End Property

    Property Text() As String
        Get
            Return RawData
        End Get
        Set(ByVal value As String)
            RawData = value
        End Set
    End Property

    Property GetRawPINNo() As String
        Get
            Return udtKeypadHWD.strHideData
        End Get
        Set(ByVal value As String)
            udtKeypadHWD.strHideData = value
        End Set
    End Property

    Property GetEncPINNo() As String
        Get
            Return udtKeypadHWD.strEncData
        End Get
        Set(ByVal value As String)
            udtKeypadHWD.strEncData = value
        End Set
    End Property

    Property GetAccountNo() As String
        Get
            Return udtKeypadHWD.strRawData
        End Get
        Set(ByVal value As String)
            udtKeypadHWD.strRawData = value
        End Set
    End Property

#End Region


#Region "Hardware Layer Event "

    Private Function KeypadGetStatus() As Boolean
        Try
            'Set the Property Value
            With udtKeypadHWD
                .strTxnStatus = TxnStatus

                If objKeypadHWD.KEYPADHWDInfo.blnKeypadStatus = True Then
                    .strErrSeverity = ERST_KEYPAD_OK
                    '.strSupplyStatus = objKeypadLookupData.RECEIPTPRINTERLOOKUPDATAInfo.SufficientPaper & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK  '1111

                    'ElseIf objKeypadHWD.RECEIPTPRINTERHWDInfo.blnPaperLow = True Then
                    '    .strErrSeverity = ERST_PRINTER_WARNING
                    '    .strSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PaperLow & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK  '2111

                    'ElseIf objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperEnd = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperJam = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = False Then
                    '    .strErrSeverity = ERST_PRINTER_FATAL
                    '    .strSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PaperExh & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK  '3111
                End If

                .strSupplyStatus = ""
                .strMStatus = MStatus
                .strMStatusData = MStatusData
            End With

            'Set the Property Value
            With udtKeypadHWD
                .strEncData = ""
                .strHideData = ""
                .strRawData = ""
                '.strDeviceStatus = strProDeviceStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'With udtDeviceError
            '    '.Status = DeviceStatus
            '    .TxnStatus = TxnStatus
            '    .ErrorSeverity = ErrorSeverity
            '    .SupplyStatus = SupplyStatus
            '    .MStatus = DGST_MSTATUS
            '    .MStatus = DGST_MSTATUSDATA
            'End With

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
                .mDeviceDataValue = ""
            End With

            'Raise the Events
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)

            Return True
            'End If

        Catch ex As Exception
            AppLogErr("Error in KeypadGetStatus:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CmdEnableTextMode() As Boolean
        Try
            RawData = ""
            EncData = ""
            HideData = ""

            AppLogInfo("clsHardwareLayer - CmdEnableTextMode. blnLock:" & blnLockKeypad)

            If blnLockKeypad = False Then

                If objKeypadHWD.Send_CmdStartClearTextMode() Then
                    AppLogInfo("clsHardwareLayer - cmdStartClearTextMode Ok")
                    Return True
                Else
                    AppLogWarn("clsHardwareLayer - cmdStartClearTextMode FALSE")
                    Return False
                End If
            Else
                AppLogWarn("clsHardwareLayer - cmdStartClearTextMode FALSE")
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in CmdEnableTextMode:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CmdDisableTextMode() As Boolean
        Try
            RawData = ""
            EncData = ""
            HideData = ""

            Thread.Sleep(objKeypadHWD.ReturnKeypadTimeout)

            If objKeypadHWD.Send_CmdStopClearTextMode() Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in CmdDisableTextMode:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CmdEnableEncryptMode() As Boolean
        Try
            RawData = ""
            EncData = ""
            HideData = ""

            If blnLockKeypad = False Then
                If objKeypadHWD.Send_CmdStartEncTextMode() Then
                    Return True

                Else
                    Return False
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in CmdEnableEncryptMode:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CmdCallKeyPress() As Boolean
        Try
            'objKeypad = New clsEPPKeypad
            If objKeypadHWD.Send_CmdCallKeyPress() Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in CmdCallKeyPress:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CmdCallPINEntry() As Boolean
        Try
            If objKeypadHWD.Send_CmdCallKeyPress() Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in CmdCallKeyPress:" & ex.Message)
            Return False
        End Try
    End Function

    'Public Function CmdCallTestEpp() As Boolean
    '    Try
    '        'objKeypad = New clsEPPKeypad

    '        If objKeypadHWD.Send_CmdTestEpp Then
    '            Return True

    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        objAppLogFunction.AppLogErr("Error in CmdCallClickEnterButton:" & ex.Message)
    '        Return False
    '    End Try
    'End Function

    Public Function CmdCallClickEnterButton() As Boolean
        Try
            'objKeypad = New clsEPPKeypad

            If KeypadEnterButtonClicked() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in CmdCallClickEnterButton:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function KeypadDeviceDataReady() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PINPAD_SUCCESS
            strProErrSeverity = ERST_KEYPAD_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = "1"

            'Set the Property Value
            With udtKeypadHWD
                .strRawData = RawData
                .strEncData = EncData
                .strHideData = HideData
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
                .iDeviceState = DST_STARTPINPADSTATE
                .strEventType = EVTTYPE_DEVICEWRAP
                .iDeviceTrace = ReturGenIDeviceTrace

                If udtKeypadHWD.blnEncMode = True Then
                    .mDeviceDataValue = udtKeypadHWD.strEncData
                Else
                    .mDeviceDataValue = udtKeypadHWD.strRawData
                End If
            End With

            RawData = ""
            EncData = ""
            HideData = ""

            'Raise the Events
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("Raise event in Keypad: EvtDeviceDataReady")

            Return True
        Catch ex As Exception
            AppLogErr("Error in KeypadDeviceDataReady:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function KeypadDeviceError() As Boolean
        Try

            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PINPAD_DEVICE_NOT_CFG
            strProErrSeverity = ERST_KEYPAD_FATAL
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = ""

            'Set the Property Value
            With udtKeypadHWD
                .strEncData = ""
                .strHideData = ""
                .strRawData = ""
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
            AppLogWarn("Raise event in Keypad: EvtDeviceError")

            Return True

        Catch ex As Exception
            AppLogErr("Error in KeypadDeviceError:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function KeypadDeviceTimeout() As Boolean
        Try

            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PINPAD_NOT_COMPLETE
            strProErrSeverity = ERST_KEYPAD_WARNING
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = "1"

            'Set the Property Value
            With udtKeypadHWD
                .strEncData = ""
                .strHideData = ""
                .strRawData = ""
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
                .iDeviceState = DST_STARTPINPADSTATE
                .strEventType = EVTTYPE_TIMEOUT
                .iDeviceTrace = ReturGenIDeviceTrace
                If udtKeypadHWD.blnEncMode = True Then
                    .mDeviceDataValue = udtKeypadHWD.strEncData
                Else
                    .mDeviceDataValue = udtKeypadHWD.strRawData
                End If
            End With

            'Raise the Events
            RaiseEvent EvtDeviceTimeout(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("Raise event in Keypad: EvtDeviceTimeout")
            Return True

        Catch ex As Exception
            AppLogErr("Error in KeypadDeviceTimeout:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function KeypadEnterButtonClicked() As Boolean
        Try

            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PINPAD_SUCCESS
            strProErrSeverity = ERST_KEYPAD_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = ""

            'Set the Property Value
            With udtKeypadHWD
                .strEncData = RawData
                .strHideData = ""
                .strRawData = ""
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
                .iDeviceState = DST_STARTPINPADSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturGenIDeviceTrace
                .mDeviceDataValue = udtKeypadHWD.strEncData
            End With

            'Raise the Events
            RaiseEvent EnterClick(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("Raise event in Keypad: EnterClick")
            Return True

        Catch ex As Exception
            AppLogErr("Error in KeypadEnterButtonClicked:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function KeypadClearButtonClicked() As Boolean
        Try

            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PINPAD_SUCCESS
            strProErrSeverity = ERST_KEYPAD_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = ""

            'Set the Property Value
            With udtKeypadHWD
                .strEncData = ""
                .strHideData = ""
                .strRawData = strProSupplyStatus
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
                .iDeviceState = DST_STARTPINPADSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturGenIDeviceTrace
                .mDeviceDataValue = RawData
            End With

            'Raise the Events
            RaiseEvent ClearClick(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("Raise event in Keypad: ClearClick")
            Return True

        Catch ex As Exception
            AppLogErr("Error in KeypadEnterButtonClicked:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function KeypadCancelButtonClicked() As Boolean
        Try

            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PINPAD_SUCCESS
            strProErrSeverity = ERST_KEYPAD_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = ""

            'Set the Property Value
            With udtKeypadHWD
                .strEncData = ""
                .strHideData = ""
                .strRawData = RawData
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
                .iDeviceState = DST_STARTPINPADSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturGenIDeviceTrace
                .mDeviceDataValue = RawData
            End With

            'Raise the Events
            RaiseEvent CancelClick(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("Raise event in Keypad: CancelClick")
            Return True

        Catch ex As Exception
            AppLogErr("Error in KeypadCancelButtonClicked:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function KeypadNumericButtonClicked() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 1
            strProTxnStatus = TXST_PINPAD_SUCCESS
            strProErrSeverity = ERST_KEYPAD_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = ""

            'Set the Property Value
            With udtKeypadHWD
                .strEncData = RawData
                .strHideData = ""
                .strRawData = RawData
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
                .iDeviceState = DST_STARTPINPADSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturGenIDeviceTrace


                If EncryptMode = True Then
                    .mDeviceDataValue = udtKeypadHWD.strEncData
                    AppLogInfo(" KeypadNumericButtonClicked MEDIADATA: EvtDeviceDataReady Value:" & udtKeypadHWD.strRawData)

                Else
                    .mDeviceDataValue = udtKeypadHWD.strRawData
                    AppLogInfo(" KeypadNumericButtonClicked MEDIADATA: EvtDeviceDataReady Value:" & udtKeypadHWD.strRawData)

                End If

            End With

            'Raise the Events
            RaiseEvent NumericClick(udtDeviceSender, udtEventDeviceArgs)
            'RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("Raise event in Keypad: EvtDeviceDataReady")

            Return True

        Catch ex As Exception
            AppLogErr("Error in KeypadEnterButtonClicked:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function KeypadHexKeyButtonClicked() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 1
            strProTxnStatus = TXST_PINPAD_SUCCESS
            strProErrSeverity = ERST_KEYPAD_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = ""

            'Set the Property Value
            With udtKeypadHWD
                .strEncData = ""
                .strHideData = ""
                .strRawData = RawData
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
                .iDeviceState = DST_STARTPINPADSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturGenIDeviceTrace
                .mDeviceDataValue = udtKeypadHWD.strRawData


            End With

            'Raise the Events
            RaiseEvent HexKeyClick(udtDeviceSender, udtEventDeviceArgs)
            'RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("Raise event in HexKey Keypad: EvtDeviceDataReady")

            Return True

        Catch ex As Exception
            AppLogErr("Error in KeypadEnterButtonClicked:" & ex.Message)
            Return False
        End Try
    End Function

    Private Sub objKeypad_KeypadClickClearButton() Handles objKeypad.KeypadClickClearButton
        Try
            RawData = ""
            If KeypadClearButtonClicked() Then
                'RaiseEvent KeypadPress("B")
                'objAppLogFunction.AppLogInfo("Raise Event in ClearClick")
            End If
        Catch ex As Exception
            AppLogErr("Error in ClearClick:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypad_KeypadClickEnterButton() Handles objKeypad.KeypadClickEnterButton
        Try
            blnTimerStop = True
            KeypadEnterButtonClicked()

        Catch ex As Exception
            AppLogErr("Error in EnterClick:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypad_KeypadClickCancelButton() Handles objKeypad.KeypadClickCancelButton
        Try
            RawData = ""
            'objKeypadHWD.Send_CmdStopClearTextMode()
            'blnTimerStop = True
            If KeypadCancelButtonClicked() Then
                'objAppLogFunction.AppLogInfo("Raise Event in CancelClick")
            End If
        Catch ex As Exception
            AppLogErr("Error in CancelClick:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypad_KeypadClickUnknownButton(ByVal keyInput As String) Handles objKeypad.KeypadClickUnknownButton
        Try
            RawData &= String.Empty
            RaiseEvent KeypadPress(keyInput)
        Catch ex As Exception
            AppLogErr("Error in objKeypad_KeypadClickUnknownButton:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypad_KeyPadInput(ByVal keyInput As String) Handles objKeypad.KeyPadInput
        Try
            RawData &= keyInput
            KeypadNumericButtonClicked()
            RaiseEvent KeypadPress(keyInput)

        Catch ex As Exception
            AppLogErr("Error in objKeypad_KeyPadInput:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypad_KeyPadHexInput(ByVal keyInput As String) Handles objKeypad.KeyPadHexInput
        Dim bytVariable(3) As Byte
        objSagemEPP = New clsKeypad.clsEPPKeypad

        Try
            intStartCount += 1
            keyInput = "*"
            RawData &= keyInput
            KeypadHexKeyButtonClicked()
            RaiseEvent KeypadPress(keyInput)

            If intStartCount = 32 Then
                If ControlKey = "A" Then
                    'timerSupervisorKey.Interval = 2000
                    'timerSupervisorKey.Start()

                ElseIf ControlKey = "B" Then
                    'timerSupervisorKey.Interval = 2000
                    'timerSupervisorKey.Start()
                End If

                intStartCount = 0
            End If
        Catch ex As Exception
            AppLogErr("Error in objKeypad_KeyPadHexInput:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypad_KeypadClickEncryptNumericButton(ByVal keyInput As String) Handles objKeypad.KeypadClickEncryptNumericButton
        Try

            If intStartCount >= MaxLength Then
                'timer.Start()
                'timer.Interval = 5000
            Else
                intStartCount += 1
                keyInput = "N"
                RawData &= keyInput
                KeypadNumericButtonClicked()
                RaiseEvent KeypadPress(keyInput)

                If intStartCount = MaxLength Then
                    intStartCount = 0
                    'timer.Start()
                    'timer.Interval = 1000
                End If

            End If
        Catch ex As Exception
            AppLogErr("Error in objKeypad_KeypadClickEncryptNumericButton:" & ex.Message)
        End Try
    End Sub

    Private Sub objKeypad_KeypadTimeout() Handles objKeypad.KeypadTimeout
        Try
            'If objKeypadHWD.Send_CmdStopClearMode() Then
            RaiseEvent Timeout()
            AppLogInfo("Raise Event in EvtDeviceTimeout")

            If KeypadDeviceTimeout() Then
                'objAppLogFunction.AppLogInfo("Raise Event in EvtDeviceTimeout")
            End If
            'End If
        Catch ex As Exception
            AppLogErr("Error in KeypadTimeout:" & ex.Message)
        End Try
    End Sub
#End Region

    'Private Sub timer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer.Elapsed
    '    Dim strTempPINBlock As String = ""

    '    Try
    '        timer.Stop()

    '        'read pin from Keypad
    '        'If objKeypadHWD.Send_CmdStopEncTextMode() Then

    '        '    If objEPPController.ReadPIN(strTempPINBlock) Then
    '        '        'read pin
    '        '        RawData = strTempPINBlock

    '        '        KeypadEnterButtonClicked()
    '        '    End If

    '        'End If

    '        'read pin from HSM
    '        RawData = "Encrypted PIN Block"
    '        objKeypadHWD.Send_CmdStopEncTextMode()
    '        KeypadEnterButtonClicked()
    '    Catch ex As Exception
    '        AppLogErr("Error in timer_Elapsed:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub timerEvent_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timerEvent.Elapsed
    '    Try
    '        'timerEvent.Stop()
    '        KeypadDeviceDataReady()
    '    Catch ex As Exception
    '        AppLogErr("Error in timerEvent_Elapsed:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub timerSupervisorKey_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timerSupervisorKey.Elapsed


    '    Try
    '        Dim bytVariable(3) As Byte

    '        timerSupervisorKey.Stop()

    '        If ControlKey = "A" Then

    '            bytVariable(0) = "75"
    '            bytVariable(1) = "80"
    '            bytVariable(2) = "95"
    '            bytVariable(3) = "49"

    '            If objSagemEPP.CMD_STOP_KEY(bytVariable) Then

    '                If objSagemEPP.CMD_CHECK_SUM("KP_1", String.Empty) Then
    '                    RawData = String.Empty
    '                    RawData &= objSagemEPP.GetCheckSumedData
    '                    KeypadDeviceDataReady()

    '                    MsgBox("Register " & ControlKey & " Key OK")
    '                End If

    '            Else
    '                MsgBox("Register " & ControlKey & " Key failed")
    '            End If

    '        ElseIf ControlKey = "B" Then

    '            bytVariable(0) = "75"
    '            bytVariable(1) = "80"
    '            bytVariable(2) = "95"
    '            bytVariable(3) = "50"

    '            If objSagemEPP.CMD_STOP_KEY(bytVariable) Then

    '                If objSagemEPP.CMD_CHECK_SUM("KP_2", String.Empty) Then
    '                    RawData = String.Empty
    '                    RawData &= objSagemEPP.GetCheckSumedData
    '                    KeypadDeviceDataReady()

    '                    MsgBox("Register " & ControlKey & " Key OK")
    '                End If

    '            Else
    '                MsgBox("Register " & ControlKey & " Key failed")
    '            End If
    '        End If

    '    Catch ex As Exception
    '        AppLogErr("Error in timerSupervisorKey_Elapsed:" & ex.Message)
    '    End Try
    'End Sub




End Class
