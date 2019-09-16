Imports System
Imports System.IO
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD
Imports clsRSI.clsAppStructure

Public Class clsRSIHWD

#Region "Class Variable"

    Private blnLockRSI As Boolean = False
    Private strGeniDeviceTrace As String = ""

    Private strProDeviceStatus As Integer = 0
    Private strProTxnStatus As String = String.Empty
    Private strProErrSeverity As String = String.Empty
    Private strProDignosticStatus As String = String.Empty
    Private strProSupplyStatus As String = String.Empty


    'RSI Const Value
    Private RSIDeviceGraphicId As String = ""
    Private RSIDeviceName As String = ""

    'TxnStatus - TXST
    '0 - RSI OK
    '1 - RSI Failed

    Private TXST_RSI_OK As String = ""
    Private TXST_RSI_FAILED As String = ""
    'Private TXST_PRINT_DEVICE_NOT_CFG As String = ""
    'Private TXST_PRINT_CANCEL_SIDEWAY As String = ""

    'ErrorSeverity - ERST
    Private ERST_RSI_OK As String = ""
    Private ERST_RSI_ERROR As String = ""
    Private ERST_RSI_WARNING As String = ""
    Private ERST_RSI_FATAL As String = ""

    'Diagnostic Status -DGST
    Private DGST_MSTATUS As String = ""
    Private DGST_MSTATUSDATA As String = ""

    'SupplyStatus - SYST  
    'Private Const SYST_STATUS As String = ""

    'Value for iDeviceState
    Private DST_RSIONSTATE As String = "" ' RSI On 
    Private DST_RSIOFFSTATE As String = "" 'RSI Off

    'Value for strEvenType
    Private EVTTYPE_DEVICEERROR As String = ""
    Private EVTTYPE_DEVICEWRAP As String = ""
    Private EVTTYPE_DATAARRIVED As String = ""



#End Region

#Region "RSI Events - EvtDeviceDataReady,EvtDeviceError,EvtDeviceTimeOut"

    Private udtDeviceSender As DeviceSender
    Private udtEventDeviceArgs As EventDeviceArgs
    Private udtDeviceError As device_error

    'RSI Events  
    Public Event EvtDeviceDataReady(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)

#End Region

#Region "New"

    Public Sub New()
        Try

            'Init the Logger
            If InitRSIHWD() = True Then
                'Init the Hwd Object and the Log File
                If InitRSIControl(strRSIINI1, strRSIINI2) = True Then

                    'Init RSI 
                    initDeviceState()
                    initEventType()
                    initRSIConst()
                    initRSIProperty()

                    blnRSILock = False

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

#End Region


#Region "Instance Control - Single Instance"

    Private m_singleInstance As clsRSIHWD = Nothing

    Public Function SingleInstance() As clsRSIHWD
        If m_singleInstance Is Nothing Then
            m_singleInstance = New clsRSIHWD()
        End If
        Return m_singleInstance
    End Function

    Protected Overrides Sub finalize()
        If m_singleInstance IsNot Nothing Then
            m_singleInstance = Nothing
        End If
    End Sub

#End Region

#Region "RSI Const - Property Value EventDeviceArgs value iDeviceState,strEventYype"

    Public Sub initRSIConst()
        Try
            With udtRSIHookupCFG
                RSIDeviceGraphicId = .DeviceGraphicID
                RSIDeviceName = .DeviceName
            End With
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.initRSIConst.ErrInfo" & ex.Message)
        End Try
    End Sub

    Public Sub initRSIProperty()
        Try
            With udtRSIHookupCFG
                TXST_RSI_OK = .TxnStatusRSISoundOK
                TXST_RSI_FAILED = .TxnStatusRSISoundFailed
                'TXST_PRINT_DEVICE_NOT_CFG = objRSILookupData.RSILOOKUPDATAInfo.TxnStatusDeviceNotCfg
                'TXST_PRINT_CANCEL_SIDEWAY = objRSILookupData.RSILOOKUPDATAInfo.TxnStatusCancelSideway

                ERST_RSI_OK = .ErrSvrtyPrtOk
                ERST_RSI_ERROR = .ErrSvrtyPrtError
                ERST_RSI_WARNING = .ErrSvrtyPrtWarning
                ERST_RSI_FATAL = .ErrSvrtyPrtFatal

                DGST_MSTATUS = .MStatus
                DGST_MSTATUSDATA = .MStatusData
            End With

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.initRSIProperty.ErrInfo" & ex.Message)
        End Try
    End Sub

    Public Sub initDeviceState()
        Try
            With udtRSIHookupCFG
                DST_RSIONSTATE = .DeviceStateRSIOn
                DST_RSIOFFSTATE = .DeviceStateRSIOff
            End With

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.initDeviceState.ErrInfo" & ex.Message)
        End Try
    End Sub

    Public Sub initEventType()
        Try
            With udtRSIHookupCFG
                EVTTYPE_DEVICEERROR = .EvtDeviceErr
                EVTTYPE_DEVICEWRAP = .EvtDeviceWrap
                EVTTYPE_DATAARRIVED = .EvtDeviceReady
            End With

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.initEventType.ErrInfo" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Property - RSI Setting "

    ReadOnly Property EnableRSI() As Boolean
        Get
            Return udtRSIHwdCFG.blnEnableRSI
        End Get
    End Property

    ReadOnly Property NoofRSIComport() As String
        Get
            Return udtRSIHwdCFG.strNumofRSI.Trim
        End Get
    End Property

    ReadOnly Property RSI1ComportNo() As String
        Get
            Return udtRSIHwdCFG.strC1Comport.Trim
        End Get
    End Property

    ReadOnly Property RSI2ComportNo() As String
        Get
            Return udtRSIHwdCFG.strC2Comport.Trim
        End Get
    End Property

#End Region


#Region "Property - TxnStatus,ErrorSeverity,SupplyStatus,MStatus"

    Property TxnStatus() As String
        Get
            Return udtRSIHWD.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtRSIHWD.strTxnStatus = value
        End Set
    End Property

    Property ErrorSeverity() As String
        Get
            Return udtRSIHWD.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtRSIHWD.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtRSIHWD.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtRSIHWD.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtRSIHWD.strMStatus
        End Get
        Set(ByVal value As String)
            udtRSIHWD.strMStatus = value
        End Set
    End Property

    Property MStatusData() As String
        Get
            Return udtRSIHWD.strMStatusData
        End Get
        Set(ByVal value As String)
            udtRSIHWD.strMStatusData = value
        End Set
    End Property

#End Region

#Region "Support Property"

    Property udtReplyRSIHWD() As RSIHWDSTR
        Get
            Return udtRSIHWD
        End Get
        Set(ByVal value As RSIHWDSTR)
            udtRSIHWD = value
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


#Region "Init Class - InitRSIHWDLoggger"

    Private Function InitRSIHWD() As Boolean

        Try
            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(RSIHWDINIPATH, RSI) = True Then

                'Read INI File
                'Log Ini File
                '1.RSI Hardware
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strRSIINI1 = .strAppLayerINIPath1.Trim
                    strRSIINI2 = .strAppLayerINIPath2.Trim
                    strLogIniPath = .strLogINIPath.Trim
                End With

                'Init the logger
                InitLog(strLogIniPath)
                AppLogInfo("== RSI HWD Class Init Ok ==")
                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger Ini Path:" & strLogIniPath)
                AppLogInfo("RSI HWD Setting:" & strRSIINI1)
                AppLogInfo("RSI Code Setting:" & strRSIINI2)

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.InitRSIHWD:" & ex.Message)
            Return False
        End Try
    End Function

    'Init Class Object
    Private Function InitRSIControl(ByVal strRSIHWDIniPath As String, ByVal strRSILOOKUPDATAIniPath As String) As Boolean
        Try

            If File.Exists(strRSIHWDIniPath) = True And File.Exists(strRSILOOKUPDATAIniPath) = True Then

                'Init Layer Classes
                '1.RSI Setting
                If ReadRSIHWDSetting(strRSIHWDIniPath) = False Then
                    AppLogWarn("Read RSI Hardware Setting Failed")
                    Return False
                Else
                    '2.RSI Lookup Data Setting
                    If ReadRSILookupDataSetting(strRSILOOKUPDATAIniPath) = False Then
                        AppLogWarn("Read RSI Lookup Data Setting Failed")
                        Return False
                    Else
                        AppLogInfo("Read RSI Lookup Data Setting OK")
                        Return True
                    End If
                End If
            Else
                AppLogErr("RSI Hardware Path Not Found")
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "RSI Method: ReadINI File Value"

    Public Function ReadRSISetting() As Boolean
        Try
            If ReadRSIHWDSetting(strRSIINI1) = True Then
                AppLogInfo("Read RSI Hardware Setting Successfully")
                Return True
            Else
                AppLogWarn("Read RSI Hardware Setting Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.ReadRSISetting:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UpdateRSISetting(ByVal strRSIEnable As String, ByVal strNumofRSI As String, ByVal strComport1 As String, ByVal strComport2 As String) As Boolean
        Try

            If UpdateRSIHWDSetting(strRSIEnable, strNumofRSI, strComport1, strComport2, strRSIINI1) = True Then
                AppLogInfo("Update RSI Hardware Setting Successfully")
                Return True
            Else
                AppLogWarn("Update RSI Hardware Setting Failed")
                Return False
            End If

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.UpdateRSISetting:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "RSI Method: StartDevice, StopDevice, WrapDevice"

    Public Function StartDevice() As Boolean
        Try

            'Init the RSI ComPort
            If udtRSIHwdCFG.blnEnableRSI = True Then
                If objRSIHWD.InitRSIHWD = True Then
                    RSIDeviceDataReady()
                    AppLogInfo("RSI - StartDevice Success")
                    Return True
                Else
                    RSIDeviceError()
                    AppLogErr("RSI - StartDevice Failed")
                    Return False
                End If
            Else
                'Disable RSI
                RSIDeviceDataReady()
                AppLogInfo("RSI Disable - StartDevice Success")
                Return True
            End If

          
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.StartDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Dim blnReply As Boolean = False

        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                If objRSIHWD.CloseRSIHWD = True Then
                    AppLogInfo("RSI - StopDevice Success")
                    blnReply = True
                Else
                    AppLogInfo("RSI - StopDevice Failed")
                    blnReply = False
                End If
            Else
                AppLogInfo("RSI Disable - StopDevice Success")
                blnReply = True
            End If

            'Close Logger - Hwd
            CloseLog()
            Return blnReply
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.StopDevice:" & ex.Message)
            'Close Logger - Hwd
            CloseLog()
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean

        Try

            blnRSILock = False

            'If objRSIHWD.RECEIPTPRINTERHWDInfo.blnPrinterStatus = True Then
            strProDeviceStatus = 0
            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = ""

            With udtRSIHWD
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
                .strMStatusData = ""
            End With

            'RSI EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = RSIDeviceGraphicId
                .strDeviceName = RSIDeviceName
            End With

            'RSI EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = RSIDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DST_RSIOFFSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DEVICEWRAP
                .mDeviceDataValue = ""
            End With

            'Log Info
            AppLogInfo("RSI - WrapDevice Success")
            Return True

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Try
            blnRSILock = True
            AppLogInfo("RSI - LockDevice Success")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.LockDevice:" & ex.Message)
            blnRSILock = False
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Try
            blnRSILock = False
            'No Hardware Checking
            AppLogInfo("RSI - UnlockDevice Success")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.UnLockDevice:" & ex.Message)
            blnRSILock = False
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal strDeviceState As Integer) As Boolean
        Try
            blnRSILock = False
            AppLogInfo("RSI Disable - WakeUpDevice Success")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.WakeUpDevice:" & ex.Message)
            blnRSILock = False
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        Try
            RSIGetDiagnosStatus()
            AppLogInfo("RSI - DiagnosticDevice Success")
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.DiagnosticDevice:" & ex.Message)
            Return False
        End Try
    End Function

   
#End Region


#Region "RSI Light Function"

    Public Function RSILIGHT1Trigger(ByVal blnMode As Boolean) As Boolean
        Dim blnReply As Boolean = False
        Try

            If blnRSILock = False Then
                If blnMode = True Then
                    blnReply = objRSIHWD.RSIL1TriggerOn()
                    AppLogInfo("RSI Light 1 - Trigger ON=" & blnReply)
                Else
                    blnReply = objRSIHWD.RSIL1TriggerOff()
                    AppLogInfo("RSI Light 1 - Trigger OFF=" & blnReply)
                End If
            Else
                AppLogWarn("RSI Light 1 - Device Locked")
                blnReply = True
            End If

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.RSILIGHT1Trigger:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSILIGHT2Trigger(ByVal blnMode As Boolean) As Boolean
        Dim blnReply As Boolean = False
        Try
            If blnRSILock = False Then
                If blnMode = True Then
                    blnReply = objRSIHWD.RSIL2TriggerOn()
                    AppLogInfo("RSI Light 2 - Trigger ON=" & blnReply)
                Else
                    blnReply = objRSIHWD.RSIL2TriggerOff()
                    AppLogInfo("RSI Light 2 - Trigger OFF=" & blnReply)
                End If
            Else
                AppLogWarn("RSI Light 2 - Device Locked")
                blnReply = True
            End If

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.RSILIGHT2Trigger:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSILIGHT3Trigger(ByVal blnMode As Boolean) As Boolean
        Dim blnReply As Boolean = False
        Try
            If blnRSILock = False Then
                If blnMode = True Then
                    blnReply = objRSIHWD.RSIL3TriggerOn()
                    AppLogInfo("RSI Light 3 - Trigger ON=" & blnReply)
                Else
                    blnReply = objRSIHWD.RSIL3TriggerOff()
                    AppLogInfo("RSI Light 3 - Trigger OFF=" & blnReply)
                End If
            Else
                AppLogWarn("RSI Light 3 - Device Locked")
                blnReply = True
            End If

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.RSILIGHT3Trigger:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSILIGHT4Trigger(ByVal blnMode As Boolean) As Boolean
        Dim blnReply As Boolean = False
        Try
            If blnRSILock = False Then
                If blnMode = True Then
                    blnReply = objRSIHWD.RSIL4TriggerOn()
                    AppLogInfo("RSI Light 4 - Trigger ON=" & blnReply)
                Else
                    blnReply = objRSIHWD.RSIL4TriggerOff()
                    AppLogInfo("RSI Light 4 - Trigger OFF=" & blnReply)
                End If
            Else
                AppLogWarn("RSI Light 4 - Device Locked")
                blnReply = True
            End If

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.RSILIGHT4Trigger:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "Hardware Layer Event - RSIDeviceDataReady,RSIDeviceError,RSIGetDiagnosStatus"

    Private Function RSIDeviceDataReady() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TXST_RSI_OK
            strProErrSeverity = ERST_RSI_OK
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = udtRSIHookupCFG.RSISuplyStatus

            'Set the Property Value
            With udtRSIHWD
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
                .iDeviceState = DST_RSIONSTATE
                .strEventType = EVTTYPE_DATAARRIVED
                .iDeviceTrace = ReturnGenIDeviceTrace
                .mDeviceDataValue = "" ' Device Value
            End With

            'Raise the Events
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("RaiseEvent EvtDeviceDataReady")

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.RSIDeviceDataReady:" & ex.Message)
            Return False
        End Try

    End Function

    Private Function RSIDeviceError() As Boolean
        Try
            strProDeviceStatus = 0
            strProTxnStatus = TXST_RSI_FAILED
            strProErrSeverity = ERST_RSI_FATAL
            strProDignosticStatus = DGST_MSTATUS & DGST_MSTATUSDATA
            strProSupplyStatus = udtRSIHookupCFG.RSISuplyStatus

            'Set the Property Value
            With udtRSIHWD
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
                .strDeviceGraphicId = RSIDeviceGraphicId
                .strDeviceName = RSIDeviceName
            End With

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_RSIOFFSTATE
                .strEventType = EVTTYPE_DEVICEERROR
                .iDeviceTrace = ReturnGenIDeviceTrace
                .mDeviceDataValue = udtReplyDeviceError
            End With

            'Raise the Events
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
            AppLogInfo("RaiseEvent EvtDeviceError")

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.RSIDeviceError:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function RSIGetDiagnosStatus() As Boolean
        Try
            'Value to Set for NDC
            strProDeviceStatus = 0
            strProTxnStatus = TxnStatus
            strProErrSeverity = ErrorSeverity
            strProDignosticStatus = MStatus & MStatusData
            strProSupplyStatus = udtRSIHookupCFG.RSISuplyStatus

            'Set the Property Value
            With udtRSIHWD
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            AppLogInfo("RSI - DiagnosStatus")
            AppLogInfo("Txn Status: " & TxnStatus)
            AppLogInfo("Error Severity: " & ErrorSeverity)
            AppLogInfo("Supply Status: " & SupplyStatus)
            AppLogInfo("Mstatus: " & MStatus)
            AppLogInfo("MStatus Data: " & MStatusData)

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsRSIHWD.RSIGetDiagnosStatus:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

   
End Class
