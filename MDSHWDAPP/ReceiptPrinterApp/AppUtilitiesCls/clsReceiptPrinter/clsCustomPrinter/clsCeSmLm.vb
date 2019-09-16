Imports System
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Text

Public Class clsCeSmLm

    <DllImport("CeSmLm.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Protected Shared Function CePrnInitCeUsbSI(ByRef dwSysErr As UInt32) As [UInt32]
    End Function

    <DllImport("CeSmLm.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Protected Shared Function CePrnGetStsUsb(ByVal intDev As Integer, ByRef dwSts As UInt32, ByRef dwSysErr As UInt32) As [UInt32]
    End Function
End Class