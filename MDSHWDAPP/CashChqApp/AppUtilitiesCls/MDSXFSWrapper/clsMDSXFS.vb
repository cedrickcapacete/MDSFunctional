Imports MDS_Wrapper_Control
Imports clsAppMainDirectory
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class clsMDSXFS
    Implements IDisposable

#Region "XFS MDS Object"

    'MDS_Wrapper_Control - Rototype

    Private WithEvents mdsobj As MDSControl

#End Region


#Region "Cls Variable"

    Dim strTitle As String = "clsMDSXFS"
    Private MDSCurrentState As enMDSCurrentRunningState

    Dim notelist As TrxNoteList = Nothing
    Dim chqlist As TrxChqList = Nothing

#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If Not IsNothing(mdsobj) Then
                    mdsobj = Nothing
                End If
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "Events - MDS"

    Public Event MDSInsertItemsEvt()
    Public Event MDSProcessingEvt()
    Public Event MDSCleanFeederEvt()
    Public Event MDSRepositionDocumentsEvt()
    Public Event MDSTakeReturnedItemsEvt()
    Public Event MDSNotifyCounterfeitEvt()

    Public Event MDSWantToInsertMoreCashQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16)
    Public Event MDSWantToInsertMoreChequeQuestionEvt(ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)

    'New Edit MIX  - 28/03/2017
    Public Event MDSWantToInsertMoreCashNChqQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)



    'Public Event MDSTakeReturnedItemsEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)
    'Public Event MDSCompleteDepositQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)

    Public Event MDSCASHCompleteDepositQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16)
    Public Event MDSCHQCompleteDepositQuestionEvt(ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)

    'New Edit MIX  - 28/03/2017
    Public Event MDSCASHNCHQCompleteDepositQuestionEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChequeList As TrxChqList, ByVal intChqCount As UInt16)


    Public Event MDSStatusUpdateEvt(ByVal MDSCurrentState As enMDSCurrentRunningState, ByVal MDSStatus As MDSControl.MDSStatusOptions, ByVal strStatusText As String, ByVal intDoorEvent As Integer)

    Public Event MDSCashTransactionCompletedEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16)
    Public Event MDSCashTransactionFailedEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16)

    Public Event MDSChequeTransactionCompletedEvt(ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)
    Public Event MDSChequeTransactionFailedEvt(ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)

    'New Edit MIX - 28/03/2017
    Public Event MDSCashNChqTransactionCompletedEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)
    Public Event MDSCashNChqTransactionFailedEvt(ByVal Notelist As TrxNoteList, ByVal intCashCount As UInt16, ByVal ChqList As TrxChqList, ByVal intChqCount As UInt16)

    Public Event MDSChequeTimeout()
    Public Event MDSCashTimeout()

    'Supervisor
    Public Event MDSNoteListCount(ByVal NotelistCnt As TrxNoteList, ByVal intCountDenoAvailable As UInt16)

#End Region

#Region "Enums "

    Enum enMDSCurrentRunningState
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

#End Region

#Region "Structure - DEPOSITORCTR,CASHDEVICECODE,CHEQUEDEVICECODE,DOORSENSORDEVICECODE"


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

        'MICR Validation
        Dim intMinMICRLEN As Integer
        Dim intMaxErrMark As Integer

        'MDS Status Control
        Dim strMDSErrStatusReply As String

        'NEW MIX MODE MICR CFG Control
        Dim blnMIXModeMICRCheck As Boolean

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

#Region "Structure - Cash and Cheque"

    Public Structure DenoInfo
        Dim NoteCount As UInt32
        Dim NoteValue As UInt32
        Dim NoteCurrency As String
        Dim bGoodNote As Boolean
    End Structure

    Public Structure BoxInfo
        Dim LogicalID As Integer
        Dim DenoList() As DenoInfo
    End Structure

    Public Structure CashBox
        Dim BoxList() As BoxInfo
    End Structure

    Public Structure ChequeBox
        Dim intChqCount As Integer
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

    Public Structure CashNoteInfo
        Dim NoteLogicalBoxNum As UInt32
        Dim NoteCount As UInt32
        Dim NoteValue As UInt32
        Dim NoteCurrency As String
        Dim bGoodNote As Boolean
    End Structure

    Public Structure TrxNoteList
        Dim NoteInfo() As CashNoteInfo
    End Structure

    Public Structure MDSCIMStatusDetail
        Dim CIMDeviceStatus As UInteger
        Dim SafeDoorStatus As UInteger
        Dim AcceptorStatus As UInteger
        Dim IntermediateStackerStatus As UInteger
        Dim StackerItemsStatus As UInteger
        Dim BanknoteReaderStatus As UInteger
        'Dim dwGuidLights() As UInteger
        Dim InputShutterStatus As UInteger
        Dim InputPositionStatus As UInteger
        Dim InputTransportStatus As UInteger
        Dim InputTransportFillingStatus As UInteger
        Dim OutputShutterStatus As UInteger
        Dim OutputPositionStatus As UInteger
        Dim OutputTransportStatus As UInteger
        Dim OutputTransportFillingStatus As UInteger

        Dim dwGuidLights As List(Of RotoKiosk.Constants.GuidanceLight)
        Dim LogicalBoxStatus As List(Of RotoKiosk.Constants.BoxStatus)
        Dim PyhsicalBoxStatus As List(Of RotoKiosk.Constants.BoxStatus)
        Dim NotesInPhysicalBox As List(Of UInteger)

        Dim SuiPortStatus As UInteger

    End Structure

    Public Structure MDSIPMStatusDetail
        Dim IPMDeviceStatus As UInteger
        Dim RefusedShutterStatus As UInteger
        Dim RefusedPositionStatus As UInteger
        Dim RefusedTransportStatus As UInteger
        Dim RefusedTransportFillingStatus As UInteger
        Dim MediaStatus As UInteger
        Dim TonerStatus As UInteger
        Dim InkStatus As UInteger
        Dim FrontScannerStatus As UInteger
        Dim BackScannerStatus As UInteger
        Dim MICRStatus As UInteger
        Dim MediaFeederStatus As UInteger
        Dim StackerEnabled As Boolean
        Dim CMC7Mapping As String
        Dim E13BMapping As String
        Dim ChequeBoxCount As UInteger
        Dim IPMRetractOperationsCount As UInteger
        Dim ChequeBoxStatus As UInteger
        Dim IPRetractBoxStatus As UInteger
    End Structure

    Public Structure MDSStatusDetailInfo
        Dim DeviceStatus As UInteger
        Dim CIMStatusDetail As MDSCIMStatusDetail
        Dim IPMStatusDetail As MDSIPMStatusDetail
    End Structure

#End Region

#Region "Cls Property"

    ReadOnly Property DEPOSITORHWDInfo() As DEPOSITORCTR
        Get
            Return udtDEPOSITORHWDCFG
        End Get
    End Property

    ReadOnly Property CASHDEPOSITORDeviceCode() As CASHDEVICECODE
        Get
            Return udtCASHMODEDevCode
        End Get
    End Property

    ReadOnly Property CHQDEPOSITORDeviceCode() As CHEQUEDEVICECODE
        Get
            Return udtCHQMODEDevCode
        End Get
    End Property


    ReadOnly Property DOORDeviceCode() As DOORSENSORDEVICECODE
        Get
            Return udtDOORSENSORDevCode
        End Get
    End Property


#End Region

#Region "Properties"

    ReadOnly Property MDSChequesInBox As UInt32
        Get
            Return mdsobj.ChequesInBox
        End Get
    End Property

    ReadOnly Property MDSCurrrentState As enMDSCurrentRunningState
        Get
            Return MDSCurrentState
        End Get
    End Property

    ReadOnly Property MDSDoorStatus As Integer
        Get
            Return intMDSDoorStatus
        End Get
    End Property

    ReadOnly Property StatusDetail As MDSStatusDetailInfo
        Get
            Dim mdsStsDet As MDSStatusDetailInfo

            'Overall Device Status
            mdsStsDet.DeviceStatus = mdsobj.MDSStatusDetails.DeviceStatus

            'Status for Cash In Module

            With mdsStsDet.CIMStatusDetail
                .CIMDeviceStatus = mdsobj.MDSStatusDetails.CIMDeviceStatus
                .SafeDoorStatus = mdsobj.MDSStatusDetails.SafeDoorStatus
                .AcceptorStatus = mdsobj.MDSStatusDetails.AcceptorStatus
                .IntermediateStackerStatus = mdsobj.MDSStatusDetails.IntermediateStackerStatus
                .StackerItemsStatus = mdsobj.MDSStatusDetails.StackerItemsStatus
                .BanknoteReaderStatus = mdsobj.MDSStatusDetails.BanknoteReaderStatus
                .InputShutterStatus = mdsobj.MDSStatusDetails.InputShutterStatus
                .InputPositionStatus = mdsobj.MDSStatusDetails.InputPositionStatus
                .InputTransportStatus = mdsobj.MDSStatusDetails.InputTransportStatus
                .InputTransportFillingStatus = mdsobj.MDSStatusDetails.InputTransportFillingStatus
                .OutputShutterStatus = mdsobj.MDSStatusDetails.OutputShutterStatus
                .OutputPositionStatus = mdsobj.MDSStatusDetails.OutputPositionStatus
                .OutputTransportStatus = mdsobj.MDSStatusDetails.OutputTransportStatus
                .OutputTransportFillingStatus = mdsobj.MDSStatusDetails.OutputTransportFillingStatus

                .LogicalBoxStatus = New List(Of RotoKiosk.Constants.BoxStatus)
                .PyhsicalBoxStatus = New List(Of RotoKiosk.Constants.BoxStatus)
                .NotesInPhysicalBox = New List(Of UInteger)
                .dwGuidLights = New List(Of RotoKiosk.Constants.GuidanceLight)

                .dwGuidLights.AddRange(mdsobj.MDSStatusDetails.dwGuidLights)
                .LogicalBoxStatus.AddRange(mdsobj.MDSStatusDetails.LogicalBoxStatus)
                .PyhsicalBoxStatus.AddRange(mdsobj.MDSStatusDetails.PhysicalBoxStatus)
                .NotesInPhysicalBox.AddRange(mdsobj.MDSStatusDetails.NotesInPhysicalBox)
            End With


            'Status for Cheque Module
            With mdsStsDet.IPMStatusDetail
                .IPMDeviceStatus = mdsobj.MDSStatusDetails.IPMDeviceStatus
                .RefusedShutterStatus = mdsobj.MDSStatusDetails.RefusedShutterStatus
                .RefusedPositionStatus = mdsobj.MDSStatusDetails.RefusedPositionStatus
                .RefusedTransportStatus = mdsobj.MDSStatusDetails.RefusedTransportStatus
                .RefusedTransportFillingStatus = mdsobj.MDSStatusDetails.RefusedTransportFillingStatus
                .MediaStatus = mdsobj.MDSStatusDetails.MediaStatus
                .TonerStatus = mdsobj.MDSStatusDetails.TonerStatus
                .InkStatus = mdsobj.MDSStatusDetails.InkStatus
                .FrontScannerStatus = mdsobj.MDSStatusDetails.FrontScannerStatus
                .BackScannerStatus = mdsobj.MDSStatusDetails.BackScannerStatus
                .MICRStatus = mdsobj.MDSStatusDetails.MICRStatus
                .MediaFeederStatus = mdsobj.MDSStatusDetails.MediaFeederStatus
                .StackerEnabled = mdsobj.MDSStatusDetails.StackerEnabled
                .CMC7Mapping = mdsobj.MDSStatusDetails.CMC7Mapping
                .E13BMapping = mdsobj.MDSStatusDetails.E13BMapping

                '.ChequeBoxCount = mdsobj.MDSStatusDetails.ChequeBoxCount
                '.IPMRetractOperationsCount = mdsobj.MDSStatusDetails.IPMRetractOperationsCount
                '.ChequeBoxStatus = mdsobj.MDSStatusDetails.ChequeBoxStatus
                '.IPRetractBoxStatus = mdsobj.MDSStatusDetails.IPRetractBoxStatus
            End With

            Return mdsStsDet
        End Get
    End Property

    Property InsertItemsTimeout() As UInt16
        Get
            Return mdsobj.GetTimeout(MDSControl.UserNotificationOptions.InsertItems)
        End Get
        Set(ByVal value As UInt16)
            WriteInsertFeederTimeout(value)
            If Not IsNothing(mdsobj) Then
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.InsertItems, value)
            End If
        End Set
    End Property

    Property TakeReturnedItemsTimeout() As UInt16
        Get
            Return mdsobj.GetTimeout(MDSControl.UserNotificationOptions.TakeReturnedItems)
        End Get
        Set(ByVal value As UInt16)
            WriteTakeReturnedItemTimeout(value)
            If Not IsNothing(mdsobj) Then
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.TakeReturnedItems, value)
            End If
        End Set
    End Property

    Property RepositionDocumentsTimeout() As UInt16
        Get
            Return mdsobj.GetTimeout(MDSControl.UserNotificationOptions.RepositionDocuments)
        End Get
        Set(ByVal value As UInt16)
            WriteRepositionTimeout(value)
            If Not IsNothing(mdsobj) Then
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.RepositionDocuments, value)
            End If
        End Set
    End Property

    Property CleanFeederTimeout() As UInt16
        Get
            Return mdsobj.GetTimeout(MDSControl.UserNotificationOptions.CleanFeeder)
        End Get
        Set(ByVal value As UInt16)
            WriteCleanFeederTimeout(value)
            If Not IsNothing(mdsobj) Then
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.CleanFeeder, value)
            End If
        End Set
    End Property

    Property MachineStartupInterval() As UInt16
        Get
            Return udtDEPOSITORHWDCFG.CASHDEPOSITORMachineStartupTimeout
        End Get
        Set(ByVal value As UInt16)
            WriteMachineStartupTimeout(value)
        End Set
    End Property


#End Region


#Region "InitCls/Close Object -  Control"

    'Init Class Object
    Public Function InitCASHDEPOSITControl() As Boolean
        Dim strLogIniPath As String = String.Empty
        Dim strCASHDEPOSITORHWDIniPath As String = String.Empty
        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(CASHDEPOSITORHWDLAYERINIPATH, MDS) = True Then

                'Read INI File
                'Log Ini File
                '1.Cardreader Hardware
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strCASHDEPOSITORHWDIniPath = .strAppLayerINIPath1.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                
                'LOG ININ PATH
                AppLogInfo("MDS XFS Wrapper Layer Class Init Ok")
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                AppLogInfo("XFS HWD INI PATH:" & strCASHDEPOSITORHWDIniPath.Trim)

                AppLogInfo("==== Start Read MDS Setting ====")

                'Read Ini File Setting
                ReadCASHDEPOSITORHWDCFG()

                'Read the Decode Setting
                ReadDEPOSITORDeviceCode()

                AppLogInfo("==== End Read MDS Setting ====")

                blnInitAppLayer = True
                Return True

            Else
                blnInitAppLayer = False
                Return False
            End If

        Catch ex As Exception
            blnInitAppLayer = False
            Return False
        End Try
    End Function

    'Close Control
    Public Function CloseMDSXFSControl() As Boolean
        Try
            If blnInitAppLayer = True Then
                'Close Logger
                'CloseLog()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


#Region "MDS INI SETTING"

    Public Function ReadCASHDEPOSITORHWDCFG() As Boolean
        Try
            'Read CASHDEPOSITOR HWD CFG
            If ReadCASHDEPOSITORSetting() = True Then

                AppLogInfo("Read MDS HWD CFG Info OK")

                With udtDEPOSITORHWDCFG
                    AppLogInfo("MDS EnableOpt:" & .blnCASHDEPOSITORENABLE)
                    AppLogInfo("PortNum:" & .CASHDEPOSITORPortNum)

                    AppLogInfo("OperationMode:" & .CASHDEPOSITOROperationMode)
                    AppLogInfo("TraceFile:" & .CASHDEPOSITORTraceFilePath)
                    AppLogInfo("ChequeImgPath:" & .CASHDEPOSITORChequeImagePath)
                    AppLogInfo("TemplateFile:" & .CASHDEPOSITORTemplateFilePath)

                    AppLogInfo("InsertItemTimeout:" & .CASHDEPOSITORInsertItemTimeout)
                    AppLogInfo("RepositionDoc:" & .CASHDEPOSITORRepositonDocTimeout)
                    AppLogInfo("TakeReturn:" & .CASHDEPOSITORTakeReturnTimeout)
                    AppLogInfo("CleanFeeder:" & .CASHDEPOSITORCleanFeederTimeout)

                    AppLogInfo("ForceAcceptCheque:" & .blnCASHDEPOSITORForceAcceptChq)


                    AppLogInfo("Min MICR Length:" & .intMinMICRLEN)
                    AppLogInfo("Max MICR QMark:" & .intMaxErrMark)


                    AppLogInfo("MDS ErrStatus Control String:" & .strMDSErrStatusReply)

                    AppLogInfo("MDS MIX Mode MICR Check=" & .blnMIXModeMICRCheck)

                    'AppLogInfo("DEPOSITOR ImgConversion:" & .blnIMGConversion)
                    'AppLogInfo("DEPOSITOR DOORCheckInterval:" & .DOORCheckInterval)

                    'MDSLightFlashing = 1
                    'MDSCardRearderLightFlashing = 4

                    'AppLogInfo("MDS Light Flashing:" & .strMDSLightMode)
                    'AppLogInfo("MDS CardReaderLight Flashing:" & .strMDSCardReaderLightMode)
                End With

                Return True
            Else
                AppLogErr("Read clsMDSXFS.Read MDS HWD CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".ReadDEPOSITORHWDCFG. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function ReadDEPOSITORDeviceCode() As Boolean
        Try
            'Read CASHDEPOSITOR HWD CFG
            If ReadDEPOSITORCASHDEVCODE() = True Then
                AppLogInfo("Read DEPOSITOR Device Code - CASH OK")
                AppLogInfo("Device NAME:" & udtCASHMODEDevCode.CASHDEVDeviceName)
                If ReadDEPOSITORCHEQUEDEVCODE() = True Then
                    AppLogInfo("Read DEPOSITOR Device Code - CHEQUE OK")
                    AppLogInfo("Device NAME:" & udtCHQMODEDevCode.CHQDEVDeviceName)
                    If ReadDOORSENSORDEVCODE() = True Then
                        AppLogInfo("Read DEPOSITOR Device Code - DOOR OK")
                        AppLogInfo("Device NAME:" & udtDOORSENSORDevCode.DOORDEVDeviceName)
                        Return True
                    Else
                        AppLogErr("Read DEPOSITOR Device Code - DOOR Failed")
                        Return False
                    End If
                Else
                    AppLogErr("Read DEPOSITOR Device Code - CHEQUE Failed")
                    Return False
                End If
            Else
                AppLogErr("Read DEPOSITOR Device Code - CASH Failed")
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".ReadDEPOSITORDeviceCode. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function


#End Region


#Region "Initialse Object Function - InitCASHDEPOSITObject"


    Public Function InitCASHDEPOSITObject() As Boolean
        Try

            AppLogInfo("clsMDSXFS - InitCASHDEPOSITObject")

            mdsobj = New MDSControl

            With udtDEPOSITORHWDCFG
                mdsobj.ComPort = .CASHDEPOSITORPortNum                      'COM Port  
                mdsobj.LogPath = .CASHDEPOSITORTraceFilePath                'Path to store MDS hardware trace file
            End With

            MDSCurrentState = enMDSCurrentRunningState.Undefined
            Return True

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.InitCASHDEPOSITObject: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function InitCASHDEPOSITHWD() As Boolean
        Dim blnRtn As Boolean = False

        Try
            AppLogInfo("clsMDSXFS.InitCASHDEPOSITHWD")

            If InitCASHDEPOSITObject() Then
                If Not IsNothing(mdsobj) Then               'Check if object created
                    MDSCurrentState = enMDSCurrentRunningState.Initialize
                    mdsobj.Initialise()
                    blnRtn = True
                Else
                    AppLogErr("Error in clsMDSXFS.InitCASHDEPOSITHWD: Object Not Initialized") 'Cannot proceed to initialize because object not created
                    blnRtn = False
                End If
            Else
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.InitCASHDEPOSITHWD: " & ex.Message)
            Return False
        End Try
    End Function


#End Region


#Region "Set Property - SetCashProperties,SetChequeProperties,SetCashNChqProperties"


    Public Sub SetCashProperties()
        Try

            AppLogInfo("clsMDSXFS.SetCashProperties: Cash Only")

            With udtDEPOSITORHWDCFG
                mdsobj.ImagePath = .CASHDEPOSITORChequeImagePath            'Path to store cheque imamge
                mdsobj.TemplateFile = .CASHDEPOSITORTemplateFilePath        'Template File Name
                mdsobj.Mode = MACHINEMODE_CASH

                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.InsertItems, .CASHDEPOSITORInsertItemTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.CleanFeeder, .CASHDEPOSITORCleanFeederTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.RepositionDocuments, .CASHDEPOSITORRepositonDocTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.TakeReturnedItems, .CASHDEPOSITORTakeReturnTimeout)
            End With

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.SetCashProperties: " & ex.Message)
        End Try
    End Sub


    Public Sub SetCashNChqProperties()
        Try

            AppLogInfo("clsMDSXFS.SetCashNChqProperties: Mix Mode")

            With udtDEPOSITORHWDCFG
                mdsobj.ImagePath = .CASHDEPOSITORChequeImagePath            'Path to store cheque imamge
                mdsobj.TemplateFile = .CASHDEPOSITORTemplateFilePath        'Template File Name
                mdsobj.Mode = MACHINEMODE_MIX

                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.InsertItems, .CASHDEPOSITORInsertItemTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.CleanFeeder, .CASHDEPOSITORCleanFeederTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.RepositionDocuments, .CASHDEPOSITORRepositonDocTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.TakeReturnedItems, .CASHDEPOSITORTakeReturnTimeout)
            End With

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.SetCashNChqProperties: " & ex.Message)
        End Try
    End Sub

    Public Sub SetChequeProperties()
        Try

            AppLogInfo("clsMDSXFS.SetChequeProperties: Cheque Only")

            With udtDEPOSITORHWDCFG

                mdsobj.ImagePath = .CASHDEPOSITORChequeImagePath            'Path to store cheque imamge
                mdsobj.TemplateFile = .CASHDEPOSITORTemplateFilePath        'Template File Name
                mdsobj.Mode = MACHINEMODE_CHEQUE                            'use CIM Mode

                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.InsertItems, .CASHDEPOSITORInsertItemTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.CleanFeeder, .CASHDEPOSITORCleanFeederTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.RepositionDocuments, .CASHDEPOSITORRepositonDocTimeout)
                mdsobj.SetTimeout(MDSControl.UserNotificationOptions.TakeReturnedItems, .CASHDEPOSITORTakeReturnTimeout)
            End With

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.SetCashProperties: " & ex.Message)
        End Try
    End Sub

#End Region


#Region "MDS Command - MDSStop"

    Public Function MDSStop() As Boolean
        Try
            'AppLogInfo("clsMDSXFS.MDSStop")

            'If (mdsobj.MDSStatus = MDSControl.MDSStatusOptions.Ok) Then
            '    mdsobj.Stop()
            'ElseIf (mdsobj.MDSStatus = MDSControl.MDSStatusOptions.NotOk) Then
            '    mdsobj.Stop()
            '    AppLogErr(mdsobj.MDSStatusText)
            '    Return False
            'ElseIf (mdsobj.MDSStatus = MDSControl.MDSStatusOptions.Disconnected) Then
            '    AppLogErr(mdsobj.MDSStatusText)
            'End If

            mdsobj.Stop()
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.MDSStop: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StartTransaction() As Boolean
        Try
            AppLogInfo("clsMDSXFS.StartTransaction")
            mdsobj.TransactionStart()       'Start the transaction  
            MDSCurrentState = enMDSCurrentRunningState.StartTransaction
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StartTransaction: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopTransaction() As Boolean
        Try
            AppLogInfo("clsMDSXFS.StopTransaction")

            If UserChoice(USER_CHOICE_CANCEL) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StopTransaction: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserChoiceNo() As Boolean
        Try
            AppLogInfo("clsMDSXFS.UserChoiceNo")
            UserChoice(USER_CHOICE_NO)
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.UserChoiceNo: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserChoiceCancel() As Boolean
        Try
            AppLogInfo("clsMDSXFS.UserChoiceCancel")
            UserChoice(USER_CHOICE_CANCEL)
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.UserChoiceCancel: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserChoiceYes() As Boolean
        Try
            AppLogInfo("clsMDSXFS.UserChoiceYes")
            UserChoice(USER_CHOICE_YES)
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.UserChoiceYes: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserChoiceTimeout() As Boolean
        Try
            AppLogInfo("clsMDSXFS.UserChoiceTimeout")
            UserChoice(USER_CHOICE_TIMEOUT)
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.UserChoiceTimeout: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function UserChoice(ByVal strChoice As String) As Boolean
        Try
            AppLogInfo("clsMDSXFS.UserChoice")

            If (strChoice = USER_CHOICE_YES) Then
                mdsobj.UserChoice(MDSControl.UserOptions.Yes)
            ElseIf (strChoice = USER_CHOICE_NO) Then
                mdsobj.UserChoice(MDSControl.UserOptions.No)
            ElseIf (strChoice = USER_CHOICE_CANCEL) Then
                mdsobj.UserChoice(MDSControl.UserOptions.Cancel)
            ElseIf (strChoice = USER_CHOICE_TIMEOUT) Then
                mdsobj.UserChoice(MDSControl.UserOptions.Timeout)
            End If
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.UserChoice: " & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "Events XFS"

    Private Sub mdsobj_InitialiseCompleted(ByVal sender As Object) Handles mdsobj.InitialiseCompleted
        Try
            AppLogInfo("clsMDSXFS.InitialiseCompleted")
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.mdsobj_InitialiseCompleted: " & ex.Message)
        End Try
    End Sub

    Private Sub mdsobj_UserNotificationEvent(ByVal sender As Object, ByVal uno As MDS_Wrapper_Control.MDSControl.UserNotificationOptions) Handles mdsobj.UserNotification
        'Dim notelist As TrxNoteList = Nothing
        Dim intCashCount As UInt16 = 0

        'Dim chqlist As TrxChqList = Nothing
        Dim intChqCount As UInt16 = 0

        Dim strValueInfo As String = ""


        Try

            strValueInfo = ReplyUserNotificationInfo(uno)
            AppLogInfo("clsMDSXFS.User Notification Info : " & uno & strValueInfo)
            LastState = uno

            If (uno = MDSControl.UserNotificationOptions.InsertItems) Then

                '0-InsertItems
                AppLogInfo("RaiseEvent-MDSInsertItemsEvt")
                RaiseEvent MDSInsertItemsEvt()

            ElseIf (uno = MDSControl.UserNotificationOptions.Processing) Then

                '7-Processing
                AppLogInfo("RaiseEvent-MDSProcessingEvt")
                RaiseEvent MDSProcessingEvt()

            ElseIf (uno = MDSControl.UserNotificationOptions.CleanFeeder) Then
               
                '1-CleanFeeder
                AppLogInfo("RaiseEvent-MDSCleanFeederEvt")
                RaiseEvent MDSCleanFeederEvt()

            ElseIf (uno = MDSControl.UserNotificationOptions.RepositionDocuments) Then
              
                '2-RepositionDocuments
                AppLogInfo("RaiseEvent-MDSRepositionDocumentsEvt")
                RaiseEvent MDSRepositionDocumentsEvt()

            ElseIf (uno = MDSControl.UserNotificationOptions.TakeReturnedItems) Then

                'FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                'FillChqList(chqlist, intChqCount, mdsobj.TransactionChequeList)
                'RaiseEvent MDSTakeReturnedItemsEvt(notelist, intCashCount, chqlist, intChqCount)

                'Take returned items from return slot
                '3-TakeReturnedItems
                AppLogInfo("RaiseEvent-MDSTakeReturnedItemsEvt")
                RaiseEvent MDSTakeReturnedItemsEvt()

            ElseIf (uno = MDSControl.UserNotificationOptions.NotifyCounterfeit) Then

                '4-NotifyCounterfeit
                AppLogInfo("RaiseEvent-MDSNotifyCounterfeitEvt")
                RaiseEvent MDSNotifyCounterfeitEvt()

            ElseIf (uno = MDSControl.UserNotificationOptions.WantToInsertMoreQuestion) Then

                'Validation
                '5-WantToInsertMoreQuestion
                If mdsobj.Mode = MACHINEMODE_CASH Then
                    FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                    AppLogInfo("RaiseEvent-MDSWantToInsertMoreCashQuestionEvt")
                    RaiseEvent MDSWantToInsertMoreCashQuestionEvt(notelist, intCashCount)
                ElseIf mdsobj.Mode = MACHINEMODE_CHEQUE Then
                    ValidationFillChqList(chqlist, intChqCount, mdsobj.TransactionChequeList)
                    AppLogInfo("RaiseEvent-MDSWantToInsertMoreChequeQuestionEvt")
                    RaiseEvent MDSWantToInsertMoreChequeQuestionEvt(chqlist, intChqCount)
                ElseIf mdsobj.Mode = MACHINEMODE_MIX Then
                    'New Edit 28/03/2017
                    FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                    ValidationFillChqList(chqlist, intChqCount, mdsobj.TransactionChequeList)
                    AppLogInfo("RaiseEvent- MDSWantToInsertMoreCashNChqQuestionEvt")
                    RaiseEvent MDSWantToInsertMoreCashNChqQuestionEvt(notelist, intCashCount, chqlist, intChqCount)
                End If

            ElseIf (uno = MDSControl.UserNotificationOptions.CompleteDepositQuestion) Then

                '6-CompleteDepositQuestion
                If mdsobj.Mode = MACHINEMODE_CASH Then
                    FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                    AppLogInfo("RaiseEvent-MDSCASHCompleteDepositQuestionEvt")
                    RaiseEvent MDSCASHCompleteDepositQuestionEvt(notelist, intCashCount)
                ElseIf mdsobj.Mode = MACHINEMODE_CHEQUE Then
                    FillChqList(chqlist, intChqCount, mdsobj.TransactionChequeList)
                    AppLogInfo("RaiseEvent-MDSCHQCompleteDepositQuestionEvt")
                    RaiseEvent MDSCHQCompleteDepositQuestionEvt(chqlist, intChqCount)
                ElseIf mdsobj.Mode = MACHINEMODE_MIX Then
                    FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                    FillChqList(chqlist, intChqCount, mdsobj.TransactionChequeList)
                    AppLogInfo("RaiseEvent- MDSCASHNCHQCompleteDepositQuestionEvt")
                    RaiseEvent MDSCASHNCHQCompleteDepositQuestionEvt(notelist, intCashCount, chqlist, intChqCount)
                End If

            End If

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.UserNotification: " & ex.Message)
        End Try
    End Sub

    Private Function ReplyUserNotificationInfo(ByVal intValue As MDSControl.UserNotificationOptions) As String
        Dim strReplyInfo As String = ""
        Try

            Select Case intValue

                Case MDSControl.UserNotificationOptions.InsertItems   '0-InsertItems
                    strReplyInfo = "-InsertItems"
                Case MDSControl.UserNotificationOptions.CleanFeeder '1-CleanFeeder
                    strReplyInfo = "-CleanFeeder"
                Case MDSControl.UserNotificationOptions.RepositionDocuments '2-RepositionDocuments
                    strReplyInfo = "-RepositionDocuments"
                Case MDSControl.UserNotificationOptions.TakeReturnedItems '3-TakeReturnedItems
                    strReplyInfo = "-TakeReturnedItems"
                Case MDSControl.UserNotificationOptions.NotifyCounterfeit '4-NotifyCounterfeit
                    strReplyInfo = "-NotifyCounterfeit"
                Case MDSControl.UserNotificationOptions.WantToInsertMoreQuestion '5-WantToInsertMoreQuestion
                    strReplyInfo = "-WantToInsertMoreQuestion"
                Case MDSControl.UserNotificationOptions.CompleteDepositQuestion '6-CompleteDepositQuestion
                    strReplyInfo = "-CompleteDepositQuestion"
                Case MDSControl.UserNotificationOptions.Processing '7-Processing
                    strReplyInfo = "-Processing"
                Case Else
                    strReplyInfo = "-Unknown"
            End Select

            Return strReplyInfo

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.UserNotification: " & ex.Message)
            strReplyInfo = "-UnknownErr"
            Return strReplyInfo
        End Try
    End Function

    Private Sub mdsobj_StatusUpdateEvent(ByVal sender As Object) Handles mdsobj.StatusUpdate
        Try

            'get all the mdsstatus
            'for the Door suiport status
            intMDSDoorStatus = mdsobj.MDSStatusDetails.SiuPortStatus

            '*MDS Status Update 
            AppLogInfo("clsMDSXFS.MDSStatusUpdateEvt Info - DoorStatus=" & intMDSDoorStatus)
            AppLogInfo("RaiseEvent-MDSStatusUpdateEvt")
            RaiseEvent MDSStatusUpdateEvt(MDSCurrentState, mdsobj.MDSStatus, mdsobj.MDSStatusText, intMDSDoorStatus)

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StatusUpdate: " & ex.Message)
        End Try
    End Sub

    Private Sub mdsobj_TransactionCompleted(ByVal sender As Object, ByVal uno As MDS_Wrapper_Control.MDSControl.TransactionResult) Handles mdsobj.TransactionCompleted
        Dim notelist As TrxNoteList = Nothing
        Dim chqList As TrxChqList = Nothing
        Dim intChqCnt As UInt16 = 0
        Dim intCashCount As UInt16 = 0

        Try

            AppLogInfo("clsMDSXFS.mdsobj_TransactionCompleted TransStatus: " & uno & "-" & MDSUnoStatesDesc(uno))
            AppLogInfo("clsMDSXFS.mdsobj_TransactionCompleted UserStatus: " & mdsobj.UserStatus & "-" & MDSUserStatesDesc(uno))

            If (uno = MDSControl.TransactionResult.Ok) Then
                If mdsobj.UserStatus = MDSControl.UserStatusOptions.Ok Then
                    If mdsobj.Mode = MACHINEMODE_CASH Then
                        FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                        AppLogInfo("RaiseEvent-MDSCashTransactionCompletedEvt")
                        RaiseEvent MDSCashTransactionCompletedEvt(notelist, intCashCount)
                    ElseIf mdsobj.Mode = MACHINEMODE_CHEQUE Then
                        FillChqList(chqList, intChqCnt, mdsobj.TransactionChequeList)
                        AppLogInfo("RaiseEvent-MDSChequeTransactionCompletedEvt")
                        RaiseEvent MDSChequeTransactionCompletedEvt(chqList, intChqCnt)
                    ElseIf mdsobj.Mode = MACHINEMODE_MIX Then
                        FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                        FillChqList(chqList, intChqCnt, mdsobj.TransactionChequeList)
                        AppLogInfo("RaiseEvent- MDSCashNChqTransactionCompletedEvt")
                        RaiseEvent MDSCashNChqTransactionCompletedEvt(notelist, intCashCount, chqList, intChqCnt)
                    End If
                ElseIf mdsobj.UserStatus = MDSControl.UserStatusOptions.Timeout Then
                    If mdsobj.Mode = MACHINEMODE_CASH Then
                        AppLogInfo("RaiseEvent-MDSCashTimeout")
                        RaiseEvent MDSCashTimeout()
                    ElseIf mdsobj.Mode = MACHINEMODE_CHEQUE Then
                        AppLogInfo("RaiseEvent-MDSChequeTimeout")
                        RaiseEvent MDSChequeTimeout()
                    ElseIf mdsobj.Mode = MACHINEMODE_MIX Then
                        AppLogInfo("RaiseEvent-MDSCashNChqTimeout")
                        RaiseEvent MDSCashTimeout()
                    End If
                End If
            Else
                If mdsobj.Mode = MACHINEMODE_CASH Then
                    FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                    AppLogInfo("RaiseEvent-MDSCashTransactionFailedEvt")
                    RaiseEvent MDSCashTransactionFailedEvt(notelist, intCashCount)
                ElseIf mdsobj.Mode = MACHINEMODE_CHEQUE Then
                    FillChqList(chqList, intChqCnt, mdsobj.TransactionChequeList)
                    AppLogInfo("RaiseEvent-MDSChequeTransactionFailedEvt")
                    RaiseEvent MDSChequeTransactionFailedEvt(chqList, intChqCnt)
                ElseIf mdsobj.Mode = MACHINEMODE_MIX Then
                    FillNoteList(notelist, intCashCount, mdsobj.TransactionNoteList)
                    FillChqList(chqList, intChqCnt, mdsobj.TransactionChequeList)
                    AppLogInfo("RaiseEvent-MDSCashNChqTransactionFailedEvt")
                    RaiseEvent MDSCashNChqTransactionFailedEvt(notelist, intCashCount, chqList, intChqCnt)
                End If
            End If
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.TransactionCompleted: " & ex.Message)
        End Try
    End Sub

    Public Function MDSUnoStatesDesc(ByVal uno As MDS_Wrapper_Control.MDSControl.TransactionResult) As String
        Dim strReplyDesc As String = ""
        Try

            Select Case uno

                Case MDSControl.TransactionResult.Ok ' 0
                    strReplyDesc = "TransactionResult-Ok"
                Case MDSControl.TransactionResult.Failed '1
                    strReplyDesc = "TransactionResult-Failed"

            End Select

            Return strReplyDesc
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.TransactionCompleted: " & ex.Message)
            Return "SysErrUnknown"
        End Try
    End Function

    Public Function MDSUserStatesDesc(ByVal uso As MDS_Wrapper_Control.MDSControl.UserStatusOptions) As String
        Dim strReplyDesc As String = ""
        Try

            Select Case uso

                Case MDSControl.UserStatusOptions.Ok '0
                    strReplyDesc = "UserStatusOptions-Ok"
                Case MDSControl.UserStatusOptions.Timeout  '1
                    strReplyDesc = "UserStatusOptions-Timeout"

            End Select

            Return strReplyDesc
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.TransactionCompleted: " & ex.Message)
            Return "SysErrUnknown"
        End Try
    End Function


#End Region

#Region "Function Control FillNoteList, FillChqList"


    Public Sub FillNoteList(ByRef notelist As TrxNoteList, ByRef intCount As Int16, ByVal mdsNotelist As IEnumerable)
        Dim ns As MDSControl.NoteSet
        Dim i As Int16 = 0

        Try

            AppLogInfo("clsMDSXFS.FillNoteList")

            intCount = 0
            'Get the total nos of Currency Type Captured in the current transaction in MDS
            For Each ns In mdsNotelist
                intCount += 1
            Next

            ReDim notelist.NoteInfo(intCount)

            'Fill the CashNoteInfo structure with the Currency Info
            For Each ns In mdsNotelist

                notelist.NoteInfo(i).NoteCount = ns.Number
                notelist.NoteInfo(i).NoteValue = ns.Value
                notelist.NoteInfo(i).NoteCurrency = ns.Currency
                If (ns.Level = MDSControl.NoteSet.NoteLevel.Good) Then
                    notelist.NoteInfo(i).bGoodNote = True
                ElseIf (ns.Level = MDSControl.NoteSet.NoteLevel.Counterfeit) Then
                    notelist.NoteInfo(i).bGoodNote = False
                End If

                'Note Info
                AppLogInfo("clsMDSXFS.Note Number :" & ns.Number)
                AppLogInfo("clsMDSXFS.Note Value :" & ns.Value)
                AppLogInfo("clsMDSXFS.Note Currency :" & ns.Currency)
                AppLogInfo("clsMDSXFS.Note Level :" & ns.Level)

                i += 1
            Next

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.FillNoteList: " & ex.Message)
        End Try
    End Sub

    Public Sub FillChqList(ByRef chqlist As TrxChqList, ByRef intCount As Int16, ByVal mdsChqlist As IEnumerable)

        Dim cqs As MDSControl.ChequeSet
        Dim i As Int16 = 0

        Dim strMICRTemp As String = ""

        Try

            AppLogInfo("clsMDSXFS.FillChqList")

            intCount = 0

            For Each cqs In mdsChqlist
                intCount += 1
            Next

            ReDim chqlist.ChequeList(intCount - 1)

            For Each cqs In mdsChqlist
                With chqlist.ChequeList(i)
                    .ChequeCodeline = cqs.Codeline
                    .ChequeID = cqs.ID
                    .ChequeImagePath = cqs.ImagePath
                    .ChequeStat = cqs.Status

                    'AppLogInfo("Before MICR Validation Original Value:-")
                    'AppLogInfo("clsMDSXFS.Cheque Deposit Number :" & i)
                    'AppLogInfo("clsMDSXFS.Cheque ID :" & cqs.ID)
                    'AppLogInfo("clsMDSXFS.Cheque Codeline :" & cqs.Codeline)
                    'AppLogInfo("clsMDSXFS.Cheque ImagePath :" & cqs.ImagePath)
                    'AppLogInfo("clsMDSXFS.Cheque Status :" & cqs.Status)

                    'AppLogInfo("-- MICR Validation Process --")

                    ''Edit - 22/Dec/2015
                    ''MICR Validaiton
                    'If udtDEPOSITORHWDCFG.blnCASHDEPOSITORForceAcceptChq = False Then
                    '    strMICRTemp = cqs.Codeline
                    '    If IsBadChqMICRCodeline(strMICRTemp) = True Then
                    '        .ChequeStat = ChequeStatus.EscrowedBad
                    '        'mdsobj.RejectCheque(.ChequeID)
                    '        AppLogInfo("After MICR Validation:Chq Satatus - Bad.")
                    '    Else
                    '        .ChequeStat = ChequeStatus.EscrowedGood
                    '        'mdsobj.AcceptCheque(.ChequeID)
                    '        AppLogInfo("After MICR Validation:Chq Satatus - Good.")
                    '    End If
                    'Else
                    '    'AppLogInfo("clsMDSXFS.ForceAcceptCheque")
                    '    'mdsobj.AcceptCheque(.ChequeID)
                    '    .ChequeStat = ChequeStatus.EscrowedGood
                    '    AppLogInfo("After ForceAcceptChq: All Chq Satatus - Good.")
                    'End If

                    'Cheque Info
                    'AppLogInfo("After MICR Validation Value:-")
                    AppLogInfo("clsMDSXFS.Cheque Deposit Number :" & i)
                    AppLogInfo("clsMDSXFS.Cheque ID :" & .ChequeID)
                    AppLogInfo("clsMDSXFS.Cheque Codeline :" & .ChequeCodeline)
                    AppLogInfo("clsMDSXFS.Cheque ImagePath :" & .ChequeImagePath)
                    AppLogInfo("clsMDSXFS.Cheque Status :" & .ChequeStat)

                End With
                i += 1
            Next

            ''Fore to Accepted the Bad Cheque
            ''Control
            'If udtDEPOSITORHWDCFG.blnCASHDEPOSITORForceAcceptChq = True Then
            '    ForceAcceptCheque(ChqList)
            'End If

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.FillChqList: " & ex.Message)
        End Try
    End Sub

    'Public Sub ForceAcceptCheque(ByRef ChqList As TrxChqList)
    '    Dim intChqInt As Integer
    '    Try
    '        AppLogInfo("clsMDSXFS.ForceAcceptCheque")

    '        For i = 0 To ChqList.ChequeList.Length - 1
    '            intChqInt = ChqList.ChequeList(i).ChequeID
    '            If ChqList.ChequeList(i).ChequeStat = ChequeStatus.EscrowedBad Then
    '                'ChqList.ChequeList(i).ChequeStat = ChequeStatus.EscrowedGood
    '                mdsobj.AcceptCheque(intChqInt)
    '            End If
    '        Next
    '    Catch ex As Exception
    '        AppLogErr("Error in clsMDSXFS.ForceAcceptCheque: " & ex.Message)
    '    End Try
    'End Sub

    Public Function IsBadChqMICRCodeline(ByVal strMICRCode As String) As Boolean
        Dim intMICRErrMark As Integer
        Dim strTmpErrMark As String = String.Empty
        Dim inti As Integer

        Dim MICR_MINLENGTH As Integer
        Dim MICR_MAXErrMark As Integer

        Try

            'Setting
            With udtDEPOSITORHWDCFG
                MICR_MINLENGTH = .intMinMICRLEN
                MICR_MAXErrMark = .intMaxErrMark
            End With

            'Input - MICR Line
            strMICRCode = strMICRCode.Trim


            'Invalid MICR Length less than 20
            If strMICRCode.Length = 0 Or strMICRCode.Length < MICR_MINLENGTH Then
                Return True
            Else

                'Calculate the MICR Error Mark = ?
                intMICRErrMark = 0
                For inti = 0 To strMICRCode.Length - 1
                    strTmpErrMark = strMICRCode.Substring(inti, 1)
                    If StrComp(strTmpErrMark, "?", CompareMethod.Text) = 0 Then
                        intMICRErrMark = intMICRErrMark + 1
                    End If
                Next

                If intMICRErrMark >= MICR_MAXErrMark Then
                    Return True
                Else
                    Return False
                End If

            End If
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.IsBadChqMICRCodeline: " & ex.Message)
            Return True
        End Try
    End Function


    Public Sub ValidationFillNoteList(ByRef notelist As TrxNoteList, ByRef intCount As Int16, ByVal mdsNotelist As IEnumerable)
        Dim ns As MDSControl.NoteSet
        Dim i As Int16 = 0

        Try

            AppLogInfo("clsMDSXFS.FillNoteList")

            intCount = 0
            'Get the total nos of Currency Type Captured in the current transaction in MDS
            For Each ns In mdsNotelist
                intCount += 1
            Next

            ReDim notelist.NoteInfo(intCount)

            'Fill the CashNoteInfo structure with the Currency Info
            For Each ns In mdsNotelist

                notelist.NoteInfo(i).NoteCount = ns.Number
                notelist.NoteInfo(i).NoteValue = ns.Value
                notelist.NoteInfo(i).NoteCurrency = ns.Currency
                If (ns.Level = MDSControl.NoteSet.NoteLevel.Good) Then
                    notelist.NoteInfo(i).bGoodNote = True
                ElseIf (ns.Level = MDSControl.NoteSet.NoteLevel.Counterfeit) Then
                    notelist.NoteInfo(i).bGoodNote = False
                End If

                'Note Info
                AppLogInfo("clsMDSXFS.Note Number :" & ns.Number)
                AppLogInfo("clsMDSXFS.Note Value :" & ns.Value)
                AppLogInfo("clsMDSXFS.Note Currency :" & ns.Currency)
                AppLogInfo("clsMDSXFS.Note Level :" & ns.Level)

                i += 1
            Next

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.FillNoteList: " & ex.Message)
        End Try
    End Sub

    Public Sub ValidationFillChqList(ByRef chqlist As TrxChqList, ByRef intCount As Int16, ByVal mdsChqlist As IEnumerable)

        Dim cqs As MDSControl.ChequeSet
        Dim i As Int16 = 0

        Dim strMICRTemp As String = ""

        Try

            AppLogInfo("clsMDSXFS.ValidationFillChqList")

            intCount = 0

            For Each cqs In mdsChqlist
                intCount += 1
            Next

            ReDim chqlist.ChequeList(intCount - 1)

            For Each cqs In mdsChqlist
                With chqlist.ChequeList(i)
                    .ChequeCodeline = cqs.Codeline
                    .ChequeID = cqs.ID
                    .ChequeImagePath = cqs.ImagePath
                    '.ChequeStat = cqs.Status

                    AppLogInfo("Before MICR Validation Original Value:-")
                    AppLogInfo("clsMDSXFS.Cheque Deposit Number :" & i)
                    AppLogInfo("clsMDSXFS.Cheque ID :" & cqs.ID)
                    AppLogInfo("clsMDSXFS.Cheque Codeline :" & cqs.Codeline)
                    AppLogInfo("clsMDSXFS.Cheque ImagePath :" & cqs.ImagePath)
                    AppLogInfo("clsMDSXFS.Cheque FirmwareStatus :" & cqs.Status)

                    AppLogInfo("-- MICR Validation Process --")

                    'Edit - 22/Dec/2015
                    If mdsobj.Mode = MACHINEMODE_MIX Then
                        'MIX Mode
                        If udtDEPOSITORHWDCFG.blnMIXModeMICRCheck = True Then
                            strMICRTemp = cqs.Codeline
                            If IsBadChqMICRCodeline(strMICRTemp) = True Then
                                cqs.Status = MDSControl.ChequeSet.ChequeStatus.EscrowedBad
                                .ChequeStat = ChequeStatus.EscrowedBad
                                'mdsobj.RejectCheque(.ChequeID)
                                AppLogInfo("After MICR Validation:Chq Satatus - Bad.")
                            Else
                                cqs.Status = MDSControl.ChequeSet.ChequeStatus.EscrowedGood
                                .ChequeStat = ChequeStatus.EscrowedGood
                                'mdsobj.AcceptCheque(.ChequeID)
                                AppLogInfo("After MICR Validation:Chq Satatus - Good.")
                            End If
                        Else
                            'MIX Mode Not Need check the MICR
                            'AppLogInfo("clsMDSXFS.ForceAcceptCheque")
                            'mdsobj.AcceptCheque(.ChequeID)
                            cqs.Status = MDSControl.ChequeSet.ChequeStatus.EscrowedGood
                            .ChequeStat = ChequeStatus.EscrowedGood
                            AppLogInfo("After ForceAcceptChq: All Chq Satatus - Good.")
                        End If
                    Else
                        'Cheque Control
                        'MICR Validaiton
                        If udtDEPOSITORHWDCFG.blnCASHDEPOSITORForceAcceptChq = False Then
                            strMICRTemp = cqs.Codeline
                            If IsBadChqMICRCodeline(strMICRTemp) = True Then
                                cqs.Status = MDSControl.ChequeSet.ChequeStatus.EscrowedBad
                                .ChequeStat = ChequeStatus.EscrowedBad
                                'mdsobj.RejectCheque(.ChequeID)
                                AppLogInfo("After MICR Validation:Chq Satatus - Bad.")
                            Else
                                cqs.Status = MDSControl.ChequeSet.ChequeStatus.EscrowedGood
                                .ChequeStat = ChequeStatus.EscrowedGood
                                'mdsobj.AcceptCheque(.ChequeID)
                                AppLogInfo("After MICR Validation:Chq Satatus - Good.")
                            End If
                        Else
                            'AppLogInfo("clsMDSXFS.ForceAcceptCheque")
                            'mdsobj.AcceptCheque(.ChequeID)
                            cqs.Status = MDSControl.ChequeSet.ChequeStatus.EscrowedGood
                            .ChequeStat = ChequeStatus.EscrowedGood
                            AppLogInfo("After ForceAcceptChq: All Chq Satatus - Good.")
                        End If

                    End If

                    'Cheque Info
                    AppLogInfo("After MICR Validation Value:-")
                    AppLogInfo("clsMDSXFS.Cheque Deposit Number :" & i)
                    AppLogInfo("clsMDSXFS.Cheque ID :" & .ChequeID)
                    AppLogInfo("clsMDSXFS.Cheque Codeline :" & .ChequeCodeline)
                    AppLogInfo("clsMDSXFS.Cheque ImagePath :" & .ChequeImagePath)
                    AppLogInfo("clsMDSXFS.Cheque Status :" & .ChequeStat & " FirmwarecqsStatus:" & cqs.Status)

                End With
                i += 1
            Next

            ''Fore to Accepted the Bad Cheque
            ''Control
            'If udtDEPOSITORHWDCFG.blnCASHDEPOSITORForceAcceptChq = True Then
            '    ForceAcceptCheque(ChqList)
            'End If

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.ValidationFillChqList: " & ex.Message)
        End Try
    End Sub





#End Region


#Region "Set MDS Mode"

    Public Sub SetMDSCashMode()
        Try
            AppLogInfo("clsMDSXFS.SetMDSCashMode")
            'Only at CIM Mode
            With udtDEPOSITORHWDCFG
                mdsobj.ImagePath = .CASHDEPOSITORChequeImagePath            'Path to store cheque imamge
                mdsobj.TemplateFile = .CASHDEPOSITORTemplateFilePath        'Template File Name
                mdsobj.Mode = MACHINEMODE_CASH
            End With
            mdsobj.Initialise()
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.SetMDSCashMode: " & ex.Message)
        End Try
    End Sub

    Public Sub SetMDSChequeMode()
        Try
            AppLogInfo("clsMDSXFS.SetMDSChequeMode")
            'Only at IPM Mode
            With udtDEPOSITORHWDCFG
                mdsobj.ImagePath = .CASHDEPOSITORChequeImagePath            'Path to store cheque imamge
                mdsobj.TemplateFile = .CASHDEPOSITORTemplateFilePath        'Template File Name
                mdsobj.Mode = MACHINEMODE_CHEQUE                    'use CIM Mode
            End With
            mdsobj.Initialise()
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.SetMDSChequeMode: " & ex.Message)
        End Try
    End Sub


#End Region

#Region "Functions - Start Exchange"

    Public Sub StartExchange()
        Try
            AppLogInfo("clsMDSXFS.StartExchange")
            'Only at CIM Mode
            mdsobj.Mode = MACHINEMODE_CASH
            mdsobj.StartExchange()
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StartExchange: " & ex.Message)
        End Try
    End Sub

    Public Sub EndExchange()
        Try
            AppLogInfo("clsMDSXFS.EndExchange")
            'Only at CIM Mode
            mdsobj.Mode = MACHINEMODE_CASH
            mdsobj.EndExchange()
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StartExchange: " & ex.Message)
        End Try
    End Sub

    Public Sub EndExchangeCheques()
        Try
            AppLogInfo("clsMDSXFS.EndExchangeCheque")
            'Only at IPM Mode
            'mdsobj.Mode = MACHINEMODE_CHEQUE
            mdsobj.ResetChequesInBoxCounter()
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.EndExchangeCheques: " & ex.Message)
        End Try
    End Sub


    Public Sub NoteCountInBox(ByRef CashBoxList As CashBox, ByRef ChqCount As ChequeBox, ByRef intCount As UInt16)
        Dim mdsStsDet As MDSStatusDetailInfo
        Dim bolNoteCountAvailable As Boolean = False
        Dim ns As MDSControl.NoteSet
        Dim x As Integer

        Try

            AppLogInfo("clsMDSXFS.NoteCountInBox")

            mdsStsDet.DeviceStatus = mdsobj.MDSStatusDetails.DeviceStatus

            With mdsStsDet.CIMStatusDetail
                .LogicalBoxStatus = New List(Of RotoKiosk.Constants.BoxStatus)
                .PyhsicalBoxStatus = New List(Of RotoKiosk.Constants.BoxStatus)
                .NotesInPhysicalBox = New List(Of UInteger)

                .LogicalBoxStatus.AddRange(mdsobj.MDSStatusDetails.LogicalBoxStatus)
                .PyhsicalBoxStatus.AddRange(mdsobj.MDSStatusDetails.PhysicalBoxStatus)
                .NotesInPhysicalBox.AddRange(mdsobj.MDSStatusDetails.NotesInPhysicalBox)

                ReDim CashBoxList.BoxList(.LogicalBoxStatus.Count - 1)

                For i = 0 To .LogicalBoxStatus.Count - 1
                    'If .LogicalBoxStatus(i) = RotoKiosk.Constants.BoxStatus.WFS_CIM_STATCUOK Then

                    For Each ns In mdsobj.NotesInBox(i)
                        intCount = intCount + 1
                    Next

                    If intCount > 0 Then
                        ReDim CashBoxList.BoxList(i).DenoList(intCount)
                    End If

                    x = 0
                    For Each ns In mdsobj.NotesInBox(i)
                        CashBoxList.BoxList(i).LogicalID = i

                        CashBoxList.BoxList(i).DenoList(x).NoteCount = ns.Number
                        CashBoxList.BoxList(i).DenoList(x).NoteValue = ns.Value
                        CashBoxList.BoxList(i).DenoList(x).NoteCurrency = ns.Currency

                        If (ns.Level = MDSControl.NoteSet.NoteLevel.Good) Then
                            CashBoxList.BoxList(i).DenoList(x).bGoodNote = True
                        ElseIf (ns.Level = MDSControl.NoteSet.NoteLevel.Counterfeit) Then
                            CashBoxList.BoxList(i).DenoList(x).bGoodNote = False
                        End If
                        x = x + 1
                    Next


                    'For Each ns In mdsobj.NotesInBox(i)
                    '    notelist.NoteInfo(i).NoteLogicalBoxNum = i
                    '    notelist.NoteInfo(i).NoteCount = ns.Number
                    '    notelist.NoteInfo(i).NoteValue = ns.Value
                    '    notelist.NoteInfo(i).NoteCurrency = ns.Currency
                    '    If (ns.Level = MDSControl.NoteSet.NoteLevel.Good) Then
                    '        notelist.NoteInfo(i).bGoodNote = True
                    '    ElseIf (ns.Level = MDSControl.NoteSet.NoteLevel.Counterfeit) Then
                    '        notelist.NoteInfo(i).bGoodNote = False
                    '    End If
                    '    intCount = +1
                    'Next
                    ' End If
                Next
            End With

            ChqCount.intChqCount = mdsStsDet.IPMStatusDetail.ChequeBoxCount

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.NoteCountInBox. ErrInfo:" & ex.Message)
        End Try
    End Sub

    Public Sub FillNoteBoxCountList(ByRef notelist As TrxNoteList, ByRef intCount As Int16, ByVal mdsNotelist As IEnumerable, ByRef LogicalNumber As Int16)
        Dim ns As MDSControl.NoteSet
        Dim i As Int16 = 0

        Try

            AppLogInfo("clsMDSXFS.FillNoteBoxCountList")

            intCount = 0
            'Get the total nos of Currency Type Captured in the current transaction in MDS
            For Each ns In mdsNotelist
                intCount += 1
            Next

            ReDim notelist.NoteInfo(intCount)
            'Fill the CashNoteInfo structure with the Currency Info
            For Each ns In mdsNotelist
                notelist.NoteInfo(i).NoteLogicalBoxNum = LogicalNumber
                notelist.NoteInfo(i).NoteCount = ns.Number
                notelist.NoteInfo(i).NoteValue = ns.Value
                notelist.NoteInfo(i).NoteCurrency = ns.Currency
                If (ns.Level = MDSControl.NoteSet.NoteLevel.Good) Then
                    notelist.NoteInfo(i).bGoodNote = True
                ElseIf (ns.Level = MDSControl.NoteSet.NoteLevel.Counterfeit) Then
                    notelist.NoteInfo(i).bGoodNote = False
                End If
            Next

        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.FillNoteBoxCountList. ErrInfo:" & ex.Message)
        End Try
    End Sub

#End Region


#Region "MDS XFS Kiosk Light and Card Reader Light Control"


#Region "MDS Kiosk Light Control"

    Public Function StartMDSLightXFS(ByVal strUserChoice As String) As Boolean
        Try
            AppLogInfo("clsMDSXFS.StartMDSLightXFS UserChoice:" & strUserChoice)

            'Lightup the Kiosk lighthing  
            Select Case strUserChoice
                Case "0"
                    mdsobj.TopKioskLight(MDSControl.LightStatus.On)
                Case "1"
                    mdsobj.TopKioskLight(MDSControl.LightStatus.Off)
                Case Else
                    mdsobj.TopKioskLight(MDSControl.LightStatus.Off)
            End Select

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StartMDSLightControlXFS: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSLightXFS() As Boolean
        Try
            AppLogInfo("clsMDSXFS.StopMDSLightXFS")
            mdsobj.TopKioskLight(MDSControl.LightStatus.Off)
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StopMDSLightControlXFS: " & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "MDS Card Reader Light Control"

    Public Function StartMDSReaderLightXFS(ByVal strUserChoice As String) As Boolean
        Try
            'Prompt user to insert card
            AppLogInfo("clsMDSXFS.StartMDSReaderLightXFS UserChoice:" & strUserChoice)

            'Lightup the Kiosk lighthing  
            Select Case strUserChoice
                Case "0"
                    mdsobj.CardReaderLight(MDSControl.LightStatus.On)
                Case "1"
                    mdsobj.CardReaderLight(MDSControl.LightStatus.Off)
                Case "2"
                    mdsobj.CardReaderLight(MDSControl.LightStatus.FlashingFast)
                Case "3"
                    mdsobj.CardReaderLight(MDSControl.LightStatus.FlashingSlow)
                Case Else
                    mdsobj.CardReaderLight(MDSControl.LightStatus.Off)
            End Select

            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StartMDSReaderLightXFS: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopMDSReaderLightXFS() As Boolean
        Try
            'Prompt user to insert card
            AppLogInfo("clsMDSXFS.StopMDSReaderLightXFS")
            mdsobj.CardReaderLight(MDSControl.LightStatus.Off)
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.StopMDSReaderLightXFS: " & ex.Message)
            Return False
        End Try
    End Function


#End Region


#End Region


#Region "MDS - CLS Property: MDSStatus, MDSStatusText, MDSStatusDetails"

    'MDS Status Option - 0:Disconnect 1:NotOk 2:OK
    ReadOnly Property MDSReplyStatus As MDSControl.MDSStatusOptions
        Get
            Return mdsobj.MDSStatus
        End Get
    End Property

    'Reason for MDS
    ReadOnly Property MDSReplyStatusReason As String
        Get
            Return mdsobj.MDSStatusText
        End Get
    End Property

    ReadOnly Property MDSReplyStatusDetails As String
        Get

            Dim strMDSReplyStatusDetails As String = ""


            With mdsobj.MDSStatusDetails

                strMDSReplyStatusDetails = .DeviceStatus & "|"
                strMDSReplyStatusDetails &= .CIMDeviceStatus & "|"
                strMDSReplyStatusDetails &= .IPMDeviceStatus & "|"
                strMDSReplyStatusDetails &= .SafeDoorStatus & "|"
                strMDSReplyStatusDetails &= .AcceptorStatus & "|"
                strMDSReplyStatusDetails &= .IntermediateStackerStatus & "|"
                strMDSReplyStatusDetails &= .StackerItemsStatus & "|"
                strMDSReplyStatusDetails &= .BanknoteReaderStatus & "|"

                strMDSReplyStatusDetails &= .InputShutterStatus & "|"
                strMDSReplyStatusDetails &= .InputPositionStatus & "|"
                strMDSReplyStatusDetails &= .InputTransportStatus & "|"
                strMDSReplyStatusDetails &= .InputTransportFillingStatus & "|"

                strMDSReplyStatusDetails &= .OutputShutterStatus & "|"
                strMDSReplyStatusDetails &= .OutputPositionStatus & "|"
                strMDSReplyStatusDetails &= .OutputTransportStatus & "|"
                strMDSReplyStatusDetails &= .OutputTransportFillingStatus & "|"

                strMDSReplyStatusDetails &= .RefusedShutterStatus & "|"
                strMDSReplyStatusDetails &= .RefusedPositionStatus & "|"
                strMDSReplyStatusDetails &= .RefusedTransportStatus & "|"
                strMDSReplyStatusDetails &= .RefusedTransportFillingStatus & "|"


                strMDSReplyStatusDetails &= .MediaStatus & "|"
                strMDSReplyStatusDetails &= .TonerStatus & "|"
                strMDSReplyStatusDetails &= .InkStatus & "|"
                strMDSReplyStatusDetails &= .FrontScannerStatus & "|"
                strMDSReplyStatusDetails &= .BackScannerStatus & "|"
                strMDSReplyStatusDetails &= .MICRStatus & "|"
                strMDSReplyStatusDetails &= .MediaFeederStatus & "|"
                strMDSReplyStatusDetails &= .StackerEnabled & "|"

                strMDSReplyStatusDetails &= .CMC7Mapping & "|"
                strMDSReplyStatusDetails &= .E13BMapping & "|"

                strMDSReplyStatusDetails &= .ChequeBoxCount & "|"
                strMDSReplyStatusDetails &= .IPMRetractOperationsCount & "|"
                strMDSReplyStatusDetails &= .ChequeBoxStatus & "|"
                strMDSReplyStatusDetails &= .IPMRetractBoxStatus & "|"
                strMDSReplyStatusDetails &= .SiuPortStatus


            End With

            Return strMDSReplyStatusDetails
        End Get
    End Property

    ReadOnly Property MDSReplyUserStatus As MDSControl.UserStatusOptions
        Get
            Return mdsobj.UserStatus
        End Get
    End Property



#End Region


#Region "NotesInPhysicalBoxStatus"

    ReadOnly Property MDSNoteInAllBoxCount As String
        Get
            Dim strMDSReplyStatusDetails As String = ""

            'With mdsobj.MDSStatusDetails
            '    strMDSReplyStatusDetails = .NotesInPhysicalBox(1) & "|"
            '    strMDSReplyStatusDetails &= .NotesInPhysicalBox(2) & "|"
            '    strMDSReplyStatusDetails &= .NotesInPhysicalBox(3) & "|"
            '    strMDSReplyStatusDetails &= .NotesInPhysicalBox(4) & "|"
            '    strMDSReplyStatusDetails &= .NotesInPhysicalBox(5) & "|"
            '    strMDSReplyStatusDetails &= .NotesInPhysicalBox(6) & "|"
            '    strMDSReplyStatusDetails &= .NotesInPhysicalBox(7)
            'End With

            Dim enReply As IEnumerable
            Dim ns As MDSControl.NoteSet
            Dim shtLogicalBox1 As Short = 1
            Dim shtLogicalBox2 As Short = 2
            Dim shtLogicalBox3 As Short = 3
            Dim shtLogicalBox4 As Short = 4
            Dim shtLogicalBox5 As Short = 5
            Dim shtLogicalBox6 As Short = 6
            Dim shtLogicalBox7 As Short = 7

            Dim strBox1Item As String = "0"
            Dim strBox2Item As String = "0"
            Dim strBox3Item As String = "0"
            Dim strBox4Item As String = "0"
            Dim strBox5Item As String = "0"
            Dim strBox6Item As String = "0"
            Dim strBox7Item As String = "0"

            enReply = mdsobj.NotesInBox(shtLogicalBox1)
            For Each ns In enReply
                strBox1Item = ns.Number
            Next

            enReply = mdsobj.NotesInBox(shtLogicalBox2)
            For Each ns In enReply
                strBox2Item = ns.Number
            Next

            enReply = mdsobj.NotesInBox(shtLogicalBox3)
            For Each ns In enReply
                strBox3Item = ns.Number
            Next

            enReply = mdsobj.NotesInBox(shtLogicalBox4)
            For Each ns In enReply
                strBox4Item = ns.Number
            Next

            enReply = mdsobj.NotesInBox(shtLogicalBox5)
            For Each ns In enReply
                strBox5Item = ns.Number
            Next

            enReply = mdsobj.NotesInBox(shtLogicalBox6)
            For Each ns In enReply
                strBox6Item = ns.Number
            Next

            enReply = mdsobj.NotesInBox(shtLogicalBox7)
            For Each ns In enReply
                strBox7Item = ns.Number
            Next

            strMDSReplyStatusDetails = strBox1Item & "|" & strBox2Item & "|" & strBox3Item & "|" & strBox4Item & "|" & strBox5Item & "|" & strBox6Item & "|" & strBox7Item
            Return strMDSReplyStatusDetails
        End Get
    End Property


#End Region

#Region "Retract Note"

    ReadOnly Property MDSRetractBoxNoteCount As String
        Get
            'Return mdsobj.MDSStatusDetails.NotesInPhysicalBox(2)

            Dim enReply As IEnumerable
            Dim ns As MDSControl.NoteSet
            Dim shtLogicalBox2 As Short = 2
            Dim strRetractBoxItem As String = "0"

            enReply = mdsobj.NotesInBox(shtLogicalBox2)
            For Each ns In enReply
                strRetractBoxItem = ns.Number
            Next

            Return strRetractBoxItem
        End Get
    End Property


#End Region

#Region "Counterfeit Note"

    ReadOnly Property MDSConterfeitBoxNoteCount As String
        Get

            'Return mdsobj.MDSStatusDetails.NotesInPhysicalBox(1)

            'Method 2: NotesInBox
            Dim enReply As IEnumerable
            Dim ns As MDSControl.NoteSet
            Dim shtLogicalBox1 As Short = 1
            Dim strCounterfeitBoxItem As String = "0"

            enReply = mdsobj.NotesInBox(shtLogicalBox1)
            For Each ns In enReply
                strCounterfeitBoxItem = ns.Number
            Next

            Return strCounterfeitBoxItem
        End Get
    End Property


#End Region

#Region "Function"

    Public Function MDSReplyNoteInBox(ByVal strLogicalBox As String) As String
        Dim strReplyValue As String = ""
        Dim enReply As IEnumerable
        Dim ns As MDSControl.NoteSet
        Dim shtLogicalBox As Short

        Try

            shtLogicalBox = CShort(strLogicalBox.Trim)

            enReply = mdsobj.NotesInBox(shtLogicalBox)

            'Fill the CashNoteInfo structure with the Currency Info
            For Each ns In enReply
                strReplyValue = ns.Currency & "|" & ns.Number & "|" & ns.Value & "|" & ns.Level
            Next

            Return strReplyValue
        Catch ex As Exception
            AppLogErr("Error in clsMDSXFS.MDSReplyNoteInBox: " & ex.Message)
            Return ""
        End Try
    End Function

    Public Function MDSResetCleanPaperPath() As Boolean
        Try

            'Check if object created
            If Not IsNothing(mdsobj) Then
                mdsobj.Initialise()
                Return True
            Else
                AppLogErr("Error in clsMDSXFS.InitCASHDEPOSITHWD: Object Not Initialized") 'Cannot proceed to initialize because object not created
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MDSClose() As Boolean
        Try

            If Not IsNothing(mdsobj) Then
                mdsobj.Stop()
                Return True
            Else
                AppLogErr("Error in clsMDSXFS. MDSClose: Object Not Initialized") 'Cannot proceed to initialize because object not created
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region


#Region "Update MDS Setting"

    Public Function UpdateMDSHWDLayerSetting(ByVal strEnableOpt As String, ByVal strForeChqOpt As String, ByVal strComportNo As String, ByVal strInsertItemTMOut As String, ByVal strRepositionTMOut As String, ByVal strCleanFeederTMOut As String, ByVal strTakeReturnTMOut As String, ByVal strTraceLogPath As String, ByVal strChqImagePath As String, ByVal strChqTemplatePath As String) As Boolean
        Try
            If UpdateMDSSetting(strEnableOpt, strForeChqOpt, strComportNo, strInsertItemTMOut, strRepositionTMOut, strCleanFeederTMOut, strTakeReturnTMOut, strTraceLogPath, strChqImagePath, strChqTemplatePath) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in UpdateMDSHWDLayerSetting:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


End Class

