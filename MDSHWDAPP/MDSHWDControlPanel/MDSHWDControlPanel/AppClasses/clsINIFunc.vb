Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class clsINIFunc

    '    BOOL WritePrivateProfileSection(
    '  LPCTSTR lpAppName,
    '  LPCTSTR lpString,
    '  LPCTSTR lpFileName
    ');

    'BOOL WritePrivateProfileString(
    '  LPCTSTR lpAppName,
    '  LPCTSTR lpKeyName,
    '  LPCTSTR lpString,
    '  LPCTSTR lpFileName
    ');


    Private Declare Auto Function GetPrivateProfileString Lib "kernel32.dll" (ByVal lpSection As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Auto Function GetPrivateProfileSection Lib "kernel32.dll" (ByVal lpSection As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Auto Function WritePrivateProfileString Lib "kernel32.dll" (ByVal lpSection As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Declare Auto Function GetPrivateProfileInt Lib "kernel32.dll" (ByVal lpSection As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpFileName As String) As Integer
    Private Declare Auto Function WritePrivateProfileSection Lib "kernel32.dll" (ByVal lpSection As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    Private mstrIniFileName As String
    Private mstrCurrentSection As String

    Public Property FileName() As String
        Get
            Return mstrIniFileName
        End Get
        Set(ByVal Value As String)
            mstrIniFileName = Value
        End Set
    End Property

    Public Property CurrentSection() As String
        Get
            Return mstrCurrentSection
        End Get
        Set(ByVal Value As String)
            mstrCurrentSection = Value
        End Set
    End Property

    Sub New(ByVal fileName As String, ByVal useAppPath As Boolean)
        mstrIniFileName = fileName
    End Sub

    Public Function GetIntVal(ByVal key As String, ByVal dflt As Integer) As Integer
        Return GetPrivateProfileInt(mstrCurrentSection, key, dflt, mstrIniFileName)
    End Function

    Public Function GetLongVal(ByVal key As String, ByVal dflt As Long) As String
        Dim retstr As String = Space(255)
        Dim retval As Integer = GetPrivateProfileString(mstrCurrentSection, key, "", retstr, retstr.Length, mstrIniFileName)
        retstr = Mid(retstr, 1, retstr.IndexOf(Chr(0)))
        Dim lngVal As Long = dflt
        If (Len(Trim(retstr)) > 0) Then lngVal = CLng(retstr)
        Return lngVal
    End Function

    Public Function GetDblVal(ByVal key As String, ByVal dflt As Double) As String
        Dim retstr As String = Space(255)
        Dim retval As Integer = GetPrivateProfileString(mstrCurrentSection, key, "", retstr, retstr.Length, mstrIniFileName)
        retstr = Mid(retstr, 1, retstr.IndexOf(Chr(0)))
        Dim dblVal As Double = dflt
        If (Len(Trim(retstr)) > 0) Then dblVal = CDbl(retstr)
        Return dblVal
    End Function

    Public Function GetStrVal(ByVal key As String, ByVal dflt As String) As String
        Dim retstr As String = Space(255)
        Dim retval As Integer = GetPrivateProfileString(mstrCurrentSection, key, dflt, retstr, retstr.Length, mstrIniFileName)
        Return Mid(retstr, 1, retstr.IndexOf(Chr(0)))
    End Function

    Public Function GetBoolVal(ByVal key As String, ByVal dflt As Boolean) As String
        Dim retstr As String = Space(255)
        Dim dfltBool As String = IIf(dflt, "1", "0")
        Dim retval As Integer = GetPrivateProfileString(mstrCurrentSection, key, dfltBool, retstr, retstr.Length, mstrIniFileName)
        Dim strVal As String = Mid(retstr, 1, retstr.IndexOf(Chr(0)))
        Return IIf(Trim(strVal) = "0", False, True)
    End Function

    Public Sub SetVal(ByVal key As String, ByVal val As String)
        WritePrivateProfileString(mstrCurrentSection, key, val, mstrIniFileName)
    End Sub

    Public Sub SetVal(ByVal key As String, ByVal val As Integer)
        WritePrivateProfileString(mstrCurrentSection, key, val.ToString, mstrIniFileName)
    End Sub

    Public Sub SetVal(ByVal key As String, ByVal val As Long)
        WritePrivateProfileString(mstrCurrentSection, key, val.ToString, mstrIniFileName)
    End Sub

    Public Sub SetVal(ByVal key As String, ByVal val As Double)
        WritePrivateProfileString(mstrCurrentSection, key, val.ToString, mstrIniFileName)
    End Sub

    Public Sub DelSection()
        WritePrivateProfileSection(mstrCurrentSection, vbNullString, mstrIniFileName)
    End Sub

    Public Sub WriteSection(ByVal val As String)
        WritePrivateProfileSection(mstrCurrentSection, val, mstrIniFileName)
    End Sub

    Public Sub DeleteSection()
        WritePrivateProfileSection(mstrCurrentSection, vbNullString, mstrIniFileName)
    End Sub

    Public Sub DeleteSection(ByVal SectionName As String)
        mstrCurrentSection = SectionName
        WritePrivateProfileSection(mstrCurrentSection, vbNullString, mstrIniFileName)
    End Sub

    Public Sub DeleteKey(ByVal key As String)
        WritePrivateProfileString(mstrCurrentSection, key, vbNullString, mstrIniFileName)
    End Sub



End Class
