Imports System
Imports System.IO

Public Class clsMultilinkUPS

#Region "UPS Integration Connection"



#Region "Property - Get"

    Property GetUPSStatusCode() As Integer
        Get
            Return UPSStatusCode
        End Get
        Set(ByVal value As Integer)
            UPSStatusCode = value
        End Set
    End Property

#End Region

    Public Function ProcessUPSStatus() As Boolean
        Dim strMultilinkStatus As String = String.Empty

        Try

            'UPSServiceStart = 1
            'UPSCommunicationEstablished = 2
            'UPSServiceStop = 3
            'UPSCommunicationLost = 4
            'UPSBatteryMode = 5
            'UPSPowerSupplyUp = 6


            strMultilinkStatus = MultiLinkReadLogFile()

            'Process the error code
            If strMultilinkStatus.Length > 0 Then
                UPSStatusCode = CInt(strMultilinkStatus)

                Select Case UPSStatusCode
                    Case udtUPSHWDCFG.intUPSCommLost
                        AppLogErr("UPS Status:" & strMultilinkStatus & "- Communication lost")
                        Return False
                    Case udtUPSHWDCFG.intUPSServiceStop
                        AppLogErr("UPS Status:" & strMultilinkStatus & "- Service stop")
                        Return False
                    Case udtUPSHWDCFG.intUPSBatteryMode
                        AppLogWarn("UPS Status:" & strMultilinkStatus & "- Running on battery")
                        Return False
                    Case udtUPSHWDCFG.intUPSPowerSupplyUp, udtUPSHWDCFG.intUPSServiceStart, udtUPSHWDCFG.intUPSCommEstablished
                        AppLogInfo("UPS Status:" & strMultilinkStatus & "- AC Power OK")
                        Return True
                    Case Else
                        AppLogInfo("UPS Status:" & strMultilinkStatus & "- Unknown")
                        Return True
                End Select
            Else
                AppLogInfo("UPS Status Empty String")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in ClsMultilinkUPS.GetUPSStatus:" & ex.Message)
            Return False
        End Try
    End Function

    'Public Function UPSBatteryMode() As Boolean
    '    Try
    '        UPSStatusCode = CInt(MultiLinkReadLogFile())

    '        Select Case UPSStatusCode

    '            Case udtUPSHWDCFG.intUPSBatteryMode
    '                AppLogWarn("UPS Status: Running on battery")
    '                Return True
    '            Case udtUPSHWDCFG.intUPSPowerSupplyUp
    '                AppLogInfo("UPS Status: AC Power OK")
    '                Return False
    '            Case udtUPSHWDCFG.intUPSCommLost
    '                AppLogErr("UPS Status: Communication lost")
    '                Return False
    '            Case udtUPSHWDCFG.intUPSServiceStop
    '                AppLogErr("UPS Status: Service stop")
    '                Return False
    '            Case Else
    '                Return False
    '        End Select

    '    Catch ex As Exception
    '        AppLogErr("Error in UPSBatteryMode:" & ex.Message)
    '        Return False
    '    End Try
    'End Function

    'Public Function UPSACPowerSupplyMode() As Boolean
    '    Try
    '        ErrorCode = CInt(MultiLinkReadLogFile())

    '        Select Case ErrorCode

    '            Case udtUPSHWDCFG.intUPSBatteryMode
    '                Return False
    '            Case udtUPSHWDCFG.intUPSPowerSupplyUp
    '                Return True
    '            Case Else
    '                Return False
    '        End Select

    '    Catch ex As Exception
    '        AppLogErr("Error in UPSACPowerSupplyMode:" & ex.Message)
    '        Return False
    '    End Try
    'End Function

    Private Function MultiLinkReadLogFile() As String
        Dim strLine As String = String.Empty
        Dim strLastLine As String = String.Empty
        Dim strSplitLastLine() As String = Nothing
        Dim logFileStream As System.IO.FileStream = Nothing
        Dim rdr As System.IO.StreamReader = Nothing

        Try

            If File.Exists(udtUPSHWDCFG.strMonitoringPath) = True Then

                logFileStream = New System.IO.FileStream(udtUPSHWDCFG.strMonitoringPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                rdr = New System.IO.StreamReader(logFileStream)

                Do
                    strLine = rdr.ReadLine
                    If Not IsNothing(strLine) Then
                        strLastLine = strLine
                    End If
                Loop Until strLine Is Nothing

                If (strLastLine.Trim.Length > 0) Then
                    strSplitLastLine = strLastLine.Split(udtUPSHWDCFG.strTextSplitter)

                    'UPS Code and Details
                    UPSReplyCode = strSplitLastLine(1)
                    UPSReplyCode = UPSReplyCode.Trim
                    UPSReplyCDDetails = strLastLine.Trim

                    Return strSplitLastLine(1)
                Else
                    Return String.Empty
                End If

            Else
                AppLogWarn("UPS Monitoring File Not Found:" & udtUPSHWDCFG.strMonitoringPath.Trim)
                Return String.Empty
            End If

        Catch ex As Exception
            AppLogErr("Error in Reading Multilink UPS Log File:" & ex.Message)
            Return String.Empty
        Finally
            If (Not IsNothing(logFileStream)) Then
                logFileStream.Dispose()
                logFileStream.Close()
                logFileStream = Nothing
            End If
            If (Not IsNothing(rdr)) Then
                rdr.Dispose()
                rdr.Close()
                rdr = Nothing
            End If
        End Try
    End Function

#End Region

End Class
