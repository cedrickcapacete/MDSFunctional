Imports System
Imports System.IO

Module modAppFunc


#Region "Get Main System Folder Control"

    Public Function GetMainSystemPathControl() As String
        Dim strMainAppDrive As String = String.Empty

        Try
            'GET the System Main Dirve Folder and Backup
            strMainAppDrive = Environment.GetEnvironmentVariable(EnvironmentVariableName)

            strMainAppDrive = strMainAppDrive.Trim

            If strMainAppDrive.Length = 0 Then
                strMainAppDrive = strDefaultMainINIDirectoryPath
            End If

            Return strMainAppDrive
        Catch ex As Exception
            Return strDefaultMainINIDirectoryPath
        End Try
    End Function

#End Region

End Module
