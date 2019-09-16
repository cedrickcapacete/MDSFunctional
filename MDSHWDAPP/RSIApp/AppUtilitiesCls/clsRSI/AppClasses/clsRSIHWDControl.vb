Imports System
Imports System.IO
Imports clsRSI.clsAppStructure

Public Class clsRSIHWDControl

#Region "RSI COMPORT Object"

    Private WithEvents objComPort1 As Ports.SerialPort
    Private WithEvents objComPort2 As Ports.SerialPort

#End Region

#Region "RSI Control Function - InitRSIHWD,CloseRSIHWD,EnableRSI,DisableRSI,RSITriggerOn"

    Public Function InitRSIHWD() As Boolean
        Try
            If InitRSICommunication() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in InitRSIHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CloseRSIHWD() As Boolean
        Try
            If CloseRSICommunication() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in CloseRSIHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "RSI Light Control"

    Public Function RSIL1TriggerOn() As Boolean
        Try
            If RSILight1TriggerOn() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in RSIL1TriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSIL1TriggerOff() As Boolean
        Try
            If RSILight1TriggerOff() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in RSIL1TriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSIL2TriggerOn() As Boolean
        Try
            If RSILight2TriggerOn() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in RSIL2TriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSIL2TriggerOff() As Boolean
        Try
            If RSILight2TriggerOff() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in RSIL2TriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSIL3TriggerOn() As Boolean
        Try
            If RSILight3TriggerOn() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in RSIL3TriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSIL3TriggerOff() As Boolean
        Try
            If RSILight3TriggerOff() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in RSIL3TriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSIL4TriggerOn() As Boolean
        Try
            If RSILight4TriggerOn() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in RSIL4TriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function RSIL4TriggerOff() As Boolean
        Try
            If RSILight4TriggerOff() = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in RSIL4TriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "RSI Integration Connection"

    Private Function InitRSICommunication() As Boolean
        Dim strTmpComport As String = ""
        Dim blnReply As Boolean = False

        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                If udtRSIHwdCFG.strNumofRSI = "2" Then

                    'Comport RSI1
                    strTmpComport = "COM" & udtRSIHwdCFG.strC1Comport
                    strTmpComport = strTmpComport.Trim
                    objComPort1 = New Ports.SerialPort(strTmpComport, udtRSIHwdCFG.strC1Baudrate, udtRSIHwdCFG.strC1Parity, udtRSIHwdCFG.strC1Databits, udtRSIHwdCFG.strC1Stopbits)
                    'Open 1st comport
                    If (Not IsNothing(objComPort1)) Then
                        If (objComPort1.IsOpen) Then
                            objComPort1.Close()
                        End If
                        objComPort1.Open()
                        AppLogInfo("Open RSI Comport1: " & strTmpComport & " OK")
                    End If

                    'Comport RSI2
                    strTmpComport = "COM" & udtRSIHwdCFG.strC2Comport
                    strTmpComport = strTmpComport.Trim
                    objComPort2 = New Ports.SerialPort(strTmpComport, udtRSIHwdCFG.strC2Baudrate, udtRSIHwdCFG.strC2Parity, udtRSIHwdCFG.strC2Databits, udtRSIHwdCFG.strC2Stopbits)
                    'Open 1st comport
                    If (Not IsNothing(objComPort2)) Then
                        If (objComPort2.IsOpen) Then
                            objComPort2.Close()
                        End If
                        objComPort2.Open()
                        AppLogInfo("Open RSI Comport2: " & strTmpComport & " OK")
                    End If

                    blnReply = True

                Else

                    strTmpComport = "COM" & udtRSIHwdCFG.strC1Comport
                    strTmpComport = strTmpComport.Trim
                    objComPort1 = New Ports.SerialPort(strTmpComport, udtRSIHwdCFG.strC1Baudrate, udtRSIHwdCFG.strC1Parity, udtRSIHwdCFG.strC1Databits, udtRSIHwdCFG.strC1Stopbits)
                    'Open 1st comport
                    If (Not IsNothing(objComPort1)) Then
                        If (objComPort1.IsOpen) Then
                            objComPort1.Close()
                        End If
                        objComPort1.Open()
                        AppLogInfo("Open RSI Comport1: " & strTmpComport & " OK")
                    End If

                    blnReply = True

                End If

                Return blnReply
            Else
                'RSI Disable
                AppLogInfo("RSI is Disable")
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in InitRSICommunication:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function CloseRSICommunication() As Boolean
        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then

                If udtRSIHwdCFG.strNumofRSI = "2" Then

                    If (Not IsNothing(objComPort1)) Then
                        If (objComPort1.IsOpen) Then
                            objComPort1.Close()
                        End If
                        AppLogInfo("Close RSI Comport1 Success")
                    End If


                    If (Not IsNothing(objComPort2)) Then
                        If (objComPort2.IsOpen) Then
                            objComPort2.Close()
                        End If
                        AppLogInfo("Close RSI Comport2 Success")
                    End If

                Else

                    If (Not IsNothing(objComPort1)) Then
                        If (objComPort1.IsOpen) Then
                            objComPort1.Close()
                        End If
                        AppLogInfo("Close RSI Comport1 Success")
                    End If

                End If

                Return True
            Else
                'RSI Disable
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in CloseRSICommunication. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

   
#End Region

#Region "RSI Comport 1 Control"

    'NetworkOk="1,RTS,0"
    'NetworkErr="1,RTS,1"

    'MachineOk="1,DTR,0"
    'MachineErr="1,DTR,1"

    'CardReader="2,DTR,0"
    'CardReader="2,DTR,1"

    'AdviceOk="2,RTS,0"
    'AdviceErr="2,RTS,1"

    Private Function RSILight1TriggerOn() As Boolean

        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                'RSI Light 1 enable - ON
                If (Not IsNothing(objComPort1)) Then
                    objComPort1.RtsEnable = True
                End If
                Return True
            Else
                'RSI disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in RSILight1TriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function RSILight1TriggerOff() As Boolean

        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                'RSI Light1 enable - OFF
                If (Not IsNothing(objComPort1)) Then
                    objComPort1.RtsEnable = False
                End If
                Return True
            Else
                'RSI disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in RSILight1TriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function RSILight2TriggerOn() As Boolean
        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                'RSI Light 2 enable - ON
                If (Not IsNothing(objComPort1)) Then
                    objComPort1.DtrEnable = True
                End If
                Return True
            Else
                'RSI disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in RSILight2TriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function RSILight2TriggerOff() As Boolean

        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                'RSI Light2 enable - OFF
                If (Not IsNothing(objComPort1)) Then
                    objComPort1.DtrEnable = False
                End If
                Return True
            Else
                'RSI disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in RSILight2TriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function RSILight3TriggerOn() As Boolean
        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                'RSI Light 3 enable - ON
                If (Not IsNothing(objComPort2)) Then
                    objComPort2.DtrEnable = True
                End If
                Return True
            Else
                'RSI disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in RSILight3TriggerOn. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function RSILight3TriggerOff() As Boolean

        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                'RSI Light3 enable - OFF
                If (Not IsNothing(objComPort2)) Then
                    objComPort2.DtrEnable = False
                End If
                Return True
            Else
                'RSI disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in RSILight3TriggerOff. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function RSILight4TriggerOn() As Boolean
        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                'RSI Light 4 enable - ON
                If (Not IsNothing(objComPort2)) Then
                    objComPort2.RtsEnable = True
                End If
                Return True
            Else
                'RSI disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in RSILight4TriggerOn.ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Private Function RSILight4TriggerOff() As Boolean

        Try
            If udtRSIHwdCFG.blnEnableRSI = True Then
                'RSI Light4 enable - OFF
                If (Not IsNothing(objComPort2)) Then
                    objComPort2.RtsEnable = False
                End If
                Return True
            Else
                'RSI disabled
                Return True
            End If

        Catch ex As Exception
            AppLogErr("Error in RSILight4TriggerOff.ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function


#End Region


End Class
