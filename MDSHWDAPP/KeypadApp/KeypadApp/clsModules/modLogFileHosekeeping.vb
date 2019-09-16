Imports System
Imports System.IO

Module modLogFileHosekeeping


    Private strTitle As String = "modLogFileHousekeeping"

#Region "Housekeeping Option - AutoHousekeepingLog "

    Public Sub AutoHousekeepingLog()

        Dim strErrMsg As String = String.Empty
        Dim strFolders() As String
        Dim strFolder As String = String.Empty
        Dim strSubFolder As String = String.Empty
        Dim strLogPath As String = String.Empty
        Dim intLastIdx As Integer
        Dim intLen As Integer
        Dim intTmpYr As Integer
        Dim intTmpMth As Integer
        Dim intTmpDay As Integer

        Dim dtmStart As DateTime
        Dim intRecCnt As Integer

        Dim dblDay As Double
        Dim intttlhskcnt As Integer

        Try


            If objAppHSKCfg.udtAppHSKCfg.blnHSKLogMode = True Then
                'Auto LogFile Housekeeping Checking -Enable Auto Housekeeping

                dblDay = objAppHSKCfg.udtAppHSKCfg.dblHSKLogDay

                'get Log file Day
                'dtmStart = DateTime.Today.AddMonths(intMth * -1)
                'Log Housekeeping Period
                dtmStart = DateTime.Today.AddDays(dblDay * -1)

                'Log Events
                AppLogInfo("AutoHSKLog: HskDate Delete Log File before:" & dtmStart.Date & " Housekeeping routines day:" & dblDay.ToString)

                'get Log file Path
                strLogPath = objAppHSKCfg.udtAppHSKCfg.HSKLogFilePath.Trim

                If (strLogPath.Length > 0) Then
                    If (Directory.Exists(strLogPath)) Then

                        'Progress Bar Value
                        intttlhskcnt = 0
                        intRecCnt = 1

                        strFolders = Directory.GetDirectories(strLogPath)

                        For Each strFolder In strFolders

                            intLastIdx = strFolder.LastIndexOf("\") + 1
                            intLen = strFolder.Length
                            strSubFolder = strFolder.Substring(intLastIdx, intLen - intLastIdx)

                            'Folder Length must be eight ddmmyyyy
                            If (strSubFolder.Length = 8) Then
                                If (IsNumeric(strSubFolder)) Then
                                    intTmpYr = CInt(strSubFolder.Substring(4, 4))
                                    intTmpMth = CInt(strSubFolder.Substring(2, 2))
                                    intTmpDay = CInt(strSubFolder.Substring(0, 2))
                                    Dim dtm1 As DateTime = New DateTime(dtmStart.Year, dtmStart.Month, dtmStart.Day)
                                    Dim dtm2 As DateTime = New DateTime(intTmpYr, intTmpMth, intTmpDay)
                                    If (dtm2 < dtm1) Then
                                        'Delete the Log Folder
                                        Directory.Delete(strFolder, True)
                                        intttlhskcnt = intttlhskcnt + 1
                                    End If
                                End If

                            End If

                            intRecCnt = intRecCnt + 1
                        Next

                        'Log Events
                        AppLogInfo("AutoHSKLog:Total Log Folder Deleted:" & intttlhskcnt.ToString)

                    Else
                        'Log Events
                        AppLogErr("AutoHSKLogErr:Log file folder not found.")
                    End If
                End If

            Else
                'Log Events
                AppLogInfo("Auto Log Housekeeping Disable - Manual Housekeeping")
            End If


        Catch ex As Exception
            strErrMsg = "Error in AutoHousekeepingLog."
            strErrMsg += ControlChars.NewLine & ex.Message
            MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
        End Try
    End Sub

#End Region


End Module
