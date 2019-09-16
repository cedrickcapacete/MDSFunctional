Imports clsKeypad

Public Class clsEPPModuleController

    Dim udtReturnKEYPADHWDSTR As clsKeypad.clsKeypadControl.KEYPADHWDSTR

#Region "Variables"

    Public Shared blnEngineStarted As Boolean = False
    Public Shared blnEngineStopped As Boolean = False
    Public objSagemEPP As clsKeypad.clsKeypadControl


#End Region

#Region "ErrorEvent"

    Public Shared Event EPPControlErrorReceived(ByVal strExMsg As String)

#End Region

#Region "EPP Properties"

    Public Shared strPAN As String = ""
    Public Shared strFormatType As Integer = 0
    Public Shared strPAD As String = ""

    Property getPAN() As String
        Get
            Return strPAN
        End Get
        Set(ByVal value As String)
            strPAN = value
        End Set
    End Property

    Property getPADString() As String
        Get
            Return strPAD
        End Get
        Set(ByVal value As String)
            strPAD = value
        End Set
    End Property

    Property getFormatType() As Integer
        Get
            Return strFormatType
        End Get
        Set(ByVal value As Integer)
            strFormatType = value
        End Set
    End Property

#End Region

    Public Sub New()
        Try
            objSagemEPP = New clsKeypad.clsKeypadControl

        Catch ex As Exception

        End Try
    End Sub

    Public Function InitializeEPP(ByVal strDevice As String) As Boolean
        Dim blnKeypadStatus As Boolean = False

        Try
            'If strDevice = "KEYPAD" Then
            If blnEngineStarted = False Then
                If objSagemEPP.InitKEYPADHWD(strDevice) = True Then
                    blnEngineStarted = True
                    Return True

                Else
                    Return False
                End If
            Else
                Return True
            End If
            'Else
            '    If blnEngineStarted = False Then
            '        If objSagemEPP.InitHSMHWD(strDevice) = True Then
            '            blnEngineStarted = True
            '            Return True

            '        Else
            '            Return False
            '        End If
            '    Else
            '        Return True
            '    End If
            'End If



        Catch ex As Exception
            RaiseEvent EPPControlErrorReceived("Error In Start EPP - " & ex.Message)
            Return False
        End Try
    End Function

    Public Function CloseEPP() As Boolean
        Try
            If blnEngineStarted = True Then
                If objSagemEPP.CloseKEYPADHWD() = True Then
                    blnEngineStarted = False
                    blnEngineStopped = True
                    Return True

                Else
                    Return False
                End If
            Else
                blnEngineStarted = False
                If blnEngineStopped = True Then
                    blnEngineStarted = False
                    Return True
                Else
                    Return False
                End If

            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ReadPIN(ByRef strPINBlock As String) As Boolean

        Try
            If getFormatType = 1 Then
                strPINBlock = objSagemEPP.Send_CmdReadPIN(getFormatType, getPAN, "P11")

            ElseIf getFormatType = 2 Or getFormatType = 3 Or getFormatType = 5 Or getFormatType = 6 Then
                strPINBlock = objSagemEPP.Send_CmdReadPIN(getFormatType, getPADString, "P11")

            ElseIf getFormatType = 4 Then
                strPINBlock = objSagemEPP.Send_CmdReadPIN(getFormatType, getPAN, "P11")
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
