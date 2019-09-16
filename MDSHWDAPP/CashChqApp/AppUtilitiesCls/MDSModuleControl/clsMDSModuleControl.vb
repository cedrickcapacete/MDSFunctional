Imports MDS_Wrapper_Control
Imports MDSXFSWrapper

Public Class clsMDSModuleControl

#Region "Cls Object"

    Public Shared WithEvents objMDSControl As New clsMDSXFS

#End Region

#Region "Variables"

    'Private Shared WithEvents tmrCheckDoor As Timers.Timer
    Private Shared CurrentTransactionType As TransactionType
    Public Shared blnEngineStarted As Boolean = False

#End Region


#Region "Device Code - Structure"

    Public Shared mdsSettingParam As DEPOSITORCTR
    Public Shared cashDevCode As CASHDEVICECODE
    Public Shared chequeDevCode As CHEQUEDEVICECODE
    Public Shared doorDevCode As DOORSENSORDEVICECODE

    Structure DEPOSITORCTR
        Dim blnCASHDEPOSITORENABLE As Boolean
        Dim CASHDEPOSITORPortNum As Integer
        Dim CASHDEPOSITORTraceFilePath As String
        Dim CASHDEPOSITORChequeImagePath As String
        Dim CASHDEPOSITORTemplateFilePath As String
        Dim CASHDEPOSITOROperationMode As String
        Dim CASHDEPOSITORInsertItemTimeout As Integer
        Dim CASHDEPOSITORCleanFeederTimeout As Integer
        Dim CASHDEPOSITORRepositonDocTimeout As Integer
        Dim CASHDEPOSITORTakeReturnTimeout As Integer
        Dim CASHDEPOSITORMachineStartupTimeout As Integer
        Dim blnCASHDEPOSITORForceAcceptChq As Boolean
        Dim DOORCheckInterval As Integer
        Dim blnIMGConversion As Boolean
        Dim strMDSLightMode As String
        Dim strMDSCardReaderLightMode As String
    End Structure


    Structure CASHDEVICECODE
        Dim CASHDEVDeviceID As String
        Dim CASHDEVDeviceName As String
        Dim CASHDEVEventWrap As String
        Dim CASHDEVEventTimeout As String
        Dim CASHDEVEventDataArrive As String
        Dim CASHDEVEventError As String
        Dim CASHDEVErrDevState As String
        Dim CASHDEVErrorStateNoError As String
        Dim CASHDEVErrorStateError As String
        Dim CASHDEVErrorStateFatal As String
        Dim CASHDEVErrorStateWarning As String
        Dim CASHDEVErrorDGSTStatus As String
        Dim CASHDEVErrorSysStatNoState As String
    End Structure

    Structure CHEQUEDEVICECODE
        Dim CHQDEVDeviceID As String
        Dim CHQDEVDeviceName As String
        Dim CHQDEVEventWrap As String
        Dim CHQDEVEventTimeout As String
        Dim CHQDEVEventDataArrive As String
        Dim CHQDEVEventError As String
        Dim CHQDEVErrDevState As String
        Dim CHQDEVErrorStateNoError As String
        Dim CHQDEVErrorStateError As String
        Dim CHQDEVErrorStateFatal As String
        Dim CHQDEVErrorStateWarning As String
        Dim CHQDEVErrorDGSTStatus As String
        Dim CHQDEVErrorSysStatNoState As String
    End Structure

    Structure DOORSENSORDEVICECODE
        Dim DOORDEVDeviceID As String
        Dim DOORDEVDeviceName As String
        Dim DOORDEVEventWrap As String
        Dim DOORDEVEventTimeout As String
        Dim DOORDEVEventDataArrive As String
        Dim DOORDEVEventError As String
    End Structure

#End Region

#Region "Cash And Cheque Detail Structure"

    Public Structure CashNoteInfo
        Dim NoteCount As UInt32
        Dim NoteValue As UInt32
        Dim NoteCurrency As String
        Dim bGoodNote As Boolean
    End Structure

    Public Structure TrxNoteList
        Dim NoteInfo() As CashNoteInfo
    End Structure

    Public Structure ChequeSet
        Dim ChequeStat As ChequeStatus
        Dim ChequeID As Integer
        Dim ChequeCodeline As String
        Dim ChequeImagePath As String
    End Structure

    Public Structure TrxChqList
        Dim ChequeList() As ChequeSet
    End Structure

    Public Structure BoxDenoList
        Dim NoteInfo() As CashNoteInfo
        Dim LogicalNumber As Integer
    End Structure

    Public Structure CashBoxCount
        Dim ListOfBox() As BoxDenoList
    End Structure

    Public Structure ChqBoxCount
        Dim intChqCount As Integer
    End Structure

#End Region

#Region " Enums "

    Enum ControlMDSCurrentRunningState
        Undefined = 0
        Initialize = 1
        StartTransaction = 2
    End Enum

    Enum ChequeStatus
        ReturnedToUser
        EscrowedGood
        EscrowedBad
        Stored
        Unknown
    End Enum

    Enum ControlMDSStatusOptions
        Disconnected
        NotOK
        OK
    End Enum

    Enum TransactionType
        CashMode
        ChequeMode
        CashChequeMode
    End Enum

#End Region

#Region "Events"

#Region "Event MDS"


    Public Shared Event MDSControlErrorReceived(ByVal strExMsg As String)

    'MIX Event
    Public Shared Event MDSCashNChqTransactionFailedEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)
    Public Shared Event MDSCashNChqCompleteDepositQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)
    Public Shared Event MDSWantToInsertMoreCashNChqQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)
    Public Shared Event MDSCashNChqTransactionCompletedEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)



    'Cash Event
    Public Shared Event MDSCashTransactionCompletedEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16)
    Public Shared Event MDSCashTransactionFailedEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16)
    Public Shared Event MDSWantToInsertMoreCashQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16)
    Public Shared Event MDSCashInsertItemsEvt()
    Public Shared Event MDSCashProcessingEvt()
    Public Shared Event MDSCashCleanFeederEvt()
    Public Shared Event MDSCashRepositionDocumentsEvt()
    Public Shared Event MDSCashTakeReturnedItemsEvt()
    Public Shared Event MDSCashNotifyCounterfeitEvt()
    Public Shared Event MDSCashCompleteDepositQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16)
    Public Shared Event MDSCashStatusUpdateEvt(ByVal MDSCurrentState As ControlMDSCurrentRunningState, ByVal MDSStatus As ControlMDSStatusOptions, ByVal strStatusText As String)
    Public Shared Event MDSCashTimeout()

    'Cheque Event
    Public Shared Event MDSChequeTransactionCompletedEvt(ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)
    Public Shared Event MDSChequeTransactionFailedEvt(ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)
    Public Shared Event MDSWantToInsertMoreChequeQuestionEvt(ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)
    Public Shared Event MDSChequeInsertItemsEvt()
    Public Shared Event MDSChequeProcessingEvt()
    Public Shared Event MDSChequeCleanFeederEvt()
    Public Shared Event MDSChequeRepositionDocumentsEvt()
    Public Shared Event MDSChequeTakeReturnedItemsEvt()
    Public Shared Event MDSChequeNotifyCounterfeitEvt()
    Public Shared Event MDSChqeueCompleteDepositQuestionEvt(ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)
    Public Shared Event MDSChqeueStatusUpdateEvt(ByVal MDSCurrentState As ControlMDSCurrentRunningState, ByVal MDSStatus As ControlMDSStatusOptions, ByVal strStatusText As String)
    Public Shared Event MDSChequeTimeout()


    'Supervisor Module
    Public Shared Event MDSBoxCashCount(ByVal NoteList As CashBoxCount, ByVal ChqCount As ChqBoxCount, ByVal intDenoCount As UInt16)


    Public Shared Event MDSDoorEvent(ByVal intDoorStatusID As Integer)


#End Region

#Region "Event Door"
    '0: RotoKiosk.Constants.SIU_PortStatus.WFS_SIU_NOT_AVAILABLE
    '1:RotoKiosk.Constants.SIU_PortStatus.WFS_SIU_RUN
    '2:RotoKiosk.Constants.SIU_PortStatus.WFS_SIU_MAINTENANCE
    '3:RotoKiosk.Constants.SIU_PortStatus.WFS_SIU_SUPERVISOR

    Enum MDSDoorSIUStatus
        SIU_NOT_AVAILABLE = 0
        SIU_RUN = 1
        SIU_MAINTENANCE = 2
        SIU_SUPERVISOR = 3
    End Enum

    Public Shared Event EvtDoorOpenReceived(ByVal DoorSIUStatus As MDSDoorSIUStatus)
    Public Shared Event EvtDoorClosedReceived(ByVal DoorSIUStatus As MDSDoorSIUStatus)

#End Region

#End Region


#Region "Support Property"

    'ReadOnly Property MDSHWDParamInfo As DEPOSITORCTR
    '    Get
    '        Return mdsSettingParam
    '    End Get
    'End Property

#Region "Support Property"

    ReadOnly Property EnableMDSOptHWD() As Boolean
        Get
            Return objMDSControl.DEPOSITORHWDInfo.blnCASHDEPOSITORENABLE
        End Get
    End Property

    ReadOnly Property MDSComportNoHWD() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.CASHDEPOSITORPortNum.ToString
        End Get
    End Property

    ReadOnly Property ForeAcceptChqOptHWD() As Boolean
        Get
            Return objMDSControl.DEPOSITORHWDInfo.blnCASHDEPOSITORForceAcceptChq
        End Get
    End Property

    ReadOnly Property MDSInsertItemTimeoutHWD() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.CASHDEPOSITORInsertItemTimeout.ToString
        End Get
    End Property

    ReadOnly Property MDSCleanFeederTimeoutHWD() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.CASHDEPOSITORCleanFeederTimeout.ToString
        End Get
    End Property

    ReadOnly Property MDSRepositionDocTimeoutHWD() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.CASHDEPOSITORRepositonDocTimeout.ToString
        End Get
    End Property

    ReadOnly Property MDSTakeReturnItemTimeoutHWD() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.CASHDEPOSITORTakeReturnTimeout.ToString
        End Get
    End Property

    ReadOnly Property MDSXFSLogPathHWD() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.CASHDEPOSITORTraceFilePath
        End Get
    End Property

    ReadOnly Property MDSChqImagePathHWD() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.CASHDEPOSITORChequeImagePath
        End Get
    End Property

    ReadOnly Property MDSChqTemplatePathHWD() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.CASHDEPOSITORTemplateFilePath
        End Get
    End Property

    ReadOnly Property MDSErrStatusReplyCFG() As String
        Get
            Return objMDSControl.DEPOSITORHWDInfo.strMDSErrStatusReply
        End Get
    End Property

#End Region


#End Region


#Region "Property"

    ReadOnly Property MDSChequeInBoxValue As Integer
        Get
            Return objMDSControl.MDSChequesInBox
        End Get
    End Property


    ReadOnly Property CashDepositorDeviceCode As CASHDEVICECODE
        Get
            Return cashDevCode
        End Get
    End Property

    ReadOnly Property ChequeDepositorDeviceCode As CHEQUEDEVICECODE
        Get
            Return chequeDevCode
        End Get
    End Property

    ReadOnly Property DoorDeviceCode As DOORSENSORDEVICECODE
        Get
            Return doorDevCode
        End Get
    End Property


    Private MDSCurrentState As ControlMDSCurrentRunningState

    ReadOnly Property MDSControlState As ControlMDSCurrentRunningState
        Get
            If objMDSControl.MDSCurrrentState = clsMDSXFS.enMDSCurrentRunningState.Initialize Then
                Return ControlMDSCurrentRunningState.Initialize
            ElseIf objMDSControl.MDSCurrrentState = clsMDSXFS.enMDSCurrentRunningState.StartTransaction Then
                Return ControlMDSCurrentRunningState.StartTransaction
            Else
                Return ControlMDSCurrentRunningState.Undefined
            End If
        End Get
    End Property

    ReadOnly Property MDSControlStatus As ControlMDSStatusOptions
        Get
            If objMDSControl.MDSReplyStatus = MDSControl.MDSStatusOptions.Disconnected Then
                Return ControlMDSStatusOptions.Disconnected
            ElseIf objMDSControl.MDSReplyStatus = MDSControl.MDSStatusOptions.NotOk Then
                Return ControlMDSStatusOptions.NotOK
            Else
                Return ControlMDSStatusOptions.OK
            End If
        End Get
    End Property

    ReadOnly Property StatusText As String
        Get
            Return objMDSControl.MDSReplyStatusReason
        End Get
    End Property

    Property InsertItemTimeout() As Long
        Get
            Return objMDSControl.InsertItemsTimeout
        End Get
        Set(ByVal value As Long)
            objMDSControl.InsertItemsTimeout = value
        End Set
    End Property

    Property TakeReturnedItemTimeout() As Long
        Get
            Return objMDSControl.TakeReturnedItemsTimeout
        End Get
        Set(ByVal value As Long)
            objMDSControl.TakeReturnedItemsTimeout = value
        End Set
    End Property

    Property RepositionItemTimeout() As Long
        Get
            Return objMDSControl.RepositionDocumentsTimeout
        End Get
        Set(ByVal value As Long)
            objMDSControl.RepositionDocumentsTimeout = value
        End Set
    End Property

    Property CleanReturnFeederTimeout() As Long
        Get
            Return objMDSControl.CleanFeederTimeout
        End Get
        Set(ByVal value As Long)
            objMDSControl.CleanFeederTimeout = value
        End Set
    End Property

    Property MachineStartupInterval() As Long
        Get
            Return objMDSControl.MachineStartupInterval
        End Get
        Set(ByVal value As Long)
            objMDSControl.MachineStartupInterval = value
        End Set
    End Property

    ReadOnly Property MDSDoorStatus() As Integer
        Get
            Return objMDSControl.MDSDoorStatus
        End Get
    End Property

#End Region

    Public Function InitializeMDSXFSLogger() As Boolean
        Dim blnReply As Boolean = False
        Try
            blnReply = objMDSControl.InitCASHDEPOSITControl()
            Return blnReply
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function InitializeMDS() As Boolean
        Dim blnRtn As Boolean = False
        Try

            If blnEngineStarted = False Then

                If objMDSControl.MDSCurrrentState = clsMDSXFS.enMDSCurrentRunningState.Initialize Then
                    objMDSControl.MDSStop()
                End If

                If objMDSControl.InitCASHDEPOSITHWD Then
                    If objMDSControl.MDSCurrrentState = clsMDSXFS.enMDSCurrentRunningState.Initialize Then
                        MapDeviceCode()
                        blnEngineStarted = True
                        blnRtn = True
                    Else
                        blnRtn = False
                    End If
                Else
                    blnRtn = False
                End If

            ElseIf blnEngineStarted = True Then
                blnRtn = True
            End If

            Return blnRtn
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.InitializeMDS:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDS() As Boolean
        Dim blnRtn As Boolean = False
        Try
            'If Not IsNothing(objMDSControl) Then
            'If (objMDSControl.Status = MDSControl.MDSStatusOptions.Ok) Then

            'If objMDSControl.MDSStop Then
            '    blnRtn = True
            '    blnEngineStarted = False
            'Else
            '    blnRtn = False
            'End If
            'Close the timer
            'tmrCheckDoor.Close()

            ''Else
            ''    blnRtn = False
            ''End If
            'Else
            'blnRtn = False
            'End If

            objMDSControl.MDSStop()
            blnRtn = True
            blnEngineStarted = False

            'Close the Logger
            objMDSControl.CloseMDSXFSControl()

            Return blnRtn
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.StopMDS:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StartMDSLightHWD(ByVal strUserChoice As String) As Boolean
        Try
            If objMDSControl.StartMDSLightXFS(strUserChoice) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.StartMDSLightHWD - " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSLightHWD() As Boolean

        Try
            If objMDSControl.StopMDSLightXFS Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.StopMDSLightHWD - " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StartMDSReaderLightHWD(ByVal strUserChoice As String) As Boolean
        Try
            If objMDSControl.StartMDSReaderLightXFS(strUserChoice) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.StartMDSReaderLightHWD - " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSReaderLightHWD() As Boolean

        Try
            If objMDSControl.StopMDSReaderLightXFS Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.StopMDSReaderLightHWD - " & ex.Message)
            Return False
        End Try
    End Function


    Public Sub SetCurrentTransCheque()
        'Set the Cheque Properties
        objMDSControl.SetChequeProperties()
    End Sub

    Public Sub SetCurrentTransCash()
        'Set the Cash Properties
        objMDSControl.SetCashProperties()
    End Sub

    Public Sub SetCurrentTransCashNChq()
        'Set the Cash Properties
        objMDSControl.SetCashNChqProperties()
    End Sub

    Public Function StartMDSTransaction(ByVal TransMode As TransactionType) As Boolean

        Try
            CurrentTransactionType = TransMode

            If CurrentTransactionType = TransactionType.CashMode Then
                SetCurrentTransCash()
            ElseIf CurrentTransactionType = TransactionType.ChequeMode Then
                SetCurrentTransCheque()
            ElseIf CurrentTransactionType = TransactionType.CashChequeMode Then
                SetCurrentTransCashNChq()
            End If

            If objMDSControl.StartTransaction Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.StartMDSTransaction:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSTransaction() As Boolean

        Try
            If objMDSControl.StopTransaction Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.StopMDSTransaction:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserReactYES() As Boolean
        Try
            objMDSControl.UserChoiceYes()
            Return True
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.UserReactYES:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserReactNO() As Boolean
        Try
            objMDSControl.UserChoiceNo()
            Return True
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.UserReactNO - " & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserReactCANCEL() As Boolean
        Try
            objMDSControl.UserChoiceCancel()
            Return True
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.UserReactCANCEL:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserReactTIMEOUT() As Boolean
        Try
            objMDSControl.UserChoiceTimeout()
            Return True
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.UserReactTIMEOUT:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CheckDeviceStatus() As Boolean
        Try
            If objMDSControl.MDSCurrrentState = clsMDSXFS.enMDSCurrentRunningState.Initialize Then
                If (objMDSControl.MDSReplyStatus = MDSControl.MDSStatusOptions.NotOk) Or (objMDSControl.MDSReplyStatus = MDSControl.MDSStatusOptions.Disconnected) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.CheckDeviceStatus:" & ex.Message)
            Return False
        End Try
    End Function


    Public Sub SetMDSINCashMode()
        Try
            CurrentTransactionType = TransactionType.CashMode
            objMDSControl.SetMDSCashMode()
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.SetMDSINCashMode:" & ex.Message)
        End Try
    End Sub

    Public Sub SetMDSINChequeMode()
        Try
            CurrentTransactionType = TransactionType.CashMode
            objMDSControl.SetMDSChequeMode()
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.SetMDSINChequeMode:" & ex.Message)
        End Try
    End Sub

    Public Sub GetCashBoxCounter()
        Dim Notelist As MDSXFSWrapper.clsMDSXFS.CashBox = Nothing
        Dim ChqCount As MDSXFSWrapper.clsMDSXFS.ChequeBox = Nothing
        Dim intCount As UInt16
        Try
            objMDSControl.StartExchange()
            objMDSControl.NoteCountInBox(Notelist, ChqCount, intCount)
            MapCashBoxCountCash(Notelist, ChqCount, intCount)
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.GetCashBoxCounter:" & ex.Message)
        End Try
    End Sub

    Public Sub ResetCashBoxCounter()
        Dim Notelist As MDSXFSWrapper.clsMDSXFS.CashBox = Nothing
        Dim ChqCount As MDSXFSWrapper.clsMDSXFS.ChequeBox = Nothing
        'Dim intCount As UInt16
        Try
            CurrentTransactionType = TransactionType.CashMode
            objMDSControl.EndExchange()
            'objMDSControl.NoteCountInBox(Notelist, ChqCount, intCount)
            'MapCashBoxCountCash(Notelist, ChqCount, intCount)
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.ResetCashBoxCounter:" & ex.Message)
        End Try
    End Sub

    Public Sub ResetChequeBoxCounter()
        Try
            CurrentTransactionType = TransactionType.CashMode
            objMDSControl.EndExchangeCheques()
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.ResetChequeBoxCounter:" & ex.Message)
        End Try
    End Sub

    Public Shared Sub MapStatusUpdate(ByVal MDSCurrentState As MDSXFSWrapper.clsMDSXFS.enMDSCurrentRunningState, ByVal MDSStatus As MDS_Wrapper_Control.MDSControl.MDSStatusOptions, ByVal strStatusText As String, ByVal intDoorEvent As Integer)
        Dim ControlCurrentState As ControlMDSCurrentRunningState
        Dim ControlStatus As ControlMDSStatusOptions

        Try
            If MDSCurrentState = clsMDSXFS.enMDSCurrentRunningState.Initialize Then
                ControlCurrentState = ControlMDSCurrentRunningState.Initialize
            ElseIf MDSCurrentState = clsMDSXFS.enMDSCurrentRunningState.StartTransaction Then
                ControlCurrentState = ControlMDSCurrentRunningState.StartTransaction
            Else
                ControlCurrentState = ControlMDSCurrentRunningState.Undefined
            End If

            If MDSStatus = ControlMDSStatusOptions.Disconnected Then
                ControlStatus = ControlMDSStatusOptions.Disconnected
            ElseIf MDSStatus = MDSControl.MDSStatusOptions.NotOk Then
                ControlStatus = ControlMDSStatusOptions.NotOK
            Else
                ControlStatus = ControlMDSStatusOptions.OK
            End If

            If CurrentTransactionType = TransactionType.CashMode Then
                RaiseEvent MDSCashStatusUpdateEvt(ControlCurrentState, ControlStatus, strStatusText)
                RaiseEvent MDSDoorEvent(intDoorEvent)

            ElseIf CurrentTransactionType = TransactionType.CashChequeMode Then
                RaiseEvent MDSCashStatusUpdateEvt(ControlCurrentState, ControlStatus, strStatusText)
                RaiseEvent MDSDoorEvent(intDoorEvent)
            ElseIf CurrentTransactionType = TransactionType.ChequeMode Then
                RaiseEvent MDSChqeueStatusUpdateEvt(ControlCurrentState, ControlStatus, strStatusText)
                RaiseEvent MDSDoorEvent(intDoorEvent)
            Else
                RaiseEvent MDSCashStatusUpdateEvt(ControlCurrentState, ControlStatus, strStatusText)
                RaiseEvent MDSDoorEvent(intDoorEvent)
            End If

            'If strStatusText.Contains("WFS_STAT_DEVOFFLINE") Then
            '    TriggerDoorSensor(strStatusText)
            'End If

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapStatusUpdate:" & ex.Message)
        End Try
    End Sub

    Public Shared Sub MapCashTransactionComplete(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort)
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.TrxNoteList
        Dim udtMDSControlNoteList As TrxNoteList

        Try
            udtNoteList = New MDSXFSWrapper.clsMDSXFS.TrxNoteList
            udtNoteList = Notelist

            udtMDSControlNoteList = New TrxNoteList
            ReDim udtMDSControlNoteList.NoteInfo(intCashCount)

            For i = 0 To udtNoteList.NoteInfo.Length - 1
                With udtNoteList.NoteInfo(i)
                    udtMDSControlNoteList.NoteInfo(i).bGoodNote = .bGoodNote
                    udtMDSControlNoteList.NoteInfo(i).NoteCount = .NoteCount
                    udtMDSControlNoteList.NoteInfo(i).NoteCurrency = .NoteCurrency
                    udtMDSControlNoteList.NoteInfo(i).NoteValue = .NoteValue
                End With
            Next

            RaiseEvent MDSCashTransactionCompletedEvt(udtMDSControlNoteList, intCashCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapCashTransactionComplete:" & ex.Message)
        End Try
    End Sub

    Public Shared Sub MapCashTransactionNotComplete(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort)
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.TrxNoteList
        Dim udtMDSControlNoteList As TrxNoteList

        Try
            udtNoteList = New MDSXFSWrapper.clsMDSXFS.TrxNoteList
            udtNoteList = Notelist

            udtMDSControlNoteList = New TrxNoteList
            ReDim udtMDSControlNoteList.NoteInfo(intCashCount)

            For i = 0 To udtNoteList.NoteInfo.Length - 1
                With udtNoteList.NoteInfo(i)
                    udtMDSControlNoteList.NoteInfo(i).bGoodNote = .bGoodNote
                    udtMDSControlNoteList.NoteInfo(i).NoteCount = .NoteCount
                    udtMDSControlNoteList.NoteInfo(i).NoteCurrency = .NoteCurrency
                    udtMDSControlNoteList.NoteInfo(i).NoteValue = .NoteValue
                End With
            Next

            RaiseEvent MDSCashTransactionFailedEvt(udtMDSControlNoteList, intCashCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapCashTransactionNotComplete:" & ex.Message)
        End Try
    End Sub

    Public Shared Sub MapCashInsertMoreCash(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort)
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.TrxNoteList
        Dim udtMDSControlNoteList As TrxNoteList

        Try
            udtNoteList = New MDSXFSWrapper.clsMDSXFS.TrxNoteList
            udtNoteList = Notelist

            udtMDSControlNoteList = New TrxNoteList
            ReDim udtMDSControlNoteList.NoteInfo(intCashCount)

            For i = 0 To udtNoteList.NoteInfo.Length - 1
                With udtNoteList.NoteInfo(i)
                    udtMDSControlNoteList.NoteInfo(i).bGoodNote = .bGoodNote
                    udtMDSControlNoteList.NoteInfo(i).NoteCount = .NoteCount
                    udtMDSControlNoteList.NoteInfo(i).NoteCurrency = .NoteCurrency
                    udtMDSControlNoteList.NoteInfo(i).NoteValue = .NoteValue
                End With
            Next

            RaiseEvent MDSWantToInsertMoreCashQuestionEvt(udtMDSControlNoteList, intCashCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapCashTransactionComplete:" & ex.Message)
        End Try
    End Sub

    Public Shared Sub MapCashBoxCountCash(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.CashBox, ByVal ChqCount As MDSXFSWrapper.clsMDSXFS.ChequeBox, ByVal intCashCount As UShort)
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.CashBox

        Dim udtMDSControlNoteList As CashBoxCount
        Dim udtMDSChqCount As ChqBoxCount

        'Dim intDenoCount As Integer

        Try
            udtNoteList = New MDSXFSWrapper.clsMDSXFS.CashBox
            udtNoteList = Notelist

            udtMDSControlNoteList = New CashBoxCount
            udtMDSChqCount = New ChqBoxCount

            ReDim udtMDSControlNoteList.ListOfBox(Notelist.BoxList.Length - 1)

            For x = 0 To udtMDSControlNoteList.ListOfBox.Length - 1
                If Not IsNothing(udtNoteList.BoxList(x).DenoList) Then
                    ReDim udtMDSControlNoteList.ListOfBox(x).NoteInfo(udtNoteList.BoxList(x).DenoList.Length - 1)
                End If
            Next

            For i = 0 To udtNoteList.BoxList.Length - 1
                With udtNoteList.BoxList(i)
                    udtMDSControlNoteList.ListOfBox(i).LogicalNumber = i
                    If Not IsNothing(udtNoteList.BoxList(i).DenoList) Then
                        For x = 0 To udtMDSControlNoteList.ListOfBox(i).NoteInfo.Length - 1
                            With udtMDSControlNoteList.ListOfBox(i).NoteInfo(x)
                                .bGoodNote = udtNoteList.BoxList(i).DenoList(x).bGoodNote
                                .NoteValue = udtNoteList.BoxList(i).DenoList(x).NoteValue
                                .NoteCurrency = udtNoteList.BoxList(i).DenoList(x).NoteCurrency
                                .NoteCount = udtNoteList.BoxList(i).DenoList(x).NoteCount

                            End With
                        Next
                    End If
                End With
            Next

            udtMDSChqCount.intChqCount = ChqCount.intChqCount

            RaiseEvent MDSBoxCashCount(udtMDSControlNoteList, udtMDSChqCount, intCashCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapCashTransactionComplete:" & ex.Message)
        End Try
    End Sub

    Public Shared Sub MapChequeTransactionComplete(ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort)
        'Dim udtChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList
        Dim udtMDSControlChequeList As TrxChqList

        Try
            'udtChequeList = New MDSXFSWrapper.clsMDSXFS.TrxChqList
            'udtChequeList = ChqList

            udtMDSControlChequeList = New TrxChqList
            ReDim udtMDSControlChequeList.ChequeList(intChqCount)


            'For i = 0 To udtChequeList.ChequeList.Length - 1
            '    With udtChequeList.ChequeList(i)
            '        udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
            '        udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
            '        udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
            '        udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
            '    End With
            'Next

            For i = 0 To ChqList.ChequeList.Length - 1
                With ChqList.ChequeList(i)
                    udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
                    udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
                    udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
                    udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
                End With
            Next

            RaiseEvent MDSChequeTransactionCompletedEvt(udtMDSControlChequeList, intChqCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapChequeTransactionComplete:" & ex.Message)
        End Try
    End Sub

    Public Shared Sub MapChequeTransactionNotComplete(ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort)
        Dim udtChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList
        Dim udtMDSControlChequeList As TrxChqList

        Try
            udtChequeList = New MDSXFSWrapper.clsMDSXFS.TrxChqList
            udtChequeList = ChqList

            udtMDSControlChequeList = New TrxChqList
            ReDim udtMDSControlChequeList.ChequeList(intChqCount)

            For i = 0 To udtChequeList.ChequeList.Length - 1
                With udtChequeList.ChequeList(i)
                    udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
                    udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
                    udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
                    udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
                End With
            Next

            RaiseEvent MDSChequeTransactionFailedEvt(udtMDSControlChequeList, intChqCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapChequeTransactionNotComplete:" & ex.Message)
        End Try
    End Sub

    Public Shared Sub MapChequeInsertMoreCheque(ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort)
        Dim udtChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList
        Dim udtMDSControlChequeList As TrxChqList

        Try
            udtChequeList = New MDSXFSWrapper.clsMDSXFS.TrxChqList
            udtChequeList = ChqList

            udtMDSControlChequeList = New TrxChqList
            ReDim udtMDSControlChequeList.ChequeList(intChqCount - 1)

            For i = 0 To udtChequeList.ChequeList.Length - 1
                With udtChequeList.ChequeList(i)
                    udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
                    udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
                    udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
                    udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
                End With
            Next

            RaiseEvent MDSWantToInsertMoreChequeQuestionEvt(udtMDSControlChequeList, intChqCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapChequeTransactionComplete:" & ex.Message)
        End Try
    End Sub

  
  

    Public Shared Sub MapInsertMoreItem()
        If CurrentTransactionType = TransactionType.CashMode Then
            RaiseEvent MDSCashInsertItemsEvt()
        ElseIf CurrentTransactionType = TransactionType.CashChequeMode Then
            RaiseEvent MDSCashInsertItemsEvt()
        ElseIf CurrentTransactionType = TransactionType.ChequeMode Then
            RaiseEvent MDSChequeInsertItemsEvt()
        End If
    End Sub

    Public Shared Sub MapNotifyCounterfeit()
        If CurrentTransactionType = TransactionType.CashMode Then
            RaiseEvent MDSCashNotifyCounterfeitEvt()
        ElseIf CurrentTransactionType = TransactionType.CashChequeMode Then
            RaiseEvent MDSCashNotifyCounterfeitEvt()
        ElseIf CurrentTransactionType = TransactionType.ChequeMode Then
            RaiseEvent MDSChequeNotifyCounterfeitEvt()
        End If
    End Sub

    Public Shared Sub MapProcessing()
        If CurrentTransactionType = TransactionType.CashMode Then
            RaiseEvent MDSCashProcessingEvt()
        ElseIf CurrentTransactionType = TransactionType.CashChequeMode Then
            RaiseEvent MDSCashProcessingEvt()
        ElseIf CurrentTransactionType = TransactionType.ChequeMode Then
            RaiseEvent MDSChequeProcessingEvt()
        End If

    End Sub


    Private Shared Sub objMDSControl_MDSCashTimeout() Handles objMDSControl.MDSCashTimeout
        RaiseEvent MDSCashTimeout()
    End Sub

    Private Shared Sub objMDSControl_MDSCashTransactionCompletedEvt(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort) Handles objMDSControl.MDSCashTransactionCompletedEvt
        MapCashTransactionComplete(Notelist, intCashCount)
    End Sub

    Private Shared Sub objMDSControl_MDSCashTransactionFailedEvt(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort) Handles objMDSControl.MDSCashTransactionFailedEvt
        MapCashTransactionNotComplete(Notelist, intCashCount)
    End Sub

    Private Shared Sub objMDSControl_MDSChequeTransactionCompletedEvt(ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSChequeTransactionCompletedEvt
        MapChequeTransactionComplete(ChqList, intChqCount)
    End Sub

    Private Shared Sub objMDSControl_MDSChequeTransactionFailedEvt(ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSChequeTransactionFailedEvt
        MapChequeTransactionNotComplete(ChqList, intChqCount)
    End Sub

    Private Shared Sub objMDSControl_MDSInsertItemsEvt() Handles objMDSControl.MDSInsertItemsEvt
        MapInsertMoreItem()
    End Sub

    Private Shared Sub objMDSControl_MDSNotifyCounterfeitEvt() Handles objMDSControl.MDSNotifyCounterfeitEvt
        MapNotifyCounterfeit()
    End Sub

    Private Shared Sub objMDSControl_MDSProcessingEvt() Handles objMDSControl.MDSProcessingEvt
        MapProcessing()
    End Sub


    Private Shared Sub objMDSControl_MDSWantToInsertMoreCashQuestionEvt(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort) Handles objMDSControl.MDSWantToInsertMoreCashQuestionEvt
        MapCashInsertMoreCash(Notelist, intCashCount)
    End Sub

    Private Shared Sub objMDSControl_MDSWantToInsertMoreChequeQuestionEvt(ByVal ChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSWantToInsertMoreChequeQuestionEvt
        MapChequeInsertMoreCheque(ChequeList, intChqCount)
    End Sub

#Region "MDS Reject item Control"

    Private Shared Sub objMDSControl_MDSRepositionDocumentsEvt() Handles objMDSControl.MDSRepositionDocumentsEvt
        If CurrentTransactionType = TransactionType.CashMode Then
            RaiseEvent MDSCashRepositionDocumentsEvt()
        ElseIf CurrentTransactionType = TransactionType.CashChequeMode Then
            RaiseEvent MDSCashRepositionDocumentsEvt()
        ElseIf CurrentTransactionType = TransactionType.ChequeMode Then
            RaiseEvent MDSChequeRepositionDocumentsEvt()
        End If
    End Sub

    Private Shared Sub objMDSControl_MDSTakeReturnedItemsEvt() Handles objMDSControl.MDSTakeReturnedItemsEvt
        If CurrentTransactionType = TransactionType.CashMode Then
            RaiseEvent MDSCashTakeReturnedItemsEvt()
        ElseIf CurrentTransactionType = TransactionType.CashChequeMode Then
            RaiseEvent MDSCashTakeReturnedItemsEvt()
        ElseIf CurrentTransactionType = TransactionType.ChequeMode Then
            RaiseEvent MDSChequeTakeReturnedItemsEvt()
        End If
    End Sub

    Private Shared Sub objMDSControl_MDSCleanFeederEvt() Handles objMDSControl.MDSCleanFeederEvt
        If CurrentTransactionType = TransactionType.CashMode Then
            RaiseEvent MDSCashCleanFeederEvt()
        ElseIf CurrentTransactionType = TransactionType.CashChequeMode Then
            RaiseEvent MDSCashCleanFeederEvt()
        ElseIf CurrentTransactionType = TransactionType.ChequeMode Then
            RaiseEvent MDSChequeCleanFeederEvt()
        End If
    End Sub

#End Region

    Private Shared Sub objMDSControl_MDSStatusUpdateEvt(ByVal MDSCurrentState As MDSXFSWrapper.clsMDSXFS.enMDSCurrentRunningState, ByVal MDSStatus As MDS_Wrapper_Control.MDSControl.MDSStatusOptions, ByVal strStatusText As String, ByVal intDoorEvent As Integer) Handles objMDSControl.MDSStatusUpdateEvt
        MapStatusUpdate(MDSCurrentState, MDSStatus, strStatusText, intDoorEvent)
        'RaiseEvent MDSDoorEvent(intDoorEvent)
    End Sub


    Private Shared Sub objMDSControl_MDSChequeTimeout() Handles objMDSControl.MDSChequeTimeout
        RaiseEvent MDSChequeTimeout()
    End Sub

    Private Shared Sub MapDeviceCode()
        Try
            With objMDSControl.CASHDEPOSITORDeviceCode
                cashDevCode.CASHDEVDeviceID = .CASHDEVDeviceID
                cashDevCode.CASHDEVDeviceName = .CASHDEVDeviceName

                cashDevCode.CASHDEVErrDevState = .CASHDEVErrDevState
                cashDevCode.CASHDEVErrorDGSTStatus = .CASHDEVErrorDGSTStatus

                cashDevCode.CASHDEVErrorStateError = .CASHDEVErrorStateError
                cashDevCode.CASHDEVErrorStateFatal = .CASHDEVErrorStateFatal
                cashDevCode.CASHDEVErrorStateNoError = .CASHDEVErrorStateNoError
                cashDevCode.CASHDEVErrorStateWarning = .CASHDEVErrorStateWarning

                cashDevCode.CASHDEVErrorSysStatNoState = .CASHDEVErrorSysStatNoState

                cashDevCode.CASHDEVEventDataArrive = .CASHDEVEventDataArrive
                cashDevCode.CASHDEVEventError = .CASHDEVEventError
                cashDevCode.CASHDEVEventTimeout = .CASHDEVEventTimeout
                cashDevCode.CASHDEVEventWrap = .CASHDEVEventWrap
            End With

            With objMDSControl.CHQDEPOSITORDeviceCode
                chequeDevCode.CHQDEVDeviceID = .CHQDEVDeviceID
                chequeDevCode.CHQDEVDeviceName = .CHQDEVDeviceName

                chequeDevCode.CHQDEVErrDevState = .CHQDEVErrDevState
                chequeDevCode.CHQDEVErrorDGSTStatus = .CHQDEVErrorDGSTStatus

                chequeDevCode.CHQDEVErrorStateError = .CHQDEVErrorStateError
                chequeDevCode.CHQDEVErrorStateFatal = .CHQDEVErrorStateFatal
                chequeDevCode.CHQDEVErrorStateNoError = .CHQDEVErrorStateNoError
                chequeDevCode.CHQDEVErrorStateWarning = .CHQDEVErrorStateWarning

                chequeDevCode.CHQDEVErrorSysStatNoState = .CHQDEVErrorSysStatNoState

                chequeDevCode.CHQDEVEventDataArrive = .CHQDEVEventDataArrive
                chequeDevCode.CHQDEVEventError = .CHQDEVEventError
                chequeDevCode.CHQDEVEventTimeout = .CHQDEVEventTimeout
                chequeDevCode.CHQDEVEventWrap = .CHQDEVEventWrap
            End With

            With objMDSControl.DOORDeviceCode
                doorDevCode.DOORDEVDeviceID = .DOORDEVDeviceID
                doorDevCode.DOORDEVDeviceName = .DOORDEVDeviceName

                doorDevCode.DOORDEVEventDataArrive = .DOORDEVEventDataArrive
                doorDevCode.DOORDEVEventError = .DOORDEVEventError
                doorDevCode.DOORDEVEventTimeout = .DOORDEVEventTimeout
                doorDevCode.DOORDEVEventWrap = .DOORDEVEventWrap
            End With

        Catch ex As Exception

        End Try
    End Sub


#Region "New MDS Control"


    'MDS Status Option - 0:Disconnect 1:NotOk 2:OK
    ReadOnly Property MDSReplyStatusHWD As String
        Get
            If objMDSControl.MDSReplyStatus = MDSControl.MDSStatusOptions.Disconnected Then
                Return "Disconnected"
            ElseIf objMDSControl.MDSReplyStatus = MDSControl.MDSStatusOptions.NotOk Then
                Return "NotOk"
            ElseIf objMDSControl.MDSReplyStatus = MDSControl.MDSStatusOptions.Ok Then
                Return "Ok"
            Else
                Return "Error Unknown"
            End If
        End Get
    End Property

    'Reason for MDS
    ReadOnly Property MDSReplyStatusReasonHWD As String
        Get
            Return objMDSControl.MDSReplyStatusReason
        End Get
    End Property


    'MDS Status Details
    ReadOnly Property MDSReplyStatusDetailsHWD As String
        Get
            Return objMDSControl.MDSReplyStatusDetails
        End Get
    End Property

    ReadOnly Property MDSReplyUserStatusHWD As String
        Get
            If objMDSControl.MDSReplyUserStatus = MDSControl.UserStatusOptions.Ok Then
                Return "Ok"
            ElseIf objMDSControl.MDSReplyUserStatus = MDSControl.UserStatusOptions.Timeout Then
                Return "Timeout"
            Else
                Return "Error Unknown"
            End If
        End Get
    End Property


#End Region


#Region "NotesInPhysicalBoxStatus"

    ReadOnly Property MDSNoteInAllBoxCountHWD As String
        Get
            Return objMDSControl.MDSNoteInAllBoxCount
        End Get
    End Property


#End Region

#Region "Retract Note"

    ReadOnly Property MDSRetractBoxNoteCountHWD As String
        Get
            Return objMDSControl.MDSRetractBoxNoteCount
        End Get
    End Property


#End Region

#Region "Counterfeit Note"

    ReadOnly Property MDSConterfeitBoxNoteCountHWD As String
        Get
            Return objMDSControl.MDSConterfeitBoxNoteCount
        End Get
    End Property


#End Region


#Region "New Function"


    Public Function HWDGetNoteInBox(ByVal strLogicalBox As String) As String
        Dim strReply As String = ""
        Try
            strReply = objMDSControl.MDSReplyNoteInBox(strLogicalBox)
            Return strReply
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.HWDGetNoteInBox:" & ex.Message)
            Return ""
        End Try
    End Function


    Public Function MDSReInitialise() As Boolean
        Dim blnReply As Boolean = False
        Try
            blnReply = objMDSControl.MDSResetCleanPaperPath
            Return blnReply
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MDSReInitialise:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function MDSClose() As Boolean
        Dim blnReply As Boolean = False
        Try
            blnReply = objMDSControl.MDSClose
            Return blnReply
        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MDSClose:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "Update MDS Setting"

    Public Function UpdateMDSHwdLayerParamSetting(ByVal strEnableOpt As String, ByVal strForeChqOpt As String, ByVal strComportNo As String, ByVal strInsertItemTMOut As String, ByVal strRepositionTMOut As String, ByVal strCleanFeederTMOut As String, ByVal strTakeReturnTMOut As String, ByVal strTraceLogPath As String, ByVal strChqImagePath As String, ByVal strChqTemplatePath As String) As Boolean
        Try
            If objMDSControl.UpdateMDSHWDLayerSetting(strEnableOpt, strForeChqOpt, strComportNo, strInsertItemTMOut, strRepositionTMOut, strCleanFeederTMOut, strTakeReturnTMOut, strTraceLogPath, strChqImagePath, strChqTemplatePath) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "New Events"

    Private Shared Sub objMDSControl_MDSCASHCompleteDepositQuestionEvt(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort) Handles objMDSControl.MDSCASHCompleteDepositQuestionEvt
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.TrxNoteList
        Dim udtMDSControlNoteList As TrxNoteList

        Try
            udtNoteList = New MDSXFSWrapper.clsMDSXFS.TrxNoteList
            udtNoteList = Notelist

            udtMDSControlNoteList = New TrxNoteList
            ReDim udtMDSControlNoteList.NoteInfo(intCashCount)

            For i = 0 To udtNoteList.NoteInfo.Length - 1
                With udtNoteList.NoteInfo(i)
                    udtMDSControlNoteList.NoteInfo(i).bGoodNote = .bGoodNote
                    udtMDSControlNoteList.NoteInfo(i).NoteCount = .NoteCount
                    udtMDSControlNoteList.NoteInfo(i).NoteCurrency = .NoteCurrency
                    udtMDSControlNoteList.NoteInfo(i).NoteValue = .NoteValue
                End With
            Next

            RaiseEvent MDSCashCompleteDepositQuestionEvt(udtMDSControlNoteList, intCashCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapCashTransactionCompleteQQ:" & ex.Message)
        End Try
    End Sub

    Private Shared Sub objMDSControl_MDSCHQCompleteDepositQuestionEvt(ByVal ChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSCHQCompleteDepositQuestionEvt
        Dim udtChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList
        Dim udtMDSControlChequeList As TrxChqList

        Try

            udtChequeList = New MDSXFSWrapper.clsMDSXFS.TrxChqList
            udtChequeList = ChequeList

            udtMDSControlChequeList = New TrxChqList
            ReDim udtMDSControlChequeList.ChequeList(intChqCount)

            For i = 0 To udtChequeList.ChequeList.Length - 1
                With udtChequeList.ChequeList(i)
                    udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
                    udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
                    udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
                    udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
                End With
            Next

            RaiseEvent MDSChqeueCompleteDepositQuestionEvt(udtMDSControlChequeList, intChqCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapChequeTransactionCompleteQQ:" & ex.Message)
        End Try
    End Sub

#End Region


#Region "Mix Mode"

    Private Shared Sub objMDSControl_MDSWantToInsertMoreCashNChqQuestionEvt(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort, ByVal ChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSWantToInsertMoreCashNChqQuestionEvt
        'MapCashInsertMoreCash(Notelist, intCashCount)
        'MapChequeInsertMoreCheque(ChequeList, intChqCount)
        MapCashNChqInsertMoreCash(Notelist, intCashCount, ChequeList, intChqCount)
    End Sub

    Public Shared Sub MapCashNChqInsertMoreCash(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort, ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort)
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.TrxNoteList
        Dim udtMDSControlNoteList As TrxNoteList

        Dim udtChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList
        Dim udtMDSControlChequeList As TrxChqList

        Try
            udtNoteList = New MDSXFSWrapper.clsMDSXFS.TrxNoteList
            udtNoteList = Notelist

            udtMDSControlNoteList = New TrxNoteList
            ReDim udtMDSControlNoteList.NoteInfo(intCashCount)

            For i = 0 To udtNoteList.NoteInfo.Length - 1
                With udtNoteList.NoteInfo(i)
                    udtMDSControlNoteList.NoteInfo(i).bGoodNote = .bGoodNote
                    udtMDSControlNoteList.NoteInfo(i).NoteCount = .NoteCount
                    udtMDSControlNoteList.NoteInfo(i).NoteCurrency = .NoteCurrency
                    udtMDSControlNoteList.NoteInfo(i).NoteValue = .NoteValue
                End With
            Next

            'Chq Part
            udtChequeList = New MDSXFSWrapper.clsMDSXFS.TrxChqList
            udtChequeList = ChqList

            udtMDSControlChequeList = New TrxChqList
            ReDim udtMDSControlChequeList.ChequeList(intChqCount - 1)

            For i = 0 To udtChequeList.ChequeList.Length - 1
                With udtChequeList.ChequeList(i)
                    udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
                    udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
                    udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
                    udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
                End With
            Next


            'RaiseEvent MDSWantToInsertMoreCashQuestionEvt(udtMDSControlNoteList, intCashCount)
            'RaiseEvent MDSWantToInsertMoreChequeQuestionEvt(udtMDSControlChequeList, intChqCount)

            RaiseEvent MDSWantToInsertMoreCashNChqQuestionEvt(udtMDSControlNoteList, intCashCount, udtMDSControlChequeList, intChqCount)


        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapCashNChqInsertMoreCash:" & ex.Message)
        End Try
    End Sub


    Private Shared Sub objMDSControl_MDSCASHNCHQCompleteDepositQuestionEvt(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort, ByVal ChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSCASHNCHQCompleteDepositQuestionEvt
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.TrxNoteList
        Dim udtMDSControlNoteList As TrxNoteList

        Dim udtChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList
        Dim udtMDSControlChequeList As TrxChqList


        Try
            'CASH PART
            udtNoteList = New MDSXFSWrapper.clsMDSXFS.TrxNoteList
            udtNoteList = Notelist

            udtMDSControlNoteList = New TrxNoteList
            ReDim udtMDSControlNoteList.NoteInfo(intCashCount)

            For i = 0 To udtNoteList.NoteInfo.Length - 1
                With udtNoteList.NoteInfo(i)
                    udtMDSControlNoteList.NoteInfo(i).bGoodNote = .bGoodNote
                    udtMDSControlNoteList.NoteInfo(i).NoteCount = .NoteCount
                    udtMDSControlNoteList.NoteInfo(i).NoteCurrency = .NoteCurrency
                    udtMDSControlNoteList.NoteInfo(i).NoteValue = .NoteValue
                End With
            Next


            'CHQ Part
            udtChequeList = New MDSXFSWrapper.clsMDSXFS.TrxChqList
            udtChequeList = ChequeList

            udtMDSControlChequeList = New TrxChqList
            ReDim udtMDSControlChequeList.ChequeList(intChqCount)

            For i = 0 To udtChequeList.ChequeList.Length - 1
                With udtChequeList.ChequeList(i)
                    udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
                    udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
                    udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
                    udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
                End With
            Next

            'RaiseEvent MDSCashCompleteDepositQuestionEvt(udtMDSControlNoteList, intCashCount)
            'RaiseEvent MDSChqeueCompleteDepositQuestionEvt(udtMDSControlChequeList, intChqCount)

            RaiseEvent MDSCashNChqCompleteDepositQuestionEvt(udtMDSControlNoteList, intCashCount, udtMDSControlChequeList, intChqCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MDSCASHNCHQCompleteDepositQuestionEvt:" & ex.Message)
        End Try
    End Sub

    Private Shared Sub objMDSControl_MDSCashNChqTransactionCompletedEvt(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort, ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSCashNChqTransactionCompletedEvt
        'MapCashTransactionComplete(Notelist, intCashCount)
        'MapChequeTransactionComplete(ChqList, intChqCount)
        MapCashNChqTransactionComplete(Notelist, intCashCount, ChqList, intChqCount)
    End Sub

    Public Shared Sub MapCashNChqTransactionComplete(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort, ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort)
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.TrxNoteList
        Dim udtMDSControlNoteList As TrxNoteList

        Dim udtMDSControlChequeList As TrxChqList

        Try
            udtNoteList = New MDSXFSWrapper.clsMDSXFS.TrxNoteList
            udtNoteList = Notelist

            udtMDSControlNoteList = New TrxNoteList
            ReDim udtMDSControlNoteList.NoteInfo(intCashCount)

            For i = 0 To udtNoteList.NoteInfo.Length - 1
                With udtNoteList.NoteInfo(i)
                    udtMDSControlNoteList.NoteInfo(i).bGoodNote = .bGoodNote
                    udtMDSControlNoteList.NoteInfo(i).NoteCount = .NoteCount
                    udtMDSControlNoteList.NoteInfo(i).NoteCurrency = .NoteCurrency
                    udtMDSControlNoteList.NoteInfo(i).NoteValue = .NoteValue
                End With
            Next

            'Chq Part
            udtMDSControlChequeList = New TrxChqList
            ReDim udtMDSControlChequeList.ChequeList(intChqCount)

            For i = 0 To ChqList.ChequeList.Length - 1
                With ChqList.ChequeList(i)
                    udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
                    udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
                    udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
                    udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
                End With
            Next

            'RaiseEvent MDSCashTransactionCompletedEvt(udtMDSControlNoteList, intCashCount)
            'RaiseEvent MDSChequeTransactionCompletedEvt(udtMDSControlChequeList, intChqCount)

            RaiseEvent MDSCashNChqTransactionCompletedEvt(udtMDSControlNoteList, intCashCount, udtMDSControlChequeList, intChqCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapCashNChqTransactionComplete:" & ex.Message)
        End Try
    End Sub



    Private Shared Sub objMDSControl_MDSCashNChqTransactionFailedEvt(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort, ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort) Handles objMDSControl.MDSCashNChqTransactionFailedEvt
        'MapCashTransactionNotComplete(Notelist, intCashCount)
        'MapChequeTransactionNotComplete(ChqList, intChqCount)
        MapCashNChqTransactionNotComplete(Notelist, intCashCount, ChqList, intChqCount)
    End Sub

    Public Shared Sub MapCashNChqTransactionNotComplete(ByVal Notelist As MDSXFSWrapper.clsMDSXFS.TrxNoteList, ByVal intCashCount As UShort, ByVal ChqList As MDSXFSWrapper.clsMDSXFS.TrxChqList, ByVal intChqCount As UShort)
        Dim udtNoteList As MDSXFSWrapper.clsMDSXFS.TrxNoteList
        Dim udtMDSControlNoteList As TrxNoteList

        Dim udtChequeList As MDSXFSWrapper.clsMDSXFS.TrxChqList
        Dim udtMDSControlChequeList As TrxChqList

        Try

            'CASH PART

            udtNoteList = New MDSXFSWrapper.clsMDSXFS.TrxNoteList
            udtNoteList = Notelist

            udtMDSControlNoteList = New TrxNoteList
            ReDim udtMDSControlNoteList.NoteInfo(intCashCount)

            For i = 0 To udtNoteList.NoteInfo.Length - 1
                With udtNoteList.NoteInfo(i)
                    udtMDSControlNoteList.NoteInfo(i).bGoodNote = .bGoodNote
                    udtMDSControlNoteList.NoteInfo(i).NoteCount = .NoteCount
                    udtMDSControlNoteList.NoteInfo(i).NoteCurrency = .NoteCurrency
                    udtMDSControlNoteList.NoteInfo(i).NoteValue = .NoteValue
                End With
            Next

            'CHQ PART
            udtChequeList = New MDSXFSWrapper.clsMDSXFS.TrxChqList
            udtChequeList = ChqList

            udtMDSControlChequeList = New TrxChqList
            ReDim udtMDSControlChequeList.ChequeList(intChqCount)

            For i = 0 To udtChequeList.ChequeList.Length - 1
                With udtChequeList.ChequeList(i)
                    udtMDSControlChequeList.ChequeList(i).ChequeCodeline = .ChequeCodeline
                    udtMDSControlChequeList.ChequeList(i).ChequeID = .ChequeID
                    udtMDSControlChequeList.ChequeList(i).ChequeImagePath = .ChequeImagePath
                    udtMDSControlChequeList.ChequeList(i).ChequeStat = .ChequeStat
                End With
            Next

            RaiseEvent MDSCashNChqTransactionFailedEvt(udtMDSControlNoteList, intCashCount, udtMDSControlChequeList, intChqCount)

        Catch ex As Exception
            RaiseEvent MDSControlErrorReceived("Error clsMDSModuleControl.MapCashNChqTransactionNotComplete:" & ex.Message)
        End Try
    End Sub


#End Region

End Class
