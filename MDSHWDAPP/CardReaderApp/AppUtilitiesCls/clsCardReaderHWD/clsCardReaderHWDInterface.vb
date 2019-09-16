Imports System
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class clsCardReaderHWDInterface

#Region "Hardware Object - Variable"
    'CardReader Hardware object
    Public WithEvents objCardReaderHWD As New clsCardReader.clsCardReaderControl
#End Region


#Region "CARDREADER Timer Control"

    Public WithEvents CardReaderTmr As New Timers.Timer
    Public WithEvents CardReaderEjectTmr As New Timers.Timer

#End Region

#Region "Events Structure - EvtDeviceDataReady, EvtDeviceTimeout, EvtDeviceError"

    Private udtDeviceSender As DeviceSender
    Private udtEventDeviceArgs As EventDeviceArgs
    Private udtDeviceError As device_error

    Public Event EvtDeviceDataReady(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)

#End Region


#Region ""

    Public Sub New()
        Try

            'Init all logger and  Card Reader Object
            If InitCardReaderHWD() = True Then
                If objCardReaderHWD.InitCARDREADERControl = True Then
                    'Load the Card Reader Default Value
                    LoadCardReaderDeviceCode()
                    If objCardReaderHWD.ReadCARDREADERHWDCFG = True Then
                        AppLogInfo("StartDevice - Load CardReader CFG Successfully")
                    Else
                        AppLogWarn("StartDevice - Load CardReader CFG Failed")
                    End If
                Else
                    AppLogWarn("StartDevice - Init CardReader Control Failed")
                End If

                CardReaderDiagStatus()
            Else
                AppLogWarn("StartDevice- Init CardReader Logger Failed")
            End If


        Catch ex As Exception
        End Try
    End Sub

#End Region


#Region "Instance Control - Single Instance"

    Private m_singleInstance As clsCardReaderHWDInterface = Nothing

    Public Function SingleInstance() As clsCardReaderHWDInterface
        If m_singleInstance Is Nothing Then
            m_singleInstance = New clsCardReaderHWDInterface()
        End If
        Return m_singleInstance
    End Function

    Protected Overrides Sub finalize()
        If m_singleInstance IsNot Nothing Then
            m_singleInstance = Nothing
        End If
    End Sub

#End Region

#Region "Init - CardReader Class - InitCardReaderHWDLoggger"

    Public Function InitCardReaderHWD() As Boolean
        Dim strLogIniPath As String = String.Empty

        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(CARDREADERHOOKUPINIPATH, CARDREADER) = True Then

                'Read INI File
                'Log Ini File
                '1.Cardreader Hardware
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "Card Reder Hookup Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger INI Path:" & strLogIniPath)

                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            AppLogErr("Error in clsCRHWDInterface.InitCardReaderHWD:" & ex.Message)
            Return False
        End Try
    End Function


#End Region


#Region "Property - TxnStatus,ErrorSeverity,SupplyStatus,MStatus,MStatusData,TimeoutInterval"

    Property TxnStatus() As String
        Get
            Return udtCardReader.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtCardReader.strTxnStatus = value
        End Set
    End Property

    Property ErrorSeverity() As String
        Get
            Return udtCardReader.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtCardReader.strErrSeverity = value
        End Set
    End Property

    Property SupplyStatus() As String
        Get
            Return udtCardReader.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtCardReader.strSupplyStatus = value
        End Set
    End Property

    Property MStatus() As String
        Get
            Return udtCardReader.strMStatus
        End Get
        Set(ByVal value As String)
            udtCardReader.strMStatus = value
        End Set
    End Property

    Property MStatusData() As String
        Get
            udtCardReader.strMStatusData = DGST_STATUS
            Return udtCardReader.strMStatusData
        End Get
        Set(ByVal value As String)
            udtCardReader.strMStatusData = value
        End Set
    End Property

    Property TimeoutInterval(ByVal ConfigID As Long) As Long
        Get
            Select Case ConfigID
                Case 1
                    Return objCardReaderHWD.CardInsertCheckTimeout
                Case 2
                    Return objCardReaderHWD.CardAtGateTimeout
                Case 3
                    Return objCardReaderHWD.CardCommandTimeout
                Case Else
                    Return 0
            End Select
        End Get
        Set(ByVal value As Long)
            Select Case ConfigID
                Case 1
                    objCardReaderHWD.CardInsertCheckTimeout = value
                Case 2
                    objCardReaderHWD.CardAtGateTimeout = value
                Case 3
                    objCardReaderHWD.CardCommandTimeout = value
            End Select
        End Set
    End Property


#End Region


#Region "Property - Other: Card Data"

    Property CardData() As String
        Get
            Return udtCardReader.strCardData
        End Get
        Set(ByVal value As String)
            udtCardReader.strCardData = value
        End Set
    End Property

#End Region


#Region "Methods General - StartDevice,StopDevice"

    Public Function StartDevice() As Boolean
        Dim blnRtrn As Boolean = False

        Try

            If objCardReaderHWD.InitCARDREADERHWD = True Then
                'Wrap Card Reader
                If WrapDevice() = True Then
                    If objCardReaderHWD.RequestStatusCardReader Then
                        CardCaptureCount = 0
                        AppLogInfo("StartDevice Success")
                        blnRtrn = True
                    Else
                        AppLogWarn("StartDevice Failed")
                        blnRtrn = False
                    End If
                Else
                    AppLogWarn("StartDevice WrapDevice Failed")
                    blnRtrn = False
                End If
            Else
                AppLogWarn("StartDevice - Init CardReader Failed")
                blnRtrn = False
            End If

            Return blnRtrn

        Catch ex As Exception
            AppLogErr("Error in StartDevice():" & ex.Message)
            Return False
        End Try
    End Function

    Public Sub LoadCardReaderDeviceCode()
        Try
            With objCardReaderHWD.CARDREADERDEVICECODE
                CardReaderDeviceGraphicId = .CARDRDR_DEVGRAPHICID
                CardReaderDeviceName = .CARDRDR_DEVGRAPHICNAME

                DST_NOCARD = .CARDRDR_DSTNOCARD
                DST_CARDINREADER = .CARDRDR_DSTCARDINREADER
                DST_CARDSTAGE = .CARDRDR_DSTCARDSTAGE
                DST_CARDCAPTURE = .CARDRDR_DSTCARDCAPTURE
                DST_CARDEJECTFAIL = .CARDRDR_DSTCARDEJECTFAIL
                DST_CARDEJECT = .CARDRDR_DSTCARDEJECT
                DST_CARDBLOCK = .CARDRDR_DSTCARDBLOCK

                EVTTYPE_DATAARRIVED = .CARDRDR_EVTTYPE_DATAARRIVED
                EVTTYPE_TIMEOUT = .CARDRDR_EVTTYPE_TIMEOUT
                EVTTYPE_WRAPDEVICE = .CARDRDR_EVTTYPE_WRAPDEVICE

                DVST_ERRIN_READSTATE = .CARDRDR_DVST_ERRIN_READSTATE
                DVST_ERRIN_LOCKSTATE = .CARDRDR_DVST_ERRIN_LOCKSTATE

                ERST_NOERR = .CARDRDR_ERST_NOERR
                ERST_READERERR = .CARDRDR_ERST_READERERR
                ERST_READERFATAL = .CARDRDR_ERST_READERFATAL
                ERST_READERWARN = .CARDRDR_ERST_READERWARN

                DGST_STATUS = .CARDRDR_DGST_STATUS

                SYST_STATUS_NONEWSTATE = .CARDRDR_SYST_STATUSNONEWSTATE
                SYST_STATUS_NOOVERFILL = .CARDRDR_SYST_STATUSNOOVERFILL
                SYST_STATUS_OVERFILL = SYST_STATUS_OVERFILL

            End With
        Catch ex As Exception
            AppLogErr("Error in LoadCardReaderDeviceCode:" & ex.Message)
        End Try
    End Sub

    Public Function StopDevice() As Boolean
        Dim blnReply As Boolean = False
        Try

            If objCardReaderHWD.RequestStatusCardReader Then
                If objCardReaderHWD.udtCardReaderStatus.StatusCode1 = CARDINSIDE Then
                    AppLogInfo("Card Detected Inside. Capture Called")
                    objCardReaderHWD.CardReaderCaptureCard()
                ElseIf objCardReaderHWD.udtCardReaderStatus.StatusCode1 = CARDATGATE Then
                    AppLogInfo("Card Detected At Gate. Capture Called")
                    objCardReaderHWD.CardReaderCaptureCard()
                End If
            End If

            If objCardReaderHWD.CloseCARDREADERHWD = True Then
                AppLogInfo("StopDevice Success")
                blnReply = True
            Else
                AppLogWarn("StopDevice Failed")
                blnReply = False
            End If

            'Close Logger
            CloseLog()

            Return blnReply

        Catch ex As Exception
            AppLogErr("Error in StopDevice:" & ex.Message)
            CloseLog()
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Try
            If objCardReaderHWD.EnableCardReader = True Then
                AppLogInfo("UnlockDevice Success")
                Return True
            Else
                AppLogInfo("UnlockDevice Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in UnlockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Try
            If objCardReaderHWD.DisableCardReader = True Then
                AppLogInfo("LockDevice Success")
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

    Public Function WakeUpDevice(ByVal intState As Integer) As Boolean
        Dim blnRtn As Boolean
        Dim intTmpState As Integer

        Try

            AppLogInfo("WakeupDevice - Card Reader State:" & intState)

            If intState >= 128 Then
                intTmpState = intState - 128
                intState = intTmpState
            End If

            Select Case intState
                'Case 0       'No Card State

                '    If objCardReaderHWD.EnableCardReader = True Then
                '        AppLogInfo("Enable Device Success")
                '        blnRtn = True
                '    Else
                '        AppLogInfo("Enable Device Failed")
                '        blnRtn = False
                '    End If

                Case CurrentState.CardInReader, CurrentState.CardStage      'No Card State

                    If objCardReaderHWD.EnableCardReader = True Then
                        AppLogInfo("WakeupDevice - Enable Card Reader Success")
                        CardReaderTimerStart()
                        blnRtn = True
                    Else
                        AppLogInfo("WakeupDevice - Enable Card Reader Failed")
                        blnRtn = False
                    End If

                Case CurrentState.Captured       'Card Capture

                    If objCardReaderHWD.CardReaderCaptureCard = True Then
                        AppLogInfo("WakeupDevice - Capture Card Success")
                        blnRtn = True
                    Else
                        AppLogInfo("WakeupDevice - Capture Card Failed")
                        blnRtn = False
                    End If

                Case CurrentState.Ejected       'Card Eject

                    If objCardReaderHWD.CardReaderEjectCard = True Then
                        AppLogInfo("WakeupDevice - Eject Card Success")
                    Else
                        'CardReaderEjectError()
                        AppLogInfo("WakeupDevice - Eject Card Failed")
                    End If

                    If objCardReaderHWD.DisableCardReader = True Then
                        AppLogInfo("WakeupDevice - Disable After Eject Card Success")
                        blnRtn = True
                    Else
                        AppLogInfo("WakeupDevice - Disable After Eject Card Failed")
                        blnRtn = False
                    End If

                    'Eject Timer
                    CardCaptureCount = 0
                    CardReaderEjectTimerStart()

                Case Else
                    blnRtn = False
            End Select

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in WakeupDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        Try

            If objCardReaderHWD.RequestStatusCardReader Then
                AppLogInfo("DiagnosticDevice - Request Status Success")
                CardReaderDiagStatus()
                Return True
            Else
                AppLogInfo("DiagnosticDevice - Request Status Failed")
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in DiagnosticDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty

        Try
            'Stop the HWD Timer
            CardReaderTimerStop()

            'Lock Device For Clean State
            If objCardReaderHWD.RequestStatusCardReader Then
                If objCardReaderHWD.udtCardReaderStatus.StatusCode1 = CARDINSIDE Then
                    AppLogInfo("Card Detected Inside. Capture Called")
                    objCardReaderHWD.CardReaderCaptureCard()
                ElseIf objCardReaderHWD.udtCardReaderStatus.StatusCode1 = CARDATGATE Then
                    AppLogInfo("Card Detected At Gate. Capture Called")
                    objCardReaderHWD.CardReaderCaptureCard()
                End If
            End If

            If objCardReaderHWD.DisableCardReader Then
                AppLogInfo("WrapDevice - Disable Card Reader")
            End If

            'General Property - 'clean the data

            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = SYST_STATUS_NONEWSTATE  'always set to 1

            With udtCardReader
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DST_NOCARD
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_WRAPDEVICE
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

#End Region

#Region "Card Info Methods - TrackI,TrackII,TrackIII,PMPCCardSerialNum"

    Public Function ReadCard(ByVal intCardType As Integer) As Integer
        Dim intReturnType As Integer

        Try

            AppLogInfo("ReadCard Method Called")
            'CardReaderTimerStart()

            Select Case intCardType
                Case 0
                    If objCardReaderHWD.ReadCardTrack = True Then
                        AppLogInfo("Read Card Track Success")
                        'CardReaderGetTrack()
                        intReturnType = intCardType
                    Else
                        If objCardReaderHWD.RequestStatusCardReader Then
                            AppLogInfo("Read Card Track Fail")
                            'CardReaderGetTrackTimeout()
                        Else
                            AppLogInfo("Read Card Track Fail Reader Error")
                            CardReaderGetTrackReaderError()
                        End If

                        intReturnType = -1
                    End If

                Case 1
                    If objCardReaderHWD.ReadCardTrackPMPC = True Then
                        AppLogInfo("Read Card Track PMPC Success")
                        CardReaderGetTrack()
                        intReturnType = intCardType
                    Else
                        If objCardReaderHWD.RequestStatusCardReader Then
                            AppLogInfo("Read Card Track Fail")
                            'CardReaderGetTrackTimeout()
                        Else
                            AppLogInfo("Read Card Track Fail Reader Error")
                            CardReaderGetTrackReaderError()
                        End If
                        intReturnType = -1
                    End If

                Case Else
                    If objCardReaderHWD.ReadCardTrack = True Then
                        AppLogInfo("Read Card Track Success")
                        CardReaderGetTrack()

                        intReturnType = intCardType
                    Else
                        If objCardReaderHWD.RequestStatusCardReader Then
                            AppLogInfo("Read Card Track Fail")
                            'CardReaderGetTrackTimeout()
                        Else
                            AppLogInfo("Read Card Track Fail Reader Error")
                            CardReaderGetTrackReaderError()
                        End If

                        intReturnType = -1
                    End If
            End Select

            Return intReturnType
        Catch ex As Exception
            AppLogErr("Error in ReadCard:" & ex.Message)
            Return -1
        End Try
    End Function

    Public Function TrackI() As String
        Try
            Return objCardReaderHWD.udtCardInfo.strTrack1
        Catch ex As Exception
            AppLogErr("Error In TrackI:" & ex.Message)
            Return String.Empty
        End Try
    End Function

    Public Function TrackII() As String
        Try
            Return objCardReaderHWD.udtCardInfo.strTrack2
        Catch ex As Exception
            AppLogErr("Error In TrackII:" & ex.Message)
            Return String.Empty
        End Try
    End Function

    Public Function TrackIII() As String
        Try
            Return objCardReaderHWD.udtCardInfo.strTrack3
        Catch ex As Exception
            AppLogErr("Error In TrackIII:" & ex.Message)
            Return String.Empty
        End Try
    End Function

    Public Function CardSerialNum() As String
        Try
            Return objCardReaderHWD.PMPCCardSerialNumber
        Catch ex As Exception
            AppLogErr("Error in CardSerialNum:" & ex.Message)
            Return String.Empty
        End Try
    End Function

#End Region

#Region "Hardware Layer Event"

    Private Function CardReaderDiagStatus() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty
        Try

            'Value to Set for NDC
            strProTxnStatus = objCardReaderHWD.CARDREADERERRORCODE.CARDRDRTRXSTATNOERR

            'Value set for Error Severity

            If objCardReaderHWD.udtCardReaderStatus.StatusType = POSITIVEREPLY Then
                If objCardReaderHWD.udtCardReaderStatus.StatusCode1 = NOCARD Then
                    strProErrSeverity = ERST_NOERR
                Else
                    strProErrSeverity = ERST_READERWARN
                End If
            Else
                strProErrSeverity = ERST_READERFATAL
            End If

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE


            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            'Event Device Args
            With udtEventDeviceArgs
                If objCardReaderHWD.udtCardReaderStatus.StatusCode1 = CARDINSIDE Then
                    .iDeviceState = DST_CARDINREADER
                ElseIf objCardReaderHWD.udtCardReaderStatus.StatusCode1 = CARDATGATE Then
                    .iDeviceState = DST_CARDSTAGE
                Else
                    .iDeviceState = DST_NOCARD
                End If
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = Nothing
                .iDeviceTrace = strGeniDeviceTrace
            End With

            'Log Info
            AppLogInfo("CardReaderDiagStatus Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            Return True
        Catch ex As Exception
            AppLogErr("Error in CardReaderDiagStatus:" & ex.Message)
            Return False
        End Try

    End Function

    Private Function CardReaderGetTrack() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strTrackInfo As String = String.Empty
        Try

            'Value to Set for NDC
            strProTxnStatus = objCardReaderHWD.CARDREADERERRORCODE.CARDRDRTRXSTATNOERR

            'Value set for Error Severity
            strProErrSeverity = ERST_NOERR

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE

            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_CARDINREADER
                .strEventType = EVTTYPE_DATAARRIVED

                With objCardReaderHWD.udtCardInfo
                    strTrackInfo = .strTrack1 & .strTrack2 & .strTrack3
                End With

                .mDeviceDataValue = strTrackInfo
                .iDeviceTrace = strGeniDeviceTrace
            End With

            AppLogInfo("CardReaderGetTrack Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            Return True
        Catch ex As Exception
            AppLogErr("Error in CardReaderGetTrack:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CardInsertedInReader() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strTrackInfo As String = String.Empty
        Try

            'Value to Set for NDC
            strProTxnStatus = DST_CARDINREADER

            'Value set for Error Severity
            strProErrSeverity = ERST_NOERR

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE

            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_CARDINREADER
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
                .iDeviceTrace = strGeniDeviceTrace
            End With

            AppLogInfo("CardInsertedInReader Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

            Return True
        Catch ex As Exception
            AppLogErr("Error in CardInsertedInReader:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CardReaderCardContained() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strTrackInfo As String = String.Empty
        Try

            'Value to Set for NDC
            strProTxnStatus = DST_CARDCAPTURE

            'Value set for Error Severity
            strProErrSeverity = ERST_NOERR

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE

            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_CARDCAPTURE
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
                .iDeviceTrace = strGeniDeviceTrace
            End With

            AppLogInfo("CardReaderCardContained Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

            Return True
        Catch ex As Exception
            AppLogErr("Error in CardReaderCardContained:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CardReaderCardContainedError() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strTrackInfo As String = String.Empty
        Try

            'Value to Set for NDC
            strProTxnStatus = DST_CARDBLOCK

            'Value set for Error Severity
            strProErrSeverity = ERST_NOERR

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE

            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_CARDBLOCK
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
                .iDeviceTrace = strGeniDeviceTrace
            End With

            AppLogInfo("CardReaderCardContainedError Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

            Return True
        Catch ex As Exception
            AppLogErr("Error in CardReaderCardContainedError:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CardTakenOutFromReader() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strTrackInfo As String = String.Empty
        Try

            'Value to Set for NDC
            strProTxnStatus = DST_NOCARD

            'Value set for Error Severity
            strProErrSeverity = ERST_NOERR

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE

            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_NOCARD
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = ""
                .iDeviceTrace = strGeniDeviceTrace
            End With

            AppLogInfo("CardTakenOutFromReader Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)

            Return True
        Catch ex As Exception
            AppLogErr("Error in CardTakenOutFromReader:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CardReaderGetTrackTimeout()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strTrackInfo As String = String.Empty
        Try
            'Value to Set for NDC
            strProTxnStatus = objCardReaderHWD.CARDREADERERRORCODE.CARDRDRTRXSTATINVALIDTRACK

            'Value set for Error Severity
            strProErrSeverity = ERST_READERERR

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE


            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_NOCARD
                .strEventType = EVTTYPE_TIMEOUT
                .mDeviceDataValue = Nothing
                .iDeviceTrace = strGeniDeviceTrace
            End With

            AppLogInfo("CardReaderGetTrackTimeout Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceTimeout(udtDeviceSender, udtEventDeviceArgs)

            Return True
        Catch ex As Exception
            AppLogErr("Error in CardReaderGetTrackTimeout:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CardReaderGetTrackReaderError()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strTrackInfo As String = String.Empty
        Try
            'Value to Set for NDC
            strProTxnStatus = objCardReaderHWD.CARDREADERERRORCODE.CARDRDRTRXSTATERRTRCKDT

            'Value set for Error Severity
            strProErrSeverity = ERST_READERERR

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE


            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtDeviceError
                .Status = strProTxnStatus
                .ErrorSeverity = strProErrSeverity
                .SupplyStatus = strProSupplyStatus
                .MStatus = "0"
                .MStatus = DGST_STATUS
            End With

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_NOCARD
                .strEventType = EVTTYPE_TIMEOUT
                .mDeviceDataValue = udtDeviceError
                .iDeviceTrace = strGeniDeviceTrace
            End With

            AppLogInfo("CardReaderGetTrackReaderError Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
            Return True
        Catch ex As Exception
            AppLogErr("Error in CardReaderGetTrackReaderError:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CardReaderEjectError()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim strTrackInfo As String = String.Empty
        Try
            'Value to Set for NDC
            strProTxnStatus = DST_CARDEJECTFAIL

            'Value set for Error Severity
            strProErrSeverity = ERST_READERERR

            'Value set for Diagnostic Status - Always 0000000000000000
            strProDignosticStatus = DGST_STATUS

            'Value set for Supply Status
            '0 - No New State
            '1 - Not overfill
            '4 - Overfill capture bin 
            strProSupplyStatus = SYST_STATUS_NONEWSTATE


            'Set the Property Value
            With udtCardReader
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = strProDignosticStatus
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = CardReaderDeviceGraphicId
                .strDeviceName = CardReaderDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = CardReaderDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtDeviceError
                .Status = strProTxnStatus
                .ErrorSeverity = strProErrSeverity
                .SupplyStatus = strProSupplyStatus
                .MStatus = "0"
                .MStatus = DGST_STATUS
            End With

            'Event Device Args
            With udtEventDeviceArgs
                .iDeviceState = DST_CARDEJECTFAIL
                .strEventType = EVTTYPE_TIMEOUT
                .mDeviceDataValue = udtDeviceError
                .iDeviceTrace = strGeniDeviceTrace
            End With

            AppLogInfo("CardReaderEjectError Success - " & udtEventDeviceArgs.iDeviceState & "," & udtEventDeviceArgs.iDeviceTrace & "," & udtEventDeviceArgs.mDeviceDataValue)
            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
            Return True
        Catch ex As Exception
            AppLogErr("Error in CardReaderEjectError:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Timer Control - Waiting Card"

    Private Sub CardReaderTimerStop()
        Try
            CardReaderTmr.Stop()
        Catch ex As Exception
            AppLogErr("Error in CardReaderTimerStop:" & ex.Message)
        End Try
    End Sub

    Private Sub CardReaderTimerStart()
        Try
            CardReaderTmr.Interval = (objCardReaderHWD.CardInsertCheckTimeout * 1000)
            CardReaderTmr.Start()
        Catch ex As Exception
            AppLogErr("Error in CardReaderTimerStart:" & ex.Message)
        End Try
    End Sub

    Private Sub CardReaderTmr_Tick(sender As Object, e As System.EventArgs) Handles CardReaderTmr.Elapsed
        Try
            CardReaderTmr.Stop()

            ''If objCardReaderHWD.ReadCardTrack = True Then
            ''    AppLogInfo("Read Card Track Success")
            ''    CardReaderGetTrack()
            ''Else
            ''    If objCardReaderHWD.RequestStatusCardReader Then
            ''        AppLogInfo("Read Card Track Fail Timeout")
            ''        CardReaderGetTrackTimeout()
            ''    Else
            ''        AppLogInfo("Read Card Track Fail Reader Error")
            ''        CardReaderGetTrackReaderError()
            ''    End If
            ''End If

            If objCardReaderHWD.CheckCardReader Then
                If objCardReaderHWD.udtCardReaderStatus.StatusCode1 = CARDINSIDE Then
                    CardInsertedInReader()
                Else
                    CardReaderTmr.Start()
                End If
            Else
                CardReaderTmr.Start()
            End If

        Catch ex As Exception
            AppLogErr("Error in CardReaderTmr_Tick:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Timer Control - After Eject Card"

    Private Sub CardReaderEjectTimerStop()
        Try
            CardReaderEjectTmr.Stop()
        Catch ex As Exception
            AppLogErr("Error in CardReaderEjectTimerStop:" & ex.Message)
        End Try
    End Sub

    Private Sub CardReaderEjectTimerStart()
        Try
            'AppLogInfo("Card Reader Start Interval" & objCardReaderHWD.CardInsertCheckTimeout)
            'CardReaderEjectTmr.Interval = (objCardReaderHWD.CardAtGateTimeout * 1000)
            CardReaderEjectTmr.Interval = (objCardReaderHWD.CardInsertCheckTimeout * 1000)
            CardReaderEjectTmr.Start()
        Catch ex As Exception
            AppLogErr("Error in CardReaderEjectTimerStart:" & ex.Message)
        End Try
    End Sub

    Private Sub CardReaderEjectTmr_Tick(sender As Object, e As System.EventArgs) Handles CardReaderEjectTmr.Elapsed
        Dim intTimeoutCount As Integer

        Try

            'CardReaderEjectTmr.Stop()

            intTimeoutCount = objCardReaderHWD.CardAtGateTimeout

            If objCardReaderHWD.RequestStatusCardReader = True Then

                AppLogInfo("EjectTimer Card Reader Status :" & objCardReaderHWD.udtCardReaderStatus.StatusCode1)

                If objCardReaderHWD.udtCardReaderStatus.StatusCode1 = NOCARD Then

                    CardReaderEjectTmr.Stop()
                    CardTakenOutFromReader()

                    'ElseIf objCardReaderHWD.udtCardReaderStatus.StatusCode1 = CARDATGATE Then

                Else

                    'AppLogInfo("At Gate CardCaptureCount:" & CardCaptureCount)
                    'AppLogInfo("At Gate Timeout Count:" & intTimeoutCount)

                    If CardCaptureCount >= intTimeoutCount Then
                        If objCardReaderHWD.CardReaderCaptureCard = True Then
                            AppLogInfo("Capture Card Success")
                            CardReaderCardContained()
                        Else
                            AppLogInfo("Capture Card Failed")
                            CardReaderCardContainedError()
                        End If
                    End If

                End If

            Else
                AppLogInfo("EjectTimer Read Card Failed")
                CardReaderCardContainedError()
            End If

            CardCaptureCount = CardCaptureCount + 1

        Catch ex As Exception
            AppLogErr("Error in CardReaderEjectTmr_Tick:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Update Card Reader Setting"

    Public Function UpdateCardReaderSetting(ByVal strCREnableOpt As String, ByVal strCRComport As String, ByVal strCRCmdTMOut As String, ByVal strCRChkTMOut As String, ByVal strCREjtTMOut As String, ByVal strPSAMIDPath As String) As Boolean
        Try
            If objCardReaderHWD.UpdateHWDCRSetting(strCREnableOpt, strCRComport, strCRCmdTMOut, strCRChkTMOut, strCREjtTMOut, strPSAMIDPath) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in UpdateCardReaderSetting:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function ReadCardReaderSetting() As Boolean
        Try
            If objCardReaderHWD.ReadCARDREADERHWDCFG = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in ReadCardReaderSetting:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "CR Setting Property"

    ReadOnly Property EnableCardReaderOpt() As Boolean
        Get
            Return objCardReaderHWD.EnableCROpt
        End Get
    End Property

    ReadOnly Property CardReaderComportNo() As String
        Get
            Return objCardReaderHWD.CRComportNo
        End Get
    End Property

    ReadOnly Property CardReaderCommandTimeout() As String
        Get
            Return objCardReaderHWD.CRCmdTimeout
        End Get
    End Property

    ReadOnly Property CardReaderCheckTimeout() As String
        Get
            Return objCardReaderHWD.CRChkTimeout
        End Get
    End Property

    ReadOnly Property CardReaderEjectTimeout() As String
        Get
            Return objCardReaderHWD.CREjtTimeout
        End Get
    End Property

    ReadOnly Property CardReaderPSAMIDPath() As String
        Get
            Return objCardReaderHWD.CRPSAMIDPath
        End Get
    End Property


#End Region



   
End Class
