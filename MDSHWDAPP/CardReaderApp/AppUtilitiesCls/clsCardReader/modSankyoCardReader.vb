Imports System.Runtime.InteropServices

Module modSankyoCardReader

    Public Const MAX_DATA_ARRAY_SIZE As Integer = 1024

    ' DLL Information Structure
    Const MAX_FNAME As Integer = 256
    Public udtDLLInformation As DLLInformation

    Structure DLLInformation
        Public UpperDll As DllRevFileNmInfoStruct
        Public LowerDll As DllRevFileNmInfoStruct
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Structure DllRevFileNmInfoStruct
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_FNAME)> _
        Public szFileName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public szRevision As String
    End Structure

    'DLL Command Structure

    Public udtCommandStruct As CommandStruct
    Public udtReplyStruct As ReplyData

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure CmdData
        '''DWORD->unsigned int
        Public dwSize As UInteger
        '''LPBYTE->BYTE*
        Public lpbBody As System.IntPtr
    End Structure

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure CommandStruct
        '''BYTE->unsigned char
        Public bCommandCode As Byte
        '''BYTE->unsigned char
        Public bParameterCode As Byte
        '''CmdData
        Public Data As CmdData
    End Structure

    Public Enum REPLY_TYPE
        PositiveReply
        NegativeReply
        ReplyReceivingFailure
        CommandCancellation
        ReplyTimeout
    End Enum

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure pStatusCode
        '''BYTE->unsigned char
        Public bSt1 As Byte
        '''BYTE->unsigned char
        Public bSt0 As Byte
    End Structure

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure pDataReply
        '''DWORD->unsigned int 
        Public dwSize As UInteger
        '''BYTE[]
        <MarshalAs(UnmanagedType.ByValArray, sizeConst:=MAX_DATA_ARRAY_SIZE)> Public bBody() As Byte
    End Structure

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure POSITIVE_REPLY
        '''BYTE->unsigned char
        Public bCommandCode As Byte
        '''BYTE->unsigned char
        Public bParameterCode As Byte
        '''pStatusCode
        Public StatusCode As pStatusCode
        '''DataReply
        Public Data As pDataReply
    End Structure

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure nStatusCode
        '''BYTE->unsigned char
        Public bE1 As Byte
        '''BYTE->unsigned char
        Public bE0 As Byte
    End Structure

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure nDataReply
        '''DWORD->unsigned int
        Public dwSize As UInteger
        '''BYTE[]
        <MarshalAs(UnmanagedType.ByValArray, sizeConst:=MAX_DATA_ARRAY_SIZE)> Public bBody() As Byte
    End Structure

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure NEGATIVE_REPLY
        '''BYTE->unsigned char
        Public bCommandCode As Byte
        '''BYTE->unsigned char
        Public bParameterCode As Byte
        '''nStatusCode
        Public ErrorCode As nStatusCode
        '''nDataReply
        Public Data As nDataReply
    End Structure

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)> _
    Public Structure ReplyMessage
        '''POSITIVE_REPLY->Anonymous_11374f74_a0bb_4368_ac94_3db52380d888
        <System.Runtime.InteropServices.FieldOffsetAttribute(0)> _
        Public positiveReply As POSITIVE_REPLY
        '''NEGATIVE_REPLY->Anonymous_8667a835_401a_48d7_b691_9a7a14093622
        <System.Runtime.InteropServices.FieldOffsetAttribute(0)> _
        Public negativeReply As NEGATIVE_REPLY
    End Structure

    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure ReplyData
        '''REPLY_TYPE->Anonymous_352400db_743c_4bc7_85a7_ac187881ab6e
        Public replyType As REPLY_TYPE
        '''Anonymous_f853f5b4_baad_45e9_861c_57329adab44b
        Public message As ReplyMessage
    End Structure

    Partial Public Class NativeMethods

        '''Return Type: DWORD->unsigned int
        '''param0: LPCSTR->CHAR*
        '''param1: COMMAND->Anonymous_a5fb3e18_44d5_40e7_8e30_626eb0d94df7
        '''param2: DWORD->unsigned int
        '''param3: LPREPLY->Anonymous_4ccde0aa_2c5d_43ec_bde1_211582a9ab11*
        <System.Runtime.InteropServices.DllImportAttribute("ICT3Q8_0171DLL.dll", EntryPoint:="ExecuteCommand")> _
        Public Shared Function ExecuteCommand(<System.Runtime.InteropServices.InAttribute(), System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)> ByVal param0 As String, ByVal param1 As CommandStruct, ByVal param2 As UInteger, ByRef param3 As ReplyData) As UInteger
        End Function

    End Class


    'DLL Function - Get DLL Information
    <DllImport("ICT3Q8_0171DLL.dll")> _
    Public Function GetDllInformation(ByVal dllInfo As IntPtr) As IntPtr
    End Function

    ''DLL Function - Connect Device
    <DllImport("ICT3Q8_0171DLL.dll")> _
    Public Function ConnectDevice(ByVal strComNumber As String, ByVal lngBaudRate As Integer) As Integer
    End Function

    'DLL Function - Disconnect Device
    <DllImport("ICT3Q8_0171DLL.dll")> _
    Public Function DisconnectDevice(ByVal strComNumber As String) As Integer
    End Function

    ''DLL Function - Execute Command
    '<DllImport("ICT3Q8_0171DLL.dll", CallingConvention:=CallingConvention.Cdecl, CharSet:=CharSet.Ansi)> _
    'Public Function ExecuteCommand2(ByVal strComNumber As String, ByVal udtCommandParam As CommandSend, ByVal lngTimeout As Long, ByRef udtReply As IntPtr) As Integer
    'End Function

    'Declare Function ExecuteCommand1 Lib "ICT3Q8_0171DLL.dll" Alias "ExecuteCommand" (ByVal strComNumber As String, ByVal udtCommandParam As CommandStruct, ByVal lngTimeout As Long, ByRef udtReply As IntPtr) As Integer
    'Public Function ExecuteCommand(ByVal strComNumber As String, ByVal strCommand As String, ByVal lngTimeout As Long, ByVal strReply As String) As Integer
    'End Function


End Module
