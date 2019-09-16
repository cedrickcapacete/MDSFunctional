Imports System.ServiceProcess

Imports clsReceiptPrinter

Imports clsHWDGlobalInterface
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD


Public Class clsHardwareLayer

#Region "Hardware Object - Variable "

    'Receipt Printer Hardware object
    Public WithEvents objReceiptPrinterHWD As New clsReceiptPrinter.clsReceiptPrinterControl
    Public objReceiptPrinterLookupData As New clsReceiptPrinter.clsReceiptPrinterControl


#End Region

#Region "Printer Receipt Events"

    Private udtDeviceSender As DeviceSender
    Private udtEventDeviceArgs As EventDeviceArgs
    Private udtDeviceError As device_error

    'Printer Receipt Events  
    Public Event EvtDeviceDataReady(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)

#End Region


#Region "Form Variable"

    Structure ReceiptPrinterHWDSTR
        Dim strReceiptData As String
        Dim strImageFileName As String
        'Dim strDeviceStatus As Integer
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure

    Public udtReceiptPrinterHWD As ReceiptPrinterHWDSTR

#End Region



#Region "Property Tmp Variable and Control"

    Private strProDeviceStatus As Integer = 0
    Private strProTxnStatus As String = String.Empty
    Private strProErrSeverity As String = String.Empty
    Private strProDignosticStatus As String = String.Empty
    Private strProSupplyStatus As String = String.Empty

#End Region

#Region "Receipt Printer Const Value"

    Private PrinterReceiptDeviceGraphicId As String = ""
    Private PrinterReceiptDeviceName As String = ""

    Public Sub initReceiptPrinterConst()
        Try
            PrinterReceiptDeviceGraphicId = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.DeviceGraphicID
            PrinterReceiptDeviceName = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.DeviceName

        Catch ex As Exception
            AppLogErr("Error in initReceiptPrinterConst()" & ex.Message)
        End Try
    End Sub
#End Region



#Region "Printer Receipt Property Value"

    'TxnStatus - TXST
    '1 - Successful Print
    '2 - Print Operation Not Successfully Completed
    '3 - Device Not Configure
    '4 - Cancel key pressed during sisdeways receipt print
    Private TXST_PRINT_SUCCESS As String = ""
    Private TXST_PRINT_NOT_COMPLETE As String = ""
    Private TXST_PRINT_DEVICE_NOT_CFG As String = ""
    Private TXST_PRINT_CANCEL_SIDEWAY As String = ""

    'ErrorSeverity - ERST
    Private ERST_PRINTER_OK As String = ""
    Private ERST_PRINTER_ERROR As String = ""
    Private ERST_PRINTER_WARNING As String = ""
    Private ERST_PRINTER_FATAL As String = ""

    'Diagnostic Status -DGST
    Private DGST_MSTATUS As String = ""
    Private DGST_MSTATUSDATA As String = ""

    'SupplyStatus - SYST  
    'Private Const SYST_STATUS As String = ""

    Public Sub initReceiptPrinterProperty()
        Try
            TXST_PRINT_SUCCESS = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.TxnStatusPrtSuccess
            TXST_PRINT_NOT_COMPLETE = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.TxnStatusPrtNotComplete
            TXST_PRINT_DEVICE_NOT_CFG = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.TxnStatusDeviceNotCfg
            TXST_PRINT_CANCEL_SIDEWAY = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.TxnStatusCancelSideway

            ERST_PRINTER_OK = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.ErrSvrtyPrtOk
            ERST_PRINTER_ERROR = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.ErrSvrtyPrtError
            ERST_PRINTER_WARNING = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.ErrSvrtyPrtWarning
            ERST_PRINTER_FATAL = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.ErrSvrtyPrtFatal

            DGST_MSTATUS = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.MStatus
            DGST_MSTATUSDATA = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.MStatusData

        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "EventDeviceArgs value iDeviceState,strEventYype"

#Region "Printer Receipt Const Device State - iDeviceState"

    'Value for iDeviceState
    Private DST_PRINTSTATE As String = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.DeviceStatePrint ' Print 
    Private DST_NOPRINTSTATE As String = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.DeviceStateNoPrint ' No Print

    Public Sub initDeviceState()
        Try
            DST_PRINTSTATE = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.DeviceStatePrint
            DST_NOPRINTSTATE = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.DeviceStateNoPrint

        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Printer Receipt Const Event Type - strEvenType"

    'Value for strEvenType
    Private EVTTYPE_DEVICEERROR As String = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.EvtDeviceErr ' Print Failed
    Private EVTTYPE_DEVICEWRAP As String = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.EvtDeviceWrap ' Print Wrap
    Private EVTTYPE_DATAARRIVED As String = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.EvtDeviceReady ' Print Complete


    Public Sub initEventType()
        Try
            EVTTYPE_DEVICEERROR = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.EvtDeviceErr ' Print Failed
            EVTTYPE_DEVICEWRAP = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.EvtDeviceWrap ' Print Wrap
            EVTTYPE_DATAARRIVED = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.EvtDeviceReady ' Print Complete

        Catch ex As Exception

        End Try
    End Sub
#End Region

#End Region



#Region "Instatnce Control"

    Private m_singleInstance As clsHardwareLayer = Nothing

    Public Function SingleInstance() As clsHardwareLayer
        If m_singleInstance Is Nothing Then
            m_singleInstance = New clsHardwareLayer()
        End If
        Return m_singleInstance
    End Function

    Protected Overrides Sub finalize()
        If m_singleInstance IsNot Nothing Then
            m_singleInstance = Nothing
        End If
    End Sub

#End Region


#Region "New Class"

    Public Sub New()
        Try

            If InitReceiptPrinterHWD() = True Then
                'Init the Hwd Object and the Log File
                If objReceiptPrinterHWD.InitRECEIPTPRINTERControl = True Then
                    'Init the Receipt Printer comport setting
                    If objReceiptPrinterHWD.ReadRECEIPTPRINTERHWDCFG = True Then

                        'Init Receip Printer Lookup Data
                        initDeviceState()
                        initEventType()
                        initReceiptPrinterConst()
                        initReceiptPrinterProperty()

                        'Else
                        '    blnRtrn = False
                        '    AppLogErr("StartDevice - Read Receipt Printer Hardware Setting Failed")
                    End If
                    'Else
                    '    blnRtrn = False
                    '    AppLogErr("StartDevice - Init Receipt Printer Hwd Failed")
                End If
                'Else
                '    blnRtrn = False
                '    'AppLogErr("Read Receipt Printer Init Logger Failed")
            End If

        Catch ex As Exception
        End Try
    End Sub

#End Region



#Region "Support Property"

    ReadOnly Property ReceiptPrinterName() As String
        Get
            Return objReceiptPrinterHWD.HwdReceiptPrinterName
        End Get
    End Property

#End Region


#Region "Method: StartDevice, StopDevice, WrapDevice"

    Public Function InitReceiptPrinterHWD() As Boolean
        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppINI.ReadAppLayerINICFGFile(HWDLAYERINIPATH, THERMALPRINTER) = True Then

                'Read INI File
                'Log Ini File
                '1.UPS Setting
                '2.UPS Lookup Data

                With objAppINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "Printer Hookup Class Init Ok"
                AppLogInfo(strLogEvt)

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in InitReceiptPrinterHWD:" & ex.Message)
            Return False
        End Try
    End Function


    Public Function StartDevice() As Boolean
        Dim blnRtrn As Boolean = False

        Try

            'Check Printer Spooler Service is Started or Stopped
            If PrinterIsStopped() = False Then
                blnRtrn = True
                'Init the Receipt Printer Communication Port
                If objReceiptPrinterHWD.InitRECEIPTPRINTERHWD = True Then
                    blnRtrn = True
                    If WrapDevice() = True Then
                        udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_SUCCESS
                        ReceiptPrinterGetStatus()
                        'strGeniDeviceTrace = ""
                        AppLogInfo("StartDevice - WrapDeive Success")
                    Else
                        blnRtrn = False
                        AppLogWarn("StartDevice - WrapDeive Failed")
                    End If
                Else
                    ReceiptPrinterGetStatus()
                    'ReceiptPrinterDeviceError()
                    blnRtrn = False
                    AppLogWarn("StartDevice - Init Receipt Printer Printer Device Failed")
                End If
            Else
                ReceiptPrinterServiceIsStop()
                blnRtrn = False
                AppLogWarn("StartDevice - Priter Spooler service is not running. Please restart PC")
            End If
            'Reply Status
            Return blnRtrn
        Catch ex As Exception
            AppLogErr("Error in StartDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Try
            AppLogInfo("StopDevice")
            'End Logger
            CloseLog()
            Return True
        Catch ex As Exception
            AppLogErr("Error in StopDevice:" & ex.Message)
            'End Logger
            CloseLog()
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean

        Try
            If ReceiptData.Length > 0 Then
                'objCustomPrinter.SetCurrentY(1)
                DoPrintJob()
            End If

            'General Property - 'clean the data
            ReceiptData = ""
            ReceiptImg = ""


            'If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = True Then
            strProDeviceStatus = 0
            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = ""

            With udtReceiptPrinterHWD
                .strReceiptData = ""
                .strImageFileName = ""
                '.strDeviceStatus = 0
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
                .strMStatusData = ""
            End With

            'Receipt Printer EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = PrinterReceiptDeviceGraphicId
                .strDeviceName = PrinterReceiptDeviceName
            End With

            'Receipt Printer EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = PrinterReceiptDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = "0" 'DST_NOPRINTSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DEVICEWRAP
                .mDeviceDataValue = ""
            End With

            'Log Info
            AppLogInfo("WrapDevice Success")

            Return True


        Catch ex As Exception
            AppLogErr("Error in WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function


    Public Function LockDevice() As Boolean
        Try
            If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = True Then
                AppLogInfo("LockDevice Success")
                blnLockPrinter = True
                Return True
            Else
                AppLogInfo("LockDevice Failed")
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in LockDevice:" & ex.Message)

            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Try
            If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = True Then
                AppLogInfo("UnlockDevice Success")
                blnLockPrinter = False
                Return True
            Else
                AppLogInfo("UnlockDevice Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in UnLockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal strDeviceState As Integer) As Boolean
        Try
            If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = False Then
                blnLockPrinter = False
                AppLogInfo("WakeUpDevice Success")
                Return True
            Else
                AppLogInfo("WakeUpDevice Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in WakeUpDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        objCustomPrinter = New clsReceiptPrinter.clsCustomPrinter

        Try
            If PrinterIsStopped() = False Then
                If objCustomPrinter.InitPrinterConnection = True Then
                    If objCustomPrinter.PrinterGetStatus = True Then
                        udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_SUCCESS
                        If ReceiptPrinterGetStatus() = True Then
                            AppLogInfo("DiagnosticDevice- Printer Ok")
                            Return True
                        Else
                            AppLogWarn("DiagnosticDevice- Printer Status Error")
                            Return False
                        End If
                    Else
                        AppLogWarn("DiagnosticDevice- Printer Get Status Failed")
                        udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_NOT_COMPLETE
                        Return False
                    End If
                Else
                    AppLogWarn("DiagnosticDevice- Printer Not Init")
                    udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_DEVICE_NOT_CFG
                    Return False
                End If
            Else
                AppLogWarn("DiagnosticDevice- Printer Sploorer is Stop")
                ReceiptPrinterServiceIsStop()
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in DiagnosticDevice:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "Property - TxnStatus,ErrorSeverity,SupplyStatus"

    Property TxnStatus() As String
        Get
            Return udtReceiptPrinterHWD.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtReceiptPrinterHWD.strTxnStatus = value
        End Set
    End Property


    Property ErrorSeverity() As String
        Get
            Return udtReceiptPrinterHWD.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtReceiptPrinterHWD.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtReceiptPrinterHWD.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtReceiptPrinterHWD.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtReceiptPrinterHWD.strMStatus
        End Get
        Set(ByVal value As String)
            udtReceiptPrinterHWD.strMStatus = value
        End Set
    End Property

    Property MStatusData() As String
        Get
            Return udtReceiptPrinterHWD.strMStatusData
        End Get
        Set(ByVal value As String)
            udtReceiptPrinterHWD.strMStatusData = value
        End Set
    End Property

    Property udtReplyReceiptPrinterHWD() As ReceiptPrinterHWDSTR
        Get
            Return udtReceiptPrinterHWD
        End Get
        Set(ByVal value As ReceiptPrinterHWDSTR)
            udtReceiptPrinterHWD = value
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

    Property udtReplyDeviceSender() As clsHWDGlobalInterface.clsGolbalVar.DeviceSender
        Get
            Return udtDeviceSender
        End Get
        Set(ByVal value As clsHWDGlobalInterface.clsGolbalVar.DeviceSender)
            udtDeviceSender = value
        End Set
    End Property

#End Region


#Region "Property - Receipt Printer"

    Property ReceiptDataToBePrint() As String
        Get
            Return udtReceiptPrinterHWD.strReceiptData
        End Get
        Set(ByVal value As String)
            udtReceiptPrinterHWD.strReceiptData = value
        End Set
    End Property

    Property ReceiptImageToBePrint() As String
        Get
            Return udtReceiptPrinterHWD.strImageFileName
        End Get
        Set(ByVal value As String)
            udtReceiptPrinterHWD.strImageFileName = value
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


#Region "Hardware Layer Event "

    Private Function ReceiptPrinterDeviceDataReady() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PRINT_SUCCESS
            strProErrSeverity = ERST_PRINTER_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.SufficientPaper & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK '1111

            'Set the Property Value
            With udtReceiptPrinterHWD
                .strReceiptData = ""  'Device Value
                .strImageFileName = ""
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
                .iDeviceState = "1" 'DST_PRINTSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturGenIDeviceTrace
                .mDeviceDataValue = ReceiptData & vbCrLf & ReceiptImg ' Device Value
            End With

            ReceiptData = ""
            ReceiptImg = ""

            'Raise the Events
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("RaiseEvent EvtDeviceDataReady")
            Return True
        Catch ex As Exception
            AppLogErr("Error in ReceiptPrinterDeviceDataReady:" & ex.Message)
            Return False
        End Try

    End Function

    Private Function ReceiptPrinterGetStatus() As Boolean
        Dim strPrinterStatus As String = String.Empty

        Try

            With objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo
                AppLogInfo("Printer Status:" & .blnPrinterStatus)
                AppLogInfo("Printer Failed:" & .blnPrinterFail)
                AppLogInfo("Paper OK:" & .blnPaperOK)
                AppLogInfo("Paper Low:" & .blnPaperLow)
                AppLogInfo("Paper End:" & .blnPaperEnd)
                AppLogInfo("Paper Jam:" & .blnPaperJam)

                strPrinterStatus = .blnPrinterStatus & "|" & .blnPrinterFail & "|" & .blnPaperOK & "|" & .blnPaperLow & "|" & .blnPaperEnd & "|" & .blnPaperJam

            End With

            With udtReceiptPrinterHWD
                .strTxnStatus = TxnStatus

                If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperOK = True Then
                    .strErrSeverity = ERST_PRINTER_OK
                    .strSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.SufficientPaper & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK  '1111
                ElseIf objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperLow = True Then
                    .strErrSeverity = ERST_PRINTER_WARNING
                    .strSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PaperLow & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK  '2111
                ElseIf objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperEnd = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperJam = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = False Then
                    .strErrSeverity = ERST_PRINTER_FATAL
                    .strSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PaperExh & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK  '3111
                End If

                .strMStatus = MStatus
                .strMStatusData = MStatusData
            End With

            If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterFail = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperEnd = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperJam = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperLow = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = False Then

                'Device Sender
                'With udtDeviceSender
                '    .strDeviceGraphicId = udtReplyDeviceSender.strDeviceGraphicId
                '    .strDeviceName = udtReplyDeviceSender.strDeviceName
                'End With

                udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_DEVICE_NOT_CFG

                'Device Error
                With udtDeviceError
                    '.Status = DeviceStatus
                    .TxnStatus = TxnStatus
                    .ErrorSeverity = ErrorSeverity
                    .SupplyStatus = SupplyStatus
                    .MStatus = DGST_MSTATUS
                    .MStatus = DGST_MSTATUSDATA
                End With

                'Event Device Args
                With udtEventDeviceArgs
                    .iDeviceState = "0" 'DST_NOPRINTSTATE
                    .strEventType = EVTTYPE_DEVICEERROR
                    .iDeviceTrace = ReturGenIDeviceTrace
                    .mDeviceDataValue = strPrinterStatus  'udtReplyDeviceError
                End With

                'Raise the Events
                RaiseEvent EvtDeviceError(udtReplyDeviceSender, udtEventDeviceArgs)
                AppLogInfo(" RaiseEvent EvtDeviceError")

                Return False

            Else
                'Paper OK 
                Return True
            End If

         
        Catch ex As Exception
            AppLogErr("Error in ReceiptPrinterGetStatus:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function ReceiptPrinterDeviceError() As Boolean
        Dim blnResult As Boolean
        Dim strPrinterStatus As String = String.Empty

        Try

            With objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo
                AppLogInfo("Printer Status:" & .blnPrinterStatus)
                AppLogInfo("Printer Failed:" & .blnPrinterFail)
                AppLogInfo("Paper OK:" & .blnPaperOK)
                AppLogInfo("Paper Low:" & .blnPaperLow)
                AppLogInfo("Paper End:" & .blnPaperEnd)
                AppLogInfo("Paper Jam:" & .blnPaperJam)

                strPrinterStatus = .blnPrinterStatus & "|" & .blnPrinterFail & "|" & .blnPaperOK & "|" & .blnPaperLow & "|" & .blnPaperEnd & "|" & .blnPaperJam

            End With




            If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = True Then
                If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterFail = True Or objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperJam = True Then

                    'Value to Set for NDC
                    strProDeviceStatus = 0
                    strProTxnStatus = TXST_PRINT_DEVICE_NOT_CFG
                    strProErrSeverity = ERST_PRINTER_FATAL
                    strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
                    strProSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PaperExh & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK  '3111

                ElseIf objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperLow = True Then
                    'Value to Set for NDC
                    strProDeviceStatus = 0
                    strProTxnStatus = TXST_PRINT_NOT_COMPLETE
                    strProErrSeverity = ERST_PRINTER_WARNING
                    strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
                    strProSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PaperLow & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK  '2111


                ElseIf objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPaperEnd = True Then
                    'Value to Set for NDC
                    strProDeviceStatus = 0
                    strProTxnStatus = TXST_PRINT_NOT_COMPLETE
                    strProErrSeverity = ERST_PRINTER_FATAL
                    strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
                    strProSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PaperExh & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK '3111
                End If

                'Set the Property Value
                With udtReceiptPrinterHWD
                    .strReceiptData = ""
                    .strImageFileName = ""
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
                    .iDeviceState = "0" 'DST_NOPRINTSTATE
                    .strEventType = EVTTYPE_DEVICEERROR
                    .iDeviceTrace = ReturGenIDeviceTrace
                    .mDeviceDataValue = strPrinterStatus 'udtReplyDeviceError
                End With

                'Raise the Events
                RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
                blnResult = True

            End If

            If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = False Then
                'Value to Set for NDC
                strProDeviceStatus = 0
                strProTxnStatus = TXST_PRINT_DEVICE_NOT_CFG
                strProErrSeverity = ERST_PRINTER_ERROR
                strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
                strProSupplyStatus = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PaperExh & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.RibbonOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.PrintHeadOK & objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.KnifeOK '3111


                'Set the Property Value
                With udtReceiptPrinterHWD
                    .strReceiptData = ""
                    .strImageFileName = ""
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
                    .MStatus = MStatus
                    .MStatus = MStatusData
                End With

                'Device Sender
                With udtDeviceSender
                    .strDeviceGraphicId = udtReplyDeviceSender.strDeviceGraphicId
                    .strDeviceName = udtReplyDeviceSender.strDeviceGraphicId
                End With


                'Event Device Args
                With udtEventDeviceArgs
                    .iDeviceState = "0" 'DST_NOPRINTSTATE
                    .strEventType = EVTTYPE_DEVICEERROR
                    .iDeviceTrace = strGeniDeviceTrace
                    .mDeviceDataValue = strPrinterStatus 'udtReplyDeviceError
                End With

                'Raise the Events
                RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
                AppLogInfo("RaiseEvent EvtDeviceError")
                blnResult = True
            End If

            Return blnResult

        Catch ex As Exception
            AppLogErr("Error in ReceiptPrinterDeviceError:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function PrinterIsStopped() As Boolean
        Try
            Dim service As New ServiceController("Print Spooler")
            Dim status As ServiceControllerStatus = service.Status
            service.Dispose()
            If status = ServiceControllerStatus.Stopped Or status = ServiceControllerStatus.Paused Then
                'AppLogInfo("Printer Spooler is Stopped or Paused")
                Return True
            Else
                'AppLogInfo("Printer Spooler Status Ok")
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error occurred at PrinterIsStopped: " & ex.Message)
            Return False
        End Try

    End Function

    Private Function ReceiptPrinterServiceIsStop() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_PRINT_DEVICE_NOT_CFG
            strProErrSeverity = ERST_PRINTER_ERROR
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = ""

            'Set the Property Value
            With udtReceiptPrinterHWD
                .strReceiptData = ""
                .strImageFileName = ""
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
                .strDeviceGraphicId = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.DeviceGraphicID
                .strDeviceName = objReceiptPrinterLookupData.RECEIPTPRINTERLOOKUPDATAInfo.DeviceName
            End With

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = "0" 'DST_NOPRINTSTATE
                .strEventType = EVTTYPE_DEVICEERROR
                .iDeviceTrace = strGeniDeviceTrace
                .mDeviceDataValue = "99" 'udtReplyDeviceError
            End With

            'Raise the Events
            RaiseEvent EvtDeviceError(udtReplyDeviceSender, udtEventDeviceArgs)
            AppLogInfo("RaiseEvent EvtDeviceError")
            Return True


        Catch ex As Exception
            AppLogErr("Error in ReceiptPrinterServiceIsStop:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function PrintText(ByVal ReceiptStr As String, ByVal CutPaper As Boolean) As Boolean

        Try
            If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = True And blnLockPrinter = False Then
                If CutPaper = True Then
                    If ReceiptData.Length = 0 Then
                        ReceiptData = ReceiptStr
                    Else
                        ReceiptData &= "|" & ReceiptStr
                    End If
                    udtReceiptPrinterHWD.strReceiptData = ReceiptData
                    DoPrintJob()
                    'ReceiptData = ""
                Else
                    If ReceiptData.Length = 0 Then
                        ReceiptData = ReceiptStr
                    Else
                        ReceiptData &= "|" & ReceiptStr
                    End If

                    udtReceiptPrinterHWD.strReceiptData = ReceiptData
                End If
                Return True

            Else
                AppLogErr("Printer is Locked")
                Return False
            End If


        Catch ex As Exception
            AppLogErr("Error in PrintTextFunc:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function PrintImage(ByVal ImgFilName As String, ByVal CutPaper As Boolean) As Boolean
        Try
            If objReceiptPrinterHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = True And blnLockPrinter = False Then
                If ImgFilName.Length > 0 Then
                    If CutPaper = True Then
                        If ReceiptData.Length = 0 Then
                            ReceiptData = ImgFilName
                        Else
                            ReceiptData &= "|" & ImgFilName
                        End If

                        udtReceiptPrinterHWD.strReceiptData = ReceiptData
                        DoPrintJob()

                    Else
                        If ReceiptData.Length = 0 Then
                            ReceiptData = ImgFilName
                        Else
                            ReceiptData &= "|" & ImgFilName
                        End If
                        udtReceiptPrinterHWD.strReceiptData = ReceiptData
                    End If
                    Return True

                Else
                    Return False
                End If

            Else
                AppLogErr("Printer is Locked")
                Return False
            End If


        Catch ex As Exception
            AppLogErr("Error in PrintImage():" & ex.Message)
            Return False
        End Try
    End Function

    Private Function DoPrintJob() As Boolean
        Dim udtReplyReceiptPrinterHWDSTR As clsReceiptPrinter.clsReceiptPrinterControl.ReplyReceiptPrinterHWDSTR
        Dim blnResult As Boolean

        Try
            udtReceiptPrinterHWD.strTxnStatus = TXST_PRINT_SUCCESS
            udtReceiptPrinterHWD.strErrSeverity = ERST_PRINTER_OK

            With udtReplyReceiptPrinterHWDSTR
                .strReceiptData = ReceiptDataToBePrint
                .strImageFileName = ReceiptImageToBePrint
                '.strDeviceStatus = DeviceStatus
                .strTxnStatus = TxnStatus
                .strErrSeverity = ErrorSeverity
                .strMStatus = MStatus
                .strSupplyStatus = SupplyStatus
            End With

            With udtReceiptPrinterHWD
                '.strDeviceStatus = DeviceStatus
                .strTxnStatus = TxnStatus
                .strErrSeverity = ErrorSeverity
                .strMStatus = MStatus
                .strSupplyStatus = SupplyStatus
            End With

            If objReceiptPrinterHWD.StartPrint(udtReplyReceiptPrinterHWDSTR) = True Then
                ReceiptPrinterDeviceDataReady()
                blnResult = True
            Else
                blnResult = False
            End If

            Return blnResult

        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region

End Class

