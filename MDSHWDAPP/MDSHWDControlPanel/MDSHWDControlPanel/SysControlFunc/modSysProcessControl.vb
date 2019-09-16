Imports System
Imports System.IO


Module modSysProcessControl

#Region "Variable"
    Dim strTitle As String = "modSysProcessControl"
#End Region

#Region "Process Control Function"

    ';RESTART APP EXE PATH
    'RESTARTAPPPAHT = "D:\Hello\Hello.exe|D:\Hello\Hello.exe"
    ';NAME="QCONSOLE"
    'RESTARTAPPNAME = "Hello|Hello"
    ';RESTART WINDISPLAY MODE
    ';1=Maximized 2=Minimized 3=Normal
    'WINDISPLAYMODE = "2|2"
    Public Function RestartProcessAppFunc(ByVal strINRestartPath As String, ByVal strINRestartProcessName As String, ByVal strWinDisplayMode As String) As Boolean
        Dim strProcessPath As String = String.Empty
        Dim strProcessName As String = String.Empty
        Dim strProcessWinMode As String = String.Empty

        Try

            'Restart Process Setting
            strProcessPath = strINRestartPath.Trim
            strProcessName = strINRestartProcessName.Trim
            strProcessWinMode = strWinDisplayMode.Trim

            If File.Exists(strProcessPath) Then
                'Restart Process Start Contorl Logic

                '1. End Process
                AppLogInfo("End Process")
                EndProcessFunc(strProcessName, strProcessPath)

                '2. Start Process
                AppLogInfo("Start Process")
                StartProcessFunc(strProcessPath, strProcessWinMode)

            Else
                'Restart Process Path Not Exists
                AppLogWarn("Restart App Path Not Exists:" & strProcessPath.Trim)
            End If


            Return True
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".RestartProcessAppFunc:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function IsSearchProcessIDExists(ByVal strINEndProcessName_Search As String) As Boolean
        Dim strProcessName As String = String.Empty
        Dim myCProcess As Process()
        Dim intTtlProcess As Integer

        Try

            strProcessName = strINEndProcessName_Search.Trim
            myCProcess = Process.GetProcessesByName(strProcessName)

            intTtlProcess = myCProcess.Length

            AppLogInfo("No of ProcessName:" & strProcessName & " Detected:" & intTtlProcess)

            If intTtlProcess > 0 Then
                Return True
            Else
                Return False
            End If

            myCProcess = Nothing

        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".IsSearchProcessIDExists:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function EndProcessFunc(ByVal strINEndProcessName As String, ByVal strINEndProcessPath As String) As Boolean
        Dim strProcessName As String = String.Empty
        Dim myCProcess As Process()
        Dim intTtlProcess As Integer

        Try

            strProcessName = strINEndProcessName.Trim
            myCProcess = Process.GetProcessesByName(strProcessName)

            intTtlProcess = myCProcess.Length

            AppLogInfo("No of ProcessName:" & strProcessName & " Detected:" & intTtlProcess)

            If intTtlProcess > 0 Then

                While intTtlProcess > 0
                    If myCProcess((intTtlProcess - 1)).ProcessName = strProcessName Then
                        myCProcess((intTtlProcess - 1)).Kill()
                        myCProcess((intTtlProcess - 1)).WaitForExit()
                        myCProcess((intTtlProcess - 1)).Close()
                    End If
                    intTtlProcess = intTtlProcess - 1
                End While

                'End Process Time
                'strCurrTm = TimeOfDay 'String.Format("{0:HH:mm:ss}", DateTime.Now)
                AppLogInfo("End Process:" & strINEndProcessPath & " at:" & TimeOfDay)

            End If

            myCProcess = Nothing

            Return True
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".EndProcessFunc:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function StartProcessFunc(ByVal strINStartProcessPath As String, ByVal strInWinDisplayMode As String) As Boolean
        Dim strProcessFilePath As String = String.Empty
        Dim myProcess As System.Diagnostics.Process = New System.Diagnostics.Process()

        Try

            strProcessFilePath = strINStartProcessPath.Trim

            If strProcessFilePath.Length > 0 Then

                myProcess.StartInfo.FileName = strProcessFilePath

                ';RESTART WINDISPLAY MODE
                ';1=Maximized 2=Minimized 3=Normal
                Select Case strInWinDisplayMode
                    Case "1"
                        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized
                    Case "2"
                        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                    Case "3"
                        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                    Case Else
                        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                End Select

                myProcess.Start()
                AppLogInfo("Start Process:" & strProcessFilePath & " at:" & myProcess.StartTime.ToLongTimeString())
                'myProcess.WaitForExit()
                myProcess.Close()

            End If

            myProcess = Nothing

            Return True
        Catch ex As Exception
            AppLogErr("Error in " & strTitle & ".StartProcessFunc:" & ex.Message)
            Return False
        End Try
    End Function


#End Region

End Module
