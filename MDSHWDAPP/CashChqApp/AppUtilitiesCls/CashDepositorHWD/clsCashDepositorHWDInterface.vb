Imports System
Imports MDSModuleControl
Imports MDSXFSWrapper
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class clsCashDepositorHWDInterface

#Region "Hardware Object - Variable"

    Public WithEvents objMDSControl As New clsMDSModuleControl

#End Region

#Region "MIX MODE"

    Dim TotalCheque As Integer
    Structure ChequeDetailsDeposited
        Dim ChequeCountNumber As Integer
    End Structure

    Public udtChequeInserted As ChequeDetailsDeposited


    Dim blnMIXMODESelected As Boolean

    Property SetMixModeOn() As Boolean
        Get
            Return blnMIXMODESelected
        End Get
        Set(ByVal value As Boolean)
            blnMIXMODESelected = value
        End Set
    End Property

    Public Function SetMixModeOnFunc(ByVal blnSETNO As Boolean) As Boolean
        Try
            'SetPropertiesCashTransaction()
            blnMIXMODESelected = blnSETNO
            AppLogInfo("MDS Cash -SetMixModeOnFunc:" & blnSETNO)
            Return True
        Catch ex As Exception
            AppLogErr("Error in CashDepositSetMixModeOnFunc: " & ex.Message)
            Return False
        End Try
    End Function


#End Region

#Region "Class Variables"

    Dim strTitle As String = "clsCashDepositorHWDInterface"

    Dim TotalAmount As Double

    Dim LastMDSInteract As LastMDSActivity
    Dim WithEvents MDSStartupTimerControl As System.Timers.Timer
    'Dim WithEvents MDSReturnTimerControl As System.Timers.Timer

    Dim intCheckCount As Integer
    Dim blnInitializeFlag As Boolean = False
    Dim blnDoorSensor As Boolean = False

    Dim blnCashEnabled As Boolean = False

    'Hardware CashDepositor Const Value
    Private CashDepositorDeviceGraphicId As String
    Private CashDepositorDeviceName As String

    'CashDepositor Const Event Type
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


#Region "CASHDEPOSITOR Variable"

    Structure CashDepositorHWDSTATUS
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure

    Public udtCashDepositorHWDStatus As CashDepositorHWDSTATUS

    Structure NoteInserted
        Dim RM1DenominationCnt As Integer
        Dim RM5DenominationCnt As Integer
        Dim RM10DenominationCnt As Integer
        Dim RM20DenominationCnt As Integer
        Dim RM50DenominationCnt As Integer
        Dim RM100DenominationCnt As Integer
    End Structure

    Public udtNoteInserted As NoteInserted

#End Region

#Region "Instance Control"

    Private m_singleInstance As clsCashDepositorHWDInterface = Nothing

    Public Function SingleInstance() As clsCashDepositorHWDInterface
        If m_singleInstance Is Nothing Then
            m_singleInstance = New clsCashDepositorHWDInterface()
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

            'Init the Cash Logger
            InitCashDepositorHWDLogger()

            'Init the MDS XFS Logger
            objMDSControl.InitializeMDSXFSLogger()

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Support Property"

    ReadOnly Property EnableMDSOpt() As Boolean
        Get
            Return objMDSControl.EnableMDSOptHWD
        End Get
    End Property

    ReadOnly Property MDSComportNo() As String
        Get
            Return objMDSControl.MDSComportNoHWD
        End Get
    End Property

    ReadOnly Property ForeAcceptChqOpt() As Boolean
        Get
            Return objMDSControl.ForeAcceptChqOptHWD
        End Get
    End Property

    ReadOnly Property MDSInsertItemTimeout() As String
        Get
            Return objMDSControl.MDSInsertItemTimeoutHWD
        End Get
    End Property

    ReadOnly Property MDSCleanFeederTimeout() As String
        Get
            Return objMDSControl.MDSCleanFeederTimeoutHWD
        End Get
    End Property

    ReadOnly Property MDSRepositionDocTimeout() As String
        Get
            Return objMDSControl.MDSRepositionDocTimeoutHWD
        End Get
    End Property

    ReadOnly Property MDSTakeReturnItemTimeout() As String
        Get
            Return objMDSControl.MDSTakeReturnItemTimeoutHWD
        End Get
    End Property

    ReadOnly Property MDSXFSLogPath() As String
        Get
            Return objMDSControl.MDSXFSLogPathHWD
        End Get
    End Property

    ReadOnly Property MDSChqImagePath() As String
        Get
            Return objMDSControl.MDSChqImagePathHWD
        End Get
    End Property

    ReadOnly Property MDSChqTemplatePath() As String
        Get
            Return objMDSControl.MDSChqTemplatePathHWD
        End Get
    End Property

#End Region





#Region "Update MDS Setting"

    Public Function UpdateMDSSetting(ByVal strEnableOpt As String, ByVal strForeChqOpt As String, ByVal strComportNo As String, ByVal strInsertItemTMOut As String, ByVal strRepositionTMOut As String, ByVal strCleanFeederTMOut As String, ByVal strTakeReturnTMOut As String, ByVal strTraceLogPath As String, ByVal strChqImagePath As String, ByVal strChqTemplatePath As String) As Boolean
        Try
            If objMDSControl.UpdateMDSHwdLayerParamSetting(strEnableOpt, strForeChqOpt, strComportNo, strInsertItemTMOut, strRepositionTMOut, strCleanFeederTMOut, strTakeReturnTMOut, strTraceLogPath, strChqImagePath, strChqTemplatePath) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region



#Region "1. Init CASH Depositor Hardware Logger"

    Public Sub InitCashDepositorHWDLogger()
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
                strLogEvt = "MDS - CASH Hookup Class Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger INI Path:" & strLogIniPath)

            Else
                'MSD - CASH Init Failed
            End If

        Catch ex As Exception
            AppLogErr("Error in InitCashDepositorHWDLogger:" & ex.Message)
        End Try
    End Sub


#End Region


#Region "Support Function - SetCashHWDLayerProperty"

    Public Sub SetCashHWDLayerProperty()
        Try
            With objMDSControl.CashDepositorDeviceCode

                'DEVICE_GRAPHIC_ID = "D"
                'DEVICE_NAME = "CASHDEPOSITORY"

                CashDepositorDeviceGraphicId = .CASHDEVDeviceID
                CashDepositorDeviceName = .CASHDEVDeviceName

                EVTTYPE_WRAPDEVICE = .CASHDEVEventWrap
                EVTTYPE_TIMEOUT = .CASHDEVEventTimeout
                EVTTYPE_DATAARRIVED = .CASHDEVEventDataArrive
                EVTTYPE_ERROR = .CASHDEVEventError

                DVST_ERRIN_DEVSTATE = .CASHDEVErrDevState
                ERST_NOERR = .CASHDEVErrorStateNoError
                ERST_CDERR = .CASHDEVErrorStateError
                ERST_CDFATAL = .CASHDEVErrorStateFatal
                ERST_CDWARN = .CASHDEVErrorStateWarning

                DGST_STATUS = .CASHDEVErrorDGSTStatus
                SYST_STATUS_NOSTATE = .CASHDEVErrorSysStatNoState

            End With

        Catch ex As Exception
            AppLogErr("Error in SetCashHWDLayerProperty:" & ex.Message)
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
            AppLogErr("Error In RunStartupWatcher :" & ex.Message)
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
            AppLogErr("Error in GenDateTimeStamp:" & ex.Message)
            Return "9999"
        End Try
    End Function


#End Region



#Region "MDS Light Control"

    Public Function StartMDSLight(ByVal strUserChoice As String) As Boolean
        Dim blnRtn As Boolean = False
        Try
            If objMDSControl.StartMDSLightHWD(strUserChoice) Then
                AppLogInfo("MDS Cash-StartMDSLight Success User Choice:" & strUserChoice)
                blnRtn = True
            Else
                AppLogWarn("MDS Cash-StartMDSLight Failed User Choice:" & strUserChoice)
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in CashDepositor-StartMDSLight: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSLight() As Boolean
        Dim blnRtn As Boolean = False
        Try
            If objMDSControl.StopMDSLightHWD Then
                AppLogInfo("MDS Cash-StopMDSLight Success")
                blnRtn = True
            Else
                AppLogWarn("MDS Cash-StopMDSLight Failed")
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in Cash-StopMDSLight: " & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "MDS Card Reader Control"

    Public Function StartMDSReaderLight(ByVal strUserChoice As String) As Boolean
        Dim blnRtn As Boolean = False
        Try
            If objMDSControl.StartMDSReaderLightHWD(strUserChoice) Then
                AppLogInfo("MDS Cash-StartMDSReaderLight Success User Choice:" & strUserChoice)
                blnRtn = True
            Else
                AppLogWarn("MDS Cash-StartMDSReaderLight Failed User Choice:" & strUserChoice)
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in CashDepositor-StartMDSReaderLight: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSReaderLight() As Boolean
        Dim blnRtn As Boolean = False
        Try
            If objMDSControl.StopMDSReaderLightHWD Then
                AppLogInfo("MDS Cash-StopMDSReaderLight Success")
                blnRtn = True
            Else
                AppLogWarn("MDS Cash-StopMDSReaderLight Failed")
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in CashDepositor-StopMDSReaderLight: " & ex.Message)
            Return False
        End Try
    End Function


#End Region


#Region "Method - StartDevice, StopDevice "


    Public Function StartDevice() As Boolean
        Dim blnRtn As Boolean = False
        Try

            'Ping Failed
            blnMDSPingFailed = False


            If objMDSControl.InitializeMDS() Then

                'Set the Cash Property - Default Value
                SetCashHWDLayerProperty()
                intCheckCount = 0
                blnInitializeFlag = True
                blnRtn = True
                AppLogInfo("MDS Cash - StartDevice Success")
            Else
                AppLogErr("MDS Cash - StartDevice Fail")
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in CashDeposit-StartDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Try

            'Ping Failed
            blnMDSPingFailed = False


            'Stop MDS
            objMDSControl.StopMDS()

            'Close the Logger
            CloseLog()

            Return True
        Catch ex As Exception
            AppLogErr("Error in CashDeposit-StopDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Dim blnRtn As Boolean = False
        Try
            'If objMDSControl.StartMDSLightHWD Then
            '    AppLogInfo("MDS Cash -UnlockDevice Success")
            '    blnRtn = True
            'Else
            '    AppLogInfo("MDS Cash -UnlockDevice Failed")
            '    blnRtn = False
            'End If
            'Return blnRtn

            Return True

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-UnlockDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Dim blnRtn As Boolean = False
        Try
            'If objMDSControl.StopMDSLightHWD Then
            '    AppLogInfo("MDS Cash -LockDevice Success")
            '    blnRtn = True
            'Else
            '    AppLogInfo("MDS Cash -LockDevice Failed")
            '    blnRtn = False
            'End If
            'Return blnRtn

            Return True

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-LockDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal intState As Integer) As Boolean
        Dim blnRtn As Boolean
        Try
            'SetPropertiesCashTransaction()
            blnRtn = ActionOnWakeUp(intState)

            AppLogInfo("MDS Cash -WakeupDevice State:" & intState & " Reply:" & blnRtn)
            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in CashDeposit-WakeUpDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub SetPropertiesCashTransaction()
        Try
            objMDSControl.SetCurrentTransCash()
        Catch ex As Exception
            AppLogErr("Error in CashDeposit-SetPropertiesCashTransaction: " & ex.Message)
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

                            AppLogInfo("MDS Cash -Stop Transaction Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cash -Stop Transaction Failed")
                        End If
                    Else
                        If objMDSControl.StopMDSTransaction Then
                            AppLogInfo("MDS Cash -Stop Transaction Success")
                            blnRtn = True
                        Else
                            AppLogErr("MDS Cash -Stop Transaction Failed")
                        End If
                    End If

                Case CurrentState.OpenFeeder
                    intCurrentState = intState
                    Enabled = True

                    If blnCashEnabled = True Then

                        AppLogInfo("MDS Cash OpenFeeder Control. MixMode=" & blnMIXMODESelected)

                        If blnMIXMODESelected = False Then
                            If objMDSControl.StartMDSTransaction(clsMDSModuleControl.TransactionType.CashMode) Then
                                bolTransStart = True
                                AppLogInfo("MDS Cash -Start Transaction Success")
                                blnRtn = True
                            Else
                                AppLogErr("MDS Cash -Start Transaction Failed")
                            End If
                        Else
                            If objMDSControl.StartMDSTransaction(clsMDSModuleControl.TransactionType.CashChequeMode) Then
                                bolTransStart = True
                                AppLogInfo("MDS CashNChq -Start Transaction Success")
                                blnRtn = True
                            Else
                                AppLogErr("MDS CashNChq -Start Transaction Failed")
                            End If
                        End If
                    Else
                        AppLogErr("MDS Cash -StartDevice Fail - Enable False")
                        blnRtn = False
                    End If

                Case CurrentState.InsertMore
                        If LastMDSInteract = LastMDSActivity.WantToInsertMore Then

                            If objMDSControl.UserReactYES() Then
                                AppLogInfo("MDS Cash -Command AddMore Success")
                                blnRtn = True
                            Else
                                AppLogErr("MDS Cash -Command AddMore Failed")
                            End If
                        End If


                Case CurrentState.RefundCash
                        intCurrentState = intState

                        If LastMDSInteract = LastMDSActivity.WantToInsertMore Then

                            If objMDSControl.UserReactNO() Then
                                AppLogInfo("MDS Cash -Command RefundCash Success")
                                blnRtn = True
                            Else
                                AppLogErr("MDS Cash -Command RefundCash Failed")
                            End If

                        ElseIf LastMDSInteract = LastMDSActivity.DepositItemFull Then

                            If objMDSControl.UserReactNO() Then
                                AppLogInfo("MDS Cash -Command RefundCash Success")
                                blnRtn = True
                            Else
                                AppLogErr("MDS Cash -Command RefundCash Failed")
                            End If

                        End If

                Case CurrentState.FinishDeposit
                        intCurrentState = intState

                        If LastMDSInteract = LastMDSActivity.WantToInsertMore Then

                            If objMDSControl.UserReactNO() Then
                                AppLogInfo("MDS Cash -Command FinishDeposit Success")
                                blnRtn = True
                            Else
                                AppLogErr("MDS Cash -Command FinishDeposit Failed")
                            End If

                        ElseIf LastMDSInteract = LastMDSActivity.DepositItemFull Then

                            'Bin Full
                            If objMDSControl.UserReactYES() Then
                                AppLogInfo("MDS Cash -Command AddMore Success")
                                blnRtn = True
                            Else
                                AppLogErr("MDS Cash -Command AddMore Failed")
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
            AppLogErr("Error in CashDeposit-ActionOnWakeUp: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        Dim blnRtn As Boolean
        Try
            If objMDSControl.CheckDeviceStatus = True Then
                DiagDeviceStatusOK()
                AppLogInfo("MDS Cash -Command DiagnosticDevice Success")
                blnRtn = False
            Else
                DiagDeviceStatusError()
                AppLogErr("MDS Cash -Command DiagnosticDevice Failed")
                blnRtn = True
            End If
            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in CashDeposit-DiagnosticDevice: " & ex.Message)
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

            With udtCashDepositorHWDStatus
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
                .strMStatusData = ""
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = "0"
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_WRAPDEVICE
                .mDeviceDataValue = "0000000000000000"
            End With

            'Clean Buffer Records
            udtNoteInserted = New NoteInserted
            TotalAmount = 0

            'Clean Buffer Records
            udtChequeInserted = New ChequeDetailsDeposited
            TotalCheque = 0

            'Log Info
            AppLogInfo("MDS Cash -WrapDevice iDeviceTrace ID:" & strGeniDeviceTrace)

            Return True
        Catch ex As Exception
            AppLogErr("Error in CashDeposit-WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Collection - GetCashCounter,ResetCashCounter"

    Public Function GetCashCounter() As Boolean
        Try
            objMDSControl.GetCashBoxCounter()
            AppLogInfo("MDS Cash -GetCashCounter")
            Return True
        Catch ex As Exception
            AppLogErr("Error in CashDeposit-GetCashCounter:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function ResetCashCounter() As Boolean
        Try
            objMDSControl.ResetCashBoxCounter()
            AppLogInfo("MDS Cash -ResetCashCounter")
            Return True
        Catch ex As Exception
            AppLogErr("Error in ResetCashCounter:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function ResetChequeCounter() As Boolean
        Try
            objMDSControl.ResetChequeBoxCounter()
            AppLogInfo("MDS Cheque -ResetChequeCounter")
            Return True
        Catch ex As Exception
            AppLogErr("Error in ResetChequeCounter:" & ex.Message)
            Return False
        End Try
    End Function


#End Region

#Region "Property - TxnStatus,ErrorSeverity"

    Property TxnStatus() As String
        Get
            Return udtCashDepositorHWDStatus.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtCashDepositorHWDStatus.strTxnStatus = value
        End Set
    End Property

    Property ErrorSeverity() As String
        Get
            Return udtCashDepositorHWDStatus.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtCashDepositorHWDStatus.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtCashDepositorHWDStatus.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtCashDepositorHWDStatus.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtCashDepositorHWDStatus.strMStatus
        End Get
        Set(ByVal value As String)
            udtCashDepositorHWDStatus.strMStatus = value
        End Set
    End Property

    Property MStatusData() As String
        Get
            Return udtCashDepositorHWDStatus.strMStatusData
        End Get
        Set(ByVal value As String)
            udtCashDepositorHWDStatus.strMStatusData = value
        End Set
    End Property


#End Region

#Region "Property -  Amount,Enabled,TimeoutInterval"

    Public Function Amount() As Double
        Return TotalAmount
    End Function

    'New - Interdeals - Device Interface Spec V0.5
    'New Property - Public Shadows Property Enabled()

    Public Shadows Property Enabled() As Boolean
        Get
            Return blnCashEnabled
        End Get
        Set(ByVal value As Boolean)
            blnCashEnabled = value
        End Set
    End Property

    'New Property - TimeoutInterval

    Public Property TimeoutInterval(ByVal ConfigID As Long) As Long
        Get

            '1:InsertTimeout , 2:TakeReturnedItemTimeout , 3:RepositionItemTimeout, 4:CleanReturnFeederTimeout, 5:MachineStartupInterval

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






#Region "Event EvtDeviceDataReady - CashInsertEscrowedValidated"

    Public Sub CashInsertEscrowedValidated(ByVal NoteList As clsMDSModuleControl.TrxNoteList)
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Dim dblTmpDepositAmt As Double
        Dim strCurrencyType As String = ""

        Try
            strProTxnStatus = CurrentState.Escrowed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            udtNoteList = New clsMDSModuleControl.TrxNoteList
            udtNoteList = NoteList

            TotalAmount = 0.0

            For i = 0 To udtNoteList.NoteInfo.Length - 1

                If udtNoteList.NoteInfo(i).bGoodNote Then
                    intCountPcs = udtNoteList.NoteInfo(i).NoteCount
                    intCurrencyVal = udtNoteList.NoteInfo(i).NoteValue
                    strCurrencyType = udtNoteList.NoteInfo(i).NoteCurrency
                    dblTmpDepositAmt = intCountPcs * intCurrencyVal

                    'strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & Chr(28) & intCountPcs.ToString & Chr(29)
                    strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & "," & intCountPcs.ToString & "," & strCurrencyType.Trim & "|"

                    TotalAmount = TotalAmount + dblTmpDepositAmt
                End If

                dblTmpDepositAmt = 0

                Select Case intCurrencyVal
                    Case 1
                        udtNoteInserted.RM1DenominationCnt = intCountPcs
                    Case 5
                        udtNoteInserted.RM5DenominationCnt = intCountPcs
                    Case 10
                        udtNoteInserted.RM10DenominationCnt = intCountPcs
                    Case 20
                        udtNoteInserted.RM20DenominationCnt = intCountPcs
                    Case 50
                        udtNoteInserted.RM50DenominationCnt = intCountPcs
                    Case 100
                        udtNoteInserted.RM100DenominationCnt = intCountPcs
                End Select

                intCountPcs = 0
                intCurrencyVal = 0
            Next

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Escrowed
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = strDeviceDataValue
            End With

            AppLogInfo("MDS Cash -CashInsertEscrowedValidated Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertEscrowedValidated:" & ex.Message)
        End Try
    End Sub

    Public Sub CashInsertEscrowedValidatedItemFull(ByVal NoteList As clsMDSModuleControl.TrxNoteList)
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Dim dblTmpDepositAmt As Double
        Dim strCurrencyType As String = ""

        Try
            strProTxnStatus = CurrentState.Escrowed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            udtNoteList = New clsMDSModuleControl.TrxNoteList
            udtNoteList = NoteList

            TotalAmount = 0.0

            For i = 0 To udtNoteList.NoteInfo.Length - 1

                If udtNoteList.NoteInfo(i).bGoodNote Then
                    intCountPcs = udtNoteList.NoteInfo(i).NoteCount
                    intCurrencyVal = udtNoteList.NoteInfo(i).NoteValue
                    strCurrencyType = udtNoteList.NoteInfo(i).NoteCurrency
                    dblTmpDepositAmt = intCountPcs * intCurrencyVal
                    'strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & Chr(28) & intCountPcs.ToString & Chr(29)
                    strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & "," & intCountPcs.ToString & "," & strCurrencyType & "|"

                    TotalAmount = TotalAmount + dblTmpDepositAmt
                End If

                dblTmpDepositAmt = 0

                Select Case intCurrencyVal
                    Case 1
                        udtNoteInserted.RM1DenominationCnt = intCountPcs
                    Case 5
                        udtNoteInserted.RM5DenominationCnt = intCountPcs
                    Case 10
                        udtNoteInserted.RM10DenominationCnt = intCountPcs
                    Case 20
                        udtNoteInserted.RM20DenominationCnt = intCountPcs
                    Case 50
                        udtNoteInserted.RM50DenominationCnt = intCountPcs
                    Case 100
                        udtNoteInserted.RM100DenominationCnt = intCountPcs
                End Select

                intCountPcs = 0
                intCurrencyVal = 0
            Next

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.DepositItemFull
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = strDeviceDataValue
            End With

            AppLogInfo("MDS Cash -CashInsertEscrowedValidatedItemFull Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertEscrowedValidatedItemFull:" & ex.Message)
        End Try
    End Sub

    Public Sub CashRejectComplete()
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

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            TotalAmount = 0.0

            TotalCheque = 0

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.TransComplete
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cash -CashRejectComplete Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashRejectComplete:" & ex.Message)
        End Try
    End Sub

    Public Sub CashInsertComplete(ByVal NoteList As clsMDSModuleControl.TrxNoteList)
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Dim dblTmpDepositAmt As Double = 0.0
        Dim strCurrencyType As String = ""

        Try
            strProTxnStatus = CurrentState.TransComplete
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim
            TotalAmount = 0.0

            udtNoteList = New clsMDSModuleControl.TrxNoteList
            udtNoteList = NoteList

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.TransComplete
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED

                For i = 0 To udtNoteList.NoteInfo.Length - 1
                    If udtNoteList.NoteInfo(i).bGoodNote Then
                        intCountPcs = udtNoteList.NoteInfo(i).NoteCount
                        intCurrencyVal = udtNoteList.NoteInfo(i).NoteValue
                        strCurrencyType = udtNoteList.NoteInfo(i).NoteCurrency
                        dblTmpDepositAmt = intCurrencyVal * intCountPcs
                        'strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & Chr(28) & intCountPcs.ToString & Chr(29)
                        strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & "," & intCountPcs.ToString & "," & strCurrencyType.Trim & "|"
                        TotalAmount = TotalAmount + dblTmpDepositAmt
                    End If
                Next

                .mDeviceDataValue = strDeviceDataValue
            End With

            AppLogInfo("MDS Cash -CashInsertComplete Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertComplete:" & ex.Message)
        End Try
    End Sub

    Public Sub AllBoxCount(ByVal NoteList As clsMDSModuleControl.CashBoxCount, ByVal ChqCount As clsMDSModuleControl.ChqBoxCount)
        Dim strGeniDeviceTrace As String = ""
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = ""
        Dim dblTmpDepositAmt As Double = 0.0

        Dim strCurrencyType As String = ""
        Dim strLogicalBox As String = ""

        Try
            strProTxnStatus = CurrentState.Supervisor
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim
            TotalAmount = 0.0

            udtCashBoxNoteList = New clsMDSModuleControl.CashBoxCount
            udtCashBoxNoteList = NoteList

            udtChqBoxList = New clsMDSModuleControl.ChqBoxCount
            udtChqBoxList = ChqCount

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Supervisor
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED

                For i = 0 To udtCashBoxNoteList.ListOfBox.Length - 1
                    If Not IsNothing(udtCashBoxNoteList.ListOfBox(i).NoteInfo) Then
                        For x = 0 To udtCashBoxNoteList.ListOfBox(i).NoteInfo.Length - 1
                            If udtCashBoxNoteList.ListOfBox(i).NoteInfo(x).bGoodNote = True Then
                                intCurrencyVal = udtCashBoxNoteList.ListOfBox(i).NoteInfo(x).NoteValue
                                intCountPcs = udtCashBoxNoteList.ListOfBox(i).NoteInfo(x).NoteCount
                                strCurrencyType = udtCashBoxNoteList.ListOfBox(i).NoteInfo(x).NoteCurrency
                                strLogicalBox = udtCashBoxNoteList.ListOfBox(i).LogicalNumber.ToString

                                dblTmpDepositAmt = intCurrencyVal * intCountPcs
                                'strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & Chr(28) & intCountPcs.ToString & Chr(29)
                                strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & "," & intCountPcs.ToString & "," & strCurrencyType & "," & strLogicalBox & "|"
                                TotalAmount = TotalAmount + dblTmpDepositAmt
                            End If
                        Next
                    End If
                Next

                .mDeviceDataValue = strDeviceDataValue '& "|" & udtChqBoxList.intChqCount
            End With

            AppLogInfo("MDS Cash -AllBoxCount Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & " - Data Value:" & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-AllBoxCount:" & ex.Message)
        End Try
    End Sub

    Public Sub CashInsertProcessing()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.ProcessingCash
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.ProcessingCash
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("CashInsertProcessing Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertProcessing:" & ex.Message)
        End Try
    End Sub

    Public Sub CashInsertPutItemInFeeder()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.OpenFeeder
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.OpenFeeder
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cash -CashInsertPutItemInFeeder Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertPutItemInFeeder:" & ex.Message)
        End Try
    End Sub

    Public Sub CashInsertEscrowed()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.Escrowed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Escrowed
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cash -CashInsertEscrowed Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertEscrowed:" & ex.Message)
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

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Close
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_ERROR
                .mDeviceDataValue = 0
            End With

            AppLogInfo("MDS Cash -ErrorHWD Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-ErrorHWD:" & ex.Message)
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

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Close
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = 0
            End With

            AppLogInfo("MDS Cash -InitializeHWDOK Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-InitializeHWDOK:" & ex.Message)
        End Try
    End Sub

    Public Sub CashInsertRejected()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.RejectCash
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.RejectCash
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cash -CashInsertRejected Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertRejected:" & ex.Message)
        End Try
    End Sub

    Public Sub CashInsertTimeout()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.Close
            strProErrSeverity = 1
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Close
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_TIMEOUT
                .mDeviceDataValue = Nothing
            End With

            AppLogInfo("MDS Cash -CashInsertTimeout Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceTimeout(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertTimeout:" & ex.Message)
        End Try
    End Sub

    Public Sub CashInsertReposition()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.RejectCash
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.RepositionDoc
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cash -CashInsertReposition Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashInsertReposition:" & ex.Message)
        End Try
    End Sub

    Public Sub CashCleanFeeder()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.RejectCash
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.CleanFeederDoc
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS Cash -CashCleanFeeder Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-CashCleanFeeder:" & ex.Message)
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

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DVST_ERRIN_DEVSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = Nothing
            End With

            AppLogInfo("MDS Cash -DiagDeviceStatusError Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-DiagDeviceStatusError:" & ex.Message)
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

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DVST_ERRIN_DEVSTATE
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = Nothing
            End With

            AppLogInfo("MDS Cash -DiagDeviceStatusOK Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)

        Catch ex As Exception
            AppLogErr("Error in CashDeposit-DiagDeviceStatusOK:" & ex.Message)
        End Try
    End Sub



#End Region

#Region "Timer Operations"

    Private Sub MDSTImerControl_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles MDSStartupTimerControl.Elapsed
        MDSStartupTimerControl.Stop()
        If objMDSControl.MDSControlStatus <> clsMDSModuleControl.ControlMDSStatusOptions.OK Then
            If intCheckCount < 3 Then
                intCheckCount = intCheckCount + 1
                MDSStartupTimerControl.Start()
            Else
                AppLogErr("StartupWatcher - CashDeposit: MDSControl NotOK: " & objMDSControl.StatusText)
                ErrorHWD()
            End If
        Else
            AppLogInfo("StartupWatcher End")
        End If
    End Sub

    'Private Sub MDSReturnTimerControl_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles MDSReturnTimerControl.Elapsed
    '    MDSReturnTimerControl.Stop()
    '    If LastMDSInteract = LastMDSActivity.TakeReturnedItem Then
    '        If (intCurrentState = CurrentState.RefundCash) Or (intCurrentState = CurrentState.OpenFeeder) Then
    '            AppLogInfo("Event Received - CashDeposit  : User Timeout Sent")
    '            CashInsertTimeout()
    '        End If
    '    End If
    'End Sub


#End Region


#Region "MDS Control"


    Private Sub objMDSControl_MDSBoxCashCount(ByVal NoteList As MDSModuleControl.clsMDSModuleControl.CashBoxCount, ByVal ChqCount As MDSModuleControl.clsMDSModuleControl.ChqBoxCount, ByVal intDenoCount As UShort) Handles objMDSControl.MDSBoxCashCount
        Try
            AppLogInfo("MDS Cash -Event Received : MDSBoxCashCount")
            AllBoxCount(NoteList, ChqCount)
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSBoxCashCount: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSCashTimeout() Handles objMDSControl.MDSCashTimeout
        Try
            AppLogInfo("MDS Cash -Event Received : MDSCashTimeout")
            CashInsertTimeout()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSCashTimeout: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSCashTransactionCompletedEvt(ByVal Notelist As MDSModuleControl.clsMDSModuleControl.TrxNoteList, ByVal intCashCount As UShort) Handles objMDSControl.MDSCashTransactionCompletedEvt
        Try

            AppLogInfo("MDS Cash -Event Received : Trans Complete")
            LastMDSInteract = LastMDSActivity.CompletedTransaction

            'MsgBox("MDSCashTransactionCompletedEvt state:" & intCurrentState)

            If intCurrentState = CurrentState.RefundCash Then
                CashRejectComplete()
            ElseIf intCurrentState = CurrentState.FinishDeposit Then
                CashInsertComplete(Notelist)
            End If

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSCashTransactionCompletedEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSCashTransactionFailedEvt(ByVal Notelist As MDSModuleControl.clsMDSModuleControl.TrxNoteList, ByVal intCashCount As UShort) Handles objMDSControl.MDSCashTransactionFailedEvt
        Try
            AppLogInfo("MDS Cash -Event Received : Trans Failed")
            LastMDSInteract = LastMDSActivity.TransactionCompleteFail

            'Hardware Error Detected
            ErrorHWD()

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSCashTransactionFailedEvt: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSCleanFeederEvt() Handles objMDSControl.MDSCashCleanFeederEvt
        Try
            AppLogInfo("MDS Cash -Event Received : Clean Feeder")
            LastMDSInteract = LastMDSActivity.cleanFeeder
            CashCleanFeeder()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSCleanFeederEvt: " & ex.Message)
        End Try
    End Sub

    'Private Sub objMDSControl_MDSCompleteDepositQuestionEvt() Handles objMDSControl.MDSCashCompleteDepositQuestionEvt
    '    Try
    '        AppLogInfo("MDS Cash -Event Received : Complete Deposit")
    '        If intCurrentState = CurrentState.Close Then
    '            If objMDSControl.UserReactNO() Then
    '                AppLogInfo("Refund Success")
    '            Else
    '                AppLogInfo("Refund Failed")
    '            End If
    '        ElseIf intCurrentState = CurrentState.RefundCash Then
    '            If objMDSControl.UserReactNO() Then
    '                AppLogInfo("Refund Success")

    '                'Clean Inserted amount and denomination
    '                TotalAmount = 0
    '                udtNoteInserted = New NoteInserted
    '            Else
    '                AppLogInfo("Refund Failed")
    '            End If
    '        ElseIf intCurrentState = CurrentState.FinishDeposit Then
    '            If objMDSControl.UserReactYES Then
    '                AppLogInfo("Deposit Success")
    '            Else
    '                AppLogInfo("Deposit Failed")
    '            End If
    '        End If

    '        LastMDSInteract = LastMDSActivity.CompleteDeposit

    '    Catch ex As Exception
    '        AppLogErr("Error in MDSCompleteDepositQuestionEvt: " & ex.Message)
    '    End Try
    'End Sub

    'Private Sub objMDSControl_MDSControlErrorReceived(ByVal strExMsg As String) Handles objMDSControl.MDSControlErrorReceived
    '    AppLogInfo("CashDepositHWD - Error received : " & strExMsg.Trim)
    'End Sub

    Private Sub objMDSControl_MDSInsertItemsEvt() Handles objMDSControl.MDSCashInsertItemsEvt
        Try
            AppLogInfo("MDS Cash -Event Received : Insert Item")
            LastMDSInteract = LastMDSActivity.insertItem
            CashInsertPutItemInFeeder()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSInsertItemsEvt: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSNotifyCounterfeitEvt() Handles objMDSControl.MDSCashNotifyCounterfeitEvt
        Try
            AppLogInfo("MDS Cash -Event Received : Notifiy Counterfit")
            LastMDSInteract = LastMDSActivity.NotifyCounterfit
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSNotifyCounterfeitEvt: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSProcessingEvt() Handles objMDSControl.MDSCashProcessingEvt
        Try
            AppLogInfo("MDS Cash -Event Received : Processing")
            LastMDSInteract = LastMDSActivity.Processing
            CashInsertProcessing()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSProcessingEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSRepositionDocumentsEvt() Handles objMDSControl.MDSCashRepositionDocumentsEvt
        Try
            AppLogInfo("MDS Cash -Event Received : Reposition")
            LastMDSInteract = LastMDSActivity.RepositionDocuments
            CashInsertReposition()
        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSRepositionDocumentsEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSStatusUpdateEvt(ByVal MDSCurrentState As MDSModuleControl.clsMDSModuleControl.ControlMDSCurrentRunningState, ByVal MDSStatus As MDSModuleControl.clsMDSModuleControl.ControlMDSStatusOptions, ByVal strStatusText As String) Handles objMDSControl.MDSCashStatusUpdateEvt
        Try


            AppLogInfo("MDS Cash -Status Update:" & MDSCurrentState & "," & MDSStatus & "," & strStatusText)

            'New-Control
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

            '        ErrorHWD()

            '        'intCheckCount = intCheckCount + 1
            '        'AppLogErr("MDSControl NotOK: " & objMDSControl.StatusText)
            '        'If intCheckCount > 2 Then
            '        '    ErrorHWD()
            '        'ElseIf strStatusText.Contains(MDS_ERROR_STATE) Or strStatusText.Contains(MDS_ERRORSTATE_SAFEDOOR) Then
            '        '    ErrorHWD()
            '        'End If
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

    Private Sub objMDSControl_MDSTakeReturnedItemsEvt() Handles objMDSControl.MDSCashTakeReturnedItemsEvt
        Try
            AppLogInfo("MDS Cash -Event Received : Take Return Item")
            LastMDSInteract = LastMDSActivity.TakeReturnedItem
            CashInsertRejected()

            'If intCurrentState = CurrentState.RefundCash Or intCurrentState = CurrentState.OpenFeeder Then
            '    MDSReturnTimerControl = New Timers.Timer
            '    MDSReturnTimerControl.Interval = (objMDSControl.CleanReturnFeederTimeout * 1000) - 1000
            '    MDSReturnTimerControl.Start()
            'End If

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSTakeReturnedItemsEvt: " & ex.Message)

        End Try
    End Sub

    Private Sub objMDSControl_MDSWantToInsertMoreCashQuestionEvt(ByVal Notelist As MDSModuleControl.clsMDSModuleControl.TrxNoteList, ByVal intCashCount As UShort) Handles objMDSControl.MDSWantToInsertMoreCashQuestionEvt
        Try


            AppLogInfo("MDS Cash -Event Received : Insert More")
            'CashInsertEscrowedValidated(Notelist)

            If intCashCount <> 0 Then
                CashInsertEscrowedValidated(Notelist)

            ElseIf bolCancelTrans = True Then
                objMDSControl.UserReactNO()
                CashRejectComplete()
                bolCancelTrans = False

            Else
                objMDSControl.UserReactYES()
                CashInsertPutItemInFeeder()
            End If

            LastMDSInteract = LastMDSActivity.WantToInsertMore

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSTakeReturnedItemsEvt: " & ex.Message)
        End Try

    End Sub


#End Region


    Private Sub objMDSControl_MDSCashCompleteDepositQuestionEvt(ByVal Notelist As MDSModuleControl.clsMDSModuleControl.TrxNoteList, ByVal intCashCount As UShort) Handles objMDSControl.MDSCashCompleteDepositQuestionEvt
        Try

            AppLogInfo("MDS Cash -Event Received : Complete Deposit Question. CurrState=" & intCurrentState & "-" & CompleteQuestionStatusDesc(intCurrentState))

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
                    TotalAmount = 0
                    udtNoteInserted = New NoteInserted

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
                If intCashCount <> 0 Then
                    CashInsertEscrowedValidatedItemFull(Notelist)
                Else
                    objMDSControl.UserReactYES()
                    CashInsertPutItemInFeeder()
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



#Region "New MDS Control"


    ReadOnly Property MDSReplyStatus() As String
        Get
            Return objMDSControl.MDSReplyStatusHWD
        End Get
    End Property

    'MDS Status Reason
    ReadOnly Property MDSReplyStatusReason() As String
        Get
            Return objMDSControl.MDSReplyStatusReasonHWD
        End Get
    End Property

    'MDS Status Details
    ReadOnly Property MDSReplyStatusDetails As String
        Get
            Return objMDSControl.MDSReplyStatusDetailsHWD
        End Get
    End Property


    ReadOnly Property MDSUserStatus() As String
        Get
            Return objMDSControl.MDSReplyUserStatusHWD
        End Get
    End Property



#Region "Ping Failed Control"

    ReadOnly Property MDSPingFailed() As Boolean
        Get
            Return blnMDSPingFailed
        End Get
    End Property


#End Region

#End Region




#Region "NotesInPhysicalBoxStatus"

    ReadOnly Property MDSNoteInAllBoxCount As String
        Get
            Return objMDSControl.MDSNoteInAllBoxCountHWD
        End Get
    End Property


#End Region

#Region "Retract Note"

    ReadOnly Property MDSRetractBoxNoteCount As String
        Get
            Return objMDSControl.MDSRetractBoxNoteCountHWD
        End Get
    End Property


#End Region

#Region "Counterfeit Note"

    ReadOnly Property MDSConterfeitBoxNoteCount As String
        Get
            Return objMDSControl.MDSConterfeitBoxNoteCountHWD
        End Get
    End Property


#End Region





#Region "MDS Function"


#Region "New Function"


    Public Function GetNoteInBox(ByVal strLogicalBox As String) As String
        Dim strReply As String = ""
        Try
            strReply = objMDSControl.HWDGetNoteInBox(strLogicalBox)
            Return strReply
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function MDSReset() As Boolean
        Dim blnReply As Boolean = False
        Try
            blnReply = objMDSControl.MDSReInitialise
            Return blnReply
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MDSClose() As Boolean
        Dim blnReply As Boolean = False
        Try
            blnReply = objMDSControl.MDSClose
            Return blnReply
        Catch ex As Exception
             Return False
        End Try
    End Function

#End Region


#End Region


#Region "MDS Mode - CASH"


    Public Function SetMDSTOCashMode() As Boolean
        Try
            objMDSControl.SetMDSINCashMode()
            AppLogInfo("MDS-SetMDSTOCashMode")
            Return True
        Catch ex As Exception
            AppLogErr("Error in SetMDSTOCashMode:" & ex.Message)
            Return False
        End Try
    End Function



#End Region

#Region "MDS Mode - CASH"

    Public Function SetMDSTOChequeMode() As Boolean
        Try
            objMDSControl.SetMDSINChequeMode()
            AppLogInfo("MDS SetMDSTOChequeMode")
            Return True
        Catch ex As Exception
            AppLogErr("Error in SetMDSTOChequeMode:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "MDS Status IN Details"

    'Public Structure MDSCIMStatusDetail
    '    Dim CIMDeviceStatus As UInteger
    '    Dim SafeDoorStatus As UInteger
    '    Dim AcceptorStatus As UInteger
    '    Dim IntermediateStackerStatus As UInteger
    '    Dim StackerItemsStatus As UInteger
    '    Dim BanknoteReaderStatus As UInteger
    '    'Dim dwGuidLights() As UInteger
    '    Dim InputShutterStatus As UInteger
    '    Dim InputPositionStatus As UInteger
    '    Dim InputTransportStatus As UInteger
    '    Dim InputTransportFillingStatus As UInteger
    '    Dim OutputShutterStatus As UInteger
    '    Dim OutputPositionStatus As UInteger
    '    Dim OutputTransportStatus As UInteger
    '    Dim OutputTransportFillingStatus As UInteger

    '    'Dim dwGuidLights As List(Of RotoKiosk.Constants.GuidanceLight)
    '    'Dim LogicalBoxStatus As List(Of RotoKiosk.Constants.BoxStatus)
    '    'Dim PyhsicalBoxStatus As List(Of RotoKiosk.Constants.BoxStatus)


    '    Dim NotesInPhysicalBox As List(Of UInteger)

    '    Dim SuiPortStatus As UInteger

    'End Structure

    'Public Structure MDSIPMStatusDetail
    '    Dim IPMDeviceStatus As UInteger
    '    Dim RefusedShutterStatus As UInteger
    '    Dim RefusedPositionStatus As UInteger
    '    Dim RefusedTransportStatus As UInteger
    '    Dim RefusedTransportFillingStatus As UInteger
    '    Dim MediaStatus As UInteger
    '    Dim TonerStatus As UInteger
    '    Dim InkStatus As UInteger
    '    Dim FrontScannerStatus As UInteger
    '    Dim BackScannerStatus As UInteger
    '    Dim MICRStatus As UInteger
    '    Dim MediaFeederStatus As UInteger
    '    Dim StackerEnabled As Boolean
    '    Dim CMC7Mapping As String
    '    Dim E13BMapping As String
    '    Dim ChequeBoxCount As UInteger
    '    Dim IPMRetractOperationsCount As UInteger
    '    Dim ChequeBoxStatus As UInteger
    '    Dim IPRetractBoxStatus As UInteger
    'End Structure

    'Public Structure MDSStatusDetailInfo
    '    Dim DeviceStatus As UInteger
    '    Dim CIMStatusDetail As MDSCIMStatusDetail
    '    Dim IPMStatusDetail As MDSIPMStatusDetail
    'End Structure


#End Region


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

#Region "Event EvtDeviceDataReady - MIX Mode"


    Private Sub objMDSControl_MDSCashNChqCompleteDepositQuestionEvt(ByVal Notelist As MDSModuleControl.clsMDSModuleControl.TrxNoteList, ByVal intCashCount As UShort, ByVal ChqList As MDSModuleControl.clsMDSModuleControl.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSCashNChqCompleteDepositQuestionEvt

        Try

            AppLogInfo("MDS Cash N Chq -Event Received : Complete Deposit Question. CurrState=" & intCurrentState & "-" & CompleteQuestionStatusDesc(intCurrentState))

            If intCurrentState = CurrentState.Close Then
                If objMDSControl.UserReactNO() Then
                    AppLogInfo("Refund CashChq Success")
                Else
                    AppLogInfo("Refund CashChq Failed")
                End If
                LastMDSInteract = LastMDSActivity.CompleteDeposit
            ElseIf intCurrentState = CurrentState.RefundCash Then
                If objMDSControl.UserReactNO() Then
                    AppLogInfo("Refund CashChq Success")

                    'Clean Inserted amount and denomination
                    TotalAmount = 0
                    udtNoteInserted = New NoteInserted

                    'Clean Inserted amount and denomination
                    TotalCheque = 0
                    udtChequeInserted = New ChequeDetailsDeposited

                Else
                    AppLogInfo("Refund CashChq Failed")
                End If
                LastMDSInteract = LastMDSActivity.CompleteDeposit

            ElseIf intCurrentState = CurrentState.FinishDeposit Then
                If objMDSControl.UserReactYES Then
                    AppLogInfo("Deposit CashChq Success")
                Else
                    AppLogInfo("Deposit CashChq Failed")
                End If
                LastMDSInteract = LastMDSActivity.CompleteDeposit
            Else
                If intCashCount <> 0 Or intChqCount <> 0 Then

                    'CashInsertEscrowedValidatedItemFull(Notelist)
                    'ChqInsertedEscrowedValidatedItemFULL(ChequeList)

                    CashChqInsertEscrowedValidatedItemFull(Notelist, ChqList)

                Else
                    objMDSControl.UserReactYES()

                    'CashInsertPutItemInFeeder()
                    'ChequeInsertPutItemInFeeder()

                    CashChqInsertPutItemInFeeder()

                End If

                'Reject - Hit the maximum 
                'If intChqCount <> 0 Then
                '    ChqInsertedEscrowedValidatedItemFULL(ChequeList)
                'Else
                '    objMDSControl.UserReactYES()
                '    ChequeInsertPutItemInFeeder()
                'End If


                LastMDSInteract = LastMDSActivity.DepositItemFull
            End If

        Catch ex As Exception
            AppLogErr("Error in MDSCashNChqCompleteDepositQuestionEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSCashNChqTransactionCompletedEvt(ByVal Notelist As MDSModuleControl.clsMDSModuleControl.TrxNoteList, ByVal intCashCount As UShort, ByVal ChqList As MDSModuleControl.clsMDSModuleControl.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSCashNChqTransactionCompletedEvt
        Try

            AppLogInfo("MDS Cash N Chq -Event Received : Trans Complete")
            LastMDSInteract = LastMDSActivity.CompletedTransaction

            'MsgBox("MDSCashTransactionCompletedEvt state:" & intCurrentState)

            If intCurrentState = CurrentState.RefundCash Then
                'CashRejectComplete()
                'ChequeRejectComplete()
                CashChqRejectComplete()
            ElseIf intCurrentState = CurrentState.FinishDeposit Then
                'CashInsertComplete(Notelist)
                'ChequeInsertComplete(ChqList)
                CashChqInsertComplete(Notelist, ChqList)
            End If

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSCashNChqTransactionCompletedEvt: " & ex.Message)
        End Try

    End Sub

    Private Sub objMDSControl_MDSCashNChqTransactionFailedEvt(ByVal Notelist As MDSModuleControl.clsMDSModuleControl.TrxNoteList, ByVal intCashCount As UShort, ByVal ChqList As MDSModuleControl.clsMDSModuleControl.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSCashNChqTransactionFailedEvt
        Try
            AppLogInfo("MDS Cash N Chq -Event Received : Trans Failed")
            LastMDSInteract = LastMDSActivity.TransactionCompleteFail

            'Hardware Error Detected
            ErrorHWD()

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSCashNChqTransactionFailedEvt: " & ex.Message)
        End Try
    End Sub

    Private Sub objMDSControl_MDSWantToInsertMoreCashNChqQuestionEvt(ByVal Notelist As MDSModuleControl.clsMDSModuleControl.TrxNoteList, ByVal intCashCount As UShort, ByVal ChequeList As MDSModuleControl.clsMDSModuleControl.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSWantToInsertMoreCashNChqQuestionEvt
        Try


            AppLogInfo("MDS Cash N Chq -Event Received : Insert More")

            If intCashCount <> 0 Or intChqCount <> 0 Then
                'CashInsertEscrowedValidated(Notelist)
                'ChqInsertedEscrowedValidated(ChequeList)
                CashChqInsertEscrowedValidated(Notelist, ChequeList)

            ElseIf bolCancelTrans = True Then
                objMDSControl.UserReactNO()
                'CashRejectComplete()
                'ChequeRejectComplete()

                CashChqRejectComplete()

                bolCancelTrans = False


            Else
                objMDSControl.UserReactYES()
                'CashInsertPutItemInFeeder()
                'ChequeInsertPutItemInFeeder()

                CashChqInsertPutItemInFeeder()

            End If

            LastMDSInteract = LastMDSActivity.WantToInsertMore

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSWantToInsertMoreCashNChqQuestionEvt: " & ex.Message)
        End Try
    End Sub

#Region "Sub Mix Support Func"

    Public Sub CashChqInsertEscrowedValidatedItemFull(ByVal NoteList As clsMDSModuleControl.TrxNoteList, ByVal ChqList As clsMDSModuleControl.TrxChqList)
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Dim dblTmpDepositAmt As Double
        Dim strCurrencyType As String = ""


        Dim strDeviceDataValueCash As String = String.Empty
        Dim strDeviceDataValueChq As String = String.Empty

        Try
            strProTxnStatus = CurrentState.Escrowed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            udtNoteList = New clsMDSModuleControl.TrxNoteList
            udtNoteList = NoteList

            TotalAmount = 0.0

            For i = 0 To udtNoteList.NoteInfo.Length - 1

                If udtNoteList.NoteInfo(i).bGoodNote Then
                    intCountPcs = udtNoteList.NoteInfo(i).NoteCount
                    intCurrencyVal = udtNoteList.NoteInfo(i).NoteValue
                    strCurrencyType = udtNoteList.NoteInfo(i).NoteCurrency
                    dblTmpDepositAmt = intCountPcs * intCurrencyVal
                    'strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & Chr(28) & intCountPcs.ToString & Chr(29)
                    strDeviceDataValueCash = strDeviceDataValueCash & intCurrencyVal.ToString & "," & intCountPcs.ToString & "," & strCurrencyType & "|"

                    TotalAmount = TotalAmount + dblTmpDepositAmt
                End If

                dblTmpDepositAmt = 0

                Select Case intCurrencyVal
                    Case 1
                        udtNoteInserted.RM1DenominationCnt = intCountPcs
                    Case 5
                        udtNoteInserted.RM5DenominationCnt = intCountPcs
                    Case 10
                        udtNoteInserted.RM10DenominationCnt = intCountPcs
                    Case 20
                        udtNoteInserted.RM20DenominationCnt = intCountPcs
                    Case 50
                        udtNoteInserted.RM50DenominationCnt = intCountPcs
                    Case 100
                        udtNoteInserted.RM100DenominationCnt = intCountPcs
                End Select

                intCountPcs = 0
                intCurrencyVal = 0
            Next


            'Chq
            udtChequeList = New clsMDSModuleControl.TrxChqList
            udtChequeList = ChqList

            For i = 0 To udtChequeList.ChequeList.Length - 1
                If udtChequeList.ChequeList(i).ChequeCodeline <> Nothing Then
                    strDeviceDataValueChq = strDeviceDataValueChq & udtChequeList.ChequeList(i).ChequeCodeline & "|"
                End If
            Next

            'MIX Mode
            strDeviceDataValue = strDeviceDataValueCash & ";" & strDeviceDataValueChq

            'Final

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.DepositItemFull
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = strDeviceDataValue
            End With

            AppLogInfo("MDS CashChq -CashChqInsertEscrowedValidatedItemFull Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashChqDeposit-CashChqInsertEscrowedValidatedItemFull:" & ex.Message)
        End Try
    End Sub

    Public Sub CashChqInsertEscrowedValidated(ByVal NoteList As clsMDSModuleControl.TrxNoteList, ByVal ChqList As clsMDSModuleControl.TrxChqList)
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty

        Dim strDeviceDataValueCash As String = String.Empty
        Dim strDeviceDataValueChq As String = String.Empty


        Dim dblTmpDepositAmt As Double
        Dim strCurrencyType As String = ""

        Try
            strProTxnStatus = CurrentState.Escrowed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            'CASH
            udtNoteList = New clsMDSModuleControl.TrxNoteList
            udtNoteList = NoteList

            TotalAmount = 0.0

            For i = 0 To udtNoteList.NoteInfo.Length - 1

                If udtNoteList.NoteInfo(i).bGoodNote Then
                    intCountPcs = udtNoteList.NoteInfo(i).NoteCount
                    intCurrencyVal = udtNoteList.NoteInfo(i).NoteValue
                    strCurrencyType = udtNoteList.NoteInfo(i).NoteCurrency
                    dblTmpDepositAmt = intCountPcs * intCurrencyVal

                    'strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & Chr(28) & intCountPcs.ToString & Chr(29)
                    strDeviceDataValueCash = strDeviceDataValueCash & intCurrencyVal.ToString & "," & intCountPcs.ToString & "," & strCurrencyType.Trim & "|"

                    TotalAmount = TotalAmount + dblTmpDepositAmt
                End If

                dblTmpDepositAmt = 0

                Select Case intCurrencyVal
                    Case 1
                        udtNoteInserted.RM1DenominationCnt = intCountPcs
                    Case 5
                        udtNoteInserted.RM5DenominationCnt = intCountPcs
                    Case 10
                        udtNoteInserted.RM10DenominationCnt = intCountPcs
                    Case 20
                        udtNoteInserted.RM20DenominationCnt = intCountPcs
                    Case 50
                        udtNoteInserted.RM50DenominationCnt = intCountPcs
                    Case 100
                        udtNoteInserted.RM100DenominationCnt = intCountPcs
                End Select

                intCountPcs = 0
                intCurrencyVal = 0
            Next


            'CHQ
            udtChequeList = New clsMDSModuleControl.TrxChqList
            udtChequeList = ChqList

            For i = 0 To udtChequeList.ChequeList.Length - 1
                If udtChequeList.ChequeList(i).ChequeCodeline <> Nothing Then
                    strDeviceDataValueChq = strDeviceDataValueChq & udtChequeList.ChequeList(i).ChequeCodeline & "|"
                End If
            Next


            'MIX Mode
            strDeviceDataValue = strDeviceDataValueCash & ";" & strDeviceDataValueChq

            'Final
            With udtEventDeviceArgs
                .iDeviceState = CurrentState.Escrowed
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = strDeviceDataValue
            End With

            AppLogInfo("MDS CashChq -CashChqInsertEscrowedValidated Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashChqDeposit-CashChqInsertEscrowedValidated:" & ex.Message)
        End Try
    End Sub

    Public Sub CashChqInsertPutItemInFeeder()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Try
            strProTxnStatus = CurrentState.OpenFeeder
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.OpenFeeder
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS CashChq -CashChqInsertPutItemInFeeder Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashChqDeposit-CashChqInsertPutItemInFeeder:" & ex.Message)
        End Try
    End Sub

    Public Sub CashChqRejectComplete()
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

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            TotalAmount = 0.0
            TotalCheque = 0

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.TransComplete
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
            End With

            AppLogInfo("MDS CashChq -CashChqRejectComplete Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashChqDeposit-CashChqRejectComplete:" & ex.Message)
        End Try
    End Sub

    Public Sub CashChqInsertComplete(ByVal NoteList As clsMDSModuleControl.TrxNoteList, ByVal ChqList As clsMDSModuleControl.TrxChqList)
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Dim dblTmpDepositAmt As Double = 0.0
        Dim strCurrencyType As String = ""

        Dim strImgPath As String = ""
        Dim strChqStatus As String = ""

        Dim strDeviceDataValueCash As String = String.Empty
        Dim strDeviceDataValueChq As String = String.Empty

        Try
            strProTxnStatus = CurrentState.TransComplete
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtCashDepositorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = CashDepositorDeviceGraphicId
                .strDeviceName = CashDepositorDeviceName
            End With

            strGeniDeviceTrace = CashDepositorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim
            TotalAmount = 0.0

            udtNoteList = New clsMDSModuleControl.TrxNoteList
            udtNoteList = NoteList

            With udtEventDeviceArgs
                .iDeviceState = CurrentState.TransComplete
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED

                For i = 0 To udtNoteList.NoteInfo.Length - 1
                    If udtNoteList.NoteInfo(i).bGoodNote Then
                        intCountPcs = udtNoteList.NoteInfo(i).NoteCount
                        intCurrencyVal = udtNoteList.NoteInfo(i).NoteValue
                        strCurrencyType = udtNoteList.NoteInfo(i).NoteCurrency
                        dblTmpDepositAmt = intCurrencyVal * intCountPcs
                        'strDeviceDataValue = strDeviceDataValue & intCurrencyVal.ToString & Chr(28) & intCountPcs.ToString & Chr(29)
                        strDeviceDataValueCash = strDeviceDataValueCash & intCurrencyVal.ToString & "," & intCountPcs.ToString & "," & strCurrencyType.Trim & "|"
                        TotalAmount = TotalAmount + dblTmpDepositAmt
                    End If
                Next



            End With


            'Chq
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
                        strDeviceDataValueChq = strDeviceDataValueChq & udtChequeList.ChequeList(i).ChequeCodeline & "," & udtChequeList.ChequeList(i).ChequeImagePath & "|"
                    End If

                End If

            Next


            'MIX Mode
            strDeviceDataValue = strDeviceDataValueCash & ";" & strDeviceDataValueChq


            'Final

            udtEventDeviceArgs.mDeviceDataValue = strDeviceDataValue


            AppLogInfo("MDS CashChq -CashChqInsertComplete Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in CashChqDeposit-CashChqInsertComplete:" & ex.Message)
        End Try
    End Sub

#End Region

#End Region


End Class
