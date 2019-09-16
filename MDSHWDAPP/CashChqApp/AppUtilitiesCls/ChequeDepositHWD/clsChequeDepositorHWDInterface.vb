Imports MDSModuleControl
Imports MDSXFSWrapper
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppLogger.clsAppLoggerControl.LoggerEvtTypeEnum
Imports clsAppLogger.clsAppLoggerControl.LoggerMsgTypeEnum
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class clsChequeDepositorHWDInterface

#Region "HWD Object Variables"
    Public WithEvents objMDSControl As New clsMDSModuleControl
#End Region

#Region "Variables"

    Dim strTitle As String = "clsChequeDepositorHWDInterface"

    Dim TotalCheque As Integer

    Dim LastMDSInteract As LastMDSActivity
    Dim WithEvents MDSStartupTimerControl As System.Timers.Timer
    'Dim WithEvents MDSReturnTimerControl As System.Timers.Timer

    Dim intCheckCount As Integer
    Dim blnInitializeFlag As Boolean = False
    Dim blnDoorSensor As Boolean = False

    Dim blnChqEnable As Boolean = False

    'Hardware Cheque Const Value
    Private ChequeDeviceGraphicId As String
    Private ChequeDeviceName As String

    'ChequeHWD Const Event Type
    Private EVTTYPE_WRAPDEVICE As String
    Private EVTTYPE_TIMEOUT As String
    Private EVTTYPE_DATAARRIVED As String
    Private EVTTYPE_ERROR As String

    'Property Tmp Variable and Control
    Private strProTxnStatus As String = String.Empty
    Private strProErrSeverity As String = String.Empty
    Private strProDignosticStatus As String = String.Empty
    Private strProSupplyStatus As String = String.Empty

    Dim blnMDSPingFailed As Boolean = False

#End Region


#Region "Events Structure"

    Private udtDeviceSender As DeviceSender
    Private udtEventDeviceArgs As EventDeviceArgs
    Private udtDeviceError As device_error

    Public Event EvtDeviceDataReady(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)

#End Region

#Region "Structures"

    Structure ChequeHWDStatus
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure

    Public udtChequeHWDStatus As ChequeHWDStatus

    Structure ChequeDetailsDeposited
        Dim ChequeCountNumber As Integer
    End Structure

    Public udtChequeInserted As ChequeDetailsDeposited

#End Region


#Region "Instance Control"
    Private m_singleInstance As clsChequeDepositorHWDInterface = Nothing

    Public Function SingleInstance() As clsChequeDepositorHWDInterface
        If m_singleInstance Is Nothing Then
            m_singleInstance = New clsChequeDepositorHWDInterface()
        End If
        Return m_singleInstance
    End Function

    Protected Overrides Sub finalize()
        If m_singleInstance IsNot Nothing Then
            m_singleInstance = Nothing
        End If
    End Sub
#End Region


#Region "New "

    Public Sub New()
        Try

            'Init the Chq Logger
            InitChequeDepositorHWDLogger()

        Catch ex As Exception

        End Try
    End Sub

#End Region


#Region "Logger"
    Public Sub InitChequeDepositorHWDLogger()
        Try
            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(MDSHWDINIPATH, MDS) = True Then

                'Read INI File
                'Log Ini File
                '1.MDS CASH Hardware
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "MDS - Cheque Hookup Class Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                'AppLogInfo("Cheque Depositor Interface Loaded")

            Else
                'MSD - Cheque Init Failed


            End If

        Catch ex As Exception
            AppLogErr("Error in clsChequeHWD.InitChequeDepositorHWDLogger:" & ex.Message)
        End Try
    End Sub
#End Region


#Region "Support Function"

    Public Sub SetChequeHWDLayerProp()
        Try
            With objMDSControl.ChequeDepositorDeviceCode
                ChequeDeviceGraphicId = .CHQDEVDeviceID
                ChequeDeviceName = .CHQDEVDeviceName

                EVTTYPE_WRAPDEVICE = .CHQDEVEventWrap
                EVTTYPE_TIMEOUT = .CHQDEVEventTimeout
                EVTTYPE_DATAARRIVED = .CHQDEVEventDataArrive
                EVTTYPE_ERROR = .CHQDEVEventError

                DVST_ERRIN_DEVSTATE = .CHQDEVErrDevState
                ERST_NOERR = .CHQDEVErrorStateNoError
                ERST_CDERR = .CHQDEVErrorStateError
                ERST_CDFATAL = .CHQDEVErrorStateFatal
                ERST_CDWARN = .CHQDEVErrorStateWarning

                DGST_STATUS = .CHQDEVErrorDGSTStatus
                SYST_STATUS_NOSTATE = .CHQDEVErrorSysStatNoState
            End With

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-SetChequeHWDLayerProp:" & ex.Message)
        End Try
    End Sub


    Public Sub RunStartupWatcher()
        Try
            MDSStartupTimerControl = New Timers.Timer
            MDSStartupTimerControl.Enabled = True
            MDSStartupTimerControl.Interval = (objMDSControl.MachineStartupInterval * 1000)
            MDSStartupTimerControl.Start()

            intCheckCount = 0
        Catch ex As Exception
            AppLogErr("Error In ChequeDepositor-RunStartupWatcher :" & ex.Message)
        End Try
    End Sub


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
            Return "9999"
        End Try
    End Function



#End Region


#Region "MDS Light Control"

    Public Function StartMDSLight(ByVal strUserChoice As String) As Boolean
        Dim blnRtn As Boolean = False
        Try
            If objMDSControl.StartMDSLightHWD(strUserChoice) Then
                AppLogInfo("MDS Cheque-StartMDSLight Success User Choice:" & strUserChoice)
                blnRtn = True
            Else
                AppLogWarn("MDS Cheque-StartMDSLight Failed User Choice:" & strUserChoice)
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-StartMDSLight: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSLight() As Boolean
        Dim blnRtn As Boolean = False
        Try
            If objMDSControl.StopMDSLightHWD Then
                AppLogInfo("MDS Cheque-StopMDSLight Success")
                blnRtn = True
            Else
                AppLogWarn("MDS Cheque-StopMDSLight Failed")
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-StopMDSLight: " & ex.Message)
            Return False
        End Try
    End Function


#End Region

#Region "MDS Card Reader Light Control"

    Public Function StartMDSReaderLight(ByVal strUserChoice As String) As Boolean
        Dim blnRtn As Boolean = False
        Try
            If objMDSControl.StartMDSReaderLightHWD(strUserChoice) Then
                AppLogInfo("MDS Cheque-StartMDSReaderLight Success User Choice:" & strUserChoice)
                blnRtn = True
            Else
                AppLogWarn("MDS Cheque-StartMDSReaderLight Failed User Choice:" & strUserChoice)
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-StartMDSReaderLight: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSReaderLight() As Boolean
        Dim blnRtn As Boolean = False
        Try
            If objMDSControl.StopMDSReaderLightHWD Then
                AppLogInfo("MDS Cheque-StopMDSReaderLight Success")
                blnRtn = True
            Else
                AppLogWarn("MDS Cheque-StopMDSReaderLight Failed")
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-StopMDSReaderLight: " & ex.Message)
            Return False
        End Try
    End Function


#End Region


#Region "Method - StartDevice"


    Public Function StartDevice() As Boolean

        Dim blnRtn As Boolean = False
        Try

            'Ping Failed
            blnMDSPingFailed = False

            'Init the Cheque Logger
            'InitChequeDepositorHWDLogger()

            If objMDSControl.InitializeMDS Then
                'Set the property
                SetChequeHWDLayerProp()
                intCheckCount = 0
                blnInitializeFlag = True
                blnRtn = True
                AppLogInfo("MDS Cheque-StartDevice Success")
            Else
                AppLogInfo("MDS Cheque-StartDevice Success")
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-StartDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Dim blnRtn As Boolean = False
        Try
            'objMDSControl.StopMDS()

            'Ping Failed
            blnMDSPingFailed = False

            'Close the Logger
            CloseLog()

            blnRtn = True

            'If objMDSControl.StopMDS Then
            '    AppLogInfo("MDS Cheque-StopDevice Success")
            '    blnRtn = True
            'Else
            '    AppLogErr("MDS Cheque-StopDevice Failed")
            '    blnRtn = False
            'End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-StopDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Dim blnRtn As Boolean = False
        Try
            'If objMDSControl.StartMDSLightHWD Then
            '    AppLogInfo("MDS Cheque-UnlockDevice Success")
            '    blnRtn = True
            'Else
            '    AppLogErr("MDS Cheque-UnlockDevice Failed")
            '    blnRtn = False
            'End If
            'Return blnRtn

            Return True
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-UnlockDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Dim blnRtn As Boolean = False
        Try
            'If objMDSControl.StopMDSLight Then
            '    AppLogInfo("MDS Cheque-LockDevice Success")
            '    blnRtn = True
            'Else
            '    AppLogErr("MDS Cheque-LockDevice Failed")
            '    blnRtn = False
            'End If
            'Return blnRtn

            Return True
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-LockDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal intState As Integer) As Boolean
        Dim blnRtn As Boolean
        Try
            'If intState = CurrentState.OpenFeeder Then
            '    SetPropertiesChequeTransaction()
            'End If

            blnRtn = ActionOnWakeUp(intState)
            AppLogInfo("MDS Cheque-WakeupDevice State:" & intState & " Reply:" & blnRtn)

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-WakeUpDevice: " & ex.Message)
            Return False
        End Try
    End Function


    Public Sub SetPropertiesChequeTransaction()
        Try
            objMDSControl.SetCurrentTransCheque()
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-SetPropertiesChequeTransaction: " & ex.Message)
        End Try
    End Sub

    Public Function ActionOnWakeUp(ByVal intState As Integer) As Boolean
        Dim blnRtn As Boolean = False

        Try
            Select Case intState

                Case CurrentState.Close

                    intCurrentState = intState

                    If bolTransStart = True Then

                        If objMDSControl.UserReactCANCEL() Then

                            bolTransStart = False
                            bolCancelTrans = True

                            AppLogInfo("MDS Cheque-Stop Transaction Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cheque-Stop Transaction Failed")
                        End If
                    Else
                        If objMDSControl.StopMDSTransaction Then
                            AppLogInfo("MDS Cheque-Stop Transaction Success")
                            blnRtn = True
                        Else
                            AppLogErr("ChequeDepositor-Stop Transaction Failed")
                        End If
                    End If


                Case CurrentState.OpenFeeder
                    intCurrentState = intState
                    Enabled = True

                    If blnChqEnable = True Then
                        If objMDSControl.StartMDSTransaction(clsMDSModuleControl.TransactionType.ChequeMode) Then
                            bolTransStart = True
                            AppLogInfo("MDS Cheque-Start Transaction Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cheque-Start Transaction Failed")
                        End If
                    Else
                        AppLogErr("MDS Cheque-StartDevice Fail - Enable False")
                        blnRtn = False
                    End If

                Case CurrentState.InsertMore
                    If LastMDSInteract = LastMDSActivity.WantToInsertMore Then

                        If objMDSControl.UserReactYES() Then
                            AppLogInfo("MDS Cheque-Command AddMore Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cheque-Command AddMore Failed")
                        End If

                    End If

                Case CurrentState.RefundCash
                    intCurrentState = intState

                    If LastMDSInteract = LastMDSActivity.WantToInsertMore Then

                        If objMDSControl.UserReactNO() Then
                            AppLogInfo("MDS Cheque-Command Refund Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cheque-Command Refund Failed")
                        End If

                    ElseIf LastMDSInteract = LastMDSActivity.DepositItemFull Then

                        If objMDSControl.UserReactNO() Then
                            AppLogInfo("MDS Cheque-Command Refund Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cheque-Command Refund Failed")
                        End If

                    End If

                Case CurrentState.FinishDeposit

                    intCurrentState = intState

                    If LastMDSInteract = LastMDSActivity.WantToInsertMore Then

                        If objMDSControl.UserReactNO() Then
                            AppLogInfo("MDS Cheque-Command AddMore Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cheque-Command AddMore Failed")
                        End If

                    ElseIf LastMDSInteract = LastMDSActivity.DepositItemFull Then

                        'Bin Full
                        If objMDSControl.UserReactYES() Then
                            AppLogInfo("MDS Cheque-Command AddMore Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cheque-Command AddMore Failed")
                        End If

                    End If

                Case CurrentState.UserTimeoutST
                    intCurrentState = intState

                    If objMDSControl.UserReactTIMEOUT() Then
                        AppLogInfo("MDS Cheque-Command Timeout Success")
                        blnRtn = True
                    Else
                        AppLogErr("MDS Cheque-Command Timeout Failed")
                    End If

            End Select

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ActionOnWakeUp: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        Dim blnRtn As Boolean
        Try
            If objMDSControl.CheckDeviceStatus = True Then
                DiagDeviceStatusOK()
                AppLogErr("MDS Cheque-DiagnosticDevice Success")
                blnRtn = False
            Else
                DiagDeviceStatusError()
                AppLogErr("MDS Cheque-DiagnosticDevice Failed")
                blnRtn = True
            End If
            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-DiagnosticDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty

        Try
            'General Property - 'clean the data
            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = "0"  'always set to 1

            With udtChequeHWDStatus
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
                .strMStatusData = ""
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = "0"
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_WRAPDEVICE
                .mDeviceDataValue = "0000000000000000"
            End With

            'Clean Buffer Records
            udtChequeInserted = New ChequeDetailsDeposited
            TotalCheque = 0

            'Log Info
            AppLogInfo("MDS Cheque-WrapDevice iDeviceTrace ID:" & strGeniDeviceTrace)

            Return True
        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function

   
#End Region


#Region "Support Property"

    Public ReadOnly Property MDSChequeInBoxCounter As Integer
        Get
            Return objMDSControl.MDSChequeInBoxValue
        End Get
    End Property


#End Region

#Region "Ping Failed Control"

    ReadOnly Property MDSPingFailed() As Boolean
        Get
            Return blnMDSPingFailed
        End Get
    End Property


#End Region


#Region "Property - General"

    Property TxnStatus() As String
        Get
            Return udtChequeHWDStatus.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtChequeHWDStatus.strTxnStatus = value
        End Set
    End Property


    Property ErrorSeverity() As String
        Get
            Return udtChequeHWDStatus.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtChequeHWDStatus.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtChequeHWDStatus.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtChequeHWDStatus.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtChequeHWDStatus.strMStatus
        End Get
        Set(ByVal value As String)
            udtChequeHWDStatus.strMStatus = value
        End Set
    End Property


    Property MStatusData() As String
        Get
            Return udtChequeHWDStatus.strMStatusData
        End Get
        Set(ByVal value As String)
            udtChequeHWDStatus.strMStatusData = value
        End Set
    End Property

    'New - Interdeals - Device Interface Spec V0.5
    'New Property - Public Shadows Property Enabled()

    Public Shadows Property Enabled() As Boolean
        Get
            Return blnChqEnable
        End Get
        Set(ByVal value As Boolean)
            blnChqEnable = value
        End Set
    End Property

    'New Property - TimeoutInterval

    Public Property TimeoutInterval(ByVal ConfigID As Long) As Long
        Get
            Select Case ConfigID
                Case 1
                    Return objMDSControl.InsertItemTimeout
                Case 2
                    Return objMDSControl.TakeReturnedItemTimeout
                Case 3
                    Return objMDSControl.RepositionItemTimeout
                Case 4
                    Return objMDSControl.CleanReturnFeederTimeout
                Case 5
                    Return objMDSControl.MachineStartupInterval
                Case Else
                    Return 0
            End Select
        End Get
        Set(ByVal value As Long)
            Select Case ConfigID
                Case 1
                    objMDSControl.InsertItemTimeout = value
                Case 2
                    objMDSControl.TakeReturnedItemTimeout = value
                Case 3
                    objMDSControl.RepositionItemTimeout = value
                Case 4
                    objMDSControl.CleanReturnFeederTimeout = value
                Case 5
                    objMDSControl.MachineStartupInterval = value
            End Select
        End Set
    End Property


#End Region

#Region "Event"

    Public Sub ChqInsertedEscrowedValidated(ByVal ChqList As clsMDSModuleControl.TrxChqList)
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strDeviceDataValue As String = String.Empty

        Try
            strProTxnStatus = CurrentState.Escrowed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            udtChequeList = New clsMDSModuleControl.TrxChqList
            udtChequeList = ChqList

            For i = 0 To udtChequeList.ChequeList.Length - 1
                If udtChequeList.ChequeList(i).ChequeCodeline <> Nothing Then
                    strDeviceDataValue = strDeviceDataValue & udtChequeList.ChequeList(i).ChequeCodeline & "|"
                End If
            Next

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Escrowed
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = strDeviceDataValue
            End With

            AppLogInfo("MDS Cheque-ChqInsertedEscrowedValidated Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChqInsertedEscrowedEvt:" & ex.Message)
        End Try
    End Sub

    Public Sub ChqInsertedEscrowedValidatedItemFULL(ByVal ChqList As clsMDSModuleControl.TrxChqList)
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strDeviceDataValue As String = String.Empty

        Try
            strProTxnStatus = CurrentState.Escrowed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            udtChequeList = New clsMDSModuleControl.TrxChqList
            udtChequeList = ChqList

            For i = 0 To udtChequeList.ChequeList.Length - 1
                If udtChequeList.ChequeList(i).ChequeCodeline <> Nothing Then
                    strDeviceDataValue = strDeviceDataValue & udtChequeList.ChequeList(i).ChequeCodeline & "|"
                End If
            Next

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.DepositItemFull
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = strDeviceDataValue
            End With

            AppLogInfo("MDS Cheque-ChqInsertedEscrowedValidatedItemFull Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChqInsertedEscrowedValidatedItemFull:" & ex.Message)
        End Try
    End Sub


    Public Sub ChequeRejectComplete()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Dim dblTmpDepositAmt As Double = 0.0

        Try
            strProTxnStatus = CurrentState.TransComplete
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            TotalCheque = 0

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.TransComplete
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cheque-ChequeRejectComplete Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChequeRejectComplete:" & ex.Message)
        End Try
    End Sub

    Public Sub ChequeInsertComplete(ByVal ChqList As clsMDSModuleControl.TrxChqList)
        Dim strGeniDeviceTrace As String = ""
        Dim strDeviceDataValue As String = ""

        Dim strImgPath As String = ""
        Dim strChqStatus As String = ""

        Try
            strProTxnStatus = CurrentState.TransComplete
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            udtChequeList = New clsMDSModuleControl.TrxChqList
            udtChequeList = ChqList

            For i = 0 To udtChequeList.ChequeList.Length - 1

                If udtChequeList.ChequeList(i).ChequeStat = CurrChequeStatus.EscrowedGood Or udtChequeList.ChequeList(i).ChequeStat = CurrChequeStatus.Stored Then
                    'strDeviceDataValue = strDeviceDataValue & udtChequeList.ChequeList(i).ChequeCodeline & Chr(28) & udtChequeList.ChequeList(i).ChequeImagePath & Chr(29)

                    strImgPath = udtChequeList.ChequeList(i).ChequeImagePath
                    strChqStatus = udtChequeList.ChequeList(i).ChequeStat
                    strImgPath = strImgPath.Trim
                    strChqStatus = strChqStatus.Trim

                    AppLogInfo("Chq Status=" & strChqStatus & "-ImgPath=" & strImgPath)

                    If strImgPath.Length > 0 Then
                        strDeviceDataValue = strDeviceDataValue & udtChequeList.ChequeList(i).ChequeCodeline & "," & udtChequeList.ChequeList(i).ChequeImagePath & "|"
                    End If

                End If

            Next

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.TransComplete
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = strDeviceDataValue
            End With

            AppLogInfo("MDS Cheque-ChequeInsertComplete Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)

            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChequeDepositCompleteEvt:" & ex.Message)
        End Try
    End Sub

    Public Sub ChequeInsertProcessing()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.ProcessingCash
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.ProcessingCash
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cheque-ChequeInsertProcessing Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChequeInsertProcessing:" & ex.Message)
        End Try
    End Sub

    Public Sub ChequeInsertReposition()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.RejectCash
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.RepositionDoc
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cheque-ChequeInsertReposition Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChequeInsertReposition:" & ex.Message)
        End Try
    End Sub

    Public Sub ChequeInsertPutItemInFeeder()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.OpenFeeder
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.OpenFeeder
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cheque-ChequeInsertPutItemInFeeder Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChequeInsertPutItemInFeeder:" & ex.Message)
        End Try
    End Sub

    Public Sub ChequeInsertEscrowed()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.Escrowed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Escrowed
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cheque-ChequeInsertEscrowed Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChequeInsertEscrowed:" & ex.Message)
        End Try
    End Sub

    Public Sub ErrorHWD()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty

        Try
            strProTxnStatus = CurrentState.Close
            strProErrSeverity = 1
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Close
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_ERROR
                .mDeviceDataValue = 0
            End With

            AppLogInfo("MDS Cheque-ErrorHWD Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ErrorHWD:" & ex.Message)
        End Try
    End Sub

    Public Sub InitializeHWDOK()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty

        Try
            strProTxnStatus = CurrentState.Close
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Close
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = 0
            End With

            AppLogInfo("MDS Cheque-InitializeHWDOK Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-InitializeHWDOK:" & ex.Message)
        End Try
    End Sub

    Public Sub ChequeInsertRejected()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.RejectCash
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.RejectCash
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cheque-ChequeInsertRejected Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChequeInsertRejected:" & ex.Message)
        End Try
    End Sub

    Public Sub ChequeInsertTimeout()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try



            strProTxnStatus = CurrentState.Close
            strProErrSeverity = 1
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Close
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_TIMEOUT
                .mDeviceDataValue = Nothing
            End With

            AppLogInfo("MDS Cheque-ChequeInsertTimeout Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceTimeout(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-CashInsertRejected:" & ex.Message)
        End Try
    End Sub

    Public Sub DiagDeviceStatusError()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty

        Try
            strProTxnStatus = DVST_ERRIN_DEVSTATE
            strProErrSeverity = ERST_CDERR
            strProSupplyStatus = DGST_STATUS
            strProDignosticStatus = SYST_STATUS_NOSTATE

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DVST_ERRIN_DEVSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = Nothing
            End With

            AppLogInfo("MDS Cheque-DiagDeviceStatusError Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-DiagDeviceStatusError:" & ex.Message)
        End Try
    End Sub

    Public Sub DiagDeviceStatusOK()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty

        Try
            strProTxnStatus = DVST_ERRIN_DEVSTATE
            strProErrSeverity = ERST_NOERR
            strProSupplyStatus = DGST_STATUS
            strProDignosticStatus = SYST_STATUS_NOSTATE

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DVST_ERRIN_DEVSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = Nothing
            End With

            AppLogInfo("MDS Cheque-DiagDeviceStatusOK Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-DiagDeviceStatusOK:" & ex.Message)
        End Try
    End Sub

    Public Sub ChequeCleanFeeder()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.RejectCash
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtChequeHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = ChequeDeviceGraphicId
                .strDeviceName = ChequeDeviceName
            End With

            strGeniDeviceTrace = ChequeDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.CleanFeederDoc
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cheque-ChequeCleanFeeder Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in ChequeDepositor-ChequeInsertRejected:" & ex.Message)
        End Try
    End Sub


#End Region


#Region "Timer Control"
    Private Sub MDSTImerControl_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles MDSStartupTimerControl.Elapsed
        MDSStartupTimerControl.Stop()
        If objMDSControl.MDSControlStatus <> clsMDSModuleControl.ControlMDSStatusOptions.OK Then
            If intCheckCount < 3 Then
                intCheckCount = intCheckCount + 1
                MDSStartupTimerControl.Start()
            Else
                AppLogErr("MDS Cheque - ChequeDepositor: MDSControl NotOK: " & objMDSControl.StatusText)
                ErrorHWD()
            End If
        Else
            AppLogInfo("ChequeDepositor-StartupWatcher End")
        End If
    End Sub

    'Private Sub MDSReturnTimerControl_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles MDSReturnTimerControl.Elapsed
    '    MDSReturnTimerControl.Stop()
    '    If LastMDSInteract = LastMDSActivity.TakeReturnedItem Then
    '        If (intCurrentState = CurrentState.RefundCash) Or (intCurrentState = CurrentState.OpenFeeder) Then

    '            AppLogInfo("Event Received -ChequeDepositor : User Timeout Sent")
    '            ChequeInsertTimeout()

    '        End If
    '    End If
    'End Sub

#End Region

#Region "MDS Control"


    Private Sub objMDSControl_MDSChequeTimeout() Handles objMDSControl.MDSChequeTimeout
        Try
            AppLogInfo("MDS Cheque - Event Received : Cheque Insert Timeout")
            ChequeInsertTimeout()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSChequeTimeout:" & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSChequeTransactionCompletedEvt(ByVal ChqList As MDSModuleControl.clsMDSModuleControl.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSChequeTransactionCompletedEvt
        Try

            'MsgBox("objMDSControl_MDSChequeTransactionCompletedEvt=" & intCurrentState)

            AppLogInfo("MDS Cheque - Event Received : Trans Complete")
            LastMDSInteract = LastMDSActivity.CompletedTransaction

            If intCurrentState = CurrentState.RefundCash Then
                ChequeRejectComplete()
            ElseIf intCurrentState = CurrentState.FinishDeposit Then
                ChequeInsertComplete(ChqList)
            End If

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSChequeTransactionCompletedEvt:" & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSChequeTransactionFailedEvt(ByVal ChqList As MDSModuleControl.clsMDSModuleControl.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSChequeTransactionFailedEvt
        Try
            AppLogInfo("MDS Cheque - Event Received : Trans Failed")
            LastMDSInteract = LastMDSActivity.TransactionCompleteFail

            'Hardware Error Detected
            ErrorHWD()

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSChequeTransactionFailedEvt:" & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSCleanFeederEvt() Handles objMDSControl.MDSChequeCleanFeederEvt
        Try
            AppLogInfo("MDS Cheque - Event Received : Clean Feeder")
            LastMDSInteract = LastMDSActivity.cleanFeeder
            'MsgBox("MDS Cheque Clean Feeder")
            ChequeCleanFeeder()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSCleanFeederEvt:" & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSControlErrorReceived(ByVal strExMsg As String) Handles objMDSControl.MDSControlErrorReceived
        Try
            AppLogInfo("MDS Cheque - Error received : " & strExMsg.Trim)
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSControlErrorReceived: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSInsertItemsEvt() Handles objMDSControl.MDSChequeInsertItemsEvt
        Try
            AppLogInfo("MDS Cheque - Event Received : Insert Item")
            LastMDSInteract = LastMDSActivity.insertItem
            ChequeInsertPutItemInFeeder()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSInsertItemsEvt: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSNotifyCounterfeitEvt() Handles objMDSControl.MDSChequeNotifyCounterfeitEvt
        Try
            AppLogInfo("MDS Cheque - Event Received : Notifiy Counterfit")
            LastMDSInteract = LastMDSActivity.NotifyCounterfit
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSNotifyCounterfeitEvt: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSProcessingEvt() Handles objMDSControl.MDSChequeProcessingEvt
        Try
            AppLogInfo("MDS Cheque - Event Received : Processing")
            LastMDSInteract = LastMDSActivity.Processing
            ChequeInsertProcessing()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSProcessingEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSRepositionDocumentsEvt() Handles objMDSControl.MDSChequeRepositionDocumentsEvt
        Try
            AppLogInfo("MDS Cheque - Event Received : Reposition")
            'MsgBox("Cheque Reposition")
            LastMDSInteract = LastMDSActivity.RepositionDocuments
            ChequeInsertReposition()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSRepositionDocumentsEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSStatusUpdateEvt(ByVal MDSCurrentState As MDSModuleControl.clsMDSModuleControl.ControlMDSCurrentRunningState, ByVal MDSStatus As MDSModuleControl.clsMDSModuleControl.ControlMDSStatusOptions, ByVal strStatusText As String) Handles objMDSControl.MDSChqeueStatusUpdateEvt
        Try

            AppLogInfo("MDS Cheque - Status Update:" & MDSCurrentState & "," & MDSStatus & "," & strStatusText)

            'New Control
            CheckMDSAutoReconnectFlag(strStatusText)

            If MDSStatus = clsMDSModuleControl.ControlMDSStatusOptions.NotOK Then
                ErrorHWD()
            ElseIf MDSStatus = clsMDSModuleControl.ControlMDSStatusOptions.OK Then
                blnInitializeFlag = False
                blnDoorSensor = True
                InitializeHWDOK()
            End If


            'If blnInitializeFlag = True Then
            '    If MDSStatus = clsMDSModuleControl.ControlMDSStatusOptions.NotOK Then
            '        intCheckCount = intCheckCount + 1

            '        If intCheckCount > 2 Then
            '            AppLogErr("MDSControl NotOK: " & objMDSControl.StatusText)
            '            ErrorHWD()
            '        ElseIf strStatusText.Contains(MDS_ERROR_STATE) Or strStatusText.Contains(MDS_ERRORSTATE_SAFEDOOR) Then
            '            ErrorHWD()
            '        End If

            '    ElseIf MDSStatus = clsMDSModuleControl.ControlMDSStatusOptions.OK Then
            '        blnInitializeFlag = False
            '        blnDoorSensor = True
            '        InitializeHWDOK()
            '    End If
            'ElseIf strStatusText.Contains(MDS_ERROR_STATE) Then
            '    ErrorHWD()
            'End If

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSStatusUpdateEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSTakeReturnedItemsEvt() Handles objMDSControl.MDSChequeTakeReturnedItemsEvt
        Try


            AppLogInfo("MDS Cheque - Event Received : Take Return Item")

            'MsgBox("Take Reject Item")

            LastMDSInteract = LastMDSActivity.TakeReturnedItem
            ChequeInsertRejected()

            'If intCurrentState = CurrentState.RefundCash Or intCurrentState = CurrentState.OpenFeeder Then
            '    MDSReturnTimerControl = New Timers.Timer
            '    MDSReturnTimerControl.Interval = (objMDSControl.CleanReturnFeederTimeout * 1000) - 1000
            '    MDSReturnTimerControl.Start()
            'End If

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSTakeReturnedItemsEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSWantToInsertMoreChequeQuestionEvt(ByVal ChequeList As MDSModuleControl.clsMDSModuleControl.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSWantToInsertMoreChequeQuestionEvt
        Try


            AppLogInfo("MDS Cheque - Event Received : Insert More Cheque")

            ' MsgBox("Insert More Cheques Int ChqCounter:" & intChqCount)

            If intChqCount <> 0 Then
                ChqInsertedEscrowedValidated(ChequeList)
            ElseIf bolCancelTrans = True Then
                objMDSControl.UserReactNO()
                ChequeRejectComplete()
                bolCancelTrans = False
            Else
                objMDSControl.UserReactYES()
                ChequeInsertPutItemInFeeder()
            End If
            LastMDSInteract = LastMDSActivity.WantToInsertMore

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSWantToInsertMoreChequeQuestionEvt: " & ex.Message)
        End Try

    End Sub


#End Region

    Private Sub objMDSControl_MDSChqeueCompleteDepositQuestionEvt(ByVal ChequeList As MDSModuleControl.clsMDSModuleControl.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSChqeueCompleteDepositQuestionEvt
        Try
            AppLogInfo("MDS Cheque - Event Received : Complete Deposit Question. CurrState=" & intCurrentState & "-" & CompleteQuestionStatusDesc(intCurrentState))

            If intCurrentState = CurrentState.Close Then

                If objMDSControl.UserReactNO() Then
                    AppLogInfo("Refund Success")
                Else
                    AppLogInfo("Refund Failed")
                End If

                LastMDSInteract = LastMDSActivity.CompleteDeposit

            ElseIf intCurrentState = CurrentState.RefundCash Then

                If objMDSControl.UserReactNO() Then
                    AppLogInfo("Refund Success")

                    'Clean Inserted amount and denomination
                    TotalCheque = 0
                    udtChequeInserted = New ChequeDetailsDeposited
                Else
                    AppLogInfo("Refund Failed")
                End If

                LastMDSInteract = LastMDSActivity.CompleteDeposit

            ElseIf intCurrentState = CurrentState.FinishDeposit Then

                If objMDSControl.UserReactYES Then
                    AppLogInfo("Deposit Success")
                Else
                    AppLogInfo("Deposit Failed")
                End If

                LastMDSInteract = LastMDSActivity.CompleteDeposit
            Else

                'Reject - Hit the maximum 
                If intChqCount <> 0 Then
                    ChqInsertedEscrowedValidatedItemFULL(ChequeList)
                Else
                    objMDSControl.UserReactYES()
                    ChequeInsertPutItemInFeeder()
                End If

                LastMDSInteract = LastMDSActivity.DepositItemFull

            End If

        Catch ex As Exception
            AppLogErr("Error in MDSCompleteDepositQuestionEvt: " & ex.Message)
        End Try

    End Sub


    Public Function CompleteQuestionStatusDesc(ByVal intStateCD As String) As String
        Dim strReplyCD As String = ""

        Try

            Select Case intStateCD

                Case CurrentState.Close '0
                    strReplyCD = "Close"

                Case CurrentState.OpenFeeder  '1
                    strReplyCD = "OpenFeeder"

                Case CurrentState.InsertMore  '2
                    strReplyCD = "InsertMore"

                Case CurrentState.RejectCash '3
                    strReplyCD = "RejectCash"

                Case CurrentState.RefundCash '4
                    strReplyCD = "RefundCash"

                Case CurrentState.Escrowed  '5
                    strReplyCD = "Escrowed"

                Case CurrentState.ProcessingCash '6
                    strReplyCD = "ProcessingCash"

                Case CurrentState.FinishDeposit  '7
                    strReplyCD = "FinishDeposit"

                Case CurrentState.TransComplete '8
                    strReplyCD = "TransComplete"

                Case CurrentState.Supervisor '9
                    strReplyCD = "Supervisor"

                Case CurrentState.UserTimeoutST   '10
                    strReplyCD = "UserTimeoutST"

                Case CurrentState.DepositItemFull ' 11
                    strReplyCD = "DepositItemFull"

                Case CurrentState.RepositionDoc '12
                    strReplyCD = "RepositionDoc"

                Case CurrentState.CleanFeederDoc '13
                    strReplyCD = "CleanFeederDoc"
            End Select

            Return strReplyCD
        Catch ex As Exception
            AppLogErr("Error in CompleteQuestionStatusDesc: " & ex.Message)
            Return "SysErrUnknow"
        End Try
    End Function



#Region "MDS Auto Reconnect Validation"

    Private Function CheckMDSAutoReconnectFlag(ByVal strInputValue As String) As Boolean
        Dim strINIFileValue As String = ""
        Dim ArrValue() As String = Nothing
        Dim strTmpValue As String = ""
        Dim intRec As Integer

        Try

            strInputValue = strInputValue.Trim
            'AppLogInfo("CheckMDSAutoReconnectFlag InputText:" & strInputValue)

            strINIFileValue = objMDSControl.MDSErrStatusReplyCFG
            strINIFileValue = strINIFileValue.Trim
            'AppLogInfo("CheckMDSAutoReconnectFlag CheckFlagValue:" & strINIFileValue)

            If strInputValue.Length > 0 And strINIFileValue.Length > 0 Then

                ArrValue = Split(strINIFileValue, "|", -1, CompareMethod.Text)

                If ArrValue.Length > 0 Then
                    For intRec = 0 To ArrValue.Length - 1
                        strTmpValue = ArrValue(intRec)
                        strTmpValue = strTmpValue.Trim

                        If strInputValue = strTmpValue Then
                            'AppLogInfo("MDS Auto Reconnect Flag Match")
                            blnMDSPingFailed = True
                            AppLogWarn("MDS Cash Ping Failed. StatusFlag=" & blnMDSPingFailed)
                            Return True
                        End If
                    Next intRec
                Else
                    Return False
                End If

            End If

            Return False
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".CheckMDSAutoReconnectFlag:" & ex.Message)
            Return False
        End Try
    End Function


#End Region

   
End Class
