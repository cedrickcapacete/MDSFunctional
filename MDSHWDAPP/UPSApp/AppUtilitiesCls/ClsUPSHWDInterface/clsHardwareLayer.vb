'Imports System.ServiceProcess
Imports clsHWDGlobalInterface
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD
Imports ClsUPSHWDInterface.clsAppStructure

Public Class clsHardwareLayer

#Region "Hardware Object - Variable "

    Public WithEvents TimeChecking As New Timers.Timer

#End Region

#Region "UPS Events - EvtDeviceDataReady,EvtDeviceError,EvtDeviceTimeOut"

    Private udtDeviceSender As DeviceSender
    Private udtEventDeviceArgs As EventDeviceArgs
    Private udtDeviceError As device_error

    'UPS Events  
    Public Event EvtDeviceDataReady(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)

    'UPS Support Events
    Public Event EvtCheckUPSBatteryMode(ByVal blnFlag As Boolean)

#End Region

#Region "New"

    Public Sub New()
        Try

            'Init the Logger
            If InitclsHWDLogger() Then
                If InitUPSControl() = True Then

                    'Init UPS Lookup Data
                    initDeviceState()
                    initEventType()
                    initUPSConst()
                    initUPSProperty()

                    blnLockUPS = False

                    'Else
                    '    AppLogWarn("UPS StartDevice - Read UPS Hardware CFG Failed")
                    '    Return False
                End If
                'Else
                '    AppLogWarn("UPS StartDevice - Init UPS Hardware Failed")
                '    Return False
            End If

        Catch ex As Exception
        End Try
    End Sub

#End Region


#Region "Form Variable"

    'Property Tmp Variable and Control
    Private strProDeviceStatus As Integer = 0
    Private strProTxnStatus As String = String.Empty
    Private strProErrSeverity As String = String.Empty
    Private strProDignosticStatus As String = String.Empty
    Private strProSupplyStatus As String = String.Empty

    Private UPSDeviceGraphicId As String = ""
    Private UPSDeviceName As String = ""


    'TxnStatus - TXST
    '0 - UPS AC Power OK
    '1 - UPS Battery Mode

    Private TXST_UPS_ACPOWER_MODE As String = ""
    Private TXST_UPS_BATTERY_MODE As String = ""
    'Private TXST_PRINT_DEVICE_NOT_CFG As String = ""
    'Private TXST_PRINT_CANCEL_SIDEWAY As String = ""

    'ErrorSeverity - ERST
    Private ERST_UPS_OK As String = ""
    Private ERST_UPS_ERROR As String = ""
    Private ERST_UPS_WARNING As String = ""
    Private ERST_UPS_FATAL As String = ""

    'Diagnostic Status -DGST
    Private DGST_MSTATUS As String = ""
    Private DGST_MSTATUSDATA As String = ""

    'SupplyStatus - SYST  
    'Private Const SYST_STATUS As String = ""

    'Value for iDeviceState
    Private DST_POWERSUPPLYSTATE As String = "" ' Power Restore 
    Private DST_BATTERYSTATE As String = "" ' Battery Mode

    'Value for strEvenType
    Private EVTTYPE_DEVICEERROR As String = ""
    Private EVTTYPE_DEVICEWRAP As String = ""
    Private EVTTYPE_DATAARRIVED As String = ""


#End Region

#Region "UPS Const Device State - iDeviceState"

    Public Sub initUPSConst()
        Try
            With udtUPSLOOKUPDATACFG
                UPSDeviceGraphicId = .DeviceGraphicID
                UPSDeviceName = .DeviceName
            End With
        Catch ex As Exception
            AppLogErr("Error in clsUPSHWDInterface.initUPSConst:" & ex.Message)
        End Try
    End Sub

    Public Sub initUPSProperty()
        Try
            With udtUPSLOOKUPDATACFG
                TXST_UPS_ACPOWER_MODE = .TxnStatusACPowerMode
                TXST_UPS_BATTERY_MODE = .TxnStatusBatteryMode

                ERST_UPS_OK = .ErrSvrtyUPSOk
                ERST_UPS_ERROR = .ErrSvrtyUPSError
                ERST_UPS_WARNING = .ErrSvrtyUPSWarning
                ERST_UPS_FATAL = .ErrSvrtyUPSFatal

                DGST_MSTATUS = .MStatus
                DGST_MSTATUSDATA = .MStatusData

            End With

        Catch ex As Exception
            AppLogErr("Error in clsUPSHWDInterface.initUPSProperty:" & ex.Message)
        End Try
    End Sub

    Public Sub initDeviceState()
        Try
            With udtUPSLOOKUPDATACFG
                DST_POWERSUPPLYSTATE = .DeviceStatePowerSupply
                DST_BATTERYSTATE = .DeviceStateBattery
            End With

        Catch ex As Exception
            AppLogErr("Error in clsUPSHWDInterface.initDeviceState:" & ex.Message)
        End Try
    End Sub

    Public Sub initEventType()
        Try
            With udtUPSLOOKUPDATACFG
                EVTTYPE_DEVICEERROR = .EvtDeviceErr ' Print Failed
                EVTTYPE_DEVICEWRAP = .EvtDeviceWrap ' Print Wrap
                EVTTYPE_DATAARRIVED = .EvtDeviceReady ' Print Complete
            End With

        Catch ex As Exception
            AppLogErr("Error in clsUPSHWDInterface.initEventType:" & ex.Message)
        End Try
    End Sub

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


#Region "Property - UPS Setting "

    ReadOnly Property EnableUPS() As Boolean
        Get
            Return udtUPSHWDCFG.blnEnableUPS
        End Get
    End Property


    ReadOnly Property UPSStatusPath() As String
        Get
            Return udtUPSHWDCFG.strMonitoringPath.Trim
        End Get
    End Property


#End Region


#Region "Property - TxnStatus,ErrorSeverity"

    Property TxnStatus() As String
        Get
            Return udtUPSHWD.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtUPSHWD.strTxnStatus = value
        End Set
    End Property


    Property ErrorSeverity() As String
        Get
            Return udtUPSHWD.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtUPSHWD.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtUPSHWD.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtUPSHWD.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtUPSHWD.strMStatus
        End Get
        Set(ByVal value As String)
            udtUPSHWD.strMStatus = value
        End Set
    End Property

    Property MStatusData() As String
        Get
            Return udtUPSHWD.strMStatusData
        End Get
        Set(ByVal value As String)
            udtUPSHWD.strMStatusData = value
        End Set
    End Property

#End Region

#Region "Support Property"


    ReadOnly Property UPSMonitorStatusCode() As String
        Get
            Return UPSReplyCode
        End Get
    End Property

    ReadOnly Property UPSMonitorStatusDetails() As String
        Get
            Return UPSReplyCDDetails
        End Get
    End Property


    Property udtReplyUPSHWD() As UPSHWDSTR
        Get
            Return udtUPSHWD
        End Get
        Set(ByVal value As UPSHWDSTR)
            udtUPSHWD = value
        End Set
    End Property

    Property ReturnGenIDeviceTrace() As String
        Get
            Return strGeniDeviceTrace
        End Get
        Set(ByVal value As String)
            strGeniDeviceTrace = value
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


#Region "UPS Logger"


    Private Function InitclsHWDLogger() As Boolean
        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppINI.ReadAppLayerINICFGFile(HWDLAYERINIPATH, UPS) = True Then

                'Read INI File
                'Log Ini File
                '1.UPS Setting
                '2.UPS Lookup Data

                With objAppINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strIniPath1 = .strAppLayerINIPath1.Trim
                    strIniPath2 = .strAppLayerINIPath2.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                AppLogInfo("== UPS HWD Class Init Ok ==")
                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger Ini Path:" & strLogIniPath)
                AppLogInfo("UPS HWD Setting:" & strIniPath1)
                AppLogInfo("UPS Code Setting:" & strIniPath2)

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function InitUPSControl() As Boolean
        Try

            '1.UPS Setting
            If ReadUPSHWDSetting(strIniPath1) = False Then
                AppLogWarn("Read UPS Hardware Setting Failed")
                Return False
            Else

                '2.UPS Lookup Data Setting
                If ReadUPSLookupDataSetting(strIniPath2) = False Then
                    AppLogWarn("Read UPS Lookup Data Setting Failed")
                    Return False
                Else
                    AppLogInfo("Read UPS Lookup Data Setting OK")
                    Return True
                End If

            End If
        Catch ex As Exception
            AppLogErr("Error in InitUPSControl:" & ex.Message)
            Return False
        End Try
    End Function


#End Region


#Region "UPS Method - StartDevice, StopDevice, WrapDevice"

    Public Function StartDevice() As Boolean

        Try

            'Process UPS Status
            If udtUPSHWDCFG.blnEnableUPS = True Then
                If objUPS.ProcessUPSStatus = True Then
                    UPSDeviceDataReady()
                    AppLogInfo("UPS - StartDevice Success")
                    Return True
                Else
                    UPSDeviceError()
                    AppLogWarn("UPS - StartDevice Failed")
                    Return False
                End If
            Else
                'UPS Disable
                AppLogInfo("UPS Disable - StartDevice Success")
                Return True
            End If



        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.StartDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Dim blnReply As Boolean = False

        Try
            If udtUPSHWDCFG.blnEnableUPS = True Then
                TimeChecking.Stop()
                TimeChecking.Dispose()
                AppLogInfo("UPS - StopDevice Success")
                blnReply = True
            Else
                AppLogInfo("UPS Disable - StopDevice Success")

                blnReply = True
            End If

            'Close Logger
            CloseLog()

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.StopDevice:" & ex.Message)
            'Close Logger
            CloseLog()
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean

        Try

            blnLockUPS = False

            strProDeviceStatus = 0
            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = ""

            With udtUPSHWD
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
                .strMStatusData = ""
            End With

            'Beeper EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = UPSDeviceGraphicId
                .strDeviceName = UPSDeviceName
            End With

            'Beeper EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = UPSDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DST_BATTERYSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DEVICEWRAP
                .mDeviceDataValue = ""
            End With

            'Log Info
            AppLogInfo("UPS - WrapDevice Success")
            Return True

        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Try
            If udtUPSHWDCFG.blnEnableUPS = True Then
                blnLockUPS = True
                'Stop the checking timer
                TimeChecking.Stop()
                TimeChecking.Dispose()
                AppLogInfo("UPS - LockDevice Success")
                Return True
            Else
                blnLockUPS = True
                AppLogWarn("UPS Disable - LockDevice Success")
                Return True
            End If
        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.LockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Try
            blnLockUPS = False
            AppLogInfo("UPS - UnlockDevice Success")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.UnLockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal strDeviceState As Integer) As Boolean
        Try
            If udtUPSHWDCFG.blnEnableUPS = True Then
                If blnLockUPS = False Then
                    TimeChecking = New Timers.Timer
                    TimeChecking.Interval = udtUPSHWDCFG.dblCheckInterval
                    TimeChecking.Start()
                    AppLogInfo("UPS - WakeUpDevice Success")
                    Return True
                Else
                    AppLogWarn("UPS Locked - WakeUpDevice Failed")
                    Return False
                End If
            Else
                AppLogInfo("UPS Disable - WakeUpDevice Success")
                Return True
            End If
        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.WakeUpDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        Try
            UPSGetDiagnosStatus()
            AppLogInfo("UPS Disable - DiagnosticDevice Success")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.DiagnosticDevice:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "UPS Method: ReadINI File Value"

    Public Function ReadUPSSetting() As Boolean
        Try
            If ReadUPSHWDSetting(strIniPath1) = True Then
                AppLogInfo("Read UPS Hardware Setting Successfully")
                Return True
            Else
                AppLogWarn("Read UPS Hardware Setting Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in clsUPSControl.ReadUPSSetting:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UpdateUPSSetting(ByVal strUPSEnable As String, ByVal strUPSStatusPath As String) As Boolean
        Try

            If UpdateUPSHWDSetting(strUPSEnable, strUPSStatusPath, strIniPath1) = True Then
                AppLogInfo("Update UPS Hardware Setting Successfully")
                Return True
            Else
                AppLogWarn("Update UPS Hardware Setting Failed")
                Return False
            End If

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsUPSControl.UpdateUPSSetting:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Hardware Layer Event "

    Private Function UPSDeviceDataReady() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_UPS_ACPOWER_MODE

            If objUPS.GetUPSStatusCode = 5 Then
                strProErrSeverity = ERST_UPS_WARNING
            Else
                strProErrSeverity = ERST_UPS_OK
            End If

            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = udtUPSLOOKUPDATACFG.PowerUP

            'Set the Property Value
            With udtUPSHWD
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
                .iDeviceState = DST_POWERSUPPLYSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturnGenIDeviceTrace
                .mDeviceDataValue = "" ' Device Value
            End With

            'Raise the Events
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("RaiseEvent EvtDeviceDataReady")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.UPSDeviceDataReady:" & ex.Message)
            Return False
        End Try

    End Function

    Private Function UPSDeviceError() As Boolean

        Try
            strProDeviceStatus = 0
            strProTxnStatus = TXST_UPS_BATTERY_MODE
            strProErrSeverity = ERST_UPS_FATAL
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = udtUPSLOOKUPDATACFG.PowerOnBattery

            'Set the Property Value
            With udtUPSHWD
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
                .strDeviceGraphicId = UPSDeviceGraphicId
                .strDeviceName = UPSDeviceName
            End With

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_BATTERYSTATE
                .strEventType = EVTTYPE_DEVICEERROR
                .iDeviceTrace = ReturnGenIDeviceTrace
                .mDeviceDataValue = udtReplyDeviceError
            End With

            'Raise the Events
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("RaiseEvent EvtDeviceError")
            Return True

        Catch ex As Exception
            AppLogErr("Error in clsUPSHwd.UPSDeviceError:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function UPSGetDiagnosStatus() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TxnStatus
            strProErrSeverity = ErrorSeverity
            strProDignosticStatus = MStatus & MStatusData
            strProSupplyStatus = SupplyStatus

            'Set the Property Value
            With udtUPSHWD
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            AppLogInfo("UPS Diagnostic")
            AppLogInfo("Txn Status: " & TxnStatus)
            AppLogInfo("Error Severity: " & ErrorSeverity)
            AppLogInfo("Supply Status: " & SupplyStatus)
            AppLogInfo("Mstatus: " & MStatus)
            AppLogInfo("MStatus Data: " & MStatusData)

            Return True
        Catch ex As Exception
            AppLogErr("Error in UPSGetDiagnosStatus:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Timer Checking"

    Private Sub TimeChecking_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles TimeChecking.Elapsed
        Try
            If objUPS.ProcessUPSStatus = True Then
                UPSDeviceDataReady()
                AppLogInfo("UPS CheckStatusTimer  - AC Power Mode")
                'RaiseEvent EvtCheckUPSBatteryMode(False)
            Else
                UPSDeviceError()
                AppLogInfo("UPS CheckStatusTimer - Battery Mode")
                'RaiseEvent EvtCheckUPSBatteryMode(True)
            End If
        Catch ex As Exception
            AppLogErr("Error in UPSGetDiagnosStatus.TimeChecking_Elapsed:" & ex.Message)
        End Try
    End Sub

#End Region


End Class

