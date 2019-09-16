Imports System
Imports System.Runtime.InteropServices

Public Class clsPCControl

    ' Constants
    Public Const SE_PRIVILEGE_ENABLED As Integer = &H2
    Public Const TOKEN_QUERY As Integer = &H8
    Public Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
    Public Const SE_SHUTDOWN_NAME As String = "SeShutdownPrivilege"

    ' Exit Windows Constants
    Public Const EWX_LOGOFF As Integer = &H0
    Public Const EWX_SHUTDOWN As Integer = &H1
    Public Const EWX_REBOOT As Integer = &H2
    Public Const EWX_FORCE As Integer = &H4
    Public Const EWX_POWEROFF As Integer = &H8
    Public Const EWX_FORCEIFHUNG As Integer = &H10

    'Structure
    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure Luid
        Public Count As Integer
        Public Luid As Long
        Public Attr As Integer
    End Structure 'TokPriv1Luid

    <DllImport("user32.dll", ExactSpelling:=True)> _
    Public Shared Function LockWorkStation() As Boolean
    End Function

    ' Get Current Processes
    <DllImport("kernel32.dll", ExactSpelling:=True)> _
    Public Shared Function GetCurrentProcess() As IntPtr
    End Function

    ' Open Process Token
    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function OpenProcessToken(ByVal h As IntPtr, ByVal acc As Integer, ByRef phtok As IntPtr) As Boolean
    End Function

    ' Look up Priviledge Value
    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function LookupPrivilegeValue(ByVal host As String, ByVal name As String, ByRef pluid As Long) As Boolean
    End Function

    ' Adjust Token Priviledges
    <DllImport("advapi32.dll", ExactSpelling:=True, SetLastError:=True)> _
    Public Shared Function AdjustTokenPrivileges(ByVal htok As IntPtr, ByVal disall As Boolean, ByRef newst As Luid, ByVal len As Integer, ByVal prev As IntPtr, ByVal relen As IntPtr) As Boolean
    End Function

    ' Exit Windows
    <DllImport("user32.dll", ExactSpelling:=True, SetLastError:=True)> _
    Public Shared Function ExitWindowsEx(ByVal flg As Integer, ByVal rea As Integer) As Boolean
    End Function

    ' Exit Windows Sub
    Private Shared Sub DoExitWindows(ByVal flg As Integer)
        Dim tp As Luid
        Dim hproc As IntPtr = GetCurrentProcess()
        Dim htok As IntPtr = IntPtr.Zero
        OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, htok)
        tp.Count = 1
        tp.Luid = 0
        tp.Attr = SE_PRIVILEGE_ENABLED
        LookupPrivilegeValue(Nothing, SE_SHUTDOWN_NAME, tp.Luid)
        AdjustTokenPrivileges(htok, False, tp, 0, IntPtr.Zero, IntPtr.Zero)
        ExitWindowsEx(flg, 0)
    End Sub

    ' Shutdown
    Public Shared Sub Shutdown()
        DoExitWindows(EWX_SHUTDOWN)
    End Sub

    ' Restart
    Public Shared Sub Restart()
        DoExitWindows(EWX_REBOOT Or EWX_FORCE)
    End Sub

    ' Log off
    Public Shared Sub LogOff()
        DoExitWindows(EWX_LOGOFF)
    End Sub

    ' Lock Workstation
    Public Shared Sub LockTheComputer()
        LockWorkStation()
    End Sub



End Class
