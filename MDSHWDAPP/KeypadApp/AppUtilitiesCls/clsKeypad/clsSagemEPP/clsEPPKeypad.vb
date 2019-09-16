Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Imports System.Threading

Public Class clsEPPKeypad

#Region "Class Properties"

    ReadOnly Property GetMACedData() As String
        Get
            Return MAC_DATA
        End Get
    End Property

    ReadOnly Property GetCheckSumedData() As String
        Get
            Return CHECK_SUM
        End Get
    End Property

#End Region

#Region "Pinpad Event"

    Public Shared Event KeyPadInput(ByVal keyInput As String)
    Public Shared Event KeyPadHexInput(ByVal keyInput As String)
    Public Shared Event KeypadTimeout()
    Public Shared Event KeypadClickEnterButton()
    Public Shared Event KeypadClickCancelButton()
    Public Shared Event KeypadClickClearButton()
    Public Shared Event KeypadClickUnknownButton(ByVal keyInput As String)
    Public Shared Event KeypadClickEncryptNumericButton(ByVal keyInput As String)

#End Region

#Region "Open, Close, getComport Handler"

    Public Function OpenEPPComport(ByVal intComNum As String, ByVal lngBaudRate As String, ByVal strDevice As String) As Boolean
        Try
            If strDevice = "KEYPAD" Then
                EPP_COMPORT_HANDLER = OpenComPort(intComNum, lngBaudRate)

                If EPP_COMPORT_HANDLER = 0 Then
                    Return False
                Else
                    udtKEYPADHWDCFG.blnKeypadStatus = True
                    AppLogInfo("Keypad Status: Online")
                    Return True
                End If
            Else
                EPP_COMPORT_HANDLER = OpenComPort(intComNum, lngBaudRate)

                If EPP_COMPORT_HANDLER = 0 Then
                    Return False
                Else
                    udtHSMHWDCFG.blnKeypadStatus = True
                    AppLogInfo("HSM Status: Online")
                    Return True
                End If
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CloseEPPComport() As Boolean
        Try

            RC = CloseComPort(EPP_COMPORT_HANDLER)

            Select Case RC
                Case RC_OK
                    EPP_COMPORT_HANDLER = IntPtr.Zero
                    'MsgBox("RC_OK - Close Comport OK")
                    AppLogInfo("RC_OK - Close Comport OK")

                Case RC_COMPORTNEVEROPENED
                    'MsgBox("RC_COMPORTNEVEROPENED - Close Comport without call OpenComPort()")
                    AppLogWarn("RC_COMPORTNEVEROPENED - Close Comport without call OpenComPort()")
                Case RC_CLOSEERR
                    'MsgBox("RC_CLOSEERR - Close Comport Failed")
                    AppLogErr("RC_CLOSEERR - Close Comport Failed")
            End Select

            Marshal.FreeHGlobal(EPP_COMPORT_HANDLER)

            Return True

        Catch ex As Exception
            Return False
        End Try


    End Function

    Public Function GetHandleComPort(ByVal strComNum As String) As Boolean
        Dim intDataPts As IntPtr
        Dim intComNum As Short = CShort(Right(strComNum, 1))

        Try
            intDataPts = GetHandle(intComNum)

            If intDataPts = RC_OK Then
                MsgBox("Get COM pot Failed")
            Else
                MsgBox("Get COM pot Success")
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try


    End Function

#End Region

#Region "Build, Send, Receive, Get Paramter - Packet"
    Public Function MakeCmd(ByVal shtCmd As UShort) As Byte()

        'define sender packet to epp
        Dim tx_packet(99) As Byte
        Dim ptrTx_packet As IntPtr = IntPtr.Zero

        Try
            ptrTx_packet = Marshal.AllocHGlobal(tx_packet.Length)
            Marshal.Copy(tx_packet, 0, ptrTx_packet, tx_packet.Length)
            Make_command_vb(ptrTx_packet, shtCmd)

            Marshal.Copy(ptrTx_packet, tx_packet, 0, tx_packet.Length)

            Return tx_packet
        Catch ex As Exception
            Return tx_packet
        End Try

    End Function

    Public Function AppendParam(ByVal tx_packet As Byte(), ByVal ParamByte As Byte(), ByVal ParamType As Integer) As Byte()
        Dim ptrTx_packet As IntPtr = IntPtr.Zero
        Dim ptrBaudrate As IntPtr = IntPtr.Zero

        Try

            ptrTx_packet = Marshal.AllocHGlobal(tx_packet.Length)
            Marshal.Copy(tx_packet, 0, ptrTx_packet, tx_packet.Length)

            ptrBaudrate = Marshal.AllocHGlobal(ParamByte.Length)
            Marshal.Copy(ParamByte, 0, ptrBaudrate, ParamByte.Length)

            Append_parameter_vb(ptrTx_packet, ptrBaudrate, ParamByte.Length, ParamType)
            Marshal.Copy(ptrTx_packet, tx_packet, 0, tx_packet.Length)

            Return tx_packet

        Catch ex As Exception
            Return tx_packet
        End Try
    End Function

    Public Function SendReceive(ByVal tx_packet As Byte()) As Byte()
        Dim rx_packet(99) As Byte
        Dim RC As Integer

        Try
            RC = Send_receive(tx_packet, rx_packet, rx_packet.Length, EPP_COMPORT_HANDLER)
            Return rx_packet

        Catch ex As Exception
            Return rx_packet
        End Try
    End Function

    'Public Function GetCommand() As Short
    '    Dim CmdNo As Short = 0

    '    Try
    '        'With APP_
    '        '    .app_packet_length = 0
    '        '    .app_command = 0
    '        'End With

    '        CmdNo = Get_command(APP_)

    '        Return CmdNo
    '    Catch ex As Exception
    '        Return CmdNo
    '    End Try
    'End Function
#End Region


#Region "Pinpad Command"

    Public Function CMD_Set_Clear_Text() As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        'set param
        Dim bytMACValue(3) As Byte

        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Try

            AppLogInfo("clsEPPKeyPad - CMD_Set_Clear_Text")

            tx_packet = MakeCmd(1008)
            tx_packet = AppendParam(tx_packet, bytMACValue, CHARACTERS)

            rx_packet = SendReceive(tx_packet)

            AppLogInfo("clsEPPKeyPad - CMD_Set_Clear_Text: RC=" & RC)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CMD_SET_CLEAR_KEY(ByVal strClearKey As String, ByVal strClearAllKey As String) As Boolean
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub CMD_KEY_DETECTED(ByVal ptrRxPacket As IntPtr, ByVal ptrComPortHandle As IntPtr)
        Dim keys As String = New String("0", 99)
        Dim rx_packet(99) As Byte
        Dim ptrKeys As IntPtr = IntPtr.Zero
        Dim param_len As Integer
        Dim RC As Integer = 0

        Try
            GC.KeepAlive(ptrRxPacket)
            GC.KeepAlive(ptrComPortHandle)

            If RC = RC_OK Then
                Marshal.Copy(ptrRxPacket, rx_packet, 0, rx_packet.Length)
                ptrKeys = Marshal.AllocHGlobal(keys.Length)
                ptrKeys = Marshal.StringToBSTR(ptrKeys)

                Get_parameter_vb(rx_packet, 1, ptrKeys, param_len)

                KEY_STROKE = Marshal.PtrToStringAnsi(ptrKeys)

                'raise event
                Select Case KEY_STROKE
                    Case "D"
                        RaiseEvent KeypadClickEnterButton()

                    Case "T"
                        RaiseEvent KeypadTimeout()

                    Case "*", "#", "C"
                        RaiseEvent KeypadClickUnknownButton(KEY_STROKE)

                    Case "A"
                        RaiseEvent KeypadClickCancelButton()

                    Case "B"
                        RaiseEvent KeypadClickClearButton()

                    Case "N"
                        RaiseEvent KeypadClickEncryptNumericButton(KEY_STROKE)

                    Case Else
                        RaiseEvent KeyPadInput(KEY_STROKE)
                End Select

            End If

        Catch ex As Exception
        Finally
            ptrKeys = IntPtr.Zero
            ptrRxPacket = IntPtr.Zero
            ptrComPortHandle = IntPtr.Zero
            Marshal.FreeHGlobal(ptrKeys)
            Marshal.FreeHGlobal(ptrRxPacket)
            Marshal.FreeHGlobal(ptrComPortHandle)
        End Try
    End Sub

    Public Sub CMD_HEX_KEY_DETECTED(ByVal ptrRxPacket As IntPtr, ByVal ptrComPortHandle As IntPtr)
        Dim keys As String = New String("0", 99)
        Dim rx_packet(99) As Byte
        Dim ptrKeys As IntPtr = IntPtr.Zero
        Dim param_len As Integer
        Dim RC As Integer = 0

        Try
            GC.KeepAlive(ptrRxPacket)
            GC.KeepAlive(ptrComPortHandle)

            If RC = RC_OK Then
                Marshal.Copy(ptrRxPacket, rx_packet, 0, rx_packet.Length)
                ptrKeys = Marshal.AllocHGlobal(keys.Length)
                ptrKeys = Marshal.StringToBSTR(ptrKeys)

                Get_parameter_vb(rx_packet, 1, ptrKeys, param_len)

                KEY_STROKE = Marshal.PtrToStringAnsi(ptrKeys)

                'raise event
                Select Case KEY_STROKE

                    Case "T"
                        RaiseEvent KeypadTimeout()

                    Case Else
                        RaiseEvent KeyPadHexInput(KEY_STROKE)
                End Select

            End If

        Catch ex As Exception
        Finally
            ptrKeys = IntPtr.Zero
            ptrRxPacket = IntPtr.Zero
            ptrComPortHandle = IntPtr.Zero
            Marshal.FreeHGlobal(ptrKeys)
            Marshal.FreeHGlobal(ptrRxPacket)
            Marshal.FreeHGlobal(ptrComPortHandle)
        End Try
    End Sub

    Public Function CMD_START_PIN() As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        'set param
        'Dim bytMACValue(3) As Byte

        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Try
            tx_packet = MakeCmd(19)

            'timer.Interval = udtKEYPADHWDCFG.intKeypadTimeOut
            'timer.Enabled = True
            'timer.Start()

            'bytGlobalTxPacket = tx_packet
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CMD_STOP_PIN() As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        'set param
        'Dim bytMACValue(3) As Byte

        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Try
            tx_packet = MakeCmd(20)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CMD_READ_PIN(ByVal bytFormatType As Byte(), ByVal bytParam As Byte(), ByVal bytKeyIndex As Byte(), ByRef strEncPINBlock As String) As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        'set param
        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Dim PINBlock As String = String.Empty
        Dim strPINBlock As String

        Try
            tx_packet = MakeCmd(26)
            tx_packet = AppendParam(tx_packet, bytFormatType, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytParam, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytKeyIndex, CHARACTERS)

            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            'get response code from epp
            If RC = RC_OK Then
                RC = Get_parameter_vb(rx_packet, 2, ptrReturnEppRC, param_len)

                If RC = RC_OK Then
                    'intReturnKCV = CDec(Marshal.ReadInt16(ptrReturnKCV))
                    'intReturnKCV = SwapHighLow(intReturnKCV)
                    For i As Integer = 10 To 17
                        strPINBlock = ChangeDecToHexDec(rx_packet(i))

                        If PINBlock = String.Empty Then
                            PINBlock = strPINBlock
                        Else
                            PINBlock &= strPINBlock
                        End If
                    Next

                    PIN_BLOCK = PINBlock
                    strEncPINBlock = PIN_BLOCK
                End If
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CMD_TEST_EPP() As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte
        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Try
            tx_packet = MakeCmd(38)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CMD_CLEAN_DATA() As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte
        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Try
            tx_packet = MakeCmd(122)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CMD_SET_CONFIGURATION() As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        'set param
        Dim bytConfig(7) As Byte

        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer

        Try
            bytConfig(0) = 1
            bytConfig(1) = 1
            bytConfig(3) = 1
            bytConfig(3) = 1
            bytConfig(4) = 1
            bytConfig(5) = 1
            bytConfig(6) = 1
            bytConfig(7) = 127

            tx_packet = MakeCmd(993)
            tx_packet = AppendParam(tx_packet, bytConfig, CHARACTERS)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                If RC = RC_OK Then
                    intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                    intReturnEppRC = SwapHighLow(intReturnEppRC)

                    RC = intReturnEppRC
                End If

            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CMD_START_KEY() As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte
        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Try
            tx_packet = MakeCmd(1014)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CMD_STORE_KEY() As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte
        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Try
            tx_packet = MakeCmd(1014)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CMD_STOP_KEY(ByVal bytVariable As Byte()) As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte
        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Dim bytMACValue(3) As Byte

        Try
            tx_packet = MakeCmd(1015)
            tx_packet = AppendParam(tx_packet, bytVariable, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytMACValue, CHARACTERS)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                intReturnEppRC = SwapHighLow(intReturnEppRC)

                RC = intReturnEppRC
            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If

            'Return True

        Catch ex As Exception
            AppLogErr("Error in CMD_STOP_KEY : " & ex.Message)
            Return False
        End Try

    End Function

    Public Function CMD_WRITE_MPA(ByVal bytKeyIndex As Byte(), ByVal bytWorkingKey As Byte(), ByVal bytEncKeyIndex As Byte(), ByVal bytIndicator As Byte(), ByVal bytWorkingKeyLen As Byte()) As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Dim bytZeroBits(1) As Byte


        Try
            tx_packet = MakeCmd(7)
            tx_packet = AppendParam(tx_packet, bytKeyIndex, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytIndicator, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytEncKeyIndex, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytWorkingKeyLen, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytWorkingKey, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytZeroBits, CHARACTERS)

            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                If RC = RC_OK Then
                    intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                    intReturnEppRC = SwapHighLow(intReturnEppRC)

                    RC = intReturnEppRC
                End If

            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CMD_SET_KEY(ByVal strVariable As String) As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        'parameter
        Dim bytVar() As Byte
        Dim bytMACValue(3) As Byte

        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim param_len As Integer
        Dim Ascii As New ASCIIEncoding()

        Try
            bytVar = Ascii.GetBytes(strVariable)
            tx_packet = MakeCmd(1016)
            tx_packet = AppendParam(tx_packet, bytVar, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytMACValue, CHARACTERS)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                If RC = RC_OK Then
                    intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                    intReturnEppRC = SwapHighLow(intReturnEppRC)

                    RC = intReturnEppRC
                End If

            End If

            If RC = RC_OK Then
                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CMD_CHECK_SUM(ByVal strVarKey As String, ByVal strKVVData As String) As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        'parameter
        Dim bytKeyIndex() As Byte

        Dim RC As Integer
        Dim KCV As String = String.Empty
        Dim intReturnEppRC As Integer
        Dim strKCV As String
        Dim ptrReturnEppRC As IntPtr
        Dim ptrReturnKCV As IntPtr
        Dim bytReturnEppChksum(4) As Byte
        'Dim ptrReturnEppChksum As IntPtr
        Dim param_len As Integer
        Dim Ascii As New ASCIIEncoding()

        Try
            bytKeyIndex = Ascii.GetBytes(strVarKey)
            tx_packet = MakeCmd(10)
            tx_packet = AppendParam(tx_packet, bytKeyIndex, CHARACTERS)
            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                If RC = RC_OK Then
                    intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                    intReturnEppRC = SwapHighLow(intReturnEppRC)

                    RC = intReturnEppRC
                End If
            End If

            'get decimal KCV
            If RC = RC_OK Then

                RC = Get_parameter_vb(rx_packet, 2, ptrReturnKCV, param_len)

                If RC = RC_OK Then

                    For i As Integer = 10 To 13
                        strKCV = ChangeDecToHexDec(rx_packet(i))

                        If KCV = String.Empty Then
                            KCV = strKCV
                        Else
                            KCV &= strKCV
                        End If
                    Next
                End If

            End If

            If strKVVData = String.Empty Then
                If RC = RC_OK Then

                    CHECK_SUM = KCV

                    Return True

                Else
                    Return False
                End If

            Else
                If RC = RC_OK Then

                    CHECK_SUM = KCV

                    If CHECK_SUM = strKVVData Then
                        Return True
                    Else
                        Return False
                    End If

                Else
                    Return False
                End If

            End If



        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CMD_CALC_CBC_MAC(ByVal bytVar As Byte(), ByVal bytData As Byte()) As Boolean
        Dim tx_packet(99) As Byte
        Dim rx_packet(99) As Byte

        Dim RC As Integer
        Dim intReturnEppRC As Integer
        Dim ptrReturnEppRC As IntPtr
        Dim ptrReturnMAC As IntPtr
        Dim param_len As Integer
        Dim strMAC As String
        Dim MAC As String = ""

        Dim bytLen(1) As Byte
        bytLen(0) = 0
        bytLen(1) = 32

        Try
            tx_packet = MakeCmd(15)
            tx_packet = AppendParam(tx_packet, bytVar, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytLen, CHARACTERS)
            tx_packet = AppendParam(tx_packet, bytData, CHARACTERS)

            rx_packet = SendReceive(tx_packet)

            'get response code from epp
            If RC = RC_OK Then
                ptrReturnEppRC = Marshal.AllocHGlobal(100)
                Marshal.WriteInt16(ptrReturnEppRC, intReturnEppRC)

                RC = Get_parameter_vb(rx_packet, 1, ptrReturnEppRC, param_len)

                If RC = RC_OK Then
                    intReturnEppRC = CDec(Marshal.ReadInt16(ptrReturnEppRC))
                    intReturnEppRC = SwapHighLow(intReturnEppRC)

                    RC = intReturnEppRC
                End If

            End If

            'get decimal MAC
            If RC = RC_OK Then

                RC = Get_parameter_vb(rx_packet, 2, ptrReturnMAC, param_len)

                If RC = RC_OK Then

                    For i As Integer = 10 To 13
                        strMAC = ChangeDecToHexDec(rx_packet(i))

                        If MAC = String.Empty Then
                            MAC = strMAC
                        Else
                            MAC &= strMAC
                        End If
                    Next
                End If

            End If

            If RC = RC_OK Then
                MAC_DATA = MAC
                Return True
            Else
                Return False
            End If

        Catch
            Return False
        End Try
    End Function

    Public Function SwapHighLow(ByVal value As Integer) As Integer
        Dim intResult As Integer

        Try
            intResult = ((value And &HFF) * 256) + (value / 256)
            Return intResult

        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region


#Region "UDF Function"

    Public Function ChangeDecToHexDec(ByVal strValue As Integer) As String
        Dim strHexVal As String = ""
        Dim strReturnHexVal As String = ""

        Try
            strHexVal = Hex(strValue)
            If strHexVal.Length = 1 Then
                strReturnHexVal = "0" & strHexVal
            Else
                strReturnHexVal = strHexVal
            End If
            Return strReturnHexVal
        Catch ex As Exception
            Return strHexVal
        End Try
    End Function

#End Region

End Class
