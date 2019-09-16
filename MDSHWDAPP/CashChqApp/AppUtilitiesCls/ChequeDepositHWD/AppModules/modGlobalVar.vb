Imports clsHWDGlobalInterface.clsGolbalVar
Imports CashDepositorHWD.clsCashDepositorHWDInterface

Module modGlobalVar

#Region "App Layer Default Ini File"
    'Hookup Ini File
    Public Const MDSHWDINIPATH As String = "AppIniFile\CHQHWDCFG.ini"
#End Region

#Region "Object"

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl


#End Region

#Region "Variable"


    'Application Variable
    'Dim strTitle As String = "modGolbalAppCFG"

    Public strErrMsg As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strLogMsg As String = String.Empty
    Public strSelMsgErr As String = String.Empty
    Public strLogIniPath As String = String.Empty

  
    Public intCurrentState As Integer

    Public udtChequeList As MDSModuleControl.clsMDSModuleControl.TrxChqList

    
    Public bolTransStart As Boolean
    Public bolCancelTrans As Boolean

#End Region



#Region "Enums State"

    Public Enum CurrentState
        Close = 0
        OpenFeeder = 1
        InsertMore = 2
        RejectCash = 3
        RefundCash = 4
        Escrowed = 5
        ProcessingCash = 6
        FinishDeposit = 7
        TransComplete = 8
        Supervisor = 9
        UserTimeoutST = 10

        'Item Full
        DepositItemFull = 11

        'Edit CR002-IA=24/06/2016
        RepositionDoc = 12
        CleanFeederDoc = 13

    End Enum

    Enum CurrChequeStatus
        ReturnedToUser
        EscrowedGood
        EscrowedBad
        Stored
        Unknown
    End Enum

    Public Enum LastMDSActivity
        insertItem = 0
        cleanFeeder = 1
        RepositionDocuments = 2
        Processing = 3
        TakeReturnedItem = 4
        NotifyCounterfit = 5
        WantToInsertMore = 6
        CompleteDeposit = 7
        CompletedTransaction = 8
        TransactionCompleteFail = 9

        'Item Full
        DepositItemFull = 10

    End Enum

#End Region

#Region "Property Value"

    'Device States
    Public DVST_ERRIN_DEVSTATE As String

    'Error Severity
    Public ERST_NOERR As String
    Public ERST_CDERR As String
    Public ERST_CDFATAL As String
    Public ERST_CDWARN As String

    'Diagnostic Status -DGST
    Public DGST_STATUS As String

    'Supplies Status - SYST
    Public SYST_STATUS_NOSTATE As String

#End Region


End Module
