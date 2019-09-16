Imports System
Imports System.IO
Imports clsAppLogger.clsAppLoggerControl

Public Class clsMultilinkUPS

#Region "UPS Integration Connection"

    Public ErrorCode As Integer = 0

    Property GetErrorCode() As Integer
        Get
            Return ErrorCode
        End Get
        Set(ByVal value As Integer)
            ErrorCode = value
        End Set
    End Property

    Public Function InitUPSCommunication() As Boolean
        Try
            ErrorCode = CInt(MultiLinkReadLogFile())

            Select Case ErrorCode
                Case udtUPSHWDCFG.intUPSCommLost
                    AppLogErr("UPS Status: Communication lost")
                    Return False
                Case udtUPSHWDCFG.intUPSServiceStop
                    AppLogErr("UPS Status: Service stop")
                    Return False
                Case udtUPSHWDCFG.intUPSBatteryMode
                    AppLogWarn("UPS Status: Running on battery")
                    Return True
                Case udtUPSHWDCFG.intUPSPowerSupplyUp, udtUPSHWDCFG.intUPSServiceStart
                    AppLogInfo("UPS Status: AC Power OK")
                    Return True
                Case Else
                    Return False
            End Select


        Catch ex As Exception
            AppLogErr("Error in InitUPSCommunication:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UPSBatteryMode() As Boolean
        Try
            ErrorCode = CInt(MultiLinkReadLogFile())

            Select Case ErrorCode

                Case udtUPSHWDCFG.intUPSBatteryMode
                    AppLogWarn("UPS Status: Running on battery")
                    Return True
                Case udtUPSHWDCFG.intUPSPowerSupplyUp
                    AppLogInfo("UPS Status: AC Power OK")
                    Return False
                Case udtUPSHWDCFG.intUPSCommLost
                    AppLogErr("UPS Status: Communication lost")
                    Return False
                Case udtUPSHWDCFG.intUPSServiceStop
                    AppLogErr("UPS Status: Service stop")
                    Return False
                Case Else
                    Return False
            End Select

        Catch ex As Exception
            AppLogErr("Error in UPSBatteryMode:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UPSACPowerSupplyMode() As Boolean
        Try
            ErrorCode = CInt(MultiLinkReadLogFile())

            Select Case ErrorCode

                Case udtUPSHWDCFG.intUPSBatteryMode
                    Return False
                Case udtUPSHWDCFG.intUPSPowerSupplyUp
                    Return True
                Case Else
                    Return False
            End Select

        Catch ex As Exception
            AppLogErr("Error in UPSACPowerSupplyMode:" & ex.Message)
            Return False
        End Try
    End Function

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
