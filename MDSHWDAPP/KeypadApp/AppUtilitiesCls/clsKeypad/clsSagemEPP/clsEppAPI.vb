Imports System
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Text

Module clsEppAPI

    Structure app_packet_header
        '<MarshalAs(UnmanagedType.I4)> Public app_packet_length As Integer
        '<MarshalAs(UnmanagedType.I4)> Public app_command As Integer
        Public app_packet_length As Short
        Public app_command As Short
    End Structure

    'Dim APP_ As app_packet_header

#Region "Comport connection configuration"

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Function OpenComPort(ByVal strComNumber As String, ByVal strBaudRate As String) As IntPtr
    End Function

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Function GetHandle(ByVal strComNumber As Integer) As IntPtr
    End Function

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Function CloseComPort(ByVal strComHandler As IntPtr) As Short
    End Function

#End Region


#Region "Packet Handling"

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Sub Make_command(ByRef packet_adress As app_packet_header, ByVal command As Short)
    End Sub

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Sub Make_command_vb(ByVal packet_adress As IntPtr, ByVal command As UShort)
    End Sub

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Sub Append_parameter(ByRef packet_adress As app_packet_header, ByVal parameter_adress As IntPtr, ByVal parameter_length As Integer, ByVal type As Integer)
    End Sub

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Sub Append_parameter_vb(ByVal packet_adress As IntPtr, ByVal parameter_adress As IntPtr, ByVal parameter_length As Integer, ByVal type As Integer)
    End Sub

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Function Send_receive(ByVal Cmd As Byte(), ByVal Resp As Byte(), ByVal MaxLength As Short, ByVal hCom As IntPtr) As Short
    End Function

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Function Get_command(ByRef app_packet As app_packet_header) As Short
    End Function

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Function Get_parameter(ByRef pck_adr As app_packet_header, ByVal par_num As Integer, ByVal par_adr As IntPtr, ByRef par_length As Integer) As Integer
    End Function

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Function Get_parameter_vb(ByVal pck_adr As Byte(), ByVal par_num As Integer, ByRef par_adr As IntPtr, ByRef par_length As Integer) As Integer
    End Function

#End Region


#Region "Handling Assynchronous Event"

    <System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)>
    Public Delegate Sub async_event_func_pointer(ByVal param0 As System.IntPtr, ByVal param1 As System.IntPtr)

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Sub SetAsync_event_func(ByVal lpasync_event_func As async_event_func_pointer)
    End Sub

    <DllImport("EPPAPI_1.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Function GetAsync_event_func() As async_event_func_pointer
    End Function

#End Region

    <DllImport("EPPAPI_1.dll")> _
    Public Function GetSwVersion() As String
    End Function

End Module
