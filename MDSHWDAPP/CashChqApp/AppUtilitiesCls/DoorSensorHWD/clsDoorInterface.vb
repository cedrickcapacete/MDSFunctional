Imports MDSModuleControl
Imports clsHWDGlobalInterface.clsGolbalVar
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class clsDoorInterface


#Region "Object"

    Dim WithEvents objMDSControl As New clsMDSModuleControl

#End Region

#Region "Variables"

    Public blnDoorStarted As Boolean = False
    Dim blnInitializeFlag As Boolean = False
    Dim blnDoorSensor As Boolean = False
    Dim intCheckCount As Integer

    'Property Tmp Variable and Control
    Private strProTxnStatus As String = String.Empty
    Private strProErrSeverity As String = String.Empty
    Private strProDignosticStatus As String = String.Empty
    Private strProSupplyStatus As String = String.Empty

    'Hardware Door Const Value
    Private DoorDeviceGraphicId As String
    Private DoorDeviceName As String

    'Door Const Event Type
    Private EVTTYPE_WRAPDEVICE As String
    Private EVTTYPE_TIMEOUT As String
    Private EVTTYPE_DATAARRIVED As String
    Private EVTTYPE_ERROR As String

#End Region

#Region "Structures"

    Structure DoorSensorHWDStatus
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
        Dim strMStatusData As String
    End Structure

    Public udtDoorSensorHWDStatus As DoorSensorHWDStatus

#End Region

#Region "Events Structure - Events"

    Private udtDeviceSender As DeviceSender
    Private udtEventDeviceArgs As EventDeviceArgs
    Private udtDeviceError As device_error

    Public Event EvtDeviceDataReady(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceTimeout(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)
    Public Event EvtDeviceError(ByVal Sender As DeviceSender, ByVal e As EventDeviceArgs)

    'Public Event EvtDeviceMDSDoorOpen()
    'Public Event EvtDeviceMDSDoorClose()

#End Region

#Region "Instance Control"
    Private m_singleInstance As clsDoorInterface = Nothing

    Public Function SingleInstance() As clsDoorInterface
        If m_singleInstance Is Nothing Then
            m_singleInstance = New clsDoorInterface()
        End If
        Return m_singleInstance
    End Function

    Protected Overrides Sub finalize()
        If m_singleInstance IsNot Nothing Then
            m_singleInstance = Nothing
        End If
    End Sub
#End Region

#Region "Support Function - SetDoorHWDLayerProp"

    Public Sub InitVariable()
        Try

            intCheckCount = 0
            blnInitializeFlag = True
            blnDoorStarted = True

        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.InitVariable:" & ex.Message)
        End Try
    End Sub


    Public Sub SetDoorHWDLayerProp()
        Try
            With objMDSControl.DoorDeviceCode
                DoorDeviceGraphicId = .DOORDEVDeviceID
                DoorDeviceName = .DOORDEVDeviceName

                EVTTYPE_WRAPDEVICE = .DOORDEVEventWrap
                EVTTYPE_TIMEOUT = .DOORDEVEventTimeout
                EVTTYPE_DATAARRIVED = .DOORDEVEventDataArrive
                EVTTYPE_ERROR = .DOORDEVEventError
            End With

        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.SetDoorHWDLayerProp:" & ex.Message)
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
            AppLogErr("Error in clsDoorInterface.GenDateTimeStamp:" & ex.Message)
            Return "9999"
        End Try
    End Function

#End Region


#Region "New "

    Public Sub New()
        Try

            'Init the Door Logger
            InitDoorHWDLogger()

        Catch ex As Exception

        End Try
    End Sub

#End Region


#Region "Logger"

    Public Sub InitDoorHWDLogger()
        Try
            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(MDSHWDINIPATH, MDS) = True Then

                'Read INI File
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)


                strLogEvt = "MDS - Door Hookup Class Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                'AppLogInfo("MDS Door Interface Loaded")

            Else
                'MSD - Cheque Init Failed
            End If

        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.InitDoorHWDLogger:" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Method - StartDevice,StopDevice etc ..."

    Public Function StartDevice() As Boolean
        Try

            If objMDSControl.InitializeMDS = True Then
                'Set the Door Hardware Properties
                SetDoorHWDLayerProp()
                'Init the Variable
                InitVariable()
                AppLogInfo("MDS Door-StartDevice Success")
                Return True
            Else
                blnDoorStarted = False
                AppLogWarn("MDS Door-StartDevice Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.StartDevice:" & ex.Message)
            blnDoorStarted = False
            Return False
        End Try
    End Function

    Public Function StopDevice() As Boolean
        Try

            'objMDSControl.StopMDS()

            'Close the Logger
            CloseLog()

            Return True

            'If Not IsNothing(objMDSControl) Then
            '    If objMDSControl.StopMDS = True Then
            '        AppLogInfo("MDS Door-StopDevice Success")
            '        Return True
            '    Else
            '        AppLogWarn("MDS Door-StopDevice Failed")
            '        blnDoorStarted = False
            '        Return False
            '    End If
            'Else
            '    AppLogWarn("Error: clsDoorInterface.MDSXFSWrapper is nothing")
            '    blnDoorStarted = False
            '    Return False
            'End If

        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.StopDevice:" & ex.Message)
            blnDoorStarted = False
            Return False
        End Try
    End Function

    Public Function LockDevice() As Boolean
        Try
            If blnDoorStarted = True Then
                'If objMDSControl.TriggerOFFDoorSensor() = True Then
                '    AppLogInfo("MDS Door-LockDevice Success")
                '    Return True
                'Else
                '    AppLogWarn("MDS Door-LockDevice Failed")
                '    Return False
                'End If

                Return True
            Else
                AppLogWarn("MDS Door-LockDevice Failed. Door Control not started.")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.LockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UnlockDevice() As Boolean
        Try
            If blnDoorStarted = True Then
                'If objMDSControl.TriggerDoorSensor() = True Then
                '    AppLogInfo("MDS Door-UnlockDevice Success")
                '    Return True
                'Else
                '    AppLogWarn("MDS Door-UnlockDevice Failed")
                '    Return False
                'End If
                Return True
            Else
                AppLogWarn("MDS Door-UnlockDevice Failed. Door Control not started.")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.UnlockDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WakeUpDevice(ByVal intRecv As Integer) As Boolean
        Try
            If blnDoorStarted = True Then
                'If objMDSControl.TriggerDoorSensor() = True Then
                '    AppLogInfo("MDS Door-WakeUpDevice Success")
                '    Return True
                'Else
                '    AppLogWarn("MDS Door-WakeUpDevice Failed")
                '    Return False
                'End If
                Return True
            Else
                AppLogWarn("MDS Door-WakeUpDevice Failed. Door Control not started.")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.WakeUpDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function WrapDevice() As Boolean
        Dim strGeniDeviceTrace As String = String.Empty

        Try

            'Reset all the value

            'General Property - 'clean the data
            strProTxnStatus = ""
            strProErrSeverity = ""
            strProDignosticStatus = ""
            strProSupplyStatus = "0"  'always set to 1

            With udtDoorSensorHWDStatus
                .strTxnStatus = ""
                .strErrSeverity = ""
                .strSupplyStatus = ""
                .strMStatus = ""
                .strMStatusData = ""
            End With

            'Cardreader EvtSender
            With udtDeviceSender
                .strDeviceGraphicId = DoorDeviceGraphicId
                .strDeviceName = DoorDeviceName
            End With

            'Cardreader EvtDeviceArgs
            'Trace ID - strDeviceGraphicId & DDMMYYYYHHMMSS
            strGeniDeviceTrace = DoorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim


            'iDeviceStatus
            'Door Close - 0
            'Door Open - 1

            With udtEventDeviceArgs
                .iDeviceState = DoorStatus.DoorClosed
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_WRAPDEVICE
                .mDeviceDataValue = "0000000000000000"
            End With

            AppLogInfo("MDS Door- WrapDevice Success")
            Return True

        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.WrapDevice:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function DiagnosticDevice() As Boolean
        Try
            If udtEventDeviceArgs.iDeviceState = "1" Then
                DoorSensorDetectedOpen()
            Else
                DoorSensorDetectedClosed()
            End If
            Return True
        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.DiagnosticDevice: " & ex.Message)
            Return False
        End Try
    End Function



#End Region


#Region "Property MDSDoorStatus,TxnStatus,ErrorSeverity,SupplyStatus ..."

    Public Overloads Property TxnStatus() As String
        Get
            Return udtDoorSensorHWDStatus.strTxnStatus
        End Get
        Set(ByVal value As String)
            udtDoorSensorHWDStatus.strTxnStatus = value
        End Set
    End Property


    Public Overloads Property ErrorSeverity() As String
        Get
            Return udtDoorSensorHWDStatus.strErrSeverity
        End Get
        Set(ByVal value As String)
            udtDoorSensorHWDStatus.strErrSeverity = value
        End Set
    End Property

    Public Overloads Property SupplyStatus() As String
        Get
            Return udtDoorSensorHWDStatus.strSupplyStatus
        End Get
        Set(ByVal value As String)
            udtDoorSensorHWDStatus.strSupplyStatus = value
        End Set
    End Property

    Public Overloads Property MStatus() As String
        Get
            Return udtDoorSensorHWDStatus.strMStatus
        End Get
        Set(ByVal value As String)
            udtDoorSensorHWDStatus.strMStatus = value
        End Set
    End Property

    Public Overloads Property MStatusdata() As String
        Get
            Return udtDoorSensorHWDStatus.strMStatusData
        End Get
        Set(ByVal value As String)
            udtDoorSensorHWDStatus.strMStatusData = value
        End Set
    End Property

    Public ReadOnly Property MDSDoorStatus() As String
        Get
            Return objMDSControl.MDSDoorStatus
        End Get
    End Property

#End Region

#Region "Event - Door Interface"

    Public Sub DoorSensorDetectedOpen()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Dim dblTmpDepositAmt As Double = 0.0

        Try

            AppLogInfo("MDS - Door Open Detected")

            strProTxnStatus = DoorStatus.DoorOpened
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtDoorSensorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = DoorDeviceGraphicId
                .strDeviceName = DoorDeviceName
            End With

            strGeniDeviceTrace = DoorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                '.iDeviceState = DoorStatus.DoorOpened
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = udtDoorSensorHWDStatus
            End With

            'RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            'RaiseEvent EvtDeviceMDSDoorOpen()

        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.DoorSensorDetectedOpen:" & ex.Message)
        End Try
    End Sub

    Public Sub DoorSensorDetectedClosed()
        Dim strGeniDeviceTrace As String = String.Empty
        Dim intCountPcs As Integer = 0
        Dim intCurrencyVal As Integer = 0
        Dim strDeviceDataValue As String = String.Empty
        Dim dblTmpDepositAmt As Double = 0.0

        Try

            AppLogInfo("MDS - Door Close Detected")

            strProTxnStatus = DoorStatus.DoorClosed
            strProErrSeverity = 0
            strProSupplyStatus = 0
            strProDignosticStatus = 0

            With udtDoorSensorHWDStatus
                .strTxnStatus = strProTxnStatus
                .strErrSeverity = strProErrSeverity
                .strSupplyStatus = strProSupplyStatus
                .strMStatus = "0"
                .strMStatusData = "0000000000000000"
            End With

            With udtDeviceSender
                .strDeviceGraphicId = DoorDeviceGraphicId
                .strDeviceName = DoorDeviceName
            End With

            strGeniDeviceTrace = DoorDeviceGraphicId & GenDateTimeStamp()
            strGeniDeviceTrace = strGeniDeviceTrace.Trim

            With udtEventDeviceArgs
                .iDeviceState = DoorStatus.DoorClosed
                .iDeviceTrace = strGeniDeviceTrace
                .strEventType = EVTTYPE_DATAARRIVED
                .mDeviceDataValue = udtDoorSensorHWDStatus
            End With

            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
            'RaiseEvent EvtDeviceMDSDoorClose()

        Catch ex As Exception
            AppLogErr("Error in clsDoorInterface.DoorSensorDetectedClosed:" & ex.Message)
        End Try
    End Sub


#End Region

#Region "MDS Control - Events"

    'Private Sub objMDSControl_EvtDoorClosedReceived() Handles objMDSControl.EvtDoorClosedReceived
    '    Try
    '        'AppLogInfo("EvtDoorClosedReceived.Door Status" & objMDSControl.MDSDoorStatus)
    '        If blnDoorStarted = True Then
    '            DoorSensorDetectedClosed()
    '            RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
    '        End If
    '    Catch ex As Exception
    '        AppLogErr("Error in clsDoorInterface.objMDSControl_EvtDoorClosedReceived:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub objMDSControl_EvtDoorOpenReceived() Handles objMDSControl.EvtDoorOpenReceived
    '    Try
    '        'AppLogInfo("EvtDoorOpenReceived.Door Status" & objMDSControl.MDSDoorStatus)
    '        If blnDoorStarted = True Then
    '            DoorSensorDetectedOpen()
    '            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
    '        End If
    '    Catch ex As Exception
    '        AppLogErr("Error in clsDoorInterface.objMDSControl_EvtDoorOpenReceived:" & ex.Message)
    '    End Try
    'End Sub



    'Private Sub objMDSControl_EvtDoorClosedReceived(ByVal DoorSIUStatus As MDSModuleControl.clsMDSModuleControl.MDSDoorSIUStatus) Handles objMDSControl.EvtDoorClosedReceived
    '    Try
    '        'AppLogInfo("EvtDoorClosedReceived.Door Status" & objMDSControl.MDSDoorStatus)
    '        'If blnDoorStarted = True Then
    '        '    DoorSensorDetectedClosed()
    '        '    RaiseEvent EvtDeviceDataReady(udtDeviceSender, udtEventDeviceArgs)
    '        'End If
    '    Catch ex As Exception
    '        AppLogErr("Error in clsDoorInterface.objMDSControl_EvtDoorClosedReceived:" & ex.Message)
    '    End Try
    'End Sub

    'Private Sub objMDSControl_EvtDoorOpenReceived(ByVal DoorSIUStatus As MDSModuleControl.clsMDSModuleControl.MDSDoorSIUStatus) Handles objMDSControl.EvtDoorOpenReceived
    '    Try
    '        'AppLogInfo("EvtDoorOpenReceived.Door Status" & objMDSControl.MDSDoorStatus)
    '        'If blnDoorStarted = True Then

    '        '    DoorSensorDetectedOpen()

    '        '    Select Case DoorSIUStatus
    '        '        Case clsMDSModuleControl.MDSDoorSIUStatus.SIU_NOT_AVAILABLE
    '        '            udtEventDeviceArgs.iDeviceState = DoorStatus.DoorOpened
    '        '            AppLogInfo("EvtDoorOpenReceived.Door Status:SIU_NOT_AVAILABLE")
    '        '        Case clsMDSModuleControl.MDSDoorSIUStatus.SIU_RUN
    '        '            udtEventDeviceArgs.iDeviceState = DoorStatus.DoorClosed
    '        '            AppLogInfo("EvtDoorOpenReceived.Door Status:SIU_RUN")
    '        '        Case clsMDSModuleControl.MDSDoorSIUStatus.SIU_MAINTENANCE
    '        '            udtEventDeviceArgs.iDeviceState = DoorStatus.DoorMaintenaceMode
    '        '            AppLogInfo("EvtDoorOpenReceived.Door Status:SIU_MAINTENANCE")
    '        '        Case clsMDSModuleControl.MDSDoorSIUStatus.SIU_SUPERVISOR
    '        '            udtEventDeviceArgs.iDeviceState = DoorStatus.DoorSupervisorMode
    '        '            AppLogInfo("EvtDoorOpenReceived.Door Status:SIU_SUPERVISOR")
    '        '        Case Else
    '        '            udtEventDeviceArgs.iDeviceState = DoorStatus.DoorOpened
    '        '            AppLogInfo("EvtDoorOpenReceived.Door Status:Unknown")
    '        '    End Select

    '        '    RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)
    '        'End If
    '    Catch ex As Exception
    '        AppLogErr("Error in clsDoorInterface.objMDSControl_EvtDoorOpenReceived:" & ex.Message)
    '    End Try
    'End Sub


#End Region

    Private Sub objMDSControl_MDSDoorEvent(ByVal intDoorStatusID As Integer) Handles objMDSControl.MDSDoorEvent
        Try

            AppLogInfo("Door Events - objMDSControl_MDSCashStatusUpdateEvt:" & intDoorStatusID)

            Select Case intDoorStatusID
                Case 0
                    udtEventDeviceArgs.iDeviceState = DoorStatus.DoorClosed
                    'udtEventDeviceArgs.iDeviceState = DoorStatus.DoorSupervisorMode
                    AppLogInfo("EvtDoorOpenReceived.Door Status:SIU_NOT_AVAILABLE")
                Case 1
                    udtEventDeviceArgs.iDeviceState = DoorStatus.DoorClosed
                    AppLogInfo("EvtDoorOpenReceived.Door Status:SIU_RUN")
                Case 2
                    udtEventDeviceArgs.iDeviceState = DoorStatus.DoorMaintenaceMode
                    AppLogInfo("EvtDoorOpenReceived.Door Status:SIU_MAINTENANCE")
                Case 4
                    udtEventDeviceArgs.iDeviceState = DoorStatus.DoorSupervisorMode
                    AppLogInfo("EvtDoorOpenReceived.Door Status:SIU_SUPERVISOR")
                Case Else
                    udtEventDeviceArgs.iDeviceState = DoorStatus.DoorSupervisorMode
                    AppLogInfo("EvtDoorOpenReceived.Door Status:Unknown")
            End Select

            RaiseEvent EvtDeviceError(udtDeviceSender, udtEventDeviceArgs)

        Catch ex As Exception
            AppLogErr("Error in objMDSControl_MDSCashStatusUpdateEvt:" & ex.Message)
        End Try
    End Sub
End Class
