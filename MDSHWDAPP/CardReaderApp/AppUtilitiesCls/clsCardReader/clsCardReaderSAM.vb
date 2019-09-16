Imports System
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text

Public Class clsCardReaderSAM

    Dim intReaderComportNum As Integer
    Dim KRPIssuer(2) As Byte                                                'KRP used for Issuer Authentication
    Dim KRPVerifyValidation(2) As Byte                                      'KRP used for certificate verification and card verification
    Dim SAMRandomNumber(8) As Byte                                          'SAM Generated Random Number
    Dim SAMCryptogramKRPKey(7) As Byte                                      'SAM Profile generated - Cryptogram KRP 
    Dim SAMCARDCertificate(8) As Byte                                       'Certificate from card read result
    Dim CARDSerialNumber(8) As Byte                                         'Card Serial Number
    Dim CARDSAMIDKey(2) As Byte                                             'Default Directory header of the SAM which contains keys that associates with the card keys
    Dim EXTRandomNumber(8) As Byte                                          'Cerificate
    Dim AUTHMessage(8) As Byte                                              'Certificate  

    Dim strCardSerialNumber As String
    Dim strAPIError As String

    Structure CARDEFTrack
        Dim strEFTrack1 As String
        Dim strEFTrack2 As String
        Dim strEFTrack3 As String
    End Structure

    ReadOnly Property PMPCTrack As CARDEFTrack
        Get
            Return udtTrackEF
        End Get
    End Property


    ReadOnly Property GetLastAPIError As String
        Get
            Return strAPIError
        End Get
    End Property

    ReadOnly Property GetCardSerialNumber As String
        Get
            Return strCardSerialNumber
        End Get
    End Property

    Public Function GetPMPCTrack(ByVal intComNumber As Integer) As Boolean
        Dim SAMSerialNumber(8) As Byte                          'SAM Serial Number
        Dim nextAction As Integer = 0
        Dim intStatus As Integer
        Dim tmpStatus As Integer

        'Dim SAMCmdSend As String
        'Dim DCSKRef As Byte                                     'D_CSK...Reference
        'Dim SAMNewPin(8) As Byte
        'Dim intSCAddrx As Integer                               'Start address of new PIN - decimal
        'Dim intPreStatus As Integer
        'Dim bolValid As Boolean
        'Dim intTmpLen As Integer
        'Dim intTTmpLen As Integer
        'Dim intDummy As Integer
        'int next = 0;
        'unsigned int i;
        'USHORT mcrw_dev = ANY_DEVICE;
        'unsigned char rand_dummy[8] = {0,0,0,0,0,0,0,0};
        'unsigned char no_track1[PTRACK1LEN+1];
        'unsigned char no_track3[PTRACK3LEN+1];
        intReaderComportNum = intComNumber
        LastPMPCProcess = 0

        Try
            While nextAction >= 0

                tmpStatus = nextAction

                Select Case (nextAction)
                    Case 0
                        'ADI_LOOKUP_NAME
                        intStatus = CARDICCContactCmd()
                        If intStatus = NOERROR Then
                            nextAction = 1
                        End If
                    Case 1
                        '1.CRD : ATR
                        intStatus = CARDATR()
                        If intStatus = NOERROR Then
                            nextAction = 2
                        End If

                    Case 2
                        '2.SAM : ATR
                        intStatus = SAMATR()
                        If intStatus = NOERROR Then
                            nextAction = 3
                        End If
                    Case 3
                        '3. SAM : Select 3F00
                        intStatus = SAMSelectMF()
                        If intStatus = NOERROR Then
                            nextAction = 4
                        End If
                    Case 4
                        '4. SAM : Select EF Profile
                        intStatus = SAMSelectEFProfile()
                        If intStatus = NOERROR Then
                            nextAction = 5
                        End If
                    Case 5
                        '5. SAM : Read Profile
                        intStatus = SAMReadProfile()
                        If intStatus = NOERROR Then
                            nextAction = 6
                        End If
                    Case 6
                        '6. CRD : Select Public File
                        intStatus = CARDSelectPublicFile()
                        If intStatus = NOERROR Then
                            nextAction = 7
                        End If
                    Case 7
                        '7. CRD : Read Serial Number
                        intStatus = CARDReadSerialNumber()
                        If intStatus = NOERROR Then
                            nextAction = 8
                        End If
                    Case 8
                        '8. CRD : Select ATM File
                        intStatus = CARDSelectATMFile()
                        If intStatus = NOERROR Then
                            nextAction = 9
                        End If
                    Case 9
                        '9. CRD : Select EF-Key-SAM-ID File
                        intStatus = CARDSelectEFKeySAMID()
                        If intStatus = NOERROR Then
                            nextAction = 10
                        End If
                    Case 10
                        '10. CRD : Select Key-SAM-ID Number Reading
                        intStatus = CARDKeySAMID()
                        If intStatus = NOERROR Then
                            nextAction = 11
                        End If
                    Case 11
                        '11. SAM : Selection of FI file
                        intStatus = SAMSelectFIFile()
                        If intStatus = NOERROR Then
                            nextAction = 12
                        ElseIf intStatus = NOTONUSTERMINAL Then
                            nextAction = 110
                        End If
                    Case 110
                        '11a. SAM : Selection of MEPS Key DF (7F00)
                        intStatus = SAMSelectMEPSKeyDF()
                        If intStatus = NOERROR Then
                            nextAction = 12
                        End If
                    Case 12
                        '12. SAM : Submit PIN Code
                        intStatus = SAMSubmitPinCode()
                        If intStatus = NOERROR Then
                            nextAction = 13
                        End If
                    Case 13
                        '13. SAM : Generate Random
                        intStatus = SAMGenerateRandom()
                        If intStatus = NOERROR Then
                            nextAction = 14
                        End If
                    Case 14
                        '14. CARD : Certificate Calculation
                        intStatus = CARDCertificateCalculation()
                        If intStatus = NOERROR Then
                            nextAction = 15
                        End If
                    Case 15
                        '15. CARD : Read Result
                        intStatus = CARDReadResult()
                        If intStatus = NOERROR Then
                            nextAction = 16
                        End If
                    Case 16
                        '16. SAM : Select Encryption M_CAK0V0 / M_CAK2V0,V1,V2
                        intStatus = SAMSelectEncryptionMCAK0V0()
                        If intStatus = NOERROR Then
                            nextAction = 17
                        End If
                    Case 17
                        '17. SAM : Provide Reference and Content
                        intStatus = SAMProvideRefContent()
                        If intStatus = NOERROR Then
                            nextAction = 18
                        End If
                    Case 18
                        '18. SAM : Generate Certificate
                        intStatus = SAMGenerateCertificate()
                        If intStatus = NOERROR Then
                            nextAction = 19
                        End If
                    Case 19
                        '19. SAM : Verify Certificate
                        intStatus = SAMVerifyCertificate()
                        If intStatus = NOERROR Then
                            nextAction = 20
                        End If
                    Case 20
                        '20. CARD : Generate Random
                        intStatus = CARDGenerateRandom()
                        If intStatus = NOERROR Then
                            nextAction = 21
                        End If
                    Case 21
                        '21. SAM : Select Encryption M_CSK0V0
                        intStatus = SAMSelectEncryptionMCSK0V0()
                        If intStatus = NOERROR Then
                            nextAction = 22
                        End If
                    Case 22
                        '22. SAM : Provide Input Parameter
                        intStatus = SAMProvideInputParameter()
                        If intStatus = NOERROR Then
                            nextAction = 23
                        End If
                    Case 23
                        '23. SAM : Calculate Auth Message
                        intStatus = SAMCalculateAuthMsg()
                        If intStatus = NOERROR Then
                            nextAction = 24
                        End If
                    Case 24
                        '24. CARD : Card Auth D_CSK0V0
                        intStatus = CARDAuthDCSK()
                        If intStatus = NOERROR Then
                            nextAction = 25
                        End If
                    Case 25
                        '25. CARD : Select EFTrack
                        intStatus = CARDSelectEFTrack2()
                        intStatus = CARDRead35BTrack2()

                        intStatus = CARDSelectEFTrack1()
                        intStatus = CARDRead35BTrack1()

                        intStatus = CARDSelectEFTrack3()
                        intStatus = CARDRead35BTrack3()

                        If intStatus = NOERROR Then
                            nextAction = -1
                        End If

                End Select

                If tmpStatus = nextAction Then
                    LastPMPCProcess = nextAction

                    CARDICCDeactivate()
                    SAMDeactivate()
                    CARDICCReleaseContactCmd()
                    Return False
                    Exit While
                End If

                If nextAction = -1 Then

                    CARDICCDeactivate()
                    SAMDeactivate()
                    CARDICCReleaseContactCmd()
                    Return True
                End If
            End While

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CARDICCContactCmd() As Integer
        Dim strComPortName As String
        Dim intReply As Integer
        Dim bData() As Byte = {}
        Dim strReply As String
        Dim intStatus As Integer
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H40
                .bParameterCode = &H30

                .Data.lpbBody = Marshal.AllocHGlobal(bData.Length)
                Marshal.Copy(bData, 0, .Data.lpbBody, bData.Length)
                .Data.dwSize = bData.Length
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                intStatus = NOERROR
            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDATR() As Integer
        Dim strComPortName As String
        Dim intReply As Integer
        Dim bData() As Byte = {}
        Dim strReply As String
        Dim intStatus As Integer
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H30

                .Data.lpbBody = Marshal.AllocHGlobal(bData.Length)
                Marshal.Copy(bData, 0, .Data.lpbBody, bData.Length)
                .Data.dwSize = bData.Length
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                intStatus = NOERROR
            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function SAMATR() As Integer
        Dim strComPortName As String
        Dim intReply As Integer
        Dim bData() As Byte = {}
        Dim strReply As String
        Dim intStatus As Integer

        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H40

                .Data.lpbBody = Marshal.AllocHGlobal(bData.Length)
                Marshal.Copy(bData, 0, .Data.lpbBody, bData.Length)
                .Data.dwSize = bData.Length
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                intStatus = NOERROR

            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMSelectMF() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim SAM_SelectMFCmd() As Byte = {&HBC, &HA4, &H0, &H0, &H2, &H3F, &H0}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_SelectMFCmd.Length)
                Marshal.Copy(SAM_SelectMFCmd, 0, .Data.lpbBody, SAM_SelectMFCmd.Length)
                .Data.dwSize = SAM_SelectMFCmd.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                intStatus = SAMERROR
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If

            Else
                intStatus = SAMERROR
            End If
            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMSelectEFProfile() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim SAM_SelectEFProf() As Byte = {&HBC, &HA4, &H0, &H0, &H2, &H2F, &H2}
        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_SelectEFProf.Length)
                Marshal.Copy(SAM_SelectEFProf, 0, .Data.lpbBody, SAM_SelectEFProf.Length)
                .Data.dwSize = SAM_SelectEFProf.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))

                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMReadProfile() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim SAM_ReadProf() As Byte = {&HBC, &HB0, &H0, &H0, &HC}
        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_ReadProf.Length)
                Marshal.Copy(SAM_ReadProf, 0, .Data.lpbBody, SAM_ReadProf.Length)
                .Data.dwSize = SAM_ReadProf.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                With udtReplyStruct.message.positiveReply.Data
                    SAMCryptogramKRPKey(0) = .bBody(0)
                    SAMCryptogramKRPKey(1) = .bBody(1)
                    SAMCryptogramKRPKey(2) = .bBody(4)
                    SAMCryptogramKRPKey(3) = .bBody(5)
                    SAMCryptogramKRPKey(4) = .bBody(6)
                    SAMCryptogramKRPKey(5) = .bBody(7)
                    SAMCryptogramKRPKey(6) = .bBody(8)
                End With

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(12))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If

            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function CARDSelectPublicFile() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_SelPublicFile() As Byte = {&H0, &HA4, &H0, &H0, &H2, &H17, &HFF}
        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_SelPublicFile.Length)
                Marshal.Copy(CARD_SelPublicFile, 0, .Data.lpbBody, CARD_SelPublicFile.Length)
                .Data.dwSize = CARD_SelPublicFile.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDReadSerialNumber() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim strTemSerialNum As String = ""

        Dim CARD_ReadSerialNum() As Byte = {&H0, &HB0, &H0, &H0, &H8}
        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            strCardSerialNumber = String.Empty

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_ReadSerialNum.Length)
                Marshal.Copy(CARD_ReadSerialNum, 0, .Data.lpbBody, CARD_ReadSerialNum.Length)
                .Data.dwSize = CARD_ReadSerialNum.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                For i = 0 To CARDSerialNumber.Length - 2
                    CARDSerialNumber(i) = udtReplyStruct.message.positiveReply.Data.bBody(i)

                    'MsgBox("Card Serial Number:" & Hex(CARDSerialNumber(i)))
                    'MsgBox("Card Serial Number - " & i & "-" & CARDSerialNumber(i) & "-" & Hex(CARDSerialNumber(i)))

                    strTemSerialNum = Hex(CARDSerialNumber(i))
                    strTemSerialNum = strTemSerialNum.Trim
                    If strTemSerialNum.Length < 2 Then
                        strTemSerialNum = "0" & strTemSerialNum
                    End If
                    strCardSerialNumber = strCardSerialNumber & strTemSerialNum.Trim
                    strCardSerialNumber = strCardSerialNumber.Trim

                Next

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(8))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDSelectATMFile() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_SelATMFile() As Byte = {&H0, &HA4, &H0, &H0, &H2, &H7F, &H10}
        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_SelATMFile.Length)
                Marshal.Copy(CARD_SelATMFile, 0, .Data.lpbBody, CARD_SelATMFile.Length)
                .Data.dwSize = CARD_SelATMFile.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Or bDataReply = Hex(&H97) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDSelectEFKeySAMID() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_SelEFKeySAMID() As Byte = {&H0, &HA4, &H0, &H0, &H2, &H6F, &H10}

        Try
            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_SelEFKeySAMID.Length)
                Marshal.Copy(CARD_SelEFKeySAMID, 0, .Data.lpbBody, CARD_SelEFKeySAMID.Length)
                .Data.dwSize = CARD_SelEFKeySAMID.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDKeySAMID() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_KeySAMIDNumRead() As Byte = {&H0, &HB0, &H0, &H0, &H2}
        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_KeySAMIDNumRead.Length)
                Marshal.Copy(CARD_KeySAMIDNumRead, 0, .Data.lpbBody, CARD_KeySAMIDNumRead.Length)
                .Data.dwSize = CARD_KeySAMIDNumRead.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                For i = 0 To CARDSAMIDKey.Length - 1
                    CARDSAMIDKey(i) = udtReplyStruct.message.positiveReply.Data.bBody(i)
                Next

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(2))

                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function SAMSelectFIFile() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim SAM_SelectFIFile() As Byte = {&HBC, &HA4, &H0, &H0, &H2, 0, 0}
        Try

            SAM_SelectFIFile(5) = CARDSAMIDKey(0)
            SAM_SelectFIFile(6) = CARDSAMIDKey(1)

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_SelectFIFile.Length)
                Marshal.Copy(SAM_SelectFIFile, 0, .Data.lpbBody, SAM_SelectFIFile.Length)
                .Data.dwSize = SAM_SelectFIFile.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(1))

                If bDataReply = &H0 Then

                    'On-US Terminal
                    KRPVerifyValidation(0) = &HC
                    KRPVerifyValidation(1) = SAMCryptogramKRPKey(5)
                    KRPIssuer(0) = &H8
                    KRPIssuer(1) = SAMCryptogramKRPKey(4)
                    intStatus = NOERROR

                ElseIf bDataReply = &H8 Then

                    'Not-On-US Terminal
                    KRPVerifyValidation(0) = &HE
                    KRPVerifyValidation(1) = SAMCryptogramKRPKey(3)
                    KRPIssuer(0) = &HA
                    KRPIssuer(1) = SAMCryptogramKRPKey(2)
                    intStatus = NOTONUSTERMINAL

                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMSelectMEPSKeyDF() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim SAM_SelectMEPSKeyDF() As Byte = {&HBC, &HA4, &H0, &H0, &H2, &H7F, &H0}

        Try
            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_SelectMEPSKeyDF.Length)
                Marshal.Copy(SAM_SelectMEPSKeyDF, 0, .Data.lpbBody, SAM_SelectMEPSKeyDF.Length)
                .Data.dwSize = SAM_SelectMEPSKeyDF.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMSubmitPinCode() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte
        Dim x As Integer = 0

        Dim SAM_SubmitPinCode() As Byte = {&HBC, &H20, &H0, &H0, &H8, 0, 0, 0, 0, 0, 0, 0, 0}
        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            For i = 5 To SAM_SubmitPinCode.Length - 1
                SAM_SubmitPinCode(i) = SAMPINCode(x)
                x = x + 1
            Next

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_SubmitPinCode.Length)
                Marshal.Copy(SAM_SubmitPinCode, 0, .Data.lpbBody, SAM_SubmitPinCode.Length)
                .Data.dwSize = SAM_SubmitPinCode.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))

                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If

            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMGenerateRandom() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim SAM_GenerateRandom() As Byte = {&HBC, &HC4, &H0, &H0, &H8}

        Try
            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_GenerateRandom.Length)
                Marshal.Copy(SAM_GenerateRandom, 0, .Data.lpbBody, SAM_GenerateRandom.Length)
                .Data.dwSize = SAM_GenerateRandom.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                For i = 0 To SAMRandomNumber.Length - 1
                    SAMRandomNumber(i) = udtReplyStruct.message.positiveReply.Data.bBody(i)
                Next

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(8))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function CARDCertificateCalculation() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_CertificateCalculation() As Byte = {&H0, &H88, &H0, &H0, &HA, 0, 0, &H0, &H0, 0, 0, 0, 0, 0, 0}
        'Bytes for Certificate Calculation - 0h 88h 0h 0h 0Ah KRPVerifiValid(2-byte) 0h 0h SAMRandomKey(6-bytes)  
        Try

            'Assign from SAM Random Generation
            CARD_CertificateCalculation(5) = KRPVerifyValidation(0)
            CARD_CertificateCalculation(6) = KRPVerifyValidation(1)

            CARD_CertificateCalculation(9) = SAMRandomNumber(0)
            CARD_CertificateCalculation(10) = SAMRandomNumber(1)
            CARD_CertificateCalculation(11) = SAMRandomNumber(2)
            CARD_CertificateCalculation(12) = SAMRandomNumber(3)
            CARD_CertificateCalculation(13) = SAMRandomNumber(4)
            CARD_CertificateCalculation(14) = SAMRandomNumber(5)

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_CertificateCalculation.Length)
                Marshal.Copy(CARD_CertificateCalculation, 0, .Data.lpbBody, CARD_CertificateCalculation.Length)
                .Data.dwSize = CARD_CertificateCalculation.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDReadResult() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_ReadResult() As Byte = {&H0, &HC0, &H0, &H0, &H8}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_ReadResult.Length)
                Marshal.Copy(CARD_ReadResult, 0, .Data.lpbBody, CARD_ReadResult.Length)
                .Data.dwSize = CARD_ReadResult.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                For i = 0 To SAMCARDCertificate.Length - 1
                    SAMCARDCertificate(i) = udtReplyStruct.message.positiveReply.Data.bBody(i)
                Next

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(8))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function SAMSelectEncryptionMCAK0V0() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim x As Integer = 0
        Dim bDataReply As Byte

        Dim SAM_SelectEncrypKey() As Byte = {&HCC, &HA0, 0, 0, &H8, 0, 0, 0, 0, 0, 0, 0, 0}

        Try

            SAM_SelectEncrypKey(2) = KRPVerifyValidation(0)
            SAM_SelectEncrypKey(3) = SAMCryptogramKRPKey(6)

            x = 0
            For i = 5 To SAM_SelectEncrypKey.Length - 1
                SAM_SelectEncrypKey(i) = CARDSerialNumber(x)
                x = x + 1
            Next

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_SelectEncrypKey.Length)
                Marshal.Copy(SAM_SelectEncrypKey, 0, .Data.lpbBody, SAM_SelectEncrypKey.Length)
                .Data.dwSize = SAM_SelectEncrypKey.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMProvideRefContent() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String

        Dim x As Integer = 0
        Dim bDataReply As Byte

        Dim SAM_ProvideRefContent() As Byte = {&HCC, &HD0, &H0, &H0, &H8, &H6F, &H10, &HD0, &H80, 0, 0, &H0, &H0}

        Try
            SAM_ProvideRefContent(9) = CARDSAMIDKey(0)
            SAM_ProvideRefContent(10) = CARDSAMIDKey(1)

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_ProvideRefContent.Length)
                Marshal.Copy(SAM_ProvideRefContent, 0, .Data.lpbBody, SAM_ProvideRefContent.Length)
                .Data.dwSize = SAM_ProvideRefContent.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMGenerateCertificate() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim x As Integer
        Dim bDataReply As Byte

        Dim SAM_GenCert() As Byte = {&HCC, &H80, &H0, &H0, &H8, &H0, &H0, 0, 0, 0, 0, 0, 0}
        Try

            x = 0
            For i = 7 To SAM_GenCert.Length - 1
                SAM_GenCert(i) = SAMRandomNumber(x)
                x = x + 1
            Next

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_GenCert.Length)
                Marshal.Copy(SAM_GenCert, 0, .Data.lpbBody, SAM_GenCert.Length)
                .Data.dwSize = SAM_GenCert.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMVerifyCertificate() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim x As Integer
        Dim bDataReply As Byte

        Dim SAM_VerifyCert() As Byte = {&HCC, &HF0, &H0, &H0, &H8, 0, 0, 0, 0, 0, 0, 0, 0}
        Try

            x = 0
            For i = 5 To SAM_VerifyCert.Length - 1
                SAM_VerifyCert(i) = SAMCARDCertificate(x)
                x = x + 1
            Next

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_VerifyCert.Length)
                Marshal.Copy(SAM_VerifyCert, 0, .Data.lpbBody, SAM_VerifyCert.Length)
                .Data.dwSize = SAM_VerifyCert.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function CARDGenerateRandom() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_GenerateRand() As Byte = {&H0, &HC4, &H0, &H0, &H8}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_GenerateRand.Length)
                Marshal.Copy(CARD_GenerateRand, 0, .Data.lpbBody, CARD_GenerateRand.Length)
                .Data.dwSize = CARD_GenerateRand.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                For i = 0 To EXTRandomNumber.Length - 1
                    EXTRandomNumber(i) = udtReplyStruct.message.positiveReply.Data.bBody(i)
                Next

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(8))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function SAMSelectEncryptionMCSK0V0() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String

        Dim x As Integer = 0
        Dim bDataReply As Byte

        Dim SAM_SelectMCSKEncrypKey() As Byte = {&HCC, &HA0, 0, 0, &H8, 0, 0, 0, 0, 0, 0, 0, 0}
        Try

            SAM_SelectMCSKEncrypKey(2) = KRPIssuer(0)
            SAM_SelectMCSKEncrypKey(3) = SAMCryptogramKRPKey(6)

            x = 0
            For i = 5 To SAM_SelectMCSKEncrypKey.Length - 1
                SAM_SelectMCSKEncrypKey(i) = CARDSerialNumber(x)
                x = x + 1
            Next

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_SelectMCSKEncrypKey.Length)
                Marshal.Copy(SAM_SelectMCSKEncrypKey, 0, .Data.lpbBody, SAM_SelectMCSKEncrypKey.Length)
                .Data.dwSize = SAM_SelectMCSKEncrypKey.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMProvideInputParameter() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String

        Dim x As Integer = 0
        Dim bDataReply As Byte

        Dim SAM_ProvideInputParam() As Byte = {&HCC, &HD0, &H0, &H0, &H8, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Try

            x = 0
            For i = 5 To SAM_ProvideInputParam.Length - 1
                SAM_ProvideInputParam(i) = EXTRandomNumber(x)
                x = x + 1
            Next

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_ProvideInputParam.Length)
                Marshal.Copy(SAM_ProvideInputParam, 0, .Data.lpbBody, SAM_ProvideInputParam.Length)
                .Data.dwSize = SAM_ProvideInputParam.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function SAMCalculateAuthMsg() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim x As Integer = 0
        Dim bDataReply As Byte

        Dim SAM_CalcAuthMsg() As Byte = {&HCC, &H70, &H0, &H0, &H8}
        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H49

                .Data.lpbBody = Marshal.AllocHGlobal(SAM_CalcAuthMsg.Length)
                Marshal.Copy(SAM_CalcAuthMsg, 0, .Data.lpbBody, SAM_CalcAuthMsg.Length)
                .Data.dwSize = SAM_CalcAuthMsg.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                For i = 0 To AUTHMessage.Length - 1
                    AUTHMessage(i) = udtReplyStruct.message.positiveReply.Data.bBody(i)
                Next

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(8))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = SAMERROR
                End If
            Else
                intStatus = SAMERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return SAMERROR
        End Try
    End Function

    Public Function CARDAuthDCSK() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim x As Integer
        Dim bDataReply As Byte

        Dim CARD_AuthDCSK() As Byte = {&H0, &H48, &H0, &H0, &HA, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

        Try

            CARD_AuthDCSK(5) = KRPIssuer(0)
            CARD_AuthDCSK(6) = KRPIssuer(1)

            x = 0
            For i = 7 To CARD_AuthDCSK.Length - 1
                CARD_AuthDCSK(i) = AUTHMessage(x)
                x = x + 1
            Next

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_AuthDCSK.Length)
                Marshal.Copy(CARD_AuthDCSK, 0, .Data.lpbBody, CARD_AuthDCSK.Length)
                .Data.dwSize = CARD_AuthDCSK.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDSelectEFTrack1() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_GetEFTrack1() As Byte = {&H0, &HA4, &H0, &H0, &H2, &H6F, &H11}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_GetEFTrack1.Length)
                Marshal.Copy(CARD_GetEFTrack1, 0, .Data.lpbBody, CARD_GetEFTrack1.Length)
                .Data.dwSize = CARD_GetEFTrack1.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDSelectEFTrack2() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_GetEFTrack2() As Byte = {&H0, &HA4, &H0, &H0, &H2, &H6F, &H12}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_GetEFTrack2.Length)
                Marshal.Copy(CARD_GetEFTrack2, 0, .Data.lpbBody, CARD_GetEFTrack2.Length)
                .Data.dwSize = CARD_GetEFTrack2.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDSelectEFTrack3() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        Dim bDataReply As Byte

        Dim CARD_GetEFTrack3() As Byte = {&H0, &HA4, &H0, &H0, &H2, &H6F, &H13}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_GetEFTrack3.Length)
                Marshal.Copy(CARD_GetEFTrack3, 0, .Data.lpbBody, CARD_GetEFTrack3.Length)
                .Data.dwSize = CARD_GetEFTrack3.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))
                If bDataReply = Hex(&H90) Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function


    Public Function CARDRead35BTrack2() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        'Dim bDataReply As Byte

        Dim str35BCardData As String = String.Empty

        Dim CARD_GetDataTrack2() As Byte = {&H0, &HB0, &H0, &H0, &H27}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_GetDataTrack2.Length)
                Marshal.Copy(CARD_GetDataTrack2, 0, .Data.lpbBody, CARD_GetDataTrack2.Length)
                .Data.dwSize = CARD_GetDataTrack2.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                'bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(76))

                For i = 0 To udtReplyStruct.message.positiveReply.Data.bBody.Length - 1
                    str35BCardData = str35BCardData & Chr(udtReplyStruct.message.positiveReply.Data.bBody(i))
                Next

                udtTrackEF.strEFTrack2 = str35BCardData
                intStatus = NOERROR

                'If bDataReply = Hex(&H90) Then
                '    intStatus = NOERROR
                'Else
                '    intStatus = ICCERROR
                'End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDRead35BTrack1() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        'Dim bDataReply As Byte

        Dim str35BCardData As String = String.Empty

        Dim CARD_GetDataTrack1() As Byte = {&H0, &HB0, &H0, &H0, &H4C}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_GetDataTrack1.Length)
                Marshal.Copy(CARD_GetDataTrack1, 0, .Data.lpbBody, CARD_GetDataTrack1.Length)
                .Data.dwSize = CARD_GetDataTrack1.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                'bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(76))

                For i = 0 To udtReplyStruct.message.positiveReply.Data.bBody.Length - 1
                    str35BCardData = str35BCardData & Chr(udtReplyStruct.message.positiveReply.Data.bBody(i))
                Next

                udtTrackEF.strEFTrack1 = str35BCardData
                intStatus = NOERROR

                'If bDataReply = Hex(&H90) Then
                '    intStatus = NOERROR
                'Else
                '    intStatus = ICCERROR
                'End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDRead35BTrack3() As Integer
        Dim intReply As Integer
        Dim intStatus As Integer
        Dim strComPortName As String
        Dim strReply As String
        'Dim bDataReply As Byte

        Dim str35BCardData As String = String.Empty


        Dim CARD_GetDataTrack3() As Byte = {&H0, &HB0, &H0, &H0, &H48}

        Try

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H39

                .Data.lpbBody = Marshal.AllocHGlobal(CARD_GetDataTrack3.Length)
                Marshal.Copy(CARD_GetDataTrack3, 0, .Data.lpbBody, CARD_GetDataTrack3.Length)
                .Data.dwSize = CARD_GetDataTrack3.Length
            End With

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                'bDataReply = Hex(udtReplyStruct.message.positiveReply.Data.bBody(0))

                For i = 0 To udtReplyStruct.message.positiveReply.Data.bBody.Length - 1
                    str35BCardData = str35BCardData & Chr(udtReplyStruct.message.positiveReply.Data.bBody(i))
                Next

                udtTrackEF.strEFTrack3 = str35BCardData
                intStatus = NOERROR

                'If bDataReply = Hex(&H90) Then
                '    intStatus = NOERROR
                'Else
                '    intStatus = ICCERROR
                'End If

            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function


    Public Function CARDICCReleaseContactCmd() As Integer
        Dim strComPortName As String
        Dim intReply As Integer
        Dim bData() As Byte = {}
        Dim strReply As String
        Dim intStatus As Integer
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H40
                .bParameterCode = &H32

                .Data.lpbBody = Marshal.AllocHGlobal(bData.Length)
                Marshal.Copy(bData, 0, .Data.lpbBody, bData.Length)
                .Data.dwSize = bData.Length
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                If Hex(udtReplyStruct.message.positiveReply.Data.bBody(9)) = &H90 Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If
            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function CARDICCDeactivate() As Integer
        Dim strComPortName As String
        Dim intReply As Integer
        Dim bData() As Byte = {}
        Dim strReply As String
        Dim intStatus As Integer
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H40
                .bParameterCode = &H31

                .Data.lpbBody = Marshal.AllocHGlobal(bData.Length)
                Marshal.Copy(bData, 0, .Data.lpbBody, bData.Length)
                .Data.dwSize = bData.Length
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)

                If Hex(udtReplyStruct.message.positiveReply.Data.bBody(9)) = &H90 Then
                    intStatus = NOERROR
                Else
                    intStatus = ICCERROR
                End If
            Else
                intStatus = ICCERROR
            End If

            Return intStatus
        Catch ex As Exception
            Return ICCERROR
        End Try
    End Function

    Public Function SAMDeactivate() As Integer
        Dim strComPortName As String
        Dim intReply As Integer

        Dim strReply As String
        Try
            udtCommandStruct = New CommandStruct

            With udtCommandStruct
                .bCommandCode = &H49
                .bParameterCode = &H41

                .Data.dwSize = 0
            End With

            udtReplyStruct = New ReplyData
            strComPortName = "COM" & intReaderComportNum & " "

            intReply = NativeMethods.ExecuteCommand(strComPortName, udtCommandStruct, 10000, udtReplyStruct)

            If udtReplyStruct.replyType = REPLY_TYPE.NegativeReply Then
                strReply = "Reply Type - Negative, Reply Code - bE0:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE0) & " & bE1:" & Chr(udtReplyStruct.message.negativeReply.ErrorCode.bE1)
                Return strReply
            ElseIf udtReplyStruct.replyType = REPLY_TYPE.PositiveReply Then
                strReply = "Reply Type - Positive, Reply Code - bSt0:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt0) & " & bSt1:" & Chr(udtReplyStruct.message.positiveReply.StatusCode.bSt1)
                ' Return strReply
            Else
                ' Return "Reply Type - " & udtReplyStruct.replyType
            End If

            Return intReply

        Catch ex As Exception
            strReply = "EXCEPTION ERROR DeactivateSAM: " & ex.Message
            Return 0
        End Try
    End Function

End Class
