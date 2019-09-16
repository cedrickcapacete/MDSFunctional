Imports System.Drawing
Imports System.Drawing.Printing

Imports clsAppLogger.clsAppLoggerControl
Imports clsAppLogger.clsAppLoggerControl.LoggerMsgTypeEnum

Public Class clsCustomPrinter
    Inherits clsCeSmLm

#Region "Printer Integration API"

    Public Function InitPrinter() As Boolean
        Dim blnResult As Boolean
        Try
            If InitPrinterConnection() Then
                If PrinterGetStatus() Then
                    blnResult = True
                Else
                    blnResult = False
                End If
            Else
                blnResult = False
            End If

            Return blnResult

        Catch ex As Exception
            AppLogErr("Error in InitPrinter:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function InitPrinterConnection() As Boolean
        Dim dwReturn As UInt32
        Dim dwSysError As UInt32

        With udtRECEIPTPRINTERHWDCFG
            .blnPrinterStatus = False
            .blnPaperEnd = False
            .blnPaperJam = False
            .blnPaperLow = False
            .blnPaperOK = False
        End With

        Try
            udtRECEIPTPRINTERHWDCFG.strPrinterModel = udtRECEIPTPRINTERSETTINGCFG.DefaultCustomPrinterName

            dwReturn = CePrnInitCeUsbSI(dwSysError)

            AppLogInfo("InitPrinterConnection dwReturn Value =" & dwReturn)

            If dwReturn > 0 Then
                udtRECEIPTPRINTERHWDCFG.blnPrinterStatus = True
                AppLogInfo("Printer Status: Online")
                Return True
            Else
                udtRECEIPTPRINTERHWDCFG.blnPrinterStatus = False
                AppLogInfo("Printer Status: Offline")
                Return False
            End If


        Catch ex As Exception
            AppLogErr("Error in InitPrinterConnection:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function PrinterGetStatus() As Boolean
        Dim dwReturn As UInt32
        Dim dwStatus As UInt32
        Dim dwSysError As UInt32
        Dim intDev As Integer
        Dim strErrCode As String = ""

        Dim strdwReturn As String = ""
        Dim strdwStatus As String = ""


        Try
            'ResetError()
            dwReturn = CePrnGetStsUsb(intDev, dwStatus, dwSysError)

            'CeSmLm.dll value
            strdwReturn = dwReturn.ToString
            strdwStatus = dwStatus.ToString

            AppLogInfo("PrinterGetStatus dwReturn Value=" & strdwReturn)
            AppLogInfo("PrinterGetStatus dwStatus Value=" & strdwStatus)

            'Check Printer Status
            If CheckPrinterStatus(strdwReturn, udtRECEIPTPRINTERSETTINGCFG.strPrintDWValue) = True Then

                If udtRECEIPTPRINTERSETTINGCFG.blnBypassUnknownStatus = False Then
                    'Printer Error - Unknown
                    strErrCode = "004"
                    GetPrinterStatusDesc(strErrCode)
                Else
                    'Printer Error - Unknown - Bypass
                    strErrCode = "000"
                    GetPrinterStatusDesc(strErrCode)
                End If

                Return True

            Else

                'Printer Status
                If CheckPrinterStatus(strdwStatus, udtRECEIPTPRINTERSETTINGCFG.strPaperOKCD) = True Then
                    'Paper Ok
                    strErrCode = "000"
                    GetPrinterStatusDesc(strErrCode)
                    Return True
                ElseIf CheckPrinterStatus(strdwStatus, udtRECEIPTPRINTERSETTINGCFG.strPaperJamCD) = True Then
                    'Paper Jammed
                    strErrCode = "001"
                    GetPrinterStatusDesc(strErrCode)
                    Return True
                ElseIf CheckPrinterStatus(strdwStatus, udtRECEIPTPRINTERSETTINGCFG.strPaperOutCD) = True Then
                    'Paper Out
                    strErrCode = "002"
                    GetPrinterStatusDesc(strErrCode)
                    Return True
                ElseIf CheckPrinterStatus(strdwStatus, udtRECEIPTPRINTERSETTINGCFG.strPaperLowCD) = True Then
                    'Paper Low
                    strErrCode = "003"
                    GetPrinterStatusDesc(strErrCode)
                    Return True
                Else
                    If udtRECEIPTPRINTERSETTINGCFG.blnBypassUnknownStatus = False Then
                        'Printer Error - Unknown
                        strErrCode = "004"
                        GetPrinterStatusDesc(strErrCode)
                    Else
                        'Printer Error - Unknown - Bypass
                        strErrCode = "000"
                        GetPrinterStatusDesc(strErrCode)
                    End If

                    Return True

                End If
            End If


                'If dwReturn = 280 Then
                '    strErrCode = "004"
                '    GetPrinterStatusDesc(strErrCode)
                '    'Return False
                '    Return True
                'Else

                '    Select Case dwStatus

                '        Case 8388608, 2147483648, 0

                '            'Paper Ok
                '            strErrCode = "000"
                '            GetPrinterStatusDesc(strErrCode)
                '            Return True

                '        Case 8388609, 1, 419430, 4194336

                '            'Paper Jammed
                '            strErrCode = "001"
                '            GetPrinterStatusDesc(strErrCode)
                '            'Return False
                '            Return True

                '        Case 8388609, 1, 5, 2147483649

                '            'Paper Out
                '            strErrCode = "002"
                '            GetPrinterStatusDesc(strErrCode)
                '            'Return False
                '            Return True

                '        Case 8388612, 4, 2147483680, 2158679008

                '            'Paper Low
                '            strErrCode = "003"
                '            GetPrinterStatusDesc(strErrCode)
                '            Return True

                '        Case Else

                '            If udtRECEIPTPRINTERSETTINGCFG.blnBypassUnknownStatus = False Then
                '                'Printer Error - Unknown
                '                strErrCode = "004"
                '                GetPrinterStatusDesc(strErrCode)
                '            Else
                '                'Printer Error - Unknown - Bypass
                '                strErrCode = "000"
                '                GetPrinterStatusDesc(strErrCode)
                '            End If
                '            Return True

                '    End Select

                'End If


        Catch ex As Exception
            AppLogErr("Error in PrinterGetStatus:" & ex.Message)
            Return False
        End Try
    End Function

    Private Sub GetPrinterStatusDesc(ByVal strErrorCode As String)

        Select Case strErrorCode
            Case "000"
                udtRECEIPTPRINTERHWDCFG.blnPaperOK = True
                AppLogInfo("Paper Status: Paper OK")

            Case "001"
                udtRECEIPTPRINTERHWDCFG.blnPaperJam = True
                AppLogWarn("Paper Status: Paper Jammed")

            Case "002"
                udtRECEIPTPRINTERHWDCFG.blnPaperEnd = True
                AppLogWarn("Paper Status: Paper Out")

            Case "003"
                udtRECEIPTPRINTERHWDCFG.blnPaperLow = True
                AppLogWarn("Paper Status: Paper Low")

            Case "004"
                udtRECEIPTPRINTERHWDCFG.blnPrinterFail = True
                AppLogWarn("Paper Status: Printer Error")

            Case Else
                udtRECEIPTPRINTERHWDCFG.blnPaperOK = True
                AppLogWarn("Paper Status: Paper OK - Unknown State")
        End Select
    End Sub


    Private Function CheckPrinterStatus(ByVal strInput As String, ByVal strCompareValue As String) As Boolean
        Dim ArrReply() As String = Nothing
        Dim intindex As Integer = 0
        Dim strTempValue As String = ""
        Dim blnReply As Boolean = False

        Try

            AppLogInfo("CheckPrinterStatus InputValue=" & strInput.Trim)
            'AppLogInfo("CheckPrinterStatus CompareValue=" & strCompareValue.Trim)

            strCompareValue = strCompareValue.Trim

            ArrReply = Split(strCompareValue, ",", -1, CompareMethod.Text)

            For intindex = 0 To ArrReply.Length - 1
                strTempValue = ArrReply(intindex)
                If strTempValue = strInput Then
                    AppLogInfo("Printer Status Value Found=" & strTempValue)
                    Return True
                End If
            Next

            Return False
        Catch ex As Exception
            AppLogErr("Error in CheckPrinterStatus:" & ex.Message)
            Return False
        End Try
    End Function






#End Region

#Region "Set Y"

    Public m_intCurrY As Integer

    Public Function SetCurrentY(ByVal intHeight As Single) As Integer
        m_intCurrY += intHeight
        Return m_intCurrY
    End Function

    Property CurrYInfo() As Integer
        Get
            Return m_intCurrY
        End Get
        Set(ByVal value As Integer)
            m_intCurrY = value
        End Set
    End Property

#End Region

#Region "Printer Function Utilities"

    Public Sub PrintText(ByVal MyGraphic As Graphics, ByVal strText As String, ByVal strFontName As String, ByVal dblFontSize As Short, ByVal bFontBold As String, ByVal bFontItalic As String, ByVal intAlign As Integer, ByVal GetLeftMargin As Integer, ByVal GetRightMargin As Integer, ByVal GetPrinterWidth As Integer, ByVal GetPrinterHeight As Integer)

        Dim MyPrintFont As Font = New Font(strFontName, dblFontSize)
        Dim MyBrush As Brush = New SolidBrush(Color.Black)
        Dim MyStringFormat As New StringFormat()

        Dim x As Integer = 0
        Dim strTextArray(0) As String
        Dim strTextArrange As String = String.Empty
        Dim strTmpTextArrange As String = String.Empty
        Dim bolNewLine As Boolean = False

        If bFontBold = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Bold)
        If bFontItalic = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Italic)

        If intAlign = 0 Then
            Dim SplitText() As String
            Dim strText1 As String = String.Empty
            Dim strText2 As String = String.Empty
            Dim bContinue As Boolean = False
            If strText.Length > 50 Then
                SplitText = Split(strText)

                For i As Short = 0 To UBound(SplitText)
                    If ((strText1.Length + SplitText(i).Length) > 50) Then
                        strText2 &= SplitText(i) & " "
                        bContinue = True
                    Else
                        If bContinue = True Then
                            strText2 &= SplitText(i) & " "
                        Else
                            strText1 &= SplitText(i) & " "
                        End If
                    End If
                Next

                MyGraphic.DrawString(strText1, MyPrintFont, MyBrush, GetLeftMargin, m_intCurrY)
                SetCurrentY(MyPrintFont.Height)
                strText = strText2

                For i As Short = 0 To UBound(SplitText)
                    If Not ((strTextArrange.Length + SplitText(i).Length) > 50) Then
                        If bolNewLine Then
                            strTextArrange = strTmpTextArrange
                            bolNewLine = False
                        End If

                        If strTextArrange = String.Empty Then
                            strTextArrange = SplitText(i).Trim
                        Else
                            strTextArrange = strTextArrange & " " & SplitText(i).Trim
                        End If
                    Else
                        bolNewLine = True
                        strTmpTextArrange = SplitText(i).Trim

                        ReDim Preserve strTextArray(x)
                        strTextArray(x) = strTextArrange
                        strTextArrange = String.Empty
                        x = x + 1

                    End If
                Next

                If strTextArray(x - 1) <> strTextArrange Then
                    ReDim Preserve strTextArray(x)
                    strTextArray(x) = strTextArrange
                    x = x + 1
                End If

                For x = 0 To strTextArray.Length - 1
                    MyGraphic.DrawString(strTextArray(x), MyPrintFont, MyBrush, GetLeftMargin, m_intCurrY)
                    SetCurrentY(MyPrintFont.Height)
                Next
            Else
                MyGraphic.DrawString(strText, MyPrintFont, MyBrush, GetLeftMargin, m_intCurrY)
                SetCurrentY(MyPrintFont.Height)
            End If
        Else
            If intAlign = 1 Then             'center
                MyStringFormat.Alignment = StringAlignment.Center
            ElseIf intAlign = 2 Then         'left
                MyStringFormat.Alignment = StringAlignment.Near
            ElseIf intAlign = 3 Then         'right
                MyStringFormat.Alignment = StringAlignment.Far
            End If
            Dim MyRect As Rectangle = New Rectangle(GetLeftMargin, m_intCurrY, GetPrinterWidth - GetRightMargin, GetPrinterHeight)
            MyGraphic.DrawString(strText, MyPrintFont, MyBrush, MyRect, MyStringFormat)
        End If

        SetCurrentY(MyPrintFont.Height)
        strTextArray = Nothing

    End Sub

    Public Sub PrintText2(ByVal MyGraphic As Graphics, ByVal strText1 As String, ByVal strText2 As String, ByVal intMiddleX As Integer, ByVal intLeftMargin As Integer, ByVal strFontName As String, ByVal dblFontSize As Short, ByVal bFontBold As String, ByVal bFontItalic As String)

        Dim MyPrintFont As Font = New Font(strFontName, dblFontSize)
        Dim MyBrush As Brush = New SolidBrush(Color.Black)
        Dim MyStringFormat As New StringFormat()

        MyPrintFont = New Font(MyPrintFont, FontStyle.Regular)
        If bFontBold = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Bold)
        If bFontItalic = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Italic)

        MyGraphic.DrawString(strText1, MyPrintFont, MyBrush, intLeftMargin, m_intCurrY)

        Dim SplitText() As String
        Dim strText2a As String = String.Empty
        Dim strText2b As String = String.Empty
        Dim strText2c As String = String.Empty
        Dim strText2d As String = String.Empty
        'Dim b24 As Boolean = False
        'Dim b48 As Boolean = False
        Dim TempPosition As Integer = 1

        If strText2.Length > 22 Then
            SplitText = Split(strText2)
            For i As Short = 0 To UBound(SplitText)
                Select Case TempPosition
                    Case 1
                        If strText2a.Length <= 24 Then
                            strText2a &= SplitText(i) & " "
                        Else
                            TempPosition = 2
                            strText2b &= SplitText(i) & " "
                        End If
                    Case 2
                        If strText2b.Length <= 24 Then
                            strText2b &= SplitText(i) & " "
                        Else
                            TempPosition = 3
                            strText2c &= SplitText(i) & " "
                        End If
                    Case 3
                        If strText2c.Length <= 24 Then
                            strText2c &= SplitText(i) & " "
                        Else
                            TempPosition = 4
                            strText2c &= SplitText(i) & " "
                        End If
                    Case 4
                        If strText2d.Length <= 24 Then
                            strText2d &= SplitText(i) & " "
                        End If
                End Select
            Next

            MyGraphic.DrawString(strText2a, MyPrintFont, MyBrush, intMiddleX, m_intCurrY)
            SetCurrentY(MyPrintFont.Height)
            If strText2b <> "" Then
                MyGraphic.DrawString("   " & strText2b, MyPrintFont, MyBrush, intMiddleX, m_intCurrY)
                SetCurrentY(MyPrintFont.Height)
            End If
            If strText2c <> "" Then
                MyGraphic.DrawString("   " & strText2c, MyPrintFont, MyBrush, intMiddleX, m_intCurrY)
                SetCurrentY(MyPrintFont.Height)
            End If
            strText2 = "    " & strText2d
        End If
        If strText2.Trim <> "" Then
            MyGraphic.DrawString(strText2, MyPrintFont, MyBrush, intMiddleX, m_intCurrY)
            SetCurrentY(MyPrintFont.Height)
        End If

    End Sub

    Public Sub PrintNoteDenomination(ByVal MyGraphic As Graphics, ByVal strRM2 As String, ByVal intRM2Count As Integer, ByVal strRM5 As String, ByVal intRM5Count As Integer, ByVal strRM10 As String, ByVal intRM10Count As Integer, ByVal strRM20 As String, ByVal intRM20Count As Integer, ByVal strRM50 As String, ByVal intRM50Count As Integer, ByVal strRM100 As String, ByVal intRM100Count As Integer, ByVal int1stColumnX As Integer, ByVal int2ndColumnX As Integer, ByVal int3rdColumnX As Integer, ByVal strFontName As String, ByVal dblFontSize As Short, ByVal bFontBold As String, ByVal bFontItalic As String)
        Dim MyPrintFont As Font = New Font(strFontName, dblFontSize)
        Dim MyBrush As Brush = New SolidBrush(Color.Black)
        Dim MyStringFormat As New StringFormat()
        Dim NoteRm2 As String = strRM2 & "  = " & CStr(intRM2Count)
        Dim NoteRm5 As String = strRM5 & "  = " & CStr(intRM5Count)
        Dim NoteRm10 As String = strRM10 & "  = " & CStr(intRM10Count)
        Dim NoteRm20 As String = strRM20 & "= " & CStr(intRM20Count)
        Dim NoteRm50 As String = strRM50 & "= " & CStr(intRM50Count)
        Dim NoteRm100 As String = strRM100 & "= " & CStr(intRM100Count)

        MyPrintFont = New Font(MyPrintFont, FontStyle.Regular)
        If bFontBold = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Bold)
        If bFontItalic = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Italic)

        MyGraphic.DrawString(NoteRm2, MyPrintFont, MyBrush, int1stColumnX, m_intCurrY)
        MyGraphic.DrawString(NoteRm5, MyPrintFont, MyBrush, int2ndColumnX, m_intCurrY)
        MyGraphic.DrawString(NoteRm10, MyPrintFont, MyBrush, int3rdColumnX, m_intCurrY)
        SetCurrentY(MyPrintFont.Height)

        MyGraphic.DrawString(NoteRm20, MyPrintFont, MyBrush, int1stColumnX, m_intCurrY)
        MyGraphic.DrawString(NoteRm50, MyPrintFont, MyBrush, int2ndColumnX, m_intCurrY)
        MyGraphic.DrawString(NoteRm100, MyPrintFont, MyBrush, int3rdColumnX, m_intCurrY)
        SetCurrentY(MyPrintFont.Height)
    End Sub

    Public Sub PrintBody1(ByVal MyGraphic As Graphics, ByVal strText1 As String, ByVal strText2 As String, ByVal strText3 As String, ByVal dblTtlAmount As Double, ByVal strDate As String, ByVal strTime As String, ByVal int1stColumnX As Integer, ByVal int2ndColumnX As Integer, ByVal int3rdColumnX As Integer, ByVal strFontName As String, ByVal dblFontSize As Short, ByVal bFontBold As String, ByVal bFontItalic As String)
        Dim MyPrintFont As Font = New Font(strFontName, dblFontSize)
        Dim MyBrush As Brush = New SolidBrush(Color.Black)
        Dim MyStringFormat As New StringFormat()
        Dim tmpTtlAmt As String

        tmpTtlAmt = "RM" & dblTtlAmount.ToString("#,##0.00")

        MyPrintFont = New Font(MyPrintFont, FontStyle.Regular)
        If bFontBold = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Bold)
        If bFontItalic = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Italic)

        MyGraphic.DrawString(strText1, MyPrintFont, MyBrush, int1stColumnX, m_intCurrY)
        MyGraphic.DrawString(strText2, MyPrintFont, MyBrush, int2ndColumnX, m_intCurrY)
        MyGraphic.DrawString(strText3, MyPrintFont, MyBrush, int3rdColumnX, m_intCurrY)
        SetCurrentY(MyPrintFont.Height)

        MyGraphic.DrawString(tmpTtlAmt, MyPrintFont, MyBrush, int1stColumnX, m_intCurrY)
        MyGraphic.DrawString(strDate, MyPrintFont, MyBrush, int2ndColumnX, m_intCurrY)
        MyGraphic.DrawString(strTime, MyPrintFont, MyBrush, int3rdColumnX, m_intCurrY)
        SetCurrentY(MyPrintFont.Height)
    End Sub

    Public Sub PrintBody2(ByVal MyGraphic As Graphics, ByVal strText1 As String, ByVal strText2 As String, ByVal strText3 As String, ByVal strTxnType As String, ByVal strTxnFrom As String, ByVal strTxnTo As String, ByVal strBranchName As String, ByVal int1stColumnX As Integer, ByVal int2ndColumnX As Integer, ByVal int3rdColumnX As Integer, ByVal strFontName As String, ByVal dblFontSize As Short, ByVal bFontBold As String, ByVal bFontItalic As String)
        Dim MyPrintFont As Font = New Font(strFontName, dblFontSize)
        Dim MyBrush As Brush = New SolidBrush(Color.Black)
        Dim MyStringFormat As New StringFormat()
        Dim tmpBranchName As String = ""
        Dim tmpAccLabel As String = ""

        MyPrintFont = New Font(MyPrintFont, FontStyle.Regular)
        If bFontBold = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Bold)
        If bFontItalic = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Italic)

        MyGraphic.DrawString(strText1, MyPrintFont, MyBrush, int1stColumnX, m_intCurrY)
        MyGraphic.DrawString(strText2, MyPrintFont, MyBrush, int2ndColumnX, m_intCurrY)
        MyGraphic.DrawString(strText3, MyPrintFont, MyBrush, int3rdColumnX, m_intCurrY)
        SetCurrentY(MyPrintFont.Height)

        MyGraphic.DrawString(strTxnFrom, MyPrintFont, MyBrush, int1stColumnX, m_intCurrY)

        If strTxnType = "FT" Then
            tmpAccLabel = "OTHER A/C"

        ElseIf strTxnType = "CD" Then
            If strTxnTo = "OWNEDACC" Then
                tmpAccLabel = "WADIAHSA"

            ElseIf strTxnTo = "OTHERACC" Then
                tmpAccLabel = "OTHER A/C"
            End If

        End If
        MyGraphic.DrawString(tmpAccLabel, MyPrintFont, MyBrush, int2ndColumnX, m_intCurrY)

        If strBranchName.Length > 20 Then
            tmpBranchName = Left(strBranchName, 20)

        Else
            tmpBranchName = strBranchName
        End If

        MyGraphic.DrawString(tmpBranchName, MyPrintFont, MyBrush, int3rdColumnX, m_intCurrY)
        SetCurrentY(MyPrintFont.Height)
    End Sub

    Public Sub PrintBody3(ByVal MyGraphic As Graphics, ByVal strText1 As String, ByVal strAccNo As String, ByVal dblAccBal As Double, ByVal int1stColumnX As Integer, ByVal strFontName As String, ByVal dblFontSize As Short, ByVal bFontBold As String, ByVal bFontItalic As String)
        Dim MyPrintFont As Font = New Font(strFontName, dblFontSize)
        Dim MyBrush As Brush = New SolidBrush(Color.Black)
        Dim MyStringFormat As New StringFormat()
        Dim tmpStr As String = ""

        MyPrintFont = New Font(MyPrintFont, FontStyle.Regular)
        If bFontBold = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Bold)
        If bFontItalic = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Italic)

        If strText1 = "Cash Deposit" Then
            tmpStr = "OTHER A/C =       " & strAccNo
            MyGraphic.DrawString(tmpStr, MyPrintFont, MyBrush, int1stColumnX, m_intCurrY)
        ElseIf strText1 = "Fund Transfer" Then
            tmpStr = strText1 & " =  " & CStr(dblAccBal)
            MyGraphic.DrawString(tmpStr, MyPrintFont, MyBrush, int1stColumnX, m_intCurrY)
        End If

        SetCurrentY(MyPrintFont.Height)
    End Sub


    Public Sub PrintLine(ByVal MyGraphic As Graphics, ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer)

        Dim MyPen As New Pen(Color.Black, 0)
        MyGraphic.DrawLine(MyPen, X1, Y1, X2, Y2)
        SetCurrentY(5)

        'Dim strtest As String = "asdasd" & vbCrLf

    End Sub

    Public Sub PrintLineSAuto(ByVal MyGraphic As Graphics, ByVal intCurrentY As Integer, ByVal intLeftMargin As Integer, ByVal intRightMargin As Integer, ByVal intPrinterWidth As Integer, ByVal intPrinterHeight As Integer)

        Dim MyPen As New Pen(Color.Black, 0)
        MyGraphic.DrawLine(MyPen, intLeftMargin, CurrYInfo, intPrinterWidth - intRightMargin, CurrYInfo)
        SetCurrentY(5)

    End Sub

    Public Sub BlankLine(ByVal MyGraphic As Graphics)
        SetCurrentY(14)
    End Sub

    Public Sub SendPrintImgFormat(ByVal e As Graphics, ByVal strImgPath As String, ByVal strType As Integer, ByVal intImgWidth As Integer, ByVal intImgHeight As Integer, ByVal blnBorder As Boolean)
        Dim strErrMsg As String
        Try
            Select Case strType
                Case 2
                    'PrintLine(e, intImgWidth, intImgHeight)
                Case 3
                    DrawImg(e, strImgPath, intImgWidth, intImgHeight, blnBorder)
            End Select

        Catch ex As Exception
            strErrMsg = "Error in Send Print Image Format " & ex.Message
            'objCLSLog.AppEvtLog(LOGINFO, strErrMsg)
        End Try
    End Sub

    Public Sub DrawImg(ByVal MyGraphic As Graphics, ByVal strImgPath As String, _
                       ByVal intPrintWidth As Integer, ByVal intPrintHeight As Integer, _
                       ByVal blnBorder As Boolean)

        Dim intActualX As Integer
        If My.Computer.FileSystem.FileExists(strImgPath) Then

            intActualX = Int((300 - intPrintWidth) / 2)
            'intActualX = 50
            If blnBorder = True Then
                DrawImgBorder(MyGraphic, intPrintWidth, intPrintHeight + 2)
            End If

            Dim MyImgRect As New Rectangle(10, CurrYInfo, intPrintWidth, intPrintHeight)

            'Dim img As Bitmap = New Bitmap(Image.FromFile(strImgPath).Width, Image.FromFile(strImgPath).Height)
            MyGraphic.DrawImage(Image.FromFile(strImgPath), MyImgRect)

        End If
    End Sub

    Private Sub DrawImgBorder(ByVal MyGraphic As Graphics, ByVal intWidth As Single, ByVal intHeight As Single)
        Dim MyPen As New Pen(Color.Black, 1)
        MyGraphic.DrawRectangle(MyPen, 0, 0, intWidth, intHeight)
    End Sub

    Public Sub DrawRect(ByVal MyGraphic As Graphics, ByVal intPenWidth As Integer, ByVal intWidth As Single, ByVal intHeight As Single)

        Dim MyPen As New Pen(Color.Black, intPenWidth)
        MyGraphic.DrawRectangle(MyPen, 0, 0, intWidth, intHeight)

    End Sub

    Public Sub DoPrintReceiptLogo(ByVal e As Graphics, ByVal strLogoPath As String)
        Dim strErrMsg As String
        Dim strText As String
        Dim strImgPath As String
        Dim imgHeader As Image
        Dim intImgWidth As Integer
        Dim intImgHeight As Integer
        Dim intRadio As Double

        Try
            strText = ""
            strImgPath = strLogoPath
            If My.Computer.FileSystem.FileExists(strImgPath) Then
                imgHeader = Image.FromFile(strImgPath)
                intImgWidth = imgHeader.Width : intImgHeight = imgHeader.Height

                intRadio = intImgWidth / 290
                SendPrintImgFormat(e, strImgPath, 3, intImgWidth / intRadio, intImgHeight / intRadio, False)
            End If
        Catch ex As Exception
            strErrMsg = "Error in PrintReceiptLogo " & ControlChars.NewLine & ex.Message
            'objCLSLog.AppEvtLog(LOGINFO, strErrMsg)
        End Try
        SetCurrentY(intImgHeight / intRadio)
    End Sub

    Public Sub DoPrintReceiptData(ByVal MyGraphic As Graphics, ByVal strText1 As String, ByVal intLeftMargin As Integer, ByVal strFontName As String, ByVal dblFontSize As Short, ByVal bFontBold As String, ByVal bFontItalic As String)
        Dim strTextArr() As String = Nothing
        strTextArr = strText1.Split(vbCrLf)

        Dim MyPrintFont As Font = New Font(strFontName, dblFontSize)
        Dim MyBrush As Brush = New SolidBrush(Color.Black)
        Dim MyStringFormat As New StringFormat()

        MyPrintFont = New Font(MyPrintFont, FontStyle.Regular)
        If bFontBold = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Bold)
        If bFontItalic = "Y" Then MyPrintFont = New Font(MyPrintFont, FontStyle.Italic)

        For i = 0 To (strTextArr.Length - 1)
            MyGraphic.DrawString(strTextArr(i), MyPrintFont, MyBrush, intLeftMargin, m_intCurrY)
            SetCurrentY(MyPrintFont.Height)
        Next

        MyGraphic.DrawString("", MyPrintFont, MyBrush, intLeftMargin, m_intCurrY)
        SetCurrentY(MyPrintFont.Height)

    End Sub

#End Region


End Class
