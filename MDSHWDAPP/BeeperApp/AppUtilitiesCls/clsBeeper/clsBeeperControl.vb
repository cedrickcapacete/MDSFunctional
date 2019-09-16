Imports System
Imports System.IO
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD
Imports clsBeeperHWD.clsAppStructure

Public Class clsBeeperControl

#Region "Class Variable"

    Private blnLockBeeper As Boolean = False
    Private strGeniDeviceTrace As String = ""

    Private strProDeviceStatus As Integer = 0
    Private strProTxnStatus As String = String.Empty
    Private strProErrSeverity As String = String.Empty
    Private strProDignosticStatus As String = String.Empty
    Private strProSupplyStatus As String = String.Empty


    'Beeper Const Value
    Private BeeperDeviceGraphicId As String = ""
    Private BeeperDeviceName As String = ""

    'TxnStatus - TXST
    '0 - Beeper OK
    '1 - Beeper Failed

    Private TXST_BEEPER_OK As String = ""
    Private TXST_BEEPER_FAILED As String = ""
    'Private TXST_PRINT_DEVICE_NOT_CFG As String = ""
    'Private TXST_PRINT_CANCEL_SIDEWAY As String = ""

    'ErrorSeverity - ERST
    Private ERST_BEEPER_OK As String = ""
    Private ERST_BEEPER_ERROR As String = ""
    Private ERST_BEEPER_WARNING As String = ""
    Private ERST_BEEPER_FATAL As String = ""

    'Diagnostic Status -DGST
    Private DGST_MSTATUS As String = ""
    Private DGST_MSTATUSDATA As String = ""

    'SupplyStatus - SYST  
    'Private Const SYST_STATUS As String = ""

    'Value for iDeviceState
    Private DST_BEEPERONSTATE As String = "" ' Beeper On 
    Private DST_BEEPEROFFSTATE As String = "" 'Beeper Off

    'Value for strEvenType
    Private EVTTYPE_DEVICEERROR As String = ""
    Private EVTTYPE_DEVICEWRAP As String = ""
    Private EVTTYPE_DATAARRIVED As String = ""



#End Region

#Region "Beeper Events - EvtDeviceDataReady,EvtDeviceError,EvtDeviceTimeOut"

    Private udtDeviceSender As DeviceSender
    Private udtEventDeviceArgs As EventDeviceArgs
    Private udtDeviceError As device_error

    'Beeper Events  
    Public Event EvtDeviceDataReady(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)

#End Region

#Region "New"

    Public Sub New()
        Try

            'Init the Logger
            If InitBEEPERHWD() = True Then
                'Init the Hwd Object and the Log File
                If InitBEEPERControl(strBeeperINI1, strBeeperINI2) = True Then

                    'Init Beeper 
                    initDeviceState()
                    initEventType()
                    initBeeperConst()
                    initBeeperProperty()

                    blnBeeperLock = False

                    'Else
                    'AppLogErr("StartDevice - Read Beeper Hwd CFG Failed")
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

#End Region


#Region "Instance Control - Single Instance"

    Private m_singleInstance As clsBeeperControl = Nothing

    Public Function SingleInstance() As clsBeeperControl
        If m_singleInstance Is Nothing Then
            m_singleInstance = New clsBeeperControl()
        End If
        Return m_singleInstance
    End Function

    Protected Overrides Sub finalize()
        If m_singleInstance IsNot Nothing Then
            m_singleInstance = Nothing
        End If
    End Sub

#End Region

#Region "Beeper Const - Property Value EventDeviceArgs value iDeviceState,strEventYype"

    Public Sub initBeeperConst()
        Try
            With udtBeeperHookupCFG
                BeeperDeviceGraphicId = .DeviceGraphicID
                BeeperDeviceName = .DeviceName
            End With
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.initBeeperConst.ErrInfo" & ex.Message)
        End Try
    End Sub

    Public Sub initBeeperProperty()
        Try
            With udtBeeperHookupCFG
                TXST_BEEPER_OK = .TxnStatusBeeperSoundOK
                TXST_BEEPER_FAILED = .TxnStatusBeeperSoundFailed
                'TXST_PRINT_DEVICE_NOT_CFG = objBeeperLookupData.BEEPERLOOKUPDATAInfo.TxnStatusDeviceNotCfg
                'TXST_PRINT_CANCEL_SIDEWAY = objBeeperLookupData.BEEPERLOOKUPDATAInfo.TxnStatusCancelSideway

                ERST_BEEPER_OK = .ErrSvrtyPrtOk
                ERST_BEEPER_ERROR = .ErrSvrtyPrtError
                ERST_BEEPER_WARNING = .ErrSvrtyPrtWarning
                ERST_BEEPER_FATAL = .ErrSvrtyPrtFatal

                DGST_MSTATUS = .MStatus
                DGST_MSTATUSDATA = .MStatusData
            End With

        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.initBeeperProperty.ErrInfo" & ex.Message)
        End Try
    End Sub

    Public Sub initDeviceState()
        Try
            With udtBeeperHookupCFG
                DST_BEEPERONSTATE = .DeviceStateBeeperOn
                DST_BEEPEROFFSTATE = .DeviceStateBeeperOff
            End With

        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.initDeviceState.ErrInfo" & ex.Message)
        End Try
    End Sub

    Public Sub initEventType()
        Try
            With udtBeeperHookupCFG
                EVTTYPE_DEVICEERROR = .EvtDeviceErr
                EVTTYPE_DEVICEWRAP = .EvtDeviceWrap
                EVTTYPE_DATAARRIVED = .EvtDeviceReady
            End With

        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.initEventType.ErrInfo" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Property - Beeper Setting "

    ReadOnly Property EnableBeeper() As Boolean
        Get
            Return udtBeeperHwdCFG.blnEnableBeeper
        End Get
    End Property


    ReadOnly Property BeeperComportNo() As String
        Get
            Return udtBeeperHwdCFG.strComport.Trim
        End Get
    End Property


#End Region


#Region "Property - TxnStatus,ErrorSeverity,SupplyStatus,MStatus"

    Property TxnStatus() As String
        Get
            Return udtBeeperHWD.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtBeeperHWD.strTxnStatus = value
        End Set
    End Property

    Property ErrorSeverity() As String
        Get
            Return udtBeeperHWD.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtBeeperHWD.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtBeeperHWD.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtBeeperHWD.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtBeeperHWD.strMStatus
        End Get
        Set(ByVal value As String)
            udtBeeperHWD.strMStatus = value
        End Set
    End Property

    Property MStatusData() As String
        Get
            Return udtBeeperHWD.strMStatusData
        End Get
        Set(ByVal value As String)
            udtBeeperHWD.strMStatusData = value
        End Set
    End Property

#End Region

#Region "Support Property"

    Property udtReplyBeeperHWD() As BeeperHWDSTR
        Get
            Return udtBeeperHWD
        End Get
        Set(ByVal value As BeeperHWDSTR)
            udtBeeperHWD = value
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

    Property udtReplyDeviceError() As device_error
        Get
            Return udtDeviceError
        End Get
        Set(ByVal value As device_error)
            udtDeviceError = value
        End Set
    End Property

    Property udtReplyDeviceSender() As DeviceSender
        Get
            Return udtDeviceSender
        End Get
        Set(ByVal value As DeviceSender)
            udtDeviceSender = value
        End Set
    End Property

#End Region


#Region "Init Class - InitBEEPERHWDLoggger"

    Private Function InitBEEPERHWD() As Boolean

        Try
            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(BEEPERHWDINIPATH, BEEPER) = True Then

                'Read INI File
                'Log Ini File
                '1.BEEPER Hardware
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strBeeperINI1 = .strAppLayerINIPath1.Trim
                    strBeeperINI2 = .strAppLayerINIPath2.Trim
                    strLogIniPath = .strLogINIPath.Trim
                End With

                'Init the logger
                InitLog(strLogIniPath)
                AppLogInfo("Beeper HWD Class Init Ok")

                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger Ini Path:" & strLogIniPath)
                AppLogInfo("Beeper HWD Ini Path:" & strBeeperINI1)
                AppLogInfo("Beeper Code Ini Path:" & strBeeperINI2)

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    'Init Class Object
    Private Function InitBEEPERControl(ByVal strBEEPERHWDIniPath As String, ByVal strBEEPERLOOKUPDATAIniPath As String) As Boolean
        Try

            If File.Exists(strBEEPERHWDIniPath) = True And File.Exists(strBEEPERLOOKUPDATAIniPath) = True Then

                'Init Layer Classes
                '1.Beeper Setting
                If ReadBeeperHWDSetting(strBEEPERHWDIniPath) = False Then
                    AppLogWarn("Read Beeper Hardware Setting Failed")
                    Return False
                Else
                    '2.Beeper Lookup Data Setting
                    If ReadBeeperLookupDataSetting(strBEEPERLOOKUPDATAIniPath) = False Then
                        AppLogWarn("Read Beeper Lookup Data Setting Failed")
                        Return False
                    Else
                        AppLogInfo("Read Beeper Lookup Data Setting OK")
                        Return True
                    End If
                End If
            Else
                AppLogErr("Beeper Hardware Path Not Found")
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Beeper Method: StartDevice, StopDevice, WrapDevice"

    Public Function StartDevice() As Boolean
        Try

            'Init the Beeper ComPort
            If udtBeeperHwdCFG.blnEnableBeeper = True Then
                If objBeeperHWD.InitBEEPERHWD = True Then
                    BeeperDeviceDataReady()
                    AppLogInfo("Beeper - StartDevice Success")
                    Return True
                Else
                    BeeperDeviceError()
                    AppLogErr("Beeper - StartDevice Failed")
                    Return False
                End If
            Else
                'Disable Beeper
                BeeperDeviceDataReady()
                AppLogInfo("Beeper Disable - StartDevice Success")
                Return True
            End If


        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.StartDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Dim blnReply As Boolean = False

        Try
            If udtBeeperHwdCFG.blnEnableBeeper = True Then
                If objBeeperHWD.CloseBEEPERHWD = True Then
                    AppLogInfo("Beeper - StopDevice Success")
                    blnReply = True
                Else
                    AppLogInfo("Beeper - StopDevice Failed")
                    blnReply = False
                End If
            Else
                AppLogInfo("Beeper Disable - StopDevice Success")
                blnReply = True
            End If

            'Close Logger - Hwd
            CloseLog()

            Return blnReply
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.StopDevice:" & ex.Message)
            'Close Logger - Hwd
            CloseLog()
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean

        Try

            blnBeeperLock = False

            'If objBeeperHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = True Then
            strProDeviceStatus = 0
            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = ""

            With udtBeeperHWD
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
                .strMStatusData = ""
            End With

            'Beeper EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = BeeperDeviceGraphicId
                .strDeviceName = BeeperDeviceName
            End With

            'Beeper EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = BeeperDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DST_BEEPEROFFSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DEVICEWRAP
                .mDeviceDataValue = ""
            End With

            'Log Info
            AppLogInfo("Beeper - WrapDevice Success")
            Return True

        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Try
            If udtBeeperHwdCFG.blnEnableBeeper = True Then
                If objBeeperHWD.BeeperTriggerOff = True Then
                    blnBeeperLock = True
                    AppLogInfo("Beeper - LockDevice Success")
                    Return True
                Else
                    blnBeeperLock = False
                    AppLogInfo("Beeper - LockDevice Failed")
                    Return False
                End If
            Else
                blnBeeperLock = True
                AppLogInfo("Beeper Disable - LockDevice Success")
                Return True
            End If
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.LockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Try
            blnBeeperLock = False
            'No Hardware Checking
            AppLogInfo("Beeper - UnlockDevice Success")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.UnLockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal strDeviceState As Integer) As Boolean
        Try
            If udtBeeperHwdCFG.blnEnableBeeper = True Then
                If blnBeeperLock = False Then
                    If objBeeperHWD.BeeperTriggerOn = True Then
                        AppLogInfo("Beeper - WakeUpDevice Success")
                        Return True
                    Else
                        AppLogInfo("Beeper - WakeUpDevice Failed")
                        Return False
                    End If
                Else
                    AppLogInfo("Beeper Locked - WakeUpDevice Failed")
                    Return False
                End If
            Else
                AppLogInfo("Beeper Disable - WakeUpDevice Success")
                Return True
            End If
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.WakeUpDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        Try
            BeeperGetDiagnosStatus()
            AppLogInfo("Beeper - DiagnosticDevice Success")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.DiagnosticDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function Trigger(ByVal blnMode As Boolean) As Boolean
        Try
            If blnMode = True Then
                objBeeperHWD.BeeperTriggerOn()
                AppLogInfo("Beeper - Trigger ON Success")
            Else
                objBeeperHWD.BeeperTriggerOff()
                AppLogInfo("Beeper - Trigger OFF Success")
            End If

            Return True

        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.Trigger:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "Beeper Method: ReadINI File Value"

    Public Function ReadBeeperSetting() As Boolean
        Try
            If ReadBeeperHWDSetting(strBeeperINI1) = True Then
                AppLogInfo("Read Beeper Hardware Setting Successfully")
                Return True
            Else
                AppLogWarn("Read Beeper Hardware Setting Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.ReadBeeperSetting:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UpdateBeeperSetting(ByVal strBeeperEnable As String, ByVal strComport As String) As Boolean
        Try

            If UpdateBeeperHWDSetting(strBeeperEnable, strComport, strBeeperINI1) = True Then
                AppLogInfo("Update Beeper Hardware Setting Successfully")
                Return True
            Else
                AppLogWarn("Update Beeper Hardware Setting Failed")
                Return False
            End If

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.UpdateBeeperSetting:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "Hardware Layer Event - BeeperDeviceDataReady,BeeperDeviceError,BeeperGetDiagnosStatus"

    Private Function BeeperDeviceDataReady() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_BEEPER_OK
            strProErrSeverity = ERST_BEEPER_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = udtBeeperHookupCFG.BeeperSuplyStatus

            'Set the Property Value
            With udtBeeperHWD
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
                .iDeviceState = DST_BEEPERONSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturnGenIDeviceTrace
                .mDeviceDataValue = "" ' Device Value
            End With

            'Raise the Events
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("RaiseEvent EvtDeviceDataReady")

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.BeeperDeviceDataReady:" & ex.Message)
            Return False
        End Try

    End Function

    Private Function BeeperDeviceError() As Boolean
        Try
            strProDeviceStatus = 0
            strProTxnStatus = TXST_BEEPER_FAILED
            strProErrSeverity = ERST_BEEPER_FATAL
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = udtBeeperHookupCFG.BeeperSuplyStatus

            'Set the Property Value
            With udtBeeperHWD
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
                .strDeviceGraphicId = BeeperDeviceGraphicId
                .strDeviceName = BeeperDeviceName
            End With

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_BEEPEROFFSTATE
                .strEventType = EVTTYPE_DEVICEERROR
                .iDeviceTrace = ReturnGenIDeviceTrace
                .mDeviceDataValue = udtReplyDeviceError
            End With

            'Raise the Events
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("RaiseEvent EvtDeviceError")

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.BeeperDeviceError:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function BeeperGetDiagnosStatus() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TxnStatus
            strProErrSeverity = ErrorSeverity
            strProDignosticStatus = MStatus & MStatusData
            strProSupplyStatus = udtBeeperHookupCFG.BeeperSuplyStatus

            'Set the Property Value
            With udtBeeperHWD
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            AppLogInfo("Beeper - DiagnosStatus")
            AppLogInfo("Txn Status: " & TxnStatus)
            AppLogInfo("Error Severity: " & ErrorSeverity)
            AppLogInfo("Supply Status: " & SupplyStatus)
            AppLogInfo("Mstatus: " & MStatus)
            AppLogInfo("MStatus Data: " & MStatusData)

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsBeeperControl.BeeperGetDiagnosStatus:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

   
End Class
