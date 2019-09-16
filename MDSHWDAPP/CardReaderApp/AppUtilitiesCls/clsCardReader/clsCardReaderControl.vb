Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD


Public Class clsCardReaderControl


#Region "Card Reader Control"

    Structure CARDREADERHWDSTR
        Dim blnCARDREADERENABLE As Boolean
        Dim CARDREADERPortNum As Integer
        Dim CARDREADERBaudRate As String
        Dim CARDREADERCMDTimeout As Long
        Dim CARDREADERCheckTimeout As Long
        Dim CARDREADEREjectTimeout As Long
        Dim CARDREADERMaxCaptureCount As Integer
        Dim PMPCPINFilePath As String
    End Structure

    Structure CARDREADERERROR

        Dim CARDRDRTRXSTATNOERR As String
        Dim CARDRDRTRXSTATCARDCAPTURETIMEOUT As String
        Dim CARDRDRTRXSTATCARDCAPTUREJAM As String
        Dim CARDRDRTRXSTATCARDUPDERR As String
        Dim CARDRDRTRXSTATINVALIDTRACK As String
        Dim CARDRDRTRXSTATERRTRCKDT As String

        Dim CARDRDRSMRTCARDGOOD As String
        Dim CARDRDRSMRTCARDCMDERR As String
        Dim CARDRDRSMRTCARDNOPRSNT As String
        Dim CARDRDRSMRTCARDFATALSMRTCARD As String
        Dim CARDRDRSMRTCARDSCIFWARN As String
        Dim CARDRDRSMRTCARDSCIFENCRYPTERR As String
        Dim CARDRDRSMRTCARDOUTPOSITION As String

    End Structure

    Structure CARDREADERDEVICE

        Dim CARDRDR_DEVGRAPHICID As String
        Dim CARDRDR_DEVGRAPHICNAME As String

        Dim CARDRDR_DSTNOCARD As String
        Dim CARDRDR_DSTCARDINREADER As String
        Dim CARDRDR_DSTCARDSTAGE As String
        Dim CARDRDR_DSTCARDCAPTURE As String
        Dim CARDRDR_DSTCARDEJECTFAIL As String
        Dim CARDRDR_DSTCARDEJECT As String
        Dim CARDRDR_DSTCARDBLOCK As String

        Dim CARDRDR_EVTTYPE_WRAPDEVICE As String
        Dim CARDRDR_EVTTYPE_TIMEOUT As String
        Dim CARDRDR_EVTTYPE_DATAARRIVED As String
        Dim CARDRDR_EVTTYPE_ERROR As String

        Dim CARDRDR_DVST_ERRIN_READSTATE As String
        Dim CARDRDR_DVST_ERRIN_LOCKSTATE As String

        Dim CARDRDR_ERST_NOERR As String
        Dim CARDRDR_ERST_READERERR As String
        Dim CARDRDR_ERST_READERFATAL As String
        Dim CARDRDR_ERST_READERWARN As String

        Dim CARDRDR_DGST_STATUS As String

        Dim CARDRDR_SYST_STATUSNONEWSTATE As String
        Dim CARDRDR_SYST_STATUSNOOVERFILL As String
        Dim CARDRDR_SYST_STATUSOVERFILL As String

    End Structure

    ReadOnly Property CARDREADERHWDInfo() As CARDREADERHWDSTR
        Get
            Return udtCARDREADERHWDCFG
        End Get
    End Property

    ReadOnly Property CARDREADERERRORCODE() As CARDREADERERROR
        Get
            Return udtCARDREADERTRXSTATERRORCODE
        End Get
    End Property

    ReadOnly Property CARDREADERDEVICECODE() As CARDREADERDEVICE
        Get
            Return udtCARDREADERDEVICECODE
        End Get
    End Property

    Property CardInsertCheckTimeout As Long
        Get
            Return udtCARDREADERHWDCFG.CARDREADERCheckTimeout
        End Get
        Set(value As Long)
            udtCARDREADERHWDCFG.CARDREADERCheckTimeout = value
            WriteCardInsertCheckTimeout(value)
        End Set
    End Property

    Property CardAtGateTimeout As Long
        Get
            Return udtCARDREADERHWDCFG.CARDREADEREjectTimeout
        End Get
        Set(value As Long)
            udtCARDREADERHWDCFG.CARDREADEREjectTimeout = value
            WriteCardAtGateTimeout(value)
        End Set
    End Property

    Property CardCommandTimeout As Long
        Get
            Return udtCARDREADERHWDCFG.CARDREADERCMDTimeout
        End Get
        Set(value As Long)
            udtCARDREADERHWDCFG.CARDREADERCMDTimeout = value
            WriteCardCommandTimeout(value)
        End Set
    End Property


#End Region


#Region "CR Setting Property"

    ReadOnly Property EnableCROpt() As Boolean
        Get
            Return udtCARDREADERHWDCFG.blnCARDREADERENABLE
        End Get
    End Property

    ReadOnly Property CRComportNo() As String
        Get
            Return udtCARDREADERHWDCFG.CARDREADERPortNum.ToString
        End Get
    End Property

    ReadOnly Property CRCmdTimeout() As String
        Get
            Return udtCARDREADERHWDCFG.CARDREADERCMDTimeout.ToString
        End Get
    End Property

    ReadOnly Property CRChkTimeout() As String
        Get
            Return udtCARDREADERHWDCFG.CARDREADERCheckTimeout.ToString
        End Get
    End Property

    ReadOnly Property CREjtTimeout() As String
        Get
            Return udtCARDREADERHWDCFG.CARDREADEREjectTimeout.ToString
        End Get
    End Property

    ReadOnly Property CRPSAMIDPath() As String
        Get
            Return udtCARDREADERHWDCFG.PMPCPINFilePath
        End Get
    End Property


#End Region


#Region "Card Reader Event"

    Public Event CardReaderDataReady(ByVal strData As CardTrackReading)

#End Region

#Region "InitCls/Close Object -  Control"

    'Init Class Object
    Public Function InitCARDREADERControl() As Boolean
        Dim strLogIniPath As String = String.Empty
        Dim strCARDREADERHWDIniPath As String = String.Empty
        Dim strCARDREADERERRORCODEIniPath As String = String.Empty
        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(CARDREADERHWDLAYERINIPATH, CARDREADER) = True Then

                'Read INI File
                'Log Ini File
                '1.Cardreader Hardware
                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strCARDREADERHWDIniPath = .strAppLayerINIPath1.Trim
                    strCARDREADERERRORCODEIniPath = .strAppLayerINIPath2.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "CARD READER Layer Class Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                AppLogInfo("Logger INI Path:" & strLogIniPath)

                'Log Ini File
                '1.Card Reader Hardware
                AppLogInfo("CARD READER HWD INI PATH:" & strCARDREADERHWDIniPath.Trim)

                'Init/Get CARDREADER Layer CLASS Setting
                ReadCARDREADERHWDCFG()

                '2. Card Reader Error Code
                AppLogInfo("CARD READER ERROR CODE INI PATH:" & strCARDREADERERRORCODEIniPath.Trim)

                'Init/Get CARDREADER Layer CLASS Setting
                ReadCARDREADERERRORCODE()
                ReadCARDREADERDEVICECODE()

                'Read the PMPC SAM ID
                If ReadSAMPINCode() = False Then
                    Return False
                End If

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
    Public Function CloseCARDREADERControl() As Boolean
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

#Region "CARDREADER Read INI Setting"




    Public Function ReadCARDREADERHWDCFG() As Boolean
        Try
            'Read CARDREADER HWD CFG
            If clsReadCARDREADERSetting() = True Then
                AppLogInfo("Read CARDREADER HWD CFG Info OK")
                With udtCARDREADERHWDCFG
                    AppLogInfo("CARDREADER Enable:" & .blnCARDREADERENABLE)
                    AppLogInfo("CARDREADER COMPortNum:" & .CARDREADERPortNum)
                    AppLogInfo("CARDREADER COMBoundRate:" & .CARDREADERBaudRate.Trim)
                    AppLogInfo("CARDREADER CheckCMDTimeout:" & .CARDREADERCMDTimeout)
                    AppLogInfo("CARDREADER CheckTimeout:" & .CARDREADERCheckTimeout)
                    AppLogInfo("CARDREADER EjectTimeout:" & .CARDREADEREjectTimeout)
                    AppLogInfo("CARDREADER MaxCaptureCount:" & .CARDREADERMaxCaptureCount)
                    AppLogInfo("CARDREADER SAMPinPath:" & .PMPCPINFilePath)
                End With
                Return True
            Else
                AppLogWarn("Read CARDREADER HWD CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  ReadCARDREADERHWDCFG. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "CARDREADER ERROR CODE INI"

    Public Function ReadCARDREADERERRORCODE() As Boolean
        Try
            If clsReadCARDREADERErrorCode() = True Then
                AppLogInfo("Read CARDREADER ERRORCODE CFG Info OK")
                With udtCARDREADERTRXSTATERRORCODE
                    AppLogInfo("CARDREADER NOERROR:" & .CARDRDRTRXSTATNOERR)
                    AppLogInfo("CARDREADER CAPTURETIMEOUT:" & .CARDRDRTRXSTATCARDCAPTURETIMEOUT)
                    AppLogInfo("CARDREADER CAPTUREJAM:" & .CARDRDRTRXSTATCARDCAPTUREJAM)
                    AppLogInfo("CARDREADER UPDATEERROR:" & .CARDRDRTRXSTATCARDUPDERR)
                    AppLogInfo("CARDREADER INVLDTRACK:" & .CARDRDRTRXSTATINVALIDTRACK)
                End With
                Return True
            Else
                AppLogWarn("Read CARDREADER ERRORCODE CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  ReadCARDREADERERRORCODE. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function ReadCARDREADERDEVICECODE() As Boolean
        Try
            If clsReadCARDREADERDeviceNDCCode() = True Then
                AppLogInfo("Read clsReadCARDREADERDeviceNDCCode Info OK")
                Return True
            Else
                AppLogWarn("Read clsReadCARDREADERDeviceNDCCode Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  clsReadCARDREADERDeviceNDCCode. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function
#End Region

#Region "Read SAMPIN Code"

    Public Function ReadSAMPINCode() As Boolean
        Dim strPath As String = ""
        Dim strPIN As String = ""
        Dim strPINByte() As String
        Dim strReader As StreamReader = Nothing
        Dim blnRtn As Boolean
        Try

            If File.Exists(udtCARDREADERHWDCFG.PMPCPINFilePath) Then


                'Read the PSAM ID KEY FROM INI
                strPIN = ReadPSAMIDKeyValue(udtCARDREADERHWDCFG.PMPCPINFilePath)

                'strPath = udtCARDREADERHWDCFG.PMPCPINFilePath
                'strReader = New StreamReader(strPath)
                'strPIN = strReader.ReadToEnd
                strPINByte = Split(strPIN, "|", -1, CompareMethod.Text)

                ReDim SAMPINCode(8)

                For i = 0 To strPINByte.Count - 1
                    SAMPINCode(i) = Convert.ToByte(strPINByte(i), 16)
                Next

                AppLogInfo("PSAMID Read Success")
                blnRtn = True
            Else
                AppLogWarn("PSAMID File Not Exist")
                blnRtn = False
            End If

            Return blnRtn

        Catch ex As Exception
            AppLogErr("Error In Read SAMPIN Code:" & ex.Message)
            Return False
        End Try
    End Function
#End Region

#Region "Card Reader Status"

    Structure CardReaderStatus
        Dim StatusType As String
        Dim StatusCode1 As String
        Dim StatusCode1Desc As String
        Dim StatusCode2 As String
        Dim StatusCode2Desc As String
    End Structure

    Public udtCardReaderStatus As CardReaderStatus

#End Region

#Region "Initialize \ Close CardReader - NDC Integration"

    Public Function InitCARDREADERHWD() As Boolean
        Dim strTmpComPort As String
        Dim intReply As Integer
        Dim strReply As String
        Dim strReplyDesc As String

        Dim blnRtn As Boolean = False

        Try
            If udtCARDREADERHWDCFG.blnCARDREADERENABLE = True Then
                strTmpComPort = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "
                intReply = ConnectDevice(strTmpComPort, udtCARDREADERHWDCFG.CARDREADERBaudRate)
                strReply = Hex(intReply)

                If strReply = 0 Then
                    If InitializeCardReader() = True Then
                        AppLogInfo("Init CARDREADER Comport Success")
                        blnRtn = True
                    Else
                        AppLogInfo("Init CARDREADER Comport Failed")
                        blnRtn = False
                    End If
                Else
                    strReplyDesc = ConnectDeviceReplyDesc(strReply)
                    AppLogInfo("Connect Card Reader Device Error = " & strReply & "-" & strReplyDesc)
                    blnRtn = False
                End If
            Else
                AppLogInfo("Card Reader Disabled")
                blnRtn = True
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in InitCARDREADERHWD. Err Info: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function CloseCARDREADERHWD() As Boolean
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Dim strReplyDesc As String
        Try

            If udtCARDREADERHWDCFG.blnCARDREADERENABLE = True Then
                strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "
                intReply = DisconnectDevice(strComPortName)
                strReply = Hex(intReply)

                If strReply = 0 Then
                    AppLogInfo("Close CARDREADER Comport Success")
                    Return True
                Else
                    strReplyDesc = DisconnectDeviceReplyDesc(strReply)
                    AppLogInfo("Disconnect Card Reader Device Error = " & strReply & "-" & strReplyDesc)
                    Return False
                End If
            Else
                'Card Reader Disable
                Return True
            End If

            'Close the logger

        Catch ex As Exception
            AppLogErr("Error in CloseCARDREADERHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function InitializeCardReader() As Boolean
        Dim strComPortName As String
        Dim strReply As String

        Dim intReply As Integer
        Dim bData() As Byte = {&H33, &H32, &H34, &H30, &H30, &H30, &H30, &H30, &H30, &H30, &H30}
        Try
            strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "

            udtCommandStruct = New CommandStruct
            With udtCommandStruct
                .bCommandCode = &H30
                .bParameterCode = &H32
                'Allocate IntPtr Size, and copy Data Byte
                .Data.lpbBody = Marshal.AllocHGlobal(bData.Length)
                Marshal.Copy(bData, 0, .Data.lpbBody, bData.Length)
                .Data.dwSize = bData.Length
            End With

            udtReplyStruct = New ReplyData
            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                AppLogWarn(strReply)
                Return False
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                AppLogInfo(strReply)
                Return True
            Else
                AppLogErr("Reply Type - " & udtReplyStruct.replyType)
                Return False
            End If

        Catch ex As Exception
            AppLogErr("EXCEPTION ERROR InitializeReader: " & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Card Reader Operation - NDC Integration"

    Public Function EnableCardReader() As Boolean
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H3A
                .bParameterCode = &H30

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                AppLogWarn(strReply)
                Return False
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                AppLogInfo(strReply)
                Return True
            Else
                AppLogErr("Reply Type - " & udtReplyStruct.replyType)
                Return False
            End If

        Catch ex As Exception
            AppLogErr("EXCEPTION ERROR EnableCmd: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function DisableCardReader() As Boolean
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H3A
                .bParameterCode = &H31

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                AppLogWarn(strReply)
                Return False
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                AppLogInfo(strReply)
                Return True
            Else
                AppLogErr("Reply Type - " & udtReplyStruct.replyType)
                Return False
            End If

        Catch ex As Exception
            AppLogErr("EXCEPTION ERROR DisableCmd: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function RequestStatusCardReader() As Boolean
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String

        Dim intDataReplyCnt As Integer
        Dim strMsgDetReply As String = String.Empty

        Dim strCode1 As Char
        Dim strCode2 As Char

        Dim strCode1Desc As String
        Dim strCode2Desc As String

        Try
            udtCommandStruct = New CommandStruct
            udtCardReaderStatus = New CardReaderStatus

            With udtCommandStruct
                .bCommandCode = &H31
                .bParameterCode = &H32
                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, udtCARDREADERHWDCFG.CARDREADERCMDTimeout, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then

                strReply = GetReplyType(udtReplyStruct.replyType)

                strCode1 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0)
                strCode2 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)

                strCode1Desc = ErrorCodeDesc(strCode1)
                strCode2Desc = ErrorCodeDesc(strCode2)

                With udtCardReaderStatus
                    .StatusType = strReply
                    .StatusCode1 = strCode1
                    .StatusCode1Desc = strCode1Desc
                    .StatusCode2 = strCode2
                    .StatusCode2Desc = strCode2Desc
                End With

                AppLogInfo("Reply Type - " & strReply & ", Reply Code - bE0: " & strCode1 & "-" & strCode1Desc)
                Return False

            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then


                strReply = GetReplyType(udtReplyStruct.replyType)

                strCode1 = Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0)
                strCode2 = Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                strCode1Desc = StatusCodeDesc(strCode1)
                strCode2Desc = StatusCodeDesc(strCode2)

                intDataReplyCnt = udtReplyStruct.message.positiveReply.Data.dwSize

                For i = 0 To intDataReplyCnt - 1
                    If strMsgDetReply = String.Empty Then
                        strMsgDetReply = udtReplyStruct.message.positiveReply.Data.bBody(i)
                    Else
                        strMsgDetReply = strMsgDetReply & "|" & udtReplyStruct.message.positiveReply.Data.bBody(i)
                    End If
                Next

                With udtCardReaderStatus
                    .StatusType = strReply
                    .StatusCode1 = strCode1
                    .StatusCode1Desc = strCode1Desc
                    .StatusCode2 = strCode2
                    .StatusCode2Desc = strCode2Desc
                End With

                AppLogInfo("Reply Type - " & strReply & ", Reply Code - bE0: " & strCode1 & "-" & strCode1Desc)
                Return True
            Else

                strReply = GetReplyType(udtReplyStruct.replyType)

                strCode1 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0)
                strCode2 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)

                strCode1Desc = ErrorCodeDesc(strCode1)
                strCode2Desc = ErrorCodeDesc(strCode2)

                With udtCardReaderStatus
                    .StatusType = strReply
                    .StatusCode1 = strCode1
                    .StatusCode1Desc = strCode1Desc
                    .StatusCode2 = strCode2
                    .StatusCode2Desc = strCode2Desc
                End With

                AppLogInfo("Reply Type - " & strReply & ", Reply Code - bE0: " & strCode1 & "-" & strCode1Desc)
                Return False

            End If

        Catch ex As Exception
            AppLogErr("EXCEPTION ERROR RequestStatusSankyoDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function CheckCardReader() As Boolean
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String

        Dim intDataReplyCnt As Integer
        Dim strMsgDetReply As String = String.Empty

        Dim strCode1 As Char
        Dim strCode2 As Char

        Dim strCode1Desc As String
        Dim strCode2Desc As String

        Try
            udtCommandStruct = New CommandStruct
            udtCardReaderStatus = New CardReaderStatus

            With udtCommandStruct
                .bCommandCode = &H31
                .bParameterCode = &H32
                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, udtCARDREADERHWDCFG.CARDREADERCMDTimeout, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then

                strReply = GetReplyType(udtReplyStruct.replyType)

                strCode1 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0)
                strCode2 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)

                strCode1Desc = ErrorCodeDesc(strCode1)
                strCode2Desc = ErrorCodeDesc(strCode2)

                With udtCardReaderStatus
                    .StatusType = strReply
                    .StatusCode1 = strCode1
                    .StatusCode1Desc = strCode1Desc
                    .StatusCode2 = strCode2
                    .StatusCode2Desc = strCode2Desc
                End With

                'AppLogInfo("Reply Type - " & strReply & ", Reply Code - bE0: " & strCode1 & "-" & strCode1Desc)
                Return False

            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then


                strReply = GetReplyType(udtReplyStruct.replyType)

                strCode1 = Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0)
                strCode2 = Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                strCode1Desc = StatusCodeDesc(strCode1)
                strCode2Desc = StatusCodeDesc(strCode2)

                intDataReplyCnt = udtReplyStruct.message.positiveReply.Data.dwSize

                For i = 0 To intDataReplyCnt - 1
                    If strMsgDetReply = String.Empty Then
                        strMsgDetReply = udtReplyStruct.message.positiveReply.Data.bBody(i)
                    Else
                        strMsgDetReply = strMsgDetReply & "|" & udtReplyStruct.message.positiveReply.Data.bBody(i)
                    End If
                Next

                With udtCardReaderStatus
                    .StatusType = strReply
                    .StatusCode1 = strCode1
                    .StatusCode1Desc = strCode1Desc
                    .StatusCode2 = strCode2
                    .StatusCode2Desc = strCode2Desc
                End With

                'AppLogInfo("Reply Type - " & strReply & ", Reply Code - bE0: " & strCode1 & "-" & strCode1Desc)
                Return True
            Else

                strReply = GetReplyType(udtReplyStruct.replyType)

                strCode1 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0)
                strCode2 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)

                strCode1Desc = ErrorCodeDesc(strCode1)
                strCode2Desc = ErrorCodeDesc(strCode2)

                With udtCardReaderStatus
                    .StatusType = strReply
                    .StatusCode1 = strCode1
                    .StatusCode1Desc = strCode1Desc
                    .StatusCode2 = strCode2
                    .StatusCode2Desc = strCode2Desc
                End With

                'AppLogInfo("Reply Type - " & strReply & ", Reply Code - bE0: " & strCode1 & "-" & strCode1Desc)
                Return False

            End If

        Catch ex As Exception
            AppLogErr("EXCEPTION ERROR RequestStatusSankyoDevice: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function ReadCardTrack() As Boolean
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H36
                .bParameterCode = &H35

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)
            udtCardInfo = New CardTrackReading

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return False

            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then

                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                ProcessCardTrack(udtReplyStruct.message.positiveReply.Data.bBody, udtReplyStruct.message.positiveReply.Data.dwSize)
                Return True
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR CardCarryCaptureCmd: " & ex.Message
        End Try
    End Function

    Public Function ReadCardTrackPMPC() As Boolean
        Dim blnRtn As Boolean

        Try
            If objPMPCOperation.GetPMPCTrack(udtCARDREADERHWDCFG.CARDREADERPortNum) Then
                ProcessPMPCTrackData()
                blnRtn = True
            Else
                blnRtn = False
            End If

            Return blnRtn

        Catch ex As Exception
            blnRtn = False
            Return blnRtn
        End Try
    End Function

    Public Function CardReaderCaptureCard() As Boolean
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H33
                .bParameterCode = &H31
                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                AppLogInfo(strReply)
                Return False
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                AppLogInfo(strReply)
                Return True
            Else
                strReply = "Reply Type - " & udtReplyStruct.replyType
                AppLogInfo(strReply)
                Return False
            End If

        Catch ex As Exception
            AppLogErr("EXCEPTION ERROR CaptureCardReader: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function CardReaderEjectCard() As String
        Dim strComPortName As String
        Dim intReply As Integer
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H33
                .bParameterCode = &H30
                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & udtCARDREADERHWDCFG.CARDREADERPortNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                AppLogInfo("Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1))
                Return False
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                AppLogInfo("Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1))
                Return True
            Else
                AppLogInfo("Reply Type - " & udtReplyStruct.replyType)
                Return False
            End If

        Catch ex As Exception
            AppLogErr("EXCEPTION ERROR CardCarryEjectCmd: " & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Card Reader Operation - Single Operation"

    Public Function GetDLLInfo() As String
        Dim MyDataPts As IntPtr = IntPtr.Zero
        Dim strDLLInformation As String = String.Empty

        Try
            udtDLLInformation = New DLLInformation

            MyDataPts = Marshal.AllocHGlobal(1024)
            GetDllInformation(MyDataPts)
            udtDLLInformation = CType(Marshal.PtrToStructure(MyDataPts, GetType(DLLInformation)), DLLInformation)
            Marshal.FreeHGlobal(MyDataPts)

            With udtDLLInformation
                strDLLInformation = .UpperDll.szFileName & " ( " & .UpperDll.szRevision & " ) " & Environment.NewLine & .LowerDll.szFileName & " ( " & .LowerDll.szRevision & " ) "
            End With

            Return strDLLInformation
        Catch ex As Exception
            Return "Error In GetDLLInformation : " & ex.Message
        End Try
    End Function

    Public Function ConnectSankyoDevice(ByVal intComPort As Integer, ByVal lngBaudRate As Long) As String
        Dim strComPortName As String
        Dim strReply As String
        Dim intReply As Integer

        Try
            strComPortName = "COM" & intComPort & " "
            intReply = ConnectDevice(strComPortName, lngBaudRate)
            strReply = Hex(intReply)

            If strReply = "0" Then
                Return "NO ERROR"
            ElseIf strReply = "101" Then
                Return "CANNOT_CREATE_OBJECT_ERROR"
            ElseIf strReply = "102" Then
                Return "DEVICE_NOT_READY_ERROR"
            ElseIf strReply = "103" Then
                Return "CANNOT_OPEN_PORT_ERROR"
            ElseIf strReply = "104" Then
                Return "FAILED_TO_BEGIN_THREAD_ERROR"
            ElseIf strReply = "105" Then
                Return "DEVICE_ALREADY_CONNECTED_ERROR"
            Else
                Return "ERROR_UNKNOWN"
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR ConnectSankyoDevice: " & ex.Message
        End Try
    End Function

    Public Function DisconnectSankyoDevice(ByVal intComPort As Integer) As String
        Dim strComPortName As String
        Dim strReply As String
        Dim intReply As Integer

        Try
            strComPortName = "COM" & intComPort & " "
            intReply = DisconnectDevice(strComPortName)
            strReply = Hex(intReply)

            If strReply = "0" Then
                Return "NO ERROR"
            ElseIf strReply = "1" Then
                Return "DEVICE_NOT_CONNECTED_ERROR"
            Else
                Return "ERROR_UNKNOWN"
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR DisconnectSankyoDevice: " & ex.Message
        End Try
    End Function

    Public Function InitializeSankyoDevice(ByVal intComport As Integer) As String
        Dim strComPortName As String
        Dim strReply As String

        Dim intReply As Integer
        Dim bData() As Byte = {&H33, &H32, &H34, &H30, &H30, &H30, &H30, &H30, &H30, &H30, &H30}

        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H30
                .bParameterCode = &H30

                'Allocate IntPtr Size, and copy Data Byte
                .Data.lpbBody = Marshal.AllocHGlobal(bData.Length)
                Marshal.Copy(bData, 0, .Data.lpbBody, bData.Length)
                .Data.dwSize = bData.Length
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intComport & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return strReply

            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                Return strReply
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR OpenGateSankyoDevice: " & ex.Message
        End Try
    End Function

    Public Function RequestStatusSankyoDevice(ByVal intComport As Integer) As String
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String

        Dim intDataReplyCnt As Integer
        Dim strMsgDetReply As String = String.Empty

        Dim strCode1 As Char
        Dim strCode2 As Char

        Dim strCode1Desc As String
        Dim strCode2Desc As String

        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H31
                .bParameterCode = &H32
                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intComport & " "
            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then

                strCode1 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0)
                strCode2 = Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)

                strCode1Desc = ErrorCodeDesc(strCode1)
                strCode2Desc = ErrorCodeDesc(strCode2)

                strReply = "Reply Type - Negative, Reply Code - bE0: " & strCode1Desc & Environment.NewLine
                Return strReply
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then

                strCode1 = Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0)
                strCode2 = Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                strCode1Desc = StatusCodeDesc(strCode1)
                strCode2Desc = StatusCodeDesc(strCode2)

                intDataReplyCnt = udtReplyStruct.message.positiveReply.Data.dwSize

                For i = 0 To intDataReplyCnt - 1
                    If strMsgDetReply = String.Empty Then
                        strMsgDetReply = udtReplyStruct.message.positiveReply.Data.bBody(i)
                    Else
                        strMsgDetReply = strMsgDetReply & "|" & udtReplyStruct.message.positiveReply.Data.bBody(i)
                    End If
                Next

                strReply = "Reply Type - Positive, Reply Code - bSt0: " & strCode1Desc & Environment.NewLine & "Reply Data: " & strMsgDetReply
                Return strReply
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR RequestStatusSankyoDevice: " & ex.Message
        End Try
    End Function

    Public Function EntryCmd(ByVal intComport As Integer) As String
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H32
                .bParameterCode = &H30

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intComport & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return strReply
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                Return strReply
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR EntryCmd: " & ex.Message
        End Try
    End Function

    Public Function EnableDisableCmd(ByVal intComport As Integer) As String
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H3A
                .bParameterCode = &H30

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intComport & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return strReply
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                Return strReply
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR EnableDisableCmd: " & ex.Message
        End Try
    End Function

    Public Function CardCarryEjectCmd(ByVal intComport As Integer) As String
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H33
                .bParameterCode = &H30

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intComport & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return strReply
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                Return strReply
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR CardCarryEjectCmd: " & ex.Message
        End Try
    End Function

    Public Function CardCarryCaptureCmd(ByVal intComport As Integer) As String
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H33
                .bParameterCode = &H31

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intComport & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return strReply
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                Return strReply
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR CardCarryCaptureCmd: " & ex.Message
        End Try
    End Function

    Public Function CardRetrieveCmd(ByVal intComport As Integer) As String
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H34
                .bParameterCode = &H30

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intComport & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return strReply
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                Return strReply
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR CardCarryCaptureCmd: " & ex.Message
        End Try
    End Function

    Public Function ReadCardTrackCmd(ByVal intComport As Integer) As String
        Dim strComPortName As String
        Dim intReply As Integer
        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H36
                .bParameterCode = &H35

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intComport & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)
            udtCardInfo = New CardTrackReading

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return strReply
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                ProcessCardTrack(udtReplyStruct.message.positiveReply.Data.bBody, udtReplyStruct.message.positiveReply.Data.dwSize)
                Return strReply
            Else
                Return "Reply Type - " & udtReplyStruct.replyType
            End If

        Catch ex As Exception
            Return "EXCEPTION ERROR CardCarryCaptureCmd: " & ex.Message
        End Try
    End Function

#End Region

#Region "Card Track Information"

    Public udtCardInfo As CardTrackReading

    Structure CardTrackReading
        Public strTrack1 As String
        Public strTrack2 As String
        Public strTrack3 As String
    End Structure

    Public PMPCCardSerialNumber As String

    Public Sub ProcessCardTrack(ByVal CardData() As Byte, ByVal intDataCount As Integer)
        Dim strCardData As String = String.Empty
        Dim strTempCardData As String
        Dim intTrackNum As Integer

        Try
            intTrackNum = 1

            For i = 0 To intDataCount - 1
                strTempCardData = Hex(CardData(i))
                If strTempCardData <> "7E" Then
                    If strCardData = String.Empty Then
                        strCardData = ChrW(CardData(i))
                    Else
                        strCardData = strCardData & ChrW(CardData(i))
                    End If
                Else
                    Select Case intTrackNum
                        Case 1
                            udtCardInfo.strTrack1 = strCardData
                        Case 2
                            udtCardInfo.strTrack2 = strCardData
                        Case 3
                            udtCardInfo.strTrack3 = strCardData
                            Exit For
                    End Select

                    strCardData = String.Empty
                    intTrackNum = intTrackNum + 1
                End If
            Next

            udtCardInfo.strTrack3 = strCardData

        Catch ex As Exception
            udtCardInfo = Nothing
        End Try
    End Sub

    Public Sub ProcessPMPCTrackData()
        Try
            If Not IsNothing(udtTrackEF) Then
                With udtCardInfo
                    .strTrack1 = udtTrackEF.strEFTrack1
                    .strTrack2 = udtTrackEF.strEFTrack2
                    .strTrack3 = udtTrackEF.strEFTrack3
                End With

                PMPCCardSerialNumber = objPMPCOperation.GetCardSerialNumber
            End If
        Catch ex As Exception
            udtCardInfo = Nothing
        End Try
    End Sub
#End Region

#Region "Card Reader Error Information"

    Public Function ConnectDeviceReplyDesc(ByVal strReply As String) As String
        If strReply = "0" Then
            Return "NO ERROR"
        ElseIf strReply = "101" Then
            Return "CANNOT_CREATE_OBJECT_ERROR"
        ElseIf strReply = "102" Then
            Return "DEVICE_NOT_READY_ERROR"
        ElseIf strReply = "103" Then
            Return "CANNOT_OPEN_PORT_ERROR"
        ElseIf strReply = "104" Then
            Return "FAILED_TO_BEGIN_THREAD_ERROR"
        ElseIf strReply = "105" Then
            Return "DEVICE_ALREADY_CONNECTED_ERROR"
        Else
            Return "ERROR_UNKNOWN"
        End If

    End Function

    Public Function DisconnectDeviceReplyDesc(ByVal strReply As String) As String
        If strReply = "0" Then
            Return "NO ERROR"
        ElseIf strReply = "1" Then
            Return "DEVICE_NOT_CONNECTED_ERROR"
        Else
            Return "ERROR_UNKNOWN"
        End If
    End Function

    Public Function StatusCodeDesc(ByVal strCode As String) As String
        Try
            Select Case strCode
                Case "0"
                    Return strCode & " - No card detected within ICRW (including gate)"
                Case "1"
                    Return strCode & " - Card located at gate"
                Case "2"
                    Return strCode & " - Card located inside ICRW"
                Case Else
                    Return strCode & " - Unknown Status"
            End Select
        Catch ex As Exception
            Return strCode & " - Exception: " & ex.Message
        End Try
    End Function

    Public Function ErrorCodeDesc(ByVal strCode As String) As String

        Try
            Select Case strCode
                Case "0"
                    Return strCode & " - A given command code is unidentified"
                Case "1"
                    Return strCode & " - Parameter not correct"
                Case "2"
                    Return strCode & " - Command execution is imposible"
                Case "3"
                    Return strCode & " - Hardware is not present"
                Case "4"
                    Return strCode & " - Command data error"
                Case "5"
                    Return strCode & " - Tried to card feed command before the IC contact release command"
                Case "6"
                    Return strCode & " - ICRW does not have keys that decipher the data"
                Case "10"
                    Return strCode & " - Card jammed"
                Case "11"
                    Return strCode & " - Shutter failure"
                Case "12"
                    Return strCode & " - Sensor failure of PD1, PD2, PD3, PDI/Card remains inside"
                Case "13"
                    Return strCode & " - Irregular card length (LONG)"
                Case "14"
                    Return strCode & " - Irregular card length (SHORT)"
                Case "15"
                    Return strCode & " - F-ROM error"
                Case "16"
                    Return strCode & " - The card was moved forbidly"
                Case "17"
                    Return strCode & " - Jam error at retrieve"
                Case "18"
                    Return strCode & " - SW1 or SW2 error"
                Case "19"
                    Return strCode & " - Card was not inserted from the rear"
                Case "20"
                    Return strCode & " - Read Error (Parity)"
                Case "21"
                    Return strCode & " - Read Error"
                Case "22"
                    Return strCode & " - Write Error"
                Case "23"
                    Return strCode & " - Read Error (Only SS-ES-LRC)"
                Case "24"
                    Return strCode & " - Read (no encode and/or no magnetic stripe)"
                Case "25"
                    Return strCode & " - Write Verify Error"
                Case "26"
                    Return strCode & " - Read Error (No SS)"
                Case "27"
                    Return strCode & " - Read Error (No ES)"
                Case "28"
                    Return strCode & " - Read Error (LRC Error)"
                Case "29"
                    Return strCode & " - Write Verify Error (Data discordance)"
                Case "30"
                    Return strCode & " - Power Down"
                Case "31"
                    Return strCode & " - DSR Signal was turned to OFF"
                Case Else
                    Return strCode & " - Unknown Error"
            End Select
        Catch ex As Exception
            Return strCode & " - Exception: " & ex.Message
        End Try
    End Function

    Public Function GetReplyType(ByVal intReplyType As Integer) As String
        Select Case intReplyType
            Case 0
                Return "PositiveReply"
            Case 1
                Return "NegativeReply"
            Case 2
                Return "ReplyReceivingFailure"
            Case 3
                Return "CommandCancellation"
            Case 4
                Return "ReplyTimeout"
            Case Else
                Return "ReplyUnknown"
        End Select
    End Function

#End Region

#Region "Support Method"

    Public Function UpdateHWDCRSetting(ByVal strCREnableOpt As String, ByVal strCRComport As String, ByVal strCRCmdTMOut As String, ByVal strCRChkTMOut As String, ByVal strCREjtTMOut As String, ByVal strPSAMIDPath As String) As Boolean
        Try
            If UpdateCRSetting(strCREnableOpt, strCRComport, strCRCmdTMOut, strCRChkTMOut, strCREjtTMOut, strPSAMIDPath) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function



#End Region

End Class
