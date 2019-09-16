Imports System
Imports System.IO
Imports clsBeeperHWD.clsAppStructure

Public Class clsBeeperHWDControl

#Region "Beeper COMPORT Object"

    Private WithEvents objComPort As Ports.SerialPort

#End Region

#Region "BEEPER Control Function - InitBEEPERHWD,CloseBEEPERHWD,EnableBeeper,DisableBeeper,BeeperTriggerOn"

    Public Function InitBEEPERHWD() As Boolean
        Try
            If InitBeeperCommunication() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in InitBEEPERHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CloseBEEPERHWD() As Boolean
        Try
            If CloseBeeperCommunication() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in CloseBEEPERHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function BeeperTriggerOn() As Boolean
        Try
            If TriggerOn() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in BeeperTriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function BeeperTriggerOff() As Boolean
        Try
            If TriggerOff() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in BeeperTriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "BEEPER Integration Connection"

    Private Function InitBeeperCommunication() As Boolean
        Dim strTmpComport As String = String.Empty

        Try
            If udtBeeperHwdCFG.blnEnableBeeper = True Then

                strTmpComport = "COM" & udtBeeperHwdCFG.strComport
                strTmpComport = strTmpComport.Trim
                objComPort = New Ports.SerialPort(strTmpComport, udtBeeperHwdCFG.strBaudrate, udtBeeperHwdCFG.strParity, udtBeeperHwdCFG.strDatabits, udtBeeperHwdCFG.strStopbits)
                'Open 1st comport
                If (Not IsNothing(objComPort)) Then
                    If (objComPort.IsOpen) Then
                        objComPort.Close()
                    End If
                    objComPort.Open()
                    AppLogInfo("Open Beeper Comport: " & strTmpComport & " OK")
                End If

                Return True
            Else
                'Beeper Disable
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in InitBeeperCommunication:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CloseBeeperCommunication() As Boolean
        Try
            If udtBeeperHwdCFG.blnEnableBeeper = True Then
                If (Not IsNothing(objComPort)) Then
                    If (objComPort.IsOpen) Then
                        objComPort.Close()
                    End If
                    AppLogInfo("Close Beeper Comport Success")
                End If
                Return True
            Else
                'Beeper Disable
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in CloseBeeperCommunication. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function TriggerOn() As Boolean

        Try
            If udtBEEPERHWDCFG.blnEnableBeeper = True Then
                'Beeper enable - ON
                If (Not IsNothing(objComPort)) Then
                    objComPort.RtsEnable = True
                End If
                Return True
            Else
                'Beeper disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in TriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function TriggerOff() As Boolean

        Try
            If udtBEEPERHWDCFG.blnEnableBeeper = True Then
                'Beeper enable - OFF
                If (Not IsNothing(objComPort)) Then
                    objComPort.RtsEnable = False
                End If
                Return True
            Else
                'Beeper disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in TriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


End Class
